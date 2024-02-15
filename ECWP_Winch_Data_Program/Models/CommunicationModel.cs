namespace Models
{
    public partial class CommunicationModel  : ObservableObject
    {
        [ObservableProperty]
        private string tcpIpAddress = string.Empty;

        [ObservableProperty]
        private string portNumber = string.Empty;

        //Serial Vs Network
        [ObservableProperty]
        private string communicationType = string.Empty;

        [ObservableProperty]
        private string serialPort = string.Empty;

        [ObservableProperty]
        private string baudRate = string.Empty;

        [ObservableProperty]
        private string dataBits = string.Empty;

        [ObservableProperty]
        private string parity = string.Empty;

        [ObservableProperty]
        private string stopBits = string.Empty;

        //TCP/IP, UDP, etc
        [ObservableProperty]
        private string communicationProtocol = string.Empty;

        //Data string format, MTNW, UNOLS, Hawboldt, etc
        [ObservableProperty]
        private string dataProtocol = string.Empty;

        [ObservableProperty]
        private string destinationName = string.Empty;
                  
        public CommunicationModel()
        {

        }
        public CommunicationModel(string _ipAdress, string _pNumber)
        {
            TcpIpAddress = _ipAdress;

            
            PortNumber = _pNumber;//portNumberValue;
        }

        public CommunicationModel(string _ipAdress, string _pNumber, string _commType, string _sPort, string _baudRate, string _dataBit, string _parity, string _stopBits, string _commProtocol)
        {
            TcpIpAddress = _ipAdress;
            PortNumber = _pNumber;
            BaudRate = _baudRate;  
            Parity = _parity;
            StopBits = _stopBits;
            SerialPort = _sPort;
            DataBits = _dataBit;
            CommunicationType = _commType;
            CommunicationProtocol = _commProtocol;
        }
        public CommunicationModel ShallowCopy()
        {
            return (CommunicationModel)this.MemberwiseClone();
        }
    }
}
