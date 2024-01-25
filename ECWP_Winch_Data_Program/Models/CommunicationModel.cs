namespace Models
{
    public partial class CommunicationModel  : ObservableObject
    {
        [ObservableProperty]
        private string tcpIpAddress = string.Empty;

        [ObservableProperty]
        private string portNumber = string.Empty; 
                  
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _pNumber)
        {
            TcpIpAddress = _ipAdress;

            
            PortNumber = _pNumber;//portNumberValue;
        }
        
        //public CommunicationModel ShallowCopy()
        //{
        //    return (CommunicationModel)this.MemberwiseClone();
        //}
    }
}
