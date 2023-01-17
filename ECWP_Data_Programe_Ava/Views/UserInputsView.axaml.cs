using Avalonia.Markup.Xaml;

namespace Views
{
    public partial class UserInputsView : UserControl
    {
        public static ConfigDataStore? _configDataStore;
        public static GlobalConfigModel? globalConfig = new();
        public UserInputsView()
        {
            //Initialized += OnInitialized;
            InitializeComponent();
            _configDataStore = new ConfigDataStore();
            
            this.DataContext = _configDataStore;
            globalConfig = (GlobalConfigModel)FileOperationsViewModel.ReadConfig(_configDataStore);
            _configDataStore.UserInputsEnable = true;
            _configDataStore.StartStopButtonText = "Start Log";
            _configDataStore.AvailableSerialPorts = ViewModels.GetSerialPorts.FindSerialPorts();
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
