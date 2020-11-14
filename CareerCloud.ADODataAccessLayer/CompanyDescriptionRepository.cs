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
    public class CompanyDescriptionRepository : BaseRepository, IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                            ([Id]
                                            ,[Company]
                                            ,[LanguageID]
                                            ,[Company_Name]
                                            ,[Company_Description])
                                        VALUES
                                            (@Id, 
                                            @Company, 
                                            @LanguageID, 
                                            @Company_Name, 
                                            @Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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


        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyDescriptionPoco> list = new List<CompanyDescriptionPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Descriptions ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.LanguageId = item["LanguageID"].ToString();
                    poco.CompanyName = item["Company_Name"].ToString();
                    poco.CompanyDescription = item["Company_Description"].ToString();
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


        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Descriptions ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyDescriptionPoco> list = new List<CompanyDescriptionPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyDescriptionPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.LanguageId = item["LanguageID"].ToString();
                    poco.CompanyName = item["Company_Name"].ToString();
                    poco.CompanyDescription = item["Company_Description"].ToString();
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

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Descriptions] WHERE Id = @Id";
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

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                            SET [Company] = @Company, 
                                                [LanguageID] = @LanguageID, 
                                                [Company_Name] = @Company_Name, 
                                                [Company_Description] = @Company_Description 
                                                WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);
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
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
