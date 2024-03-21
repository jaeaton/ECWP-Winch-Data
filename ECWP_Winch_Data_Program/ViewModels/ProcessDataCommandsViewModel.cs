namespace ViewModels
{
    public partial class ProcessDataCommandsViewModel : ViewModelBase
    {
        
       
        ParseDataStore ParseData = ProcessDataViewModel.ParseData;

        [ObservableProperty]
        private string processButtonText = ProcessDataViewModel.ParseData.ProcessWinchDataButton;

        [RelayCommand]
        private async void File_Location()
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //build the save file name
            ParseDataStore ParseData = ProcessDataViewModel.ParseData;
            ParseData.CombinedFileName = $"{ParseData.CruiseName}_Combined.txt";
            ParseData.ProcessedFileName = $"{ParseData.CruiseName}_Processed.txt";
            saveFileDialog.InitialFileName = ParseData.CombinedFileName;
            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileName);
                ParseData.Directory = fileInfo.DirectoryName;
                //UserInputsView.globalConfig.SaveDirectorySet = true;

                //var dialog = new System.Windows.Forms.FolderBrowserDialog();
                //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                //_settingsStore.Directory = dialog.SelectedPath;
                ParseData.FileList = new Store.SortableObservableCollection<string>();//SortableObservableCollection<string>();
                DirectoryInfo di = new DirectoryInfo(ParseData.Directory);

                ParseData.FileList.Clear();
                string directory = ParseData.Directory;
                string extension;
                //Set File extension
                switch (ParseData.SelectedWinch)
                {
                    case "SIO Traction Winch":
                        extension = "*.Raw";
                        break;
                    case "MASH Winch":
                        extension = "*.CSV";
                        break;
                    case "WinchDAC": //Previously Arstrong Cast 6
                        extension = "*.MTN_WINCH";
                        break;
                    case "UNOLS String":
                        extension = "*.wire.Log";
                        break;
                    case "Jay Jay":
                        extension = "*.CSV";
                        break;
                    default:
                        extension = string.Empty;
                        break;
                }
                //Search for files and andd them to the list
                if (extension != string.Empty)
                {
                    foreach (var fi in di.GetFiles(extension, SearchOption.AllDirectories))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                    }
                    ParseData.NumberOfFiles = ParseData.FileList.Count;
                    //ParseData.FileList;//Sort();
                }
            }
        }
        [RelayCommand]
        private void Save_Config()
        {
            ProcessDataWriteFilesViewModel.WriteProcessConfig(ProcessDataViewModel.ParseData);
        }

        [RelayCommand]
        private void Combine_Files()
        {
            ProcessDataReadFilesViewModel.CombineFiles(ProcessDataViewModel.ParseData);
        }

        [RelayCommand]
        private void Process_Files()
        {
            ProcessDataReadFilesViewModel.ParseFiles(ProcessDataViewModel.ParseData);
        }

        [RelayCommand]
        private void SingleProcessFiles() 
        {
            switch (ProcessButtonText)
            {
                case "Stop Processing":
                    {
                        //Cancel Task
                        if (ParseData.CancellationTokenSource.IsCancellationRequested)
                        {
                            ParseData.CancellationTokenSource.Cancel();
                        }
                        
                        //ParseData.CancellationTokenSource.Dispose();
                        ProcessButtonText = "Start Processing";
                        break;
                    }
                default:
                    {
                        ParseData.CancellationTokenSource = new CancellationTokenSource();
                        ProcessButtonText = "Stop Processing";
                        ConfigDataStore _config = MainViewModel._configDataStore;
                        //ParseDataStore ParseData = ProcessDataViewModel.ParseData;
                        ParseData.SelectedWinch = _config.CurrentWinch.WinchLogType;
                        ParseData.MinPayout = _config.CurrentWinch.MinimumPayout;
                        ParseData.MinTension = _config.CurrentWinch.MinimumTension;
                        ParseData.StartDate = _config.StartDate;
                        ParseData.EndDate = _config.EndDate;
                        ParseData.UseDateRange = _config.DateRangeCheckBox;
                        ParseData.WinchID = _config.CurrentWinch.Atlantis3PSWinchID;
                        ExcelViewModel.SetWireLogFileName();
                        FindFiles();
                        ProcessDataReadFilesViewModel.ReadDataFiles();//ProcessDataViewModel.ParseData);
                        break;
                    }

            }
            
        
        }

        private void FindFiles()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            //ParseDataStore ParseData = ProcessDataViewModel.ParseData;
            ParseData.FileList.Clear();
            ParseData.FileList = new Store.SortableObservableCollection<string>();//SortableObservableCollection<string>();
            DirectoryInfo di = new DirectoryInfo(_config.CurrentWinch.RawLogDirectory);

            ParseData.FileList.Clear();
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
                    extension = "*.wire.Log";
                    break;
                case "Jay Jay":
                    extension = "*.CSV";
                    break;
                case "Atlantis 3PS":
                    extension = "*.3PS_Winch";
                    break;
                default:
                    extension = string.Empty;
                    break;
            }
            //Search for files and andd them to the list
            if (extension != string.Empty)
            {
                foreach (var fi in di.GetFiles(extension, SearchOption.AllDirectories))
                {
                    ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                }
                ParseData.NumberOfFiles = ParseData.FileList.Count;
                //ParseData.FileList;//Sort();
            }
        }
    }
}
