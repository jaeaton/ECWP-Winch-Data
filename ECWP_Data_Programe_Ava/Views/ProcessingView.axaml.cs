namespace Views
{
    public partial class ProcessingView : UserControl
    {
        public ProcessingView()
        {
            InitializeComponent();
            this.DataContext= MainProcessingViewModel.parseData;
        }
    }
}
