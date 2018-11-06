using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace Common
{
    public class ThreadTest
    {
        private bool instanceVariable_Done;

        public static bool staticVariable_Done;

        public static bool threadSafeVariable;

        // Object that assures thread safety when used in the "lock" statement.
        private readonly object locker = new object();

        /// <summary>
        ///  This method internally uses instance variable "done". This is to demo that the Threads share data ("done" variable) if they have a common reference to the same object instance. 
        /// </summary>
        public void InstanceMethodUsingInstanceVariable()
        {
            #region Lack of thread safety

            /* It's quite unpredictable if the writeline will execute once or twice. That's beacause code block is NOT thread safe.
             * 
             * The problem is that one thread can be evaluating the if statement right as the other thread is executing the WriteLine statement — before it’s had a chance to set variable to true.
             *
             */

            if (!this.instanceVariable_Done)
            {
                this.instanceVariable_Done = true;
                Console.WriteLine("Instance variable Done set to true.");
            }

            // Static variable is another way of sharing data between two threads.
            if(!staticVariable_Done)
            {
                Console.WriteLine("Static variable Done set to true.");
                staticVariable_Done = true;
            }
            #endregion

            // The lock statement assures that the code block is available only to one thread. It blocks another thread until other thread releases a lock.
            // Note: A thread, while blocked, doesn't consume CPU resources.
            lock(locker)
            {
                if(!threadSafeVariable)
                {
                    threadSafeVariable = true;
                    Console.WriteLine("Thread safe static variable set to true.");
                }
            }
        }

        public void PrintFullName(string firstName, string lastName)
        {
            Console.WriteLine(firstName + " " + lastName);
        }

        public void PrintName(string firstName)
        {
            Console.WriteLine(firstName);
        }

        /// <summary>
        /// Follows the signature of the <see cref="ParameterizedThreadStart"/> delegate.
        /// </summary>
        /// <param name="firstName"></param>
        public void PrintNameUsingObjectTypeArgument(object firstName)
        {
            Console.WriteLine(firstName);
        }

        public void DoWork(CancellationToken cancellationToken)
        {
            int i = 0;
            bool controlProp = true;
            while (!cancellationToken.IsCancellationRequested && controlProp)
             {
                 for (int count = 0; count < 10000; count++)
                 {
                     i++;
                     if (i == 5000000)
                     {
                         controlProp = false;
                         MessageBox.Show("Hi");
                        // throw new Exception("Oh my god.");
                     }
                 }
             }
        }

        public void PerformTask(string message)
        {
            //System.Timers.Timer timer = new Timer();
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            //timer.Enabled = true;
            //timer.AutoReset = false;
            //timer.Start();
            //timer.Interval = 5000;
            bool controlProp = true;
            int i = 0;

            while (controlProp)
           {
               for (int count = 0; count < 10000; count++)
               {
                   i++;
                   if (i == 5000000)
                   {
                       throw new Exception("Oh my god.");
                   }
               }
           }

           //     this.timer_Elapsed(null, null);

            //throw new Exception("Oh my god.");
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            throw new Exception("Oh my god.");
        }
    }
}
