namespace Views
{
    public partial class UserInputsView : UserControl
    {
        //public ConfigDataStore? _configDataStore = MainWindowViewModel._configDataStore;
        public static GlobalConfigModel? globalConfig = new();
        public UserInputsView()
        {
            //Initialized += OnInitialized;
            //InitializeComponent();
            //_configDataStore = new ConfigDataStore();
            
            this.DataContext = MainWindowViewModel._configDataStore;
            
            MainWindowViewModel._configDataStore.UserInputsEnable = true;
            MainWindowViewModel._configDataStore.StartStopButtonText = "Start Log";
            MainWindowViewModel._configDataStore.AvailableSerialPorts = ViewModels.GetSerialPorts.FindSerialPorts();
            MainWindowViewModel._configDataStore.AvailableBaudRates = new List<string>
                                                            {
                                                                "57600",
                                                                "38400",
                                                                "19200",
                                                                "9600",
                                                                "4800"
                                                            };
            MainWindowViewModel._configDataStore.AvailableProtocols = new List<string>
                                                            {
                                                                "UNOLS String",
                                                                "LCI-90i",
                                                                "Hawboldt String"
                                                            };
            MainWindowViewModel._configDataStore.SpeedUnitList = new List<string>
                                                            {
                                                                "m/min",
                                                                "ft/min"
                                                            };
            MainWindowViewModel._configDataStore.PayoutUnitList = new List<string>
                                                            {
                                                                "m",
                                                                "ft"
                                                            };
            MainWindowViewModel._configDataStore.TensionUnitList = new List<string>
                                                            {
                                                                "kg",
                                                                "lbf"
                                                            };
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.ReadConfig(MainWindowViewModel._configDataStore);
            InitializeComponent();
            //FileOperationsViewModel.WriteConfig(globalConfig);

        }
       

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);
            if (globalConfig == null)
            {
                MessageBoxViewModel.DisplayMessage("No Valid configuration");
                return;
            }
            else
                FileOperationsViewModel.WriteConfig(globalConfig);
        }
        //private void WinchDataMove(object sender, SelectionChangedEventArgs e)
        //{
        //    WinchConfigurationViewModel.LoadWinch(MainWindowViewModel._configDataStore.SelectWinch);
        //}

        //Moved to StartStoSaveView
        //public async static void SaveFileNames()
        //{
        //    //Check for valid filename constructor
        //    // Show the save file dialog
        //    SaveFileDialog saveFileDialog = new();
        //    //ConfigDataStore _configDataStore = UserInputsView._configDataStore;
        //    //build the save file name
        //    //DateTime dateTime = DateTime.Now;
        //    //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
        //    //string dateAndHour = dateTime.ToString("yyyyMMddHH");
        //    //string filename = $"{ dateAndHour } { _configDataStore.CruiseNameBox } cast { _configDataStore.CastNumberBox }.Log";
        //    //GlobalConfigModel globalConfig = new GlobalConfigModel();
        //    //var anInstanceofMyClass = new AppConfigViewModel();
        //    //var instanceofFileOperationsViewModel = new FileOperationsViewModel();
        //    globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
        //    globalConfig = (GlobalConfigModel)FileOperationsViewModel.SetFileNames(globalConfig);
        //    FileOperationsViewModel.SetFileNames(globalConfig);
        //    if (globalConfig.LogMaxValuesSwitch)
        //    {
        //        saveFileDialog.InitialFileName = globalConfig.MaxLogFileName;
        //    }
        //    if (globalConfig.Log20HzSwitch)
        //    {
        //        if (!globalConfig.LogUnolsSwitch)
        //        {
        //            saveFileDialog.InitialFileName = globalConfig.Minimal20HzLogFileName;
        //        }
        //        else
        //        {
        //            saveFileDialog.InitialFileName = globalConfig.UnolsWireLogName;
        //        }
        //    }

        //    string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
        //    if (saveFileName != null)
        //    {
        //        //DirectoryLabel.Content = saveFileDialog.InitialFileName;
        //        FileInfo fileInfo = new(saveFileName);
        //        globalConfig.SaveDirectory = (string)fileInfo.DirectoryName;
        //        _configDataStore.DirectoryLabel = globalConfig.SaveDirectory;
        //        globalConfig.SaveDirectorySet = true;


        //    }

        //}
    }
}
