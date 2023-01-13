namespace Store
{
    //Application configuration data for User Inputs View
    public partial class ConfigDataStore : ObservableObject
    {
        [ObservableProperty]
        private string? ipAddressInputSourceBox;
        
        [ObservableProperty]
        private string? portInputSourceBox;
        
        [ObservableProperty]
        private string? ipAddressInputDestinationBox;
        
        [ObservableProperty]
        private string? portInputDestinationBox;

        [ObservableProperty]
        private string? cruiseNameBox;

        [ObservableProperty]
        private string? castNumberBox;

        [ObservableProperty]
        private bool logMaxDataCheckBox;

        [ObservableProperty]
        private bool log20HzDataCheckBox;

        [ObservableProperty]
        private bool sendDataCheckBox;

        [ObservableProperty]
        private bool sendSerialDataCheckBox;

        [ObservableProperty]
        private bool useComputerTimeCheckBox;

        [ObservableProperty]
        private bool unolsUDPStringButton;
       
        [ObservableProperty]
        private bool mtnwUDPStringButton;

        [ObservableProperty]
        private string? directoryLabel;
        
        [ObservableProperty]
        private bool unolsWireLogButton;
        
        [ObservableProperty]
        private bool mtnwWireLogButton;

        [ObservableProperty]
        private string? startStopButtonText;

        [ObservableProperty]
        private bool userInputsEnable;
        
        [ObservableProperty]
        private string? serialPortName;
        
        [ObservableProperty]
        private string? baudRate;

        [ObservableProperty]
        private bool unolsSerialStringButton;
        
        [ObservableProperty]
        private bool mtnwSerialStringButton;

        [ObservableProperty]
        private List<string>? availableSerialPorts;
        
    }
}
