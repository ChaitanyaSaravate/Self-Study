using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base();
            b.NonVirtualMethod(); // Calls base class method
            b.VirtualMethod(); // Calls base class method
            b.VirtualMethod5(); // Calls base class method
                                // b.ProtectedMethod(); // Uncomment to see that protected method is not accessible outside the class

            DerivedA derivedA = new DerivedA();
            derivedA.NonVirtualMethod(); // Calls DerivedA class method
            derivedA.VirtualMethod(); // Calls DerivedA class method
            derivedA.VirtualMethod("Hi"); // Calls another overload of the method
            derivedA.ProtectedMethod(); // Calls the method that hides the base class's protected method

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Assigned instance of DerivedA to the variable of type Base");
            Console.WriteLine(Environment.NewLine);

            b = derivedA;
            b.foo(); // Observe the default parameter value. The default value declared in virtual method is used if the reference is of type Base class. Otherwise, the default value declared in overriden method is used. Note, in any case, overriden method is called.
            b.NonVirtualMethod(); // Calls base class method because variable's type at compile time is "Base" though this method is hid in the DerivedA class.
            b.VirtualMethod(); // Calls DerivedA class method because its overriden in DerivedA.
            ((DerivedA)b).VirtualMethod("Hi"); // Since b is of type Base, we need to typecast it to make second overload available.
            ((DerivedA)b).ProtectedMethod(); // This call succeds because of type casting. And the method thats called is not 'protected' in the DerviedA class.
            b.VirtualMethod5(); // Calls base class method because this is not overriden in the DerivedA.
            derivedA.VirtualMethod5(); // Calls method from DerivedA because its declared there again.

            DerivedB derivedB = new DerivedB();
            derivedB.VirtualMethod2(); // Calls method of the BASE class because its neither overriden in DerivedB nor in DerivedA
            derivedB.VirtualMethod3(); // Calls method of the DerivedA since this is overriden in DerivedA.
            derivedB.VirtualMethod4(); // Calls method of the DerivedA because DerivedA is a base class of the DerivedB and this method was declared virtual again (i.e. hiding method from Base class) in DerivedA too. 
            derivedB.VirtualMethod5(); // Calls method from the DerivedA since DerivedA is base class for DerivedB and this method is declared newly in DerivedA i.e. it hides method from the Base class.
            derivedB.VirtualMethod6(); // Calls method from DerivedB

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Assigned instance of DerivedB to the variable of type Base");
            Console.WriteLine(Environment.NewLine);

            b = derivedB;
            b.NonVirtualMethod(); // Calls method from base class since this method is not hidden in the derived class
            b.VirtualMethod(); // Calls DerivedB class method
            b.VirtualMethod6(); // Calls method from Base class (note that b is a variable of type Base and this method call does not call virtual method from DerivedA!)

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Assigned instance of DerivedB to the variable of type DerivedA");
            Console.WriteLine(Environment.NewLine);

            b = derivedA;
            b.VirtualMethod6();

            derivedA = derivedB;
            derivedA.VirtualMethod(); // Calls the method of DerivedB
            derivedA.NonVirtualMethod(); // Calls the method of DerivedA.
            derivedA.ProtectedMethod(); // Calls the method in the DerivedA since this method is "new" and public (not protected) in DerivedA
            derivedA.VirtualMethod6(); // Calls method from DerivedB since it is overriden in DerivedB and derivedA is a variable of type DerivedA. Compare it with the "b.VirtualMethod6();" line above.

            Console.WriteLine("Test construction, destruction.");
            using (Child p = new Child("Chaitanya"))
            {
                Console.WriteLine("Checking construction, destruction.");
            }
            Console.WriteLine("Test construction, destruction FINISHED.");

            //			sample s = new sample(8, 2.5);

            // Trial - trial
            Console.ReadKey();
        }
    }
}
