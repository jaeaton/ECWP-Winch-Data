namespace Views
{
    public partial class WinchConfigurationView : UserControl
    {
        public WinchConfigurationView()
        {
            this.DataContext = new ViewModels.WinchConfigurationViewModel();
            InitializeComponent();
        }
    }
}
