using Store;

namespace Models
{
    public partial class WinchModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CommunicationModel> allOutputCommunication = new();

        [ObservableProperty]
        private string assignedBreakingLoad = string.Empty;

        [ObservableProperty]
        private string atlantis3PSWinchID = string.Empty;

        [ObservableProperty]
        private bool autoLog;

        [ObservableProperty]
        private float availableLength = default;

        [ObservableProperty]
        private string baudRateOutput = string.Empty;

        [ObservableProperty]
        private CancellationTokenSource canceller = new();

        [ObservableProperty]
        private string castNumber = string.Empty;

        [ObservableProperty]
        private ChartDataViewModel chartData = new();

        [ObservableProperty]
        private string chartTimeSpan = string.Empty;

        [ObservableProperty]
        private string communicationType = string.Empty;

        [ObservableProperty]
        private bool convertPayout = false;

        [ObservableProperty]
        private bool convertSpeed = false;

        [ObservableProperty]
        private bool convertTension = false;

        [ObservableProperty]
        private double factorOfSafety = default;

        [ObservableProperty]
        private string fileExtension = string.Empty;

        [ObservableProperty]
        private string hawboldtModel = string.Empty;

        [ObservableProperty]
        private string inputCommTypeSelection = string.Empty;

        [ObservableProperty]
        private CommunicationModel inputCommunication = new();
        [ObservableProperty]
        private float installedLength;

        [ObservableProperty]
        private LiveDataDataStore liveData = new();

        [ObservableProperty]
        private bool log20Hz;

        [ObservableProperty]
        private string logFormat = string.Empty;

        [ObservableProperty]
        private bool logMax;

        [ObservableProperty]
        private MaxDataPointModel maxData = new();

        [ObservableProperty]
        private string maxWireLogName = string.Empty;

        [ObservableProperty]
        private string minimumPayout = string.Empty;

        [ObservableProperty]
        private string minimumTension = string.Empty;

        [ObservableProperty]
        private string mtnwWireLogName = string.Empty;

        [ObservableProperty]
        private TabItemModel outputCommsSelected = new();

        [ObservableProperty]
        private CommunicationModel outputCommunication = new();

        [ObservableProperty]
        private string payoutConversionUnit = string.Empty;

        [ObservableProperty]
        private string payoutUnit = string.Empty;

        [ObservableProperty]
        private bool plotSelected;

        [ObservableProperty]
        private bool protocolHawboldt;

        [ObservableProperty]
        private string rawLogDirectory = string.Empty;

        [ObservableProperty]
        private string serialFormat = string.Empty;

        [ObservableProperty]
        private bool serialFormatMtnw;

        [ObservableProperty]
        private bool serialFormatUnols;

        [ObservableProperty]
        private bool serialOutput;

        [ObservableProperty]
        private string serialPortOutput = string.Empty;

        [ObservableProperty]
        private Bitmap? sheaveTrainImage;

        [ObservableProperty]
        private string sheaveTrainPath = string.Empty;

        [ObservableProperty]
        private bool showRawInput = false;

        [ObservableProperty]
        private string speedConversionUnit = string.Empty;

        [ObservableProperty]
        private string speedUnit = string.Empty;

        [ObservableProperty]
        private string startStopButtonText = "Start Log";

        [ObservableProperty]
        private string stopLogPayout = string.Empty;

        [ObservableProperty]
        private string stopLogTension = string.Empty;

        [ObservableProperty]
        private ObservableCollection<TabItemModel> tabItemsOutputComms = new()
        {
            new TabItemModel("Add New", "Add New")
        };

        [ObservableProperty]
        private string tensionAlarmLevel = string.Empty;

        [ObservableProperty]
        private string tensionConversionUnit = string.Empty;

        [ObservableProperty]
        private string tensionMemberManufacturer = string.Empty;

        [ObservableProperty]
        private string tensionMemberName = string.Empty;

        [ObservableProperty]
        private string tensionMemberNSFID = string.Empty;

        [ObservableProperty]
        private string tensionMemberPartNumber = string.Empty;

        [ObservableProperty]
        private string tensionUnit = string.Empty;

        [ObservableProperty]
        private string tensionWarningLevel = string.Empty;

        [ObservableProperty]
        private bool threePeeEssSelected = false;

        [ObservableProperty]
        private string udpFormat = string.Empty;

        [ObservableProperty]
        private bool udpFormatMtnw;

        [ObservableProperty]
        private bool udpFormatUnols;

        [ObservableProperty]
        private bool udpOutput;

        [ObservableProperty]
        private string unolsWireLogName = string.Empty;

        [ObservableProperty]
        private bool useComputerTime;

        [ObservableProperty]
        private string winchDirectory = string.Empty;

        [ObservableProperty]
        private string winchLogName = string.Empty;

        [ObservableProperty]
        private string winchLogType = string.Empty;

        [ObservableProperty]
        private string winchManufacturer = string.Empty;

        [ObservableProperty]
        private string winchModelName = string.Empty;

        [ObservableProperty]
        private string winchName = string.Empty;

        [ObservableProperty]
        private string winchSerialNumber = string.Empty;

        [ObservableProperty]
        private WireLogModel wireLog = new();

        [ObservableProperty]
        private string wirePoolWireLogName = string.Empty;

        [ObservableProperty]
        private string towYoTime = string.Empty;

        [ObservableProperty]
        private bool towYoTimeEnable = false;

        public WinchModel()
        { }

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

        public object Sync { get; } = new object();

        public WinchModel DeepCopy()
        {
            WinchModel copy = (WinchModel)this.MemberwiseClone();
            copy.InputCommunication = this.InputCommunication.ShallowCopy();//new CommunicationModel(InputCommunication.TcpIpAddress, InputCommunication.PortNumber);
            //copy.OutputCommunication = new CommunicationModel(OutputCommunication.TcpIpAddress, OutputCommunication.PortNumber);
            lock (Sync)
            {
                foreach (CommunicationModel com in AllOutputCommunication.ToList())//.ToList())
                {
                    copy.AllOutputCommunication.Add(new CommunicationModel(com.TcpIpAddress, com.PortNumber, com.CommunicationType, com.SerialPort, com.BaudRate, com.DataBits, com.Parity, com.StopBits, com.DataProtocol));
                }
            }

            copy.LiveData = new LiveDataDataStore(LiveData.Tension, LiveData.MaxTension, LiveData.Speed, LiveData.MaxSpeed, LiveData.Payout, LiveData.MaxPayout, LiveData.RawWireData, LiveData.RawWinchData, LiveData.TensionColor);
            copy.MaxData = new MaxDataPointModel(MaxData.MaxPayout, MaxData.MaxTension, MaxData.MaxSpeed);
            copy.ChartData = new ChartDataViewModel(ChartData._observableValues, ChartData.Series, ChartData.Sections, ChartData._observableValuesZero, ChartData._observableValuesMax, ChartData.XAxes, ChartData.YAxes);
            return copy;
        }

        public void LoadComms(string comm)
        {
            if (comm != string.Empty && AllOutputCommunication != null)
            {
                int index = -1;
                if (comm == "Add New")
                {
                    OutputCommunication = new();
                }
                else
                {
                    for (int i = 0; i < AllOutputCommunication.Count; i++)
                    {
                        CommunicationModel item = AllOutputCommunication[i];
                        if (item.DestinationName == comm)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        //Deep copy to break link between class objects
                        OutputCommunication = AllOutputCommunication[index].ShallowCopy();
                    }
                }
            }
        }

        public WinchModel ShallowCopy()
        {
            return (WinchModel)this.MemberwiseClone();
        }

        partial void OnAssignedBreakingLoadChanged(string value)
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

        partial void OnConvertPayoutChanged(bool value)
        {
            if (!value) { PayoutConversionUnit = PayoutUnit; }
        }

        partial void OnConvertSpeedChanged(bool value)
        {
            if (!value) { SpeedConversionUnit = SpeedUnit; }
        }

        partial void OnConvertTensionChanged(bool value)
        {
            if (!value) { TensionConversionUnit = TensionUnit; }
        }

        partial void OnFactorOfSafetyChanged(double oldValue, double newValue)
        {
            if (double.TryParse(AssignedBreakingLoad, out double abl))
            {
                TensionAlarmLevel = (Convert.ToInt16(abl / newValue)).ToString();

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

        partial void OnInstalledLengthChanged(float value)
        {
            AvailableLength = value;
        }

        partial void OnOutputCommsSelectedChanged(TabItemModel value)
        {
            if (value != null)
            {
                LoadComms(value.Header);
            }
        }
        partial void OnPayoutUnitChanged(string value)
        {
            PayoutConversionUnit = value;
        }

        partial void OnPlotSelectedChanged(bool value)
        {
            LiveDataViewModel mldvm = new LiveDataViewModel();
            mldvm.PlotSelectionChanged(value, WinchName);
        }
        partial void OnSerialFormatMtnwChanged(bool value)
        {
            //WinchViewModel vm = new WinchViewModel();
            //vm.ChangeSerialFormat(value);
            if (value == true)
            {
                SerialFormat = $"MTNW";
            }
            else
            {
                SerialFormat = $"UNOLS";
            }
        }

        partial void OnSheaveTrainPathChanged(string value)
        {
            if (value == "none")
            {
                SheaveTrainImage = null;
            }
            else
                SheaveTrainImage = new Avalonia.Media.Imaging.Bitmap(value);
        }

        partial void OnSpeedUnitChanged(string value)
        {
            SpeedConversionUnit = value;
        }
        partial void OnTensionAlarmLevelChanged(string value)
        {
            ChartData.Sections[0].Yj = Convert.ToDouble(value);
            ChartData.Sections[1].Yi = Convert.ToDouble(value);
        }

        partial void OnTensionUnitChanged(string value)
        {
            TensionConversionUnit = value;
        }
        partial void OnTensionWarningLevelChanged(string value)
        {
            ChartData.Sections[0].Yi = Convert.ToDouble(value);
        }

        partial void OnUdpFormatMtnwChanged(bool value)
        {
            //WinchViewModel vm = new WinchViewModel();
            //vm.ChangeUDPFormat(value);
            if (value == true)
            {
                UdpFormat = $"MTNW";
            }
            else
            {
                UdpFormat = $"UNOLS";
            }
        }
        partial void OnWinchLogTypeChanged(string value)
        {
            if (value == "Atlantis 3PS")
            {
                ThreePeeEssSelected = true;
            }
            else
            {
                ThreePeeEssSelected = false;
            }
        }
    }
}