namespace ViewModels
{
    internal partial class WinchViewModel : ViewModelBase
    {
        public ConfigDataStore _configDataStore = MainViewModel._configDataStore;
        /// <summary>
        /// Add Winch adds the data currently stored in current Winch as a new selectable winch 
        /// </summary>
        [RelayCommand]
        public void AddWinch()
        {
            //Send to generic method to add winch to the winch list
            InsertWinch(_configDataStore.CurrentWinch);
            //Write the config file 
            FileOperationsViewModel.WriteConfig(_configDataStore);
        }
        [RelayCommand]
        private void RemoveWinch()
        {
            //Removes the data in the winch entry form from the list of winches
            if (_configDataStore.CurrentWinch != null && _configDataStore.AllWinches != null)
            {
                int index = -1;
                string name = _configDataStore.CurrentWinch.WinchName;
                for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
                {
                    WinchModel item = _configDataStore.AllWinches[i];
                    if (item.WinchName == name)
                    {
                        index = i;
                        break;
                    }
                }
                _configDataStore.AllWinches.RemoveAt(index);
            }
            //Clears the current list to make winch names as fresh as possible
            _configDataStore.WinchNames.Clear();
            _configDataStore.TabItems.Clear();
            new TabItemModel("Add New", "Add New");
            //Loops through all winches and puts winch names in a list for selection process
            foreach (var item in _configDataStore.AllWinches)
            {
                TabItemModel tabItem = new TabItemModel(item.WinchName, item.WinchName);
                _configDataStore.WinchNames.Add(item.WinchName);
                _configDataStore.TabItems.Add(tabItem);
            }
            //Write the config file with updated list of winches
            FileOperationsViewModel.WriteConfig(_configDataStore);
        }
        [RelayCommand]
        public void ConfigHelp()
        {
            MessageBoxViewModel.DisplayMessage($"Step 1. Provide a unique name to the winch. \n" +
                $"Step 2. Fill in communication parameters.\n" +
                "   a. Input an IP address of the winch for TCP Server Source (ECWP Winches: Moe, Larry, Curly, Shemp, Gloria, Jay Jay) or \n" +
                "   that of the host computer for TCP Client and UDP.\n" +
                "   b. Enter a port number for the communications (ECWP uses 50505)\n" +
                "   c. Select a source type from the drop down list. \n" +
                "       TCP Server -- ECWP Winch\n" +
                "       TCP Client -- LCI-90i\n" +
                "       UDP -- UDP Sources\n" +
                "   d. For winches that use the Hawboldt protocol, check the box and select the model number.\n" +
                "Step 3. Select the logging paramters\n" +
                "   a. Log 20Hz Data logs the input data at the input data rate to the local machine.\n" +
                "   Then select the log format to use. MTNW format includes the date, time, tension, \n" +
                "   speed, and payout. UNOLS format adds fields for Tension member alarms and warnings.\n" +
                "   b. Log Max Data allows the logging of the cast maximum tension and payout strings.\n " +
                "   c. Use computer time uses the time from the computer running this program for log times.\n" +
                "   d. Tension, Speed, and Payout unit selection sets the units for the incoming data.\n" +
                "Step 4. Send UDP data enables the sending of the data via UDP to another computer.\n" +
                "Step 5. Send Serial data enables sending the data via serial port.\n" +
                "Step 6. Add/Update winch either adds the information to the winch list or updates\n" +
                "   an existing entry with a namtching name.\n" +
                "Step 7. Remove winch removes the winch from the winch list.\n" +
                "Step 8. Using the drop down at the top will load the information of the selected winch." +
                "Note: Adding/Updating or Removing a winch writes a configuration file to application directory.");
        }

        public void ChangeSerialFormat(bool mtnw)
        {
            if (mtnw)
            {
                _configDataStore.CurrentWinch.SerialFormat = $"MTNW";
            }
            else
            {
                _configDataStore.CurrentWinch.SerialFormat = $"UNOLS";
            }
        }

        public void ChangeUDPFormat(bool mtnw)
        {
            if (mtnw)
            {
                _configDataStore.CurrentWinch.UdpFormat = $"MTNW";
            }
            else
            {
                _configDataStore.CurrentWinch.UdpFormat = $"UNOLS";
            }
        }

        public void ChangeLogFormat(bool mtnw)
        {
            if (mtnw)
            {
                _configDataStore.CurrentWinch.LogFormat = $"MTNW";
            }
            else
            {
                _configDataStore.CurrentWinch.LogFormat = $"UNOLS";
            }
        }

        public void InsertWinch(WinchModel Winch)
        {
            //Check to see if a cast number has been added. If not set to 1
            if (Winch.CastNumber == null)
            {
                Winch.CastNumber = "1";
            }
            //Check to see if the start button has a name. If not set to "start log"
            if (Winch.StartStopButtonText == null)
            {
                Winch.StartStopButtonText = "Start Log";
            }
            //See if winch name is already used. If it is perform update on parameters. If not add it to the list
            int index = -1;
            for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
            {
                WinchModel item = _configDataStore.AllWinches[i];
                if (item.WinchName == Winch.WinchName)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                _configDataStore.AllWinches[index] = Winch.DeepCopy();
            }
            else
            {
                _configDataStore.AllWinches.Add(Winch.DeepCopy());
            }
            //Clears the current list to make winch names as fresh as possible
            _configDataStore.WinchNames.Clear();
            _configDataStore.TabItems.Clear();
            new TabItemModel("Add New", "Add New");
            //Loops through all winches and puts winch names in a list for selection process
            if (_configDataStore.AllWinches.Count > 0)
            {
                foreach (var item in _configDataStore.AllWinches)
                {
                    TabItemModel tabItem = new TabItemModel(item.WinchName, item.WinchName);
                    _configDataStore.WinchNames.Add(item.WinchName);
                    _configDataStore.TabItems.Add(tabItem);
                }
            }

        }

        [RelayCommand]
        public void AddCommOut()
        {
            InsertCommOut();
        }

        [RelayCommand]
        public void RemoveCommOut()
        {
            //Removes the data in the winch entry form from the list of Output comms
            if (_configDataStore.CurrentWinch.OutputCommunication != null && _configDataStore.CurrentWinch.AllOutputCommunication != null)
            {
                int index = -1;
                string name = _configDataStore.CurrentWinch.OutputCommunication.DestinationName;
                for (int i = 0; i < _configDataStore.CurrentWinch.AllOutputCommunication.Count; i++)
                {
                    CommunicationModel item = _configDataStore.CurrentWinch.AllOutputCommunication[i];
                    if (item.DestinationName == name)
                    {
                        index = i;
                        break;
                    }
                }
                _configDataStore.CurrentWinch.AllOutputCommunication.RemoveAt(index);
            }
            //Clears the current list to make Comm names as fresh as possible
            //_configDataStore.WinchNames.Clear();
            _configDataStore.CurrentWinch.TabItemsOutputComms.Clear();
            new TabItemModel("Add New", "Add New");
            //Loops through all winches and puts winch names in a list for selection process
            foreach (var item in _configDataStore.CurrentWinch.AllOutputCommunication)
            {
                TabItemModel tabItem = new TabItemModel(item.DestinationName, item.DestinationName);
                //_configDataStore.WinchNames.Add(item.DestinationName);
                _configDataStore.CurrentWinch.TabItemsOutputComms.Add(tabItem);
            }
            
        }

        public void InsertCommOut()
        {
            //See if Comm name is already used. If it is perform update on parameters. If not add it to the list
            int index = -1;
            for (int i = 0; i < _configDataStore.CurrentWinch.AllOutputCommunication.Count; i++)
            {
                CommunicationModel item = _configDataStore.CurrentWinch.AllOutputCommunication[i];
                if (item.DestinationName == _configDataStore.CurrentWinch.OutputCommunication.DestinationName)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                _configDataStore.CurrentWinch.AllOutputCommunication[index] = _configDataStore.CurrentWinch.OutputCommunication.ShallowCopy();
            }
            else
            {
                _configDataStore.CurrentWinch.AllOutputCommunication.Add(_configDataStore.CurrentWinch.OutputCommunication.ShallowCopy());
            }
            //Clears the current list to make winch names as fresh as possible
            //_configDataStore.CurrentWinch.AllOutputCommunication.Clear();
            _configDataStore.CurrentWinch.TabItemsOutputComms.Clear();
            new TabItemModel("Add New", "Add New");
            //Loops through all winches and puts winch names in a list for selection process
            if (_configDataStore.CurrentWinch.AllOutputCommunication.Count > 0)
            {
                foreach (var item in _configDataStore.CurrentWinch.AllOutputCommunication.ToList())
                {
                    TabItemModel tabItem = new TabItemModel(item.DestinationName, item.DestinationName);
                    //_configDataStore.WinchNames.Add(item.WinchName);
                    _configDataStore.CurrentWinch.TabItemsOutputComms.Add(tabItem);
                }
            }
        }

        [RelayCommand]
        public void AddWireLogEvent()
        {
            ExcelViewModel.AddEvent();
        }
        [RelayCommand]
        public async void SetWinchPath()
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new();
            //build the save file name
            saveFileDialog.InitialFileName = _configDataStore.CurrentWinch.WinchName;
            string saveFileName = await saveFileDialog.ShowAsync(MainWindow.Instance);
            if (saveFileName != null)
            {
                //DirectoryLabel.Content = saveFileDialog.InitialFileName;
                FileInfo fileInfo = new(saveFileName);
                _configDataStore.CurrentWinch.WinchDirectory = fileInfo.DirectoryName;
            }
        }
    }
}
