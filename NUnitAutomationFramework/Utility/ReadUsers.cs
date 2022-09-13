using Newtonsoft.Json.Linq;
using System.Configuration;

namespace NUnitAutomationFramework.Utility
{
    public class ReadUsers
    {
        public static string UserList(string userlist = "Default_Users")
        {
            /*
             * Use this method to read Random User from UsersList.json file
             * Pass UserList Type, for which you want data
             * It will return User based on the environment that this testcase is executing and userlist data passed
             */

            string? username;

            try
            {
                string? env = ConfigurationManager.AppSettings["Environment"];
                var random = new Random();
                string currentdirectory = Directory.GetParent(System.Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
                string? jsonstring = File.ReadAllText(currentdirectory + "\\Resources\\UsersList.json");
                var json = JToken.Parse(jsonstring);
                Object? o = json?.SelectToken(env)?.Value<object>(userlist);
                List<JToken> list = JArray.FromObject(o).ToList();
                int index = random.Next(list.Count);
                username = list[index].ToString();
            }
            catch(NullReferenceException e)
            {
                username = "";
                throw new Exception("Null Reference expection is thrown in Read Users method");
            }
            catch(Exception e1)
            {
                throw new Exception("Expection occured in ReadUser method");
            }
            return username;
        }

    }
}
