using System.Windows;
namespace MyStore.Views
{
    public partial class OrderWindow : Window
    {
        public OrderWindow(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}