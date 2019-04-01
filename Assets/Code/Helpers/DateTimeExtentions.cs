using System;

namespace TeamZ.Assets.Code.Helpers
{
	public static class DateTimeExtentions
	{
		public static string ToTeamZTime(this TimeSpan time)
		{
			var minutesAndSeconds = $"{time.Minutes.ToString("00")}'{time.Seconds.ToString("00")}";
			if (time.Hours > 0)
			{
				return $"{time.Hours.ToString("00")}'{minutesAndSeconds}";
			}

			return minutesAndSeconds;
		}

		public static string ToTeamZDateTime(this DateTime dateTime)
		{
			return $"{dateTime.Day}_{dateTime.Minute}_{dateTime.Year}_{dateTime.TimeOfDay.ToTeamZTime()}";
		}
	}
}
