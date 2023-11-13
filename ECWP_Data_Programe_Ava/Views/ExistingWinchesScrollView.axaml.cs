namespace Views
{
    public partial class ExistingWinchesScrollView : UserControl
    {
        public ExistingWinchesScrollView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
