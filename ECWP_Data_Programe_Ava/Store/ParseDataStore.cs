namespace Store
{
    public partial class ParseDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? directory;

        [ObservableProperty]
        private string? cruiseName;

        [ObservableProperty]
        private string? combinedFileName;

        [ObservableProperty]
        private string? processedFileName;

        [ObservableProperty]
        private string? winchSelection;

        [ObservableProperty]
        private float? minTension;

        [ObservableProperty]
        private float? minPayout;

        [ObservableProperty]
        private string? selectedWinch;

        [ObservableProperty]
        private string? selectedWinchEnabled;

        [ObservableProperty]
        private string? fileList;

        [ObservableProperty]
        private string? readingFileName;

        [ObservableProperty]
        private string? readingLine;
    }
}
