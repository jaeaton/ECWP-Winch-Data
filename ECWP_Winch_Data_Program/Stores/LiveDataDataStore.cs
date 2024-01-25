namespace Store
{
    public partial class LiveDataDataStore : ObservableObject
    {
        [ObservableProperty]
        private string tension = string.Empty;
        //public string Tension { get => _tension; set { _tension = value; OnPropertyChanged(nameof(Tension)); } }
        [ObservableProperty]
        private string maxTension = string.Empty;
        //public string MaxTension { get => _maxTension; set { _maxTension = value; OnPropertyChanged(nameof(MaxTension)); } }
        [ObservableProperty]
        private string speed = string.Empty;
        //public string Speed { get => _speed; set { _speed = value; OnPropertyChanged(nameof(Speed)); } }
        [ObservableProperty]
        private string maxSpeed = string.Empty;
        //public string MaxSpeed { get => _maxSpeed; set { _maxSpeed = value; OnPropertyChanged(nameof(MaxSpeed)); } }
        [ObservableProperty]
        private string payout = string.Empty;
        //public string Payout { get => _payout; set { _payout = value; OnPropertyChanged(nameof(Payout)); } }
        [ObservableProperty]
        private string maxPayout = string.Empty;
        //public string MaxPayout { get => _maxPayout; set { _maxPayout = value; OnPropertyChanged(nameof(MaxPayout)); } }
        [ObservableProperty]
        private string rawWireData = string.Empty;
        //public string RawWireData { get => _rawWireData; set { _rawWireData = value; OnPropertyChanged(nameof(RawWireData)); } }
        [ObservableProperty]
        private string rawWinchData = string.Empty;
        //public string RawWinchData { get => _rawWinchData; set { _rawWinchData = value; OnPropertyChanged(nameof(RawWinchData)); } }
        [ObservableProperty]
        private string tensionColor = string.Empty;
        public LiveDataDataStore() { }
        public LiveDataDataStore(string tens, string maxTens, string spee, string maxSpee, string payo, string maxPayo, string rawWire, string rawWinch, string tenColor)
        {
            Tension = tens;
            MaxTension = maxTens;
            Speed = spee;
            MaxSpeed = maxSpee;
            Payout = payo;
            MaxPayout = maxPayo;
            RawWireData = rawWire;
            RawWinchData = rawWinch;
            TensionColor = tenColor;
        }
    }
}
