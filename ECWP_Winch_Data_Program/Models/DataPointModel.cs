namespace Models
{
    public class DataPointModel 
    {

        public string StringID  = string.Empty;
        /// <summary>
        /// Date value from data source
        /// </summary>
        public string Date   = string.Empty;
        /// <summary>
        /// Time from data source
        /// </summary>
        public string Time   = string.Empty;
        /// <summary>
        /// Combined Date and Time
        /// </summary>
        public DateTime DateAndTime   = new();
        /// <summary>
        /// Tension value
        /// </summary>
        public float Tension;
        /// <summary>
        /// Payout value
        /// </summary>
        public float Payout;
        /// <summary>
        /// Speed value
        /// </summary>
        public float Speed;
        /// <summary>
        /// Check Sum Value
        /// </summary>
        public string CheckSum   = string.Empty;
        /// <summary>
        /// Tension Member Warning (8 bits)
        /// </summary>
        public string TMWarnings   = string.Empty;
        /// <summary>
        /// Tension member alarms(8 bits)
        /// </summary>
        public string TMAlarms   = string.Empty;    
        public DataPointModel()
        {

        }
        /// <summary>
        /// Approx MTNW String overloads
        /// </summary>
        /// <param name="inID"></param>
        /// <param name="inDate"></param>
        /// <param name="inTime"></param>
        /// <param name="inTension"></param>
        /// <param name="inSpeed"></param>
        /// <param name="inPayout"></param>
        /// <param name="inCheckSum"></param>
        public DataPointModel(string inID, string inDate, string inTime, string inTension, string inSpeed, string inPayout, string inCheckSum)
        {
            StringID = inID;

            //DateTime.TryParseExact(inDate,yyyyMMdd, out DateTime YMD);
            Date = inDate;

            //DateTime.TryParse(inTime, out DateTime HMS);
            Time = inTime;

            float.TryParse(inTension, out float tension);
            Tension = tension;

            float.TryParse(inPayout, out float payout);
            Payout = payout;

            float.TryParse(inSpeed, out float speed);
            Speed = speed;

            CheckSum = inCheckSum;
            TMAlarms = "00000000";
            TMWarnings = "00000000";
        }
        /// <summary>
        /// UNOLS String, Overloads include Tension member warnings and alarms
        /// </summary>
        /// <param name="inID"></param>
        /// <param name="inDate"></param>
        /// <param name="inTime"></param>
        /// <param name="inTension"></param>
        /// <param name="inSpeed"></param>
        /// <param name="inPayout"></param>
        /// <param name="inCheckSum"></param>
        /// <param name="tMWarnings"></param>
        /// <param name="tMAlarms"></param>
        public DataPointModel(string inID, string inDate, string inTime, string inTension, string inSpeed, string inPayout, string tMWarnings, string tMAlarms, string inCheckSum)
        {
            StringID = inID;

            //DateTime.TryParseExact(inDate,yyyyMMdd, out DateTime YMD);
            Date = inDate;

            //DateTime.TryParse(inTime, out DateTime HMS);
            Time = inTime;

            float.TryParse(inTension, out float tension);
            Tension = tension;

            float.TryParse(inPayout, out float payout);
            Payout = payout;

            float.TryParse(inSpeed, out float speed);
            Speed = speed;

            CheckSum = inCheckSum;
            TMWarnings = tMWarnings;
            TMAlarms = tMAlarms;

        }
        /// <summary>
        /// Hawboldt string in
        /// </summary>
        /// <param name="inID"></param>
        /// <param name="inDate"></param>
        /// <param name="inTime"></param>
        /// <param name="inTension"></param>
        /// <param name="inSpeed"></param>
        /// <param name="inPayout"></param>
        /// <param name="tMWarnings"></param>
        /// <param name="tMAlarms"></param>
        public DataPointModel(string inID, string inDate, string inTime, string inTension, string inSpeed, string inPayout, string tMWarnings, string tMAlarms)
        {
            StringID = inID;

            //DateTime.TryParseExact(inDate,yyyyMMdd, out DateTime YMD);
            Date = inDate;

            //DateTime.TryParse(inTime, out DateTime HMS);
            Time = inTime;

            float.TryParse(inTension, out float tension);
            Tension = tension;

            float.TryParse(inPayout, out float payout);
            Payout = payout;

            float.TryParse(inSpeed, out float speed);
            Speed = speed;

            
            TMWarnings = tMWarnings;
            TMAlarms = tMAlarms;

        }
        public DataPointModel(string SID, string Ten, string Sp, string Pay)
        {
            StringID = SID;
            Tension = float.Parse(Ten);
            Speed = float.Parse(Sp);
            Payout = float.Parse(Pay);
            TMWarnings = "00000000";
            TMAlarms = "00000000";
        }

      public DataPointModel ShallowCopy()
        {
            DataPointModel copy = (DataPointModel)this.MemberwiseClone();
            return copy;
        }

    }
}
