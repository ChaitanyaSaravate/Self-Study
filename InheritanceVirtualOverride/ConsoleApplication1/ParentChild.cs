using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
	public class Parent : IDisposable
	{
		public string Name;

		public Parent()
		{
			Console.WriteLine("In Parent.");
		}

		public Parent(string name)
		{
			Name = name;
			Console.WriteLine("In Parent with parameter");
		}

		static Parent()
		{
			Console.WriteLine("In Static Parent");
		}

		~Parent()
		{
			Console.WriteLine("In Parent destructor");
		}

		public void Dispose()
		{
			Console.WriteLine("Dispose Parent");
		}
	}

	public class Child : Parent, IDisposable
	{
		public Child()
		{
			Console.WriteLine("In Child");
		}

		public Child(string name) : base(name)
		{
			Console.WriteLine("In Child with parameter");
		}

		static Child()
		{
			Console.WriteLine("In Static Child");
		}

		~Child()
		{
			Console.WriteLine("In Child destructor");
		}

		public void Dispose()
		{
			Console.WriteLine("Dispose Child");
		}
	}

	class sample
	{
		int i;
		double k;
		public sample(int ii, double kk)
		{
			i = ii;
			k = kk;
			double j = (i) + (k);
			Console.WriteLine(j);
		}
		~sample()
		{
			double j = i - k;
			Console.WriteLine(j);
		}
	}

	class Box
	{
		public int volume;
		int width;
		int height;
		int length;
		public Box( int w, int h, int l)
		{
			width = w;
			height = h;
			length = l;
		}
		~Box()
		{
			volume = width * length * height;
			Console.WriteLine(volume);
		}

	}    

}
