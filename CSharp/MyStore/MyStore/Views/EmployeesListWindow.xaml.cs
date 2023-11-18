using System.Windows;
using MyStore.Services;
using MyStore.ViewModels;


namespace MyStore.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesListWindow : Window
    {
        public EmployeesListWindow(object context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}