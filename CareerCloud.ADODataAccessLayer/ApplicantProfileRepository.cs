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
    public class ApplicantProfileRepository : BaseRepository, IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                       ([Id]
                                       ,[Login]
                                       ,[Current_Salary]
                                       ,[Current_Rate]
                                       ,[Currency]
                                       ,[Country_Code]
                                       ,[State_Province_Code]
                                       ,[Street_Address]
                                       ,[City_Town]
                                       ,[Zip_Postal_Code])
                                 VALUES
                                       (@Id, 
                                       @Login, 
                                       @Current_Salary, 
                                       @Current_Rate, 
                                       @Currency, 
                                       @Country_Code, 
                                       @State_Province_Code, 
                                       @Street_Address, 
                                       @City_Town, 
                                       @Zip_Postal_Code )";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

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
      
        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<ApplicantProfilePoco> list = new List<ApplicantProfilePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Profiles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = Guid.Parse(item["Login"].ToString());
                    poco.CurrentSalary = Decimal.Parse(item["Current_Salary"].ToString());
                    poco.CurrentRate = Decimal.Parse(item["Current_Rate"].ToString());
                    poco.Currency = item["Currency"].ToString();
                    poco.Country = item["Country_Code"].ToString();
                    poco.Province = item["State_Province_Code"].ToString();
                    poco.Street = item["Street_Address"].ToString();
                    poco.City = item["City_Town"].ToString();
                    poco.PostalCode = item["Zip_Postal_Code"].ToString();
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

     
        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            ApplicantProfilePoco poco = new ApplicantProfilePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Applicant_Profiles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<ApplicantProfilePoco> list = new List<ApplicantProfilePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new ApplicantProfilePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = Guid.Parse(item["Login"].ToString());
                    poco.CurrentSalary = Decimal.Parse(item["Current_Salary"].ToString());
                    poco.CurrentRate = Decimal.Parse(item["Current_Rate"].ToString());
                    poco.Currency = item["Currency"].ToString();
                    poco.Country = item["Country_Code"].ToString();
                    poco.Province = item["State_Province_Code"].ToString();
                    poco.Street = item["Street_Address"].ToString();
                    poco.City = item["City_Town"].ToString();
                    poco.PostalCode = item["Zip_Postal_Code"].ToString();
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

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles] WHERE Id = @Id";
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

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (ApplicantProfilePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                        SET [Login] = @Login, 
                                            [Current_Salary] = @Current_Salary, 
                                            [Current_Rate] = @Current_Rate, 
                                            [Currency] = @Currency, 
                                            [Country_Code] = @Country_Code, 
                                            [State_Province_Code] = @State_Province_Code, 
                                            [Street_Address] = @Street_Address, 
                                            [City_Town] = @City_Town, 
                                            [Zip_Postal_Code] = @Zip_Postal_Code 
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", item.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", item.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                    cmd.Parameters.AddWithValue("@City_Town", item.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);
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
        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }


    }
}
