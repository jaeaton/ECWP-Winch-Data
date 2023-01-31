namespace ViewModels
{
    internal class FileOperationsViewModel
    {
        public static object SetFileNames(GlobalConfigModel globalConfig)
        {
            //Create File Names
            DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            string dateAndHour = dateTime.ToString("yyyyMMddHH");
            string dateOnly = dateTime.ToString("yyyyMMdd");
            globalConfig.Minimal20HzLogFileName = $"{ dateAndHour }_{ globalConfig.CruiseInformation.CruiseName }_cast_{ globalConfig.CruiseInformation.CastNumber }_short.log";
            globalConfig.UnolsWireLogName = $"{ dateAndHour }_{ globalConfig.CruiseInformation.CruiseName }_cast_{ globalConfig.CruiseInformation.CastNumber }_UNOLS.log";
            globalConfig.UnolsWinchLogName = $"{ dateOnly }_Winch.log";
            globalConfig.MaxLogFileName = $"{ dateTime.ToString("yyyyMM") }_{ globalConfig.CruiseInformation.CruiseName }.log";
            return globalConfig;
        }
        public static void WriteConfig(GlobalConfigModel globalConfig)
        {
            //Logic to save new parameters to config file
            //Set filname
            string fileName = "dataconfig.txt";
            //Set path to save config file (Application directory)
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            //Populate array with global configuartion values
            string[] lines =
                {
                $"Receive IP,{ globalConfig.ReceiveCommunication.IPAddress }",
                $"Receive Port,{ globalConfig.ReceiveCommunication.PortNumber }",
                $"Transmit IP,{ globalConfig.TransmitCommunication.IPAddress }",
                $"Transmit Port,{ globalConfig.TransmitCommunication.PortNumber }",
                $"Cruise Name,{ globalConfig.CruiseInformation.CruiseName }",
                $"Cast Number,{ globalConfig.CruiseInformation.CastNumber }",
                $"Send UDP,{ globalConfig.UDPSwitch }",
                $"Save 20 Hz Data,{ globalConfig.Log20HzSwitch }",
                $"Log Max Values,{ globalConfig.LogMaxValuesSwitch }",
                $"Use Computer Time,{ globalConfig.UseComputerTimeSwitch }",
                $"Save Location,{ globalConfig.SaveDirectory }",
                $"UNOLS String,{ globalConfig.UnolsUdpFormatSet }",
                $"UNOLS File Format, {globalConfig.LogUnolsSwitch }",
                $"Send Serial,{ globalConfig.SerialSwitch }",
                $"UNOLS Serial String,{ globalConfig.UnolsSerialFormatSet }",
                $"Serial Port Name,{ globalConfig.SerialPortName }",
                $"Serial Baud Rate,{ globalConfig.SerialPortBaud }",
                $"Selected Winch,{ globalConfig.WinchSelection }"
                };
            //Write each line of array using stream writer
            using (StreamWriter stream = new StreamWriter(destPath))
            {
                foreach (string line in lines)
                    stream.WriteLine(line);
            }
        }
        public static object ReadConfig(ConfigDataStore _configDataStore)
        {
            //Logic to read config file for initial setup based on previous saved data
            List<string> lines = new List<string>();
            //set the name of the config file
            string fileName = "dataconfig.txt";
            //set the path to application directory
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
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
                        if (line.Substring(0, delim) == "Receive IP")
                        {
                            _configDataStore.IpAddressInputSourceBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Receive Port")
                        {
                            _configDataStore.PortInputSourceBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Transmit IP")
                        {
                            _configDataStore.IpAddressInputDestinationBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Transmit Port")
                        {
                            _configDataStore.PortInputDestinationBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Cruise Name")
                        {
                            _configDataStore.CruiseNameBox = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Cast Number")
                        {
                            int castCount = int.Parse(line.Substring(delim + 1));// + 1;
                            _configDataStore.CastNumberBox = castCount.ToString();
                        }
                        if (line.Substring(0, delim) == "Send UDP")
                        {
                            _configDataStore.SendDataCheckBox = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Save 20 Hz Data")
                        {
                            _configDataStore.Log20HzDataCheckBox = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Log Max Values")
                        {
                            _configDataStore.LogMaxDataCheckBox = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Use Computer Time")
                        {
                            _configDataStore.UseComputerTimeCheckBox = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Save Location")
                        {
                            _configDataStore.DirectoryLabel = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "UNOLS String")
                        {
                            _configDataStore.UnolsUDPStringButton = bool.Parse(line.Substring(delim + 1));
                            if (!(bool)_configDataStore.UnolsUDPStringButton)
                            {
                                //if UNOLS format is not selected, select MTNW formate
                                _configDataStore.UnolsUDPStringButton = false;
                                _configDataStore.MtnwUDPStringButton = true;
                            }
                            if ((bool)_configDataStore.UnolsUDPStringButton)
                            {
                                //Select UNOLS format
                                _configDataStore.MtnwUDPStringButton = false;
                                _configDataStore.UnolsUDPStringButton = true;
                            }
                        }
                        if (line.Substring(0, delim) == "UNOLS File Format")
                        {
                            _configDataStore.UnolsWireLogButton = bool.Parse(line.Substring(delim + 1));
                            if (!(bool)_configDataStore.UnolsWireLogButton)
                            {
                                //if UNOLS format is not selected, select MTNW formate
                                _configDataStore.UnolsWireLogButton = false;
                                _configDataStore.MtnwWireLogButton = true;
                            }
                            if ((bool)_configDataStore.UnolsWireLogButton)
                            {
                                //Select UNOLS format
                                _configDataStore.MtnwWireLogButton = false;
                                _configDataStore.UnolsWireLogButton = true;
                            }
                        }
                        if (line.Substring(0, delim) == "UNOLS Serial String")
                        {
                            _configDataStore.UnolsSerialStringButton = bool.Parse(line.Substring(delim + 1));
                            if (!(bool)_configDataStore.UnolsSerialStringButton)
                            {
                                //if UNOLS format is not selected, select MTNW formate
                                _configDataStore.UnolsSerialStringButton = false;
                                _configDataStore.MtnwSerialStringButton = true;
                            }
                            if ((bool)_configDataStore.UnolsWireLogButton)
                            {
                                //Select UNOLS format
                                _configDataStore.MtnwSerialStringButton = false;
                                _configDataStore.UnolsSerialStringButton = true;
                            }
                        }
                        if (line.Substring(0, delim) == "Send Serial")
                        {
                            _configDataStore.SendSerialDataCheckBox = bool.Parse(line.Substring(delim + 1));
                        }
                        if (line.Substring(0, delim) == "Serial Port Name")
                        {
                            _configDataStore.SerialPortName = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Serial Baud Rate")
                        {
                            _configDataStore.BaudRate = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Selected Winch")
                        {
                            _configDataStore.WinchSelection = line.Substring(delim + 1);
                        }
                    }
                    GlobalConfigModel globalConfig = new GlobalConfigModel();
                    //update global config to the parameters loaded
                    globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
                    return globalConfig;
                }
            }
            catch
            {
                return null;
            }
        }
        
        
    }
}
