using System.Net;

namespace Models
{
    //[INotifyPropertyChanged]
    public partial class CommunicationModel  : ObservableObject
    {
        [ObservableProperty]
        private string? tcpIpAddress;
        partial void OnTcpIpAddressChanged(string? value)   
        {
            ////Write validation of network parameters for transmit and recieve
            ////Check for valid IP Address
            //bool output = false;
            //if (value == null)
            //{
            //    output = false;
                
            //}
            //else
            //{
            //    string[] octets = value.Split('.');
            //    if (octets.Length != 4)
            //    {
            //        output = false;
            //    }
            //    else
            //    {
            //        foreach (var octet in octets)
            //        {
            //            int octetValue;
            //            bool validOctet = int.TryParse(octet, out octetValue);
            //            if (validOctet == false || octetValue < 1 || octetValue > 255)
            //            {
            //                output = false;
            //            }
            //            else
            //            {
            //                output = true;
            //            }
            //        }
            //    }
            //}

            //if (!output)
            //{
            //    //MessageBox.Show("IP Address not valid");
            //    MessageBoxViewModel.DisplayMessage("IP Address not valid");
            //}

        }
    
        [ObservableProperty]
        private string? portNumber; 
        partial void OnPortNumberChanged(string? value)
        {
            //bool output = false;
            ////Check for valid port number
            ////int port = 0;
            //bool validPort = int.TryParse(value, out _);
            //if (!validPort)
            //{
            //    output = false;
            //}
            //else
            //{
            //    output = true;
            //}
            //if (!output)
            //{
            //    MessageBoxViewModel.DisplayMessage("Port number not valid");
            //}
            
        }
           
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _pNumber)
        {
            TcpIpAddress = _ipAdress;

            //int portNumberValue = 50505;
            //int.TryParse(_portNumber, out portNumberValue);
            PortNumber = _pNumber;//portNumberValue;
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
        //public CommunicationModel ShallowCopy()
        //{
        //    return (CommunicationModel)this.MemberwiseClone();
        //}
    }
}
