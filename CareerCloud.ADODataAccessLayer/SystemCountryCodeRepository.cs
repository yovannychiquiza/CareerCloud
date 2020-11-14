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
    public class SystemCountryCodeRepository : BaseRepository, IDataRepository<SystemCountryCodePoco>
    {
        public void Add(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                           ([Code]
                                           ,[Name])
                                        VALUES
                                           (@Code, 
                                           @Name)";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);

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

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<SystemCountryCodePoco> list = new List<SystemCountryCodePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from System_Country_Codes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = item["Code"].ToString();
                    poco.Name = item["Name"].ToString();
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

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SystemCountryCodePoco poco = new SystemCountryCodePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from System_Country_Codes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<SystemCountryCodePoco> list = new List<SystemCountryCodePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new SystemCountryCodePoco();
                    poco.Code = item["Code"].ToString();
                    poco.Name = item["Name"].ToString();
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

        public void Remove(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[System_Country_Codes] WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
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

        public void Update(params SystemCountryCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (SystemCountryCodePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                       SET [Name] = @Name
                                    WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.Parameters.AddWithValue("@Name", item.Name);
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
