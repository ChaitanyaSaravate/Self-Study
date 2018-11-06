using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delegates
{
    public class Events
    {
        public event Delegates.DelegateWithOneArgument EventForDelegateWithOneArgument;

        public event Delegates.DelegateWithReturnValue EventForDelegateWithReturnValue;

        public void RaiseEvents()
        {
            this.EventForDelegateWithOneArgument("This is my first event");
            Console.WriteLine(this.EventForDelegateWithReturnValue("Amey"));
        }
    }
}
