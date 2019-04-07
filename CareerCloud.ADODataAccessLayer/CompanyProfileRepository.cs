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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO Company_Profiles 
                                        ([Id],[Registration_Date],[Company_Website],[Contact_Phone], [Contact_Name], [Company_Logo])
                                        VALUES(@Id, @RegistrationDate, @CompanyWebsite, @ContactPhone, @ContactName, @CompanyLogo)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    command.Parameters.AddWithValue("@CompanyWebsite", poco.CompanyWebsite);
                    command.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    command.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    command.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);
                    
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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[3000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select [Id],[Registration_Date],[Company_Website],[Contact_Phone], [Contact_Name], [Company_Logo], Time_Stamp FROM Company_Profiles", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                    {
                        poco.CompanyWebsite = reader.GetString(2);
                    }
                    else
                    {
                        poco.CompanyWebsite = null;
                    }
                    poco.ContactPhone = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                    {
                        poco.ContactName = reader.GetString(4);
                    }
                    else
                    {
                        poco.ContactName = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.CompanyLogo = (byte[])reader[5];
                    }
                    else
                    {
                        poco.CompanyLogo = null;
                    }

                    poco.TimeStamp = (byte[])reader[6];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = "DELETE FROM Company_Profiles WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    command.CommandText = @"UPDATE Company_Profiles 
                                        SET 
                                            [Registration_Date]=@RegistrationDate,
                                            [Company_Website]=@CompanyWebsite,
                                            [Contact_Phone]=@ContactPhone, 
                                            [Contact_Name]=@ContactName, 
                                            [Company_Logo]=@CompanyLogo                                            
                                            WHERE [Id]=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    command.Parameters.AddWithValue("@CompanyWebsite", poco.CompanyWebsite);
                    command.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    command.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    command.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
