namespace Views
{
    public partial class ProcessDataDataGridView : UserControl
    {
        public ProcessDataDataGridView()
        {
            InitializeComponent();
            DataContext = ViewModels.ProcessDataViewModel.ParseData;
        }
    }
}
