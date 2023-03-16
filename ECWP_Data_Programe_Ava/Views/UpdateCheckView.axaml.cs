using System.Reflection;

namespace Views
{
    public partial class UpdateCheckView : UserControl
    {
        public UpdateCheckView()
        {
            UpdateCheckViewModel viewModel = new UpdateCheckViewModel();
            InitializeComponent();
            DataContext = viewModel;
            //UpdateCheckViewModel viewModel = new UpdateCheckViewModel();
            //Gets version from project file (right click project and select edit project file)
            viewModel.RunningVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }
    }
}
