namespace ViewModels
{
    public partial class ProcessDataChartViewModel : ObservableObject
    {
        public ObservableCollection<DateTimePoint> _observableValues = new ObservableCollection<DateTimePoint>();
        public ObservableCollection<DateTimePoint> _observableStore = new ObservableCollection<DateTimePoint>();
        public ObservableCollection<ISeries> Series { get; set; } = new();
        //Uncomment to allow for windowing of plot
        //public ObservableCollection<DateTimePoint> _observableValuesZero = new ObservableCollection<DateTimePoint>();
        //public ObservableCollection<DateTimePoint> _observableValuesMax = new ObservableCollection<DateTimePoint>();
        public IEnumerable<ICartesianAxis> XAxes { get; set; } 
        public IEnumerable<ICartesianAxis> YAxes { get; set; }
       // public RectangularSection[]? Sections { get; set; }
        public ProcessDataChartViewModel(WinchModel winchModel)
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
                //    Stroke = new SolidColorPaint(SKColors.Red, 1),
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

        //public  int  i = 0;

        public ProcessDataChartViewModel(ObservableCollection<DateTimePoint> _observableVals, ObservableCollection<ISeries> series, IEnumerable<ICartesianAxis> xAxes, IEnumerable<ICartesianAxis> yAxes)
        {
            _observableValues = _observableVals;
            Series = series;
            XAxes = xAxes;
            YAxes = yAxes;
            //_observableValuesMax = _observableValsMax;
            //_observableValuesZero = _observableValsZero;
            //Sections = sections;
        }
        public ProcessDataChartViewModel()
        {

            //Sections = new RectangularSection[]
            //{
            //     new RectangularSection
            //     {
            //            Yi = 5,
            //            Yj = 7,
            //            Fill = new SolidColorPaint { Color = SKColors.Yellow.WithAlpha(20) }
            //        },
            //      new RectangularSection
            //        {
            //            Yi = 7,
            //            Yj = 10,
            //            Fill = new SolidColorPaint { Color = SKColors.Red.WithAlpha(20) }
            //        }
            //};

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Tension",
                    Values = _observableValues,
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    LineSmoothness = 0,
                    Stroke = new SolidColorPaint(SKColors.CornflowerBlue, 1)
                },                
                //Uncomment to add invisible series for windowing plot
                //new LineSeries<DateTimePoint>
                //{
                //    Values = _observableValuesZero,
                //    Fill = null,
                //    GeometryFill = null,
                //    GeometryStroke = null,
                //    GeometrySize = 0,
                //    LineSmoothness = 0,
                //    Stroke = new SolidColorPaint(SKColors.Empty, 1),
                //},
                //new LineSeries<DateTimePoint>
                //{
                //    Name = "Max Tension",
                //    Values = _observableValuesMax,
                //    Fill = null,
                //    GeometryFill = null,
                //    GeometryStroke = null,
                //    GeometrySize = 0,
                //    LineSmoothness = 0,
                //    Stroke = new SolidColorPaint(SKColors.Red, 1),
                //}

            };

            XAxes = new List<Axis>
            {

                new Axis
                {

                    Labeler = value => new DateTime((long) value).ToString("yy/MM/dd HH:mm:ss"),
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
        public void PlotData()
        {
            lock (ProcessDataViewModel.ParseData.Sync)
            {
                _observableValues = _observableStore;
                
            }
        }
        public void AddData(DataPointModel latest)
        {
            bool dateTimeSet = false;
            DateTime dateTime = new DateTime();
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            if (latest.DateAndTime != new DateTime())
            {
                dateTime = latest.DateAndTime;
                dateTimeSet = true;
            }
            else if (latest.Date == string.Empty || latest.Time == string.Empty || latest.Tension < 0)
            {
                return;
            }
            else if (DateTime.TryParseExact($"{latest.Date} {latest.Time}", "yyyy/MM/dd HH:mm:ss.fff", provider, styles, out dateTime))
            { 
                dateTimeSet = true;
            }
            if (dateTimeSet)
            {

                if (double.TryParse(latest.Payout.ToString(), out double Tension)) 
                {
                    DateTimePoint point = new DateTimePoint { DateTime = dateTime, Value = Tension };
                    //_observableStore.Add(point);
                    lock (ProcessDataViewModel.ParseData.Sync)
                    {
                        _observableValues.Add(point);

                    }

                };
                
                //uncomment for windowing of plot
                //_observableValuesZero.Add(new DateTimePoint { DateTime = dateTime, Value = 0 });
                //if (Double.TryParse(live.MaxTension, out double result))
                //{
                //    _observableValuesMax.Add(new DateTimePoint { DateTime = dateTime, Value = result });
                //}
                //else
                //{
                //    _observableValuesMax.Add(new DateTimePoint { DateTime = dateTime, Value = 0 });
                //}
                //TimeSpan span = _observableValues.Last().DateTime - _observableValues.First().DateTime;
                //if (TimeSpan.TryParse($"00:00:{chartLength}", out TimeSpan chartTime))
                //{
                //    //_observableValues.Add(new ObservablePoint { X = i++, Y = latest.Tension });
                //    if (span.Seconds > chartTime.Seconds)
                //    {
                //        _observableValues.RemoveAt(0);
                //        _observableValuesMax.RemoveAt(0);
                //    }
                //}
                //else if (_observableValues.Count > 500)
                //{
                //    _observableValues.RemoveAt(0);
                //    _observableValuesMax.RemoveAt(0);
                //}
                //uncomment for windowing of plot Keeps zero series and max series small
                //if (_observableValuesZero.Count > 10)
                //{
                //    _observableValuesZero.RemoveAt(0);
                //    //_observableValuesMax.RemoveAt(0);

                //}
            }
        }

    }
}
