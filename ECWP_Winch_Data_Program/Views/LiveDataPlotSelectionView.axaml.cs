using Avalonia.Controls;

namespace Views
{
    public partial class LiveDataPlotSelectionView : UserControl
    {
        public LiveDataPlotSelectionView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
