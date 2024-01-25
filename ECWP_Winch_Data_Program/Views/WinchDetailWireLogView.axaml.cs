namespace Views
{
    public partial class WinchDetailWireLogView : UserControl
    {
        public WinchDetailWireLogView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
