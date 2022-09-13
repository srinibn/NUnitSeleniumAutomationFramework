using System.Xml;

namespace NUnitAutomationFramework.Utility
{
    public class ReadTestData
    {
        public static string? Value { get; set; }

        public static string GetTestData(string testcase_id, string node_element)
        {
            /*
             * Use this method to read testdata for respective testcase
             * Pass the testcase-id and element name for which user need a data. 
             * Ex: Node Element = "TestData" , Node Element = "Username"
             */
            Value = "";
            try
            {
                testcase_id = testcase_id.Split("_")[0];
                string? currentdirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(currentdirectory + "\\Resources\\TestData.xml");
                XmlNodeList? nodeList = xmldoc.SelectNodes("TestCases/TestCase");

                foreach (XmlNode node in nodeList)
                {
                    string? testcase = node["TestCaseId"]?.InnerText;

                    if (testcase != null && testcase.Equals(testcase_id))
                    {
                        Value = node[node_element]?.InnerText;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new Exception("Null Reference error occured in ReadTestData Method");
            }
            catch (Exception e1)
            {
                throw new Exception("Error occured in ReadTest Data Method, please check parameteres name");
            }

            return Value;
        }
    }
}
