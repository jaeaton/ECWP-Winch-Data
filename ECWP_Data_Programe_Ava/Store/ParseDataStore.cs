namespace Store
{
    //Data store for parse data
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
        private string? minTension;

        [ObservableProperty]
        private string? minPayout;

        [ObservableProperty]
        private string? selectedWinch;

        [ObservableProperty]
        private string? selectedWinchEnabled;

        [ObservableProperty]
        private List<string>? fileList;

        [ObservableProperty]
        private string? readingFileName;

        [ObservableProperty]
        private string? readingLine;

        [ObservableProperty]
        private int? numberOfFiles;

        [ObservableProperty]
        private List<string>? availableWinches;

        [ObservableProperty]
        private List<string>? availablePayouts;

        [ObservableProperty]
        private List<string>? availableTensions;
    }
}
