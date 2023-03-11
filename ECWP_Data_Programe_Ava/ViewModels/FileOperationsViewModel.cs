namespace ViewModels
{
    internal class FileOperationsViewModel
    {
        public static object SetFileNames(WinchModel winch)
        {
            ConfigDataStore _confDataStore = MainWindowViewModel._configDataStore;
            //Create File Names
            DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            string dateAndHour = dateTime.ToString("yyyyMMddHH");
            string dateOnly = dateTime.ToString("yyyyMM");
            winch.MtnwWireLogName = $"{ dateAndHour }_{ _confDataStore.CruiseNameBox }_cast_{winch.CastNumber }_{winch.WinchName}_short.log";
            winch.UnolsWireLogName = $"{ dateAndHour }_{ _confDataStore.CruiseNameBox }_cast_{ winch.CastNumber }_{winch.WinchName}_UNOLS.log";
            winch.WinchLogName = $"{ dateOnly }_{ winch.WinchName }_Winch.log";
            winch.MaxWireLogName = $"{ dateTime.ToString("yyyyMM") }_{ _confDataStore.CruiseNameBox }_{winch.WinchName}.log";
            return winch;
        }
        public static void WriteConfig(ConfigDataStore _configDataStore)
        {
            //Logic to save new parameters to config file
            //Set filname
            string fileName = "ecwp_dataconf.txt";
            //Set path to save config file (Application directory)
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            ConfigDataStore conf = _configDataStore;
            //Remove old file
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }
            foreach (WinchModel winch in conf.AllWinches)
            {
                if (winch == null)
                {
                    break;
                }
                //Populate array with configuartion values
                string[] lines =
                    {
                    $"Winch Name,{ winch.WinchName}",
                    $"Receive IP,{ winch.InputCommunication.TcpIpAddress }",
                    $"Receive Port,{ winch.InputCommunication.PortNumber }",
                    $"Transmit IP,{ winch.OutputCommunication.TcpIpAddress }",
                    $"Transmit Port,{ winch.OutputCommunication.PortNumber }",
                    $"Cruise Name,{ _configDataStore.CruiseNameBox }",
                    $"Cast Number,{ winch.CastNumber }",
                    $"Send UDP,{ winch.UdpOutput }",
                    $"UDP String Format,{ winch.UdpFormat }",
                    $"Save 20Hz Data,{ winch.Log20Hz }",
                    $"20Hz File Format,{ winch.LogFormat }",
                    $"Save Max Values,{ winch.LogMax }",
                    $"Use Computer Time,{ winch.UseComputerTime }",
                    $"Save Location,{ _configDataStore.DirectoryLabel }",
                    //$"UNOLS File Format, {globalConfig.LogUnolsSwitch }",
                    $"Send Serial,{ winch.SerialOutput }",
                    $"Serial String Format,{ winch.SerialFormat }",
                    $"Serial Port Name,{ winch.SerialPortOutput }",
                    $"Serial Baud Rate,{ winch.BaudRateOutput }",
                    $"Input Communication Type,{  winch.CommunicationType }"
                    };
                //Write each line of array using stream writer
                using (StreamWriter stream = new StreamWriter(destPath, true))
                {
                    foreach (string line in lines)
                        stream.WriteLine(line);
                }
            }
            
        }
        public static void ReadConfig(ConfigDataStore _configDataStore)
        {
            //Logic to read config file for initial setup based on previous saved data
            List<string> lines = new List<string>();
            //set the name of the config file
            string fileName = "ecwp_dataconf.txt";
            //set the path to application directory
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            WinchModel winch = new();
            //winch = null;
            WinchConfigurationViewModel viewModel = new WinchConfigurationViewModel();
            try
            {
                using (StreamReader stream = new StreamReader(destPath))
                {
                    string text;
                    while ((text = stream.ReadLine()) != null)
                    {
                        lines.Add(text);
                    }
                    foreach (var line in lines)
                    {
                        int delim = line.IndexOf(",");
                        if (line.Substring(0, delim) == "Winch Name")
                        {
                            if ( winch.WinchName != null)
                            {
                                viewModel.InsertWinch(winch);
                                winch = new();
                            }
                            winch.WinchName = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Receive IP")
                        {
                            winch.InputCommunication.TcpIpAddress = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Receive Port")
                        {
                            winch.InputCommunication.PortNumber = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Transmit IP")
                        {
                            winch.OutputCommunication.TcpIpAddress = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Transmit Port")
                        {
                            winch.OutputCommunication.PortNumber = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Cruise Name")
                        {
                            _configDataStore.CruiseNameBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Cast Number")
                        {
                            if( int.TryParse(line.Substring(delim + 1), out int castCount))// + 1;
                            {
                                winch.CastNumber = castCount.ToString();
                            }
                                
                        }
                        if (line.Substring(0, delim) == "Send UDP")
                        {
                            winch.UdpOutput = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Save 20 Hz Data")
                        {
                            winch.Log20Hz = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Save Max Values")
                        {
                            winch.LogMax = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Use Computer Time")
                        {
                            winch.UseComputerTime = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Save Location")
                        {
                            _configDataStore.DirectoryLabel = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "UDP String Format")
                        {
                            winch.UdpFormat = line.Substring(delim + 1);
                            if (winch.UdpFormat == "UNOLS")
                            {
                                winch.UdpFormatUnols = true;
                                winch.UdpFormatMtnw = false;
                            }
                            else
                            {
                                winch.UdpFormatUnols = false;
                                winch.UdpFormatMtnw = true;
                            }
                        }
                        if (line.Substring(0, delim) == "20Hz File Format")
                        {
                            winch.LogFormat = line.Substring(delim + 1);
                            if (winch.LogFormat == "UNOLS")
                            {
                                winch.LogFormatUnols = true;
                                winch.LogFormatMtnw = false;
                            }
                            else
                            {
                                winch.LogFormatUnols = false;
                                winch.LogFormatMtnw = true;
                            }
                        }
                        if (line.Substring(0, delim) == "Serial String Format")
                        {
                            winch.SerialFormat = line.Substring(delim + 1);
                            if (winch.SerialFormat == "UNOLS")
                            {
                                winch.SerialFormatUnols = true;
                                winch.SerialFormatMtnw = false;
                            }
                            else
                            {
                                winch.SerialFormatUnols = false;
                                winch.SerialFormatMtnw = true;
                            }
                        }
                        if (line.Substring(0, delim) == "Send Serial")
                        {
                            winch.SerialOutput = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Serial Port Name")
                        {
                            winch.SerialPortOutput = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Serial Baud Rate")
                        {
                           winch.BaudRateOutput = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Input Communication Type")
                        {
                            winch.CommunicationType = line.Substring(delim + 1);
                        }
                    }
                    //GlobalConfigModel globalConfig = new GlobalConfigModel();
                    ////update global config to the parameters loaded
                    //globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
                    //return globalConfig;
                    //_configDataStore.CurrentWinch = winch.ShallowCopy();
                    //if (winch.WinchName != null)
                    //{
                    //    viewModel.InsertWinch(winch);
                    //}
                    viewModel.InsertWinch(winch);

                }
                
            }
            catch
            {
               
            }
        }
        
        
    }
}
