using System.Windows;
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
