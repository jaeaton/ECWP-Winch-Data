using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ValidateIPViewModel
    {
        public bool ValidateIPFunction(string ipAddress)
        {
            bool output = true;

            //Write validation of network parameters for transmit and recieve
            //Check for valid IP Address
            string[] octets = ipAddress.Split('.');
            if (octets.Length != 4)
            {
                output = false;
            }
            else
            {
                foreach (var octet in octets)
                {
                    int octetValue;
                    bool validOctet = int.TryParse(octet, out octetValue);
                    if (validOctet == false || octetValue < 1 || octetValue > 255)
                    {
                        output = false;
                    }
                }
            }
            return output;
        }
    }
}
