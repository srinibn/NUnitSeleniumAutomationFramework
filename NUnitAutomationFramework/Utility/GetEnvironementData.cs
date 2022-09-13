using Newtonsoft.Json.Linq;
using System.Configuration;


namespace NUnitAutomationFramework.Utility
{
    public class GetEnvironementData
    {
        public static string GetEnvData(string data = "WebURL")
        {
            /*
             * Use this method to get URL for the data based on enviornment
             * parameter data : Value you want to fetch from enviornment.json file
             */

            string url = "";
            try
            {
                string? env = ConfigurationManager.AppSettings["Environment"];
                string currentdirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
                string? jsonstring = File.ReadAllText(currentdirectory + "\\Resources\\Enviornment.json");
                var json = JToken.Parse(jsonstring);
                Object? o = json?.SelectToken(env)?.Value<object>(data);
                url = o?.ToString();

            }
            catch (NullReferenceException e)
            {
                throw new Exception("Null Reference error occured in ReadTestData Method");
            }
            catch (Exception e)
            {
                throw new Exception("Error occured in the GetEnvironment Data method, please check the parameters name");
            }

            return url;
        }
    }
}
