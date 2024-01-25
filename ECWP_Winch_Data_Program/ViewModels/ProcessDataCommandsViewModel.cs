namespace ViewModels
{
    public partial class ProcessDataCommandsViewModel : ViewModelBase
    {
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


                //if (ParseData.SelectedWinch == "SIO Traction Winch")
                //{
                //    foreach (var fi in di.GetFiles("*.Raw"))
                //    {
                //        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                //    }
                //    ParseData.FileList.Sort();                                //Sorts the List by element name

                //}

                //if (ParseData.SelectedWinch == "MASH Winch")
                //{
                //    foreach (var fi in di.GetFiles("*.CSV"))
                //    {
                //        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                //    }
                //    ParseData.FileList.Sort();                                //Sorts the List by element name

                //}

                //if (ParseData.SelectedWinch == "Armstrong CAST 6")
                //{
                //    foreach (var fi in di.GetFiles("*.MTN_WINCH"))
                //    {
                //        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");
                //    }
                //    // _settingsStore.FileList.Sort();                                //Sorts the List by element name

                //}

                //if (ParseData.SelectedWinch == "UNOLS String")
                //{
                //    foreach (var fi in di.GetFiles("*_wire.log", SearchOption.AllDirectories))
                //    {
                //        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                //    }
                //    ParseData.FileList.Sort();
                //}

                //if (ParseData.SelectedWinch == "Jay Jay")
                //{
                //    foreach (var fi in di.GetFiles("*.csv", SearchOption.AllDirectories))
                //    {
                //        ParseData.FileList.Add($"{fi.DirectoryName}\\{fi.Name}");//fi.Name);
                //    }
                //    ParseData.FileList.Sort();
                //}
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
    }
}
