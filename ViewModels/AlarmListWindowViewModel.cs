using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MyAwesomeAlarm.Commands;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class AlarmListWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static string path = $"{Environment.CurrentDirectory}\\alarmslist.json";

        public FileIOService FileIOService { get; set; } = new FileIOService(path);
        public BindingList<Alarm> Alarms { get; set; }

        public AlarmListWindowViewModel()
        {
            Alarms = FileIOService.LoadData();
            Alarms.ListChanged += AlarmsList_ListChanged;

        }

        public void AlarmsList_ListChanged(object sender, ListChangedEventArgs e)
       {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    FileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
