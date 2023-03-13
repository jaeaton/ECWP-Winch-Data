using System.Diagnostics;

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
            ////Creates a new set of data so as not to reference existing data
            //WinchModel Winch = _configDataStore.CurrentWinch.ShallowCopy();
            WinchModel Winch = _configDataStore.CurrentWinch.DeepCopy();
            Winch.CastNumber = "1";
            InsertWinch(Winch);
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
            FileOperationsViewModel.WriteConfig(_configDataStore);
        }
               
        //public void LoadWinch(string? winch)
        //{
        //    if (winch != null && _configDataStore.AllWinches != null)
        //    {
        //        int index = -1;

        //        for (int i = 0; i < _configDataStore.AllWinches.Count; i++)
        //        {
        //            WinchModel item = _configDataStore.AllWinches[i];
        //            if (item.WinchName == winch)
        //            {
        //                index = i;
        //                break;
        //            }
        //        }
        //        _configDataStore.CurrentWinch = _configDataStore.AllWinches[index].ShallowCopy();
        //    }
        //}

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
            _configDataStore.AllWinches.Add(Winch);
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
