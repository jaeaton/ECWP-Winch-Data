namespace Store
{
    //Application configuration data for User Inputs View
    public class ConfigDataStore : ViewModelBase
    {
        private string? _ipaddressinputsourcebox;
        public string ipAddressInputSourceBox
        {
            get => _ipaddressinputsourcebox;
            set { _ipaddressinputsourcebox = value; OnPropertyChanged(nameof(ipAddressInputSourceBox)); }
        }
        private string? _portinputsourcebox;
        public string portInputSourceBox
        {
            get => _portinputsourcebox;
            set
            {
                _portinputsourcebox = value;
                OnPropertyChanged(nameof(portInputSourceBox));
            }
        }
        private string? _ipaddressinputdestinationbox;
        public string ipAddressInputDestinationBox
        {
            get => _ipaddressinputdestinationbox;
            set
            {
                _ipaddressinputdestinationbox = value;
                OnPropertyChanged(nameof(ipAddressInputDestinationBox));
            }
        }
        private string? _portinputdestinationbox;
        public string portInputDestinationBox
        {
            get => _portinputdestinationbox;
            set
            {
                _portinputdestinationbox = value;
                OnPropertyChanged(nameof(portInputDestinationBox));
            }
        }
        private string? _cruisenamebox;
        public string cruiseNameBox
        {
            get => _cruisenamebox;
            set
            {
                _cruisenamebox = value;
                OnPropertyChanged(nameof(cruiseNameBox));
            }
        }
        private string? _castnumberbox;
        public string castNumberBox
        {
            get => _castnumberbox;
            set
            {
                _castnumberbox = value;
                OnPropertyChanged(nameof(castNumberBox));
            }
        }
        private bool _logmaxdatacheckbox;
        public bool logMaxDataCheckBox
        {
            get => _logmaxdatacheckbox;
            set
            {
                _logmaxdatacheckbox = value;
                OnPropertyChanged(nameof(logMaxDataCheckBox));
            }
        }
        private bool _log20hzdatacheckbox;
        public bool log20HzDataCheckBox
        {
            get => _log20hzdatacheckbox;
            set
            {
                _log20hzdatacheckbox = value;
                OnPropertyChanged(nameof(log20HzDataCheckBox));
            }
        }
        private bool _senddatacheckbox;
        public bool sendDataCheckBox
        {
            get => _senddatacheckbox;
            set { _senddatacheckbox = value; OnPropertyChanged(nameof(sendDataCheckBox)); }
        }
        private bool _usecomputertimecheckbox;
        public bool useComputerTimeCheckBox
        {
            get => _usecomputertimecheckbox;
            set { _usecomputertimecheckbox = value; OnPropertyChanged(nameof(useComputerTimeCheckBox)); }
        }
        private bool _unolswirelogbutton;
        public bool unolsWireLogButton
        {
            get => _unolswirelogbutton;
            set { _unolswirelogbutton = value; OnPropertyChanged(nameof(unolsWireLogButton)); }
        }
        private bool _mtnwwirelogbutton;
        public bool mtnwWireLogButton
        {
            get => _mtnwwirelogbutton;
            set { _mtnwwirelogbutton = value; OnPropertyChanged(nameof(mtnwWireLogButton)); }
        }
        private string? _directorylabel;
        public string directoryLabel
        {
            get => _directorylabel;
            set { _directorylabel = value; OnPropertyChanged(nameof(directoryLabel)); }
        }
    }
}
