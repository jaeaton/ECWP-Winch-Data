namespace ViewModel
{
    public class ChartDataViewModel
    {
        //public static ObservableCollection<ObservablePoint> _observableValues = new ObservableCollection<ObservablePoint>();
        public static ObservableCollection<DateTimePoint> _observableValues = new ObservableCollection<DateTimePoint>();
        public static ObservableCollection<ISeries> Series { get; set; }
        public static IEnumerable<ICartesianAxis> XAxes { get; set; }
        public static IEnumerable<ICartesianAxis> YAxes { get; set; }

        public static int  i = 0;
        static ChartDataViewModel()
        {
            //LiveDataDataStore _liveData = DataHandlingViewModel._liveData;
            //_liveData.Series = new ObservableCollection<ISeries>
            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                //new LineSeries<ObservablePoint>
                {
                    Values = _observableValues,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.CornflowerBlue, 1),
                                       
                }
            };
            XAxes = new List<Axis>
            {
                new Axis
                {
                    Labeler = value => new DateTime((long) value).ToString("HH:mm:ss"),
                    LabelsRotation = -30,
                    TextSize = 14,
                    
                }

            };
            YAxes = new List<Axis>
            {
                new Axis
                {
                    //MinLimit = 0,
                    //MaxLimit = (DataHandlingViewModel.maxData.MaxTension.Tension + 100)
                }
            };
        }
        public static void AddData(DataPointModel latest)
        {
            
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            if(latest.Date == null || latest.Time == null )
            {
                return;
            }
            DateTime dateTime = DateTime.ParseExact($"{ latest.Date } { latest.Time }","yyyyMMdd HH:mm:ss.fff", provider);
            //double.TryParse(latest.Tension, out double Tension);
            _observableValues.Add(new DateTimePoint { DateTime = dateTime, Value = latest.Tension });
            //_observableValues.Add(new ObservablePoint { X = i++, Y = latest.Tension });
            if (_observableValues.Count > 500)
            {
                _observableValues.RemoveAt(0);
            }
        }

    }
}
