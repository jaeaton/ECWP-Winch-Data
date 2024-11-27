namespace ViewModels
{
    public partial class ProcessDataCommandsViewModel : ViewModelBase
    {
        
       
        public ParseDataStore _parseData = ProcessDataViewModel.ParseData;
        //_parseData.ProcessWinchDataButton = "Stop Processing";

        //[ObservableProperty]
        //private string processButtonText = ProcessDataViewModel.ParseData.ProcessWinchDataButton;

        [RelayCommand]
        public void SingleProcessFiles() 
        {
            
            switch (_parseData.ProcessWinchDataButton)
            {
                case "Stop Processing":
                    {
                        
                        _parseData.CancellationTokenSource.Cancel();
                        //_parseData.CancellationTokenSource.Dispose();
                        _parseData.ProcessWinchDataButton = "Start Processing";
                        _parseData.ReadingLine = "Process Cancelled";
                        _parseData.Cast = 0;
                        break;
                    }
                default:
                    {
                        _parseData.CancellationTokenSource = new CancellationTokenSource();
                        _parseData.WireLog.Clear();
                        _parseData.ProcessWinchDataButton = "Stop Processing";
                        _parseData.ReadingLine = string.Empty;
                        ConfigDataStore _config = MainViewModel._configDataStore;
                        //ParseDataStore ParseData = ProcessDataViewModel.ParseData;
                        _parseData.SelectedWinch = _config.CurrentWinch.WinchLogType;
                        _parseData.MinPayout = _config.CurrentWinch.MinimumPayout;
                        _parseData.MinTension = _config.CurrentWinch.MinimumTension;
                        _parseData.StartDate = _config.StartDate;
                        _parseData.EndDate = _config.EndDate;
                        _parseData.UseDateRange = _config.DateRangeCheckBox;
                        _parseData.WinchID = _config.CurrentWinch.Atlantis3PSWinchID;
                        ExcelViewModel.SetWireLogFileName(_config.CurrentWinch);
                        FindFiles();
                        ProcessDataReadFilesViewModel.ReadDataFiles();//ProcessDataViewModel.ParseData);
                        break;
                    }

            }
            
        
        }

        public void FindFiles()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            //ParseDataStore ParseData = ProcessDataViewModel.ParseData;
            _parseData.FileList.Clear();
            _parseData.FileList = new Store.SortableObservableCollection<string>();//SortableObservableCollection<string>();
            DirectoryInfo di = new DirectoryInfo(_config.CurrentWinch.RawLogDirectory);

            _parseData.FileList.Clear();
            string extension;
            //Set File extension
            switch (_config.CurrentWinch.WinchLogType)
            {
                case "SIO Traction Winch":
                    extension = "*.Raw";
                    break;
                case "MASH Winch":
                    extension = "*.CSV";
                    break;
                case "WinchDAC": //Previously Armstrong Cast 6
                    extension = "*.MTN_WINCH";
                    break;
                case "UNOLS String":
                    extension = "*wire.Log";
                    break;
                case "Jay Jay":
                    extension = "*.CSV";
                    break;
                case "Atlantis 3PS":
                    extension = "*.3PS_Winch";
                    break;
                case "ECWP MTNW":
                    extension = "*short.log";
                    break;
                //case "Mermac R30":
                //    extension = ".csv";
                //    break;
                default:
                    extension = string.Empty;
                    break;
            }
            //Search for files and andd them to the list
            if (extension != string.Empty)
            {
                foreach (var fi in di.GetFiles(extension, SearchOption.AllDirectories))
                {
                    _parseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                }
                _parseData.NumberOfFiles = _parseData.FileList.Count;
                //ParseData.FileList;//Sort();
            }
        }
        [RelayCommand]
        public static void ReadLog()
        {
            ExcelViewModel.ReadLog(MainViewModel._configDataStore.CurrentWinch);
        }
    }
}
