namespace Model
{
    public class DataPointModel
    {

        public string StringID { get; set; }
        /// <summary>
        /// Date value from data source
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// Time from data source
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// Tension value
        /// </summary>
        public float Tension { get; set; }
        /// <summary>
        /// Payout value
        /// </summary>
        public float Payout { get; set; }
        /// <summary>
        /// Speed value
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Check Sum Value
        /// </summary>
        public string CheckSum { get; set; }
        /// <summary>
        /// Tension Member Warning (8 bits)
        /// </summary>
        public string TMWarnings { get; set; }
        /// <summary>
        /// Tension member alarms(8 bits)
        /// </summary>
        public string TMAlarms { get; set; }
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
        public DataPointModel(string inID, string inDate, string inTime, string inTension, string inSpeed, string inPayout, string inCheckSum, string tMWarnings, string tMAlarms)
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
        
    }
}
