namespace ViewModels
{
    public class ExcelViewModel 
    {
        //public void AddData(WinchModel winch, DataPointModel dataMaxTension, DataPointModel dataMaxPayout, ConfigDataStore config)
        public static void AddCastData( DataPointModel dataMaxTension, DataPointModel dataMaxPayout, int cast)
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            //Should be set by WinchModel.Filename?
            string fileName = $"test";
            //Check for file
            if (!File.Exists($"{ProcessDataViewModel.ParseData.Directory}\\{fileName}.xlsx"))
            {
                NewWorkbook("test");
            }
            // Opening workbook
            var wb = new XLWorkbook($"{ProcessDataViewModel.ParseData.Directory}\\{fileName}.xlsx");

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
            ws.Cell($"C{CurrentRow}").Value = dataMaxTension.Date.ToString();
            //Cast number
            ws.Cell($"D{CurrentRow}").Value = cast;
            //Total Length of Cable
            ws.Cell($"E{CurrentRow}").Value = $"{_config.CurrentWinch.AvailableLength}";
            //Maximum Tension
            ws.Cell($"F{CurrentRow}").Value = dataMaxTension.Tension;
            //Wire Out at Max Tension
            ws.Cell($"G{CurrentRow}").Value = dataMaxTension.Payout;
            //Wire on drum at Max Tension
            //double? WireIn = winch.InstalledLength - Convert.ToDouble(dataMaxTension.Payout);
            //ws.Cell($"H{CurrentRow}").Value = $"{WireIn}";
            //Maximum Wire Out
            ws.Cell($"I{CurrentRow}").Value = dataMaxPayout.Payout;
            //Notes

            wb.Save();
        }

        public static void AddEvent()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            //Should be set by WinchModel.Filename?
            string fileName = $"test";
            //Check for file
            if (!File.Exists($"{ProcessDataViewModel.ParseData.Directory}\\{fileName}.xlsx"))
            {
                NewWorkbook("test");
            }
            // Opening workbook
            var wb = new XLWorkbook($"{ProcessDataViewModel.ParseData.Directory}\\{fileName}.xlsx");
            //Selecting a worksheet
            var ws = wb.Worksheets.Worksheet("Log");

            //Get last row
            int LastRow = ws.LastRowUsed().RowNumber();
            //Set Current Row
            int CurrentRow = LastRow + 1;

            //Event Type
            ws.Cell($"A{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventSelection}";
            //Date
            ws.Cell($"C{CurrentRow}").Value = MainViewModel._configDataStore.WireLogEventDate.ToString();
            //Wire Length
            ws.Cell($"E{CurrentRow}").Value = $"{MainViewModel._configDataStore.CurrentWinch.AvailableLength}";
            //Cutback Length
            ws.Cell($"I{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventCutBack}";
            //Notes
            ws.Cell($"J{CurrentRow}").Value = $"{MainViewModel._configDataStore.WireLogEventNotes}";

        }
        public static void NewWorkbook(string winch/*WinchModel winch*/)
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            // Creating a new workbook
            var wb = new XLWorkbook();
            //Adding a worksheet
            var ws = wb.Worksheets.Add("Log");
            
            //Adding text
            //Title
            ws.Cell("A1").Value = "Wire Log";

            //Add Header
            ws.Cell("A2").Value = "Tension Member Identifier";
            ws.Cell("E2").Value = $"{_config.CurrentWinch.TensionMemberNSFID}";
            ws.Cell("A3").Value = "Winch Name";
            ws.Cell("E3").Value = $"{_config.CurrentWinch.WinchName}";//$"{winch.WinchName}";
            ws.Cell("A4").Value = "Winch Model";
            ws.Cell("E4").Value = $"{_config.CurrentWinch.WinchModelName}";//$"{winch.WinchModelName}";
            ws.Cell("A5").Value = "Winch Manufacturer";
            ws.Cell("E5").Value = $"{_config.CurrentWinch.WinchManufacturer}"; //$"{winch.WinchManufacturer}";
            ws.Cell("A6").Value = "Ship";
            ws.Cell("E6").Value = $"{_config.ShipName}";//$"{MainViewModel._configDataStore.ShipName}";

            //Add Column Headings
            ws.Cell("A24").Value = "Event Type";
            ws.Cell("B24").Value = "Cruise Number";
            ws.Cell("C24").Value = "Date";
            ws.Cell("D24").Value = "Cast Number";
            ws.Cell("E24").Value = "Cable Length (m)";
            ws.Cell("F24").Value = "Maximum Tension (lbf)";
            ws.Cell("G24").Value = "MT Wire Out (m)";
            ws.Cell("H24").Value = "MT Wire In (m)";
            ws.Cell("I24").Value = "Max Wire Out (m)";
            ws.Cell("J24").Value = "Notes";

            //Save File Cruise Name + Winch Name
            var path = $"{ProcessDataViewModel.ParseData.Directory}\\{winch}.xlsx";
            wb.SaveAs(path);
        }
    }
}
