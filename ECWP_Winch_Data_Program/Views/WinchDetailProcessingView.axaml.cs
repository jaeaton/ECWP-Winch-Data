namespace Views
{
    public partial class WinchDetailProcessingView : UserControl
    {
        public WinchDetailProcessingView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}