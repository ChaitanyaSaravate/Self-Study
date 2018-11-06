using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Common;

namespace TestWinformApp
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(CancellationTokenSource cancellationTokenSource)
            : this()
        {
            _cancellationTokenSource = cancellationTokenSource;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            //Cancel out the task
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }

            //Exit the program
            Application.Exit();

        }

                private void button1_Click(object sender, EventArgs e)
        {
            ThreadTest tt = new ThreadTest();
            Task task;

            
            try
            {
                task = System.Threading.Tasks.Task.Factory.StartNew(() => tt.PerformTask("Oh my god"));
                if(task.Exception != null)
                {
                    MessageBox.Show("Agrregate exception.");
                }
            }
            catch (AggregateException ex)
            {
               // MessageBox.Show("Inside agrregate exception.");
                if(ex != null && ex.InnerException != null)
                {
                    DialogResult result = MessageBox.Show(ex.InnerException.Message, "test", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                   {
                       this.button1_Click(null, null);
                   }
                    if(result == DialogResult.Cancel)
                    {
                        MessageBox.Show("Lets stop");
                        Application.Exit();
                    }
                    
               }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Inside exception.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadTest tt = new ThreadTest();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
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
                    MessageBox.Show(this, exception.InnerException.Message);
                }
            });
            thread.Start();
        }
    }
}
