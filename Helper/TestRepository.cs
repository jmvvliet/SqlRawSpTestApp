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
        public static void TestRepo(this SchoolRepository repo, List<IParameter> parameters, string command, bool oldStyle = false)
        {
            string old = oldStyle ? "old style" : string.Empty;
            try
            {
                Console.WriteLine("");
                Console.WriteLine("");

                Console.WriteLine($"{command} {old}");
                Console.WriteLine("-------");

                List<School> result;

                if (!oldStyle)
                    result = repo.GetList(parameters, command);
                else
                    result = repo.GetListOldStyle(parameters, command);

                foreach (School item in result)
                {
                    Console.WriteLine($"{item.Id} - {item.Name}");
                }

                Console.WriteLine($"{command} {old}: Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{command} {old}: {ex.Message}");
            }

            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
