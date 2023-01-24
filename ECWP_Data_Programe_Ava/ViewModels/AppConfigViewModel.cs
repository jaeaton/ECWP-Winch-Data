namespace ViewModels
{
    public class AppConfigViewModel 
    {
        /// <summary>
        /// Transforms the live displayed config data to the global config
        /// </summary>
        /// <param name="_configDataStore"></param>
        /// <returns></returns>
        public static object? GetConfig(ConfigDataStore _configDataStore)
        {
            GlobalConfigModel globalConfig = new GlobalConfigModel();
            //Validate IP addresses, port numbers, cruise name, and cast numbers and if they are not valid display what needs to be fixed.
            //If everything is valid write the rest of the global config
            if (!ValidateIPViewModel.ValidateIPFunction(_configDataStore.IpAddressInputSourceBox))
            {
                MessageBoxViewModel.DisplayMessage("Data source IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidateIPFunction(_configDataStore.IpAddressInputDestinationBox) && _configDataStore.SendDataCheckBox)
            {
                MessageBoxViewModel.DisplayMessage("Data Destination IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.PortInputSourceBox))
            {
                MessageBoxViewModel.DisplayMessage("Data source port number not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.PortInputDestinationBox) && _configDataStore.SendDataCheckBox)
            {
                MessageBoxViewModel.DisplayMessage("Data Destination port number not valid");
                return null;
            }
            if(!ValidateCruiseViewModel.ValidateCruiseName(_configDataStore.CruiseNameBox) && (_configDataStore.Log20HzDataCheckBox || _configDataStore.LogMaxDataCheckBox))
            {
                MessageBoxViewModel.DisplayMessage("Cruise name not valid");
                return null;
            }
            if (!ValidateCruiseViewModel.ValidateCastNumber(_configDataStore.CastNumberBox) && (_configDataStore.Log20HzDataCheckBox || _configDataStore.LogMaxDataCheckBox))
            {
                MessageBoxViewModel.DisplayMessage("Cast number not valid");
                return null;
            }
            else
                if (_configDataStore.IpAddressInputSourceBox != null && _configDataStore.PortInputSourceBox != null)
            {
                globalConfig.ReceiveCommunication = new CommunicationModel(_configDataStore.IpAddressInputSourceBox, _configDataStore.PortInputSourceBox);
            }
            
            globalConfig.TransmitCommunication = new CommunicationModel(_configDataStore.IpAddressInputDestinationBox, _configDataStore.PortInputDestinationBox);
            globalConfig.CruiseInformation = new CruiseModel(_configDataStore.CruiseNameBox, _configDataStore.CastNumberBox);
            globalConfig.Log20HzSwitch = _configDataStore.Log20HzDataCheckBox;
            globalConfig.LogMaxValuesSwitch = _configDataStore.LogMaxDataCheckBox;
            globalConfig.UseComputerTimeSwitch = _configDataStore.UseComputerTimeCheckBox;
            globalConfig.UDPSwitch = _configDataStore.SendDataCheckBox;
            globalConfig.SaveDirectory = _configDataStore.DirectoryLabel;
            globalConfig.SerialSwitch = _configDataStore.SendSerialDataCheckBox;
            globalConfig.SerialPortName = _configDataStore.SerialPortName;
            globalConfig.SerialPortBaud = _configDataStore.BaudRate;
            globalConfig.WinchSelection= _configDataStore.WinchSelection;
            if (globalConfig.SaveDirectory != null)
            {
                globalConfig.SaveDirectorySet = true;
                FileOperationsViewModel.SetFileNames(globalConfig);
            }
            if ((bool)_configDataStore.UnolsUDPStringButton)
            {
                globalConfig.UnolsUdpFormatSet = true;
            }
            if ((bool)_configDataStore.MtnwUDPStringButton)
            {
                globalConfig.UnolsUdpFormatSet = false;
            }
            if ((bool)_configDataStore.UnolsWireLogButton)
            {
                globalConfig.LogUnolsSwitch = true;
            }
            if ((bool)_configDataStore.MtnwWireLogButton)
            {
                globalConfig.LogUnolsSwitch = false;
            }
            if ((bool)_configDataStore.UnolsSerialStringButton)
            {
                globalConfig.UnolsSerialFormatSet = true;
            }
            if ((bool)_configDataStore.MtnwSerialStringButton)
            {
                globalConfig.UnolsSerialFormatSet = false;
            }
            return globalConfig;

        }
    }
}
