namespace Views
{
    public partial class LiveDataPathView : UserControl
    {
        public LiveDataPathView()
        {
            InitializeComponent();
            this.DataContext = MainViewModel._configDataStore;
        }
    }
}
