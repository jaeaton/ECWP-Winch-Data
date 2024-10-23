namespace ViewModels
{
    public class ProcessDataWireHistoryViewModel
    {
        //Put history data in data store
        //Insert data by date
        //public static void InsertData(ConfigDataStore _config)
        //{

        //}
        //Insert Event
        public static void InsertEvent(ConfigDataStore _config) 
        { 
            WireLogModel _event = new WireLogModel();
            _event.EventDate = _config.WireLogEventDate;//.ToString("yyyy/MM/dd");
            _event.EventType = _config.WireLogEventSelection;
            _event.InstalledTensionMemberLength = _config.CurrentWinch.AvailableLength.ToString();
            _event.CutBackAmount = _config.WireLogEventCutBack;
            _event.Notes = _config.WireLogEventNotes;

            foreach(var model in ProcessDataViewModel.ParseData.WireLog.Select((value, i) => new { i, value }))
            {
                //Compare dates
                if (_event.EventDate < model.value.EventDate)
                {
                    ProcessDataViewModel.ParseData.WireLog.Insert(model.i, _event);
                    break;
                }
            }
        }
        
        //Send history data to excel viewmodel
        public static void WriteData()
        {
            ConfigDataStore _config = MainViewModel._configDataStore;
            ExcelViewModel.SetWireLogFileName(_config.CurrentWinch);
            ExcelViewModel.ClearData();
            foreach(WireLogModel _dataPoint in ProcessDataViewModel.ParseData.WireLog)
            {
                if (_dataPoint.EventType != "Cast")
                {
                    ExcelViewModel.AddEvent(_dataPoint);
                }
                else
                {
                    ExcelViewModel.AddCast(_dataPoint);
                }
            }
        }
    }
}
