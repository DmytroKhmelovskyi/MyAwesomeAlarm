using MyAwesomeAlarm.Commands;
using MyAwesomeAlarm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MyAwesomeAlarm.ViewModels
{
    public class StopWindowViewModel : INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public event PropertyChangedEventHandler PropertyChanged;
        private string message;
        public ICommand CloseCommand { get; set; }

        public ICommand StopButtonCommand { get; set; }
        public string AlarmSongUrl { get; set; }

        public StopWindowViewModel()
        {
            StopButtonCommand = new RelayCommand(StopButtonClick, CanStopButtonClick);
            CloseCommand = new RelayCommand(o => ((Window)o).Close());
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private void StopButtonClick(object obj)
        {
            mediaPlayer.Stop();
        }
        private bool CanStopButtonClick(object obj)
        {
            return true;
        }

        internal void Show()
        {
            AlarmOnSound();

        }
        public void AlarmOnSound()
        {
            if (AlarmSongUrl != "")
            {
                mediaPlayer.Open(new Uri(AlarmSongUrl));
                mediaPlayer.Play();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
