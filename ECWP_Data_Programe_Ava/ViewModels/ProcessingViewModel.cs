namespace ViewModels
{
    public partial class ProcessingViewModel : ObservableObject
    {
        [ObservableProperty]
        private ParseDataStore? parseData = (ParseDataStore?)ProcessingReadFilesViewModel.ReadProcessConfig();

        [RelayCommand]
        private async void File_Location()
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //build the save file name
            
            ParseData.CombinedFileName = $"{ ParseData.CruiseName }_Combined.txt";
            ParseData.ProcessedFileName = $"{ ParseData.CruiseName }_Processed.txt";
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
                ParseData.FileList = new List<string>();
                DirectoryInfo di = new DirectoryInfo(ParseData.Directory);
                ParseData.FileList = new List<string>();

                if (ParseData.SelectedWinch == "SIO Traction Winch")
                {
                    foreach (var fi in di.GetFiles("*.Raw"))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                    }
                    ParseData.FileList.Sort();                                //Sorts the List by element name

                }

                if (ParseData.SelectedWinch == "MASH Winch")
                {
                    foreach (var fi in di.GetFiles("*.CSV"))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                    }
                    ParseData.FileList.Sort();                                //Sorts the List by element name

                }

                if (ParseData.SelectedWinch == "Armstrong CAST 6")
                {
                    foreach (var fi in di.GetFiles("*.MTN_WINCH"))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                    }
                    // _settingsStore.FileList.Sort();                                //Sorts the List by element name

                }

                if (ParseData.SelectedWinch == "UNOLS String")
                {
                    foreach (var fi in di.GetFiles("*_wire.log", SearchOption.AllDirectories))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                    }
                    ParseData.FileList.Sort();
                }
                if (ParseData.SelectedWinch == "Jay Jay")
                {
                    foreach (var fi in di.GetFiles("*.csv", SearchOption.AllDirectories))
                    {
                        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                    }
                    ParseData.FileList.Sort();
                }
            }
        }
        [RelayCommand]
        private void Save_Config()
        {
            ProcessingWriteFilesViewModel.WriteProcessConfig(ParseData);
        }

        [RelayCommand]
        private void Combine_Files() 
        {
            ProcessingReadFilesViewModel.CombineFiles(ParseData);
        }

        [RelayCommand]
        private void Process_Files() 
        {
        ProcessingReadFilesViewModel.ParseFiles(ParseData);
        }

        
    }

    
}
