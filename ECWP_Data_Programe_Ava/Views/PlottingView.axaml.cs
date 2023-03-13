namespace Views
{
    public partial class PlottingView : UserControl
    {
        public PlottingView()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
