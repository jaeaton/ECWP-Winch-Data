namespace ViewModels
{
    internal partial class LiveDataViewModel : ViewModelBase
    {
        ConfigDataStore _configDataStore = MainViewModel._configDataStore;
        //move code from code behind to here
        [RelayCommand]
        private async void PlotHelp()
        {
            VersionCheckViewModel viewModel = new VersionCheckViewModel();
            viewModel.RunningVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            MessageBoxViewModel.DisplayMessage("Step 1: Configure winch(es) in the Config Winch tab. \n" +
                "Step 2: Select the winches to plot. \n" +
                "Step 3: Set the cruise name. \n" +
                "Step 4: Set the cast numbers for the selected winches. \n" +
                "Step 5: Set save file location. If you are saving either the max data or the 20 hz data the file location MUST be set.\n" +
                "Step 6: Update Configuration adds the cruise name, cast numbers, and file location to the config file.\n" +
                "Step 7: Start Log begins the capture of data and displays, logs, and retransmits as configured.\n" +
                "Step 8: Stop Log ends the capture of data, completes logs (including max log if selected), increases the cast count, and resets the maximum data values.\n" +
                "Step 9. Log max logs the maximum data strings, increases the cast number, and resets the maximum values.\n\n" +
                $"{viewModel.RunningVersion}");
        }
        [RelayCommand]
        private async void SaveLocation()
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //Set the dummy filename
            saveFileDialog.InitialFileName = "Date_Cruise_Winch.log";

            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileName);
                _configDataStore.DirectoryLabel = (string)fileInfo.DirectoryName;
                _configDataStore.DirectorySet = true;
                FileOperationsViewModel.WriteConfig(_configDataStore);
            }

        }
        [RelayCommand]
        private void ConfigUpdate()
        {
            FileOperationsViewModel.WriteConfig(_configDataStore);
        }

        public void PlotSelectionChanged(bool? selected, string? WinchName)
        {
            if (selected == true)
            {
                _configDataStore.WinchesToPlot.Add(WinchName);
            }
            if (selected == false)
            {
                MainViewModel._configDataStore.WinchesToPlot.Remove(WinchName);
            }
            _configDataStore.PlottingWinches.Clear();
            if (_configDataStore.AllWinches != null && _configDataStore.WinchesToPlot != null)
            {
                foreach (var winch in _configDataStore.WinchesToPlot)
                {
                    for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
                    {
                        if (_configDataStore.AllWinches[i].WinchName == winch)
                        {
                            //_configDataStore.PlottingWinches.Add(_configDataStore.AllWinches[i].ShallowCopy());
                            _configDataStore.PlottingWinches.Add(_configDataStore.AllWinches[i]);//.DeepCopy());
                            break;
                        }
                    }

                }
            }
        }
    }
}
