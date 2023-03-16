namespace ViewModels
{
   
    public partial class ChartDataViewModel : ObservableObject
    {
        //private readonly LiveDataDataStore _liveData = DataHandlingViewModel._liveData;
        public  ObservableCollection<DateTimePoint> _observableValues = new ObservableCollection<DateTimePoint>();
        public  ObservableCollection<ISeries> Series { get; set; }
        //Uncomment to allow for windowing of plot
        public  ObservableCollection<DateTimePoint> _observableValuesZero = new ObservableCollection<DateTimePoint>();
        public  ObservableCollection<DateTimePoint> _observableValuesMax = new ObservableCollection<DateTimePoint>();
        public  IEnumerable<ICartesianAxis> XAxes { get; set; }
        public  IEnumerable<ICartesianAxis> YAxes { get; set; }
        
        //Color bars for warnings and alarms
        //public RectangularSection[] Sections { get; set; }  =
        //    {
        //        new RectangularSection
        //        {
        //            Yi = 150,
        //            Yj = 200,
        //            Fill = new SolidColorPaint { Color = SKColors.Yellow.WithAlpha(20) }
        //        },
        //        new RectangularSection
        //        {
        //            Yi = 200,
        //            Yj = 100000,
        //            Fill = new SolidColorPaint { Color = SKColors.Red.WithAlpha(20) }
        //        }
        //    };

        //public  int  i = 0;

        public ChartDataViewModel(ObservableCollection<DateTimePoint> _observableVals, ObservableCollection<ISeries> series, ObservableCollection<DateTimePoint> _observableValsZero, ObservableCollection<DateTimePoint> _observableValsMax, IEnumerable<ICartesianAxis> xAxes, IEnumerable<ICartesianAxis> yAxes)
        {
            _observableValues = _observableVals;
            Series = series;
            XAxes = xAxes;
            YAxes = yAxes;
            _observableValuesMax = _observableValsMax;
            _observableValuesZero = _observableValsZero;
        }
        public ChartDataViewModel()
        {
            

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Values = _observableValues,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.CornflowerBlue, 1)
                },                
                //Uncomment to add invisible series for windowing plot
                new LineSeries<DateTimePoint>
                {
                    Values = _observableValuesZero,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.Empty, 1),
                },
                new LineSeries<DateTimePoint>
                {
                    Values = _observableValuesMax,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.Red, 1),
                }
               
            };

            XAxes = new List<Axis>
            {

                new Axis
                {
                    
                    Labeler = value => DateTime.Now.ToString("HH:mm:ss"),
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
        public void AddData(DataPointModel latest, LiveDataDataStore live)
        {
            
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            if (latest.Date == null || latest.Time == null )
            {
                return;
            }
            if( DateTime.TryParseExact($"{ latest.Date } { latest.Time }","yyyyMMdd HH:mm:ss.fff", provider, styles, out DateTime dateTime))
            {
                //double.TryParse(latest.Tension, out double Tension);
                _observableValues.Add(new DateTimePoint { DateTime = dateTime, Value = latest.Tension });
                //uncomment for windowing of plot
                _observableValuesZero.Add(new DateTimePoint { DateTime = dateTime, Value = 0 });
                if (Double.TryParse(live.MaxTension, out double result))
                {
                    _observableValuesMax.Add(new DateTimePoint { DateTime = dateTime, Value = result });
                }

                //_observableValues.Add(new ObservablePoint { X = i++, Y = latest.Tension });

                if (_observableValues.Count > 500)
                {
                    _observableValues.RemoveAt(0);
                    _observableValuesMax.RemoveAt(0);
                }
                //uncomment for windowing of plot Keeps zero series and max series small
                if (_observableValuesZero.Count > 10)
                {
                    _observableValuesZero.RemoveAt(0);
                    //_observableValuesMax.RemoveAt(0);

                }
            }
        }
        public void ResetData()
        {
            //if( _observableValues.Count != 0)
            //{
            //    _observableValues.Clear();
            //}
            
        }
        //public static DateTime AxisGenerator()
        //{
        //    string value;
        //    try
        //    {
        //        new DateTime((long)value).ToString("HH:mm:ss")

        //    }
            

        //}
        
    }
}
