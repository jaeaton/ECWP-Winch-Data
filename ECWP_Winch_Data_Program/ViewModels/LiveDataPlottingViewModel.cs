﻿namespace ViewModels
{
    internal partial class LiveDataPlottingViewModel : ViewModelBase
    {
        //public CancellationTokenSource? _canceller;
        private LiveDataHandlingViewModel dh = new LiveDataHandlingViewModel();

        private ConfigDataStore _configDataStore = MainViewModel._configDataStore;

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
        private async Task StartStop(string winchname)
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
                            winch.Canceller.Cancel();
                        }
                        catch (ObjectDisposedException ex)
                        {
                            await MessageBoxViewModel.DisplayMessage($"ObjectDisposeException: {ex}");
                        }

                        //Change button text
                        winch.StartStopButtonText = "Start Log";
                        //MainViewModel._configDataStore.UserInputsEnable = true;
                        break;
                    }
                default:
                    {
                        if (winch.Log20Hz)
                        {
                            //If the save directory is not set show popup
                            if (winch.RawLogDirectory == string.Empty)
                            {
                                await MessageBoxViewModel.DisplayMessage("Set save location before colecting data");
                                break;
                            }
                        }
                        if (winch.CastNumber == string.Empty)
                        {
                            winch.CastNumber = "1";
                        }

                        //ChartDataViewModel.ResetData();
                        //Create new cancellation token at start of data collection
                        winch.Canceller = new CancellationTokenSource();
                        //Starts Data collection on first press
                        winch.StartStopButtonText = "Stop Log";
                        dh.GetDataAsync(winch);
                        //change button text

                        //MainViewModel._configDataStore.UserInputsEnable = false;

                        break;
                    }
            }
        }
        //For future plot reset
        //[RelayCommand]
        //private void ResetPlotView(string winchname)
        //{
        //    WinchModel winch = GetWinch(winchname);
            
        //}
    }
}