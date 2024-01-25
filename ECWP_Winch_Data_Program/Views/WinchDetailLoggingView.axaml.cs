namespace Views
{
    public partial class WinchDetailLoggingView : UserControl
    {
        public WinchDetailLoggingView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
