using Avalonia.Controls;

namespace Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = MainViewModel._configDataStore;
        Instance = this;
        FileOperationsViewModel.ReadConfig(MainViewModel._configDataStore);
    }

    public static MainWindow? Instance { get; private set; }
}
