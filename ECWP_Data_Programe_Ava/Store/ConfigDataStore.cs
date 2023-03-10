using System.Diagnostics;

namespace Store
{
    //Application configuration data for User Inputs View
    //[INotifyPropertyChanged]
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? ipAddressInputSourceBox;

        [ObservableProperty]
        private string? portInputSourceBox;

        [ObservableProperty]
        private string? ipAddressInputDestinationBox;

        [ObservableProperty]
        private string? portInputDestinationBox;

        [ObservableProperty]
        private string? cruiseNameBox;

        [ObservableProperty]
        private string? castNumberBox;

        [ObservableProperty]
        private bool logMaxDataCheckBox;

        [ObservableProperty]
        private bool log20HzDataCheckBox;

        [ObservableProperty]
        private bool sendDataCheckBox;

        [ObservableProperty]
        private bool sendSerialDataCheckBox;

        [ObservableProperty]
        private bool useComputerTimeCheckBox;

        [ObservableProperty]
        private bool unolsUDPStringButton;

        [ObservableProperty]
        private bool mtnwUDPStringButton;

        [ObservableProperty]
        private string? directoryLabel;

        [ObservableProperty]
        private bool directorSet;

        [ObservableProperty]
        private bool unolsWireLogButton;

        [ObservableProperty]
        private bool mtnwWireLogButton;

        [ObservableProperty]
        private string? startStopButtonText;

        [ObservableProperty]
        private bool userInputsEnable;

        [ObservableProperty]
        private string? serialPortName;

        [ObservableProperty]
        private string? baudRate;

        [ObservableProperty]
        private bool unolsSerialStringButton;

        [ObservableProperty]
        private bool mtnwSerialStringButton;

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
                CurrentWinch = AllWinches[index].ShallowCopy();
            }
        }
    }

}
