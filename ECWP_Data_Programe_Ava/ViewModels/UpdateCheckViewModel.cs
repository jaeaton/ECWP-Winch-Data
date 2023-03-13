using Mayerch1.GithubUpdateCheck;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ViewModels
{
    internal partial class UpdateCheckViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? runningVersion;
        [ObservableProperty]
        private string? gitVersion;
        [ObservableProperty]
        private bool isAsyncUpdate;
        string url = "https://github.com/jaeaton/ECWP-Winch-Data/releases";
        [ObservableProperty]
        Uri? uri = new Uri("https://github.com/jaeaton/ECWP-Winch-Data/releases");
        
        [RelayCommand]
        public async void CheckForUpdate()
        {
            GithubUpdateCheck update = new GithubUpdateCheck("jaeaton", "ECWP-Winch-Data");
            //bool isUpdate = update.IsUpdateAvailable("1.0.0", VersionChange.Minor);
            IsAsyncUpdate = await update.IsUpdateAvailableAsync(RunningVersion, VersionChange.Revision);
            if (IsAsyncUpdate)
            {
                GitVersion = await update.VersionAsync();
            }
            

        }

        [RelayCommand]
        private void OpenUrl()
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
