namespace Views
{
    public partial class UserInputsViewOld : UserControl
    {
        public static ConfigDataStore _configDataStore;
        public static GlobalConfigModel globalConfig = new();
        public UserInputsViewOld()
        {
            InitializeComponent();

            //serialPorts.Items = SerialPort.GetPortNames(); 
            _configDataStore = new ConfigDataStore();

            this.DataContext = _configDataStore;
            _configDataStore.UserInputsEnable = true;
            _configDataStore.StartStopButtonText = "Start Log";
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
            if (globalConfig == null)
            {
                MessageBoxViewModel.DisplayMessage("No Valid configuration");
                return;
            }
            else
                FileOperationsViewModel.WriteConfig(globalConfig);
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.ReadConfig(_configDataStore);
        }
        public async static void SaveFileNames()
        {
            //Check for valid filename constructor
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //ConfigDataStore _configDataStore = UserInputsView._configDataStore;
            //build the save file name
            //DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            //string dateAndHour = dateTime.ToString("yyyyMMddHH");
            //string filename = $"{ dateAndHour } { _configDataStore.CruiseNameBox } cast { _configDataStore.CastNumberBox }.Log";
            //GlobalConfigModel globalConfig = new GlobalConfigModel();
            //var anInstanceofMyClass = new AppConfigViewModel();
            //var instanceofFileOperationsViewModel = new FileOperationsViewModel();
            globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.SetFileNames(globalConfig);
            FileOperationsViewModel.SetFileNames(globalConfig);
            if (globalConfig.LogMaxValuesSwitch)
            {
                saveFileDialog.InitialFileName = globalConfig.MaxLogFileName;
            }
            if (globalConfig.Log20HzSwitch)
            {
                if (!globalConfig.LogUnolsSwitch)
                {
                    saveFileDialog.InitialFileName = globalConfig.Minimal20HzLogFileName;
                }
                else
                {
                    saveFileDialog.InitialFileName = globalConfig.UnolsWireLogName;
                }
            }
            
            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileDialog.InitialFileName);
                globalConfig.SaveDirectory = (string)fileInfo.DirectoryName;
                _configDataStore.DirectoryLabel = globalConfig.SaveDirectory;
                globalConfig.SaveDirectorySet = true;


            }
            
        }
       
        
    }
}
