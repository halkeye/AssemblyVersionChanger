using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyInfoUtil;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class TestFourVersion
    {
        [TestMethod]
        public void TestMajorWithFourVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.1.0.0\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorWithFourVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.0.0\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionWithFourVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.2.0\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionBuildWithFourVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";
            options.Build = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.2.2\")]", "Version was updated");
        }
    }
}
