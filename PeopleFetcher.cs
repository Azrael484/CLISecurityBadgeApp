using System;
using System.Collections.Generic;
using CatWorx.BadgeMaker;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Catworx.BadgeMaker
{
    class PeopleFetcher
    {
        // code from GetEmployees() in Program.cs

        async private static Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);
                for (int i = 0; i < 10; i++)
                {
                    JToken token = json.SelectToken($"results[{i}]")!;
                    JObject person = token.ToObject<JObject>()!;
                    Employee employee = new Employee(
                        "Cat Worx",
                        person.SelectToken("name.first")!.ToString(),
                        person.SelectToken("name.last")!.ToString(),
                        Int32.Parse(person.SelectToken("id.value")!.ToString().Replace("-", "")),
                        person.SelectToken("picture.thumbnail")!.ToString()
                    );
                    employees.Add(employee);
                }
            }
            return employees;
        }
        public static async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            // Collect user values until the value is an empty string
            while (true)
            {
                Console.WriteLine("Do you want to enter the data for a particular user?:(Enter 'Y' or 'y' for yes; press 'N' or 'n' to retrieve employees from the API; anything else will quit the app.)");
                string answer = Console.ReadLine() ?? "";
                if (answer.ToUpper() == "Y")
                {
                    Console.WriteLine("Enter company name: (leave empty to exit)");
                    string coName = Console.ReadLine() ?? "";
                    if (coName == "")
                    {
                        break;
                    }

                    Console.WriteLine("Enter first name:(leave empty to exit)");
                    string firstName = Console.ReadLine() ?? ""; // ?? is the null coalescing operator
                                                                 // Break if the user hits ENTER without typing a name
                    if (firstName == "")
                    {
                        break;
                    }

                    Console.Write("Enter last name:(leave empty to exit)");
                    string lastName = Console.ReadLine() ?? "";
                    if (lastName == "")
                    {
                        break;
                    }

                    Console.Write("Enter ID:");
                    int id = Int32.Parse(Console.ReadLine() ?? "");
                
                    Console.Write("Enter Photo URL:");
                    string photoUrl = Console.ReadLine() ?? "";
                    if (photoUrl == "")
                    {
                        photoUrl = "https://placehold.co/300x300.png";
                    }
                    Employee currentEmployee = new Employee(coName, firstName, lastName, id, photoUrl);
                    employees.Add(currentEmployee);
                }
                else if (answer.ToUpper() == "N")
                {
                    Console.WriteLine("The security badges for the following ten random employees were created:");
                    employees = await PeopleFetcher.GetFromApi();
                    break;
                }
                else
                {
                    break;
                }
            }

            return employees;
        }
    }
}