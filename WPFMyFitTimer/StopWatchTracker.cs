using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFMyFitTimer
{
    class StopWatchTracker
    {
        private Thread thread;
        private Stopwatch stopwatch;
        private Handler handler;

        public StopWatchTracker()
        {
            stopwatch = new Stopwatch();
            handler = new Handler();
        }

        public void StartTimer()
        {
            stopwatch.Start();
            Console.WriteLine("Starting Timer");
            ShowTime();
        }

        public string EndTimer()
        {
            string time = FormatTime();
            thread.Abort();
            stopwatch.Stop();
            handler.OpenConnection();
            handler.InsertDB("INSERT INTO tb_timer (time) VALUES ('"+time+"')");
            handler.CloseConnection();
            stopwatch.Reset();
            return time;

        }
        public void ShowTime()
        {
            thread = new Thread(new ThreadStart(UpdateTime));
            thread.Start();
        }

        public void UpdateTime()
        {
            while (true)
            {
                Console.WriteLine(stopwatch.Elapsed);
                Thread.Sleep(5);
            }

        }

        public string FormatTime()
        {
            //00:00:02.7993305
            string formattedString;
            string original = stopwatch.Elapsed.ToString();

            int pos = original.IndexOf(":");

            formattedString = original.Substring(pos + 1);

            formattedString = formattedString.Substring(0,formattedString.Length - 5);

            Console.WriteLine(formattedString);
            Console.WriteLine(original);

            return formattedString;
        }
    }
}
