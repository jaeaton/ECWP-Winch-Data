namespace Models
{
    public class WinchModel
    {
        public string? WinchName { get; set; }
        public string? FileExtension { get; set; }
        public CommunicationModel? InputCommunication { get; set; }
        public string? CommunicationType { get; set; }
        public bool? UseComputerTime { get; set; }
        public bool? Log20Hz { get; set; }
        public bool? LogMax { get; set; }
        public bool? LogFormat { get; set; }
        public string? SpeedUnit { get; set; }
        public string? PayoutUnit { get; set; }
        public string? TensionUnit { get; set; }
        public double? StopLogTension { get; set; }
        public double? StopLogPayout { get; set; }
        public WinchModel() { }
        public WinchModel(string winchName, string fileExtension)
        {
            
            WinchName = winchName;
            FileExtension = fileExtension;
        }
    }
}
