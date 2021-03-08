using System;
using System.Collections.Generic;
using System.Text;

namespace MyAwesomeAlarm.Models {
  public class Alarm {
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public string Days {
      get {
        var sbDays = new StringBuilder();
        foreach (var day in SelectedDays) {
          switch (day) {
            case DayOfWeek.Sunday:
              sbDays.Append("Sun, ");
              break;
            case DayOfWeek.Monday:
              sbDays.Append("Mon, ");
              break;
            case DayOfWeek.Tuesday:
              break;
            case DayOfWeek.Wednesday:
              break;
            case DayOfWeek.Thursday:
              break;
            case DayOfWeek.Friday:
              break;
            case DayOfWeek.Saturday:
              break;
            default:
              break;
          }
        }
        return sbDays.ToString();
      }
      set {
        if (value.Contains("Mon")) { SelectedDays.Add(DayOfWeek.Monday); }
      }
    }
    public HashSet<DayOfWeek> SelectedDays { get; set; } = new HashSet<DayOfWeek>();
  }
}
