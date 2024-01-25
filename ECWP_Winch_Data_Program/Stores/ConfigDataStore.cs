namespace Store
{
    //Application configuration data for User Inputs View
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private string cruiseNameBox = string.Empty;

        [ObservableProperty]
        private string shipName = string.Empty;

        [ObservableProperty]
        private string directoryLabel = string.Empty;

        [ObservableProperty]
        private bool directorySet;

        [ObservableProperty]
        private bool userInputsEnable = true;

        [ObservableProperty]
        private List<string> availableSerialPorts = GetSerialPorts.FindSerialPorts();

        [ObservableProperty]
        private List<string> availableBaudRates= new List<string>
                                                            {
                                                                "57600",
                                                                "38400",
                                                                "19200",
                                                                "9600",
                                                                "4800"
                                                            };

    [ObservableProperty]
        private string winchSelection = string.Empty;

        [ObservableProperty]
        private List<string> availableProtocols = new List<string>
                                                            {
                                                                "TCP Server",
                                                                "TCP Client",
                                                                "UDP"
                                                            };

        [ObservableProperty]
        private string selectedProtocol = string.Empty;

        [ObservableProperty]
        private WinchModel currentWinch = new();

        [ObservableProperty]
        private ObservableCollection<WinchModel> allWinches = new();
        
        [ObservableProperty]
        private ObservableCollection<string> winchesToPlot = new();

        [ObservableProperty]
        private ObservableCollection<WinchModel> plottingWinches = new();

        [ObservableProperty]
        private List<string> speedUnitList = new List<string>
                                                            {
                                                                "m/min",
                                                                "ft/min"
                                                            };

        [ObservableProperty]
        private List<string> tensionUnitList = new List<string>
                                                            {
                                                                "kg",
                                                                "lbf"
                                                            };

        [ObservableProperty]
        private List<string> payoutUnitList = new List<string>
                                                            {
                                                                "m",
                                                                "ft"
                                                            };

        [ObservableProperty]
        private List<string> hawboldtModelList = new List<string>
                                                            {
                                                                "SPRE-3464",
                                                                "SPRE-2648RS",
                                                                "SPRE-2640",
                                                                "SPRE-2036S"
                                                            };

        [ObservableProperty]
        private List<double> factorOfSafetyList = new List<double>
                                                            {
                                                                5.0,
                                                                2.5,
                                                                2.0,
                                                                1.5
                                                            };

        [ObservableProperty]
        private string selectWinch = string.Empty;// = new();
        partial void OnSelectWinchChanged(string? value)
        {
            LoadWinch(value);
        }
        [ObservableProperty]
        private TabItemModel winchSelected;
        partial void OnWinchSelectedChanged(TabItemModel? value)
        {
            if (value != null)
            {
                LoadWinch(value.Header);
            }
            
        }
        [ObservableProperty]
        private ObservableCollection<string> winchNames = new();
        
        [ObservableProperty]
        private List<string> chartTimeSpanList = new List<string>
                                                            {
                                                                "10",
                                                                "20",
                                                                "30",
                                                                "45"
                                                            };

        [ObservableProperty]
        private ObservableCollection<TabItemModel> tabItems = new()
        {
            new TabItemModel("Add New", "Add New")
        };

        public void LoadWinch(string winch)
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
                //Deep copy to break link between class objects
                CurrentWinch = AllWinches[index].DeepCopy();
            }
        }

        public void RefreshWinches(ObservableCollection<WinchModel> winches)
        {
            TabItems.Clear();
            new TabItemModel("Add New", "Add New");
            foreach (var winch in winches)
            {
                TabItemModel tabitem = new TabItemModel(winch.WinchName, winch.WinchName);
                TabItems.Add(tabitem);
            }
        }
    }

}
