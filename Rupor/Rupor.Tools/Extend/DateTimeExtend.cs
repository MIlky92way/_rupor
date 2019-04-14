using System;
using System.Globalization;

namespace Rupor.Tools.Extend
{
    public static class DateTimeExtend
    {
        public static void ParseRSS2Dates(this DateTime dateTime, string lang, string date)
        {
            if (string.IsNullOrEmpty(date))
                return;
            CultureInfo culture = null;
            try
            {
                culture = string.IsNullOrEmpty(lang) ? CultureInfo.CurrentCulture : new CultureInfo(lang);
            }
            catch
            {

            }
            //TODO DateTimeExtend ParseRSS2Dates

        }
    }
}
