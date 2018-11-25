using System;

namespace TimeZoneConvert
{
    public static class Converter
    {
        public static bool TryConvertToTimeZone(DateTime fromValue, string timeZoneId, out DateTime toValue, out string errorMsg)
        {
            toValue = fromValue;
            errorMsg = null;

            try
            {
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
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
