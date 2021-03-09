using System;
using System.Collections.Generic;
using System.Text;

namespace MyAwesomeAlarm.Models
{
    public class Alarm
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public string Message { get; set; }
        public string Days
        {
            get
            {
                var sbDays = new StringBuilder();
                foreach (var day in SelectedDays)
                {
                    switch (day)
                    {
                        case DayOfWeek.Sunday:
                            sbDays.Append("Sun, ");
                            break;
                        case DayOfWeek.Monday:
                            sbDays.Append("Mon, ");
                            break;
                        case DayOfWeek.Tuesday:
                            sbDays.Append("Tue, ");
                            break;
                        case DayOfWeek.Wednesday:
                            sbDays.Append("Wed, ");
                            break;
                        case DayOfWeek.Thursday:
                            sbDays.Append("Thu, ");
                            break;
                        case DayOfWeek.Friday:
                            sbDays.Append("Fri, ");
                            break;
                        case DayOfWeek.Saturday:
                            sbDays.Append("Sat, ");
                            break;
                        default:
                            break;
                    }
                }
                return sbDays.ToString();
            }
            set
            {
                if (value.Contains("Mon")) { SelectedDays.Add(DayOfWeek.Monday); }
                if (value.Contains("Tue")) { SelectedDays.Add(DayOfWeek.Tuesday); }
                if (value.Contains("Wed")) { SelectedDays.Add(DayOfWeek.Wednesday); }
                if (value.Contains("Thu")) { SelectedDays.Add(DayOfWeek.Thursday); }
                if (value.Contains("Fri")) { SelectedDays.Add(DayOfWeek.Friday); }
                if (value.Contains("Sat")) { SelectedDays.Add(DayOfWeek.Saturday); }
                if (value.Contains("Sun")) { SelectedDays.Add(DayOfWeek.Sunday); }
            }
        }
        public HashSet<DayOfWeek> SelectedDays { get; set; } = new HashSet<DayOfWeek>();
    }
}
