namespace ViewModels
{
    public partial class WinchConfigurationViewModel : ObservableObject
    {
        ConfigDataStore _configDataStore = MainWindowViewModel._configDataStore;

        [RelayCommand]
        private async void AddWinch()
        {
            _configDataStore.AllWinches.Add(_configDataStore.CurrentWinch);
        }
        [RelayCommand]
        private async void RemoveWinch()
        {
            _configDataStore.AllWinches.Remove(_configDataStore.CurrentWinch);
        }

        
    }
}
