using System.Diagnostics;

namespace ViewModels
{
    internal class MainLiveDataViewModel
    {
        //move code from code behind to here
        public static void UpdatePlottingWinch(string winch)
        {
            if (winch != null && MainWindowViewModel._configDataStore.AllWinches != null)
            {
                //int i = MainWindowViewModel._configDataStore.AllWinches.FindIndex(a => a.WinchName == winch);
                int index = -1;

                for (int i = 0; i < MainWindowViewModel._configDataStore.AllWinches.Count; i++)
                {
                    WinchModel item = MainWindowViewModel._configDataStore.AllWinches[i];
                    if (item.WinchName == winch)
                    {
                        index = i;
                        break;
                    }
                }
                WinchModel PlottingWinch = MainWindowViewModel._configDataStore.AllWinches[index];
                if (PlottingWinch != null)
                {
                    MainWindowViewModel._configDataStore.SelectedProtocol = PlottingWinch.CommunicationType;
                    MainWindowViewModel._configDataStore.IpAddressInputSourceBox = PlottingWinch.TcpIpAddress;
                    MainWindowViewModel._configDataStore.PortInputSourceBox = PlottingWinch.TcpIpPort;
                    MainWindowViewModel._configDataStore.Log20HzDataCheckBox = PlottingWinch.Log20Hz;
                    MainWindowViewModel._configDataStore.LogMaxDataCheckBox = PlottingWinch.LogMax;
                    MainWindowViewModel._configDataStore.UseComputerTimeCheckBox = PlottingWinch.UseComputerTime;
                }
            }
        }
    }
}
