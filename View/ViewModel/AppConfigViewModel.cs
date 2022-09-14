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
            if (!ValidateIPViewModel.ValidateIPFunction(_configDataStore.ipAddressInputSourceBox))
            {
                MessageBox.Show("Data source IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidateIPFunction(_configDataStore.ipAddressInputDestinationBox) && _configDataStore.sendDataCheckBox)
            {
                MessageBox.Show("Data Destination IP address not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.portInputSourceBox))
            {
                MessageBox.Show("Data source port number not valid");
                return null;
            }
            if (!ValidateIPViewModel.ValidatePortFunction(_configDataStore.portInputDestinationBox) && _configDataStore.sendDataCheckBox)
            {
                MessageBox.Show("Data Destination port number not valid");
                return null;
            }
            if(!ValidateCruiseViewModel.ValidateCruiseName(_configDataStore.cruiseNameBox) && (_configDataStore.log20HzDataCheckBox || _configDataStore.logMaxDataCheckBox))
            {
                MessageBox.Show("Cruise name not valid");
                return null;
            }
            if (!ValidateCruiseViewModel.ValidateCastNumber(_configDataStore.castNumberBox) && (_configDataStore.log20HzDataCheckBox || _configDataStore.logMaxDataCheckBox))
            {
                MessageBox.Show("Cast number not valid");
                return null;
            }
            else
                if (_configDataStore.ipAddressInputSourceBox != null && _configDataStore.portInputSourceBox != null)
            {
                globalConfig.ReceiveCommunication = new CommunicationModel(_configDataStore.ipAddressInputSourceBox, _configDataStore.portInputSourceBox);
            }
            
            globalConfig.TransmitCommunication = new CommunicationModel(_configDataStore.ipAddressInputDestinationBox, _configDataStore.portInputDestinationBox);
            globalConfig.CruiseInformation = new CruiseModel(_configDataStore.cruiseNameBox, _configDataStore.castNumberBox);
            globalConfig.Log20HzSwitch = _configDataStore.log20HzDataCheckBox;
            globalConfig.LogMaxValuesSwitch = _configDataStore.logMaxDataCheckBox;
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
