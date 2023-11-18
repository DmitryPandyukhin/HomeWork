using System.Windows;

namespace MyStore.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartamentWindow.xaml
    /// </summary>
    public partial class DepartamentWindow : Window
    {
        public DepartamentWindow(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
