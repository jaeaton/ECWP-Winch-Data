using LiveChartsCore;

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
                List<string> lines = new();
                if (winch.WinchName != null)
                {
                    lines.Add($"Winch Name,{winch.WinchName}");
                }
                if (winch.InputCommunication.TcpIpAddress  != null)
                {
                    bool valid = ValidateIPViewModel.ValidateIPFunction(winch.InputCommunication.TcpIpAddress);
                    if (valid)
                    {
                        lines.Add($"Receive IP,{ winch.InputCommunication.TcpIpAddress }");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage(
                            $"{winch.WinchName}\n"+
                            $"Input IP Address not valid");
                        break;
                    }
                    
                }
                if (winch.InputCommunication.PortNumber != null)
                {
                    bool valid = ValidateIPViewModel.ValidatePortFunction(winch.InputCommunication.PortNumber);
                    if (valid)
                    {
                        lines.Add($"Receive Port,{winch.InputCommunication.PortNumber}");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                            $"Input Port number not valid");
                        break;
                    }
                    
                }
                if (winch.OutputCommunication.TcpIpAddress != null)
                {
                    bool valid = ValidateIPViewModel.ValidateIPFunction(winch.OutputCommunication.TcpIpAddress);
                    if (valid)
                    {
                        lines.Add($"Transmit IP,{winch.OutputCommunication.TcpIpAddress}");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                            $"Output IP Address not valid");
                        break;
                    }
                    
                }
                if (winch.OutputCommunication.PortNumber != null)
                {
                    bool valid = ValidateIPViewModel.ValidatePortFunction (winch.OutputCommunication.PortNumber);
                    if (valid)
                    {
                        lines.Add($"Transmit Port,{winch.OutputCommunication.PortNumber}");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                            $"Output Port Number not Valid");
                        break;
                    }
                    
                }
                if (_configDataStore.CruiseNameBox != null)
                {
                    bool valid = ValidateCruiseViewModel.ValidateCruiseName(_configDataStore.CruiseNameBox);
                    if (valid)
                    {
                        lines.Add($"Cruise Name,{_configDataStore.CruiseNameBox}");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage("Cruise name not valid.");
                        break;
                    }
                }
                if (winch.CastNumber != null)
                {
                    bool valid = ValidateCruiseViewModel.ValidateCastNumber(winch.CastNumber);
                    if (valid)
                    {
                        lines.Add($"Cast Number,{winch.CastNumber}");
                    }
                    else
                    {
                        MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                            $"Cast number not valid.");
                        break;
                    }
                }
                if (winch.UdpOutput != null)
                {
                    lines.Add($"Send UDP,{ winch.UdpOutput }");
                }
                if (winch.UdpFormat != null)
                {
                    lines.Add($"UDP String Format,{ winch.UdpFormat }");
                }
                if (winch.Log20Hz!= null)
                {
                    lines.Add($"Save 20Hz Data,{ winch.Log20Hz }");
                }
                if (winch.LogFormat != null)
                {
                    lines.Add($"20Hz File Format,{ winch.LogFormat }");
                }
                if (winch.LogMax != null)
                {
                    lines.Add($"Save Max Values,{ winch.LogMax }");
                }
                if (winch.UseComputerTime != null)
                {
                    lines.Add($"Use Computer Time,{ winch.UseComputerTime }");
                }
                if (_configDataStore.DirectoryLabel != null)
                {
                    lines.Add($"Save Location,{ _configDataStore.DirectoryLabel }");
                }
                if (winch.SerialOutput != null)
                {
                    lines.Add($"Send Serial,{ winch.SerialOutput }");
                }
                if (winch.SerialFormat != null)
                {
                    lines.Add($"Serial String Format,{ winch.SerialFormat }");
                }
                if (winch.SerialPortOutput != null)
                {
                    lines.Add($"Serial Port Name,{ winch.SerialPortOutput }");
                }
                if (winch.BaudRateOutput != null)
                {
                    lines.Add($"Serial Baud Rate,{ winch.BaudRateOutput }");
                }
                if (winch.CommunicationType != null)
                {
                    lines.Add($"Input Communication Type,{  winch.CommunicationType }");
                }
                if (winch.TensionUnit != null)
                {
                    lines.Add($"Tension Units,{ winch.TensionUnit }");
                }
                if (winch.PayoutUnit != null)
                {
                    lines.Add($"Payout Units,{ winch.PayoutUnit }");
                }
                if (winch.SpeedUnit != null)
                {
                    lines.Add($"Speed Units,{  winch.SpeedUnit }");
                }
                if (winch.AutoLog != null)
                {
                    lines.Add($"Auto Log,{ winch.AutoLog}");
                }
                if (winch.StopLogPayout != null)
                {
                    lines.Add($"Auto Log Stop Payout,{winch.StopLogPayout }");
                }
                if (winch.StopLogTension != null)
                {
                    lines.Add($"Auto Log Stop Tension,{ winch.StopLogTension }");
                }
                if (winch.TensionWarningLevel != null)
                {
                    lines.Add($"Tension Warning Level,{ winch.TensionWarningLevel }");
                }
                if (winch.TensionAlarmLevel != null)
                {
                    lines.Add($"Tension Alarm Level,{ winch.TensionAlarmLevel }");
                }
                if (winch.AssignedBreakingLoad != null)
                {
                    lines.Add($"Assigned Breaking Load,{ winch.AssignedBreakingLoad }");
                }
                if (winch.ProtocolHawboldt == true)
                {
                    lines.Add($"HawboldtModel,{winch.HawboldtModel}");
                }
                if (winch.ChartTimeSpan != null)
                {
                    lines.Add($"Chart Time Span,{winch.ChartTimeSpan}");
                }

                //Write each line of array using stream writer
                using (StreamWriter stream = new StreamWriter(destPath, true))
                {
                    lines.Add("-----");
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
                        if (line.Contains(","))
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
                                if (_configDataStore.DirectoryLabel != null)
                                {
                                    _configDataStore.DirectorySet = true;
                                }
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
                            if (line.Substring(0, delim) == "Tension Units")
                            {
                                winch.TensionUnit = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Payout Units")
                            {
                                winch.PayoutUnit = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Speed Units")
                            {
                                winch.SpeedUnit = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Auto Log")
                            {
                                winch.AutoLog = bool.Parse(line.Substring(delim + 1));
                            }
                            if (line.Substring(0, delim) == "Auto Log Stop Payout")
                            {
                                winch.StopLogPayout = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Auto Log Stop Tension")
                            {
                                winch.StopLogTension = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Tension Warning Level")
                            {
                                winch.TensionWarningLevel = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Tension Alarm Level")
                            {
                                winch.TensionAlarmLevel = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Assigned Breaking Load")
                            {
                                winch.AssignedBreakingLoad = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "HawboldtModel")
                            {
                                winch.HawboldtModel = line.Substring(delim + 1);
                                winch.ProtocolHawboldt = true;
                            }
                            if (line.Substring(0, delim) == "Chart Time Span")
                            {
                                winch.ChartTimeSpan = line.Substring(delim + 1);
                            }
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
