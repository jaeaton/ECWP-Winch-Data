namespace Store
{
    public partial class LiveDataDataStore : ObservableObject
    {
        [ObservableProperty]
        private string maxPayout = string.Empty;

        [ObservableProperty]
        private string maxSpeed = string.Empty;

        [ObservableProperty]
        private string maxTension = string.Empty;

        [ObservableProperty]
        private string payout = string.Empty;

        [ObservableProperty]
        private string rawWinchData = string.Empty;

        [ObservableProperty]
        private string rawWireData = string.Empty;

        [ObservableProperty]
        private string speed = string.Empty;

        [ObservableProperty]
        private string tension = string.Empty;

        [ObservableProperty]
        private string tensionColor = string.Empty;

        public LiveDataDataStore()
        { }

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