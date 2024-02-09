namespace Models
{
    public partial class WinchModel : ObservableObject
    {
        
        [ObservableProperty]
        private CommunicationModel inputCommunication = new();

        [ObservableProperty]
        private CommunicationModel outputCommunication = new();
        
        [ObservableProperty]
        private LiveDataDataStore liveData = new();

        [ObservableProperty]
        private MaxDataPointModel maxData = new();

        [ObservableProperty]
        private CancellationTokenSource canceller = new();

        [ObservableProperty]
        private ChartDataViewModel chartData = new();

        [ObservableProperty]
        private WireLogModel wireLog = new();

        [ObservableProperty]
        private string winchName = string.Empty;
        [ObservableProperty]
        private string winchManufacturer = string.Empty;
        [ObservableProperty]
        private string winchModelName = string.Empty;
        [ObservableProperty]
        private string castNumber = string.Empty;
        [ObservableProperty]
        private string fileExtension = string.Empty;
        [ObservableProperty]
        private bool plotSelected;
        partial void OnPlotSelectedChanged(bool value)
        {
            LiveDataViewModel mldvm = new LiveDataViewModel();
            mldvm.PlotSelectionChanged(value, WinchName);
        }
        [ObservableProperty]
        private string communicationType = string.Empty ;
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
            WinchViewModel vm = new WinchViewModel();
            vm.ChangeLogFormat(value);
        }
        [ObservableProperty]
        private string logFormat = string.Empty;
        
        [ObservableProperty]
        private string speedUnit = string.Empty;
        [ObservableProperty]
        private string payoutUnit = string.Empty;
        [ObservableProperty]
        private string tensionUnit = string.Empty;
        [ObservableProperty]
        private string stopLogTension = string.Empty;
        [ObservableProperty]
        private string stopLogPayout = string.Empty ;
        [ObservableProperty]
        private bool udpOutput;
        [ObservableProperty]
        private bool udpFormatUnols;
        [ObservableProperty]
        private bool udpFormatMtnw;
        partial void OnUdpFormatMtnwChanged(bool value)
        {
            WinchViewModel vm = new WinchViewModel();
            vm.ChangeUDPFormat(value);
        }
        [ObservableProperty]
        private string udpFormat = string.Empty;
        [ObservableProperty]
        private bool serialOutput;
        [ObservableProperty]
        private string serialPortOutput = string.Empty;
        [ObservableProperty]
        private string baudRateOutput = string.Empty;
        [ObservableProperty]
        private bool serialFormatUnols;
        [ObservableProperty]
        private bool serialFormatMtnw;
        partial void OnSerialFormatMtnwChanged(bool value)
        {
            WinchViewModel vm = new WinchViewModel();
            vm.ChangeSerialFormat(value);
        }
        [ObservableProperty]
        private string serialFormat = string.Empty;
        [ObservableProperty]
        private string mtnwWireLogName = string.Empty;
        [ObservableProperty]
        private string unolsWireLogName = string.Empty;
        [ObservableProperty]
        private string maxWireLogName = string.Empty;
        [ObservableProperty]
        private string winchLogName = string.Empty;
        [ObservableProperty]
        private string startStopButtonText = string.Empty;
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
            if (double.TryParse(AssignedBreakingLoad, out double abl))
            {
                ChartData.Sections[1].Yj = abl;

                if (FactorOfSafety != default)
                {
                    TensionAlarmLevel = (Convert.ToInt16(abl / FactorOfSafety)).ToString();

                    if (FactorOfSafety == 5)
                    {
                        TensionWarningLevel = (Convert.ToInt16(abl / 5.5)).ToString();
                    }
                    if (FactorOfSafety == 2.5)
                    {
                        TensionWarningLevel = (Convert.ToInt16(abl / 2.8)).ToString();
                    }
                    if (FactorOfSafety == 2.0)
                    {
                        TensionWarningLevel = (Convert.ToInt16(abl / 2.2)).ToString();
                    }
                    if (FactorOfSafety == 1.5)
                    {
                        TensionWarningLevel = (Convert.ToInt16(abl / 1.7)).ToString();
                    }
                }
            }
            
        }
        [ObservableProperty]
        private bool autoLog;
        [ObservableProperty]
        private bool protocolHawboldt;
        [ObservableProperty]
        private string hawboldtModel = string.Empty;
        [ObservableProperty]
        private string chartTimeSpan = string.Empty ;
        [ObservableProperty]
        private double factorOfSafety = default;
        partial void OnFactorOfSafetyChanged(double oldValue, double newValue)
        {
            if(double.TryParse(AssignedBreakingLoad, out double abl))
            {
                TensionAlarmLevel = (Convert.ToInt16(abl/newValue)).ToString();
                
                if(FactorOfSafety == 5)
                {
                    TensionWarningLevel = (Convert.ToInt16(abl / 5.5)).ToString();
                }
                if (FactorOfSafety == 2.5)
                {
                    TensionWarningLevel = (Convert.ToInt16(abl / 2.8)).ToString();
                }
                if (FactorOfSafety == 2.0)
                {
                    TensionWarningLevel = (Convert.ToInt16(abl / 2.2)).ToString();
                }
                if (FactorOfSafety == 1.5)
                {
                    TensionWarningLevel = (Convert.ToInt16(abl / 1.7)).ToString();
                }
            }
            
            
        }
        [ObservableProperty]
        private double installedLength = default;
        partial void OnInstalledLengthChanged(double value)
        {
            AvailableLength = value;
        }
        [ObservableProperty]
        private double availableLength = default;
        [ObservableProperty]
        private string tensionMemberName = string.Empty ;
        [ObservableProperty]
        private string tensionMemberManufacturer = string.Empty ;
        [ObservableProperty]
        private string tensionMemberPartNumber = string.Empty;
        [ObservableProperty]
        private string tensionMemberNSFID = string.Empty ;
        public WinchModel() { }
        public WinchModel(string winchName, string fileExtension)
        {
            
            WinchName = winchName;
            FileExtension = fileExtension;
        }
        public WinchModel(string _winchName, string _fileExtension, string _communicationType, bool _useComputerTime, bool _log20Hz, bool _logMax, string _speedUnit, string _payoutUnit, string _tensionUnit, string _stopLogTension, string _stopLogPayout) 
          
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
            copy.LiveData = new LiveDataDataStore(LiveData.Tension, LiveData.MaxTension, LiveData.Speed, LiveData.MaxSpeed, LiveData.Payout, LiveData.MaxPayout, LiveData.RawWireData, LiveData.RawWinchData, LiveData.TensionColor);
            copy.MaxData = new MaxDataPointModel(MaxData.MaxPayout, MaxData.MaxTension, MaxData.MaxSpeed);
            copy.ChartData = new ChartDataViewModel(ChartData._observableValues, ChartData.Series, ChartData.Sections, ChartData._observableValuesZero, ChartData._observableValuesMax, ChartData.XAxes,ChartData.YAxes);
            return copy;
        }

    }
}
