namespace Views
{
    public partial class PlottingView : UserControl
    {
        public PlottingView()
        {
            InitializeComponent();
            this.DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
