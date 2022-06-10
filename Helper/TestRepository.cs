using SqlRawSpTestApp.Entities;
using SqlRawSpTestApp.Parameters;
using SqlRawSpTestApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRawSpTestApp.Helper
{
    public static class TestRepository
    {
        public static void TestRepo(this SchoolRepository repo, List<IParameter> parameters, string command)
        {
            try
            {
                Console.WriteLine("");
                Console.WriteLine("");

                Console.WriteLine($"{command}");
                Console.WriteLine("-------");

                var result = repo.GetList(parameters, command);

                foreach (School item in result)
                {
                    Console.WriteLine($"{item.Id} - {item.Name}");
                }

                Console.WriteLine($"{command}: Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{command}: {ex.Message}");
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
