namespace Store
{
    public class LiveDataDataStore : ViewModelBase
    {
        private string _tension;
        public string tension { get { return _tension; } set { _tension = value; OnPropertyChanged(nameof(tension));  } }
        private string _maxTension;
        public string maxTension { get { return _maxTension; } set { _maxTension = value;OnPropertyChanged(nameof(maxTension)); } }
        private string _speed;
        public string speed { get { return _speed; } set { _speed = value;OnPropertyChanged(nameof(speed)); } }
        private string _maxSpeed;
        public string maxSpeed { get { return _maxSpeed; } set { _maxSpeed = value;OnPropertyChanged(nameof(maxSpeed)); } }
        private string _payout;
        public string payout { get { return _payout; } set { _payout = value;OnPropertyChanged(nameof(payout)); } }
        private string _maxPayout;
        public string maxPayout { get { return _maxPayout; } set { _maxPayout = value; OnPropertyChanged(nameof(maxPayout)); } }
        
    }
}
