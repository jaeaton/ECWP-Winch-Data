using System.Collections.Generic;

namespace Models
{
    public class ProcessCastDataModel : ICloneable
    {
        public ProcessCastDataModel()
        { }

        public ProcessCastDataModel(int castNumber, List<ProcessPointDataModel> processPoints, string maxTension, string maxPayout)
        {
            CastNumber = castNumber;
            ProcessPoints = processPoints;
            MaxTension = maxTension;
            MaxPayout = maxPayout;
        }

        public int CastNumber { get; set; }
        public string MaxPayout { get; set; } = string.Empty;
        public string MaxTension { get; set; } = string.Empty;
        public List<ProcessPointDataModel> ProcessPoints { get; set; } = new();

        public object Clone()
        {
            return new ProcessCastDataModel(CastNumber, ProcessPoints, MaxTension, MaxPayout);
        }
    }
}