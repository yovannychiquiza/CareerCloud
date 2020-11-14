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
    public class ApplicantResumeRepository : BaseRepository, IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                            ([Id]
                                            ,[Applicant]
                                            ,[Resume]
                                            ,[Last_Updated])
                                        VALUES
                                            (@Id, 
                                            @Applicant, 
                                            @Resume, 
                                            @Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    //var ss = item.LastUpdated.GetValueOrDefault().ToString("yyyy-MM-dd");
                    //cmd.Parameters.AddWithValue("@Last_Updated", "2012-12-12 00:00:00.0000000");
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantResumePoco> list = new List<ApplicantResumePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Resumes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Resume = item["Resume"].ToString();
                    DateTime last;
                    DateTime.TryParse(item["Last_Updated"].ToString(), out last);
                    poco.LastUpdated = last;
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

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantResumePoco poco = new ApplicantResumePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Resumes ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<ApplicantResumePoco> list = new List<ApplicantResumePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new ApplicantResumePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Resume = item["Resume"].ToString();
                    DateTime last;
                    DateTime.TryParse(item["Last_Updated"].ToString(), out last);
                    poco.LastUpdated = last;
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

        public void Remove(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes] WHERE Id = @Id";
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

        public void Update(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                        SET [Applicant] = @Applicant, 
                                            [Resume] = @Resume, 
                                            [Last_Updated] = @Last_Updated 
                                            WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
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
        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }


    }
}
