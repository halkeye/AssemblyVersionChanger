using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyInfoUtil;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, "blah");
            
        }
    }
}
