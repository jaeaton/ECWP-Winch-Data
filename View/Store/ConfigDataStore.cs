namespace Store
{
    //Application configuration data for User Inputs View
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? ipAddressInputSourceBox;
        
        [ObservableProperty]
        private string? portInputSourceBox;
        
        [ObservableProperty]
        private string? ipAddressInputDestinationBox;
        
        [ObservableProperty]
        private string? portInputDestinationBox;

        [ObservableProperty]
        private string? cruiseNameBox;

        [ObservableProperty]
        private string? castNumberBox;

        [ObservableProperty]
        private bool logMaxDataCheckBox;

        [ObservableProperty]
        private bool log20HzDataCheckBox;
        
        private bool _senddatacheckbox;
        public bool sendDataCheckBox
        {
            get => _senddatacheckbox;
            set { _senddatacheckbox = value; OnPropertyChanged(nameof(sendDataCheckBox)); }
        }
        private bool _sendSerialDataCheckBox;
        public bool sendSerialDataCheckBox
        {
            get => _sendSerialDataCheckBox;
            set { _sendSerialDataCheckBox = value; OnPropertyChanged(nameof(sendSerialDataCheckBox)); }
        }
        private bool _usecomputertimecheckbox;
        public bool useComputerTimeCheckBox
        {
            get => _usecomputertimecheckbox;
            set { _usecomputertimecheckbox = value; OnPropertyChanged(nameof(useComputerTimeCheckBox)); }
        }
        private bool _unolsUDPstringbutton;
        public bool unolsUDPStringButton
        {
            get => _unolsUDPstringbutton;
            set { _unolsUDPstringbutton = value; OnPropertyChanged(nameof(unolsUDPStringButton)); }
        }
        private bool _mtnUDPstringbutton;
        public bool mtnwUDPStringButton
        {
            get => _mtnUDPstringbutton;
            set { _mtnUDPstringbutton = value; OnPropertyChanged(nameof(mtnwUDPStringButton)); }
        }
        private string? _directorylabel;
        public string? directoryLabel
        {
            get => _directorylabel;
            set { _directorylabel = value; OnPropertyChanged(nameof(directoryLabel)); }
        }
        private bool _unolsWireLogButton;
        public bool unolsWireLogButton
        {
            get => _unolsWireLogButton;
            set { _unolsWireLogButton = value; OnPropertyChanged(nameof(unolsWireLogButton)); }
        }
        private bool _mtnwWireLogButton;
        public bool mtnwWireLogButton
        {
            get => _mtnwWireLogButton;
            set { _mtnwWireLogButton = value; OnPropertyChanged(nameof(mtnwWireLogButton)); }
        }
        private string? _startStopButtonText;
        public string? startStopButtonText
        {
            get => _startStopButtonText;
            set { _startStopButtonText = value; OnPropertyChanged(nameof(startStopButtonText)); }
        }
        
        private bool _userInputsEnable;
        public bool userInputsEnable
        {
            get => _userInputsEnable;
            set { _userInputsEnable = value; OnPropertyChanged(nameof(userInputsEnable)); }
        }
        private string? _serialPortName;
        public string? serialPortName
        {
            get => _serialPortName;
            set { _serialPortName = value; OnPropertyChanged(nameof(serialPortName)); }
        }
        private string? _baudRate;
        public string? baudRate
        {
            get => _baudRate;
            set { _baudRate = value; OnPropertyChanged(nameof(baudRate)); }
        }
        private bool _unolsSerialStringButton;
        public bool unolsSerialStringButton
        {
            get => _unolsSerialStringButton;
            set { _unolsSerialStringButton = value; OnPropertyChanged(nameof(unolsSerialStringButton)); }
        }
        private bool _mtnwSerialStringButton;
        public bool mtnwSerialStringButton
        {
            get => _mtnwSerialStringButton;
            set { _mtnwSerialStringButton = value; OnPropertyChanged(nameof(mtnwSerialStringButton));}
        }
    }
}
