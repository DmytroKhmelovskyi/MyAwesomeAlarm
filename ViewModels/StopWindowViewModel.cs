using MyAwesomeAlarm.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MyAwesomeAlarm.ViewModels
{
    class StopWindowViewModel : INotifyPropertyChanged
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseWindowCommand { get; set; }
        public string AlarmSongUrl { get; set; }

        public StopWindowViewModel()
        {
            CloseWindowCommand = new RelayCommand(StopLayOutName, CanStopLayOutName);
        }

        private void StopLayOutName(object obj)
        {
            mediaPlayer.Stop();
        }
        private bool CanStopLayOutName(object obj)
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
