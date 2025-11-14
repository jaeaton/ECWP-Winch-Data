namespace ViewModels
{
    internal partial class LiveDataHandlingViewModel : ViewModelBase
    {
        private UnitConversionViewModel ucVM = new();
        private ChartDataViewModel chartVM = new ChartDataViewModel();
        //private LiveDataHawboldtViewModel hawboldtProcessingVM = new LiveDataHawboldtViewModel();
        public int i = 0;

        //public SerialPort _serialPort = new SerialPort();
        public List<SerialPort> serialPorts = new List<SerialPort>();

        public List<UdpClient> udpClients = new List<UdpClient>();
        public SerialPort InputSerialPort = new SerialPort();

        //Asynchronious method to allow application to still respond to user interaction
        public async void GetDataAsync(WinchModel winch)
        {
            string dataIn;
            //ChartDataViewModel chartVM = new ChartDataViewModel(winch);
            //Setup data outputs
            if (winch.AllOutputCommunication.Count > 0)
            {
                await SetupOutputs(winch);
            }
            switch (winch.InputCommunication.CommunicationType)
            {
                case "Serial":
                    {
                        Parity InputParity = new Parity();
                        StopBits InputStopBits = new StopBits();
                        int.TryParse(winch.InputCommunication.BaudRate, out int serialBaudRate);
                        int.TryParse(winch.InputCommunication.DataBits, out int serialDataBits);
                        if (winch.InputCommunication.Parity == "N") { InputParity = Parity.None; }
                        else if (winch.InputCommunication.Parity == "E") { InputParity = Parity.Even; }
                        else if (winch.InputCommunication.Parity == "O") { InputParity = Parity.Odd; }
                        else { InputParity = Parity.None; }

                        if (winch.InputCommunication.StopBits == "1") { InputStopBits = StopBits.One; }
                        else if (winch.InputCommunication.StopBits == "1.5") { InputStopBits = StopBits.OnePointFive; }
                        else if (winch.InputCommunication.StopBits == "2") { InputStopBits = StopBits.Two; }
                        else { InputStopBits = StopBits.One; }

                        InputSerialPort.PortName = winch.InputCommunication.SerialPort;
                        InputSerialPort.BaudRate = serialBaudRate;
                        InputSerialPort.Parity = InputParity;
                        InputSerialPort.DataBits = serialDataBits;
                        InputSerialPort.StopBits = InputStopBits;
                        InputSerialPort.ReadTimeout = 2000;

                        InputSerialPort.Open();
                        //Task<string> t = new Task<string>(() => { ReadSerialData(InputSerialPort, winch); }, winch.Canceller.Token) ;
                        bool cancelled = false;
                        while (cancelled == false) //!winch.Canceller.Token.IsCancellationRequested)
                        {
                            try
                            {
                                //Asynchronious read of data to allow for other operations to occur
                                dataIn = await Task.Run(() => ReadSerialData(InputSerialPort, winch), winch.Canceller.Token);
                                //Show raw input data if selected
                                if (winch.ShowRawInput == true)
                                {
                                    winch.LiveData.RawWireData = dataIn;
                                }
                                ParseWinchData(dataIn, winch);
                            }
                            catch (Exception ae)
                            {
                                if (ae is TaskCanceledException)
                                {
                                    cancelled = true;
                                    break;
                                }
                                else
                                {
                                    cancelled = true;
                                    break;
                                }
                            }
                        }

                        if (InputSerialPort.IsOpen)
                        {
                            InputSerialPort.Close();
                            InputSerialPort.Dispose();
                        }
                    }
                    break;

                case "Network":
                    {
                        switch (winch.InputCommunication.CommunicationProtocol)
                        {
                            case "TCP Client":
                                {
                                    TcpListener server = null;
                                    TcpClient client = null;

                                    try
                                    {
                                        // Set the TcpListener to selected port
                                        Int32 port = int.Parse(winch.InputCommunication.PortNumber);
                                        //should be look up local host
                                        //TODO change to look up local host
                                        IPAddress localAddr = IPAddress.Parse(winch.InputCommunication.TcpIpAddress);

                                        // TcpListener server = new TcpListener(port);
                                        server = new TcpListener(localAddr, port);

                                        // Start listening for client requests.
                                        server.Start();
                                        // Perform a blocking call to accept requests.
                                        // You could also use server.AcceptSocket() here.
                                        client = server.AcceptTcpClient();
                                        bool cancelled = false;
                                        // Enter the listening loop.
                                        while (cancelled == false)//!winch.Canceller.Token.IsCancellationRequested)
                                        {
                                            try
                                            {
                                                //Asynchronous read of data to allow for other operations to occur
                                                dataIn = await Task.Run(() => ReadTCPData(client, winch), winch.Canceller.Token);
                                                //Show raw input data if selected
                                                if (winch.ShowRawInput == true)
                                                {
                                                    winch.LiveData.RawWireData = dataIn;
                                                }
                                                //read data
                                                ParseWinchData(dataIn, winch);
                                            }
                                            catch (Exception ae)
                                            {
                                                if (ae is TaskCanceledException)
                                                {
                                                    cancelled = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    cancelled = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    catch (SocketException e)
                                    {
                                        string msg = $"SocketException: {e.Message}";
                                        await MessageBoxViewModel.DisplayMessage(msg);
                                    }
                                    server.Stop();
                                    if (client != null)
                                    {
                                        client.Close();
                                        client.Dispose();
                                    }
                                }
                                break;

                            case "TCP Server":

                                {
                                    TcpClient client = new TcpClient();
                                    //client.ReceiveTimeout = 20000;
                                    //Check to see if TCP client is connected and if not connect
                                    try
                                    {
                                        if (!client.Connected)
                                        {
                                            if (!client.ConnectAsync(IPAddress.Parse(winch.InputCommunication.TcpIpAddress), int.Parse(winch.InputCommunication.PortNumber)).Wait(10000))
                                            {
                                                // connection failure
                                                await MessageBoxViewModel.DisplayMessage("Failed to connect to TCP Server");
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
                                        await MessageBoxViewModel.DisplayMessage($"ArgumentNullException: {e.Message}");
                                    }
                                    catch (SocketException e)
                                    {
                                        await MessageBoxViewModel.DisplayMessage($"SocketException: {e.Message}");
                                    }
                                    catch (ObjectDisposedException e)
                                    {
                                        await MessageBoxViewModel.DisplayMessage($"ObjectDisposeException: {e.Message}");
                                    }
                                    //Looks for cancellation token to stop data collection
                                    if (client.Connected)
                                    {
                                        bool cancelled = false;
                                        while (cancelled == false)
                                        {
                                            try
                                            {
                                                //Asynchronious read of data to allow for other operations to occur
                                                dataIn = await Task.Run(() => ReadTCPData(client, winch), winch.Canceller.Token);

                                                //Show raw input data if selected
                                                if (winch.ShowRawInput == true)
                                                {
                                                    winch.LiveData.RawWireData = dataIn;
                                                }

                                                //read data
                                                ParseWinchData(dataIn, winch);
                                            }
                                            catch (Exception ae)
                                            {
                                                if (ae is TaskCanceledException)
                                                {
                                                    cancelled = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    cancelled = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    //Close TCP client
                                    client.Close();
                                    client.Dispose();
                                }
                                break;

                            case "UDP":

                                {
                                    // Set the UDPClient to selected port
                                    Int32 port = int.Parse(winch.InputCommunication.PortNumber);
                                    UdpClient client = new UdpClient(port);
                                    try
                                    {
                                        bool cancelled = false;
                                        // Enter the listening loop.
                                        while (cancelled == false)
                                        {
                                            try
                                            {
                                                //Asynchronious read of data to allow for other operations to occur
                                                dataIn = await Task.Run(() => ReadUDPData(client, winch), winch.Canceller.Token);
                                                //Show raw input data if selected
                                                if (winch.ShowRawInput == true)
                                                {
                                                    winch.LiveData.RawWireData = dataIn;
                                                }
                                                //read data
                                                ParseWinchData(dataIn, winch);
                                            }
                                            catch (Exception ae)
                                            {
                                                if (ae is TaskCanceledException)
                                                {
                                                    //await MessageBoxViewModel.DisplayMessage("cancelled?");
                                                    cancelled = true;
                                                    client.Close();
                                                    break;
                                                }
                                                else
                                                {
                                                    await MessageBoxViewModel.DisplayMessage(ae.ToString());
                                                    cancelled = true;
                                                    client.Close();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    catch (SocketException e)
                                    {
                                        string msg = $"SocketException: {e.Message}";
                                        await MessageBoxViewModel.DisplayMessage(msg);
                                        client.Close();
                                    }
                                    client.Close();



                                }
                                break;

                            default:
                                {
                                    await MessageBoxViewModel.DisplayMessage("Input network communication selected with out a selected protocol");
                                }

                                break;
                        }
                    }

                    break;

                default:
                    {
                        await MessageBoxViewModel.DisplayMessage("Input communication not set");
                    }
                    break;
            }

            //Close Output communications
            if (udpClients.Count > 0)
            {
                foreach (var udpClient in udpClients)
                {
                    udpClient.Close();
                    //udpClient.Dispose();
                }
            }
            if (serialPorts.Count > 0)
            {
                foreach (SerialPort serialPort in serialPorts)
                {
                    serialPort.Close();
                }
            }

            //free up canceller resources
            winch.Canceller.Dispose();
            if (winch.LogMax == true)
            {
                //Write the max data for the cast
                //WriteMaxData(winch);
                ExcelViewModel.AddCastData(winch.MaxData.MaxTension, winch.MaxData.MaxPayout, int.Parse(winch.CastNumber), winch);
                winch.MaxData.Clear();
                //Increase the cast count
                winch.CastNumber = (int.Parse(winch.CastNumber) + 1).ToString();
                //UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);
            }
            winch.StartStopButtonText = "Start Log";
            MainViewModel._configDataStore.UserInputsEnable = true;
        }

        public void DisplayData(DataPointModel latest, WinchModel winch)
        {
            //Write data to bound variables to display on UI
            winch.LiveData.Tension = latest.Tension.ToString();
            winch.LiveData.Payout = latest.Payout.ToString();
            winch.LiveData.Speed = latest.Speed.ToString();
            //Write data to graphing view model
            winch.ChartData.AddData(latest, winch.LiveData, winch.ChartTimeSpan);
            //Set text color based on tension thresholds
            if (latest.TMWarnings.IndexOf("1") > 0)
            {
                winch.LiveData.TensionColor = "yellow";
                if (latest.TMAlarms.IndexOf("1") > 0)
                {
                    winch.LiveData.TensionColor = "red";
                }
            }
            else
            {
                winch.LiveData.TensionColor = "";
            }
        }

        private void MaxValues(WinchModel winch)
        {
            //Write max data to bound variables to display on UI
            winch.LiveData.MaxSpeed = winch.MaxData.MaxSpeed.Speed.ToString();
            winch.LiveData.MaxPayout = winch.MaxData.MaxPayout.Payout.ToString();
            winch.LiveData.MaxTension = winch.MaxData.MaxTension.Tension.ToString();
        }

        private string ReadUDPData(UdpClient udpClient, WinchModel winch)
        {
            string responseData = string.Empty;
            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Blocks until a message returns on this socket from a remote host.
            Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
            if (winch.InputCommunication.IsHawboldt)
            {
                LiveDataHawboldtViewModel hawboldtProcessingVM = new LiveDataHawboldtViewModel();
                responseData = hawboldtProcessingVM.HawboldtProcess(receiveBytes, winch.InputCommunication.HawboldtModel);
            }
            else
            {
                responseData = Encoding.ASCII.GetString(receiveBytes);
            }

            return responseData;
        }

        private string ReadSerialData(SerialPort serial, WinchModel winch)
        {
            string responseData = serial.ReadLine();
            return responseData;
        }

        private string ReadTCPData(TcpClient client, WinchModel winch)//object tcpCom)
        {
            Byte[] data; //= System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            data = new Byte[256];

            // String to store the response ASCII representation.
            string responseData;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Uncomment to show all data in Wire Data Field
            //winch.LiveData.RawWireData = responseData;

            return responseData;
        }

        private string ReplaceNonPrintableCharacters(string s, char replaceWith)
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

        private void ParseWinchData(string lines, WinchModel winch)
        {
            //Parse incoming data function and store in DataPointModel
            bool maxChange = false;
            bool getTime = false;

            lines = lines.Replace("$WIR", Environment.NewLine + "$WIR");
            //lines = lines.Replace("Cable Length", Environment.NewLine + "Cable Length");
            string pattern = @"Cable Length";
            string replacement = Environment.NewLine + pattern;
            lines = Regex.Replace(lines, pattern, replacement);
            string[] strings = lines.Split(Environment.NewLine,
                            StringSplitOptions.RemoveEmptyEntries);
            winch.LiveData.SplitWireData = strings[0];
            foreach (var line in strings)
            {
                string data = line.Replace("\0", string.Empty);
                data = ReplaceNonPrintableCharacters(data, ' ');
                //Splits the string into an array when character is found
                string[] strIn = data.Split(',', 'T', '*');
                string strID = string.Empty;

                //Keep the '0' in R30
                if (strIn[0] == "$R30C")
                {
                    strID = strIn[0];
                }
               else if (strIn[0].Contains("RD"))
                {
                    strID = "RD";
                }
                else if (strIn[0].Contains("Cable Length"))
                {
                    strID = "ODIM";
                }
                else
                {
                    strID = strIn[0].Replace("0", string.Empty);
                    strID = strID.Replace(" ", string.Empty);
                }
                DataPointModel latest = new DataPointModel();
                //Uncomment to log all data coming in
                //WriteRawLog(data, winch);
                //UNOLS String input
                latest.StringID = "empty";

                //winch.LiveData.RawWireData = data;
                switch (strID)
                {
                    //Wire Log Header
                    case "%WIR":
                        if (winch.Log20Hz)
                        {
                            Write20HzDataHeader(data, winch);
                        }
                        break;
                    //Winch Log Header
                    case "%WNC":
                        WriteWinchLog(data, winch);
                        break;
                    //Winch Log
                    case "$WNC":
                        //winch.LiveData.RawWinchData = data;
                        WriteWinchLog(data, winch);
                        break;
                    //UNOLS String Wire Log
                    case "$WIR":
                        if (strIn.Length == 9)
                        {
                            latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6], strIn[7], strIn[8]);
                        }
                        break;

                    case "NULL":
                        if (strIn.Length == 9)
                        {
                            latest = new DataPointModel("$WIR", strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6], strIn[7], strIn[8]);
                        }
                        break;

                    case "RD":
                        //MTNW Legacy input (does not include date and time)
                        if (strIn.Length == 5)
                        {
                            getTime = true;
                            latest = new DataPointModel(strID, "", "", strIn[1], strIn[2], strIn[3], strIn[4]);
                        }
                        //MTNW 1 input  (Includes date and time)
                        else if (strIn.Length == 7)
                        {
                            latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], strIn[6]);
                        }
                        break;
                    //Hawboldt SPRE-3648 UDP String
                    //Godzilla
                    case "$HWIR1":
                        latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], " ");
                        break;
                    //Hawboldt PRE-2648RS UDP string
                    case "$HWIR2":
                        latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], " ");
                        break;
                    //Hawboldt SPRE-2640RS UDP String
                    case "$HWIR3":
                        latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], " ");
                        break;
                    //Hawboldt SPRE-2036S UDP String
                    //WCWP Hawboldt small winches
                    case "$HWIR4":
                        latest = new DataPointModel(strID, strIn[1], strIn[2], strIn[3], strIn[4], strIn[5], " ");
                        break;
                    //Mermac R30 string
                    case "$R30C":
                        latest = new DataPointModel(strID, strIn[2], strIn[3], strIn[4]);
                        getTime = true;
                        break;
                    //ODIM
                    case "ODIM":
                        //string[] values = new string[6];
                        //i = 0;
                        //if (strIn.Length == 3)
                        //{
                        //    foreach (var val in strIn)
                        //    {
                        //        string[] value = val.Split(':');
                        //        values[i] = value[0];
                        //        i++;
                        //        values[i] = value[1];
                        //        i++;
                        //    }
                        if (strIn.Length == 5 && strIn[0].Contains("Length"))
                        {
                            string[] payout = strIn[0].Split(':');
                            string[] speed = strIn[1].Split(":");
                            string[] tension = strIn[4].Split(":");
                            getTime = true;
                            latest = new DataPointModel("ODIM","","", tension[1], speed[1], payout[1],"");                            
                        }
                        break;                       

                    default:
                        if (strIn.Length == 9 && winch.InputCommunication.DataProtocol == "3PS")
                        {
                            getTime = true;
                            latest = new DataPointModel("$3PS", "", "", strIn[0], strIn[2], strIn[1], "");
                        }
                        break;
                }

                if (latest.StringID != "empty")
                {
                    //Convert tension if needed
                    if (winch.ConvertTension)
                    {
                        latest.Tension = ucVM.ConvertTension(latest.Tension);
                    }
                    //Convert Payout if needed
                    if (winch.ConvertPayout)
                    {
                        latest.Payout = ucVM.ConvertPayout(latest.Payout);
                    }
                    //Convert Speed if needed
                    if (winch.ConvertSpeed)
                    {
                        latest.Speed = ucVM.ConvertSpeed(latest.Speed);
                    }

                    //If needed changes data and time stamp to local machine
                    if (winch.UseComputerTime || getTime)
                    {
                        DateTime dateTime = DateTime.Now;
                        latest.Date = dateTime.ToString("yyyyMMdd");
                        latest.Time = dateTime.ToString("HH:mm:ss.fff");
                        getTime = false;
                    }
                    //Check for max value change
                    if (winch.MaxData.MaxTension.Tension < latest.Tension)
                    {
                        winch.MaxData.MaxTension = latest;
                        maxChange = true;
                    }
                    if (Math.Abs(winch.MaxData.MaxSpeed.Speed) < latest.Speed)
                    {
                        winch.MaxData.MaxSpeed = latest;
                        maxChange = true;
                    }
                    if (winch.MaxData.MaxPayout.Payout < latest.Payout)
                    {
                        winch.MaxData.MaxPayout = latest;
                        maxChange = true;
                    }
                    if (maxChange)
                    {
                        MaxValues(winch);
                    }
                    //Look for TM warnings and alarms and set valvues
                    if (winch.TensionWarningLevel != string.Empty && winch.TensionAlarmLevel != string.Empty)
                    {
                        if (latest.Tension > float.Parse(winch.TensionWarningLevel))
                        {
                            latest.TMAlarms = "00000001";
                        }
                        if (latest.Tension > float.Parse(winch.TensionAlarmLevel))
                        {
                            latest.TMAlarms = "00000001";
                        }
                    }

                    //Write data to logfile
                    if (winch.Log20Hz && !winch.AutoLog)
                    {
                        Write20HzData(latest, winch);
                    }
                    if (winch.AutoLog)
                    {
                        if (latest.Payout > Convert.ToDouble(winch.StopLogPayout) && latest.Tension > Convert.ToDouble(winch.StopLogTension))
                        {
                            Write20HzData(latest, winch);
                        }
                        else
                        {
                            winch.Canceller.Cancel();
                        }
                    }
                    if (udpClients.Count > 0)
                    {
                        foreach (var client in udpClients)
                        {
                            Send20HzData(latest, winch, client);
                        }
                    }
                    if (serialPorts.Count > 0)
                    {
                        foreach (var port in serialPorts)
                        {
                            SendSerialData(latest, winch, port);
                        }
                    }

                    DisplayData(latest, winch);
                }

                //}
            }
        }

        private void Write20HzData(DataPointModel data, WinchModel winch)
        {
            // Write Data to files
            string line;
            string destPath;
            string fileName;

            fileName = winch.UnolsWireLogName;
            destPath = System.IO.Path.Combine(winch.RawLogDirectory, fileName);
            line = $"{data.StringID},{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms},{data.CheckSum}";

            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }
        }

        private void Write20HzDataHeader(string data, WinchModel winch)
        {
            // Write Data to files
            string line;
            string destPath;
            string fileName;
            fileName = winch.UnolsWireLogName;
            destPath = System.IO.Path.Combine(winch.RawLogDirectory, fileName);
            line = data;

            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }
        }

        private void WriteWinchLog(string data, WinchModel winch)
        {
            //Write Data to files
            string fileName = winch.WinchLogName;
            string destPath = System.IO.Path.Combine(winch.RawLogDirectory, fileName);
            string line = data;
            using (StreamWriter stream = new StreamWriter(destPath, append: true))
            {
                stream.WriteLine(line);
            }
        }

        //Currently not used. Could be desirable to write a file as it comes from the instrument
        //private void WriteRawLog(string data, WinchModel winch)
        //{
        //    //Write Data to files
        //    string fileName = $"raw_{winch.WinchName}.log";
        //    string destPath = System.IO.Path.Combine(winch.RawLogDirectory, fileName);
        //    string line = $"{data}";
        //    using (StreamWriter stream = new StreamWriter(destPath, append: true))
        //    {
        //        stream.WriteLine(line);
        //    }
        //}

        private void Send20HzData(DataPointModel data, WinchModel winch, UdpClient client)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (winch.UdpFormatUnols)
            {
                line = $"$WIR,{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms},{data.CheckSum}";
            }
            //MTNW 1 Format
            else
            {
                line = $"RD,{data.Date}T{data.Time},{data.Tension},{data.Speed},{data.Payout},";
            }
            //MTNW Legacy
            //else
            //{
            //    line = $"RD,{data.Tension},{data.Speed},{data.Payout},";
            //}

            //Add checksum
            int checkSum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
            Array.ForEach(asciiBytes, delegate (byte i) { checkSum += i; });
            line = $"{line}{checkSum}" + Environment.NewLine;
            //Send UDP packet
            byte[] sendBytes = Encoding.ASCII.GetBytes(line);

            client.SendAsync(sendBytes, sendBytes.Length);
        }

        private void SendSerialData(DataPointModel data, WinchModel winch, SerialPort _serialPort)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (winch.SerialFormatUnols)
            {
                line = $"WIR,{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms}, {data.CheckSum}";
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

            //Serial Port Transmit
            _serialPort.WriteLine(line);
        }

        public void WriteMaxData(WinchModel winch)
        {
            //Write Max data to file
            //DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            //string dateAndHour = dateTime.ToString("yyyyMMddHH");
            //FileOperationsViewModel.SetFileNames(winch);
            //string fileName = winch.MaxWireLogName;
            //string destPath = Path.Combine(MainViewModel._configDataStore.DirectoryLabel, fileName);
            //string[] lines =
            //{
            //    $"Cast { winch.CastNumber }",
            //    $"Field, Date, Time, Tension, Speed, Payout",
            //    $"Max Tension: { winch.MaxData.MaxTension.Date }, { winch.MaxData.MaxTension.Time }, { winch.MaxData.MaxTension.Tension }, { winch.MaxData.MaxTension.Speed }, { winch.MaxData.MaxTension.Payout}",
            //    $"Max Payout: { winch.MaxData.MaxPayout.Date }, { winch.MaxData.MaxPayout.Time }, { winch.MaxData.MaxPayout.Tension }, { winch.MaxData.MaxPayout.Speed }, { winch.MaxData.MaxPayout.Payout }",
            //    $" "
            //};
            //using (StreamWriter stream = new StreamWriter(destPath, append: true))
            //{
            //    foreach (string line in lines)
            //        stream.WriteLine(line);
            //}
            ExcelViewModel.AddCastData(winch.MaxData.MaxTension, winch.MaxData.MaxPayout, int.Parse(winch.CastNumber), winch);
            //Clear max data
            winch.MaxData.Clear();
        }

        private async Task<UdpClient?> CreateUdpOutputAsync(CommunicationModel output)
        {
            if (!IPAddress.TryParse(output.TcpIpAddress, out IPAddress remoteIp) ||
       !int.TryParse(output.PortNumber, out int remotePort))
            {
                await MessageBoxViewModel.DisplayMessage("Invalid UDP output address/port.");
                return null;
            }

            try
            {
                // Do not bind to the remote address. Create an unbound client and connect to the remote.
                var udp = new UdpClient();
                udp.Connect(remoteIp, remotePort);
                return udp;
            }
            catch (Exception ex)
            {
                await MessageBoxViewModel.DisplayMessage($"Failed to create UDP output: {ex.Message}");
                return null;
            }
        }
        private async Task<SerialPort?> CreateSerialPortAsync(CommunicationModel output)
        {
            if (string.IsNullOrWhiteSpace(output.SerialPort))
            {
                await MessageBoxViewModel.DisplayMessage("Serial port name is empty for output.");
                return null;
            }

            if (!int.TryParse(output.BaudRate, out int baudRate))
            {
                await MessageBoxViewModel.DisplayMessage($"Invalid baud rate for serial output: {output.BaudRate}");
                return null;
            }

            if (!int.TryParse(output.DataBits, out int dataBits))
            {
                await MessageBoxViewModel.DisplayMessage($"Invalid data bits for serial output: {output.DataBits}");
                return null;
            }

            Parity parity = Parity.None;
            if (output.Parity == "E") parity = Parity.Even;
            else if (output.Parity == "O") parity = Parity.Odd;

            StopBits stopBits = StopBits.One;
            if (output.StopBits == "1.5") stopBits = StopBits.OnePointFive;
            else if (output.StopBits == "2") stopBits = StopBits.Two;

            try
            {
                var sp = new SerialPort(output.SerialPort, baudRate, parity, dataBits, stopBits);
                sp.Open();
                return sp;
            }
            catch (Exception ex)
            {
                await MessageBoxViewModel.DisplayMessage($"Failed to open serial output {output.SerialPort}: {ex.Message}");
                return null;
            }
        }
        public async Task SetupOutputs(WinchModel winch)
        {
            if (winch.AllOutputCommunication.Count > 0)
            {
                foreach (var output in winch.AllOutputCommunication)
                {
                    if (output == null) continue;
                    if (output.CommunicationType == "Network" && output.CommunicationProtocol == "UDP")

                    {
                        //int i = udpClients.Count;
                        //if (IPAddress.TryParse(output.TcpIpAddress, out IPAddress ipAddress) && int.TryParse(output.PortNumber, out int portNumber))
                        //{
                        //    //Initial setup of UDP client
                        //    UdpClient client = new UdpClient(portNumber);
                        //    //Format IPEndPoint
                        //    IPEndPoint iPEndPoint = new(ipAddress, portNumber);
                        //    //Connect sets the UDP destination to the specified endpoint
                        //    client.Connect(iPEndPoint);
                        //    //Add to list of UDP clients
                        //    udpClients[i] = client;
                        //}

                        //else if (output.CommunicationProtocol == "TCP Server")
                        //{
                        //}
                        //else if(output.CommunicationProtocol == "TCP Client")
                        //{
                        //}

                        var udp = await CreateUdpOutputAsync(output);
                        if (udp != null)
                        {
                            udpClients.Add(udp);
                        }
                            
                    }
                    else if (output.CommunicationType == "Serial")
                    {
                        //int i = serialPorts.Count;
                        //int.TryParse(output.BaudRate, out int serialBaudRate);
                        //int.TryParse(output.DataBits, out int dataBits);
                        //Parity outParity = new Parity();
                        //StopBits outStopBits = new StopBits();
                        //if (output.Parity == "N") { outParity = Parity.None; }
                        //else if (output.Parity == "E") { outParity = Parity.Even; }
                        //else if (output.Parity == "O") { outParity = Parity.Odd; }
                        //else { outParity = Parity.None; }

                        //if (output.StopBits == "1") { outStopBits = StopBits.One; }
                        //else if (output.StopBits == "1.5") { outStopBits = StopBits.OnePointFive; }
                        //else if (output.StopBits == "2") { outStopBits = StopBits.Two; }
                        //else { outStopBits = StopBits.One; }

                        //serialPorts[i].PortName = output.SerialPort;
                        //serialPorts[i].BaudRate = serialBaudRate;
                        //serialPorts[i].Parity = outParity;
                        //serialPorts[i].DataBits = dataBits;
                        //serialPorts[i].StopBits = outStopBits;
                        //serialPorts[i].Open();

                        var sp = await CreateSerialPortAsync(output);
                        if (sp != null)
                        {
                            serialPorts.Add(sp);
                        }
                            
                    }
                }
            }
        }
    }
}