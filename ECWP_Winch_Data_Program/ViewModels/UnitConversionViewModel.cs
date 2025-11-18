namespace ViewModels
{
    internal class UnitConversionViewModel
    {
        //Tension Conversion
        public float ConvertTension(float tension, WinchModel winch)
        {
            float converted;
            switch (winch.TensionUnit)
            {
                case "lbf":
                    converted = ConvertFromPound(tension, winch.TensionConversionUnit);
                    break;

                case "kg":
                    converted = ConvertFromKg(tension, winch.TensionConversionUnit);
                    break;

                case "kip":
                    converted = ConvertFromKip(tension, winch.TensionConversionUnit);
                    break;

                case "N":
                    converted = ConvertFromNewton(tension, winch.TensionConversionUnit);
                    break;

                case "Short Ton":
                    converted = ConvertFromShortTon(tension, winch.TensionConversionUnit);
                    break;

                case "Long Ton":
                    converted = ConvertFromLongTon(tension, winch.TensionConversionUnit);
                    break;

                case "Tonne":
                    converted = ConvertFromTonne(tension, winch.TensionConversionUnit);
                    break;

                default:
                    converted = tension;
                    break;
            }
            return converted;
        }

        public float ConvertFromPound(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
            {
                case "kg":
                    converted = (input / (float)2.2);
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

        public float ConvertFromKip(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
            {
                case "kg":
                    converted = ((1000 * input) / (float)2.2);
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

        public float ConvertFromKg(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
            {
                case "lbf":
                    converted = (input * (float)2.2);
                    break;

                case "kip":
                    converted = ((input * (float)2.2) / 1000);
                    break;

                case "N":
                    converted = ((float)9.81 * input);
                    break;

                case "Short Ton":
                    converted = ((input * (float)2.2) / 2000);
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

        public float ConvertFromShortTon(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
            {
                case "kg":
                    converted = ((2000 * input) / (float)2.2);
                    break;

                case "kip":
                    converted = (input / 2);
                    break;

                case "N":
                    converted = ((float)4.44822 * 2000 * input);
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

        public float ConvertFromLongTon(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
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
                    converted = (input * (float)1.106);
                    break;

                default:
                    converted = input;
                    break;
            }
            return converted;
        }

        public float ConvertFromTonne(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
            {
                case "kg":
                    converted = (input * 1000);
                    break;

                case "kip":
                    converted = (input * (float)2.2);
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

        public float ConvertFromNewton(float tension, string TensionConversionUnit)
        {
            float input = tension;
            float converted;

            switch (TensionConversionUnit)
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
        public float ConvertPayout(float payout, WinchModel winch)
        {
            float converted;
            switch (winch.PayoutUnit)
            {
                case "m":
                    converted = ConvertFromKMeters(payout, winch.PayoutConversionUnit);
                    break;

                case "ft":
                    converted = ConvertFromFeet(payout, winch.PayoutConversionUnit);
                    break;

                case "km":
                    converted = ConvertFromKMeters(payout, winch.PayoutConversionUnit);
                    break;

                default:
                    converted = payout;
                    break;
            }
            return converted;
        }

        public float ConvertFromMeters(float payout, string PayoutConversionUnit)
        {
            float converted;
            switch (PayoutConversionUnit)
            {
                case "km":
                    converted = payout / 1000;
                    break;

                case "ft":
                    converted = (payout) / ((float)0.0254 * 12);
                    break;

                default:
                    converted = payout;
                    break;
            }

            return converted;
        }

        public float ConvertFromFeet(float payout, string PayoutConversionUnit)
        {
            float converted;
            switch (PayoutConversionUnit)
            {
                case "m":
                    converted = payout * 12 * (float)0.0254;
                    break;

                case "km":
                    converted = payout * 12 * (float)0.0000254;
                    break;

                default:
                    converted = payout;
                    break;
            }

            return converted;
        }

        public float ConvertFromKMeters(float payout, string PayoutConversionUnit)
        {
            float converted;
            switch (PayoutConversionUnit)
            {
                case "m":
                    converted = payout * 1000;
                    break;

                case "ft":
                    converted = payout / ((float)0.0000254 * 12);
                    break;

                default:
                    converted = payout;
                    break;
            }

            return converted;
        }

        //Speed Conversion
        public float ConvertSpeed(float speed, WinchModel winch)
        {
            float converted;
            switch (winch.SpeedUnit)
            {   
                case "m/min":
                    converted = ConvertFromMeterperMin(speed, winch.SpeedConversionUnit);
                    break;

                case "ft/min":
                    converted = ConvertFromFeetperMin(speed, winch.SpeedConversionUnit);
                    break;

                case "kph":
                    converted = ConvertFromKPH(speed, winch.SpeedConversionUnit);
                    break;

                case "mph":
                    converted = ConvertFromMPH(speed, winch.SpeedConversionUnit);
                    break;
                case "m/sec":
                    converted = ConvertFromMPS(speed, winch.SpeedConversionUnit);
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }

        public float ConvertFromMeterperMin(float speed, string SpeedConversionUnit)
        {
            float converted;
            switch (SpeedConversionUnit)
            {
                case "ft/min":
                    converted = speed / ((float)0.0254 * 12);
                    break;

                case "kph":
                    converted = (speed * 60) / 1000;
                    break;

                case "mph":
                    converted = (speed * 60) / ((float)0.0254 * 12 * 5280);
                    break;

                case "m/sec":
                    converted = speed / 60;
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }

        public float ConvertFromFeetperMin(float speed, string SpeedConversionUnit)
        {
            float converted;
            switch (SpeedConversionUnit)
            {
                case "m/min":
                    converted = speed * 12 * (float)0.0254;
                    break;

                case "kph":
                    converted = speed * 12 * (float)0.0000254 * 60;
                    break;

                case "mph":
                    converted = (speed * 60) / 5280;
                    break;

                case "m/sec":
                    converted = speed * 12 * (float)0.0254/60;
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }

        public float ConvertFromKPH(float speed, string SpeedConversionUnit)
        {
            float converted;
            switch (SpeedConversionUnit)
            {
                case "m/min":
                    converted = (speed * 1000) / 60;
                    break;

                case "ft/min":
                    converted = speed / ((float)0.0000254 * 60 * 12);
                    break;

                case "mph":
                    converted = speed / ((float)0.0000254 * 60 * 12 * 5280);
                    break;

                case "m/sec":
                    converted = (speed * 1000) / 3600;
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }

        public float ConvertFromMPH(float speed, string SpeedConversionUnit)
        {
            float converted;
            switch (SpeedConversionUnit)
            {
                case "m/min":
                    converted = (speed * 5280 * 12 * (float)0.0254) / 60;
                    break;

                case "ft/min":
                    converted = (speed * 5280) / 60;
                    break;

                case "kph":
                    converted = (speed * 5280 * 12 * (float)0.0000254);
                    break;

                case "m/sec":
                    converted = (speed * 5280 * 12 * (float)0.0254) / 3600;
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }
        //Meters per second
        public float ConvertFromMPS(float speed, string SpeedConversionUnit)
        {
            float converted;
            switch (SpeedConversionUnit)
            {
                case "ft/min":
                    converted = (speed * 60) / ((float)0.0254 * 12);
                    break;

                case "kph":
                    converted = (speed * 60) / 1000;
                    break;

                case "mph":
                    converted = (speed * 60) / ((float)0.0254 * 12 * 5280);
                    break;

                case "m/min":
                    converted = (speed * 60);
                    break;

                default:
                    converted = speed;
                    break;
            }
            return converted;
        }

    }
}