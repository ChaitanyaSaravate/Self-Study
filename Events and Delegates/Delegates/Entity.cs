using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegates
{
    public class Entity
    {
        public void MethodWithOneParameter(string firstPara)
        {
            Console.WriteLine("Inside Entity.MethodWithOneParameter(). Argument = " + firstPara);
        }

        public string SayHello(string name)
        {
            return "Hello " + name;
        }

        public void MethodWithOneParameterUsingEvent(string firstPara)
        {
            Console.WriteLine("Inside Entity.MethodWithOneParameterUsingEvent(). Argument = " + firstPara);
        }

        public string SayHelloUsingEvent(string name)
        {
            return "Hello from event handler " + name;
        }
    }
}
