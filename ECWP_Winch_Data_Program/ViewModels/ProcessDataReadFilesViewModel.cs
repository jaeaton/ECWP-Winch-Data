using DocumentFormat.OpenXml.EMMA;

namespace ViewModels
{
    internal class ProcessDataReadFilesViewModel
    {
        //Single command to both read and then sort the data
        //step 1
        public static async void ReadDataFiles()//ParseDataStore parseData)
        {
            ParseDataStore parseData = ProcessDataViewModel.ParseData;
            CancellationToken token = ProcessDataViewModel.ParseData.CancellationTokenSource.Token;
            ProcessDataViewModel.ParseData.ReadingLine = "Reading!"; //Update UI with reading
            ProcessDataViewModel.ParseData.NumberOfProcessedFiles = 0;
            //bool canceled = false;
            //Makes reading the file Asynchronous leaving the UI responsive
            try
            {
                await Task.Run(() => ReadDataFromLogs(parseData), ProcessDataViewModel.ParseData.CancellationTokenSource.Token);
            }
            catch (Exception ae)
            {
                if (ae is TaskCanceledException)
                {
                    //cancelled = true;
                    parseData.CancellationTokenSource.Dispose();
                }
                else
                {
                    //cancelled = true;
                    parseData.CancellationTokenSource.Cancel();
                    parseData.CancellationTokenSource.Dispose();
                }
            }
            //Dispatcher.UIThread.Post(() => ProcessDataViewModel.ParseData.ProcessWinchDataButton = "Start Processing",DispatcherPriority.Input);
            if (!parseData.CancellationTokenSource.IsCancellationRequested)
            {
                parseData.ReadingLine = "Done!"; //Update UI with done
            }

            parseData.ProcessWinchDataButton = "Start Processing";
        }

        //step 2
        public static void ReadDataFromLogs(ParseDataStore parseData)
        {
            string filePath = parseData.Directory;
            foreach (var fin in parseData.FileList)
            {
                ProcessDataViewModel.ParseData.NumberOfProcessedFiles++;
                var fileRead = fin.ToString();
                //parseData.ReadingFileName = fileRead;
                System.IO.StreamReader file = new System.IO.StreamReader(fileRead); //Setup stream reader to read file
                string line = string.Empty;
                bool flag = false;
                bool dataLine = false;
                DataPointModel lineData = new();
                List<string> Data = new();
                List<DataPointModel> DataModels = new();
                //Dispatcher.UIThread.Post(() => parseData.ReadingFileName = fileRead);

                DataModels.Clear();
                while ((line = file.ReadLine()!) != null)
                {
                    line = line.Replace("\n", String.Empty); //remove EOL Characters
                    line = line.Replace("\r", String.Empty);
                    //parseData.ReadingLine = line;
                    string[] data = line.Split(',');
                    if (data.Length > 2)
                    {
                        if (parseData.SelectedWinch == "SIO Traction Winch")
                        {
                            //SIO Traction Winch Data Format: String ID, Tension, Speed, Payout, Checksum /r/n Date, Time /r/n
                            if (data[0] == "RD")
                            {
                                bool LengthBool = false;
                                bool TensionBool = false;
                                bool SpeedBool = false;
                                bool PayoutBool = false;
                                float Tension = 0;
                                float Speed = 0;
                                float Payout = 0;

                                if (data.Length > 5)
                                {
                                    LengthBool = true;
                                    TensionBool = float.TryParse(data[3], out Tension);
                                    SpeedBool = float.TryParse(data[2], out Speed);
                                    PayoutBool = float.TryParse(data[4], out Payout);
                                }
                                if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                                {
                                    lineData.StringID = data[0];
                                    lineData.Tension = Tension;
                                    lineData.Speed = Speed;
                                    lineData.Payout = Payout;
                                    if (lineData.Payout < 0)
                                    {
                                        lineData.Payout = -1 * lineData.Payout;
                                    }
                                    else if (lineData.Payout > 0)
                                    {
                                        lineData.Payout = -1 * lineData.Payout;
                                    }
                                    lineData.CheckSum = data[4];
                                    lineData.TMAlarms = "00000000";
                                    lineData.TMWarnings = "00000000";
                                }

                                flag = true;
                            }
                            else if (flag == true && data[0].Contains('/'))
                            {
                                data = data.Take(data.Length - 1).ToArray();
                                bool test = DateTime.TryParse(data[0] + "T" + data[1], out lineData.DateAndTime);
                                if (test)
                                {
                                    flag = false;
                                    dataLine = true;
                                }
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
                            //MASH winch data format: date time, Checksum, Speed, Tension, Payout
                            if (data[0] == "[LOGGING]" ||
                                data[0] == "DATETIME[YYYY/MM/DD hh:mm:ss.s]" ||
                                data[0] == "TIME")
                            {
                                dataLine = false;
                            }
                            else
                            {
                                bool LengthBool = false;
                                bool TensionBool = false;
                                bool SpeedBool = false;
                                bool PayoutBool = false;
                                float Tension = 0;
                                float Speed = 0;
                                float Payout = 0;

                                if (data.Length > 4)
                                {
                                    LengthBool = true;
                                    TensionBool = float.TryParse(data[3], out Tension);
                                    SpeedBool = float.TryParse(data[2], out Speed);
                                    PayoutBool = float.TryParse(data[4], out Payout);
                                }
                                if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                                {
                                    string dataDateAndTime = data[0];
                                    string[] dataDate = dataDateAndTime.Split(' ');
                                    //stringData = "RD," + data[3] + "," + data[2] + "," + data[4] + "," + data[1] + "," + dataDate[0] + "," + dataDate[1];
                                    lineData.StringID = "RD";
                                    lineData.Tension = Tension;
                                    lineData.Speed = Speed;
                                    lineData.Payout = Payout;
                                    lineData.CheckSum = data[1];
                                    lineData.DateAndTime = DateTime.Parse(dataDate[0] + "T" + dataDate[1]);
                                    lineData.TMAlarms = "00000000";
                                    lineData.TMWarnings = "00000000";
                                    dataLine = true;
                                }
                            }
                        }
                        else if (parseData.SelectedWinch == "WinchDAC") //Previously Armstrong Cast 6
                        {
                            //Winch DAC Log format: Preamble (Winch Date Time MTN ID RD,Date and Time,Tension,Speed,Payout,Checksum

                            bool LengthBool = false;
                            bool TensionBool = false;
                            bool SpeedBool = false;
                            bool PayoutBool = false;
                            float Tension = 0;
                            float Speed = 0;
                            float Payout = 0;

                            if (data.Length > 4)
                            {
                                LengthBool = true;
                                TensionBool = float.TryParse(data[2], out Tension);
                                SpeedBool = float.TryParse(data[3], out Speed);
                                PayoutBool = float.TryParse(data[4], out Payout);
                            }
                            if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                            {
                                lineData.StringID = "RD";
                                lineData.Tension = Tension;
                                lineData.Speed = Speed;
                                lineData.Payout = Payout;
                                lineData.CheckSum = data[1];
                                lineData.DateAndTime = DateTime.Parse(data[1]);
                                lineData.TMAlarms = "00000000";
                                lineData.TMWarnings = "00000000";
                                dataLine = true;
                            }
                        }
                        else if (parseData.SelectedWinch == "ECWP MTNW")
                        {
                            bool LengthBool = false;
                            bool TensionBool = false;
                            bool SpeedBool = false;
                            bool PayoutBool = false;
                            float Tension = 0;
                            float Speed = 0;
                            float Payout = 0;

                            if (data.Length > 5)
                            {
                                LengthBool = true;
                                TensionBool = float.TryParse(data[3], out Tension);
                                SpeedBool = float.TryParse(data[4], out Speed);
                                PayoutBool = float.TryParse(data[5], out Payout);
                            }
                            if (LengthBool != false && TensionBool != false && SpeedBool != false && PayoutBool != false)//float.TryParse(data[2], out float Tension) != false)
                            {
                                string dataDateAndTime = data[1];
                                string[] dataDate = dataDateAndTime.Split('T');
                                //stringData = "RD," + data[3] + "," + data[2] + "," + data[4] + "," + data[1] + "," + dataDate[0] + "," + dataDate[1];
                                lineData.StringID = data[0];
                                lineData.Tension = Tension;
                                lineData.Speed = Speed;
                                lineData.Payout = Payout;
                                lineData.CheckSum = string.Empty;
                                lineData.DateAndTime = DateTime.Parse(dataDate[0] + "T" + dataDate[1]);
                                lineData.TMAlarms = "00000000";
                                lineData.TMWarnings = "00000000";
                                dataLine = true;
                            }
                        }

                        //Fix this section
                        else if (parseData.SelectedWinch == "UNOLS String")
                        {
                            //UNOLS String data Format: String ID, Date, Time, Tension, Speed, Payout, Checksum?, TM Alarms, TM Warnings
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
                                    lineData.Tension = Tension;
                                    lineData.Speed = Speed;
                                    lineData.Payout = Payout;
                                    lineData.CheckSum = data[6];
                                    lineData.DateAndTime = DateTime.ParseExact($"{data[1]}T{data[2]}", "yyyyMMddTHH:mm:ss.fff", null);
                                    lineData.TMAlarms = data[7];
                                    lineData.TMWarnings = data[8];
                                    /*
                                    //Gloria Early implementation
                                    lineData.StringID = data[0];
                                    lineData.Tension = Tension;
                                    lineData.Speed = Speed;
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
                            //Jay Jay data format: String ID, Date, Time, Tension, Speed, Payout, TM Alarm, TM Warning, Check Sum
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
                                    lineData.StringID = data[0];
                                    lineData.Tension = Tension;
                                    lineData.Speed = Speed;
                                    lineData.Payout = Payout;
                                    lineData.CheckSum = "no chk sum";
                                    lineData.DateAndTime = DateTime.ParseExact($"{data[1]} {data[2]}", "yyyyMMddTHH:mm:SS.fff", CultureInfo.InvariantCulture);
                                    lineData.Date = data[1];
                                    lineData.Time = data[2];
                                    lineData.TMAlarms = data[6];
                                    lineData.TMWarnings = data[7];

                                    dataLine = true;
                                }
                            }
                        }
                        else if (parseData.SelectedWinch == "Atlantis 3PS")
                        {
                            //Atlantis 3PS Data format: Winch Date Time 3PS Date,Time,WinchID,Tension,Tension Alarm, Speed, Speed Alarm, Payout, Payout Alarm,Maximum Tension
                            if (data.Length > 6)
                            {
                                string[] header = data[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                                bool[] bools = new bool[4];
                                if (header[3] == "3PS" && data[2] == parseData.WinchID)
                                {
                                    lineData.StringID = header[3];
                                    bools[0] = float.TryParse(data[3], out lineData.Tension);
                                    bools[1] = float.TryParse(data[7], out lineData.Speed);
                                    bools[2] = float.TryParse(data[5], out lineData.Payout);
                                    //lineData.CheckSum = data[1];
                                    lineData.Date = header[4];
                                    lineData.Time = data[1];
                                    bools[3] = DateTime.TryParse(header[1] + "T" + data[1], out lineData.DateAndTime);
                                    lineData.TMAlarms = "00000000";
                                    lineData.TMWarnings = "00000000";
                                    if (bools[0] == true && bools[1] == true && bools[2] == true && bools[3] == true)
                                    {
                                        dataLine = true;
                                    }
                                }
                            }
                        }
                        if (parseData.UseDateRange == true)
                        {
                            if (lineData.DateAndTime < parseData.StartDate || lineData.DateAndTime > parseData.EndDate)
                            {
                                dataLine = false;
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
                ParseDataFromLogs(DataModels);
                file.Close(); //Close the file

                //Cancel task if cancellation requested
                if (ProcessDataViewModel.ParseData.CancellationTokenSource.Token.IsCancellationRequested)
                {
                    ProcessDataViewModel.ParseData.CancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
            }
        }

        //Step 3
        public static void ParseDataFromLogs(List<DataPointModel> dataPointModels)
        {
            // Read stored values
            ParseDataStore parseData = ProcessDataViewModel.ParseData;
            WinchModel winchModel = MainViewModel._configDataStore.CurrentWinch;
            float minPayout = float.Parse(parseData.MinPayout);
            float minTension = float.Parse(parseData.MinTension);
            float maxTensionCurrent = parseData.MaxTensionCurrent;
            float maxTensionPayoutCurrent = parseData.MaxTensionPayoutCurrent;
            float maxPayoutCurrent = parseData.MaxPayoutCurrent;
            string maxTensionString = parseData.MaxTensionString;
            string maxPayoutString = parseData.MaxPayoutString;
            int cast = parseData.Cast;
            DateTime currentLineTime = DateTime.Now;
            DateTime lastLineTime = DateTime.Now;
            TimeSpan timeBetweenPoints = TimeSpan.Zero;
            DataPointModel MaxTensionDataPoint = parseData.MaxTensionDataPoint;
            DataPointModel MaxPayoutDataPoint = parseData.MaxPayoutDataPoint;
            //int i = 0;
            bool castActive = parseData.CastActive;
            bool deltaTime = false;
            //float temp;
            string input = string.Empty;

            //ProcessPointDataModel processPointDataModel = new();
            ProcessCastDataModel processCastDataModel = new();

            foreach (DataPointModel lineData in dataPointModels)
            {
                processCastDataModel.CastNumber = cast;

                //detect start of cast (values above threshold with positive slope)
                if (lineData.Tension > minTension && lineData.Payout > minPayout)
                {
                    castActive = true;
                    //check for new maximum values (tension and payout) and store
                    if (lineData.Tension > maxTensionCurrent)
                    {
                        maxTensionCurrent = lineData.Tension;
                        maxTensionPayoutCurrent = lineData.Payout;
                        //maxTensionString = input;
                        MaxTensionDataPoint = lineData.DeepCopy();
                    }
                    if (Math.Abs(lineData.Payout) > maxPayoutCurrent)
                    {
                        maxPayoutCurrent = Math.Abs(lineData.Payout);
                        //maxPayoutString = input;
                        MaxPayoutDataPoint = lineData.DeepCopy();
                    }
                }
                //Detect time elapsed from last line value
                if (winchModel.TowYoTimeEnable)
                {
                    lastLineTime = currentLineTime;
                    currentLineTime = lineData.DateAndTime;
                    timeBetweenPoints = currentLineTime - lastLineTime;
                    if (timeBetweenPoints > TimeSpan.Parse($"0:{winchModel.TowYoTimeSelected}:00"))
                    {
                        deltaTime = true;
                    }
                }
                else { deltaTime = true; }
                
                //detect end of cast (values below threshold with negative slope)
                //Write data point
                if (/*lineData.Tension < minTension &&*/ lineData.Payout < minPayout && castActive == true && deltaTime == true)
                {
                    float mTenSend = maxTensionCurrent;
                    float mTenPaySend = maxTensionPayoutCurrent;
                    float mPaySend = maxPayoutCurrent;
                    int castSend = cast;
                    DataPointModel mTenDataPt = new();
                    mTenDataPt = MaxTensionDataPoint.DeepCopy();
                    DataPointModel mPayDataPt = new();
                    mPayDataPt = MaxPayoutDataPoint.DeepCopy();
                    //ProcessDataWriteFilesViewModel.WriteProcessed(maxTensionString, maxPayoutString, cast); //end cast, increment cast number, write processed data
                    //Insert data directly into excel sheet
                    //ExcelViewModel.AddCastData(mTenDataPt, mPayDataPt, castSend, MainViewModel._configDataStore.CurrentWinch);
                    //parseData.ReadingLine = maxTensionString;
                    parseData.ProcessCasts.Add(processCastDataModel);
                    //parseData.WireLog.Add(new WireLogModel(lineData.DateAndTime, "Cast Data", cast, maxTensionCurrent, maxPayoutCurrent))
                    Dispatcher.UIThread.Post(() => AddData(lineData, castSend, mTenSend, mTenPaySend, mPaySend));
                    //parseData.WireLog.Add(new WireLogModel(lineData.DateAndTime, "Cast Data", cast, maxTensionCurrent, maxPayoutCurrent)) ;
                    //parseData.DataToPlot.Add(lineData);
                    cast++;
                    castActive = false;
                    deltaTime = false;
                    maxPayoutCurrent = 0;
                    maxTensionCurrent = 0;
                    maxTensionPayoutCurrent = 0;
                    parseData.MaxTensionCurrent = maxTensionCurrent;
                    parseData.MaxTensionPayoutCurrent = maxTensionPayoutCurrent;
                    parseData.MaxPayoutCurrent = maxPayoutCurrent;
                    parseData.MaxTensionString = maxTensionCurrent.ToString();
                    parseData.MaxPayoutString = maxTensionCurrent.ToString();
                    parseData.MaxPayoutDataPoint = MaxPayoutDataPoint;
                    parseData.MaxTensionDataPoint = MaxTensionDataPoint;
                    //i = 0;
                }

                if (castActive)
                {
                    parseData.DataToPlot.Add(lineData);
                }
            }
            //Live Charts 2 final may make this possible
            //foreach (var val in parseData.DataToPlot)
            //{
            //    Dispatcher.UIThread.Post(() => parseData.ChartData.AddData(val));
            //}
            parseData.MaxTensionCurrent = maxTensionCurrent;
            parseData.MaxTensionPayoutCurrent = maxTensionPayoutCurrent;
            parseData.MaxPayoutCurrent = maxPayoutCurrent;
            parseData.MaxTensionString = maxTensionCurrent.ToString();
            parseData.MaxPayoutString = maxTensionCurrent.ToString();
            parseData.Cast = cast;
            parseData.MaxPayoutDataPoint = MaxPayoutDataPoint;
            parseData.MaxTensionDataPoint = MaxTensionDataPoint;
            //parseData.DataToPlot.Clear();
        }

        private static void AddData(DataPointModel lineData, int cast, float maxTenCurrent, float maxTenPayCurrent, float maxPayCurrent)
        {
            WinchModel winch = MainViewModel._configDataStore.CurrentWinch;
            //ProcessDataViewModel.ParseData.WireLog.Add(new WireLogModel(lineData.DateAndTime, "Cast Data" ,cast, maxTenCurrent, maxTenPayCurrent, maxPayCurrent));
            //Search backwards for date
            foreach (var model in ProcessDataViewModel.ParseData.WireLog.Select((value, i) => new { i, value }).Reverse())
            {
                //Compare dates
                if (lineData.DateAndTime > model.value.EventDate)
                {
                    ProcessDataViewModel.ParseData.WireLog.Insert(model.i + 1, new WireLogModel(lineData.DateAndTime, "Cast", winch.InstalledLength, cast, maxTenCurrent, maxTenPayCurrent, maxPayCurrent, string.Empty, MainViewModel._configDataStore.CruiseNameBox));
                    break;
                }
            }
            if (ProcessDataViewModel.ParseData.WireLog.Count == 0)
            {
                ProcessDataViewModel.ParseData.WireLog.Add(new WireLogModel(lineData.DateAndTime, "Cast", winch.InstalledLength, cast, maxTenCurrent, maxTenPayCurrent, maxPayCurrent, string.Empty, MainViewModel._configDataStore.CruiseNameBox));
            }
            //ProcessDataViewModel.ParseData.WireLog.Add(new WireLogModel(lineData.DateAndTime, "Cast Data", winch.InstalledLength, cast, maxTenCurrent, maxTenPayCurrent,maxPayCurrent,string.Empty, MainViewModel._configDataStore.CruiseNameBox));
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
                "WinchDAC", // Previously "Armstrong CAST 6",
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
                    while ((text = stream.ReadLine()!) != null)
                    {
                        lines.Add(text);
                    }
                    foreach (var line in lines)
                    {
                        int delim = line.IndexOf(",");
                        if (line.Substring(0, delim) == "Minimum Tension")
                        {
                            parseData.MinTension = line.Substring(delim + 1);
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
                return string.Empty;
            }
        }
    }
}