namespace Views
{
    public partial class PlotsConfigurationView : UserControl
    {
        public PlotsConfigurationView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
