using System.Windows;
using WPFBoilerplate.ViewModels;

namespace WPFBoilerplate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModelWithCommands();
        }
    }
}