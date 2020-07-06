using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
	public class Base
	{
		public Base()
		{
			Console.WriteLine("In the constructor of Base class");
		}

		
		public void NonVirtualMethod()
		{
			Console.WriteLine("Base.NonVirtualMethod");
		}

		public virtual void VirtualMethod()
		{
			Console.WriteLine("Base.VirtualMethod");
		}

		/// <summary>
		/// Protected method
		/// </summary>
		protected void ProtectedMethod()
		{
			Console.WriteLine("Base.ProtectedMethod");
		}

		/// <summary>
		/// Protected method to demonstrate this gets called if not hidden in the derived classes.
		/// </summary>
		protected void ProtectedMethod2()
		{
			Console.WriteLine("Base.ProtectedMethod2");
		}

		/// <summary>
		/// Another virtual method to demonstrate that this gets called if no derived class overrides it.
		/// </summary>
		public virtual void VirtualMethod2()
		{
			Console.WriteLine("Base.VirtualMethod2");
		}

		/// <summary>
		/// Another virtual method. Override it in DerivedA but NOT in DerivedB to demonstrate that this gets called if no derived class overrides it.
		/// </summary>
		public virtual void VirtualMethod3()
		{
			Console.WriteLine("Base.VirtualMethod3");
		}

		/// <summary>
		/// Another virtual method. Keep it virtual in DerivedA and do NOT override in DerivedB.
		/// </summary>
		public virtual void VirtualMethod4()
		{
			Console.WriteLine("Base.VirtualMethod4");
		}

		/// <summary>
		/// Another virtual method. Hide it in DerivedA. Dont keep it virtual in DerivedA. That means it cannot be overriden in classes derived from DerivedA.
		/// </summary>
		public virtual void VirtualMethod5()
		{
			Console.WriteLine("Base.VirtualMethod5");
		}

		/// <summary>
		/// Another virtual method. Hide it in DerivedA. Keep it virtual in DerivedA. That means it CAN be overriden in classes derived from DerivedA.
		/// </summary>
		public virtual void VirtualMethod6()
		{
			Console.WriteLine("Base.VirtualMethod6");
		}

		public virtual void foo(string s = "base") { Console.WriteLine("base " + s); }
	}
}

namespace Learning 
{

    public class Base
    {
    // Base class
    }

    public class DerivedA : Base
    {
        // Level 1 Derived class
    }

    public class DervivedB : DerivedA
    {
        // Level 2 Dervived class
    }
}
