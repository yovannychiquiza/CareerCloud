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
    public class SecurityLoginRepository : BaseRepository, IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                           ([Id]
                                           ,[Login]
                                           ,[Password]
                                           ,[Created_Date]
                                           ,[Password_Update_Date]
                                           ,[Agreement_Accepted_Date]
                                           ,[Is_Locked]
                                           ,[Is_Inactive]
                                           ,[Email_Address]
                                           ,[Phone_Number]
                                           ,[Full_Name]
                                           ,[Force_Change_Password]
                                           ,[Prefferred_Language])
                                     VALUES
                                           (@Id, 
                                           @Login, 
                                           @Password, 
                                           @Created_Date, 
                                           @Password_Update_Date, 
                                           @Agreement_Accepted_Date, 
                                           @Is_Locked, 
                                           @Is_Inactive, 
                                           @Email_Address, 
                                           @Phone_Number, 
                                           @Full_Name, 
                                           @Force_Change_Password, 
                                           @Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<SecurityLoginPoco> list = new List<SecurityLoginPoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Logins ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = item["Login"].ToString();
                    poco.Password = item["Password"].ToString();
                    poco.Created = DateTime.Parse(item["Created_Date"].ToString());
                    if (item["Password_Update_Date"].ToString() != "") poco.PasswordUpdate = DateTime.Parse(item["Password_Update_Date"].ToString());
                    if (item["Agreement_Accepted_Date"].ToString() != "") poco.AgreementAccepted = DateTime.Parse(item["Agreement_Accepted_Date"].ToString());
                    poco.IsLocked = bool.Parse(item["Is_Locked"].ToString());
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
                    poco.EmailAddress = item["Email_Address"].ToString();
                    poco.PhoneNumber = item["Phone_Number"].ToString();
                    poco.FullName = item["Full_Name"].ToString();
                    poco.ForceChangePassword = bool.Parse(item["Force_Change_Password"].ToString());
                    poco.PrefferredLanguage = item["Prefferred_Language"].ToString();
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

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SecurityLoginPoco poco = new SecurityLoginPoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Logins ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<SecurityLoginPoco> list = new List<SecurityLoginPoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new SecurityLoginPoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Login = item["Login"].ToString();
                    poco.Password = item["Password"].ToString();
                    poco.Created = DateTime.Parse(item["Created_Date"].ToString());
                    if (item["Password_Update_Date"].ToString() != "") poco.PasswordUpdate = DateTime.Parse(item["Password_Update_Date"].ToString());
                    if (item["Agreement_Accepted_Date"].ToString() != "") poco.AgreementAccepted = DateTime.Parse(item["Agreement_Accepted_Date"].ToString()) ;
                    poco.IsLocked = bool.Parse(item["Is_Locked"].ToString());
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
                    poco.EmailAddress = item["Email_Address"].ToString();
                    poco.PhoneNumber = item["Phone_Number"].ToString();
                    poco.FullName = item["Full_Name"].ToString();
                    poco.ForceChangePassword = bool.Parse(item["Force_Change_Password"].ToString());
                    poco.PrefferredLanguage = item["Prefferred_Language"].ToString();
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

        public void Remove(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins] WHERE Id = @Id";
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

        public void Update(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                                        SET [Login] = @Login, 
                                            [Password] = @Password, 
                                            [Created_Date] = @Created_Date, 
                                            [Password_Update_Date] = @Password_Update_Date, 
                                            [Agreement_Accepted_Date] = @Agreement_Accepted_Date, 
                                            [Is_Locked] = @Is_Locked, 
                                            [Is_Inactive] = @Is_Inactive, 
                                            [Email_Address] = @Email_Address, 
                                            [Phone_Number] = @Phone_Number, 
                                            [Full_Name] = @Full_Name, 
                                            [Force_Change_Password] = @Force_Change_Password, 
                                            [Prefferred_Language] = @Prefferred_Language
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
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
