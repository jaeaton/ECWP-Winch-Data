namespace Models
{
    public class ProcessPointDataModel : ICloneable
    {
        public int PointNumber { get; set; }
        public float Tension { get; set; }
        public float Payout { get; set; }

       public ProcessPointDataModel() { }
        public ProcessPointDataModel(int pointNumber, float tension, float payout)
        {
            PointNumber = pointNumber;
            Tension = tension;
            Payout = payout;
        }
        public object Clone()
        {
            return new ProcessPointDataModel(PointNumber, Tension, Payout);
        }
    }
}
