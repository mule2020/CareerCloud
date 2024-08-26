using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connStr = "Server=localhost\\SQLEXPRESS;Database=JOB_PORTAL_DB;Trusted_Connection=True;";

        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles] 
                                        ([Id], [Role], [Is_Inactive])
                                        VALUES 
                                        (@Id, @Role, @IsInactive)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General Error: {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            var result = new List<SecurityRolePoco>();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Security_Roles]", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var poco = new SecurityRolePoco
                        {
                            Id = reader.GetGuid(0),
                            Role = reader.GetString(1),
                            IsInactive = reader.GetBoolean(2)
                        };
                        result.Add(poco);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public IList<SecurityRolePoco> GetList(Func<SecurityRolePoco, bool> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            var allItems = GetAll();
            return allItems.Where(where).ToList();
        }

        public SecurityRolePoco GetSingle(Func<SecurityRolePoco, bool> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            var allItems = GetAll();
            return allItems.FirstOrDefault(where);
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles] WHERE [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General Error: {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityRolePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                                        SET [Role] = @Role, 
                                            [Is_Inactive] = @IsInactive
                                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General Error: {ex.Message}");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand(name, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Item1, param.Item2);
                }

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Error: {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
