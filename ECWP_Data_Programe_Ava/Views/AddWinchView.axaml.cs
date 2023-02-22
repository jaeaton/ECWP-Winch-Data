namespace Views
{
    public partial class AddWinchView : UserControl
    {
        public AddWinchView()
        {
            InitializeComponent();
        }

        private void WinchSelected(object sender, SelectionChangedEventArgs e)
        {
            WinchConfigurationViewModel.LoadWinch(MainWindowViewModel._configDataStore.WinchSelection);
        }

    }
}
