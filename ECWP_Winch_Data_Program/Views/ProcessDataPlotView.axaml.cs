namespace Views
{
    public partial class ProcessDataPlotView : UserControl
    {
        public ProcessDataPlotView()
        {
            InitializeComponent();
            DataContext = ProcessDataViewModel.ParseData;
        }
    }
}
