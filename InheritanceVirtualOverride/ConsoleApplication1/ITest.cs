using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public interface ITest
    {
        string TestMe(string test);
    }

    public class Test : ITest
    {
        #region Implementation of ITest

        public string TestMe(string test)
        {
            return test;
        }

        #endregion
    }

    public class InterfaceTester
    {
        public ITest TestIt()
        {
            Test test = new Test();
            return test;
        }
    }
}
