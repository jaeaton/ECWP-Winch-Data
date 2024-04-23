namespace ViewModels
{
    internal class UnitConversionViewModel
    {
        //Tension Conversion
        public float ConvertTension(float tension)
        {
            float converted;
            switch (MainViewModel._configDataStore.CurrentWinch.TensionUnit)
            {
                case "lbf":
                    converted = ConvertFromPound(tension);
                    break;
                case "kg":
                    converted = ConvertFromKg(tension);
                    break;
                case "kip":
                    converted = ConvertFromKip(tension);
                    break;
                case "N":
                    converted = ConvertFromNewton(tension);
                    break;
                case "Short Ton":
                    converted = ConvertFromShortTon(tension);
                    break;
                case "Long Ton":
                    converted = ConvertFromLongTon(tension);
                    break;
                case "Tonne":
                    converted = ConvertFromTonne(tension);
                    break;
                default:
                    converted = tension;
                    break;
            }
            return converted;
        }
        public float ConvertFromPound(float tension)
        {
            float input = tension;
            float converted;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = (input / (float)2.2);
                    break;
                case "kip":
                    converted = (input/1000);
                    break;
                case "N":
                    converted = ((float)4.44822 * input);
                    break;
                case "Short Ton":
                    converted = (input / 2000);
                    break;
                case "Long Ton":
                    converted = (input / 2240);
                    break;
                case "Tonne":
                    converted = (input / (float)2204.62);
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }
        
        public float ConvertFromKip(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = ((1000 * input)/(float) 2.2 );
                    break;
                case "lbf":
                    converted = (input * 1000);
                    break;
                case "N":
                    converted = ((float)4.44822 * 1000 * input);
                    break;
                case "Short Ton":
                    converted = ((input * 1000) / 2000);
                    break;
                case "Long Ton":
                    converted = ((input * 1000) / 2240);
                    break;
                case "Tonne":
                    converted = ((input * 1000) / (float)2204.62);
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
            
        }

        public float ConvertFromKg(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "lbf":
                    converted = (input * (float)2.2);
                    break;
                case "kip":
                    converted = ((input * (float)2.2 )/ 1000);
                    break;
                case "N":
                    converted = ((float)9.81 * input);
                    break;
                case "Short Ton":
                    converted = ((input * (float)2.2 )/ 2000);
                    break;
                case "Long Ton":
                    converted = ((input * (float)2.2) / 2240);
                    break;
                case "Tonne":
                    converted = (input / 1000);
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }

        public float ConvertFromShortTon(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = ((2000 * input) / (float)2.2);
                    break;
                case "kip":
                    converted = (input / 2);
                    break;
                case "N":
                    converted = ((float)4.44822 * 2000* input);
                    break;
                case "lbf":
                    converted = (input * 2000);
                    break;
                case "Long Ton":
                    converted = (input / (float)1.12);
                    break;
                case "Tonne":
                    converted = (input / (float)1.102);
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }

        public float ConvertFromLongTon(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = ((2240 * input) / (float)2.2);
                    break;
                case "kip":
                    converted = (input * (float)2.24);
                    break;
                case "N":
                    converted = ((float)4.44822 * 2240 * input);
                    break;
                case "Short Ton":
                    converted = (input * (float)1.12);
                    break;
                case "lbf":
                    converted = (input * 2240);
                    break;
                case "Tonne":
                    converted = (input * (float)1.106 );
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }

        public float ConvertFromTonne(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = (input * 1000);
                    break;
                case "kip":
                    converted = (input * (float)2.2 );
                    break;
                case "N":
                    converted = ((float)9.81 * 1000 * input);
                    break;
                case "Short Ton":
                    converted = ((input * (float)2.2) / 2);
                    break;
                case "Long Ton":
                    converted = (input / 2240);
                    break;
                case "lbf":
                    converted = (input * ((float)2.2 * 1000));
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }

        public float ConvertFromNewton(float tension)
        {
            float input = tension;
            float converted ;

            switch (MainViewModel._configDataStore.CurrentWinch.TensionConversionUnit)
            {
                case "kg":
                    converted = ((float)2.2 * input);
                    break;
                case "kip":
                    converted = (input / 1000);
                    break;
                case "N":
                    converted = ((float)4.44822 * input);
                    break;
                case "Short Ton":
                    converted = (input / 2000);
                    break;
                case "Long Ton":
                    converted = (input / 2240);
                    break;
                case "Tonne":
                    converted = (input / (float)2204.62);
                    break;
                default:
                    converted = input;
                    break;
            }
            return converted;
        }
        //Payout Conversion
        //Speed Conversion
    }
}
