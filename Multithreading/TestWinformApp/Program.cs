using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace TestWinformApp
{
    static class Program
    {
        public static Task task;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ThreadTest tt = new ThreadTest();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Form1 form1 = new Form1(cancellationTokenSource);

            Task longRunningTask = new Task((state) =>
            {
                tt.DoWork(cancellationTokenSource.Token);

            }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning);


            Thread thread = new Thread(() =>
                                           {
                                               longRunningTask.Start();

                                               try
                                               {
                                                   longRunningTask.Wait();
                                               }
                                               catch (AggregateException exception)
                                               {
                                                   MessageBox.Show(form1, exception.InnerException.Message);
                                               }
                                           });
            thread.Start();
            Application.Run(new Form1());
        }
    }
}
