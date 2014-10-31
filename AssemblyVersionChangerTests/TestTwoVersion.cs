using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class TestTwoVersion
    {
        [TestMethod]
        public void TestMajorWithTwoVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.1\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorWithTwoVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionWithTwoVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionBuildWithTwoVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";
            options.Build = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2\")]", "Version was updated");
        }
    }
}
