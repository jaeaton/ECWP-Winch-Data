namespace Views
{
    /// <summary>
    /// Interaction logic for DataDisplay.xaml
    /// </summary>
    public partial class DataDisplay : UserControl
    {
        public DataDisplay()
        {
            InitializeComponent();
            //Set the data context for binding variables
            this.DataContext = DataHandlingViewModel._liveData;
        }
    }
}
