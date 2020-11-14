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
    public class CompanyJobSkillRepository : BaseRepository, IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                           ([Id]
                                           ,[Job]
                                           ,[Skill]
                                           ,[Skill_Level]
                                           ,[Importance])
                                     VALUES
                                           (@Id, 
                                           @Job, 
                                           @Skill, 
                                           @Skill_Level, 
                                           @Importance)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyJobSkillPoco> list = new List<CompanyJobSkillPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Job_Skills ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.Skill = item["Skill"].ToString();
                    poco.SkillLevel = item["Skill_Level"].ToString();
                    poco.Importance = int.Parse(item["Importance"].ToString());
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

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Job_Skills ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyJobSkillPoco> list = new List<CompanyJobSkillPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyJobSkillPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Job = Guid.Parse(item["Job"].ToString());
                    poco.Skill = item["Skill"].ToString();
                    poco.SkillLevel = item["Skill_Level"].ToString();
                    poco.Importance = int.Parse(item["Importance"].ToString());
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

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills] WHERE Id = @Id";
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

        public void Update(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyJobSkillPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                        SET [Job] = @Job,
                                            [Skill] = @Skill,
                                            [Skill_Level] = @Skill_Level,
                                            [Importance] = @Importance
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Job", item.Job);
                    cmd.Parameters.AddWithValue("@Skill", item.Skill);
                    cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
    }
}
