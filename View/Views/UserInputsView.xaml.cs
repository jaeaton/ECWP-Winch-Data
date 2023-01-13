namespace Views
{
    /// <summary>
    /// Interaction logic for UserInputsView.xaml
    /// </summary>
    public partial class UserInputsView : UserControl
    {
        public static ConfigDataStore _configDataStore;
        public static GlobalConfigModel globalConfig = new();
        public UserInputsView()
        {
            
            InitializeComponent();
            //static serialPort = new SerialPorts();
            serialPorts.ItemsSource = SerialPort.GetPortNames(); 
            _configDataStore = new ConfigDataStore();
            
            this.DataContext = _configDataStore;
            _configDataStore.UserInputsEnable = true;
            _configDataStore.StartStopButtonText = "Start Log";
            

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
            if(globalConfig == null)
            {
                MessageBox.Show("No Valid configuration");
                return;
            }
            else
            FileOperationsViewModel.WriteConfig(globalConfig);
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.ReadConfig(_configDataStore);
        }
        public static void SetFileNames()
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
                saveFileDialog.FileName = globalConfig.MaxLogFileName;
            }
            if (globalConfig.Log20HzSwitch)
            {
                if (!globalConfig.LogUnolsSwitch)
                {
                    saveFileDialog.FileName = globalConfig.Minimal20HzLogFileName;
                }
                else
                {
                    saveFileDialog.FileName = globalConfig.UnolsWireLogName;
                }
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                //DirectoryLabel.Content = saveFileDialog.FileName;
                FileInfo fileInfo = new(saveFileDialog.FileName);
                globalConfig.SaveDirectory = (string)fileInfo.DirectoryName;
                _configDataStore.DirectoryLabel = globalConfig.SaveDirectory;
                globalConfig.SaveDirectorySet = true;
                

            }

        }
        
    }
}
