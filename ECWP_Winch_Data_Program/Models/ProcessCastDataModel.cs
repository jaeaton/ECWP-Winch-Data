using System.Collections.Generic;

namespace Models
{
    public class ProcessCastDataModel : ICloneable
    {
        public int CastNumber { get; set; }
        public List<ProcessPointDataModel> ProcessPoints { get; set; } = new();
        public string MaxTension { get; set; } = string.Empty;
        public string MaxPayout { get; set; } = string.Empty;

        public ProcessCastDataModel() { }
        public ProcessCastDataModel(int castNumber, List<ProcessPointDataModel> processPoints, string maxTension, string maxPayout)
        {
            CastNumber = castNumber;
            ProcessPoints = processPoints;
            MaxTension = maxTension;
            MaxPayout = maxPayout;
        }

        public object Clone()
        {
            return new ProcessCastDataModel(CastNumber, ProcessPoints, MaxTension, MaxPayout);
        }
    }
}
