using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MyAwesomeAlarm.Commands;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string path = $"{Environment.CurrentDirectory}\\alarmlist.json";
        private readonly Alarm alarm = new Alarm();
        private string alarmSongUrl = "";
        private string _mediaName;
        public ICommand BtnHoursDownCommand { get; set; }
        public ICommand BtnHoursUpCommand { get; set; }
        public ICommand BtnMinutesDownCommand { get; set; }
        public ICommand BtnMinutesUpCommand { get; set; }
        public ICommand SetTheAlarmCommand { get; set; }
        public ICommand AlarmListCommand { get; set; }
        public ICommand ChooseSongCommand { get; set; }

        public int Hours
        {
            get { return alarm.Hours; }
            set
            {
                alarm.Hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get { return alarm.Minutes; }
            set
            {
                alarm.Minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public bool Mon
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Monday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Monday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Monday);
                }
                OnPropertyChanged("Mon");
            }
        }

        public bool Tue
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Tuesday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Tuesday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Tuesday);
                }
                OnPropertyChanged("Tue");
            }
        }
        public string MediaName
        {
            get { return _mediaName; }
            set
            {
                if (_mediaName != value)
                {
                    _mediaName = value;
                    OnPropertyChanged("Minutes");
                }
            }
        }
        private void LoadCommands()
        {
            BtnHoursDownCommand = new RelayCommand(BtnHoursDownClick, CanBtnHoursDownClick);
            BtnHoursUpCommand = new RelayCommand(BtnHoursUpClick, CanBtnHoursUpClick);
            BtnMinutesDownCommand = new RelayCommand(BtnMinutesDownClick, CanBtnMinutesDownClick);
            BtnMinutesUpCommand = new RelayCommand(BtnMinutesUpClick, CanBtnMinutesUpClick);
            SetTheAlarmCommand = new RelayCommand(SetTheAlarmClick, CanSetTheAlarmClick);
            AlarmListCommand = new RelayCommand(AlarmListClick, CanAlarmListClick);
            ChooseSongCommand = new RelayCommand(LoadClick, CanLoadClick);
        }
        private void BtnHoursDownClick(object obj)
        {
            alarm.Hours--;
            if (alarm.Hours < 0)
            {
                alarm.Hours = 23;
            }

        }
        private void BtnHoursUpClick(object obj)
        {
            alarm.Hours++;
            if (alarm.Hours > 23)
            {
                alarm.Hours = 0;
            }
        }
        private void BtnMinutesDownClick(object obj)
        {
            alarm.Minutes--;
            if (alarm.Minutes < 0)
            {
                alarm.Minutes = 59;
            }

        }
        private void BtnMinutesUpClick(object obj)
        {
            alarm.Minutes++;
            if (alarm.Minutes > 59)
            {
                alarm.Minutes = 0;
            }
        }
        public void SetTheAlarmClick(object obj)
        {
            var service = new FileIOService(path);
            var alarms = service.LoadData();
            alarms.Add(this.alarm);
            service.SaveData(alarms);
            MessageBox.Show("New alarm clock added!");
        }
        private void AlarmListClick(object obj)
        {
            var alarmListWindow = new AlarmListWindow();
            alarmListWindow.Show();

        }
        private void LoadClick(object obj)
        {

            var dialog = new OpenFileDialog();
            dialog.Title = "Choose Media";
            if (dialog.ShowDialog() == true)
            {
                alarmSongUrl = dialog.FileName;
                MediaName = dialog.FileName;
            }

        }


        private bool CanLoadClick(object sender)
        {
            return true;
        }
        private bool CanAlarmListClick(object obj)
        {
            return true;
        }
        public bool CanSetTheAlarmClick(object obj)
        {
            return true;
        }
        private bool CanBtnHoursDownClick(object sender)
        {
            return true;
        }
        private bool CanBtnHoursUpClick(object sender)
        {
            return true;
        }
        private bool CanBtnMinutesDownClick(object sender)
        {
            return true;
        }
        private bool CanBtnMinutesUpClick(object sender)
        {
            return true;
        }

    }
}
