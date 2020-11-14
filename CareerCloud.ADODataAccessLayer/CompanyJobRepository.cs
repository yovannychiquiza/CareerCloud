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
    public class CompanyJobRepository : BaseRepository, IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                           ([Id]
                                           ,[Company]
                                           ,[Profile_Created]
                                           ,[Is_Inactive]
                                           ,[Is_Company_Hidden])
                                     VALUES
                                           (@Id, 
                                           @Company, 
                                           @Profile_Created, 
                                           @Is_Inactive, 
                                           @Is_Company_Hidden)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyJobPoco> list = new List<CompanyJobPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Jobs ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.ProfileCreated = DateTime.Parse(item["Profile_Created"].ToString());
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
                    poco.IsCompanyHidden = bool.Parse(item["Is_Company_Hidden"].ToString());
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

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyJobPoco poco = new CompanyJobPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Jobs ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyJobPoco> list = new List<CompanyJobPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyJobPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.ProfileCreated = DateTime.Parse(item["Profile_Created"].ToString());
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
                    poco.IsCompanyHidden = bool.Parse(item["Is_Company_Hidden"].ToString());
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

        public void Remove(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs] WHERE Id = @Id";
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

        public void Update(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyJobPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                        SET [Company] = @Company, 
                                            [Profile_Created] = @Profile_Created, 
                                            [Is_Inactive] = @Is_Inactive, 
                                            [Is_Company_Hidden] = @Is_Company_Hidden
                                            WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);
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
