namespace ViewModels
{
    internal class ProcessingWriteFilesViewModel : ProcessingViewModel
    {
        public static void WriteCombined(List<DataPointModel> Data, ParseDataStore parseData)
        {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(parseData.Directory + '\\' + parseData.CombinedFileName, true))    //Write Combined Log file
            {

                string? dateFormat = "yyyy/MM/dd";
                string? timeFormat = "HH:mm:ss.fff";

                for (int j = 0; j < Data.Count; j++)
                {
                    DataPointModel lineData = Data[j];
                    file.WriteLine(lineData.StringID + "," + lineData.DateAndTime.ToString(dateFormat) + "," + lineData.DateAndTime.ToString(timeFormat) + "," + lineData.Tension + "," + lineData.Speed + "," + lineData.Payout + "," + lineData.TMWarnings + "," + lineData.TMAlarms + "," + lineData.CheckSum);

                }


            }
        }
        public static void WriteProcessed(string maxTensionString, string maxPayoutString, int cast, ParseDataStore parseData)  //function to write log
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(parseData.Directory + '\\' + parseData.ProcessedFileName, true))    //Write Processed Log file
            {
                file.WriteLine("Cast Number " + cast);
                file.WriteLine("Winch Data, Date, Time, Tension(lbf), Speed(m/min), Payout(m), Tension Warnings, Tension Alarms, Checksum/Index");
                file.WriteLine(maxTensionString);
                file.WriteLine(maxPayoutString);
                file.WriteLine("\n");
            }
        }

        public static void WriteProcessConfig(ParseDataStore ParseData)
        {
            //Logic to save new parameters to config file
            //Set filname
            string fileName = "ProcessConfig.txt";
            //Set path to save config file (Application directory)
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            //Populate array with global configuartion values
            string[] lines =
                {
                $"Minimum Tension,{ ParseData.MinTension }",
                $"Minimum Payout,{ ParseData.MinPayout }",
                $"Selected Winch,{ ParseData.WinchSelection }"
                };
            //Write each line of array using stream writer
            using (StreamWriter stream = new StreamWriter(destPath))
            {
                foreach (string line in lines)
                    stream.WriteLine(line);
            }
        }
    }
}
