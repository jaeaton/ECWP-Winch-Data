namespace ViewModels
{
    internal class ProcessingWriteFilesViewModel
    {
        public static void WriteCombined(List<DataPointModel> Data)
        {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(MainProcessingViewModel.parseData.Directory + '\\' + MainProcessingViewModel.parseData.CombinedFileName, true))    //Write Combined Log file
            {

                string date = "yyyy/MM/dd";
                string time = "HH:mm:ss.fff";

                for (int j = 0; j < Data.Count; j++)
                {
                    DataPointModel lineData = Data[j];
                    file.WriteLine(lineData.StringID + "," + lineData.DateAndTime.ToString(date) + "," + lineData.DateAndTime.ToString(time) + "," + lineData.Tension + "," + lineData.Speed + "," + lineData.Payout + "," + lineData.TMWarnings + "," + lineData.TMAlarms + "," + lineData.Checksum);

                }


            }
        }
        public static void writeProcessed(string maxTensionString, string maxPayoutString, int cast)  //function to write log
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(MainProcessingViewModel.parseData.Directory + '\\' + MainProcessingViewModel.parseData.ProcessedFileName, true))    //Write Processed Log file
            {
                file.WriteLine("Cast Number " + cast);
                file.WriteLine("Winch Data, Date, Time, Tension(lbf), Speed(m/min), Payout(m), Tension Warnings, Tension Alarms, Checksum/Index");
                file.WriteLine(maxTensionString);
                file.WriteLine(maxPayoutString);
                file.WriteLine("\n");
            }
        }
    }
}
