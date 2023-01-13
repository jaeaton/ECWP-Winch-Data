namespace ViewModels
{
    class GetSerialPorts : ObservableCollection<string>
    {
        public static List<string> FindSerialPorts()
        {
            List<string> AvailablePorts = new();
            foreach(var port in SerialPort.GetPortNames())
            {
                AvailablePorts.Add(port);
            }
            return (AvailablePorts);
        }
    }
}
