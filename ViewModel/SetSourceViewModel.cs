using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class SetSourceViewModel
    {
        public void SetSourceFunction()
        {
            //Write Source Data Function
            //Validate input
            bool[] valid = new bool[2];
            valid[0] = ValidateIPViewModel.ValidateIPFunction(ipAddressInputSourceBox.Text);
            valid[1] = ValidatePortViewModel.ValidatePortFunction(portInpuSourcetBox.Text);
        
            if (valid[0])
            {
                ipAddressInputSourceBox.Background = Brushes.PaleGreen;
            }
            if (valid[1])
            {
                portInpuSourcetBox.Background = Brushes.PaleGreen;
            }
            if (valid[0] && valid[1])
            {
                //Writes to Global config
                globalConfig.ReceiveCommunication = new CommunicationModel(ipAddressInputSourceBox.Text, portInpuSourcetBox.Text);
                WriteConfigViewModel.WriteConfigFunction();
                }
            else
            {
                if (!valid[0])
                {
                    ipAddressInputSourceBox.Background = Brushes.MistyRose;
                }
                if (!valid[1])
                {
                    portInpuSourcetBox.Background = Brushes.MistyRose;
                }
                MessageBox.Show("Source configuration invalid");
            }

        }
        
    }
}
