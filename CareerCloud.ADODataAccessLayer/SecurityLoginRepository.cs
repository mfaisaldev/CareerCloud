using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO Security_Logins 
                                        ([Id],[Login],[Password],[Created_Date], [Password_Update_Date], 
                                        [Agreement_Accepted_Date], [Is_Locked],[Is_Inactive], [Email_Address],
                                        [Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language])
                                        VALUES(@Id, @Login, @Password, @Created, @PasswordUpdate, 
                                        @AgreementAccepted, @IsLocked,@IsInactive,@EmailAddress,
                                        @PhoneNumber,@FullName,@ForceChangePassword,@PrefferredLanguage)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Password", poco.Password);
                    command.Parameters.AddWithValue("@Created", poco.Created);
                    command.Parameters.AddWithValue("@PasswordUpdate", poco.PasswordUpdate);
                    command.Parameters.AddWithValue("@AgreementAccepted", poco.AgreementAccepted);
                    command.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    command.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@EmailAddress", poco.EmailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", poco.PhoneNumber);
                    command.Parameters.AddWithValue("@FullName", poco.FullName);
                    command.Parameters.AddWithValue("@ForceChangePassword", poco.ForceChangePassword);
                    command.Parameters.AddWithValue("@PrefferredLanguage", poco.PrefferredLanguage);
                    

                    conn.Open();
                    int rowInserted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] paramters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SecurityLoginPoco[] pocos = new SecurityLoginPoco[500];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("SELECT [Id],[Login],[Password],[Created_Date], [Password_Update_Date], [Agreement_Accepted_Date], [Is_Locked],[Is_Inactive], [Email_Address], [Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language], Time_Stamp FROM Security_Logins", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetString(1);
                    poco.Password = reader.GetString(2);
                    poco.Created = reader.GetDateTime(3);
                    if (!reader.IsDBNull(4))
                    {
                        poco.PasswordUpdate = reader.GetDateTime(4);
                    }
                    else
                    {
                        poco.PasswordUpdate = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.AgreementAccepted = reader.GetDateTime(5);
                    }
                    else
                    {
                        poco.AgreementAccepted = null;
                    }

                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    if (!reader.IsDBNull(9))
                    {
                        poco.PhoneNumber = reader.GetString(9);
                    }
                    else
                    {
                        poco.PhoneNumber = null;
                    }
                    poco.FullName = reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    if (!reader.IsDBNull(12))
                    {
                        poco.PrefferredLanguage = reader.GetString(12);
                    }
                    else
                    {
                        poco.PrefferredLanguage = null;
                    }
                    poco.TimeStamp = (byte[])reader[13];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Security_Logins WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SecurityLoginPoco poco in items)
                {
                    command.CommandText = @"UPDATE Security_Logins SET [Login]=@Login, [Password]=@Password," +
                                            "[Created_Date]=@Created, [Password_Update_Date]=@PasswordUpdate," +
                                            "[Agreement_Accepted_Date]=@AgreementAccepted, [Is_Locked]=@IsLocked," +
                                            "[Is_Inactive]=@IsInactive, [Email_Address]=@EmailAddress, " +
                                            "[Phone_Number]=@PhoneNumber, [Full_Name]=@FullName, [Force_Change_Password]=@ForceChangePassword, " +
                                            "[Prefferred_Language]=@PrefferredLanguage WHERE [Id]=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@Password", poco.Password);
                    command.Parameters.AddWithValue("@Created", poco.Created);
                    command.Parameters.AddWithValue("@PasswordUpdate", poco.PasswordUpdate);
                    command.Parameters.AddWithValue("@AgreementAccepted", poco.AgreementAccepted);
                    command.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    command.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    command.Parameters.AddWithValue("@EmailAddress", poco.EmailAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", poco.PhoneNumber);
                    command.Parameters.AddWithValue("@FullName", poco.FullName);
                    command.Parameters.AddWithValue("@ForceChangePassword", poco.ForceChangePassword);
                    command.Parameters.AddWithValue("@PrefferredLanguage", poco.PrefferredLanguage);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
