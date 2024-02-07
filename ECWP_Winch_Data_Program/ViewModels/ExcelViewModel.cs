//using DocumentFormat.OpenXml.Drawing;

namespace ViewModels
{
    public class ExcelViewModel 
    {
        //public void AddData(WinchModel winch, DataPointModel dataMaxTension, DataPointModel dataMaxPayout, ConfigDataStore config)
        public static void AddData( DataPointModel dataMaxTension, DataPointModel dataMaxPayout)
        {
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

            //Cruise Number
            ws.Cell($"A{CurrentRow}").Value = "Cruise";//$"{config.CruiseNameBox}";
            //Date
            ws.Cell($"B{CurrentRow}").Value = dataMaxTension.Date.ToString();
            //Cast number
            ws.Cell($"C{CurrentRow}").Value = "Cast Number";// $"{winch.CastNumber}";
            //Total Length of Cable
            ws.Cell($"D{CurrentRow}").Value = "Length";//$"{winch.InstalledLength}";
            //Maximum Tension
            ws.Cell($"E{CurrentRow}").Value = dataMaxTension.Tension;
            //Wire Out at Max Tension
            ws.Cell($"F{CurrentRow}").Value = dataMaxTension.Payout;
            //Wire on drum at Max Tension
            //double? WireIn = winch.InstalledLength - Convert.ToDouble(dataMaxTension.Payout);
            //ws.Cell($"G{CurrentRow}").Value = $"{WireIn}";
            //Maximum Wire Out
            ws.Cell($"H{CurrentRow}").Value = dataMaxPayout.Payout;
            wb.Save();
        }

        public static void NewWorkbook(string winch/*WinchModel winch*/)
        {
            // Creating a new workbook
            //var wb = new XLWorkbook($"{winch.MaxWireLogName}.xslx");
            var wb = new XLWorkbook();
            //Adding a worksheet
            var ws = wb.Worksheets.Add("Log");
            
            //Adding text
            //Title
            ws.Cell("A1").Value = "Wire Log";

            //Add Header
            ws.Cell("A2").Value = "Tension Member Identifier";
            ws.Cell("E2").Value = "Tension Member Identifier";
            ws.Cell("A3").Value = "Winch Name";
            ws.Cell("E3").Value = "Winch Name";//$"{winch.WinchName}";
            ws.Cell("A4").Value = "Winch Model";
            ws.Cell("E4").Value = "Winch Model";//$"{winch.WinchModelName}";
            ws.Cell("A5").Value = "Winch Manufacturer";
            ws.Cell("E5").Value = "Winch Manufacturer"; //$"{winch.WinchManufacturer}";
            ws.Cell("A6").Value = "Ship";
            ws.Cell("E6").Value = "Ship";//$"{MainViewModel._configDataStore.ShipName}";

            //Add Column Headings
            ws.Cell("A24").Value = "Cruise Number";
            ws.Cell("B24").Value = "Date";
            ws.Cell("C24").Value = "Cast Number";
            ws.Cell("D24").Value = "Cable Length (m)";
            ws.Cell("E24").Value = "Maximum Tension (lbf)";
            ws.Cell("F24").Value = "MT Wire Out (m)";
            ws.Cell("G24").Value = "MT Wire In (m)";
            ws.Cell("H24").Value = "Max Wire Out (m)";
            ws.Cell("I24").Value = "Notes";

            //Save File Cruise Name + Winch Name
            var path = $"{ProcessDataViewModel.ParseData.Directory}\\{winch}.xlsx";
            wb.SaveAs(path);
        }
    }
}
