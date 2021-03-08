using System.Windows;
using MyAwesomeAlarm.ViewModels;

namespace MyAwesomeAlarm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
