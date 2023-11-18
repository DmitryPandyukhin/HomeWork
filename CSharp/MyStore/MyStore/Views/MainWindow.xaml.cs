using System.Windows;
using MyStore.ViewModels;

namespace MyStore.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}