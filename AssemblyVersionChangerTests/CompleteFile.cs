using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AssemblyInfoUtil;

using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace AssemblyVersionChangerTests
{
    [TestClass]
    public class CompleteFile
    {
        static string[] linesBefore = new string[]{
            @"using System.Reflection;",
            @"using System.Runtime.CompilerServices;",
            @"using System.Runtime.InteropServices;",
            @"",
            @"// General Information about an assembly is controlled through the following ",
            @"// set of attributes. Change these attribute values to modify the information",
            @"// associated with an assembly.",
            @"[assembly: AssemblyTitle(""AppName"")]",
            @"[assembly: AssemblyDescription("""")]",
            @"[assembly: AssemblyConfiguration("""")]",
            @"[assembly: AssemblyCompany(""CompanyName"")]",
            @"[assembly: AssemblyProduct(""assembly"")]",
            @"[assembly: AssemblyCopyright(""CompanyName. 2013"")]",
            @"[assembly: AssemblyTrademark("""")]",
            @"[assembly: AssemblyCulture("""")]",
            @"",
            @"// Setting ComVisible to false makes the types in this assembly not visible ",
            @"// to COM components.  If you need to access a type in this assembly from ",
            @"// COM, set the ComVisible attribute to true on that type.",
            @"[assembly: ComVisible(false)]",
            @"",
            @"// The following GUID is for the AnimationName of the typelib if this project is exposed to COM",
            @"[assembly: Guid(""66A7AB57-533C-4ECA-86C0-C74046E2EA2D"")]",
            @"// Version information for an assembly consists of the following four values:",
            @"//",
            @"//      Major Version",
            @"//      Minor Version ",
            @"//      Build Number",
            @"//      Revision",
            @"//",
            @"// You can specify all the values or you can default the Revision and Build Numbers ",
            @"// by using the '*' as shown below:",
            @"[assembly: AssemblyVersion(""1.0"")]",
            @"[assembly: AssemblyFileVersion(""1.0.0"")]",
        };
        static string[] linesAfter = new string[]{
            @"using System.Reflection;",
            @"using System.Runtime.CompilerServices;",
            @"using System.Runtime.InteropServices;",
            @"",
            @"// General Information about an assembly is controlled through the following ",
            @"// set of attributes. Change these attribute values to modify the information",
            @"// associated with an assembly.",
            @"[assembly: AssemblyTitle(""AppName"")]",
            @"[assembly: AssemblyDescription("""")]",
            @"[assembly: AssemblyConfiguration("""")]",
            @"[assembly: AssemblyCompany(""CompanyName"")]",
            @"[assembly: AssemblyProduct(""assembly"")]",
            @"[assembly: AssemblyCopyright(""CompanyName. 2013"")]",
            @"[assembly: AssemblyTrademark("""")]",
            @"[assembly: AssemblyCulture("""")]",
            @"",
            @"// Setting ComVisible to false makes the types in this assembly not visible ",
            @"// to COM components.  If you need to access a type in this assembly from ",
            @"// COM, set the ComVisible attribute to true on that type.",
            @"[assembly: ComVisible(false)]",
            @"",
            @"// The following GUID is for the AnimationName of the typelib if this project is exposed to COM",
            @"[assembly: Guid(""66A7AB57-533C-4ECA-86C0-C74046E2EA2D"")]",
            @"// Version information for an assembly consists of the following four values:",
            @"//",
            @"//      Major Version",
            @"//      Minor Version ",
            @"//      Build Number",
            @"//      Revision",
            @"//",
            @"// You can specify all the values or you can default the Revision and Build Numbers ",
            @"// by using the '*' as shown below:",
            @"[assembly: AssemblyVersion(""1.0"")]",
            @"[assembly: AssemblyFileVersion(""1.0.0"")]",
        };

        [TestMethod]
        public void TestFileWritingAllOptions()
        {
            AssemblyVersionChanger.Options options = new AssemblyVersionChanger.Options();
            options.Major = "2";
            options.Minor = "2";
            options.Revision = "2";
            options.Build = "2";
            options.InputFiles = new string[] { @"c:\AssemblyInfo.cs" };

            linesAfter[linesAfter.Length - 2] = @"[assembly: AssemblyVersion(""2.2"")]";
            linesAfter[linesAfter.Length - 1] = @"[assembly: AssemblyFileVersion(""2.2.2"")]";

            IFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\AssemblyInfo.cs", new MockFileData(String.Join("\n", linesBefore)) },
            });

            AssemblyVersionChanger avc = new AssemblyVersionChanger();
            avc.run(options, fileSystem);

            CollectionAssert.AreEqual(fileSystem.File.ReadAllLines(@"c:\AssemblyInfo.cs"), linesAfter);
        }

        [TestMethod]
        public void TestFileWritingMajor()
        {
            AssemblyVersionChanger.Options options = new AssemblyVersionChanger.Options();
            options.Major = "2";
            options.InputFiles = new string[] { @"c:\AssemblyInfo.cs" };

            linesAfter[linesAfter.Length - 2] = @"[assembly: AssemblyVersion(""2.0"")]";
            linesAfter[linesAfter.Length - 1] = @"[assembly: AssemblyFileVersion(""2.0.0"")]";

            IFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\AssemblyInfo.cs", new MockFileData(String.Join("\n", linesBefore)) },
            });

            AssemblyVersionChanger avc = new AssemblyVersionChanger();
            avc.run(options, fileSystem);

            CollectionAssert.AreEqual(fileSystem.File.ReadAllLines(@"c:\AssemblyInfo.cs"), linesAfter);
        }

        [TestMethod]
        public void TestFileWritingMinor()
        {
            AssemblyVersionChanger.Options options = new AssemblyVersionChanger.Options();
            options.Minor = "2";
            options.InputFiles = new string[] { @"c:\AssemblyInfo.cs" };

            linesAfter[linesAfter.Length - 2] = @"[assembly: AssemblyVersion(""1.2"")]";
            linesAfter[linesAfter.Length - 1] = @"[assembly: AssemblyFileVersion(""1.2.0"")]";

            IFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\AssemblyInfo.cs", new MockFileData(String.Join("\n", linesBefore)) },
            });

            AssemblyVersionChanger avc = new AssemblyVersionChanger();
            avc.run(options, fileSystem);

            CollectionAssert.AreEqual(fileSystem.File.ReadAllLines(@"c:\AssemblyInfo.cs"), linesAfter);
        }

        [TestMethod]
        public void TestFileWritingRevision()
        {
            AssemblyVersionChanger.Options options = new AssemblyVersionChanger.Options();
            options.Revision = "2";
            options.InputFiles = new string[] { @"c:\AssemblyInfo.cs" };

            linesAfter[linesAfter.Length - 2] = @"[assembly: AssemblyVersion(""1.0"")]";
            linesAfter[linesAfter.Length - 1] = @"[assembly: AssemblyFileVersion(""1.0.2"")]";

            IFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\AssemblyInfo.cs", new MockFileData(String.Join("\n", linesBefore)) },
            });

            AssemblyVersionChanger avc = new AssemblyVersionChanger();
            avc.run(options, fileSystem);

            CollectionAssert.AreEqual(fileSystem.File.ReadAllLines(@"c:\AssemblyInfo.cs"), linesAfter);
        }
    }
}
