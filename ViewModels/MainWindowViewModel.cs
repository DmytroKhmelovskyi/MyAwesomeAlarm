using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using MyAwesomeAlarm.Commands;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly string path = $"{Environment.CurrentDirectory}\\alarmslist.json";
        FileIOService fileIOService = new FileIOService(path);
        private readonly Alarm alarm = new Alarm();
        private string alarmSongUrl = "";
        private string _mediaName;
        readonly DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer _timer;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _currentTime;
        private string _currentDate;
        public MainWindowViewModel()
        {
            LoadCommands();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) =>
            {
                CurrentTime = DateTime.Now.ToLongTimeString();
                CurrentDate = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}";
            };
            _timer.Start();
        }
        public ICommand BtnHoursDownCommand { get; set; }
        public ICommand BtnHoursUpCommand { get; set; }
        public ICommand BtnMinutesDownCommand { get; set; }
        public ICommand BtnMinutesUpCommand { get; set; }
        public ICommand SetTheAlarmCommand { get; set; }
        public ICommand AlarmListCommand { get; set; }
        public ICommand ChooseSongCommand { get; set; }
        public ICommand TestPlayCommand { get; set; }

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
        public string Message
        {
            get { return alarm.Message; }
            set
            {
                alarm.Message = value;
                OnPropertyChanged("Message");
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
        public bool Wed
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Wednesday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Wednesday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Wednesday);
                }
                OnPropertyChanged("Wed");
            }
        }
        public bool Thu
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Thursday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Thursday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Thursday);
                }
                OnPropertyChanged("Thu");
            }
        }
        public bool Fri
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Friday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Friday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Friday);
                }
                OnPropertyChanged("Fri");
            }
        }
        public bool Sat
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Saturday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Saturday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Saturday);
                }
                OnPropertyChanged("Sat");
            }
        }
        public bool Sun
        {
            get { return alarm.SelectedDays.Contains(DayOfWeek.Sunday); }
            set
            {
                if (value)
                {
                    alarm.SelectedDays.Add(DayOfWeek.Sunday);
                }
                else
                {
                    alarm.SelectedDays.Remove(DayOfWeek.Sunday);
                }
                OnPropertyChanged("Sun");
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
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                if (_currentTime == value)
                    return;
                _currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        } 
        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                if (_currentDate == value)
                    return;
                _currentDate = value;
                OnPropertyChanged("CurrentDate");
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
            TestPlayCommand = new RelayCommand(TestClick, CanTestClick);
        }
        public void Timer_Tick(object sender, EventArgs e)
        {

            DateTime currentTime = DateTime.Now;
            var alarmList = fileIOService.LoadData();
            foreach (var al in alarmList)
            {
                if ((!al.SelectedDays.Any() || al.SelectedDays.Contains(currentTime.DayOfWeek))
                    && currentTime.Hour == al.Hours && currentTime.Second < 1)
                {
                    if (currentTime.Minute == al.Minutes)
                    {
                        OpenStopWindow();
                    }

                }
            }
        }
        private void OpenStopWindow()
        {
            StopWindow chldWindow = new StopWindow();
            var viewModel = new StopWindowViewModel();
            viewModel.AlarmSongUrl = alarmSongUrl;
            chldWindow.DataContext = viewModel;
            viewModel.AlarmOnSound();
            chldWindow.Show();

        }
        private void TestClick(object obj)
        {
            var viewModel = new StopWindowViewModel();
            viewModel.AlarmSongUrl = alarmSongUrl;
            viewModel.AlarmOnSound();
            OpenStopWindow();

        }
        private void BtnHoursDownClick(object obj)
        {
            Hours--;
            if (Hours < 0)
            {
                Hours = 23;
            }

        }
        private void BtnHoursUpClick(object obj)
        {
            Hours++;
            if (Hours > 23)
            {
                Hours = 0;
            }
        }
        private void BtnMinutesDownClick(object obj)
        {
            Minutes--;
            if (Minutes < 0)
            {
                Minutes = 59;
            }

        }
        private void BtnMinutesUpClick(object obj)
        {
            Minutes++;
            if (Minutes > 59)
            {
                Minutes = 0;
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


        private bool CanTestClick(object obj)
        {
            return true;
        }
        private bool CanLoadClick(object obj)
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
        private bool CanBtnHoursDownClick(object obj)
        {
            return true;
        }
        private bool CanBtnHoursUpClick(object obj)
        {
            return true;
        }
        private bool CanBtnMinutesDownClick(object obj)
        {
            return true;
        }
        private bool CanBtnMinutesUpClick(object obj)
        {
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
