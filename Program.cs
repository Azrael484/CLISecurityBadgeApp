using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Catworx.BadgeMaker;

namespace CatWorx.BadgeMaker
{
    class Program
    {
    async static Task Main(string[] args)
        {
            List<Employee> employees = await PeopleFetcher.GetEmployees();
            Util.PrintEmployees(employees);
            Util.MakeCSV(employees);
            await Util.MakeBadges(employees);
        }
    }
}