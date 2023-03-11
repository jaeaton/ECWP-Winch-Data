using Models;

namespace ViewModels
{
    public partial class MainLiveDataViewModel : ObservableObject
    {
        
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;
        //move code from code behind to here
        [RelayCommand]
        private async void PlotHelp()
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
                 "V5.0.0");
        }
        [RelayCommand]
        private async void SaveLocation()
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //Set the dummy filename
            saveFileDialog.InitialFileName = "Date_Cruise_Winch.log";
            
            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileName);
                _configDataStore.DirectoryLabel = (string)fileInfo.DirectoryName;
                _configDataStore.DirectorySet = true;
            }

        }

        public void PlotSelectionChanged(bool selected, string? WinchName)
        {
            if (selected == true)
            {
                _configDataStore.WinchesToPlot.Add(WinchName);
            }
            if (selected == false)
            {
                MainWindowViewModel._configDataStore.WinchesToPlot.Remove(WinchName);
            }
            _configDataStore.PlottingWinches.Clear();
            if (_configDataStore.AllWinches != null && _configDataStore.WinchesToPlot != null)
            {
                foreach (var winch in _configDataStore.WinchesToPlot)
                {
                    for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
                    {
                        if (_configDataStore.AllWinches[i].WinchName == winch)
                        {
                            //_configDataStore.PlottingWinches.Add(_configDataStore.AllWinches[i].ShallowCopy());
                            _configDataStore.PlottingWinches.Add(_configDataStore.AllWinches[i].DeepCopy());
                            break;
                        }
                    }

                }
            }
        }
    }
}
