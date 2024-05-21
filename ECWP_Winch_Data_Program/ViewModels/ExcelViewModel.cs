using DocumentFormat.OpenXml.Drawing.Charts;

namespace ViewModels
{
    public class ExcelViewModel 
    {
        
        public static void AddCastData( DataPointModel dataMaxTension, DataPointModel dataMaxPayout, int cast)
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
            SetWireLogFileName();
            //Load Filename
            string fileName = $"{_config.CurrentWinch.WirePoolWireLogName}";
            
            if (_config.CurrentWinch.WinchDirectory == string.Empty)
            {
                NoDirectory();
                return;
            }

            //Check for file
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName);
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
            ws.Cell($"C{CurrentRow}").Value = date;
            //Cast number
            ws.Cell($"D{CurrentRow}").Value = cast;
            //Total Length of Cable
            ws.Cell($"E{CurrentRow}").Value = _config.CurrentWinch.AvailableLength;
            //Maximum Tension
            ws.Cell($"F{CurrentRow}").Value = dataMaxTension.Tension;
            //Wire Out at Max Tension
            ws.Cell($"G{CurrentRow}").Value = dataMaxTension.Payout;
            //Wire on drum at Max Tension
            float WireIn = _config.CurrentWinch.InstalledLength - dataMaxTension.Payout;
            ws.Cell($"H{CurrentRow}").Value = WireIn;
            //ws.Cell($"H{CurrentRow}").Style.NumberFormat.Format = ;
            //Maximum Wire Out
            ws.Cell($"I{CurrentRow}").Value = dataMaxPayout.Payout;
            //Notes
            if (dataMaxTension.Tension > float.Parse(_config.CurrentWinch.TensionWarningLevel)) 
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Warning";
            }
            if (dataMaxTension.Tension > float.Parse(_config.CurrentWinch.TensionAlarmLevel))
            {
                ws.Cell($"J{CurrentRow}").Value = "Tension Alarm";
            }
            //Borders
            ws.Range($"A{CurrentRow}:J{CurrentRow}").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            wb.Save();
        }

        public static void AddEvent()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            SetWireLogFileName();
            //Set filename
            string fileName = $"{_config.CurrentWinch.WirePoolWireLogName}";
            if (_config.CurrentWinch.WinchDirectory == string.Empty)
            {
                NoDirectory();
                return;
            }
            //Check for file
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}"))
            {
                NewWorkbook(fileName);
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
            ws.Cell($"A{CurrentRow}").Value = MainViewModel._configDataStore.WireLogEventSelection;
            //Date
            ws.Cell($"C{CurrentRow}").Value = MainViewModel._configDataStore.WireLogEventDate.ToString("yyyy/MM/dd");
            //Wire Length
            ws.Cell($"E{CurrentRow}").Value = MainViewModel._configDataStore.CurrentWinch.AvailableLength;
            //Cutback Length
            if (MainViewModel._configDataStore.WireLogEventSelection == "Cut Back")
            {
                ws.Cell($"I{CurrentRow}").Value = float.Parse(MainViewModel._configDataStore.WireLogEventCutBack);
            }
            
            //Notes
            ws.Cell($"J{CurrentRow}").Value = MainViewModel._configDataStore.WireLogEventNotes;
            ws.Range($"J{CurrentRow}:M{CurrentRow}").Merge();
            //Borders
            ws.Range($"A{CurrentRow}:J{CurrentRow}").Style.Border.SetInsideBorder(XLBorderStyleValues.Thin);
            wb.Save();
        }

        public static void NewWorkbook(string fileName/*WinchModel winch*/)
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
            ws.Cell("C2").Value = $"{_config.CurrentWinch.TensionMemberNSFID}";
            ws.Cell("A3").Value = "Tension Member Part Number";
            ws.Cell("C3").Value = $"{_config.CurrentWinch.TensionMemberPartNumber}";
            ws.Cell("A4").Value = "Tension Member Manufacturer";
            ws.Cell("C4").Value = $"{_config.CurrentWinch.TensionMemberManufacturer}";
            ws.Cell("A5").Value = "Ship";
            ws.Cell("C5").Value = $"{_config.ShipName}";
            //Winch Data
            ws.Cell("F2").Value = "Winch Name";
            ws.Cell("H2").Value = $"{_config.CurrentWinch.WinchName}";
            ws.Cell("F3").Value = "Winch Model";
            ws.Cell("H3").Value = $"{_config.CurrentWinch.WinchModelName}";
            ws.Cell("F4").Value = "Winch Manufacturer";
            ws.Cell("H4").Value = $"{_config.CurrentWinch.WinchManufacturer}"; 
            ws.Cell("F5").Value = "Serial Number";
            ws.Cell("H5").Value = $"{_config.CurrentWinch.WinchSerialNumber}";
            

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
            if (_config.CurrentWinch.SheaveTrainPath == string.Empty)
            {
                ws.Cell("B6").Value = "Insert Sheave Train Diagram";
            }
            else
            {
                ws.AddPicture(_config.CurrentWinch.SheaveTrainPath).MoveTo(ws.Cell("B6"),ws.Cell("I20"));
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
            var path = $"{_config.CurrentWinch.WinchDirectory}\\{fileName}";
            wb.SaveAs(path);
        }

        public static void SetWireLogFileName()
        {
            ConfigDataStore _confDataStore = MainViewModel._configDataStore;
            DateTime dateTime = DateTime.Now;
            _confDataStore.CurrentWinch.WirePoolWireLogName = $"{dateTime.ToString("yyyy")}_{_confDataStore.CurrentWinch.WinchName}_Wire_Log.xlsx";
        }

        public async static void NoDirectory()
        {
            await MessageBoxViewModel.DisplayMessage(
                                    $"{MainViewModel._configDataStore.CurrentWinch.WinchName}\n" +
                                    $"Log directory is not set. \nSet in the winch configuration");
        }
    }
}
