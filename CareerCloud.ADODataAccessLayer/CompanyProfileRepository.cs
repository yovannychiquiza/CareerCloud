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
    public class CompanyProfileRepository : BaseRepository, IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                           ([Id]
                                           ,[Registration_Date]
                                           ,[Company_Website]
                                           ,[Contact_Phone]
                                           ,[Contact_Name]
                                           ,[Company_Logo])
                                     VALUES
                                           (@Id, 
                                           @Registration_Date, 
                                           @Company_Website, 
                                           @Contact_Phone, 
                                           @Contact_Name, 
                                           @Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyProfilePoco> list = new List<CompanyProfilePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Profiles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.RegistrationDate = DateTime.Parse(item["Registration_Date"].ToString());
                    poco.CompanyWebsite = item["Company_Website"].ToString();
                    poco.ContactPhone = item["Contact_Phone"].ToString();
                    poco.ContactName = item["Contact_Name"].ToString();
                    poco.CompanyLogo = Encoding.ASCII.GetBytes(item["Company_Logo"].ToString());
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

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyProfilePoco poco = new CompanyProfilePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Profiles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyProfilePoco> list = new List<CompanyProfilePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyProfilePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.RegistrationDate = DateTime.Parse(item["Registration_Date"].ToString());
                    poco.CompanyWebsite = item["Company_Website"].ToString();
                    poco.ContactPhone = item["Contact_Phone"].ToString();
                    poco.ContactName = item["Contact_Name"].ToString();
                    poco.CompanyLogo = Encoding.ASCII.GetBytes(item["Company_Logo"].ToString());
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

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles] WHERE Id = @Id";
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

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                       SET [Registration_Date] = @Registration_Date, 
                                          [Company_Website] = @Company_Website, 
                                          [Contact_Phone] = @Contact_Phone, 
                                          [Contact_Name] = @Contact_Name, 
                                          [Company_Logo] = @Company_Logo 
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

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
