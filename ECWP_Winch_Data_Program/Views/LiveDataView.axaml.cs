using Avalonia.Controls;

namespace Views;

    public partial class LiveDataView : UserControl
    {
        public LiveDataView()
        {
            InitializeComponent();
        this.DataContext = MainViewModel._configDataStore;
    }
    }

