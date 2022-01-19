namespace Winch_Data
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Set the windows data context for data binding
            this.DataContext = UserControl1._configDataStore;
        }
    }
}
