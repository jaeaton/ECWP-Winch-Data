namespace Models
{
    public partial class WinchModel : ObservableObject
    {
        [ObservableProperty]
        private string? winchName;
        [ObservableProperty]
        private string? fileExtension ;
        //[ObservableProperty]
        //private CommunicationModel? inputCommunication = new() ;
        [ObservableProperty]
        private string? tcpIpAddress;
        [ObservableProperty]
        private string? tcpIpPort;
        [ObservableProperty]
        private string? communicationType ;
        [ObservableProperty]
        private bool useComputerTime ;
        [ObservableProperty]
        private bool log20Hz ;
        [ObservableProperty]
        private bool logMax ;
        [ObservableProperty]
        private bool logFormatUnols ;
        [ObservableProperty]
        private bool logFormatMtnw;
        [ObservableProperty]
        private string? speedUnit ;
        [ObservableProperty]
        private string? payoutUnit ;
        [ObservableProperty]
        private string? tensionUnit ;
        [ObservableProperty]
        private double? stopLogTension ;
        [ObservableProperty]
        private double? stopLogPayout ;
        [ObservableProperty]
        private bool udpOutput;
        [ObservableProperty]
        private string? tcpIpAddressOutput;
        [ObservableProperty]
        private string? tcpIpPortOutput;
        [ObservableProperty]
        private bool udpFormatUnols;
        [ObservableProperty]
        private bool udpFormatMtnw;
        [ObservableProperty]
        private bool serialOutput;
        [ObservableProperty]
        private string? serialPortOutput;
        [ObservableProperty]
        private string? baudRateOutput;
        [ObservableProperty]
        private bool serialFormatUnols;
        [ObservableProperty]
        private bool serialFormatMtnw;
        public WinchModel() { }
        public WinchModel(string winchName, string fileExtension)
        {
            
            WinchName = winchName;
            FileExtension = fileExtension;
        }
        public WinchModel(string? _winchName, string? _fileExtension, string? _tcpIpAddress, string? _tcpIpPort, string? _communicationType, bool _useComputerTime, bool _log20Hz, bool _logMax, string? _speedUnit, string? _payoutUnit, string? _tensionUnit, double? _stopLogTension, double? _stopLogPayout) 
          
        {
            
            WinchName = _winchName;
            FileExtension = _fileExtension;
            //InputCommunication = _inputCommunication;
            TcpIpAddress = _tcpIpAddress;
            TcpIpPort = _tcpIpPort;
            CommunicationType = _communicationType;
            UseComputerTime = _useComputerTime;
            Log20Hz = _log20Hz;
            LogMax = _logMax;
            SpeedUnit = _speedUnit;
            PayoutUnit = _payoutUnit;
            TensionUnit = _tensionUnit;
            StopLogTension = _stopLogTension;
            //StopLogPayout = _stopLogPayout;
        }

        public WinchModel ShallowCopy()
        {
            return (WinchModel) this.MemberwiseClone();
        }
    }
}
