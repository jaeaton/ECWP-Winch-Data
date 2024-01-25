using Avalonia.Controls;

namespace Views
{
    public partial class VersionCheckView : UserControl
    {
        public VersionCheckView()
        {
            VersionCheckViewModel viewModel = new VersionCheckViewModel();
            InitializeComponent();
            DataContext = viewModel;
            //UpdateCheckViewModel viewModel = new UpdateCheckViewModel();
            //Gets version from project file (right click project and select edit project file)
            viewModel.RunningVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        }
    }
}
