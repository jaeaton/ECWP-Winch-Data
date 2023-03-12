﻿namespace Store
{
    //Application configuration data for User Inputs View
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? cruiseNameBox;
        partial void OnCruiseNameBoxChanged(string? value)
        {
            bool output = true;
            //Validate fields for cruise info
            //Check to see if a name is provided for the cruise
            if (value == null)
            {
                output = false;
            }
            else if (value.Length == 0)
            {
                output = false;
            }
            if (!output)
            {
                MessageBoxViewModel.DisplayMessage("Cruise name not valid");
            }
        }

        [ObservableProperty]
        private string? directoryLabel;

        [ObservableProperty]
        private bool directorySet;

        [ObservableProperty]
        private string? startStopButtonText;

        [ObservableProperty]
        private bool userInputsEnable;

        [ObservableProperty]
        private string? serialPortName;

        [ObservableProperty]
        private string? baudRate;

        [ObservableProperty]
        private List<string>? availableSerialPorts;

        [ObservableProperty]
        private List<string>? availableBaudRates;

        [ObservableProperty]
        private string? winchSelection;

        [ObservableProperty]
        private List<string>? availableProtocols;

        [ObservableProperty]
        private string? selectedProtocol;

        [ObservableProperty]
        private WinchModel? currentWinch = new();

        [ObservableProperty]
        private ObservableCollection<WinchModel>? allWinches = new();

        [ObservableProperty]
        private ObservableCollection<string>? winchesToPlot = new();

        [ObservableProperty]
        private ObservableCollection<WinchModel>? plottingWinches = new();

        [ObservableProperty]
        private List<string>? speedUnitList;

        [ObservableProperty]
        private List<string>? tensionUnitList;

        [ObservableProperty]
        private List<string>? payoutUnitList;

        [ObservableProperty]
        private string? selectWinch;// = new();
        partial void OnSelectWinchChanged(string? value)
        {
            LoadWinch(value);
        }

        [ObservableProperty]
        private ObservableCollection<string>? winchNames = new();


        public void LoadWinch(string? winch)
        {
            if (winch != null && AllWinches != null)
            {
                int index = -1;

                for (int i = 0; i < AllWinches.Count; i++)
                {
                    WinchModel item = AllWinches[i];
                    if (item.WinchName == winch)
                    {
                        index = i;
                        break;
                    }
                }
                //CurrentWinch = AllWinches[index].ShallowCopy();
                CurrentWinch = AllWinches[index].DeepCopy();
            }
        }
    }

}
