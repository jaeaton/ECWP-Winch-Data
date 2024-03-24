using LiveChartsCore;
using Store;

namespace ViewModels
{
    internal class FileOperationsViewModel
    {
        public static object SetFileNames(WinchModel winch)
        {
            ConfigDataStore _confDataStore = MainViewModel._configDataStore;
            //Create File Names
            DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            string dateAndHour = dateTime.ToString("yyyyMMddHH");
            string dateOnly = dateTime.ToString("yyyyMM");
            winch.MtnwWireLogName = $"{ dateAndHour }_{ _confDataStore.CruiseNameBox }_cast_{winch.CastNumber }_{winch.WinchName}_short.log";
            winch.UnolsWireLogName = $"{ dateAndHour }_{ _confDataStore.CruiseNameBox }_cast_{ winch.CastNumber }_{winch.WinchName}_UNOLS.log";
            winch.WinchLogName = $"{ dateOnly }_{ winch.WinchName }_Winch.log";
            winch.MaxWireLogName = $"{ dateTime.ToString("yyyyMM") }_{ _confDataStore.CruiseNameBox }_{winch.WinchName}.log";
            winch.WirePoolWireLogName = $"{dateTime.ToString("yyyy")}_{winch.WinchName}_Wire_Log";
            return winch;
        }
        public async static void WriteConfig(ConfigDataStore _configDataStore)
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
            List<string> lines = new();
            if (_configDataStore.ShipName != string.Empty)
            {
                lines.Add($"Ship Name,{_configDataStore.ShipName}");
            }
            else
            {
                await MessageBoxViewModel.DisplayMessage("Ship name not valid.");

            }
            if (_configDataStore.CruiseNameBox != string.Empty)
            {
                bool valid = ValidateCruiseViewModel.ValidateCruiseName(_configDataStore.CruiseNameBox);
                if (valid)
                {
                    lines.Add($"Cruise Name,{_configDataStore.CruiseNameBox}");
                }
                else
                {
                    await MessageBoxViewModel.DisplayMessage("Cruise name not valid.");
                    
                }
            }
            lines.Add("-----");
            foreach (WinchModel winch in conf.AllWinches)
            {
                if (winch == null)
                {
                    break;
                }
                //Populate array with winch configuartion values
                
                if (winch.WinchName != string.Empty)
                {
                    lines.Add($"Winch Name,{winch.WinchName}");
                }
                else
                {
                    await MessageBoxViewModel.DisplayMessage(
                           $"Winch Name must be entered.");
                    break;
                }
                if (winch.WinchManufacturer != string.Empty)
                {
                    lines.Add($"Winch Manufacturer,{winch.WinchManufacturer}");
                }
                if (winch.WinchModelName != string.Empty)
                {
                    lines.Add($"Winch Model,{winch.WinchModelName}");
                }
                if (winch.WinchSerialNumber != string.Empty)
                {
                    lines.Add($"Winch Serial Number,{winch.WinchSerialNumber}");
                }
                if (winch.Atlantis3PSWinchID != string.Empty)
                {
                    lines.Add($"3PS ID,{winch.Atlantis3PSWinchID}");
                }
                if (winch.InputCommunication.CommunicationType != string.Empty)
                {
                    if (winch.InputCommunication.CommunicationType == "Serial")
                    {
                        lines.Add($"Receive Communication Type,Serial");
                        if (winch.InputCommunication.SerialPort != string.Empty)
                        {
                            lines.Add($"Receive Serial Port Name,{winch.InputCommunication.SerialPort}");
                        }
                        if (winch.InputCommunication.BaudRate != string.Empty)
                        {
                            lines.Add($"Receive Serial Baud Rate,{winch.InputCommunication.BaudRate}");
                        }
                        if (winch.InputCommunication.Parity != string.Empty)
                        {
                            lines.Add($"Receive Parity,{winch.InputCommunication.Parity}");
                        }
                        if (winch.InputCommunication.StopBits != string.Empty)
                        {
                            lines.Add($"Receive Stop Bits,{winch.InputCommunication.StopBits}");
                        }
                        if (winch.InputCommunication.DataBits != string.Empty)
                        {
                            lines.Add($"Receive Data Bits,{winch.InputCommunication.DataBits}");
                        }
                        if (winch.InputCommunication.DataProtocol != string.Empty)
                        {
                            lines.Add($"Receive Data Protocol,{winch.InputCommunication.DataProtocol}");
                        }
                    }
                    if (winch.InputCommunication.CommunicationType == "Network")
                    {
                        lines.Add($"Receive Communication Type,Network");
                        if (winch.InputCommunication.TcpIpAddress != string.Empty)
                        {
                            bool valid = ValidateIPViewModel.ValidateIPFunction(winch.InputCommunication.TcpIpAddress);
                            if (valid)
                            {
                                lines.Add($"Receive IP,{winch.InputCommunication.TcpIpAddress}");
                            }
                            else
                            {
                                await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Input IP Address not valid");
                                break;
                            }
                        }
                        else
                        {
                            await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Input IP Address required");
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
                                await MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                                    $"Input Port number not valid");
                                break;
                            }

                        }
                        else
                        {
                            await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Input port number required");
                        }
                        if (winch.InputCommunication.CommunicationProtocol != string.Empty)
                        {
                            lines.Add($"Receive Communication Protocol,{winch.InputCommunication.CommunicationProtocol}");
                        }
                        else
                        {
                            await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Input communication protocol required");
                        }
                        if (winch.InputCommunication.DataProtocol != string.Empty)
                        {
                            lines.Add($"Receive Data Protocol,{winch.InputCommunication.DataProtocol}");
                        }
                        else
                        {
                            await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Input data procol required");
                        }
                    }
                }
                else
                {
                    await MessageBoxViewModel.DisplayMessage(
                           $"Input communications missing.");
                }
                if (winch.AllOutputCommunication != null)
                {
                    foreach (CommunicationModel com in  winch.AllOutputCommunication)
                    {
                        if(com.DestinationName != string.Empty)
                        {
                            if (com.CommunicationType == "Serial")
                            {
                                lines.Add($"Transmit Communication Type,Serial");
                                if (com.DestinationName != string.Empty)
                                {
                                    lines.Add($"Transmit Destination Name,{com.DestinationName}");
                                }
                                if (com.SerialPort != string.Empty)
                                {
                                    lines.Add($"Transmit Serial Port Name,{com.SerialPort}");
                                }
                                if (com.BaudRate != string.Empty)
                                {
                                    lines.Add($"Transmit Serial Baud Rate,{com.BaudRate}");
                                }
                                if (com.Parity != string.Empty)
                                {
                                    lines.Add($"Transmit Parity,{com.Parity}");
                                }
                                if (com.StopBits != string.Empty)
                                {
                                    lines.Add($"Transmit Stop Bits,{com.StopBits}");
                                }
                                if (com.DataBits  != string.Empty)
                                {
                                    lines.Add($"Transmit Data Bits,{com.DataBits}");
                                }
                                if (com.DataProtocol != string.Empty)
                                {
                                    lines.Add($"Transmit Data Protocol,{com.DataProtocol}");
                                }
                            }
                            if (com.CommunicationType == "Network")
                            {
                                lines.Add($"Transmit Communication Type,Network");
                                if (com.DestinationName != string.Empty)
                                {
                                    lines.Add($"Transmit Destination Name,{com.DestinationName}");
                                }
                                if (com.TcpIpAddress != string.Empty)
                                {
                                    bool valid = ValidateIPViewModel.ValidateIPFunction(com.TcpIpAddress);
                                    if (valid)
                                    {
                                        lines.Add($"Transmit IP,{com.TcpIpAddress}");
                                    }
                                    else
                                    {
                                        await MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                                            $"Output IP Address not valid");
                                        break;
                                    }

                                }
                                if (com.PortNumber != string.Empty)
                                {
                                    bool valid = ValidateIPViewModel.ValidatePortFunction(com.PortNumber);
                                    if (valid)
                                    {
                                        lines.Add($"Transmit Port,{com.PortNumber}");
                                    }
                                    else
                                    {
                                        await MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                                            $"Output Port Number not Valid");
                                        break;
                                    }
                                }
                                if (com.CommunicationProtocol != string.Empty)
                                {
                                    lines.Add($"Transmit Communication Protocol,{com.CommunicationProtocol}");
                                }
                                if (com.DataProtocol != string.Empty)
                                {
                                    lines.Add($"Transmit Data Protocol,{com.DataProtocol}");
                                }
                            }
                        }
                    }
                }

                if (winch.CastNumber != string.Empty)
                {
                    bool valid = ValidateCruiseViewModel.ValidateCastNumber(winch.CastNumber);
                    if (valid)
                    {
                        lines.Add($"Cast Number,{winch.CastNumber}");
                    }
                    else
                    {
                        await MessageBoxViewModel.DisplayMessage($"{winch.WinchName}\n" +
                            $"Cast number not valid.");
                        break;
                    }
                }
                lines.Add($"Save 20Hz Data,{ winch.Log20Hz }");
                lines.Add($"Save Max Values,{ winch.LogMax }");
                lines.Add($"Use Computer Time,{ winch.UseComputerTime }");
                if (winch.LogFormat != null)
                {
                    lines.Add($"20Hz File Format,{ winch.LogFormat }");
                }
                if (winch.WinchDirectory != string.Empty)
                {
                    lines.Add($"UNOLS Log Location,{ winch.WinchDirectory }");
                }
                if (winch.RawLogDirectory != string.Empty)
                {
                    lines.Add($"Raw Log Location,{winch.RawLogDirectory}");
                }

                if (winch.TensionUnit != string.Empty)
                {
                    lines.Add($"Tension Units,{ winch.TensionUnit }");
                }
                if (winch.PayoutUnit != string.Empty)
                {
                    lines.Add($"Payout Units,{ winch.PayoutUnit }");
                }
                if (winch.SpeedUnit != string.Empty)
                {
                    lines.Add($"Speed Units,{  winch.SpeedUnit }");
                }
                if (winch.WinchLogType != string.Empty)
                {
                    lines.Add($"Winch Log Type,{winch.WinchLogType}");
                }
                if (winch.MinimumPayout != string.Empty)
                {
                    lines.Add($"Minimum Logging Payout,{winch.MinimumPayout}");
                }
                if (winch.MinimumTension != string.Empty)
                {
                    lines.Add($"Minimum Logging Tension,{winch.MinimumTension}");
                }

                lines.Add($"Auto Log,{ winch.AutoLog}");
                if (winch.StopLogPayout != string.Empty)
                {
                    lines.Add($"Auto Log Stop Payout,{winch.StopLogPayout }");
                }
                if (winch.StopLogTension != string.Empty)
                {
                    lines.Add($"Auto Log Stop Tension,{ winch.StopLogTension }");
                }

                if (winch.TensionMemberName != string.Empty)
                {
                    lines.Add($"Tension Member Name,{winch.TensionMemberName}");
                }
                if (winch.TensionMemberManufacturer != string.Empty)
                {
                    lines.Add($"Tension Member Manufacturer,{winch.TensionMemberManufacturer}");

                }
                if (winch.TensionMemberPartNumber != string.Empty)
                {
                    lines.Add($"Tension Member Part Number,{winch.TensionMemberPartNumber}");
                }
                if (winch.TensionMemberNSFID != string.Empty)
                {
                    lines.Add($"Tension Member NSF ID,{winch.TensionMemberNSFID}");
                }
                if (winch.InstalledLength != default)
                {
                    lines.Add($"Installed Length,{ winch.InstalledLength }");
                }  
                if (winch.AvailableLength != default)
                {
                    lines.Add($"Available Length,{winch.AvailableLength}");
                }
                if (winch.AssignedBreakingLoad != string.Empty)
                {
                    lines.Add($"Assigned Breaking Load,{ winch.AssignedBreakingLoad }");
                }
                if (winch.FactorOfSafety != default)
                {
                    lines.Add($"Factor Of Safety,{winch.FactorOfSafety}");
                }

                if (winch.ChartTimeSpan != string.Empty)
                {
                    lines.Add($"Chart Time Span,{winch.ChartTimeSpan}");
                }
                lines.Add($"Plot Winch,{winch.PlotSelected}");
                
                //Write each line of array using stream writer
                using (StreamWriter stream = new StreamWriter(destPath, true))
                {
                    lines.Add("-----");
                    foreach (string line in lines) 
                    { 
                        stream.WriteLine(line);
                    }     
                }
                lines.Clear();
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
            CommunicationModel tempComms = new();
            WinchModel winch = new();
            //winch = null;
            WinchViewModel winchViewModel = new WinchViewModel();
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
                            if (line.Substring(0, delim) == "Ship Name")
                            {
                                _configDataStore.ShipName = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Cruise Name")
                            {
                                _configDataStore.CruiseNameBox = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Winch Name")
                            {
                                if ( winch.WinchName != string.Empty)
                                {
                                    winchViewModel.InsertWinch(winch);
                                    winch = new();
                                }
                                winch.WinchName = line.Substring(delim + 1);
                              
                            }
                            if (line.Substring(0, delim) == "Winch Manufacturer")
                            {
                                winch.WinchManufacturer = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Winch Model")
                            {
                                winch.WinchModelName = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Winch Serial Number")
                            {
                                winch.WinchSerialNumber = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "3PS ID")
                            {
                                winch.Atlantis3PSWinchID = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Communication Type")
                            {
                                winch.InputCommunication.CommunicationType = line.Substring(delim + 1);
                                if (winch.InputCommunication.CommunicationType == "Network")
                                {
                                    winch.InputCommunication.IsNetwork = true;
                                    winch.InputCommunication.IsSerial = false;
                                }
                                else if (winch.InputCommunication.CommunicationType == "Serial")
                                {
                                    winch.InputCommunication.IsNetwork = false;
                                    winch.InputCommunication.IsSerial = true;
                                }
                            }
                            if (line.Substring(0, delim) == "Receive IP")
                            {
                                winch.InputCommunication.TcpIpAddress = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Port")
                            {
                                winch.InputCommunication.PortNumber = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Communication Protocol")
                            {
                                winch.InputCommunication.CommunicationProtocol = line.Substring(delim + 1);
                                
                            }
                            if (line.Substring(0, delim) == "Receive Data Protocol")
                            {
                                winch.InputCommunication.DataProtocol = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Serial Port Name")
                            {
                                winch.InputCommunication.SerialPort = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Serial Baud Rate")
                            {
                                winch.InputCommunication.BaudRate = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Parity")
                            {
                                winch.InputCommunication.Parity = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Stop Bits")
                            {
                                winch.InputCommunication.StopBits = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Receive Data Bits")
                            {
                                winch.InputCommunication.DataBits = line.Substring(delim + 1);
                            }
                            
                            if (line.Substring(0, delim) == "Transmit Communication Type")
                            {
                                tempComms.CommunicationType = line.Substring(delim + 1);
                                if (tempComms.CommunicationType == "Network")
                                {
                                    tempComms.IsNetwork = true;
                                    tempComms.IsSerial = false;
                                }
                                else if (tempComms.CommunicationType == "Serial")
                                {
                                    tempComms.IsNetwork = false;
                                    tempComms.IsSerial = true;
                                }
                            }
                            if (line.Substring(0, delim) == "Transmit Destination Name")
                            {
                                tempComms.DestinationName = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Communication Protocol")
                            {
                                tempComms.CommunicationProtocol = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Serial Port Name")
                            {
                                tempComms.SerialPort = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Serial Baud Rate")
                            {
                                tempComms.BaudRate = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Parity")
                            {
                                tempComms.Parity = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Stop Bits")
                            {
                                tempComms.StopBits = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Data Bits")
                            {
                                tempComms.DataBits = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit IP")
                            {
                                tempComms.TcpIpAddress = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Port")
                            {
                                tempComms.PortNumber = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Transmit Data Protocol")
                            {
                                tempComms.DataProtocol = line.Substring(delim + 1);
                                if (tempComms.DestinationName != string.Empty)
                                {
                                    winch = InsertOutputComms(tempComms, winch);
                                    tempComms = new();
                                }
                                
                                
                            }

                            if (line.Substring(0, delim) == "Cast Number")
                            {
                                if( int.TryParse(line.Substring(delim + 1), out int castCount))// + 1;
                                {
                                    winch.CastNumber = castCount.ToString();
                                }
                                    
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
                            if (line.Substring(0, delim) == "UNOLS Log Location")
                            {
                                winch.WinchDirectory = line.Substring(delim + 1);
                                
                            }
                            if (line.Substring(0, delim) == "Raw Log Location")
                            {
                                winch.RawLogDirectory = line.Substring(delim + 1);
                                
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
                            if (line.Substring(0, delim) == "Winch Log Type")
                            {
                                winch.WinchLogType = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Minimum Logging Payout")
                            {
                                winch.MinimumPayout = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Minimum Logging Tension")
                            {
                                winch.MinimumTension = line.Substring(delim + 1);
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

                            if (line.Substring(0, delim) == "Tension Member Name")
                            {
                                winch.TensionMemberName = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Tension Member Manufacturer")
                            {
                                winch.TensionMemberManufacturer = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Tension Member Part Number")
                            {
                                winch.TensionMemberPartNumber = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Tension Member NSF ID")
                            {
                                winch.TensionMemberNSFID = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Installed Length")
                            {
                                winch.InstalledLength = float.Parse(line.Substring(delim + 1));
                            }
                            if (line.Substring(0, delim) == "Available Length")
                            {
                                winch.AvailableLength = float.Parse(line.Substring(delim + 1));
                            }
                            if (line.Substring(0, delim) == "Assigned Breaking Load")
                            {
                                winch.AssignedBreakingLoad = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Factor Of Safety")
                            {
                                winch.FactorOfSafety = Convert.ToDouble(line.Substring(delim + 1));
                            }
                            //if (line.Substring(0, delim) == "HawboldtModel")
                            //{
                            //    winch.HawboldtModel = line.Substring(delim + 1);
                            //    winch.ProtocolHawboldt = true;
                            //}
                            if (line.Substring(0, delim) == "Chart Time Span")
                            {
                                winch.ChartTimeSpan = line.Substring(delim + 1);
                            }
                            if (line.Substring(0, delim) == "Plot Winch")
                            {
                                winch.PlotSelected = bool.Parse(line.Substring(delim + 1));
                            }
                            
                        }
                        


                    }
                    
                    winchViewModel.InsertWinch(winch);

                }
                
            }
            catch
            {
               
            }
        }
        
        public static WinchModel AddComms(CommunicationModel tempComms, WinchModel winch)
        {
            winch.AllOutputCommunication.Add(tempComms.ShallowCopy());
            return winch;
        }

        public static WinchModel InsertOutputComms(CommunicationModel tempComms, WinchModel winch)
        {
            //ConfigDataStore _configDataStore = MainViewModel._configDataStore;
            //See if Comm name is already used. If it is perform update on parameters. If not add it to the list
            int index = -1;
            for (int i = 0; i < winch.AllOutputCommunication.Count; i++)
            {
                CommunicationModel item = winch.AllOutputCommunication[i];
                if (item.DestinationName == tempComms.DestinationName)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                winch.AllOutputCommunication[index] = tempComms.ShallowCopy();
            }
            else
            {
                winch.AllOutputCommunication.Add(tempComms.ShallowCopy());
            }
            //Clears the current list to make winch names as fresh as possible
            //_configDataStore.CurrentWinch.AllOutputCommunication.Clear();
            winch.TabItemsOutputComms.Clear();
            winch.TabItemsOutputComms.Add(new TabItemModel("Add New", "Add New"));
            //Loops through all winches and puts winch names in a list for selection process
            if (winch.AllOutputCommunication.Count > 0)
            {
                foreach (var item in winch.AllOutputCommunication.ToList())
                {
                    TabItemModel tabItem = new TabItemModel(item.DestinationName, item.DestinationName);
                    winch.TabItemsOutputComms.Add(tabItem);
                }
            }
            return winch;
        }
    }
}
