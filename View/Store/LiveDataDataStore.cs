namespace Store
{
    public class LiveDataDataStore : ViewModelBase
    {
        private string? _tension;
        public string Tension { get => _tension; set { _tension = value; OnPropertyChanged(nameof(Tension)); } }
        private string? _maxTension;
        public string MaxTension { get => _maxTension; set { _maxTension = value; OnPropertyChanged(nameof(MaxTension)); } }
        private string? _speed;
        public string Speed { get => _speed; set { _speed = value; OnPropertyChanged(nameof(Speed)); } }
        private string? _maxSpeed;
        public string MaxSpeed { get => _maxSpeed; set { _maxSpeed = value; OnPropertyChanged(nameof(MaxSpeed)); } }
        private string? _payout;
        public string Payout { get => _payout; set { _payout = value; OnPropertyChanged(nameof(Payout)); } }
        private string? _maxPayout;
        public string MaxPayout { get => _maxPayout; set { _maxPayout = value; OnPropertyChanged(nameof(MaxPayout)); } }
        private string? _rawWireData;
        public string RawWireData { get => _rawWireData; set { _rawWireData = value; OnPropertyChanged(nameof(RawWireData)); } }
        private string? _rawWinchData;
        public string RawWinchData { get => _rawWinchData; set { _rawWinchData = value; OnPropertyChanged(nameof(RawWinchData)); } }
    }
}
