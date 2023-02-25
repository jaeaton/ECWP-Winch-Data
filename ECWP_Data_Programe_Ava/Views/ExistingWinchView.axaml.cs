namespace Views
{
    public partial class ExistingWinchView : UserControl
    {
        public ExistingWinchView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
