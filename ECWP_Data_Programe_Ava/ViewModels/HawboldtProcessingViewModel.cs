using System;

namespace ViewModels
{
    public class HawboldtProcessingViewModel
    {
        
        public string HawboldtProcess(byte[] byteArray, string HawboldtModel)
        {
            string ResponseData = string.Empty;
            switch (HawboldtModel)
            {
                case "SPRE-3464":
                    ResponseData = SPRE_3464(byteArray);
                    return ResponseData;
                case "SPRE-2648RS":
                    ResponseData = SPRE_2648RS(byteArray);
                    return ResponseData;
                case "SPRE-2640":
                    ResponseData = SPRE_2640(byteArray);
                    return ResponseData;
                case "SPRE-2036S":
                    ResponseData = SPRE_2036S(byteArray);
                    return ResponseData;
                default:
                    return ResponseData;
                    //break;
            }

        }
        private string SPRE_3464(byte[] byteArray)
        {
            string ResponseData = string.Empty;
            byte[] bytes1 = new byte[1];
            byte[] bytes2 = new byte[2];
            byte[] bytes4 = new byte[4];
            //Process Date
            //Process Year
            Array.Copy(byteArray, 0, bytes2, 0, 2);
            string year = TwoByteInt(bytes2);
            
            //process Month
            Array.Copy(byteArray, 2, bytes1, 0, 1);
            string month = OneByteInt(bytes1);
            
            //Process Day
            Array.Copy(byteArray, 3, bytes1, 0, 1);
            string day = OneByteInt(bytes1);

            //Process Time
            //Process Hour
            Array.Copy(byteArray, 4, bytes1, 0, 1);
            string hour = OneByteInt(bytes1);

            //Process Minute
            Array.Copy(byteArray, 5, bytes1, 0, 1);
            string minute = OneByteInt(bytes1);

            //Process Seconds
            Array.Copy(byteArray, 6, bytes1, 0, 1);
            string second = OneByteInt(bytes1);

            //Process Tension
            Array.Copy(byteArray, 28, bytes4, 0, 4);
            string tension = RealByteInt(bytes4);

            //Process Payout
            Array.Copy(byteArray, 32, bytes4, 0, 4);
            string payout = RealByteInt(bytes4);

            //Process Speed
            Array.Copy(byteArray, 34, bytes4, 0, 4);
            string speed = RealByteInt(bytes4);

            //Form data into string
            ResponseData = $"$HWIR1,{year}-{month}-{day},{hour}:{minute}:{second},{tension},{speed},{payout}";
            return ResponseData;
        }
        private string SPRE_2648RS(byte[] byteArray)
        {
            string ResponseData = string.Empty;
            byte[] bytes1 = new byte[1];
            byte[] bytes2 = new byte[2];
            byte[] bytes4 = new byte[4];
            //Process Date
            //Process Year
            Array.Copy(byteArray, 0, bytes2, 0, 2);
            string year = TwoByteInt(bytes2);

            //process Month
            Array.Copy(byteArray, 2, bytes1, 0, 1);
            string month = OneByteInt(bytes1);

            //Process Day
            Array.Copy(byteArray, 3, bytes1, 0, 1);
            string day = OneByteInt(bytes1);

            //Process Time
            //Process Hour
            Array.Copy(byteArray, 4, bytes1, 0, 1);
            string hour = OneByteInt(bytes1);

            //Process Minute
            Array.Copy(byteArray, 5, bytes1, 0, 1);
            string minute = OneByteInt(bytes1);

            //Process Seconds
            Array.Copy(byteArray, 6, bytes4, 0, 4);
            string second = TimeRealByteInt(bytes4);

            //Process Tension
            Array.Copy(byteArray, 64, bytes4, 0, 4);
            string tension = RealByteInt(bytes4);

            //Process Payout
            Array.Copy(byteArray, 68, bytes4, 0, 4);
            string payout = RealByteInt(bytes4);

            //Process Speed
            Array.Copy(byteArray, 72, bytes4, 0, 4);
            string speed = RealByteInt(bytes4);

            //Form data into string
            ResponseData = $"$HWIR2,{year}-{month}-{day},{hour}:{minute}:{second},{tension},{speed},{payout}";
            return ResponseData;
        }
        private string SPRE_2640(byte[] byteArray)
        {
            string ResponseData = string.Empty;
            byte[] bytes1 = new byte[1];
            byte[] bytes2 = new byte[2];
            byte[] bytes4 = new byte[4];
            //Process Date
            //Process Year
            Array.Copy(byteArray, 0, bytes2, 0, 2);
            string year = TwoByteInt(bytes2);

            //process Month
            Array.Copy(byteArray, 2, bytes1, 0, 1);
            string month = OneByteInt(bytes1);

            //Process Day
            Array.Copy(byteArray, 3, bytes1, 0, 1);
            string day = OneByteInt(bytes1);

            //Process Time
            //Process Hour
            Array.Copy(byteArray, 4, bytes1, 0, 1);
            string hour = OneByteInt(bytes1);

            //Process Minute
            Array.Copy(byteArray, 5, bytes1, 0, 1);
            string minute = OneByteInt(bytes1);

            //Process Seconds
            Array.Copy(byteArray, 6, bytes4, 0, 4);
            string second = TimeRealByteInt(bytes4);

            //Process Tension
            Array.Copy(byteArray, 64, bytes4, 0, 4);
            string tension = RealByteInt(bytes4);

            //Process Payout
            Array.Copy(byteArray, 68, bytes4, 0, 4);
            string payout = RealByteInt(bytes4);

            //Process Speed
            Array.Copy(byteArray, 72, bytes4, 0, 4);
            string speed = RealByteInt(bytes4);

            //Form data into string
            ResponseData = $"$HWIR3,{year}-{month}-{day},{hour}:{minute}:{second},{tension},{speed},{payout}";
            return ResponseData;
        }
        private string SPRE_2036S(byte[] byteArray)
        {
            string ResponseData = string.Empty;
            byte[] bytes1 = new byte[1];
            byte[] bytes2 = new byte[2];
            byte[] bytes4 = new byte[4];
            //Process Date
            //Process Year
            Array.Copy(byteArray, 0, bytes2, 0, 2);
            string year = TwoByteInt(bytes2);

            //process Month
            Array.Copy(byteArray, 2, bytes1, 0, 1);
            string month = OneByteInt(bytes1);

            //Process Day
            Array.Copy(byteArray, 3, bytes1, 0, 1);
            string day = OneByteInt(bytes1);

            //Process Time
            //Process Hour
            Array.Copy(byteArray, 4, bytes1, 0, 1);
            string hour = OneByteInt(bytes1);

            //Process Minute
            Array.Copy(byteArray, 5, bytes1, 0, 1);
            string minute = OneByteInt(bytes1);

            //Process Seconds
            Array.Copy(byteArray, 6, bytes4, 0, 4);
            string second = TimeRealByteInt(bytes4);

            //Process Tension
            Array.Copy(byteArray, 64, bytes4, 0, 4);
            string tension = RealByteInt(bytes4);

            //Process Payout
            Array.Copy(byteArray, 68, bytes4, 0, 4);
            string payout = RealByteInt(bytes4);

            //Process Speed
            Array.Copy(byteArray, 72, bytes4, 0, 4);
            string speed = RealByteInt(bytes4);

            //Form data into string
            ResponseData = $"$HWIR4,{year}-{month}-{day},{hour}:{minute}:{second},{tension},{speed},{payout}";
            return ResponseData;
        }

        private string OneByteInt(byte[] bytes1)
        {
            //int Int = BitConverter.ToInt16(bytes1);
            int Int = (int)bytes1[0];
            return Int.ToString();
        }
        private string TwoByteInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            int Int = BitConverter.ToInt16(bytes);
            return Int.ToString();
        }
        private string RealByteInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            float Float = BitConverter.ToSingle(bytes);
            return Float.ToString("N1");
        }
        private string TimeRealByteInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            float Float = BitConverter.ToSingle(bytes);
            return Float.ToString("N3");
        }
    }
}
