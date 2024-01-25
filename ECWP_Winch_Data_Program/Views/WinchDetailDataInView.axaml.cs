namespace Views
{
    public partial class WinchDetailDataInView : UserControl
    {
        public WinchDetailDataInView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
