namespace ViewModels
{
    public partial class ProcessDataCommandsViewModel : ViewModelBase
    {
        ParseDataStore ParseData =ProcessDataViewModel.ParseData;
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
                    case "Armstrong CAST 6":
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
            ConfigDataStore _config = MainViewModel._configDataStore;
            //ParseDataStore ParseData = ProcessDataViewModel.ParseData;
            ParseData.SelectedWinch = _config.CurrentWinch.WinchLogType;
            ParseData.MinPayout = _config.CurrentWinch.MinimumPayout;
            ParseData.MinTension = _config.CurrentWinch.MinimumTension;
            ExcelViewModel.SetWireLogFileName();
            FindFiles();
            ProcessDataReadFilesViewModel.ReadDataFiles(ProcessDataViewModel.ParseData);
        
        }

        private void FindFiles()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            //ParseDataStore ParseData = ProcessDataViewModel.ParseData;

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
                case "Armstrong CAST 6":
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
}
