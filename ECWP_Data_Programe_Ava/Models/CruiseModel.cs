namespace Models
{
    public class CruiseModel : ObservableObject
    {
        /// <summary>
        /// Name of the cruise as a string
        /// </summary>
        public string? CruiseName { get; set; }
        /// <summary>
        /// Number of casts on a cruise
        /// </summary>
        public int? CastNumber { get; set; }
        /// <summary>
        /// Has the cruise name been validated?
        /// </summary>
        public bool? CruiseValid { get; set; }
        public CruiseModel() { }
        public CruiseModel(string cruiseName, string castNumber)//, bool cruiseValid)
        {
            CruiseName = cruiseName;

            int castNumberValue = 1;
            int.TryParse(castNumber, out castNumberValue);
            CastNumber = castNumberValue;

            //CruiseValid = cruiseValid;
        }

        public CruiseModel(string cruiseName, string castNumber, bool cruiseValid)
        {
            CruiseName = cruiseName;

            int castNumberValue = 1;
            int.TryParse(castNumber, out castNumberValue);
            CastNumber = castNumberValue;

            CruiseValid = cruiseValid;
        }
    }
}
