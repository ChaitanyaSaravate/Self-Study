using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace TestConsoleApp
{
	class Program
	{
		/// <summary>
		/// A delegate type which can accept first parameter as a refernce type
		/// </summary>
		/// <typeparam name="T1"></typeparam>
		/// <typeparam name="T2"></typeparam>
		/// <param name="refParameter"></param>
		/// <param name="param2"></param>
		delegate void ActionDelegateAcceptingRefParameter<T1, T2>(ref T1 refParameter, T2 param2);

		static void Main(string[] args)
		{
			ThreadStart threadStart = WriteOnOtherThread; // Instantiate ThreadStart delegate.
			Thread parallelThread = new Thread(threadStart);
			parallelThread.Start(); // Start parallel thread.

			WriteOnMainThread(); // Call a method that writes something on the main thread.

			#region Demo that there is separate memory stack for each thread

			/* Below two lines of code show you that a separate copy of the counter variable is created on each thread's memory stack, and so the output is, predictably, ten times "Hi".
            */
			new Thread(CommonMethodToWriteToMultipleThreads).Start(); // Observe that the new thread is created and started in the same line.
			CommonMethodToWriteToMultipleThreads(); // Call the same method on the main thread.

			#endregion

			#region Demo that Threads share data if they have a common reference to the same object instance
			/*
             * Observe that the "Instance variable Done set to true." is printed once instead of twice because first thread chnages the value of the "instanceVariable_Done" variable and that's shared/available
             * for the other thread.
             * ANother way of sharing data is to use static variables and this too is demonstrated here because "Static variable Done set to true."is printed once instead of twice.
             */
			ThreadTest tt = new ThreadTest();
			new Thread(tt.InstanceMethodUsingInstanceVariable).Start(); // Start a new thread using InstanceMethodUsingInstanceVariable method and  instance of ThreadTest
			tt.InstanceMethodUsingInstanceVariable(); // Call the same method from main thread using the same instance of ThreadTest
			#endregion

			#region Demo Thread.Join()

			Thread threadToDemoJoin = new Thread(MethodUsedToDemoThreadJoinMethod);
			threadToDemoJoin.Start();
			threadToDemoJoin.Join(); // The Join() makes main thread (or calling thread) wait until thread which is joined gets over. If you comment this line, you can see that the "MethodUsedToDemoThreadJoinMethod2" is executed parallely. 
			MethodUsedToDemoThreadJoinMethod2();

			#endregion

			#region Demo Thread.Sleep()

			//  Thread.Sleep(5000); // If you comment this, you can see that following line is printed immediately.
			Console.WriteLine("Writing after sleeping this thread for 5000 milliseconds.");
			// Note: Thread.Sleep(0) relinquishes the thread’s current time slice immediately, voluntarily handing over the CPU to other threads. Framework 4.0’s new Thread.Yield() method does the same thing — except that it relinquishes only to threads running on the same processor.

			#endregion

			#region Demo ParameterizedThreadStart delegate

			// ParameterizedThreadStart requires a method which returns nothing and accepts argument of type "object".
			ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(tt.PrintNameUsingObjectTypeArgument);
			Thread threadRunningParameterizedMethod = new Thread(parameterizedThreadStart);
			threadRunningParameterizedMethod.Start("Called using ParameterizedThreadStart"); // Observe that actual argument is passed to the "Start" method.

			// A shorthand for above 3 lines.
			new Thread(tt.PrintNameUsingObjectTypeArgument).Start("Called again using ParameterizedThreadStart"); // Observe that actual argument is passed to the "Start" method.

			// Using lambda expression to pass method to Thread which takes any number of arguments.
			new Thread(() => tt.PrintName("Chaitanya")).Start(); // Observe that argument is passed to the actual method and not to the "Start" method.
			new Thread(() => tt.PrintFullName("Chaitanya", "Saravate")).Start();

			#region lambda expressions and captured variables

			/*
             * As we saw, a lambda expression is the most powerful way to pass data to a thread. 
             * However, you must be careful about accidentally modifying captured variables after starting the thread, because these variables are shared. 
             */
			for (int i = 0; i < 10; i++)
			{
				new Thread(() => Console.Write(i)).Start(); // Output is nondeterministic since 'i' gets modified.

				// Note: Right way to do the above operation. Variable temp is now local to each loop iteration. Therefore, each thread captures a different memory location and there’s no problem. 
				//int temp = i;
				//new Thread(() => Console.Write(temp)).Start(); 
			}

			string str = "First String";
			new Thread(() => Console.WriteLine(str)).Start(); // This thread too prints modified value of the str variable. Because both lambda expressions capture the same str variable.

			str = "Modified string";
			new Thread(() => Console.WriteLine(str)).Start(); // This thread  prints modified value of the str variable.

			#endregion

			new Thread(() =>
						   {
							   Console.WriteLine("Running on another thread");
							   Console.WriteLine(
								   " and that too using anonymous method and lambda expressions passed to Thread's constructor");
						   }).Start();

			#endregion

			#region Demo background thread

			Thread backgroundThread = new Thread(WriteOnBackgroundThread);
			/* Note: By default, threads you create explicitly are foreground threads. 
             * Foreground threads keep the application alive for as long as any one of them is running, whereas background threads do not. Once all foreground threads finish, the application ends, and any background threads still running abruptly terminate.
             * 
             */
			backgroundThread.IsBackground = true;
			backgroundThread.Start();
			/* Note: If you wait for 5 seconds (sleep timing added on thread), you will get the line printed on your Console.
             * But If you terminate the Console, this thread will abruptly end. 
             * You can avoid this my explicitly Joining the thread. It makes main thread wait until background thread returns. Uncomment following line to understand this.
             */
			// backgroundThread.Join(); // Better to specify timeout so that thread returns if it fails to finish due to some reason. This is kind of backup exit strategy.

			#endregion

			#region Demo Exception Handling

			try
			{
				new Thread(() => MethodThrowingException(true)).Start();
			}
			catch (Exception)
			{
				// Note: This catch block will never execute because each thread has independent exceution path and so 
				// Any try/catch/finally blocks in scope when a thread is created are of no relevance to the thread when it starts executing.
				Console.WriteLine("Caugth exception occured raised from other thread.");
			}

			// The argument "false" will make correct code block execute which demos how exception should be handled on the individual threads.
			new Thread(() => MethodThrowingException(false)).Start();

			#endregion

			#region Demo C# 4.0 Task Parallel Library (TPL)

			//Note: Pooled threads are always background threads.

			System.Threading.Tasks.Task.Factory.StartNew(MethodForTPL_FollowActionDelegateSignature); // Starts a new thread
			try
			{
				Task<string> task =
				System.Threading.Tasks.Task.Factory.StartNew<string>(
					() =>
					MethodFollowingFuncDelegateSignature(
						"Called the thread using Task Library - TPL of C# 4.0. Used method following Func delegate signature."));
				Console.WriteLine(task.Result);

				task = Task.Factory.StartNew<string>(() => MethodForTPL_FollowFuncDelegateSignature2("Called the thread using Task Library - TPL of C# 4.0. Used method following Func delegate signature. Method throws exception."));
				Console.WriteLine(task.Result); // Without task.Result, exception will not be propogated to the main thread.
			}
			catch (Exception)
			{
				Console.WriteLine("Caught the exception raised from the thread created using Task.");
			}

			#endregion

			#region Demo ThreadPool.QueueUserWorkItem

			// Note: With ThreadPool.QueueUserWorkItem exception handling must be done on the new thread only.
			// Note: QueueUserWorkItem is not helpful when you want the return value.
			ThreadPool.QueueUserWorkItem(MethodForQueueUserWorkItem, "Calling thread using ThreadPool.QueueUserWorkItem");

			#endregion

			#region Demo Asynchronous Delegates

			/*
             * Asynchronous delegate invocations (asynchronous delegates for short) allow any number of typed arguments to be passed in both directions. 
             * Furthermore, unhandled exceptions on asynchronous delegates are conveniently rethrown on the original thread (or more accurately, the thread that calls EndInvoke), and so they don’t need explicit handling.
             */

			Func<string, string> funcDelegateInstance = new Func<string, string>(MethodFollowingFuncDelegateSignature);

			IAsyncResult asyncResult = funcDelegateInstance.BeginInvoke(
				"Creating thread using asynchronous delegates.", null, null);
			Console.WriteLine(funcDelegateInstance.EndInvoke(asyncResult)); // Note that without calling EndInvoke, you can nnot catch exception raised on the worker thread.


			IAsyncResult asyncResultWithCallBackHandler = funcDelegateInstance.BeginInvoke(
					"Creating thread using asynchronous delegates. Using callback handler this time.",
					AsyncCallBackHandler, funcDelegateInstance); // Note that arguments in this call to BeginInvoke are different.

			Func<string, string> funcDelegateInstanceToDemoExceptionHandling = MethodForTPL_FollowFuncDelegateSignature2;
			IAsyncResult asyncResultWithCallBackHandler2 =
				funcDelegateInstanceToDemoExceptionHandling.BeginInvoke(
					"Creating thread using asynchronous delegates. Using callback handler this time. Worker thread throws exception.",
					AsyncCallBackHandler, funcDelegateInstanceToDemoExceptionHandling);


			#endregion

			#region Parallelism

			List<string> collectionOfItemsToBeProcessedParallely = new List<string>(new[] { "Chaitanya", "Kedar", "Krupa", "Priyanka", "Ketaki" });

			// Parallelism where the operation does not have a return value.
			var result = Parallel.ForEach(collectionOfItemsToBeProcessedParallely, name => PrintName(name)); // Performs the action (passed as action delegate) on each item in the collection parallely.
			Parallel.ForEach(collectionOfItemsToBeProcessedParallely, PrintName); // Alternative syntax to do the same thing as above.


			// Parallelism where the operation does have a return value.
			Func<string, string> actualOperationToRunOnEachItemOfTheCollection = GetGreetings;
			ActionDelegateAcceptingRefParameter<string, string> methodToAggregateValuesReturnedFromEachParallelThread = CombineResults;
			string aggregatedResultOfResultsCollectedFromAllThreads = string.Empty;

			// Observe syntax of the ForEach
			Parallel.ForEach(
				collectionOfItemsToBeProcessedParallely, // The collection to perform action on. 
				() => "This is initial value of the thread-local variable", // The initial value of the thread-local variable
				(individualItemInTheCollection, parallelLoopState, theResultToPassOnToTheMethodWhichAggregatesResults) => actualOperationToRunOnEachItemOfTheCollection(individualItemInTheCollection), // Call to actual operation
				(greeting) => methodToAggregateValuesReturnedFromEachParallelThread(ref aggregatedResultOfResultsCollectedFromAllThreads, greeting)); // Call method which aggregates the result.

			Console.WriteLine(aggregatedResultOfResultsCollectedFromAllThreads);

			// Same thing but called bit differently while aggregating the result.
			aggregatedResultOfResultsCollectedFromAllThreads = string.Empty;
			Parallel.ForEach(collectionOfItemsToBeProcessedParallely,
				() => "This is initial value of the thread-local variable",
				(individualItemInTheCollection, parallelLoopState, theResultToPassOnToTheMethodWhichAggregatesResults) =>
					actualOperationToRunOnEachItemOfTheCollection(individualItemInTheCollection),
				(greeting) => // Instead of calling the method to aggregate results, use anonymous syntax. If you do it here only, you don't need delegate accpeting ref parameter.
				{
					lock (aggregatedResultOfResultsCollectedFromAllThreads)
						aggregatedResultOfResultsCollectedFromAllThreads += greeting;
				});

			Console.WriteLine(aggregatedResultOfResultsCollectedFromAllThreads);

			collectionOfItemsToBeProcessedParallely.Add("Unknown");
			collectionOfItemsToBeProcessedParallely.Add(null);

			ConcurrentBag<string> abc = new ConcurrentBag<string>();

			try
			{
				Parallel.ForEach(collectionOfItemsToBeProcessedParallely,
					() => string.Empty,
					(itemInTheCollection, loopState,
						variableWhichTracksReturnValueFromEachThread) => GetGreetings(itemInTheCollection),
					(greeting) =>
					{
						//lock (abc)
						{
							abc.Add(greeting);
						}

					});
			}
			catch (ArgumentNullException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (AggregateException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine(aggregatedResultOfResultsCollectedFromAllThreads);

			#endregion

			Console.ReadLine();
		}

		static void PrintName(string name)
		{
			Console.WriteLine("I am " + name);
		}

		static string GetGreetings(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			if (name.Equals("Unknown"))
			{
				throw new ArgumentException(nameof(name));
			}

			return "Hello " + name + "!";
		}

		static void CombineResults(ref string finalResult, string valueReturnedByOneParallelThread)
		{
			Console.WriteLine("Received result from thread : " + valueReturnedByOneParallelThread);
			finalResult += valueReturnedByOneParallelThread;
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

		static void MethodForQueueUserWorkItem(object message)
		{
			Console.WriteLine(message);
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
