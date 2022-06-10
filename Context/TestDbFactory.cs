using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRawSpTestApp.Context
{
    public class TestDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        protected readonly IConfigurationRoot _config;

        public TestDbContextFactory()
        {
            var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true)
                 .AddUserSecrets<Program>();

            _config = builder.Build();
        }

        public TestDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseSqlServer(_config.GetConnectionString("TestDb"));

            return new TestDbContext(optionsBuilder.Options);
        }
    }
}
