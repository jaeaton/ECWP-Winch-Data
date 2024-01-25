namespace ECWP_Winch_Data.Models
{
    internal class HawboldtDataModel
    {
        public string hDate { get; set; } = string.Empty;
        public string hMission { get; set; } = string.Empty;
        public string hSpoolingConf { get; set; } = string.Empty;
        public string hCableSerialNum { get; set; } = string.Empty;
        public string hCableDate { get; set; } = string.Empty;
        public float tension { get; set; }
        public float payout { get; set; }
        public float speed { get; set; }
        public float maxTension { get; set; }
        public float maxPayout { get; set; }
        public float minSpeed { get; set; }
        public float maxSpeed { get; set; }
        public float tensionLimit { get; set; }
        public float minHoldTension { get; set; }
        public float payoutTarget { get; set; }
        public float speedTarget { get; set; }
        public float tensionWarningLevel { get; set; }
        public float payoutWarningLevel { get; set; }
        public float speedWarningLevel { get; set; }
        public float tensionAlarmLevel { get; set; }
        public float payoutAlarmLevel { get; set; }
        public float speedAlarmLevel { get; set; }
        public int? tensionUnit { get; set; }
        public int? payoutUnit { get; set; }
        public int? speedUnit { get; set; }
        public string warningFlags { get; set; } = string.Empty;
        public string alarmFlags { get; set; } = string.Empty;
        public string winchStatus { get; set; } = string.Empty;
        public string controlMode { get; set; } = string.Empty;
        public int? currentLayer { get; set; }
        public float currentWrap { get; set; }
        public float joystickPercent { get; set; }
        public float winchMotorRPM { get; set; }
        public float winchMotorTorque { get; set; }
        public float winchMotorCurrent { get; set; }
        public string winchMotorVFDWarning { get; set; } = string.Empty;
        public string winchMotorVFDFault { get; set; } = string.Empty;
        public float lwMotorRPM { get; set; }
        public float lwMotorTorque { get; set; }
        public float lwMotorCurrent { get; set; }
        public string lwMotorVFDWarning { get; set; } = string.Empty;
        public string lwMotorVFDFault { get; set; } = string.Empty; 
        public float enclosureTemp { get; set; }
        public float runtimeHours { get; set; }
        public float runtimeSinceMaintainence { get; set; }
        public float ahcPayout { get; set; }
        public float ahcDepth { get; set; }
        public float ahcTargetDepth { get; set; }
        public float ahcSheaveHeave { get; set; }
        public float ahcCorrection { get; set; }


    }
}
