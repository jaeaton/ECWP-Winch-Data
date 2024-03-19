namespace Store
{
    //Data store for parse data
    public partial class ParseDataStore : ObservableObject
    {
        //[ObservableProperty]
        //private object Sync { get; }  = new object();
        public object Sync { get; } = new object();

        [ObservableProperty]
        private string directory = string.Empty;

        [ObservableProperty]
        private ProcessDataChartViewModel chartData = new();

        [ObservableProperty]
        private ObservableCollection<DataPointModel> dataToPlot = new();

        [ObservableProperty]
        private ObservableCollection<WireLogModel> wireLog = new ObservableCollection<WireLogModel>();

        [ObservableProperty]
        private DataPointModel maxTensionDataPoint = new();

        [ObservableProperty]
        private DataPointModel maxPayoutDataPoint = new();

        [ObservableProperty]
        private string cruiseName = string.Empty;

        [ObservableProperty]
        private string combinedFileName = string.Empty;

        [ObservableProperty]
        private string processedFileName = string.Empty;

        [ObservableProperty]
        private string winchSelection = string.Empty;

        [ObservableProperty]
        private string minTension = string.Empty;

        [ObservableProperty]
        private string minPayout = string.Empty;

        [ObservableProperty]
        private float maxTensionCurrent = 0;

        [ObservableProperty]
        private float maxTensionPayoutCurrent = 0;

        [ObservableProperty]
        private float maxPayoutCurrent = 0;

        [ObservableProperty]
        private string maxTensionString = string.Empty;

        [ObservableProperty]
        private string maxPayoutString = string.Empty;

        [ObservableProperty]
        private int cast = 1;

        [ObservableProperty]
        private bool castActive = false;

        [ObservableProperty]
        private string selectedWinch = string.Empty;

        [ObservableProperty]
        private string selectedWinchEnabled = string.Empty;

        [ObservableProperty]
        private SortableObservableCollection<string> fileList = new();
        
       [ObservableProperty]
        private string readingFileName = string.Empty;

        [ObservableProperty]
        private string readingLine = string.Empty   ;
        partial void OnReadingLineChanged(string value)
        {
            //Dispatcher.UIThread.Post(() => MainViewModel._configDataStore.ReadingLine = value);
           
        }

        [ObservableProperty]
        private int numberOfFiles = 0;
        partial void OnNumberOfFilesChanged(int value)
        {
            MainViewModel._configDataStore.NumberOfFiles = value;
        }
        [ObservableProperty]
        private int numberOfProcessedFiles = 0;
        partial void OnNumberOfProcessedFilesChanged(int value)
        {
            Dispatcher.UIThread.Post(() => MainViewModel._configDataStore.NumberOfProcessedFiles = value);
        }

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today;

        [ObservableProperty]
        private bool useDateRange = false;

        [ObservableProperty]
        private List<string> availableWinches = new()
        {
                "MASH Winch",
                "SIO Traction Winch",
                "Armstrong CAST 6",
                "UNOLS String",
                "Jay Jay"
        };

        [ObservableProperty]
        private List<string> availablePayouts = new()
        {
                "-10",
                "0",
                "1",
                "5",
                "10",
                "12",
                "25",
                "50"
        };

        [ObservableProperty]
        private List<string> availableTensions = new()
        {
                "-100",
                "0",
                "100",
                "250",
                "500"
        };

        [ObservableProperty]
        private List<ProcessCastDataModel> processCasts =new();

        [ObservableProperty]
        private CancellationTokenSource cancellationTokenSource = new();
    }
    public class SortableObservableCollection<T> : ObservableCollection<T>
    {
        public Func<T, object> SortingSelector { get; set; }
        public bool Descending { get; set; }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (SortingSelector == null
                || e.Action == NotifyCollectionChangedAction.Remove
                || e.Action == NotifyCollectionChangedAction.Reset)
                return;

            var query = this
              .Select((item, index) => (new { Index = index, Item = item }));
            query = Descending
              ? query.OrderByDescending(tuple => SortingSelector(tuple.Item))
              : query.OrderBy(tuple => SortingSelector(tuple.Item));

            var map = query.Select((tuple, index) => (new { OldIndex = tuple.Index, NewIndex = index }))
             .Where(o => o.OldIndex != o.NewIndex);

            using (var enumerator = map.GetEnumerator())
                if (enumerator.MoveNext())
                    Move(enumerator.Current.OldIndex, enumerator.Current.NewIndex);

            
        }
    }
}
