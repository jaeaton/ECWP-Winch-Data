namespace Models
{
    public partial class WinchModel : ObservableObject
    {
        [ObservableProperty]
        public string? winchName;
        [ObservableProperty]
        public string? fileExtension ;
        [ObservableProperty]
        public CommunicationModel? inputCommunication ;
        [ObservableProperty]
        public string? communicationType ;
        [ObservableProperty]
        public bool? useComputerTime ;
        [ObservableProperty]
        public bool? log20Hz ;
        [ObservableProperty]
        public bool? logMax ;
        [ObservableProperty]
        public bool? logFormat ;
        [ObservableProperty]
        public string? speedUnit ;
        [ObservableProperty]
        public string? payoutUnit ;
        [ObservableProperty]
        public string? tensionUnit ;
        [ObservableProperty]
        public double? stopLogTension ;
        [ObservableProperty]
        public double? stopLogPayout ;
        public WinchModel() { }
        public WinchModel(string winchName, string fileExtension)
        {
            
            WinchName = winchName;
            FileExtension = fileExtension;
        }
    }
}
