using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class DerivedB : DerivedA
    {
        /// <summary>
        /// Overrides base class's method
        /// </summary>
        public override void VirtualMethod()
        {
            Console.WriteLine("DerivedB.VirtualMethod");
            base.NonVirtualMethod(); // "base" keyword calls the method of the immediate base class (i.e. DerivedA class in this case) only.
            this.ProtectedMethod(); // Calls the method from the DerivedA class
            this.ProtectedMethod2(); // Calls the method from the BASE class
        }

        /// <summary>
        /// Another virtual method of DerivedA class which is overriden here.
        /// </summary>
        public override void VirtualMethod6()
        {
            Console.WriteLine("DerivedB.VirtualMethod6");
        }
    }
}
