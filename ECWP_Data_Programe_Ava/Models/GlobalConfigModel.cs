namespace Models
{
    public class GlobalConfigModel : ObservableObject
    {
        public CruiseModel CruiseInformation { get; set; } = new CruiseModel();
        public CommunicationModel ReceiveCommunication { get; set; } = new CommunicationModel();
        public CommunicationModel TransmitCommunication { get; set; } = new CommunicationModel();
        public List<WinchModel>? Winches { get; set; }
        public string? Minimal20HzLogFileName { get; set; }
        public string? MaxLogFileName { get; set; }
        public string? UnolsWireLogName { get; set; }
        public string? UnolsWinchLogName { get; set; }
        public bool UDPSwitch { get; set; } = new bool();
        public bool Log20HzSwitch { get; set; } = new bool();
        public bool LogUnolsSwitch { get; set; } = new bool();
        public bool LogMaxValuesSwitch { get; set; } = new bool();
        public bool UseComputerTimeSwitch { get; set; } = new bool();
        public string? SaveDirectory { get; set; }
        public bool SaveDirectorySet { get; set; } = new bool();
        public bool UnolsUdpFormatSet { get; set; } = new bool();
        public string? SerialPortName { get; set; }
        public string? SerialPortBaud { get; set; }
        public bool SerialSwitch { get; set; } = new bool();
        public bool UnolsSerialFormatSet { get; set; } = new bool();
        public string? WinchSelection { get; set;}
        public string? SelectedProtocol { get; set; }

    }
}
