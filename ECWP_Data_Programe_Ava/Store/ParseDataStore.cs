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
        private float? _minTension;

        [ObservableProperty]
        private float? _minPayout;

        [ObservableProperty]
        private string? _selectedWinch;

        [ObservableProperty]
        private string? _selectedWinchEnabled;

        [ObservableProperty]
        private string? _fileList;

        [ObservableProperty]
        private string? _readingFileName;

        [ObservableProperty]
        private string? _readingLine;

        [ObservableProperty]
        private int? _numberOfFiles;
    }
}
