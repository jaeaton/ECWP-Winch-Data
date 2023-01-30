namespace Views
{
    public partial class ProcessingView : UserControl
    {
        public ProcessingView()
        {

            //this.DataContext = new ParseDataStore();
            this.DataContext = new ViewModels.ProcessingViewModel();
            InitializeComponent();
        }

        
    }
}
