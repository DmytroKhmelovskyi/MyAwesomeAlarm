using System.Windows;
using MyAwesomeAlarm.ViewModels;

namespace MyAwesomeAlarm
{
    /// <summary>
    /// Interaction logic for AlarmListWindow.xaml
    /// </summary>
    public partial class AlarmListWindow : Window
    {
        private readonly AlarmListViewModel viewModel = new AlarmListViewModel();
        public AlarmListWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
    }
    }
}
