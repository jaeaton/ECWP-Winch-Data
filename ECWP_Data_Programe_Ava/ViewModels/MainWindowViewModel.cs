namespace ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
      public MainWindowViewModel() 
        {
            
            View = new PlottingViewModel();
        }
        public PlottingViewModel View { get; }
    }

}