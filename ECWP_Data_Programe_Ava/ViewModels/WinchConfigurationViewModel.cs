namespace ViewModels
{
    public partial class WinchConfigurationViewModel : ObservableObject
    {
        public ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;
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
            //Loops through all winches and puts winch names in a list for selection process
           foreach (var item in _configDataStore.AllWinches)
            {
                _configDataStore.WinchNames.Add(item.WinchName);
            }
           //Write the config file with updated list of winches
            FileOperationsViewModel.WriteConfig(_configDataStore);
        }

        public void ChangeSerialFormat(bool mtnw)
        {
            if(mtnw)
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
            //Loops through all winches and puts winch names in a list for selection process
            if ( _configDataStore.AllWinches.Count > 0)
            {
                foreach (var item in _configDataStore.AllWinches)
                {
                    _configDataStore.WinchNames.Add(item.WinchName);
                }
            }
            
        }
    }
}
