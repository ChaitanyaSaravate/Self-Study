using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Recap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartThread_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart = MethodForTPL_FollowActionDelegateSignature;
            Thread thread = new Thread(threadStart);
            thread.Start();

            new Thread(this.MethodForQueueUserWorkItem).Start("Parameterized Thread Start used.");

            ThreadPool.QueueUserWorkItem(MethodForQueueUserWorkItem, "My god");

        }

        private void UpdateText(string text)
        {
            this.Invoke((MethodInvoker) delegate
                                            {
                                                txtResult.Text = txtResult.Text + text;
                                            });
        }

        static void AsyncCallBackHandler(IAsyncResult asyncResult)
        {
            try
            {
                Console.WriteLine("Inside async callback handler.");
                var target = (Func<string, string>)asyncResult.AsyncState;
                Console.WriteLine(target.EndInvoke(asyncResult)); // Note that without EndInvoke, you cannot catch exception here.
            }
            catch (Exception)
            {
                Console.WriteLine("Caught the exception when it was raised from the thread started using asynchronous delegates.");
            }
        }

        void MethodForQueueUserWorkItem(object message)
        {
            this.UpdateText(message as string);
        }

        static void MethodForTPL_FollowActionDelegateSignature()
        {
            Console.WriteLine("Called the thread using Task Library - TPL of C# 4.0.");
        }

        static string MethodFollowingFuncDelegateSignature(string message)
        {
            Console.WriteLine(message);
            return "I am done!";
        }

        static string MethodForTPL_FollowFuncDelegateSignature2(string message)
        {

            Console.WriteLine(message);

            throw new ArgumentException();
        }

        static void MethodThrowingException(bool isTryCatchUsedOnMainThread)
        {
            if (isTryCatchUsedOnMainThread)
            {
                // throw new Exception(); // Uncomment this line to demo how exception handling should not be done.
            }
            else
            {
                // If argument is false, then this code block executes which handles the exception on the thread itself.
                try
                {
                    throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine("Caugth exception on the thread from which it is raised.");
                }
            }
        }

        static void WriteOnMainThread()
        {
            for (int counter = 0; counter < 11; counter++)
            {
                Console.WriteLine("Writing on Main thread. MAIN THREAD COUNTER value = " + counter);
            }
        }

        /// <summary>
        /// Method to be passed to ThreadStart delegate while creating new thread.
        /// </summary>
        static void WriteOnOtherThread()
        {
            for (int counter = 0; counter < 11; counter++)
            {
                Console.WriteLine("Writing on other thread. OTHER THREAD COUNTER value = " + counter);
            }
        }

        /// <summary>
        /// Method to be passed to a background thread
        /// </summary>
        static void WriteOnBackgroundThread()
        {

            Thread.Sleep(5000);
            Console.WriteLine("Executing background thread");
        }

        /// <summary>
        /// This method demonstrates that separate memory stack is created for each thread. And thats why value of the local variable used in the method does not affect 
        /// across the threads.
        /// </summary>
        static void CommonMethodToWriteToMultipleThreads()
        {
            for (int counter = 0; counter < 5; counter++)
            {
                Console.WriteLine("Hi");
            }
        }

        static void MethodUsedToDemoThreadJoinMethod()
        {
            for (int counter = 0; counter < 5; counter++)
            {
                Console.WriteLine("Main thread is waiting until this thread finishes.");
            }
        }

        static void MethodUsedToDemoThreadJoinMethod2()
        {

            Console.WriteLine("Other thread ended and so main thread is executing this method.");

        }
    }
}
