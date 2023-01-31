namespace Store
{
    //Data store for parse data
    public partial class ParseDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? _directory;

        [ObservableProperty]
        private string? _cruiseName;

        [ObservableProperty]
        private string? _combinedFileName;

        [ObservableProperty]
        private string? _processedFileName;

        [ObservableProperty]
        private string? _winchSelection;

        [ObservableProperty]
        private string? _minTension;

        [ObservableProperty]
        private string? _minPayout;

        [ObservableProperty]
        private string? _selectedWinch;

        [ObservableProperty]
        private string? _selectedWinchEnabled;

        [ObservableProperty]
        private List<string>? _fileList;

        [ObservableProperty]
        private string? _readingFileName;

        [ObservableProperty]
        private string? _readingLine;

        [ObservableProperty]
        private int? _numberOfFiles;

        [ObservableProperty]
        private List<string>? _availableWinches;

        [ObservableProperty]
        private List<string>? _availablePayouts;

        [ObservableProperty]
        private List<string>? _availableTensions;
    }
}
