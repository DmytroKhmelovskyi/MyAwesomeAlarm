using System;
using System.ComponentModel;
using System.Windows;
using MyAwesomeAlarm.Models;
using MyAwesomeAlarm.Services;
using MyAwesomeAlarm.ViewModels;

namespace MyAwesomeAlarm
{
    /// <summary>
    /// Interaction logic for AlarmListWindow.xaml
    /// </summary>
    public partial class AlarmListWindow : Window
    {
        private readonly AlarmListWindowViewModel viewModel = new AlarmListWindowViewModel();
        public AlarmListWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
