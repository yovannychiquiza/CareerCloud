using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : BaseRepository, IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            foreach (ApplicantEducationPoco item in items)
            {
                cmd.CommandText = @"INSERT INTO[dbo].[Applicant_Educations]
                            ([Id]
                      ,[Applicant]
                      ,[Major]
                      ,[Certificate_Diploma]
                      ,[Start_Date]
                      ,[Completion_Date]
                      ,[Completion_Percent])
                 VALUES
                       (@Id, 
                       @Applicant, 
                       @Major, 
                       @Certificate_Diploma, 
                       @Start_Date, 
                       @Completion_Date, 
                       @Completion_Percent)";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                conn.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }


        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantEducationPoco> list = new List<ApplicantEducationPoco>();
            
            DataTable table;
            conn.Open();
            string sqlCommand = "select * from Applicant_Educations ";

            using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
            {
                table = new DataTable();
                a.Fill(table);
            }

            foreach (DataRow item in table.Rows)
            {
                ApplicantEducationPoco poco = new ApplicantEducationPoco();
                poco.Id = Guid.Parse(item["Id"].ToString());
                poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                poco.Major = item["Major"].ToString();
                poco.CertificateDiploma = item["Certificate_Diploma"].ToString();
                poco.StartDate = DateTime.Parse(item["Start_Date"].ToString());
                poco.CompletionDate = DateTime.Parse(item["Completion_Date"].ToString());
                poco.CompletionPercent = Byte.Parse(item["Completion_Percent"].ToString());

                list.Add(poco);
            }
            conn.Close();
            return list;
        }


        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantEducationPoco poco = new ApplicantEducationPoco();
           
            DataTable table;
            conn.Open();
            string sqlCommand = "select * from Applicant_Educations ";
        

            using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
            {
                table = new DataTable();
                a.Fill(table);
            }

            List<ApplicantEducationPoco> list = new List<ApplicantEducationPoco>();

            foreach (DataRow item in table.Rows)
            {
                poco = new ApplicantEducationPoco();
                poco.Id = Guid.Parse(item["Id"].ToString());
                poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                poco.Major = item["Major"].ToString();
                poco.CertificateDiploma = item["Certificate_Diploma"].ToString();
                poco.StartDate = DateTime.Parse(item["Start_Date"].ToString());
                poco.CompletionDate = DateTime.Parse(item["Completion_Date"].ToString());
                poco.CompletionPercent = Byte.Parse(item["Completion_Percent"].ToString());
                list.Add(poco);
            }

            poco = list.FirstOrDefault(where.Compile());

            conn.Close();
            return poco;
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {

            SqlConnection conn = new SqlConnection(_connStr); 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantEducationPoco item in items)
            {
                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations] WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            foreach (ApplicantEducationPoco item in items)
            {
                cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations] set                             
                                    [Major] = @Major,
                                    [Certificate_Diploma] = @Certificate_Diploma,
                                    [Start_Date] = @Start_Date,
                                    [Completion_Date] = @Completion_Date,
                                    [Completion_Percent] = @Completion_Percent
                                WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

            }
            conn.Open();
            int rowEffected = cmd.ExecuteNonQuery();
            conn.Close();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
