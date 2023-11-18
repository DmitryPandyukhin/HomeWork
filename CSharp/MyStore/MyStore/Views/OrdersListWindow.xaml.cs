using System.Windows;
using MyStore.Services;
using MyStore.ViewModels;

namespace MyStore.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        public OrdersListWindow(object context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
