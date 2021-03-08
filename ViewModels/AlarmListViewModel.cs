using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class AlarmListViewModel : ViewModelBase
    {
        private static string path = $"{Environment.CurrentDirectory}\\alarmlist.json";

        private readonly FileIOService fileIOService = new FileIOService(path);
        public BindingList<Alarm> Alarms { get; set; }

        public AlarmListViewModel()
        {
            Alarms = fileIOService.LoadData();
        }

    }
}
