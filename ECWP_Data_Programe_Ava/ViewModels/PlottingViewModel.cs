namespace ViewModels
{
    public partial class PlottingViewModel
    {
        public static CancellationTokenSource? _canceller;
        DataHandlingViewModel dh = new DataHandlingViewModel();
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;

        private WinchModel GetWinch(string winchname)
        {
            int index = -1;

            for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
            {
                WinchModel item = _configDataStore.AllWinches[i];
                if (item.WinchName == winchname)
                {
                    index = i;
                    break;
                }
            }
            WinchModel winch = _configDataStore.AllWinches[index];
            return winch;
        }

        [RelayCommand]
        private void ButtonLogMax(string winchname)
        {
            WinchModel winch = GetWinch(winchname);

            //Write the max data for the cast
            dh.WriteMaxData(winch);
            //Increase the cast count
            winch.CastNumber = (int.Parse(winch.CastNumber) + 1).ToString();
            //UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);

        }

        [RelayCommand]
        private async void StartStop(string winchname)
        {
            WinchModel winch = GetWinch(winchname);
            FileOperationsViewModel.SetFileNames(winch);
            switch (winch.StartStopButtonText)
            {
                case "Stop Log":
                    {
                        try
                        {
                            //Set cancellation token to cancel to stop data collection
                            _canceller.Cancel();
                        }
                        catch (ObjectDisposedException ex)
                        {

                        }
                        if (winch.LogMax == true)
                        {
                            //Write the max data for the cast
                            dh.WriteMaxData(winch);
                            //Increase the cast count
                           winch.CastNumber = (int.Parse(winch.CastNumber) + 1).ToString();
                            //UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);

                        }

                        //Change button text
                        winch.StartStopButtonText = "Start Log";
                        MainWindowViewModel._configDataStore.UserInputsEnable = true;
                        break;
                    }
                default:
                    {
                        if (winch.Log20Hz)
                        {
                            //If the save directory is not set show popup
                            if (MainWindowViewModel._configDataStore.DirectorySet == false)
                            {
                                MessageBoxViewModel.DisplayMessage("Set save location before colecting data");
                                break;
                            }

                        }

                        //ChartDataViewModel.ResetData();
                        //Create new cancellation token at start of data collection
                        _canceller = new CancellationTokenSource();
                        //Starts Data collection on first press
                        dh.GetDataAsync(winch);
                        //change button text
                        winch.StartStopButtonText = "Stop Log";
                        MainWindowViewModel._configDataStore.UserInputsEnable = false;

                        break;
                    }
            }
        }
    }
        
}
