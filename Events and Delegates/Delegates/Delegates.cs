using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegates
{
	// Learning : All delegates in C# derive from MulticastDelegate internally.
	public class Delegates
	{
		public delegate void DelegateWithOneArgument(string firstArgument);

		public delegate string DelegateWithReturnValue(string sayHi);

		public delegate void DelegateWithoutParametersAndReurnValue();


		public delegate void SampleDelegate();

		void Test()
		{
			List<SampleDelegate> samples = new List<SampleDelegate>();
			for (int i = 0; i < 10; i++)
			{
				samples.Add(delegate { Console.WriteLine(i); });
			}

			foreach (var sample in samples)
			{
				sample();
			}

			int count = 10;
			Func<int, int> func = delegate(int i) { Console.WriteLine(count); return i * 2; };
				//delegate() => { Console.WriteLine(count); return 10; }
			Console.WriteLine(func(10));
		}


	}
}
