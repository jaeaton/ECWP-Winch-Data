namespace ViewModels
{
    public partial class WinchConfigurationViewModel : ObservableObject
    {
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;

        [RelayCommand]
        private void AddWinch()
        {
            MainWindowViewModel._configDataStore.AllWinches.Add(MainWindowViewModel._configDataStore.CurrentWinch);
            
        }
        [RelayCommand]
        private void RemoveWinch()
        {
            MainWindowViewModel._configDataStore.AllWinches.Remove(MainWindowViewModel._configDataStore.CurrentWinch);
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
