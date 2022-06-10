using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlRawSpTestApp.Context;
using SqlRawSpTestApp.Entities;
using SqlRawSpTestApp.Helper;
using SqlRawSpTestApp.Parameters;
using SqlRawSpTestApp.Repository;

Console.WriteLine("Started");

var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true)
                 .AddUserSecrets<Program>();

var config = builder.Build();

var services = new ServiceCollection();

services.AddSqlServer<TestDbContext>(config.GetConnectionString("TestDb"),
                optionsAction: options => options.UseSqlServer());

var serviceProvider = services.BuildServiceProvider();
var context = serviceProvider.GetService<TestDbContext>();

Console.WriteLine("DB Context ready");



Console.WriteLine("Configuration loaded");

SchoolRepository repo = new SchoolRepository(context!);

List<IParameter> parameters = new List<IParameter>();

parameters.Add(new UrlParameter() { ParameterName = "Id", Operator = ">=", ParameterType = typeof(int), ParameterValue = "1" });
parameters.Add(new UrlParameter() { ParameterName = "Id", Operator = "<=", ParameterType = typeof(int), ParameterValue = "5" });

/*
 * for comparison in sql server management studio, fire this. I've used an Azure Sql database for testing
 * 
declare @list dbo.IntWhereClauses

insert into @list
values(1, 'Id', '>=', 1)
insert into @list
values(2, 'Id', '<=', 5)

print 'getlist 1'
exec School_GetList @list, 1
print 'getlist 2'
exec School_GetList2 @list, 1
print 'getlist 3'
exec School_GetList3 @list, 1
print 'getlist 4'
exec School_GetList4 @list, 1
print 'getlist 5'
exec School_GetList5 @list, 1
*/

repo.TestRepo(parameters, "School_GetList");
repo.TestRepo(parameters, "School_GetList2");
repo.TestRepo(parameters, "School_GetList3");
repo.TestRepo(parameters, "School_GetList4");
repo.TestRepo(parameters, "School_GetList5");

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("End");
