namespace ViewModels
{
    public partial class WinchConfigurationViewModel : ObservableObject
    {
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;

        [RelayCommand]
        private void AddWinch()
        {
            _configDataStore.AllWinches.Add(_configDataStore.CurrentWinch);
        }
        [RelayCommand]
        private void RemoveWinch()
        {
            _configDataStore.AllWinches.Remove(_configDataStore.CurrentWinch);
        }
        public static void LoadWinch(string winch)
        {
            if (winch != null && MainWindowViewModel._configDataStore.AllWinches != null)
            {
                int i = MainWindowViewModel._configDataStore.AllWinches.FindIndex(a => a.WinchName == winch);
                MainWindowViewModel._configDataStore.CurrentWinch = MainWindowViewModel._configDataStore.AllWinches[i];
            }
        }
        
    }
}
