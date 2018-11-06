using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicCSharpFeatures
{
    using System;

    class Program
    {
        private delegate void DelForAnonymousMethod(string str);

        private delegate string DelForAnonymousMethod2(string str, string str2);

        static void Main(string[] args)
        {
            Interface1 trialClass = new TrialClass();
            trialClass.SayHello();

            if (trialClass is Interface1)
            {
                trialClass.SayHello();
            }

            if (trialClass is Interface2)
            {
                
            }
            
            
            Helper helper = new Helper();

            #region Anonymous Method/delegate and lamda expressions Demo

            string outerVariable = "I am declared outside of the lambda or anonymous delegate.";
            DelForAnonymousMethod delegate1_1 = delegate(string messageToPrint)
                                                  {
                                                      Console.WriteLine(messageToPrint);
                                                  };
            delegate1_1("Calling the delegate1 using anonymous delegate");

            // See how the anonymous delegate can be replaced easily with the lamda.
            DelForAnonymousMethod delegate1_2 = messageToPrint => { Console.WriteLine(messageToPrint); }; // This is also called "statement lamda" since it has some statemens in the {}.
            delegate1_2("Doing the same thind with lambda");

            DelForAnonymousMethod2 delForAnonymousMethod2_1 = delegate(string message1, string message2)
                                                                  {
                                                                      string result = message1 + message2;
                                                                      return result;
                                                                  };
            Console.WriteLine(delForAnonymousMethod2_1("First message ", " is appended to second message"));

            DelForAnonymousMethod2 delForAnonymousMethod2_2 = (message1, message2) => message1 + message2; // See how two arguments are passed using paranthesis.
            Console.WriteLine(delForAnonymousMethod2_2("Same thing ", " done with the lamda this time."));

            Predicate<string> predicate = delegate(string message)
                                              {
                                                  return message.Equals(outerVariable);
                                              };
            Console.WriteLine("Result of the string comparison : " + predicate("Hello"));

            Predicate<string> predicate2 = message => message.Equals(outerVariable);
            Console.WriteLine("Did the same string comparison with lamda. Result = " + predicate2("Hello"));

            int[] numbers = {10, 20, 30, 40, 50, 60, 70, 80, 90};
            Console.WriteLine("Count of numbers greater that 40 = " + Enumerable.Count(numbers, number => number > 40)); // See how easy it becomes to get the result using lambda.

	        var even = numbers.Where(Helper.Compare).ToList();

            string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            string xmlResource = resources.Where(res => res.EndsWith(".xml")).SingleOrDefault();

			#endregion

            Console.ReadLine();
        }
    }
}
