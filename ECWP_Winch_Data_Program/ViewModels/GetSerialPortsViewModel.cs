namespace ViewModels
{
    internal class GetSerialPorts : ObservableCollection<string>
    {
        public static List<string> FindSerialPorts()
        {
            List<string> AvailablePorts = new();
            //Search system for serial ports using System.IO.Ports
            foreach (var port in SerialPort.GetPortNames())
            {
                AvailablePorts.Add(port);
            }
            return (AvailablePorts);
        }
    }
}