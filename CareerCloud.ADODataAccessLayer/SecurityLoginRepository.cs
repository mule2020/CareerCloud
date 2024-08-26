using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connStr = "Server=localhost\\SQLEXPRESS;Database=JOB_PORTAL_DB;Trusted_Connection=True;";

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins] 
                                        ([Id], [Login], [Password], [Created_Date], [Password_Update_Date], 
                                         [Agreement_Accepted_Date], [Is_Locked], [Is_Inactive], [Email_Address], 
                                         [Phone_Number], [Full_Name], [Force_Change_Password], [Prefferred_Language])
                                        VALUES 
                                        (@Id, @Login, @Password, @Created_Date, @Password_Update_Date, 
                                         @Agreement_Accepted_Date, @Is_Locked, @Is_Inactive, @Email_Address, 
                                         @Phone_Number, @Full_Name, @Force_Change_Password, @Prefferred_Language)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", (object)poco.PasswordUpdate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", (object)poco.AgreementAccepted ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var result = new List<SecurityLoginPoco>();
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Security_Logins]", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var poco = new SecurityLoginPoco
                        {
                            Id = reader.GetGuid(0),
                            Login = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Password = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Created = reader.GetDateTime(3),
                            PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                            IsLocked = reader.GetBoolean(6),
                            IsInactive = reader.GetBoolean(7),
                            EmailAddress = reader.IsDBNull(8) ? null : reader.GetString(8),
                            PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9),
                            FullName = reader.IsDBNull(10) ? null : reader.GetString(10),
                            ForceChangePassword = reader.GetBoolean(11),
                            PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12),
                            TimeStamp = (byte[])reader[13]
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

        public IList<SecurityLoginPoco> GetList(Func<SecurityLoginPoco, bool> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var allItems = GetAll();
            return allItems.Where(where).ToList();
        }

        public SecurityLoginPoco GetSingle(Func<SecurityLoginPoco, bool> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var allItems = GetAll();
            return allItems.FirstOrDefault(where);
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins] WHERE [Id] = @Id";
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

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                foreach (SecurityLoginPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                                        SET [Login] = @Login, 
                                            [Password] = @Password, 
                                            [Created_Date] = @Created_Date, 
                                            [Password_Update_Date] = @Password_Update_Date, 
                                            [Agreement_Accepted_Date] = @Agreement_Accepted_Date, 
                                            [Is_Locked] = @Is_Locked, 
                                            [Is_Inactive] = @Is_Inactive, 
                                            [Email_Address] = @Email_Address, 
                                            [Phone_Number] = @Phone_Number, 
                                            [Full_Name] = @Full_Name, 
                                            [Force_Change_Password] = @Force_Change_Password, 
                                            [Prefferred_Language] = @Prefferred_Language
                                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", (object)poco.PasswordUpdate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", (object)poco.AgreementAccepted ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

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
