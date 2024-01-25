namespace Views
{
    public partial class WinchDetailDataOutView : UserControl
    {
        public WinchDetailDataOutView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
