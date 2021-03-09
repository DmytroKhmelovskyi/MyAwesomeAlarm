using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;

namespace MyAwesomeAlarm.ViewModels
{
    public class AlarmListWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static string path = $"{Environment.CurrentDirectory}\\alarmslist.json";

        private readonly FileIOService fileIOService = new FileIOService(path);
        public BindingList<Alarm> Alarms { get; set; }

        public AlarmListWindowViewModel()
        {
            Alarms = fileIOService.LoadData();
            Alarms.ListChanged += AlarmsList_ListChanged;
        }

        private void AlarmsList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    fileIOService.SaveData(sender);
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
