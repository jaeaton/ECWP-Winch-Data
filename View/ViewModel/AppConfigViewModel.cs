namespace ViewModel
{
    public class AppConfigViewModel : ViewModelBase
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
                MessageBox.Show("Data source IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidateIPFunction(_configDataStore.IpAddressInputDestinationBox) && _configDataStore.sendDataCheckBox)
            {
                MessageBox.Show("Data Destination IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.PortInputSourceBox))
            {
                MessageBox.Show("Data source port number not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.PortInputDestinationBox) && _configDataStore.sendDataCheckBox)
            {
                MessageBox.Show("Data Destination port number not valid");
                return null;
            }
            if(!ValidateCruiseViewModel.ValidateCruiseName(_configDataStore.CruiseNameBox) && (_configDataStore.Log20HzDataCheckBox || _configDataStore.LogMaxDataCheckBox))
            {
                MessageBox.Show("Cruise name not valid");
                return null;
            }
            if (!ValidateCruiseViewModel.ValidateCastNumber(_configDataStore.CastNumberBox) && (_configDataStore.Log20HzDataCheckBox || _configDataStore.LogMaxDataCheckBox))
            {
                MessageBox.Show("Cast number not valid");
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
            globalConfig.UseComputerTimeSwitch = _configDataStore.useComputerTimeCheckBox;
            globalConfig.UDPSwitch = _configDataStore.sendDataCheckBox;
            globalConfig.SaveDirectory = _configDataStore.directoryLabel;
            globalConfig.SerialSwitch = _configDataStore.sendSerialDataCheckBox;
            globalConfig.SerialPortName = _configDataStore.serialPortName;
            globalConfig.SerialPortBaud = _configDataStore.baudRate;
            if (globalConfig.SaveDirectory != null)
            {
                globalConfig.SaveDirectorySet = true;
                FileOperationsViewModel.SetFileNames(globalConfig);
            }
            if ((bool)_configDataStore.unolsUDPStringButton)
            {
                globalConfig.UnolsUdpFormatSet = true;
            }
            if ((bool)_configDataStore.mtnwUDPStringButton)
            {
                globalConfig.UnolsUdpFormatSet = false;
            }
            if ((bool)_configDataStore.unolsWireLogButton)
            {
                globalConfig.LogUnolsSwitch = true;
            }
            if ((bool)_configDataStore.mtnwWireLogButton)
            {
                globalConfig.LogUnolsSwitch = false;
            }
            if ((bool)_configDataStore.unolsSerialStringButton)
            {
                globalConfig.UnolsSerialFormatSet = true;
            }
            if ((bool)_configDataStore.mtnwSerialStringButton)
            {
                globalConfig.UnolsSerialFormatSet = false;
            }
            return globalConfig;

        }
    }
}
