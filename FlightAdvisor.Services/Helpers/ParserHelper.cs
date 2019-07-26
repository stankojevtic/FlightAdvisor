namespace FlightAdvisor.Core.Helpers
{
    public static class ParserHelper
    {
        public static double? TryParseDouble(string rowItem)
        {
            double returnValue;
            if (double.TryParse(rowItem, out returnValue))
                return returnValue;
            else
                return null;
        }

        public static int? TryParseInt(string rowItem)
        {
            int returnValue;
            if (int.TryParse(rowItem, out returnValue))
                return returnValue;
            else
                return null;
        }
    }
}
