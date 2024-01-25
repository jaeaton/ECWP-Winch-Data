using Avalonia.Controls;

namespace Views
{
    public partial class ProcessDataView : UserControl
    {
        public ProcessDataView()
        {
            InitializeComponent();
            //this.DataContext = new ProcessDataViewModel();
            DataContext = ViewModels.ProcessDataViewModel.ParseData;
        }
    }
}
