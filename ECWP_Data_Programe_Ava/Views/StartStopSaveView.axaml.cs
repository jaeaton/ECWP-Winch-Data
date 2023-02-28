using Store;

namespace Views
{
    public partial class StartStopSaveView : UserControl
    {
        public static CancellationTokenSource? _canceller;
        public StartStopSaveView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel._configDataStore;
        }
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxViewModel.DisplayMessage("Step 1: Set source parameters. Input the IP address of the winch and use the port number 50505.  \n" +
                "Step 1a: For LCI-90i connections IP adress is of the host computer. LCI-90i should be configured to send a single winch \n" +
                "using either the MTNW Legacy or MTNW1 Protocol. \n" +
                "Step 2: Set destination parameters. If using UDP logging set the logging computer IP address and the UDP port for logging. \n" +
                "Step 3: Set cruise information. Fill in the name of the cruise and the cast number. \n" +
                "Step 4: Select options for data collection. \n" +
                "Step 5: Set save file location. If you are saving either the max data or the 20 hz data the file location MUST be set.\n" +
                "Step 6: Start Capture. This connects to the winch and then processes the data as needed and saves, transmits, and displays the data.\n" +
                "Step 7: Save Max Values. This button writes the max values to a file, zeros out the max values, and increments the cast number\n\n" +
                "Notes\n" +
                "1) The program saves a config file and loads it on start up. This can Speed up the set up process after it has been set for a cruise. It is a human readable text file in the program's directory.\n" +
                "2) Max log file should be continuos for a given cruise. Each time the Log Max button is pressed a new entry is added. If the cast number is changed to a lower number it will not overwrite the previous entry.\n" +
                "3) Description of Data Source selection:\n" +
                "    a) LCI-90i Data Source implies a TCP connection with the data source acting as a TCP Client.\n" +
                "    b) UNOLS String Data Source implies a TCP Connection with the data source acting as a TCP Server/Listener. \n" +
                "    c) Hawboldt Data Source has not been implemented.\n" +
                "\n\n" +
                 "V4.2.0"); 
        }

        private void ButtonSaveLocation_Click(object sender, RoutedEventArgs e)
        {

            SaveFileNames();

        }

        private void ButtonLogMax_Click(object sender, RoutedEventArgs e)
        {
            //Write the max data for the cast
            DataHandlingViewModel.WriteMaxData(UserInputsView.globalConfig);
            //Increase the cast count
            MainWindowViewModel._configDataStore.CastNumberBox = (int.Parse(MainWindowViewModel._configDataStore.CastNumberBox) + 1).ToString();
            UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);

        }
        //public void StartStop()
        //{

        //    //Write Start and Stop Logic
        //    switch (StartStopButton.Content)
        //    {
        //        case "Stop Log":
        //            {
        //                try
        //                {
        //                    //Set cancellation token to cancel to stop data collection
        //                    _canceller.Cancel();
        //                }
        //                catch (ObjectDisposedException ex)
        //                {

        //                }
        //                if (MainWindowViewModel._configDataStore.LogMaxDataCheckBox == true)
        //                {
        //                    //Write the max data for the cast
        //                    DataHandlingViewModel.WriteMaxData(UserInputsView.globalConfig);
        //                    //Increase the cast count
        //                    MainWindowViewModel._configDataStore.CastNumberBox = (int.Parse(MainWindowViewModel._configDataStore.CastNumberBox) + 1).ToString();
        //                    UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);

        //                }

        //                //Change button text
        //                MainWindowViewModel._configDataStore.StartStopButtonText = "Start Log";
        //                MainWindowViewModel._configDataStore.UserInputsEnable = true;
        //                break;
        //            }
        //        default:
        //            {
        //                if (MainWindowViewModel._configDataStore.Log20HzDataCheckBox || MainWindowViewModel._configDataStore.LogMaxDataCheckBox)
        //                {
        //                    //If the save directory is not set show popup
        //                    if (UserInputsView.globalConfig.SaveDirectorySet == false)
        //                    {
        //                        MessageBoxViewModel.DisplayMessage("Set save location before colecting data");
        //                        break;
        //                    }

        //                }

        //                //ChartDataViewModel.ResetData();
        //                //Create new cancellation token at start of data collection
        //                _canceller = new CancellationTokenSource();
        //                //Starts Data collection on first press
        //                DataHandlingViewModel.GetDataAsync(UserInputsView.globalConfig);
        //                //change button text
        //                MainWindowViewModel._configDataStore.StartStopButtonText = "Stop Log";
        //                MainWindowViewModel._configDataStore.UserInputsEnable = false;

        //                break;
        //            }
        //    }
        //}
        //private void ButtonStartLog_Click(object sender, RoutedEventArgs e)
        //{
        //    StartStop();

        //}

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
            UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(MainWindowViewModel._configDataStore);
            UserInputsView.globalConfig = (GlobalConfigModel)FileOperationsViewModel.SetFileNames(UserInputsView.globalConfig);
            FileOperationsViewModel.SetFileNames(UserInputsView.globalConfig);
            if (UserInputsView.globalConfig.LogMaxValuesSwitch)
            {
                saveFileDialog.InitialFileName = UserInputsView.globalConfig.MaxLogFileName;
            }
            if (UserInputsView.globalConfig.Log20HzSwitch)
            {
                if (!UserInputsView.globalConfig.LogUnolsSwitch)
                {
                    saveFileDialog.InitialFileName = UserInputsView.globalConfig.Minimal20HzLogFileName;
                }
                else
                {
                    saveFileDialog.InitialFileName = UserInputsView.globalConfig.UnolsWireLogName;
                }
            }

            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileName);
                UserInputsView.globalConfig.SaveDirectory = (string)fileInfo.DirectoryName;
                MainWindowViewModel._configDataStore.DirectoryLabel = UserInputsView.globalConfig.SaveDirectory;
                UserInputsView.globalConfig.SaveDirectorySet = true;


            }

        }
    }
}
