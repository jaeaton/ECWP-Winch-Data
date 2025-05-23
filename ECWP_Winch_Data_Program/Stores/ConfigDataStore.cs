﻿namespace Store
{
    //Application configuration data for User Inputs View
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<WinchModel> allWinches = new();

        [ObservableProperty]
        private List<string> availableBaudRates = new List<string>
                                                            {
                                                                "57600",
                                                                "38400",
                                                                "19200",
                                                                "9600",
                                                                "4800"
                                                            };

        [ObservableProperty]
        private List<string> availableDataBits = new List<string>
                                                            {
                                                                "8",
                                                                "7",
                                                            };

        [ObservableProperty]
        private List<string> availableParity = new List<string>
                                                            {
                                                                "N",
                                                                "E",
                                                                "O",
                                                            };

        [ObservableProperty]
        private List<string> availablePayouts = new()
        {
                "-10",
                "-5",
                "0",
                "1",
                "5",
                "10",
                "12",
                "25",
                "50"
        };

        [ObservableProperty]
        private List<string> availableProtocols = new List<string>
                                                            {
                                                                "TCP Server",
                                                                "TCP Client",
                                                                "UDP"
                                                            };

        [ObservableProperty]
        private List<string> availableProtocolsOutput = new List<string> { "UDP" };

        [ObservableProperty]
        private List<string> availableSerialPorts = GetSerialPorts.FindSerialPorts();

        [ObservableProperty]
        private List<string> availableStopBits = new List<string>
                                                            {
                                                                "1",
                                                                "1.5",
                                                                "2",
                                                            };

        [ObservableProperty]
        private List<string> availableTensions = new()
        {
                "-100",
                "0",
                "100",
                "250",
                "500"
        };

        [ObservableProperty]
        private string buttonText = "Start Processing";

        [ObservableProperty]
        private List<string> chartTimeSpanList = new List<string>
                                                            {
                                                                "10",
                                                                "20",
                                                                "30",
                                                                "45"
                                                            };

        [ObservableProperty]
        private string cruiseNameBox = string.Empty;

        [ObservableProperty]
        private WinchModel currentWinch = new();

        [ObservableProperty]
        private List<string> dataProtocolList = new List<string>
                                                            {
                                                                "MASH",
                                                                "Jay Jay",
                                                                "UNOLS String",
                                                                "3PS",
                                                                "Mermac R30",
                                                                "MTNW 1",
                                                                "MTNW Legacy",
                                                                "Hawboldt SPRE-3464",
                                                                "Hawboldt SPRE-2648RS",
                                                                "Hawboldt SPRE-2640",
                                                                "Hawboldt SPRE-2036S"
                                                            };

        [ObservableProperty]
        private bool dateRangeCheckBox = false;

        [ObservableProperty]
        private string directoryLabel = string.Empty;

        [ObservableProperty]
        private bool directorySet;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today;

        [ObservableProperty]
        private List<double> factorOfSafetyList = new List<double>
                                                            {
                                                                5.0,
                                                                2.5,
                                                                2.0,
                                                                1.5
                                                            };

        [ObservableProperty]
        private int numberOfFiles = 0;

        [ObservableProperty]
        private int numberOfProcessedFiles = 0;

        [ObservableProperty]
        private List<string> payoutUnitList = new List<string>
                                                            {
                                                                "m",
                                                                "ft",
                                                                "km"
                                                            };

        [ObservableProperty]
        private ObservableCollection<WinchModel> plottingWinches = new();

        [ObservableProperty]
        private string readingLine = string.Empty;

        [ObservableProperty]
        private string selectedProtocol = string.Empty;

        [ObservableProperty]
        private string selectWinch = string.Empty;

        [ObservableProperty]
        private string shipName = string.Empty;

        [ObservableProperty]
        private List<string> speedUnitList = new List<string>
                                                            {
                                                                "m/min",
                                                                "ft/min",
                                                                "kph",
                                                                "mph"
                                                            };

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private ObservableCollection<TabItemModel> tabItems = new()
        {
            new TabItemModel("Add New", "Add New")
        };

        [ObservableProperty]
        private List<string> tensionUnitList = new List<string>
                                                            {
                                                                "lbf",
                                                                "kg",
                                                                "kip",
                                                                "N",
                                                                "Short Ton",
                                                                "Long Ton",
                                                                "Tonne"
                                                            };

        [ObservableProperty]
        private bool userInputsEnable = true;

        [ObservableProperty]
        private List<string> winchDataType = new()
        {
                "MASH Winch",
                "ECWP MTNW",
                "SIO Traction Winch",
                "WinchDAC", //Previously Armstrong Cast 6
                "UNOLS String",
                "Jay Jay",
                "Atlantis 3PS",
                "3PS",
                //"Mermac R30"
        };

        [ObservableProperty]
        private ObservableCollection<string> winchesToPlot = new();

        [ObservableProperty]
        private ObservableCollection<string> winchNames = new();

        [ObservableProperty]
        private TabItemModel winchSelected = new();

        [ObservableProperty]
        private string winchSelection = string.Empty;

        [ObservableProperty]
        private string wireLogEventCutBack = string.Empty;

        [ObservableProperty]
        private DateTime wireLogEventDate = DateTime.Now;

        [ObservableProperty]
        private List<string> wireLogEventList = new List<string>
                                                            {
                                                                "Cut Back",
                                                                "Installation",
                                                                "Lubrication",
                                                                "Other",
                                                                "Removal",
                                                                "Termination"
                                                            };

        [ObservableProperty]
        private string wireLogEventNotes = string.Empty;

        [ObservableProperty]
        private string wireLogEventSelection = string.Empty;

        [ObservableProperty]
        private List<string> towYoTimeList = new List<string> 
                                                            { 
                                                                "1",
                                                                "2",
                                                                "3",
                                                                "4",
                                                                "5",
                                                                "10",
                                                                "15"
                                                            };
        [ObservableProperty]
        private string towYoTimeSelected = string.Empty;

        [ObservableProperty]
        private bool towYoChecked = false;
        public void LoadWinch(string winch)
        {
            if (winch == "Add New")
            {
                CurrentWinch = new WinchModel();
                return;
            }
            if (winch != string.Empty && AllWinches != null)
            {
                int index = 0;

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
            TabItems.Add(new TabItemModel("Add New", "Add New"));
            foreach (var winch in winches)
            {
                TabItemModel tabitem = new TabItemModel(winch.WinchName, winch.WinchName);
                TabItems.Add(tabitem);
            }
        }

        public void ResetCasts()
        {
            foreach (var winch in AllWinches)
            {
                winch.CastNumber = "1";
            }
        }

        partial void OnCruiseNameBoxChanged(string value)
        {
            ResetCasts();
        }

        // = new();
        partial void OnSelectWinchChanged(string value)
        {
            ProcessDataViewModel.ParseData.WireLog.Clear();
            LoadWinch(value);
        }

        partial void OnWinchSelectedChanged(TabItemModel value)
        {
            ProcessDataViewModel.ParseData.WireLog.Clear();
            if (value != null)
            {
                LoadWinch(value.Header);
            }
        }
    }
}