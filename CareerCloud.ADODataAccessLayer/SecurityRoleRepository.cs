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
    public class SecurityRoleRepository : BaseRepository, IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                           ([Id]
                                           ,[Role]
                                           ,[Is_Inactive])
                                     VALUES
                                           (@Id, 
                                           @Role, 
                                           @Is_Inactive)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            List<SecurityRolePoco> list = new List<SecurityRolePoco>();
            try
            {
                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Roles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                foreach (DataRow item in table.Rows)
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Role = item["Role"].ToString();
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
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

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SecurityRolePoco poco = new SecurityRolePoco();
            try
            {

                DataTable table;
                conn.Open();
                string sqlCommand = "select * from Security_Roles ";

                using (SqlDataAdapter a = new SqlDataAdapter(sqlCommand, conn))
                {
                    table = new DataTable();
                    a.Fill(table);
                }

                List<SecurityRolePoco> list = new List<SecurityRolePoco>();

                foreach (DataRow item in table.Rows)
                {
                    poco = new SecurityRolePoco();
                    poco.Id = Guid.Parse(item["Id"].ToString());
                    poco.Role = item["Role"].ToString();
                    poco.IsInactive = bool.Parse(item["Is_Inactive"].ToString());
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

        public void Remove(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;

                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles] WHERE Id = @Id";
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

        public void Update(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                                       SET [Role] = @Role, 
                                          [Is_Inactive] = @Is_Inactive
                                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
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
