using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class DerivedA : Base
    {
        public int Hi { get; set; }
        
        /// <summary>
        /// See that the "new" keyword is used to say that this method is hiding base class method.
        /// </summary>
        public new void NonVirtualMethod()
        {
            Console.WriteLine("DerivedA.NonVirtualMethod");
        }

        /// <summary>
        /// Virtual method is overriden here
        /// </summary>
        public override void VirtualMethod()
        {
            Console.WriteLine("DerivedA.VirtualMethod");
        }

        // Uncommenting this code will tell you that you cannot "override" and "hide" (using "new" keyword) at a time.
        //public new void VirtualMethod()
        //{
        //    Console.WriteLine("DerivedA.VirtualMethod");
        //}

        /// <summary>
        /// You can introduce another overload in derived class.
        /// </summary>
        /// <param name="withParameter">
        /// The with parameter.
        /// </param>
        public void VirtualMethod(string withParameter)
        {
            Console.WriteLine("DerivedA.VirtualMethod");
        }

        /// <summary>
        /// Protected method of the base class is hid here
        /// </summary>
        public new void ProtectedMethod()
        {
            Console.WriteLine("DerivedA.ProtectedMethod");
        }

        /// <summary>
        /// Overriden in this class but NOT in DerivedB to demonstrate that this gets called if no derived class overrides it.
        /// </summary>
        public override void VirtualMethod3()
        {
            Console.WriteLine("DerivedA.VirtualMethod3");
        }

        /// <summary>
        /// Another virtual method. Keep it virtual in DerivedA and do NOT override in DerivedB.
        /// </summary>
        public new virtual void VirtualMethod4()
        {
            Console.WriteLine("DerivedA.VirtualMethod4");
            this.ProtectedMethod(); // Calls the method from this class
            this.ProtectedMethod2(); // Calls the method from the BASE class
        }

        /// <summary>
        /// Another virtual method of Base class which is hid here. Note that this is not declared virtual here. That means classes
        /// deriving from DerivedA cannot override this method since it is nonvirtual here.
        /// </summary>
        public new void VirtualMethod5()
        {
            Console.WriteLine("DerivedA.VirtualMethod5");
        }

        /// <summary>
        /// Another virtual method. Hide it in DerivedA. Keep it virtual in DerivedA. That means it CAN be overriden in classes derived from DerivedA.
        /// </summary>
        public new virtual void VirtualMethod6()
        {
            Console.WriteLine("DerivedA.VirtualMethod6");
        }

	    public override void foo(string s = "derived") { Console.WriteLine("derived " + s); }
    }
}
