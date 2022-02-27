namespace Views
{
    /// <summary>
    /// Interaction logic for UserInputsView.xaml
    /// </summary>
    public partial class UserInputsView : UserControl
    {
        public static ConfigDataStore _configDataStore;
        public static GlobalConfigModel globalConfig = new GlobalConfigModel();
        public UserInputsView()
        {
            
            InitializeComponent();
            _configDataStore = new ConfigDataStore();
            
            this.DataContext = _configDataStore;
            
            
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //ConfigDataStore _configDataStore = UserInputsView._configDataStore;
            //build the save file name
            //DateTime dateTime = DateTime.Now;
            //string stringDateTime = dateTime.ToString("yyyyMMddTHHmmssfff");
            //string dateAndHour = dateTime.ToString("yyyyMMddHH");
            //string filename = $"{ dateAndHour } { _configDataStore.cruiseNameBox } cast { _configDataStore.castNumberBox }.Log";
            //GlobalConfigModel globalConfig = new GlobalConfigModel();
            //var anInstanceofMyClass = new AppConfigViewModel();
            //var instanceofFileOperationsViewModel = new FileOperationsViewModel();
            globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(_configDataStore);
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.SetFileNames(globalConfig);
            FileOperationsViewModel.SetFileNames(globalConfig);
            if ((bool)globalConfig.LogMaxValuesSwitch)
            {
                saveFileDialog.FileName = globalConfig.MaxLogFileName;
            }
            if ((bool)globalConfig.Log20HzSwitch)
            {
                saveFileDialog.FileName = globalConfig.Minimal20HzLogFileName;
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                //directoryLabel.Content = saveFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                globalConfig.SaveDirectory = (string)fileInfo.DirectoryName;
                _configDataStore.directoryLabel = globalConfig.SaveDirectory;
                globalConfig.SaveDirectorySet = true;
                

            }

        }
        
    }
}
