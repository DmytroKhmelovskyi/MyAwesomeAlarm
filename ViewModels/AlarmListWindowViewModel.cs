using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class AlarmListWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static string path = $"{Environment.CurrentDirectory}\\alarmlist.json";

        private readonly FileIOService fileIOService = new FileIOService(path);
        public BindingList<Alarm> Alarms { get; set; }

        public AlarmListWindowViewModel()
        {
            Alarms = fileIOService.LoadData();
        }

  
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
