namespace ViewModels
{
    internal class SendDataViewModel
    {
        private void Send20HzData(DataPointModel data, WinchModel winch, UdpClient client)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (winch.UdpFormatUnols)
            {
                line = $"$WIR,{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms},{data.CheckSum}";
            }
            //MTNW 1 Format
            else
            {
                line = $"RD,{data.Date}T{data.Time},{data.Tension},{data.Speed},{data.Payout},";
            }
            //MTNW Legacy
            //else
            //{
            //    line = $"RD,{data.Tension},{data.Speed},{data.Payout},";
            //}

            //Add checksum
            int checkSum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
            Array.ForEach(asciiBytes, delegate (byte i) { checkSum += i; });
            line = $"{line}{checkSum}" + Environment.NewLine;
            //Send UDP packet
            byte[] sendBytes = Encoding.ASCII.GetBytes(line);

            client.Send(sendBytes, sendBytes.Length);
        }

        private void SendSerialData(DataPointModel data, WinchModel winch, SerialPort _serialPort)
        {
            //Format string based on format selection
            string line;
            //If UNOLS Format
            if (winch.SerialFormatUnols)
            {
                line = $"WIR,{data.Date},{data.Time},{data.Tension},{data.Speed},{data.Payout},{data.TMWarnings},{data.TMAlarms}, {data.CheckSum}";
            }
            //Not UNOLS Format (MTNW 1)
            else
            {
                line = $"RD,{data.Date}T{data.Time},{data.Tension},{data.Speed},{data.Payout},";
            }
            //Add checksum
            int checkSum = 0;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(line);
            Array.ForEach(asciiBytes, delegate (byte i) { checkSum += i; });
            line = $"{line}{checkSum}";

            //Serial Port Transmit
            _serialPort.WriteLine(line);
        }
    }
}
