using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommunicationModel
    {
        /// <summary>
        /// IP Address of communication partner
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// Port number of communication partnet
        /// </summary>
        public int PortNumber { get; set; }
        public CommunicationModel()
        {

        }
        public CommunicationModel(string ipAdress, string portNumber)
        {
            IPAddress = ipAdress;

            int portNumberValue = 50505;
            int.TryParse(portNumber, out portNumberValue);
            PortNumber = portNumberValue;
        }
    }
}
