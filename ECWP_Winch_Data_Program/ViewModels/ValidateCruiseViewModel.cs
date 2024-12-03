namespace ViewModels
{
    public class ValidateCruiseViewModel
    {
        public static bool ValidateCastNumber(string castNumber)
        {
            bool output = true;
            //Check to see if a number is provided for casts
            int castNum;
            bool validCast = int.TryParse(castNumber, out castNum);
            if (validCast == false || castNum < 1)
            {
                output = false;
            }
            return output;
        }

        public static bool ValidateCruiseName(string cruiseName)
        {
            bool output = true;
            //Validate fields for cruise info
            //Check to see if a name is provided for the cruise
            if (cruiseName == null)
            {
                output = false;
            }
            else if (cruiseName.Length == 0)
            {
                output = false;
            }
            return output;
        }
    }
}