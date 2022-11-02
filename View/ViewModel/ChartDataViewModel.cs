using System.Globalization;

namespace ViewModel
{
    public class ChartDataViewModel
    {
        private readonly LiveDataDataStore _liveData = DataHandlingViewModel._liveData;
        public static ObservableCollection<DateTimePoint> _observableValues = new ObservableCollection<DateTimePoint>();
        public static ObservableCollection<ISeries> Series { get; set; }
        //Uncomment to allow for windowing of plot
        //public static ObservableCollection<DateTimePoint> _observableValuesZero = new ObservableCollection<DateTimePoint>();
        //public static ObservableCollection<DateTimePoint> _observableValuesMax = new ObservableCollection<DateTimePoint>();
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
                {
                    Values = _observableValues,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.CornflowerBlue, 1)
                }//,
                //Uncomment to add invisible series for windowing plot
                //new LineSeries<DateTimePoint>
                //{
                //    Values = _observableValuesZero,
                //    Fill = null,
                //    GeometrySize = 0,
                //    LineSmoothness = 0,
                //    Stroke = new SolidColorPaint(SKColors.Empty, 1),
                //},
                //new LineSeries<DateTimePoint>
                //{
                //    Values = _observableValuesMax,
                //    Fill = null,
                //    GeometrySize = 0,
                //    LineSmoothness = 0,
                //    Stroke = new SolidColorPaint(SKColors.Empty, 1),
                //}

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
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray),
                    //Comment out for Auto Scaling of lowest value shown
                    MinLimit = 0,
                    //MaxLimit = (DataHandlingViewModel.maxData.MaxTension.Tension + 100)
                    //MaxLimit = 6000
                }
            };
        }
        public static void AddData(DataPointModel latest)
        {
            
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            if (latest.Date == null || latest.Time == null )
            {
                return;
            }
            if( DateTime.TryParseExact($"{ latest.Date } { latest.Time }","yyyyMMdd HH:mm:ss.fff", provider, styles, out DateTime dateTime))
            {
                _observableValues.Add(new DateTimePoint { DateTime = dateTime, Value = latest.Tension });
            }
            //double.TryParse(latest.Tension, out double Tension);
            
            //uncomment for windowing of plot
            //_observableValuesZero.Add(new DateTimePoint { DateTime = dateTime, Value = 0 });
            //_observableValuesMax.Add(new DateTimePoint { DateTime = dateTime, Value = Double.Parse(DataHandlingViewModel._liveData.MaxTension)*1.05 });
            //_observableValues.Add(new ObservablePoint { X = i++, Y = latest.Tension });
            if (_observableValues.Count > 500)
            {
                _observableValues.RemoveAt(0);
                
            }
            //uncomment for windowing of plot Keeps zero series and max series small
            //if (_observableValuesZero.Count > 10)
            //{
            //    _observableValuesZero.RemoveAt(0);
            //    _observableValuesMax.RemoveAt(0);

            //}
        }
        public static void ResetData()
        {
            _observableValues.Clear();
        }
    }
}
