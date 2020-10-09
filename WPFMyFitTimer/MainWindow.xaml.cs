using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WPFMyFitTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Handler handler;
        private StopWatchTracker swt;
        public MainWindow()
        {
            InitializeComponent();
            swt = new StopWatchTracker();
            handler = new Handler();
            UpdateTable();

        }

        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            swt.StartTimer();
        }

        private void StopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            string currentTime = swt.EndTimer();
            CurrentTimerLabel.Content = currentTime;
            UpdateTable();
        }

        public void UpdateTable()
        {
            List<string> newTimes = new List<string>();
            handler.OpenConnection();
            newTimes = handler.QueryDB("SELECT Time FROM tb_timer");
            handler.CloseConnection();

            listBox.Items.Clear();

            foreach(string s in newTimes)
            {
                listBox.Items.Add(s);
            }
        }
    }
}
