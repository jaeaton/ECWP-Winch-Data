namespace ViewModels
{
    public class DataHandlingViewModel
    {
        public static LiveDataDataStore _liveData = new();
        public static MaxDataPointModel maxData = new MaxDataPointModel();
        public static int i = 0;
        public static SerialPort _serialPort = new SerialPort();
        //Asynchronious method to allow application to still respond to user interaction
        public static async void GetDataAsync(GlobalConfigModel globalConfig)
        {
            if(globalConfig.SerialSwitch)
            {
                int.TryParse(globalConfig.SerialPortBaud,out int serialBaudRate);
                //_serialPort = new SerialPort(globalConfig.SerialPortName,serialBaudRate,Parity.None,8,StopBits.One);
                _serialPort.PortName = globalConfig.SerialPortName;
                _serialPort.BaudRate = serialBaudRate;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Open();
            }
            string dataIn;
            if (globalConfig.WinchSelection == "LCI-90i") 
            {
                TcpListener server = null;
                TcpClient client = null;
                try
                {
                    // Set the TcpListener to selected port 
                    Int32 port = int.Parse(globalConfig.ReceiveCommunication.PortNumber);
                    //should be look up local host
                    //TODO change to look up local host
                    IPAddress localAddr = IPAddress.Parse(globalConfig.ReceiveCommunication.IPAddress);

                    // TcpListener server = new TcpListener(port);
                    server = new TcpListener(localAddr, port);

                    // Start listening for client requests.
                    server.Start();
                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    client = server.AcceptTcpClient();

                    // Enter the listening loop.
                    while (!StartStopSaveView._canceller.Token.IsCancellationRequested)
                    {
                        
                        
                        //Asynchronious read of data to allow for other operations to occur
                        dataIn = await Task.Run(() => ReadTCPData(client));
                        //_liveData.RawWireData = dataIn;
                        //read data
                        ParseWinchData(dataIn, globalConfig);

                    }
                }
                catch (SocketException e)
                {
                    string msg = $"SocketException: {e.Message}";
                    MessageBoxViewModel.DisplayMessage(msg);
                }
                server.Stop();
                    if (client != null)
                    {
                        client.Close();
                        client.Dispose();
                    }
                    
                
            }
            else
            {
                TcpClient client = new TcpClient();
                //client.ReceiveTimeout = 20000;
                //Check to see if TCP client is connected and if not connect
                try
                {


                    if (!client.Connected)
                    {
                        if (!client.ConnectAsync(IPAddress.Parse(globalConfig.ReceiveCommunication.IPAddress), int.Parse(globalConfig.ReceiveCommunication.PortNumber)).Wait(5000))
                        {
                            // connection failure
                            MessageBoxViewModel.DisplayMessage("Failed to connect to TCP Server");
                        }
                        //client.Connect(IPAddress.Parse(globalConfig.ReceiveCommunication.IPAddress), int.Parse(globalConfig.ReceiveCommunication.PortNumber));
                    }
                }
                //catch (IOException e)
                //{
                //    MessageBox.Show($"IOException: { e.Message }");
                //}
                catch (ArgumentNullException e)
                {
                    MessageBoxViewModel.DisplayMessage($"ArgumentNullException: {e.Message}");
                }
                catch (SocketException e)
                {
                    MessageBoxViewModel.DisplayMessage($"SocketException: {e.Message}");
                }
                catch (ObjectDisposedException e)
                {
                    MessageBoxViewModel.DisplayMessage($"ObjectDisposeException: {e.Message}");
                }
                //Looks for cancellation token to stop data collection
                if (client.Connected)
                {
                    while (!StartStopSaveView._canceller.Token.IsCancellationRequested)
                    {
                        //Asynchronious read of data to allow for other operations to occur
                        dataIn = await Task.Run(() => ReadTCPData(client));
                        //_liveData.RawWireData = dataIn;
                        //read data
                        ParseWinchData(dataIn, globalConfig);

                    }
                }
                //Close TCP client
                client.Close();
                client.Dispose();
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }            
            //free up canceller resources
            StartStopSaveView._canceller.Dispose();
            MainWindowViewModel._configDataStore.StartStopButtonText = "Start Log";
            MainWindowViewModel._configDataStore.UserInputsEnable = true;


        }
        public static void DisplayData(DataPointModel latest)
        {
            //Write data to bound variables to display on UI
            _liveData.Tension = latest.Tension.ToString();
            _liveData.Payout = latest.Payout.ToString();
            _liveData.Speed = latest.Speed.ToString();
            ChartDataViewModel.AddData(latest);
        }
        private static void MaxValues()
        {
            //Write max data to bound variables to display on UI
            _liveData.MaxSpeed = maxData.MaxSpeed.Speed.ToString();
            _liveData.MaxPayout = maxData.MaxPayout.Payout.ToString();
            _liveData.MaxTension = maxData.MaxTension.Tension.ToString();
        }
        private static string ReadTCPData(TcpClient client)//object tcpCom)
        {
            
            Byte[] data; //= System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Uncomment to show all data in Wire Data Field
            _liveData.RawWireData = responseData;
            
            return responseData;
            
        }
        private static string ReplaceNonPrintableCharacters(string s, char replaceWith)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                byte b = (byte)c;
                if (b < 32)
                    result.Append(replaceWith);
                else
                    result.Append(c);
            }
            return result.ToString();
        }
        private static void ParseWinchData(string lines, GlobalConfigModel globalConfig)
        {
            //Parse incoming data function and store in DataPointModel
            bool maxChange = false;
            bool getTime = false;

            lines = lines.Replace("$WIR", Environment.NewLine + "$WIR");
            string[] strings = lines.Split(Environment.NewLine,
                            StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in strings)
            {
                string data = line.Replace("\0", string.Empty);
                data = ReplaceNonPrintableCharacters(data, ' ');
                string[] strIn = data.Split(',', 'T');
                DataPointModel latest = new DataPointModel();
                //Uncomment to log all data coming in
                //WriteRawLog(data, globalConfig);
                //UNOLS String input

                if (strIn[0].Contains("%WIR"))
                {
                    if (globalConfig.Log20HzSwitch)
                    {
                        Write20HzDataHeader(data, globalConfig);
                    }
                }
                else if (strIn[0].Contains("%WNC"))
                {
                    WriteWinchLog(data, globalConfig);
                }
                else if (strIn[0].Contains("$WNC"))
                {
                    _liveData.RawWinchData = data;
                    WriteWinchLog(data, globalConfig);
                }
                else
                {
                    //_liveData.RawWireData = data;
                    if (strIn.Length == 9 && strIn[0].Contains("$WIR"))
                    {

                        latest = new DataPointModel(strIn[0], strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6], strIn[7], strIn[8]);
                    }
                    //MTNW Legacy input (does not include date and time)
                    else if (strIn.Length == 5 && strIn[0].Contains("RD"))
                    {
                         getTime = true;
                         latest = new DataPointModel(strIn[0], "", "", strIn[1], strIn[2], strIn[3], strIn[4]);
                                                
                    }
                    //MTNW 1 input  (Includes date and time)
                    else if (strIn.Length == 7 && strIn[0].Contains("RD"))
                    {

                        latest = new DataPointModel(strIn[0], strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6]);
                    }
                    else
                    {
                        return;
                    }
                    //DataPointModel latest = new DataPointModel(strIn[0], strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6]);
                    //If needed changes data and time stamp to local machine
                    if (globalConfig.UseComputerTimeSwitch || getTime)
                    {
                        DateTime dateTime = DateTime.Now;
                        latest.Date = dateTime.ToString("yyyyMMdd");
                        latest.Time = dateTime.ToString("HH:mm:ss.fff");
                        getTime = false;
                    }
                    //Check for max value change
                    if (maxData.MaxTension.Tension < latest.Tension)
                    {
                        maxData.MaxTension = latest;
                        maxChange = true;
                    }
                    if (Math.Abs(maxData.MaxSpeed.Speed) < latest.Speed)
                    {
                        maxData.MaxSpeed = latest;
                        maxChange = true;
                    }
                    if (maxData.MaxPayout.Payout < latest.Payout)
                    {
                        maxData.MaxPayout = latest;
                        maxChange = true;
                    }
                    if (maxChange)
                    {
                        MaxValues();
                    }
                    //Write data to logfile
                    if (globalConfig.Log20HzSwitch)
                    {
                        Write20HzData(latest, globalConfig);
                    }
                    if (globalConfig.UDPSwitch)
                    {
                        Send20HzData(latest, globalConfig);
                    }
                    if (globalConfig.SerialSwitch)
                    {
                        SendSerialData(latest, globalConfig);
                    }

                    DisplayData(latest);
                }
            }
        }
        private static void Write20HzData(DataPointModel data, GlobalConfigModel globalConfig)
        {
            // Write Data to files
            string line;
            string destPath;
            string fileName;
            if (!globalConfig.LogUnolsSwitch)
            {
                fileName = globalConfig.Minimal20HzLogFileName;
                destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
                line = $"RD,{ data.Date }T{ data.Time },{ data.Tension },{ data.Speed },{ data.Payout }";
            }
            else
            {
                fileName = globalConfig.UnolsWireLogName;
                destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
                line = $"{ data.StringID },{ data.Date },{ data.Time },{ data.Tension },{ data.Speed },{ data.Payout },{ data.TMWarnings },{ data.TMAlarms },{ data.CheckSum }";
            }
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }

        }
        private static void Write20HzDataHeader(string data, GlobalConfigModel globalConfig)
        {
            // Write Data to files
            string line;
            string destPath;
            string fileName;
                fileName = globalConfig.UnolsWireLogName;
                destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
                line = data;
            
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }

        }
        private static void WriteWinchLog(string data, GlobalConfigModel globalConfig)
        {
            //Write Data to files
            string fileName = globalConfig.UnolsWinchLogName;
            string destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
            string line = data;
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }
        }
        private static void WriteRawLog(string data, GlobalConfigModel globalConfig)
        {
            //Write Data to files
            string fileName = $"raw1.log";
            string destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
            string line = $"{data}";
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }
        }
        private static void Send20HzData(DataPointModel data, GlobalConfigModel globalConfig)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (globalConfig.UnolsUdpFormatSet)
            {
                line = $"WIR,{ data.Date },{ data.Time },{ data.Tension },{ data.Speed },{ data.Payout },{ data.TMWarnings},{data.TMAlarms},";
            }
            //Not UNOLS Format (MTNW 1)
            else
            {
                line = $"RD,{ data.Date }T{ data.Time },{ data.Tension },{ data.Speed },{ data.Payout },";
            }
            //Add checksum
            int checkSum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
            Array.ForEach(asciiBytes, delegate (byte i) { checkSum += i; });
            line = $"{ line }{ checkSum }";
            //Send UDP packet
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(IPAddress.Parse( globalConfig.TransmitCommunication.IPAddress), int.Parse(globalConfig.TransmitCommunication.PortNumber));
            byte[] sendBytes = Encoding.ASCII.GetBytes(line);
            udpClient.Send(sendBytes, sendBytes.Length);
        }
        private static void SendSerialData(DataPointModel data, GlobalConfigModel globalConfig)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (globalConfig.UnolsSerialFormatSet)
            {
                line = $"WIR,{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms},";
            }
            //Not UNOLS Format (MTNW 1)
            else
            {
                line = $"RD,{data.Date}T{data.Time},{data.Tension},{data.Speed},{data.Payout},";
            }
            //Add checksum
            int checkSum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
            Array.ForEach(asciiBytes, delegate (byte i) { checkSum += i; });
            line = $"{line}{checkSum}";
            //Send UDP packet
            //UdpClient udpClient = new UdpClient();
            //udpClient.Connect(IPAddress.Parse(globalConfig.TransmitCommunication.IPAddress), int.Parse(globalConfig.TransmitCommunication.PortNumber));
            //byte[] sendBytes = Encoding.ASCII.GetBytes(line);
            //udpClient.Send(sendBytes, sendBytes.Length);
            //Serial Port Transmit
            _serialPort.WriteLine(line);
        }
        public static void WriteMaxData(GlobalConfigModel globalConfig)
        {
            //Write Max data to file
            //DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            //string dateAndHour = dateTime.ToString("yyyyMMddHH");
            string fileName = globalConfig.MaxLogFileName;
            string destPath = Path.Combine(globalConfig.SaveDirectory, fileName);
            string[] lines =
            {
                $"Cast { globalConfig.CruiseInformation.CastNumber }",
                $"Field, Date, Time, Tension, Speed, Payout",
                $"Max Tension: { maxData.MaxTension.Date }, { maxData.MaxTension.Time }, { maxData.MaxTension.Tension }, { maxData.MaxTension.Speed }, { maxData.MaxTension.Payout}",
                $"Max Payout: { maxData.MaxPayout.Date }, { maxData.MaxPayout.Time }, { maxData.MaxPayout.Tension }, { maxData.MaxPayout.Speed }, { maxData.MaxPayout.Payout }",
                $" "
            };
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                foreach (string line in lines)
                    stream.WriteLine(line);
            }
            //Clear max data
            maxData.Clear();
        }
    }
}
