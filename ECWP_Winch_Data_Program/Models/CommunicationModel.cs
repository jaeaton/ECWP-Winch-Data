namespace Models
{
    public partial class CommunicationModel : ObservableObject
    {
        [ObservableProperty]
        private string baudRate = string.Empty;

        //TCP/IP, UDP, etc
        [ObservableProperty]
        private string communicationProtocol = string.Empty;

        //Serial Vs Network
        [ObservableProperty]
        private string communicationType = "Network";

        [ObservableProperty]
        private string dataBits = string.Empty;

        //Data string format, MTNW, UNOLS, Hawboldt, etc
        [ObservableProperty]
        private string dataProtocol = "UNOLS";

        [ObservableProperty]
        private string destinationName = string.Empty;

        [ObservableProperty]
        private string hawboldtModel = string.Empty;

        [ObservableProperty]
        private bool isHawboldt = false;

        [ObservableProperty]
        private bool isMTNW = false;

        [ObservableProperty]
        private bool isNetwork = true;

        [ObservableProperty]
        private bool isSerial = false;

        [ObservableProperty]
        private bool isUNOLS = true;

        [ObservableProperty]
        private string parity = string.Empty;

        [ObservableProperty]
        private string portNumber = string.Empty;

        [ObservableProperty]
        private string serialPort = string.Empty;

        [ObservableProperty]
        private string stopBits = string.Empty;

        [ObservableProperty]
        private string tcpIpAddress = string.Empty;

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

        public CommunicationModel(string _tcpIpAddress, string _portNumber, string _communicationType, bool _isSerial, bool _isNetwork, string _serialPort, string _baudRate, string _dataBits, string _parity, string _stopBits, string _communicationProtocol, string _dataProtocol, bool _isUNOLS, bool _isMTNW, string _destinationName)
        {
            DestinationName = _destinationName;
            TcpIpAddress = _tcpIpAddress;
            PortNumber = _portNumber;
            CommunicationType = _communicationType;
            IsSerial = _isSerial;
            IsNetwork = _isNetwork;
            SerialPort = _serialPort;
            BaudRate = _baudRate;
            DataBits = _dataBits;
            Parity = _parity;
            StopBits = _stopBits;
            CommunicationProtocol = _communicationProtocol;
            DataProtocol = _dataProtocol;
            IsUNOLS = _isUNOLS;
            IsMTNW = _isMTNW;
        }

        public CommunicationModel(CommunicationModel _commModel)
        {
            DestinationName = _commModel.DestinationName;
            TcpIpAddress = _commModel.TcpIpAddress;
            PortNumber = _commModel.PortNumber;
            CommunicationType = _commModel.CommunicationType;
            IsSerial = _commModel.IsSerial;
            IsNetwork = _commModel.IsNetwork;
            SerialPort = _commModel.SerialPort;
            BaudRate = _commModel.BaudRate;
            DataBits = _commModel.DataBits;
            Parity = _commModel.Parity;
            StopBits = _commModel.StopBits;
            CommunicationProtocol = _commModel.CommunicationProtocol;
            DataProtocol = _commModel.DataProtocol;
            IsUNOLS = _commModel.IsUNOLS;
            IsMTNW = _commModel.IsMTNW;
        }

        public object Sync { get; } = new object();

        public CommunicationModel ShallowCopy()
        {
            return (CommunicationModel)this.MemberwiseClone();
        }

        partial void OnCommunicationTypeChanged(string value)
        {
            if (value == "Serial" && IsSerial == false)
            {
                IsSerial = true;
                IsNetwork = false;
            }
            if (value == "Network" && IsNetwork == false)
            {
                IsNetwork = true;
                IsSerial = false;
            }
        }

        // = new();

        partial void OnDataProtocolChanged(string value)
        {
            if (value == "UNOLS" && IsUNOLS != true)
            {
                IsUNOLS = true;
                IsMTNW = false;
            }

            if (value == "MTNW" && IsMTNW != true)
            {
                IsUNOLS = false;
                IsMTNW = true;
            }
            if (value != null && value.Contains("Hawboldt"))
            {
                IsHawboldt = true;
                if (value.Contains("3464"))
                {
                    HawboldtModel = "SPRE-3464";
                }
                else if (value.Contains("2648"))
                {
                    HawboldtModel = "SPRE-2648RS";
                }
                else if (value.Contains("2640"))
                {
                    HawboldtModel = "SPRE-2640";
                }
                else if (value.Contains("2036"))
                {
                    HawboldtModel = "SPRE-2036S";
                }
            }
            else
            {
                IsHawboldt = false;
            }
        }

        partial void OnIsMTNWChanged(bool value)
        {
            if (value == true)
            {
                DataProtocol = "MTNW";
            }
        }

        partial void OnIsNetworkChanged(bool value)
        {
            if (value == true)
            {
                CommunicationType = "Network";
            }
        }

        partial void OnIsSerialChanged(bool value)
        {
            if (value == true)
            {
                CommunicationType = "Serial";
            }
        }

        // = new();
        partial void OnIsUNOLSChanged(bool value)
        {
            if (value == true)
            {
                DataProtocol = "UNOLS";
            }
        }
    }
}