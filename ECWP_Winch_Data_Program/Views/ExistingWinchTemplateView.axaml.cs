namespace Views
{
    public partial class ExistingWinchTemplateView : UserControl
    {
        public ExistingWinchTemplateView()
        {
            InitializeComponent();
            DataContext = MainViewModel._configDataStore;
        }
    }
}
