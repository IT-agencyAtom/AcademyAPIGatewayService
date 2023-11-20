namespace CrmIntegration.Utils
{
    public static class DateHelper
    {
        public static int ToEpoch(this DateTime date)
        {
            TimeSpan timeSpan = date - new DateTime(1970, 1, 1);
            int epoch = (int)timeSpan.TotalSeconds;
            return epoch;
        }
    }
}
