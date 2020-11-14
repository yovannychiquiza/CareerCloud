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
    public class CompanyJobEducationRepository : BaseRepository, IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                                           ([Id]
                                           ,[Job]
                                           ,[Major]
                                           ,[Importance])
                                     VALUES
                                           (@Id, 
                                           @Job, 
                                           @Major, 
                                           @Importance)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyJobEducationPoco> list = new List<CompanyJobEducationPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Job_Educations ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.Major = item["Major"].ToString();
                    poco.Importance = short.Parse(item["Importance"].ToString());
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


        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Job_Educations ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyJobEducationPoco> list = new List<CompanyJobEducationPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyJobEducationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.Major = item["Major"].ToString();
                    poco.Importance = short.Parse(item["Importance"].ToString());
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

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Educations] WHERE Id = @Id";
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

        public void Update(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyJobEducationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                                        SET [Id] = @Id, 
                                            [Job] = @Job, 
                                            [Major] = @Major, 
                                            [Importance] = @Importance
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Major", item.Major);
                    cmd.Parameters.AddWithValue("@Importance", item.Importance);
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
        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
