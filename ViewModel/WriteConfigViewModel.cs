using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class WriteConfigViewModel
    {
        public void WriteConfigFunction(Model.GlobalConfigModel globalConfig)
        {
            //Logic to save new parameters to config file
            string fileName = "config.txt";
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            GetConfigFunction();
            string[] lines =
                {
                $"Receive IP,{ globalConfig.ReceiveCommunication.IPAddress }",
                $"Receive Port,{ globalConfig.ReceiveCommunication.PortNumber }",
                $"Transmit IP,{ globalConfig.TransmitCommunication.IPAddress }",
                $"Transmit Port,{ globalConfig.TransmitCommunication.PortNumber }",
                $"Cruise Name,{ globalConfig.CruiseInformation.CruiseName }",
                $"Cast Number,{ globalConfig.CruiseInformation.CastNumber }",
                $"Send UDP,{ globalConfig.UDPSwitch }",
                $"Save 20 Hz Data,{ globalConfig.Log20HzSwitch }",
                $"Log Max Values,{ globalConfig.LogMaxValuesSwitch }",
                $"Use Computer Time,{ globalConfig.UseComputerTimeSwitch }",
                $"Save Location,{ globalConfig.SaveDirectory }",
                $"UNOLS String, { globalConfig.UNOLSLogFormatSet }"
                };
            using (StreamWriter stream = new StreamWriter(destPath))
            {
                foreach (string line in lines)
                    stream.WriteLine(line);
            }
            //directoryLabel.Content = destPath;
        }
    }
}
