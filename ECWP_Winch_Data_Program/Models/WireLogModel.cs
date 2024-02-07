namespace Models
{
    public partial class WireLogModel : ObservableObject
    {
        [ObservableProperty]
        private string eventDate = string.Empty;
        [ObservableProperty]
        private string eventType = string.Empty;
        [ObservableProperty]
        private string installedTensionMemberLength = string.Empty;
        [ObservableProperty]
        private string cutBackAmount = string.Empty;
        [ObservableProperty]
        private string tensionMemberID = string.Empty;
        [ObservableProperty]
        private string castNumber = string.Empty;
        [ObservableProperty]
        private string maxTension = string.Empty;
        [ObservableProperty]
        private string maxTensionWireOut = string.Empty;
        [ObservableProperty]
        private string maxTensionWireIn = string.Empty;
        [ObservableProperty]
        private string maxWireOut = string.Empty;
        [ObservableProperty]
        private string notes = string.Empty;

        public WireLogModel()
        {
            

        }

        public WireLogModel(DateTime _date, string _eventType, int _castNumber, float _maxTension, float _maxTensionWireOut, float _maxWireOut)
        {
            EventDate = _date.ToString("yyyy-MM-dd");
            EventType = _eventType;
            CastNumber = _castNumber.ToString();
            MaxTension = _maxTension.ToString();
            MaxTensionWireOut = _maxTensionWireOut.ToString();
            MaxWireOut = _maxWireOut.ToString();
        }
    }
}
