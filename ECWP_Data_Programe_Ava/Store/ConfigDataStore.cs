using System.Diagnostics;

namespace Store
{
    //Application configuration data for User Inputs View
    [INotifyPropertyChanged]
    public partial class ConfigDataStore 
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
        partial void OnWinchSelectionChanged(string? winchSelection)
        {
            //MainLiveDataViewModel.UpdatePlottingWinch(WinchSelection);
        }

        [ObservableProperty]
        private List<string>? availableProtocols;

        [ObservableProperty]
        private string? selectedProtocol;

        [ObservableProperty]
        private WinchModel? currentWinch = new();

        [ObservableProperty]
        private ObservableCollection<WinchModel>? allWinches = new();

        [ObservableProperty]
        private List<string>? winchesToPlot = new();
        partial void OnWinchesToPlotChanged(List<string>? winchesToPlot)
        {
            PlottingWinches.Clear();
            if (AllWinches != null && WinchesToPlot != null)
            {
                foreach (var winch in WinchesToPlot)
                {
                    for (int i = 0; i < AllWinches.Count; i++)
                    {
                        if (AllWinches[i].WinchName == winch)
                        {
                            PlottingWinches.Add(AllWinches[i].ShallowCopy());
                            break;
                        }
                    }

                }   
            }
        }

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
        partial void OnSelectWinchChanged(string? selectWinch)
        {
            if (SelectWinch != null && AllWinches != null)
            {
                for (int i = 0; i < AllWinches.Count; i++)
                {
                    WinchModel item = AllWinches[i];
                    if (item.WinchName == SelectWinch)
                    {
                        
                        CurrentWinch = AllWinches[i].ShallowCopy();
                        break;
                    }
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<string>? winchNames = new();
        
        //partial void OnAllWinchesChanged(ObservableCollection<WinchModel>? allWinches)
        //{
        //    WinchNames.Clear();
        //    foreach (var item in AllWinches)
        //    {
        //        WinchNames.Add(item.WinchName);
        //    }
        //    //WinchNames = AllWinches.Select(WinchModel => WinchModel.WinchName).ToList();
        //}

    }
}
