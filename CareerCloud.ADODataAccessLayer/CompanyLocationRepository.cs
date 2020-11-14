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
    public class CompanyLocationRepository : BaseRepository, IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                           ([Id]
                                           ,[Company]
                                           ,[Country_Code]
                                           ,[State_Province_Code]
                                           ,[Street_Address]
                                           ,[City_Town]
                                           ,[Zip_Postal_Code])
                                     VALUES
                                           (@Id, 
                                           @Company, 
                                           @Country_Code, 
                                           @State_Province_Code, 
                                           @Street_Address, 
                                           @City_Town,
                                           @Zip_Postal_Code )";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<CompanyLocationPoco> list = new List<CompanyLocationPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Locations ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.CountryCode = item["Country_Code"].ToString();
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

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            CompanyLocationPoco poco = new CompanyLocationPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Company_Locations ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<CompanyLocationPoco> list = new List<CompanyLocationPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new CompanyLocationPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Company = Guid.Parse(item["Company"].ToString());
                    poco.CountryCode = item["Country_Code"].ToString();
                    poco.Province= item["State_Province_Code"].ToString();
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

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations] WHERE Id = @Id";
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

        public void Update(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (CompanyLocationPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                                       SET [Company] = @Company,
                                          [Country_Code] = @Country_Code, 
                                          [State_Province_Code] = @State_Province_Code, 
                                          [Street_Address] = @Street_Address, 
                                          [City_Town] = @City_Town, 
                                          [Zip_Postal_Code] = @Zip_Postal_Code 
                                        WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Company", item.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
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
    }
}
