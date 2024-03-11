using DocumentFormat.OpenXml.Drawing.Charts;

namespace ViewModels
{
    public class ExcelViewModel 
    {
        //public void AddData(WinchModel winch, DataPointModel dataMaxTension, DataPointModel dataMaxPayout, ConfigDataStore config)
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
                date = dataMaxTension.DateAndTime.ToShortDateString();
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
            ws.Cell($"H{CurrentRow}").Value = $"{WireIn}";
            //ws.Cell($"H{CurrentRow}").Style.NumberFormat.Format = ;
            //Maximum Wire Out
            ws.Cell($"I{CurrentRow}").Value = dataMaxPayout.Payout;
            //Notes
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
            if (!File.Exists($"{_config.CurrentWinch.WinchDirectory}\\{fileName}.xlsx"))
            {
                NewWorkbook(fileName);
            }
            // Opening workbook
            var wb = new XLWorkbook($"{_config.CurrentWinch.WinchDirectory}\\{fileName}.xlsx");
            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();
            //Set Current Row
            int CurrentRow = LastRow + 1;

            //Event Type
            ws.Cell($"A{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventSelection}";
            //Date
            ws.Cell($"C{CurrentRow}").Value = MainViewModel._configDataStore.WireLogEventDate.ToString("yyyy/MM/dd");
            //Wire Length
            ws.Cell($"E{CurrentRow}").Value = $"{MainViewModel._configDataStore.CurrentWinch.AvailableLength}";
            //Cutback Length
            ws.Cell($"I{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventCutBack}";
            //Notes
            ws.Cell($"J{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventNotes}";
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
            ws.Cell("A1").Value = "Wire Log";

            //Add Header
            ws.Cell("A2").Value = "Tension Member Identifier";
            ws.Cell("D2").Value = $"{_config.CurrentWinch.TensionMemberNSFID}";
            ws.Cell("A3").Value = "Winch Name";
            ws.Cell("D3").Value = $"{_config.CurrentWinch.WinchName}";
            ws.Cell("A4").Value = "Winch Model";
            ws.Cell("D4").Value = $"{_config.CurrentWinch.WinchModelName}";
            ws.Cell("A5").Value = "Winch Manufacturer";
            ws.Cell("D5").Value = $"{_config.CurrentWinch.WinchManufacturer}"; 
            ws.Cell("A6").Value = "Ship";
            ws.Cell("D6").Value = $"{_config.ShipName}";

            //Merge Header Cells
            ws.Range("A1:C1").Merge();
            ws.Range("A2:C2").Merge();
            ws.Range("A3:C3").Merge();
            ws.Range("A4:C4").Merge();
            ws.Range("A5:C5").Merge();
            ws.Range("A6:C6").Merge();
            ws.Range("D2:F2").Merge();
            ws.Range("D3:F3").Merge();
            ws.Range("D4:F4").Merge();
            ws.Range("D5:F5").Merge();
            ws.Range("D6:F6").Merge();

            //Merge Image Cell
            ws.Range("B8:I22").Merge();
            ws.Cell("B8").Value = "Insert Sheave Train Diagram";
            ws.Cell("B8").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            ws.Cell("B8").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            //Set Column widths
            ws.Column(1).Width = 10;
            ws.Column(2).Width = 13;
            ws.Column(3).Width = 12;
            ws.Column(4).Width = 5;
            ws.Column(5).Width = 9;
            ws.Column(6).Width = 11;
            ws.Column(7).Width = 9;
            ws.Column(8).Width = 9;
            ws.Column(9).Width = 9;
            ws.Column(10).Width = 10;

            //Set Row Heights
            ws.Row(24).Height = 30;

            //Set Text Alignment
            ws.Row(24).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);

            //Wrap text in cell
            ws.Row(24).Style.Alignment.WrapText = true;

            //Add Column Headings
            ws.Cell("A24").Value = "Event Type";
            ws.Cell("B24").Value = "Cruise #";
            ws.Cell("C24").Value = "Date";
            ws.Cell("D24").Value = "Cast #";
            ws.Cell("E24").Value = "Cable Length (m)";
            ws.Cell("F24").Value = "Maximum Tension (lbf)";
            ws.Cell("G24").Value = "MT Wire Out (m)";
            ws.Cell("H24").Value = "MT Wire In (m)";
            ws.Cell("I24").Value = "Max Wire Out (m)";
            ws.Cell("J24").Value = "Notes";
            ws.Range("A24:J24").Style.Border.SetBottomBorder(XLBorderStyleValues.Double);
            
            //Freeze the headings in place
            ws.SheetView.FreezeRows(24);

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
