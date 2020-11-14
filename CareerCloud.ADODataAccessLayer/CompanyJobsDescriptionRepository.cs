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
    public class CompanyJobDescriptionRepository : BaseRepository, IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                           ([Id]
                                           ,[Job]
                                           ,[Job_Name]
                                           ,[Job_Descriptions])
                                     VALUES
                                           (@Id, 
                                           @Job, 
                                           @Job_Name, 
                                           @Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyJobDescriptionPoco> list = new List<CompanyJobDescriptionPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Jobs_Descriptions ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.JobName = item["Job_Name"].ToString();
                    poco.JobDescriptions = item["Job_Descriptions"].ToString();
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

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Jobs_Descriptions ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyJobDescriptionPoco> list = new List<CompanyJobDescriptionPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyJobDescriptionPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.JobName = item["Job_Name"].ToString();
                    poco.JobDescriptions = item["Job_Descriptions"].ToString();
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

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions] WHERE Id = @Id";
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

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyJobDescriptionPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                        SET [Job] = @Job, 
                                            [Job_Name] = @Job_Name, 
                                            [Job_Descriptions] = @Job_Descriptions
                                            WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);
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
