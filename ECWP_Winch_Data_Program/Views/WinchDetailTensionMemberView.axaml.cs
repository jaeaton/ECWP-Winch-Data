namespace Views
{
    public partial class WinchDetailTensionMemberView : UserControl
    {
        public WinchDetailTensionMemberView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
