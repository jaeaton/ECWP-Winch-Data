namespace ViewModels
{
    internal class GetDataViewModel
    {
        private string ReadSerialData(SerialPort serial, WinchModel winch)
        {
            string responseData = serial.ReadLine();
            return responseData;
        }

        private string ReadTCPData(TcpClient client, WinchModel winch)//object tcpCom)
        {
            Byte[] data; //= System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            data = new Byte[256];

            // String to store the response ASCII representation.
            string responseData;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            //Uncomment to show all data in Wire Data Field
            //winch.LiveData.RawWireData = responseData;

            return responseData;
        }
    }
}
