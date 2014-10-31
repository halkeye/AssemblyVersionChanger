using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class TestThreeVersion
    {
        [TestMethod]
        public void TestMajorWithThreeVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.1.0\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorWithThreeVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.0\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionWithThreeVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.2\")]", "Version was updated");
        }
        [TestMethod]
        public void TestMajorMinorRevisionBuildWithThreeVersion()
        {
            AssemblyInfoUtil.AssemblyVersionChanger.Options options = new AssemblyInfoUtil.AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";
            options.Build = "2";

            string preLine = "[assembly: AssemblyVersion(\"1.1.0\")]";
            string postLine = AssemblyInfoUtil.AssemblyVersionChanger.processLine(options, preLine);
            Assert.AreEqual(postLine, "[assembly: AssemblyVersion(\"2.2.2\")]", "Version was updated");
        }
    }
}
