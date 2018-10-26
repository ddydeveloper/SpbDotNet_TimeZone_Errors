using System;

namespace TimeZoneConvert
{
    public class Converter
    {
        public bool TryConvertToTimeZone(DateTime fromValue, string timeZoneId, out DateTime toValue, out string errorMsg)
        {
            toValue = fromValue;
            errorMsg = null;

            try
            {
                /*
                    .Net Core using system time zones, but, unfortunately, Windows and Linux have different ones.
                    To avoid potentially problems with a Linux host we try to check Windows TimeZoneID, and if we can't find suitable one - use Linux TimeZoneId
                    https://stackoverflow.com/questions/41566395/timezoneinfo-in-net-core-when-hosting-on-unix-nginx?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
                */

                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                //try
                //{
                //    zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                //}
                //catch (TimeZoneNotFoundException)
                //{
                //    var ianaTimeZoneId = TZConvert.WindowsToIana(timeZoneId);
                //    zone = TimeZoneInfo.FindSystemTimeZoneById(ianaTimeZoneId);
                //}

                toValue = TimeZoneInfo.ConvertTime(fromValue, TimeZoneInfo.Local, zone);

                return true;
            }
            catch (TimeZoneNotFoundException e)
            {
                errorMsg = $"The registry does not define the {timeZoneId} zone, {e.Message}";
                return false;
            }
            catch (InvalidTimeZoneException e)
            {
                errorMsg = $"The registry does not define the {timeZoneId} zone, {e.Message}";
                return false;
            }
            catch (Exception e)
            {
                errorMsg = $"Can't convert to {timeZoneId} zone. {e.Message}";
                return false;
            }
        }
    }
}
