using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int defaultTime = 5;
        int currentTime = 0;
        bool pause = false;
        List<Task> lt;
        DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();

            lt = new();
            Task t = new();
            t.Title = "Задача 0";
            t.Time = 5;
            lt.Add(t);

            t = new();
            t.Title = "Задача 1";
            t.Time = 6;
            lt.Add(t);

            tasksList.ItemsSource = lt;

            // TODO ЗАгрузка и сохранение в xml

            

            dispatcherTimer = new ();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (currentTime > 0)
            {
                currentTime--;

                time_TextBox.Text = new TimeSpan(0, 0, currentTime).ToString();


                    //currentTime.ToString();
            }
            else
            {
                if (sender is DispatcherTimer d)
                    d.Stop();
            }    
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            start_Button.IsEnabled = false;
            stop_Button.IsEnabled = pause_Button.IsEnabled = true;

            if (pause)
            {
                dispatcherTimer.Start();
                pause = false;
            }
            else
            {
                if (tasksList.SelectedItems[0] is Task t)
                {
                    defaultTime = (Int32)t.Time;
                }
                currentTime = defaultTime;
                time_TextBox.Text = new TimeSpan(0, 0, currentTime).ToString();
                //time_TextBox.Text = currentTime.ToString();
                dispatcherTimer.Start();
            }    
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            start_Button.IsEnabled = true;
            stop_Button.IsEnabled = pause_Button.IsEnabled = false;

            dispatcherTimer.Stop();
            pause = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            start_Button.IsEnabled = true;
            stop_Button.IsEnabled = pause_Button.IsEnabled = false;

            dispatcherTimer.Stop();
            pause = false;
        }
    }
}
