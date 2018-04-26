using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Scheduler.Extensions
{
    public static class RawSQLExtension
    {
        public static List<T> ExecRawSQL<T>(this ApplicationDbContext context, string query, SqlParameter[] parameters)
        {
            using (context)
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    if(parameters.Count() > 0){
                        command.Parameters.AddRange(parameters);
                    }
                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        List<T> list = new List<T>();
                        T obj = default(T);
                        while (result.Read())
                        {
                            obj = Activator.CreateInstance<T>();
                            foreach (PropertyInfo prop in obj.GetType().GetProperties())
                            {
                                try
                                {
                                    if (!object.Equals(result[prop.Name], DBNull.Value))
                                    {
                                        prop.SetValue(obj, result[prop.Name], null);
                                    }
                                } catch (Exception) {
                                }
                                
                            }
                            list.Add(obj);
                        }
                        context.Database.CloseConnection();
                        return list;
                    }
                    
                }
            }
        }
    }
}
