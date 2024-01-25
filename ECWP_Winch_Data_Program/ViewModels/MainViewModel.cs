namespace ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public static ConfigDataStore _configDataStore = new();
    public string Greeting => "Welcome to Avalonia!";
}
