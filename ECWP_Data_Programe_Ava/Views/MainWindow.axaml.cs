namespace Views
{
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = UserInputsView._configDataStore;
            Instance = this;
        }

        
    }
}
