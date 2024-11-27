using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ViewModels
{
    public class ExcelViewModel 
    {
       //Direct write method -- old 
        public static void AddCastData( DataPointModel dataMaxTension, DataPointModel dataMaxPayout, int cast, WinchModel winch)
        {
            //Check Date for data point
            string date = string.Empty;
            if (dataMaxTension.Date != string.Empty)
            {
                date = dataMaxTension.Date;
            }
            else if (dataMaxTension.DateAndTime != new DateTime())
            {
                date = dataMaxTension.DateAndTime.ToString("yyyy/MM/dd");
            }
            else { date = "No Date"; }
            ConfigDataStore _config = MainViewModel._configDataStore;
            winch = (WinchModel)SetWireLogFileName(winch);
            //Load Filename
            string fileName = $"{winch.WirePoolWireLogName}";
            
            if (winch.WinchDirectory == string.Empty)
            {
                NoDirectory(winch);
                return;
            }

            //Check for file
            if (!File.Exists($"{winch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName, winch);
            }
            // Opening workbook
            var wb = new XLWorkbook($"{winch.WinchDirectory}\\{fileName}");

            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");
            int LastRow;
            //Get last row
            if (ws.LastRowUsed() != null)
            {
                LastRow = ws.LastRowUsed().RowNumber();
            }
            else
            {
                LastRow = 12;
            }
            //Set Current Row
            int CurrentRow = LastRow + 1;

            //Event Type
            ws.Cell($"A{CurrentRow}").Value = $"Cast";
            //Cruise Number
            ws.Cell($"B{CurrentRow}").Value = $"{_config.CruiseNameBox}";
            //Date
            ws.Cell($"C{CurrentRow}").Value = date;
            //Cast number
            ws.Cell($"D{CurrentRow}").Value = cast;
            //Total Length of Cable
            ws.Cell($"E{CurrentRow}").Value = winch.AvailableLength;
            //Maximum Tension
            ws.Cell($"F{CurrentRow}").Value = dataMaxTension.Tension;
            //Wire Out at Max Tension
            ws.Cell($"G{CurrentRow}").Value = dataMaxTension.Payout;
            //Wire on drum at Max Tension
            float WireIn = winch.InstalledLength - dataMaxTension.Payout;
            ws.Cell($"H{CurrentRow}").Value = WireIn;
            //ws.Cell($"H{CurrentRow}").Style.NumberFormat.Format = ;
            //Maximum Wire Out
            ws.Cell($"I{CurrentRow}").Value = dataMaxPayout.Payout;
            //Notes
            if (dataMaxTension.Tension > float.Parse(winch.TensionWarningLevel)) 
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Warning";
            }
            if (dataMaxTension.Tension > float.Parse(winch.TensionAlarmLevel))
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Alarm";
            }
            //Borders
            ws.Range($"A{CurrentRow}:J{CurrentRow}").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            wb.Save();
        }
        //Clear data
        public static void ClearData()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            _config.CurrentWinch = (WinchModel)SetWireLogFileName(_config.CurrentWinch);
            //Set filename
            string fileName = $"{_config.CurrentWinch.WirePoolWireLogName}";
            if (_config.CurrentWinch.WinchDirectory == string.Empty)
            {
                NoDirectory(_config.CurrentWinch);
                return;
            }
            //Check for file
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName, _config.CurrentWinch);
            }
            // Opening workbook
            var wb = new XLWorkbook($"{_config.CurrentWinch.WinchDirectory}\\{fileName}");
            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();

            while (LastRow > 22) 
            { 
                ws.Row(LastRow).Delete();
                LastRow--;
            }
        }
        //New data write method
        public static void AddCast(WireLogModel dataPoint)
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            _config.CurrentWinch = (WinchModel)SetWireLogFileName(_config.CurrentWinch);
            //Set filename
            string fileName = $"{_config.CurrentWinch.WirePoolWireLogName}";
            if (_config.CurrentWinch.WinchDirectory == string.Empty)
            {
                NoDirectory(_config.CurrentWinch);
                return;
            }
            //Check for file
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName, _config.CurrentWinch);
            }
            // Opening workbook
            var wb = new XLWorkbook($"{_config.CurrentWinch.WinchDirectory}\\{fileName}");
            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();
            //Set Current Row
            int CurrentRow = LastRow + 1;
            //Event Type
            ws.Cell($"A{CurrentRow}").Value = $"Cast";
            //Cruise Number
            ws.Cell($"B{CurrentRow}").Value = $"{_config.CruiseNameBox}";
            //Date
            ws.Cell($"C{CurrentRow}").Value = dataPoint.EventDate;
            //Cast number
            ws.Cell($"D{CurrentRow}").Value = dataPoint.CastNumber;
            //Total Length of Cable
            ws.Cell($"E{CurrentRow}").Value = dataPoint.InstalledTensionMemberLength;
            //Maximum Tension
            ws.Cell($"F{CurrentRow}").Value = dataPoint.MaxTension;
            //Wire Out at Max Tension
            ws.Cell($"G{CurrentRow}").Value = dataPoint.MaxTensionWireOut;
            //Wire on drum at Max Tension
            //float WireIn = winch.InstalledLength - dataMaxTension.Payout;
            ws.Cell($"H{CurrentRow}").Value = dataPoint.MaxTensionWireIn;
            //ws.Cell($"H{CurrentRow}").Style.NumberFormat.Format = ;
            //Maximum Wire Out
            ws.Cell($"I{CurrentRow}").Value = dataPoint.MaxWireOut; 
            //Notes
            if (float.Parse(dataPoint.MaxTension) > float.Parse(_config.CurrentWinch.TensionWarningLevel))
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Warning";
            }
            else if (float.Parse(dataPoint.MaxTension) > float.Parse(_config.CurrentWinch.TensionAlarmLevel))
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Alarm";
            }
            //Borders
            ws.Range($"A{CurrentRow}:J{CurrentRow}").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            wb.Save();
        }

        public static void AddEvent(WireLogModel dataPoint)
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            _config.CurrentWinch = (WinchModel)SetWireLogFileName(_config.CurrentWinch);
            //Set filename
            string fileName = $"{_config.CurrentWinch.WirePoolWireLogName}";
            if (_config.CurrentWinch.WinchDirectory == string.Empty)
            {
                NoDirectory(_config.CurrentWinch);
                return;
            }
            //Check for file
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName, _config.CurrentWinch);
            }
            // Opening workbook
            var wb = new XLWorkbook($"{_config.CurrentWinch.WinchDirectory}\\{fileName}");
            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();
            //Set Current Row
            int CurrentRow = LastRow + 1;

            //Event Type
            ws.Cell($"A{CurrentRow}").Value = dataPoint.EventType; //MainViewModel._configDataStore.WireLogEventSelection;
            //Date
            ws.Cell($"C{CurrentRow}").Value = dataPoint.EventDate.ToString("yyyy/MM/dd");//MainViewModel._configDataStore.WireLogEventDate.ToString("yyyy/MM/dd");
            //Wire Length
            ws.Cell($"E{CurrentRow}").Value = dataPoint.InstalledTensionMemberLength;//MainViewModel._configDataStore.CurrentWinch.AvailableLength;
            //Cutback Length
            if (dataPoint.EventType == "Cut Back")
            {
                ws.Cell($"I{CurrentRow}").Value = dataPoint.CutBackAmount; //float.Parse(MainViewModel._configDataStore.WireLogEventCutBack);
            }

            //Notes
            ws.Cell($"J{CurrentRow}").Value = dataPoint.Notes;//MainViewModel._configDataStore.WireLogEventNotes;
            ws.Range($"J{CurrentRow}:M{CurrentRow}").Merge();
            //Borders
            ws.Range($"A{CurrentRow}:J{CurrentRow}").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            wb.Save();
        }

        public static void NewWorkbook(string fileName ,WinchModel winch)
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            // Creating a new workbook
            var wb = new XLWorkbook();
            //Adding a worksheet
            var ws = wb.Worksheets.Add("Log");
            
            //Set sheet to landscape
            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
            //Set Margins
            ws.PageSetup.Margins.Top = 0.25;
            ws.PageSetup.Margins.Bottom = 0.25;
            ws.PageSetup.Margins.Left = 0.25;
            ws.PageSetup.Margins.Right = 0.25;

            //Adding text
            //Title
            ws.Cell("A1").Value = "UNOLS Wire Log";

            //Add Headers
            //Tension Member Data
            ws.Cell("A2").Value = "Tension Member NSF ID";
            ws.Cell("C2").Value = $"{winch.TensionMemberNSFID}";
            ws.Cell("A3").Value = "Tension Member Part Number";
            ws.Cell("C3").Value = $"{winch.TensionMemberPartNumber}";
            ws.Cell("A4").Value = "Tension Member Manufacturer";
            ws.Cell("C4").Value = $"{winch.TensionMemberManufacturer}";
            ws.Cell("A5").Value = "Ship";
            ws.Cell("C5").Value = $"{_config.ShipName}";
            //Winch Data
            ws.Cell("F2").Value = "Winch Name";
            ws.Cell("H2").Value = $"{winch.WinchName}";
            ws.Cell("F3").Value = "Winch Model";
            ws.Cell("H3").Value = $"{winch.WinchModelName}";
            ws.Cell("F4").Value = "Winch Manufacturer";
            ws.Cell("H4").Value = $"{winch.WinchManufacturer}"; 
            ws.Cell("F5").Value = "Serial Number";
            ws.Cell("H5").Value = $"{winch.WinchSerialNumber}";
            

            //Merge Header Cells
            ws.Range("A1:C1").Merge();
            ws.Range("A2:B2").Merge();
            ws.Range("A3:B3").Merge();
            ws.Range("A4:B4").Merge();
            ws.Range("A5:B5").Merge();
            ws.Range("C2:E2").Merge();
            ws.Range("C3:E3").Merge();
            ws.Range("C4:E4").Merge();
            ws.Range("C5:E5").Merge();
            
            ws.Range("F2:G2").Merge();
            ws.Range("F3:G3").Merge();
            ws.Range("F4:G4").Merge();
            ws.Range("F5:G5").Merge();
            ws.Range("H2:I2").Merge();
            ws.Range("H3:I3").Merge();
            ws.Range("H4:I4").Merge();
            ws.Range("H5:I5").Merge();

            //Merge Image Cell
            ws.Range("B6:I20").Merge();
            if (winch.SheaveTrainPath == string.Empty)
            {
                ws.Cell("B6").Value = "Insert Sheave Train Diagram";
            }
            else
            {
                ws.AddPicture(winch.SheaveTrainPath).MoveTo(ws.Cell("B6"),ws.Cell("I20"));
            }
            
            ws.Cell("B6").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            ws.Cell("B6").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            //Set Column widths
            ws.Column(1).Width = 11;
            ws.Column(2).Width = 14;
            ws.Column(3).Width = 12;
            ws.Column(4).Width = 5;
            ws.Column(5).Width = 9;
            ws.Column(6).Width = 11;
            ws.Column(7).Width = 9;
            ws.Column(8).Width = 9;
            ws.Column(9).Width = 9;
            ws.Column(10).Width = 10;

            //Set Row Heights
            ws.Row(22).Height = 30;

            //Set Text Alignment
            ws.Row(22).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);

            //Wrap text in cell
            ws.Row(22).Style.Alignment.WrapText = true;

            //Add Column Headings
            ws.Cell("A22").Value = "Event Type";
            ws.Cell("B22").Value = "Cruise #";
            ws.Cell("C22").Value = "Date";
            ws.Cell("D22").Value = "Cast #";
            ws.Cell("E22").Value = "Cable Length (m)";
            ws.Cell("F22").Value = "Maximum Tension (lbf)";
            ws.Cell("G22").Value = "MT Wire Out (m)";
            ws.Cell("H22").Value = "MT Wire In (m)";
            ws.Cell("I22").Value = "Max Wire Out (m)";
            ws.Cell("J22").Value = "Notes";
            ws.Range("J22:M22").Merge();
            ws.Range("A22:J22").Style.Border.SetBottomBorder(XLBorderStyleValues.Double);
            
            //Freeze the headings in place
            ws.SheetView.FreezeRows(22);

            //Save File Cruise Name + Winch Name
            var path = $"{winch.WinchDirectory}\\{fileName}";
            wb.SaveAs(path);
        }

        public static object SetWireLogFileName(WinchModel winch)
        {
            
            DateTime dateTime = DateTime.Now;
            winch.WirePoolWireLogName = $"{dateTime.ToString("yyyy")}_{winch.WinchName}_Wire_Log.xlsx";
            return winch;
        }

        public async static void NoDirectory(WinchModel winch)
        {
            await MessageBoxViewModel.DisplayMessage(
                                    $"{winch.WinchName}\n" +
                                    $"Log directory is not set. \nSet in the winch configuration");
        }

        public async static void ReadLog(WinchModel winch)
        {
            winch = (WinchModel)SetWireLogFileName(winch);
            if (winch.WinchDirectory == string.Empty || winch.WirePoolWireLogName == string.Empty)
            {
                return;
            }
            string fileName = $"{winch.WirePoolWireLogName}";
            if (!File.Exists($"{winch.WinchDirectory}\\{fileName}"))
            {
                return;
            }
            

            // Opening workbook
            var wb = new XLWorkbook($"{winch.WinchDirectory}\\{fileName}");

            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();

            //WireLogModel wireLog = new WireLogModel();
            ParseDataStore dataStore = ProcessDataViewModel.ParseData;
            //Clear current data
            dataStore.WireLog.Clear();
            //Start at row 23 and read through last row
            for (int i = 23; i <= LastRow; i++)
            {
                WireLogModel wireLog = new WireLogModel();
                string sVal = string.Empty;
                float fVal = 0;
                DateTime dtVal = new();
                int iVal = 0;
                //Write data to data array ParseDataStore.WireLog
                //Event Type
                if(ws.Cell($"A{i}").TryGetValue<string>(out sVal))
                {
                    wireLog.EventType = sVal;
                }
                
                //Cruise Number
                if(ws.Cell($"B{i}").TryGetValue<string>(out sVal))
                {
                    wireLog.CruiseNumber = sVal;
                }
                //Date
                if(ws.Cell($"C{i}").TryGetValue<string>(out sVal))
                {
                    wireLog.EventDate = DateTime.Parse(sVal);
                }
                //Cast number
                if(ws.Cell($"D{i}").TryGetValue<int>(out iVal))
                {
                    wireLog.CastNumber = iVal.ToString();
                }
                //Total Length of Cable
                if(ws.Cell($"E{i}").TryGetValue<float>(out fVal))
                {
                    wireLog.InstalledTensionMemberLength = fVal.ToString();
                }
                //Maximum Tension
                if(ws.Cell($"F{i}").TryGetValue<float>(out fVal))
                {
                    wireLog.MaxTension = fVal.ToString();
                }
                //Wire Out at Max Tension
                if(ws.Cell($"G{i}").TryGetValue<float>(out fVal))
                {
                    wireLog.MaxTensionWireOut = fVal.ToString();
                }
                //Wire on drum at Max Tension
                if(ws.Cell($"H{i}").TryGetValue<float>(out fVal))
                {
                    wireLog.MaxTensionWireIn = fVal.ToString();
                }
                //Cut back amount
                if (wireLog.EventType == "Cut Back")
                {
                    if(ws.Cell($"I{i}").TryGetValue<string>(out sVal))
                    {
                        wireLog.CutBackAmount = sVal;
                    }
                }
                else
                {
                    //Maximum Wire Out
                    if(ws.Cell($"I{i}").TryGetValue<float>(out fVal))
                    {
                        wireLog.MaxWireOut = fVal.ToString();
                    }
                }
                //Notes
                if(ws.Cell($"J{i}").TryGetValue<string>(out sVal))
                {
                    wireLog.Notes = sVal;
                }
                dataStore.WireLog.Add(wireLog.ShallowCopy());
            }

        }
    }
}
