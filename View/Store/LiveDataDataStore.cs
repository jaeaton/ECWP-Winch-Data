namespace Store
{
    public partial class LiveDataDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? _tension;
        //public string Tension { get => _tension; set { _tension = value; OnPropertyChanged(nameof(Tension)); } }
        [ObservableProperty]
        private string? _maxTension;
        //public string MaxTension { get => _maxTension; set { _maxTension = value; OnPropertyChanged(nameof(MaxTension)); } }
        [ObservableProperty]
        private string? _speed;
        //public string Speed { get => _speed; set { _speed = value; OnPropertyChanged(nameof(Speed)); } }
        [ObservableProperty]
        private string? _maxSpeed;
        //public string MaxSpeed { get => _maxSpeed; set { _maxSpeed = value; OnPropertyChanged(nameof(MaxSpeed)); } }
        [ObservableProperty]
        private string? _payout;
        //public string Payout { get => _payout; set { _payout = value; OnPropertyChanged(nameof(Payout)); } }
        [ObservableProperty]
        private string? _maxPayout;
        //public string MaxPayout { get => _maxPayout; set { _maxPayout = value; OnPropertyChanged(nameof(MaxPayout)); } }
        [ObservableProperty]
        private string? _rawWireData;
        //public string RawWireData { get => _rawWireData; set { _rawWireData = value; OnPropertyChanged(nameof(RawWireData)); } }
        [ObservableProperty]
        private string? _rawWinchData;
        //public string RawWinchData { get => _rawWinchData; set { _rawWinchData = value; OnPropertyChanged(nameof(RawWinchData)); } }
    }
}
