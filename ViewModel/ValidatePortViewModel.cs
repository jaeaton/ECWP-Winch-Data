using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ValidatePortViewModel
    {
        public bool ValidatePortFunction(string portNum)
        {
            bool output = true;
            //Check for valid port number
            int port = 0;
            bool validPort = int.TryParse(portNum, out port);
            if (validPort == false)
            {
                output = false;
            }

            return output;
        }
    }
}
