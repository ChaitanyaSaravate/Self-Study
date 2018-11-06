using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicCSharpFeatures
{
    public class Helper
    {
        public delegate void DelForMethodWithOneParam(string str);

        public void MethodWithOneParameterAndNoReturnVal(string str)
        {
            Console.WriteLine("MethodWithOneParameterAndNoReturnVal() called with argument - " + str);
        }

        public void MethodWithTwoParameterAndNoReturnVal(string str, string str2)
        {
            Console.WriteLine("MethodWithTwoParameterAndNoReturnVal() called with arguments - " + str + " and " + str2);
        }

	    public static bool Compare(int num)
	    {
		    return num > 10;
	    }
    }

    interface Interface1
    {
        void SayHello();
    }

    interface Interface2 : Interface1
    {
        void SayHi();
    }

    public class TrialClass : Interface2
    {
        public void SayHello()
        {
            
        }

        public void SayHi()
        {
            
        }
    }
}
