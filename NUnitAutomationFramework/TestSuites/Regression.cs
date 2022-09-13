using NUnit.Framework;
using NUnitAutomationFramework.Base;
using NUnitAutomationFramework.Pages;
using NUnitAutomationFramework.Utility;

namespace NUnitAutomationFramework.TestSuites
{
    [Parallelizable(ParallelScope.Children)]
    public class Regression : BaseSetup
    {
        [Test, Category("Regression")]
        public void TC001_OpenTab()
        { 
            string? testcase = TestContext.CurrentContext.Test.MethodName;
            string testdata = ReadTestData.GetTestData(testcase, "TestData");
            extent_test.Value.Info("Testdata is : " +testdata);

            string user = ReadUsers.UserList("Registered_User");
            extent_test.Value.Info("Testdata is : " + user);
         
            HomePage page = new(GetDriver(), extent_test.Value);
            page.OpenTab();
            extent_test.Value.Pass("Open Tab Testcase is passed");
        }

        [Test, Category("Regression")]
        public void TC002_MouseOver()
        {
            //To get testdata from xml file
            string? testcase = TestContext.CurrentContext.Test.MethodName;
            string testdata = ReadTestData.GetTestData(testcase, "TestData");
            extent_test.Value.Info("Testdata is : " + testdata);

            //To get user from testdata file
            string username = ReadTestData.GetTestData(testcase, "Username");
            extent_test.Value.Info("Username from xml file is : " + username);

            // To get User from UserList file
            string user = ReadUsers.UserList("Registered_User");
            extent_test.Value.Info("user is : " + user);

            HomePage page = new(GetDriver(), extent_test.Value);
            page.MouseOver();
            extent_test.Value.Pass("MouseOver Testcase is passed");
        }
    }
}
