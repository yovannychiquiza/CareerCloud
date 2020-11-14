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
    public class SystemLanguageCodeRepository : BaseRepository, IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                           ([LanguageID]
                                           ,[Name]
                                           ,[Native_Name])
                                     VALUES
                                           (@LanguageID, 
                                           @Name, 
                                           @Native_Name)";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<SystemLanguageCodePoco> list = new List<SystemLanguageCodePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from System_Language_Codes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = item["LanguageID"].ToString();
                    poco.Name = item["Name"].ToString();
                    poco.NativeName = item["Native_Name"].ToString();
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

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from System_Language_Codes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<SystemLanguageCodePoco> list = new List<SystemLanguageCodePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new SystemLanguageCodePoco();
                    poco.LanguageID = item["LanguageID"].ToString();
                    poco.Name = item["Name"].ToString();
                    poco.NativeName = item["Native_Name"].ToString();
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

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes] WHERE LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
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

        public void Update(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (SystemLanguageCodePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                        SET  [Name] = @Name, 
                                            [Native_Name] = @Native_Name
                                    WHERE [LanguageID] = @LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);
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
