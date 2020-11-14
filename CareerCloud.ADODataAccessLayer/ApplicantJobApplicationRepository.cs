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
    public class ApplicantJobApplicationRepository : BaseRepository, IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                               ([Id]
                                               ,[Applicant]
                                               ,[Job]
                                               ,[Application_Date])
                                         VALUES
                                               (@Id, 
                                               @Applicant, 
                                               @Job, 
                                               @Application_Date)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

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

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantJobApplicationPoco> list = new List<ApplicantJobApplicationPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Job_Applications ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Job= Guid.Parse(item["Job"].ToString());
                    poco.ApplicationDate = DateTime.Parse(item["Application_Date"].ToString());
                    poco.TimeStamp = Encoding.ASCII.GetBytes(item["Time_Stamp"].ToString());
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

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
            try { 

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Job_Applications ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<ApplicantJobApplicationPoco> list = new List<ApplicantJobApplicationPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new ApplicantJobApplicationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.ApplicationDate = DateTime.Parse(item["Application_Date"].ToString());
                    poco.TimeStamp = Encoding.ASCII.GetBytes(item["Time_Stamp"].ToString());
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

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try { 
                cmd.Connection = conn;

                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications] WHERE Id = @Id";
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

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try 
            { 
                foreach (ApplicantJobApplicationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                       SET [Applicant] = @Applicant, 
                                          [Job] = @Job, 
                                          [Application_Date] = @Application_Date
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);
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
        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

    }
}
