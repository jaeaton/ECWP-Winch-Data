using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GlobalConfigModel
    {
        public CruiseModel CruiseInformation { get; set; } = new CruiseModel();
        public CommunicationModel ReceiveCommunication { get; set; } = new CommunicationModel();
        public CommunicationModel TransmitCommunication { get; set; } = new CommunicationModel();
        public string FullLogFileName { get; set; }
        public string MaxLogFileName { get; set; }
        public bool UDPSwitch { get; set; } = new bool();
        public bool Log20HzSwitch { get; set; } = new bool();
        public bool LogMaxValuesSwitch { get; set; } = new bool();
        public bool UseComputerTimeSwitch { get; set; } = new bool();
        public string SaveDirectory { get; set; }
        public bool SaveDirectorySet { get; set; } = new bool();
        public bool UNOLSLogFormatSet { get; set; } = new bool();

    }
}
