using Avalonia.Controls;

namespace Views
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel._configDataStore;
        }
    }
}
