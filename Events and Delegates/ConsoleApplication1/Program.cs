using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Delegates;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Entity entity = new Entity();

			// Create delegate instance.
			Delegates.Delegates.DelegateWithOneArgument instanceOfDelegate = new Delegates.Delegates.DelegateWithOneArgument(PrintMe);
			instanceOfDelegate += entity.MethodWithOneParameter;
			instanceOfDelegate("This is my first delegate.");

			instanceOfDelegate -= PrintMe;
			Console.WriteLine("Removed static method from delegate.");
			instanceOfDelegate("Calling again.");

			// Shorthand for creating delegate instance.  Just assign method to the instance without "new".
			Delegates.Delegates.DelegateWithReturnValue instanceOfAnotherDelegate = SayHello;
			Console.WriteLine(instanceOfAnotherDelegate("Chaitanya"));

			instanceOfAnotherDelegate += entity.SayHello;
			Console.WriteLine(instanceOfAnotherDelegate("Chaitanya")); // This will print the return value of the last method added to delegate

			Events events = new Events();
			events.EventForDelegateWithOneArgument += new Delegates.Delegates.DelegateWithOneArgument(PrintMeUsingEvent);
			events.EventForDelegateWithOneArgument += entity.MethodWithOneParameterUsingEvent;

			events.EventForDelegateWithReturnValue += SayHelloUsingEvent;

			events.EventForDelegateWithReturnValue += entity.SayHelloUsingEvent;
			events.RaiseEvents();

			List<Delegates.Delegates.DelegateWithoutParametersAndReurnValue> collectionOfDelegateInstances = new List<Delegates.Delegates.DelegateWithoutParametersAndReurnValue>();
			for (int i = 0; i < 10; i++)
			{
				// var c = i; // If you uncomment this line and use variable c instead of i in next line, numbers from 0 to 9 will be printed. If it's commented 10 will be printed 10 times.
				collectionOfDelegateInstances.Add(delegate { Console.WriteLine(i); });
			}

			foreach (var delegateInstance in collectionOfDelegateInstances)
			{
				delegateInstance();
			}

			var aa = new List<Func<string>>();
			var aastrings = new[] { "Pune", "Bangalore", "Mumbai", "Delhi", "Chandigarh" };
			foreach (var s in aastrings)
			{
				aa.Add(() => s);
			}

			foreach (var a in aa)
				Console.WriteLine(a());

			var l = new List<Func<string>>();
			var strings = new[] { "Pune" , "Bangalore", "Mumbai", "Delhi", "Chandigarh" };
			foreach (var s in strings)
			{
				var t = s;
				l.Add(() => t);
			}

			foreach (var a in l)
				Console.WriteLine(a());


			Console.ReadKey();
		}

		public static void PrintMe(string message)
		{
			Console.WriteLine("Priniting message from static method - " + message);
		}

		public static string SayHello(string name)
		{
			return "Hello from static method to you - " + name;
		}

		public static void PrintMeUsingEvent(string message)
		{
			Console.WriteLine("Priniting message from static method using event- " + message);
		}

		public static string SayHelloUsingEvent(string name)
		{
			return "Hello from static method to you using event - " + name;
		}
	}
}
