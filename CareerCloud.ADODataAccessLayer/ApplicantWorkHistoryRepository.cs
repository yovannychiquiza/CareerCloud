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
    public class ApplicantWorkHistoryRepository : BaseRepository, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                           ([Id]
                                           ,[Applicant]
                                           ,[Company_Name]
                                           ,[Country_Code]
                                           ,[Location]
                                           ,[Job_Title]
                                           ,[Job_Description]
                                           ,[Start_Month]
                                           ,[Start_Year]
                                           ,[End_Month]
                                           ,[End_Year])
                                     VALUES
                                           (@Id, 
                                           @Applicant, 
                                           @Company_Name, 
                                           @Country_Code, 
                                           @Location, 
                                           @Job_Title, 
                                           @Job_Description, 
                                           @Start_Month, 
                                           @Start_Year, 
                                           @End_Month, 
                                           @End_Year)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantWorkHistoryPoco> list = new List<ApplicantWorkHistoryPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Work_History ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.CompanyName = item["Company_Name"].ToString();
                    poco.CountryCode = item["Country_Code"].ToString();
                    poco.Location = item["Location"].ToString();
                    poco.JobTitle = item["Job_Title"].ToString();
                    poco.JobDescription = item["Job_Description"].ToString();
                    poco.StartMonth = short.Parse(item["Start_Month"].ToString());
                    poco.StartYear = int.Parse(item["Start_Year"].ToString());
                    poco.EndMonth = short.Parse(item["End_Month"].ToString());
                    poco.EndYear = int.Parse(item["End_Year"].ToString());
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

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Work_History ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<ApplicantWorkHistoryPoco> list = new List<ApplicantWorkHistoryPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new ApplicantWorkHistoryPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.CompanyName = item["Company_Name"].ToString();
                    poco.CountryCode = item["Country_Code"].ToString();
                    poco.Location = item["Location"].ToString();
                    poco.JobTitle = item["Job_Title"].ToString();
                    poco.JobDescription = item["Job_Description"].ToString();
                    poco.StartMonth = short.Parse(item["Start_Month"].ToString());
                    poco.StartYear = int.Parse(item["Start_Year"].ToString());
                    poco.EndMonth = short.Parse(item["End_Month"].ToString());
                    poco.EndYear = int.Parse(item["End_Year"].ToString());
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

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History] WHERE Id = @Id";
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

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (ApplicantWorkHistoryPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                        SET [Id] = @Id,
                                            [Applicant] = @Applicant, 
                                            [Company_Name] = @Company_Name, 
                                            [Country_Code] = @Country_Code, 
                                            [Location] = @Location, 
                                            [Job_Title] = @Job_Title, 
                                            [Job_Description] = @Job_Description, 
                                            [Start_Month] = @Start_Month, 
                                            [Start_Year] = @Start_Year, 
                                            [End_Month] = @End_Month, 
                                            [End_Year] = @End_Year
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", item.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", item.EndYear);
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
        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
