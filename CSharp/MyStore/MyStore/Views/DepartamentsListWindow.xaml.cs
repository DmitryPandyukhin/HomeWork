using System.Windows;
using MyStore.Services;
using MyStore.ViewModels;

namespace MyStore.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartamentsWindow.xaml
    /// </summary>
    public partial class DepartamentsListWindow : Window
    {
        public DepartamentsListWindow(object context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
