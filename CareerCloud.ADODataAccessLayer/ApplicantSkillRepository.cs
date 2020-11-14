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
    public class ApplicantSkillRepository : BaseRepository, IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                            ([Id]
                                            ,[Applicant]
                                            ,[Skill]
                                            ,[Skill_Level]
                                            ,[Start_Month]
                                            ,[Start_Year]
                                            ,[End_Month]
                                            ,[End_Year])
                                        VALUES
                                            (@Id, 
                                            @Applicant, 
                                            @Skill, 
                                            @Skill_Level, 
                                            @Start_Month, 
                                            @Start_Year, 
                                            @End_Month, 
                                            @End_Year)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
       
        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantSkillPoco> list = new List<ApplicantSkillPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Skills ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Skill = item["Skill"].ToString();
                    poco.SkillLevel = item["Skill_Level"].ToString();
                    poco.StartMonth = byte.Parse(item["Start_Month"].ToString());
                    poco.StartYear = int.Parse(item["Start_Year"].ToString());
                    poco.EndMonth = byte.Parse(item["End_Month"].ToString());
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

       
        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantSkillPoco poco = new ApplicantSkillPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Skills ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<ApplicantSkillPoco> list = new List<ApplicantSkillPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new ApplicantSkillPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Applicant = Guid.Parse(item["Applicant"].ToString());
                    poco.Skill = item["Skill"].ToString();
                    poco.SkillLevel = item["Skill_Level"].ToString();
                    poco.StartMonth = byte.Parse(item["Start_Month"].ToString());
                    poco.StartYear = int.Parse(item["Start_Year"].ToString());
                    poco.EndMonth = byte.Parse(item["End_Month"].ToString());
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

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Skills] WHERE Id = @Id";
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

        public void Update(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (ApplicantSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                       SET [Applicant] = @Applicant,
                                          [Skill] = @Skill, 
                                          [Skill_Level] = @Skill_Level, 
                                          [Start_Month] = @Start_Month, 
                                          [Start_Year] = @Start_Year, 
                                          [End_Month] = @End_Month, 
                                          [End_Year] = @End_Year
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }


    }
}
