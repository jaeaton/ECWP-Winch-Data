namespace Views
{
    public partial class WinchDetailParticularsView : UserControl
    {
        public WinchDetailParticularsView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
