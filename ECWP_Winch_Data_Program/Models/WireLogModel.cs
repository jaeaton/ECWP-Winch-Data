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
    }
}
