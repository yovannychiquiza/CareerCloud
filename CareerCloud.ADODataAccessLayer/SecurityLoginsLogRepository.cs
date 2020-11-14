using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginsLogRepository : BaseRepository, IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                           ([Id]
                                           ,[Login]
                                           ,[Source_IP]
                                           ,[Logon_Date]
                                           ,[Is_Succesful])
                                     VALUES
                                           (@Id, 
                                           @Login, 
                                           @Source_IP, 
                                           @Logon_Date, 
                                           @Is_Succesful)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<SecurityLoginsLogPoco> list = new List<SecurityLoginsLogPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Logins_Log ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = Guid.Parse(item["Login"].ToString());
                    poco.SourceIP = item["Source_IP"].ToString();
                    poco.LogonDate = DateTime.Parse(item["Logon_Date"].ToString());
                    poco.IsSuccesful = bool.Parse(item["Is_Succesful"].ToString());
                    list.Add(poco);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;

        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Logins_Log ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<SecurityLoginsLogPoco> list = new List<SecurityLoginsLogPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new SecurityLoginsLogPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = Guid.Parse(item["Login"].ToString());
                    poco.SourceIP = item["Source_IP"].ToString();
                    poco.LogonDate = DateTime.Parse(item["Logon_Date"].ToString());
                    poco.IsSuccesful = bool.Parse(item["Is_Succesful"].ToString());
                    list.Add(poco);
                }

                poco = list.FirstOrDefault(where.Compile());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return poco;

        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                }
                conn.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                        SET [Login] = @Login, 
                                            [Source_IP] = @Source_IP, 
                                            [Logon_Date] = @Logon_Date, 
                                            [Is_Succesful] = @Is_Succesful
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                }
                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
