using Store;

namespace ViewModels
{
    internal class ProcessingReadFilesViewModel : ProcessingViewModel
    {
        public static async void CombineFiles(ParseDataStore parseData)
        {
            //ParseDataStore _settingsStore = parseData;
            List<string> fileList = new(); //List<string>();
            //fileList = _settingsStore.FileList;
            string filePath = parseData.Directory;
            foreach (var fin in parseData.FileList)
            {
                var fileRead = fin.ToString();
                parseData.ReadingFileName = fileRead;
                System.IO.StreamReader file = new System.IO.StreamReader(fileRead); //Setup stream reader to read file
                string line;
                bool flag = false;
                bool dataLine = false;
                //string stringData = null;
                DataPointModel lineData = new();// Line_Data_Model();
                List<string> Data = new();// List<string>();
                List<DataPointModel> DataModels = new();// List<Line_Data_Model>();
                //Makes reading the file Asynchronous leaving the UI responsive
                await Task.Run(() =>
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        line = line.Replace("\n", String.Empty); //remove EOL Characters
                        line = line.Replace("\r", String.Empty);
                        parseData.ReadingLine = line;
                        string[] data = line.Split(',');
                        if (data.Length > 2)
                        {

                            if (parseData.SelectedWinch == "SIO Traction Winch")
                            {
                                if (data[0] == "RD")
                                {
                                    lineData.StringID = data[0];
                                    lineData.Tension = float.Parse(data[1]);
                                    lineData.Speed = float.Parse(data[2]);
                                    lineData.Payout = float.Parse(data[3]);
                                    lineData.CheckSum = data[4];
                                    lineData.TMAlarms = "00000000";
                                    lineData.TMWarnings = "00000000";
                                    //foreach (var dat in data)
                                    //{
                                    //    stringData += dat + ',';

                                    //}
                                    flag = false;
                                }
                                else if (flag == true && data[0].Contains('/'))
                                {
                                    data = data.Take(data.Length - 1).ToArray();
                                    lineData.DateAndTime = DateTime.Parse(data[0] + "T" + data[1]);
                                    //foreach (var dat in data)
                                    //{
                                    //    stringData += dat + ',';
                                    //}

                                    //MainProcessingViewModel.parseData.ReadingLine = stringData; //Updates Line being written to UI
                                    flag = false;
                                    dataLine = true;
                                }
                                else
                                {
                                    //stringData = null;
                                    dataLine = false;
                                    //lineData = new Line_Data_Model();
                                }
                            }
                            else if (parseData.SelectedWinch == "MASH Winch")
                            {
                                if (data[0] == "[LOGGING]" ||
                                    data[0] == "DATETIME[YYYY/MM/DD hh:mm:ss.s]" ||
                                    data[0] == "TIME")
                                {
                                    dataLine = false;
                                    //stringData = null; //Ingnore line that starts with above
                                    //lineData = new Line_Data_Model();
                                }
                                else
                                {
                                    string dataDateAndTime = data[0];
                                    string[] dataDate = dataDateAndTime.Split(' ');
                                    //stringData = "RD," + data[3] + "," + data[2] + "," + data[4] + "," + data[1] + "," + dataDate[0] + "," + dataDate[1];
                                    lineData.StringID = "RD";
                                    lineData.Tension = float.Parse(data[3]);
                                    lineData.Speed = float.Parse(data[2]);
                                    lineData.Payout = float.Parse(data[4]);
                                    lineData.CheckSum = data[1];
                                    lineData.DateAndTime = DateTime.Parse(dataDate[0] + "T" + dataDate[1]);
                                    lineData.TMAlarms = "00000000";
                                    lineData.TMWarnings = "00000000";
                                    //MainProcessingViewModel.parseData.ReadingLine = stringData; //Updates Line being written to UI
                                    dataLine = true;

                                }

                            }
                            else if (parseData.SelectedWinch == "Armstrong CAST 6")
                            {

                                //string dataDateAndTime = data[0];
                                //string[] dataDate = dataDateAndTime.Split(' ');
                                //stringData = line;//"RD," + data[3] + "," + data[2] + "," + data[4] + "," + data[1] + "," + dataDate[0] + "," + dataDate[1];
                                /*foreach (var dat in data)
                                {
                                    stringData += dat + ',';
                                }*/
                                //writeCombined();
                                //MainProcessingViewModel.parseData.ReadingLine = stringData; //Updates Line being written to UI
                                //stringData = null;
                                dataLine = true;

                            }
                            //Fix this section
                            else if (parseData.SelectedWinch == "UNOLS String")
                            {
                                if (data[0] == "$WIR")
                                {
                                    bool LengthBool = false;
                                    bool TensionBool = false;
                                    bool SpeedBool = false;
                                    bool PayoutBool = false;
                                    float Tension = 0;
                                    float Speed = 0;
                                    float Payout = 0;

                                    if (data.Length > 6)
                                    {
                                        LengthBool = true;
                                        TensionBool = float.TryParse(data[3], out Tension);
                                        SpeedBool = float.TryParse(data[4], out Speed);
                                        PayoutBool = float.TryParse(data[5], out Payout);
                                    }
                                    if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                                    {


                                        //Current UNOLS String
                                        lineData.StringID = data[0];
                                        lineData.Tension = float.Parse(data[3]);
                                        lineData.Speed = float.Parse(data[4]);
                                        lineData.Payout = float.Parse(data[5]);
                                        lineData.CheckSum = data[6];
                                        lineData.DateAndTime = DateTime.ParseExact($"{data[1]}T{data[2]}","yyyyMMddTHH:mm:ss.fff", null);
                                        lineData.TMAlarms = data[7];
                                        lineData.TMWarnings = data[8];
                                        /*
                                        //Gloria Early implementation
                                        lineData.StringID = data[0];
                                        //lineData.Tension = float.Parse(data[3]);
                                        lineData.Tension = Tension;
                                        //lineData.Speed = float.Parse(data[3]);
                                        lineData.Speed = Speed;
                                        //lineData.Payout = float.Parse(data[4]);
                                        lineData.Payout = Payout;
                                        lineData.CheckSum = "no chk sum";
                                        lineData.DateAndTime = DateTime.Parse($"{data[1]}");//T{data[2]}");
                                        //lineData.Date = data[1];
                                        //lineData.Time = data[2];
                                        lineData.TMAlarms = data[6];
                                        lineData.TMWarnings = data[7];
                                        */
                                        dataLine = true;
                                    }

                                }
                            }
                            else if (parseData.SelectedWinch == "Jay Jay")
                            {
                                if (data[0] == "$WIR")
                                {
                                    bool LengthBool = false;
                                    bool TensionBool = false;
                                    bool SpeedBool = false;
                                    bool PayoutBool = false;
                                    float Tension = 0;
                                    float Speed = 0;
                                    float Payout = 0;

                                    if (data.Length > 6)
                                    {
                                        LengthBool = true;
                                        TensionBool = float.TryParse(data[3], out Tension);
                                        SpeedBool = float.TryParse(data[4], out Speed);
                                        PayoutBool = float.TryParse(data[5], out Payout);
                                    }
                                    if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                                    {


                                        //Current UNOLS String
                                        //lineData.StringID = data[0];
                                        //lineData.Tension = float.Parse(data[3]);
                                        //lineData.Speed = float.Parse(data[4]);
                                        //lineData.Payout = float.Parse(data[5]);
                                        //lineData.Checksum = data[6];
                                        //lineData.DateAndTime = DateTime.Parse($"{data[1]}T{data[2]}");
                                        //lineData.TMAlarms = data[7];
                                        //lineData.TMWarnings = data[8];

                                        //Gloria Early implementation
                                        lineData.StringID = data[0];
                                        //lineData.Tension = float.Parse(data[3]);
                                        lineData.Tension = Tension;
                                        //lineData.Speed = float.Parse(data[3]);
                                        lineData.Speed = Speed;
                                        //lineData.Payout = float.Parse(data[4]);
                                        lineData.Payout = Payout;
                                        lineData.CheckSum = "no chk sum";
                                        //lineData.DateAndTime = DateTime.ParseExact($"{data[1]} {data[2]}","yyyyMMdd HH:mm:SS.fff", CultureInfo.InvariantCulture);
                                        lineData.Date = data[1];
                                        lineData.Time = data[2];
                                        lineData.TMAlarms = data[6];
                                        lineData.TMWarnings = data[7];

                                        dataLine = true;
                                    }
                                }
                            }
                                if (dataLine == true)
                                {
                                    //Data.Add(stringData); //add the string to Data List
                                    DataModels.Add(lineData);
                                    //MainProcessingViewModel.parseData.ReadingLine = stringData; //Updates Line being written to UI
                                    //MainProcessingViewModel.parseData.ReadingLine = lineData.StringID + "," + lineData.Tension + "," + lineData.Speed + "," + lineData.Payout + "," + lineData.DateAndTime;
                                    //stringData = null; //Clear stringData for next loop
                                    lineData = new DataPointModel();
                                    dataLine = false;
                                }
                            
                        }

                    }
                    //Write data
                    ProcessingWriteFilesViewModel.WriteCombined(DataModels, parseData); //Write Data list
                });

                file.Close(); //Close the file
            }
            parseData.ReadingLine = "Done!"; //Update UI with done
        }
        public static async void ParseFiles(ParseDataStore parseData)
        {
            // Read threshold values
            float? minPayout = float.Parse(parseData.MinPayout);
            float? minTension = float.Parse(parseData.MinTension);
            parseData.ReadingFileName = parseData.CombinedFileName;
            parseData.ReadingLine = "Starting!";
            //Read in collected file and determine maximum values of casts
            using (System.IO.StreamReader sr = new System.IO.StreamReader(parseData.Directory + '\\' + parseData.CombinedFileName, true))
            {
                float maxTensionCurrent = 0;
                float maxPayoutCurrent = 0;
                string? maxTensionString = null;
                string? maxPayoutString = null;
                int cast = 1;
                int i = 0;
                bool castActive = false;
                float temp;
                string input;
                ProcessPointDataModel processPointDataModel = new();
                ProcessCastDataModel processCastDataModel = new();
                await Task.Run(() =>
                {
                    while ((input = sr.ReadLine()) != null)
                    {
                        input = input.Replace("\n", String.Empty); //remove EOL Characters
                        input = input.Replace("\r", String.Empty);
                        DataPointModel lineData = new();
                        string[] values = input.Split(',');
                        //object[] valueObject = new object[values.Length];
                        //int i = 0;

                        //lineData.StringID = values[0];
                        lineData.Tension = float.Parse(values[3]);
                        //lineData.Speed = float.Parse(values[4]);
                        lineData.Payout = float.Parse(values[5]);
                        //lineData.Checksum = values[8];
                        //lineData.DateAndTime = DateTime.Parse($"{values[1]}T{values[2]}");
                        //lineData.TMAlarms = values[7];
                        //lineData.TMWarnings = values[6];
                        processCastDataModel.CastNumber = cast;

                        {
                            
                            //detect start of cast (values above threshold with positive slope)
                            if (lineData.Tension > minTension && Math.Abs(lineData.Payout) > minPayout)
                            {
                                castActive = true;
                                //check for new maximum values (tension and payout) and store
                                if (lineData.Tension > maxTensionCurrent)
                                {
                                    maxTensionCurrent = lineData.Tension;
                                    maxTensionString = input;
                                }
                                if (Math.Abs(lineData.Payout) > maxPayoutCurrent)
                                {
                                    maxPayoutCurrent = Math.Abs(lineData.Payout);
                                    maxPayoutString = input;
                                }

                            }
                            //detect end of cast (values below threshold with negative slope)

                            if (/*lineData.Tension < minTension &&*/ Math.Abs(lineData.Payout) < minPayout && castActive == true)
                            {
                                ProcessingWriteFilesViewModel.WriteProcessed(maxTensionString, maxPayoutString, cast, parseData); //end cast, increment cast number, write processed data
                                parseData.ReadingLine = maxTensionString;
                                parseData.ProcessCasts?.Add(processCastDataModel);
                                cast++;
                                castActive = false;
                                maxPayoutCurrent = 0;
                                maxTensionCurrent = 0;
                                i = 0;

                            }

                            if (castActive)
                            {
                                processPointDataModel.Payout = lineData.Payout;
                                processPointDataModel.Tension = lineData.Tension;
                                processPointDataModel.PointNumber = i++;
                                processCastDataModel.ProcessPoints?.Add(processPointDataModel);
                            }
                        }
                        
                    }
                    parseData.ReadingLine = "Done!";
                });
            }
            //parseData.ReadingLine = "Done!";
        }
        public static object ReadProcessConfig()
        {
            //Logic to read config file for initial setup based on previous saved data
            List<string> lines = new List<string>();
            ParseDataStore parseData = new ParseDataStore();
            parseData.AvailableWinches = new List<string>
            {
                "MASH Winch",
                "SIO Traction Winch",
                "Armstrong CAST 6",
                "UNOLS String",
                "Jay Jay"
            };

            parseData.AvailableTensions = new List<string>
            {
                "-100",
                "0",
                "100",
                "250",
                "500"
            };

            parseData.AvailablePayouts = new List<string>
            {
                "-10",
                "0",
                "1",
                "5",
                "10",
                "12",
                "25",
                "50"
            };
            //set the name of the config file
            string fileName = "ProcessConfig.txt";
            //set the path to application directory
            string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            try
            {
                using (StreamReader stream = new StreamReader(destPath))
                {
                    string text;
                    while ((text = stream.ReadLine()) != null)
                    {
                        lines.Add(text);
                    }
                    foreach (var line in lines)
                    {
                        int delim = line.IndexOf(",");
                        if (line.Substring(0, delim) == "Minimum Tension")
                        {
                             parseData.MinTension= line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Minimum Payout")
                        {
                            parseData.MinPayout = line.Substring(delim + 1);
                        }
                        if (line.Substring(0, delim) == "Selected Winch")
                        {
                            parseData.SelectedWinch = line.Substring(delim + 1);
                        }
                    }
                    
                    return parseData;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
