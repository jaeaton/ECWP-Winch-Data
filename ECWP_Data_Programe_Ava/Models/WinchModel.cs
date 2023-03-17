namespace Models
{
    public partial class WinchModel : ObservableObject
    {
        
        [ObservableProperty]
        private CommunicationModel? inputCommunication = new();

        [ObservableProperty]
        private CommunicationModel? outputCommunication = new();
        
        [ObservableProperty]
        private LiveDataDataStore liveData = new();

        [ObservableProperty]
        private MaxDataPointModel maxData = new();

        [ObservableProperty]
        private CancellationTokenSource? canceller;

        [ObservableProperty]
        private ChartDataViewModel chartData = new();

        [ObservableProperty]
        private string? winchName;
        [ObservableProperty]
        private string? castNumber;
        [ObservableProperty]
        private string? fileExtension;
        [ObservableProperty]
        private bool plotSelected;
        partial void OnPlotSelectedChanged(bool value)
        {
            MainLiveDataViewModel mldvm = new MainLiveDataViewModel();
            mldvm.PlotSelectionChanged(value, WinchName);
        }
        [ObservableProperty]
        private string? communicationType ;
        [ObservableProperty]
        private bool useComputerTime ;
        [ObservableProperty]
        private bool log20Hz ;
        [ObservableProperty]
        private bool logMax ;
        [ObservableProperty]
        private bool logFormatUnols ;
        [ObservableProperty]
        private bool logFormatMtnw;
        partial void OnLogFormatMtnwChanged(bool value)
        {
            WinchConfigurationViewModel vm = new WinchConfigurationViewModel();
            vm.ChangeLogFormat(value);
        }
        [ObservableProperty]
        private string? logFormat;
        
        [ObservableProperty]
        private string? speedUnit ;
        [ObservableProperty]
        private string? payoutUnit ;
        [ObservableProperty]
        private string? tensionUnit ;
        [ObservableProperty]
        private string? stopLogTension ;
        [ObservableProperty]
        private string? stopLogPayout ;
        [ObservableProperty]
        private bool udpOutput;
        [ObservableProperty]
        private bool udpFormatUnols;
        [ObservableProperty]
        private bool udpFormatMtnw;
        partial void OnUdpFormatMtnwChanged(bool value)
        {
            WinchConfigurationViewModel vm = new WinchConfigurationViewModel();
            vm.ChangeUDPFormat(value);
        }
        [ObservableProperty]
        private string? udpFormat;
        [ObservableProperty]
        private bool serialOutput;
        [ObservableProperty]
        private string? serialPortOutput;
        [ObservableProperty]
        private string? baudRateOutput;
        [ObservableProperty]
        private bool serialFormatUnols;
        [ObservableProperty]
        private bool serialFormatMtnw;
        partial void OnSerialFormatMtnwChanged(bool value)
        {
            WinchConfigurationViewModel vm = new WinchConfigurationViewModel();
            vm.ChangeSerialFormat(value);
        }
        [ObservableProperty]
        private string? serialFormat;
        [ObservableProperty]
        private string? mtnwWireLogName;
        [ObservableProperty]
        private string? unolsWireLogName;
        [ObservableProperty]
        private string? maxWireLogName;
        [ObservableProperty]
        private string? winchLogName;
        [ObservableProperty]
        private string? startStopButtonText;
        [ObservableProperty]
        private string? tensionWarningLevel;
        partial void OnTensionWarningLevelChanged(string? value)
        {
            ChartData.Sections[0].Yi = Convert.ToDouble(value);
        }
        [ObservableProperty]
        private string? tensionAlarmLevel;
        partial void OnTensionAlarmLevelChanged(string? value)
        {
            ChartData.Sections[0].Yj = Convert.ToDouble(value);
            ChartData.Sections[1].Yi = Convert.ToDouble(value);
        }
        [ObservableProperty]
        private string? assignedBreakingLoad;
        partial void OnAssignedBreakingLoadChanged(string? value)
        {
            ChartData.Sections[1].Yj = Convert.ToDouble(value);
        }
        [ObservableProperty]
        private bool autoLog;
        
        public WinchModel() { }
        public WinchModel(string winchName, string fileExtension)
        {
            
            WinchName = winchName;
            FileExtension = fileExtension;
        }
        public WinchModel(string? _winchName, string? _fileExtension, string? _communicationType, bool _useComputerTime, bool _log20Hz, bool _logMax, string? _speedUnit, string? _payoutUnit, string? _tensionUnit, string? _stopLogTension, string? _stopLogPayout) 
          
        {
            
            WinchName = _winchName;
            FileExtension = _fileExtension;
            //InputCommunication = _inputCommunication;
            //TcpIpAddress = _tcpIpAddress;
            //TcpIpPort = _tcpIpPort;
            CommunicationType = _communicationType;
            UseComputerTime = _useComputerTime;
            Log20Hz = _log20Hz;
            LogMax = _logMax;
            SpeedUnit = _speedUnit;
            PayoutUnit = _payoutUnit;
            TensionUnit = _tensionUnit;
            StopLogTension = _stopLogTension;
            //StopLogPayout = _stopLogPayout;
        }

        public WinchModel ShallowCopy()
        {
            return (WinchModel) this.MemberwiseClone();
        }

        public WinchModel DeepCopy()
        {
            WinchModel copy = (WinchModel)this.MemberwiseClone();
            copy.InputCommunication = new CommunicationModel(InputCommunication.TcpIpAddress, InputCommunication.PortNumber);
            copy.OutputCommunication = new CommunicationModel(OutputCommunication.TcpIpAddress, OutputCommunication.PortNumber);
            copy.LiveData = new LiveDataDataStore(LiveData.Tension, LiveData.MaxTension, LiveData.Speed, LiveData.MaxSpeed, LiveData.Payout, LiveData.MaxPayout, LiveData.RawWireData, LiveData.RawWinchData);
            copy.MaxData = new MaxDataPointModel(MaxData.MaxPayout, MaxData.MaxTension, MaxData.MaxSpeed);
            copy.ChartData = new ChartDataViewModel(ChartData._observableValues, ChartData.Series, ChartData.Sections, ChartData._observableValuesZero, ChartData._observableValuesMax, ChartData.XAxes,ChartData.YAxes);
            return copy;
        }

    }
}
