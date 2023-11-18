using System.Windows;
using MyStore.Models;

namespace MyStore.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
