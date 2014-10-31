using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class TestOneVersion
    {
        [TestMethod]
        public void TestMajorWithOneVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";

            string preLine = "[assembly: AssemblyVersion(\"1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorWithOneVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";

            string preLine = "[assembly: AssemblyVersion(\"1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionWithOneVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";

            string preLine = "[assembly: AssemblyVersion(\"1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionBuildWithOneVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";
            options.Build = "2";

            string preLine = "[assembly: AssemblyVersion(\"1\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2\")]", "Version was updated");
        }
    }
}
