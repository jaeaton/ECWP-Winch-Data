namespace Views
{
    public partial class WinchDetailView : UserControl
    {
        public WinchDetailView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
