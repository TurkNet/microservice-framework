using System;

namespace Noctools.Utils.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GenerateDateTime()
        {
            return DateTimeOffset.Now.DateTime;
        }
    }
}
