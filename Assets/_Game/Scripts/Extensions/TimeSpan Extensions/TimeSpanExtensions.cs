using System;
using UnityEngine;

namespace TileCat3.Extensions
{
    public static class TimeSpanExtensions
    {
        public static bool TryConvertToSeconds(this TimeSpan timeSpan, out int seconds)
        {
            if (timeSpan.TotalSeconds <= int.MinValue || timeSpan.TotalSeconds >= int.MaxValue)
            {
                UnityEngine.Debug.LogError("Cannot convert timespan to int32");

                seconds = 0;
                return false;
            }

            seconds = Convert.ToInt32(timeSpan.TotalSeconds);
            return true;
        }

        public static string ToString(this TimeSpan timeSpan, StringFormat stringFormat, TimeFormat timeFormat)
        {
            switch (stringFormat)
            {
                case StringFormat.Colon:
                    return timeSpan.ToStringColonFormat(timeFormat);
                case StringFormat.Short:
                    return timeSpan.ToStringShortFormat(timeFormat);
                default:
                    Debug.LogError("Invalid format");
                    return null;
            }
        }

        private static string ToStringColonFormat(this TimeSpan timeSpan, TimeFormat format)
        {
            int minutes = timeSpan.Days * 24 * 60 + timeSpan.Hours * 60 + timeSpan.Minutes;
            int hours = timeSpan.Days * 24 + timeSpan.Hours;

            switch (format)
            {
                case TimeFormat.MS:
                    return string.Format(@"{0:00}:{1:00}", minutes, timeSpan.Seconds);
                case TimeFormat.HM:
                    return string.Format(@"{0:00}:{1:00}", hours, timeSpan.Minutes);
                case TimeFormat.HMS:
                    return string.Format(@"{0:00}:{1:00}:{2:00}", hours, timeSpan.Minutes, timeSpan.Seconds);
                case TimeFormat.DH:
                    return string.Format(@"{0:00}:{1:00}", timeSpan.Days, timeSpan.Hours);
                case TimeFormat.DHM:
                    return string.Format(@"{0:00}:{1:00}:{2:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
                case TimeFormat.DHMS:
                    return string.Format(@"{0:00}:{1:00}:{2:00}:{3:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                default:
                    Debug.LogError("Invalid format");
                    return null;
            }
        }

        private static string ToStringShortFormat(this TimeSpan timeSpan, TimeFormat format)
        {
            int minutes = timeSpan.Days * 24 * 60 + timeSpan.Hours * 60 + timeSpan.Minutes;
            int hours = timeSpan.Days * 24 + timeSpan.Hours;

            switch (format)
            {
                case TimeFormat.MS:
                    return string.Format(@"{0:00}m {1:00}s", minutes, timeSpan.Seconds);
                case TimeFormat.HM:
                    return string.Format(@"{0:00}h {1:00}m", hours, timeSpan.Minutes);
                case TimeFormat.HMS:
                    return string.Format(@"{0:00}h {1:00}m {2:00}s", hours, timeSpan.Minutes, timeSpan.Seconds);
                case TimeFormat.DH:
                    return string.Format(@"{0:00}d {1:00}h", timeSpan.Days, timeSpan.Hours);
                case TimeFormat.DHM:
                    return string.Format(@"{0:00}d {1:00}h {2:00}m", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
                case TimeFormat.DHMS:
                    return string.Format(@"{0:00}d {1:00}h {2:00}m {3:00}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                default:
                    Debug.LogError("Invalid format");
                    return null;
            }
        }
    }
}
