namespace Models
{
    public class CommunicationModel //: INotifyPropertyChanged
    {
        /// <summary>
        /// IP Address of communication partner
        /// </summary>
        private string ipAddress;
        public string IPAddress 
        { get; set;
            //get
            //{
            //    return ipAddress;
            //}
            //set 
            //{
            //    if (ipAddress != value)
            //    {
            //        ipAddress = value;
            //        RaisePropertyChanged("IPAddress");
            //    }
            //}
         }
        /// <summary>
        /// Port number of communication partnet
        /// </summary>
        private string portNumber;
        public string PortNumber
        {get; set; }
            //get 
            //{ 
            //    return portNumber;
            //}
            //set
            //{
            //    if (portNumber != value)
            //    {
            //        portNumber = value;
            //        RaisePropertyChanged("PortNumber");
            //    }
            //}
        //}
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _portNumber)
        {
            IPAddress = _ipAdress;

            //int portNumberValue = 50505;
            //int.TryParse(_portNumber, out portNumberValue);
            PortNumber = _portNumber;//portNumberValue;
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //private void RaisePropertyChanged(string property)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(property));
        //    }
        //}
    }
}
