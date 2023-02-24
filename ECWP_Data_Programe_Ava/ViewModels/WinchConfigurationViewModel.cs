namespace ViewModels
{
    public partial class WinchConfigurationViewModel : ObservableObject
    {
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;

        [RelayCommand]
        private void AddWinch()
        {
            //WinchModel Winch = new WinchModel();
            WinchModel Winch = MainWindowViewModel._configDataStore.CurrentWinch.ShallowCopy();
            MainWindowViewModel._configDataStore.AllWinches.Add(Winch);
            MainWindowViewModel._configDataStore.WinchNames.Clear();
            foreach (var item in MainWindowViewModel._configDataStore.AllWinches)
            {
                MainWindowViewModel._configDataStore.WinchNames.Add(item.WinchName);
            }

        }
        [RelayCommand]
        private void RemoveWinch()
        {
            MainWindowViewModel._configDataStore.AllWinches.Remove(MainWindowViewModel._configDataStore.CurrentWinch);
            MainWindowViewModel._configDataStore.WinchNames.Clear();
            foreach (var item in MainWindowViewModel._configDataStore.AllWinches)
            {
                MainWindowViewModel._configDataStore.WinchNames.Add(item.WinchName);
            }
        }
        public static void LoadWinch(string? winch)
        {
            if (winch != null && MainWindowViewModel._configDataStore.AllWinches != null)
            {
                //int i = MainWindowViewModel._configDataStore.AllWinches.FindIndex(a => a.WinchName == winch);
                int index = -1;

                for (int i = 0; i < MainWindowViewModel._configDataStore.AllWinches.Count; i++)
                {
                    WinchModel item = MainWindowViewModel._configDataStore.AllWinches[i];
                    if (item.WinchName == winch)
                    {
                        index = i;
                        break;
                    }
                }
                MainWindowViewModel._configDataStore.CurrentWinch = MainWindowViewModel._configDataStore.AllWinches[index];
            }
        }
        
    }
}
