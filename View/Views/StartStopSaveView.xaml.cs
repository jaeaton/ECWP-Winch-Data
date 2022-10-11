namespace Views
{
    /// <summary>
    /// Interaction logic for StartStopSaveView.xaml
    /// </summary>
    public partial class StartStopSaveView : UserControl
    {
        public static CancellationTokenSource? _canceller;
        public StartStopSaveView()
        {
            InitializeComponent();
        }
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Step 1: Set source parameters. Input the IP address of the winch and use the port number 50505. If valid parameters are entered fields will turn green, otherwise they will be red. \n" +
                "Step 2: Set destination parameters. If using UDP logging set the logging computer IP address and the UDP port for logging. If valid parameters are entered fields will turn green, otherwise they will be red. \n" +
                "Step 3: Set cruise information. Fill in the name of the cruise and the cast number. If valid parameters are entered fields will turn green, otherwise they will be red. \n" +
                "Step 4: Select options for data collection. \n" +
                "Step 5: Set save file location. If you are saving either the max data or the 20 hz data the file location MUST be set.\n" +
                "Step 6: Start Capture. This connects to the winch and then processes the data as needed and saves, transmits, and displays the data.\n" +
                "Step 7: Save Max Values. This button writes the max values to a file, zeros out the max values, and increments the cast number\n\n" +
                "Notes\n" +
                "1) The program saves a config file and loads it on start up. This can Speed up the set up process after it has been set for a cruise. It is a human readable text file in the program's directory.\n" +
                "2) Max log file should be continuos for a given cruise. Each time the Log Max button is pressed a new entry is added. If the cast number is changed to a lower number it will not overwrite the previous entry.\n" +
                "3) \n\n" +
                "V3.0.0 beta");
        }

        private void ButtonSaveLocation_Click(object sender, RoutedEventArgs e)
        {

            UserInputsView.SetFileNames();

        }

        private void ButtonLogMax_Click(object sender, RoutedEventArgs e)
        {
            //Write the max data for the cast
            DataHandlingViewModel.WriteMaxData(UserInputsView.globalConfig);
            //Increase the cast count
            UserInputsView._configDataStore.CastNumberBox = (int.Parse(UserInputsView._configDataStore.CastNumberBox)+1).ToString();
            UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(UserInputsView._configDataStore);
            
        }
        public void StartStop()
        {

            //Write Start and Stop Logic
            switch (StartStopButton.Content)
            {
                case "Stop Log":
                    {
                        try
                        {
                            //Set cancellation token to cancel to stop data collection
                            _canceller.Cancel();
                        }
                        catch (ObjectDisposedException ex)
                        {

                        }
                        if (UserInputsView._configDataStore.LogMaxDataCheckBox == true)
                        {
                            //Write the max data for the cast
                            DataHandlingViewModel.WriteMaxData(UserInputsView.globalConfig);
                            //Increase the cast count
                            UserInputsView._configDataStore.CastNumberBox = (int.Parse(UserInputsView._configDataStore.CastNumberBox) + 1).ToString();
                            UserInputsView.globalConfig = (GlobalConfigModel)AppConfigViewModel.GetConfig(UserInputsView._configDataStore);

                        }
                        
                        //Change button text
                        UserInputsView._configDataStore.StartStopButtonText = "Start Log";
                        UserInputsView._configDataStore.UserInputsEnable = true;
                        break;
                    }
                default:
                    {
                        if (UserInputsView._configDataStore.Log20HzDataCheckBox ||  UserInputsView._configDataStore.LogMaxDataCheckBox)
                        {
                            //If the save directory is not set show popup
                            if (UserInputsView.globalConfig.SaveDirectorySet == false )
                            {
                                MessageBox.Show("Set save location before colecting data");
                                break;
                            }

                        }
                        
                        
                        //Create new cancellation token at start of data collection
                        _canceller = new CancellationTokenSource();
                        //Starts Data collection on first press
                        DataHandlingViewModel.GetDataAsync(UserInputsView.globalConfig);
                        //change button text
                        UserInputsView._configDataStore.StartStopButtonText =  "Stop Log";
                        UserInputsView._configDataStore.UserInputsEnable = false;

                        break;
                    }
            }
        }
        private void ButtonStartLog_Click(object sender, RoutedEventArgs e)
        {
            StartStop();
            
        }
    }
}
