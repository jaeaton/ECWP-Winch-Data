﻿namespace ViewModels
{
    public class ValidateIPViewModel
    {
        public static bool ValidateIPFunction(string ipAddress)
        {
            bool output = true;

            //Write validation of network parameters for transmit and recieve
            //Check for valid IP Address
            if (ipAddress == null)
            {
                output = false;
                return output;
            }
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
                    if (validOctet == false || octetValue < 0 || octetValue > 255)
                    {
                        output = false;
                    }
                }
            }

            return output;
        }

        public static bool ValidatePortFunction(string portNum)
        {
            bool output = true;
            //Check for valid port number
            //int port = 0;
            bool validPort = int.TryParse(portNum, out _);
            if (!validPort)
            {
                output = false;
            }

            return output;
        }
    }
}