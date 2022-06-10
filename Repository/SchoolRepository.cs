using Microsoft.Data.SqlClient;
using SqlRawSpTestApp.Context;
using SqlRawSpTestApp.Entities;
using SqlRawSpTestApp.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;

namespace SqlRawSpTestApp.Repository
{
    public class SchoolRepository
    {
        protected readonly TestDbContext _context;
        protected Microsoft.EntityFrameworkCore.DbSet<School> _dbSet;

        public SchoolRepository(TestDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = _context.Set<School>();
        }

        public List<School> GetList(List<IParameter> filter, string command)
        {
            StringBuilder query = new();
            query.Append($"EXEC {command}");

            List<SqlParameter> parameters = GetListParameters(filter);

            var result = _dbSet.FromSqlRaw<School>(query.ToString(), parameters.ToArray()).ToList();

            return result;
        }

        public List<School> GetListOldStyle(List<IParameter> filter, string command)
        {
            SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString());
            List<School> result = new();

            using (connection)
            {
                List<SqlParameter> parameters = GetListParameters(filter);

                using (SqlCommand cmd = new SqlCommand(command, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IntWhereClauses", parameters[0].Value);
                    cmd.Parameters[0].SqlDbType = SqlDbType.Structured;

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new School()
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }

        private List<SqlParameter> GetListParameters(List<IParameter> parameters)
        {
            List<SqlParameter> returnParams = new();

            returnParams.Add(GenerateParameter("IntWhereClauses", typeof(int), parameters.FindAll(c => c.ParameterType == typeof(int)), 1));

            return returnParams;
        }

        private SqlParameter GenerateParameter(string typeName, Type parameterType, List<IParameter> parameters, int autoIncrementSeed)
        {
            var dt = new DataTable();

            dt.Columns.Add("ParameterIndex", typeof(int));
            dt.Columns.Add("ParameterName", typeof(string));
            dt.Columns.Add("Operator", typeof(string));
            dt.Columns.Add("ParameterValue", parameterType);

            foreach (IParameter param in parameters)
            {
                DataRow dr = dt.NewRow();
                dr["ParameterIndex"] = autoIncrementSeed++;
                dr["ParameterName"] = param.ParameterName;
                dr["Operator"] = param.Operator;
                dr["ParameterValue"] = param.ParameterValue;
                dt.Rows.Add(dr);
            }

            return new SqlParameter($"@{typeName}", dt)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = $"[dbo].[{typeName}]",
                Direction = ParameterDirection.Input,
            };
        }
    }
}
