using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GetConfigViewModel
    {
        public object GetConfig()
        {
            Model.GlobalConfigModel globalConfig = new Model.GlobalConfigModel();
            globalConfig.ReceiveCommunication = new Model.CommunicationModel(ipAddressInputSourceBox.Text, portInpuSourcetBox.Text);
            globalConfig.TransmitCommunication = new Model.CommunicationModel(ipAddressInputDestinationBox.Text, portInputDestinationBox.Text);
            globalConfig.SaveDirectory = (string)directoryLabel.Content;
            globalConfig.CruiseInformation = new Model.CruiseModel(cruiseNameBox.Text, castNumberBox.Text);
            globalConfig.LogMaxValuesSwitch = (bool)logMaxDataCheckBox.IsChecked;
            globalConfig.Log20HzSwitch = (bool)log20HzDataCheckBox.IsChecked;
            globalConfig.UDPSwitch = (bool)sendDataCheckBox.IsChecked;
            globalConfig.UseComputerTimeSwitch = (bool)useComputerTimeCheckBox.IsChecked;
            if ((bool)unolsWireLogButton.IsChecked)
            {
                globalConfig.UNOLSLogFormatSet = true;
            }
            if (!(bool)mtnwWireLogButton.IsChecked)
            {
                globalConfig.UNOLSLogFormatSet = false;
            }
            return globalConfig;
        }
    }
}
