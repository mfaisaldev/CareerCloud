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
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
           
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {                    
                    command.CommandText = @"INSERT INTO Applicant_Profiles 
                                        ([Id],[Login],[Current_Salary],[Current_Rate], [Currency], 
                                        [Country_Code], [State_Province_Code], [Street_Address], 
                                        [City_Town], [Zip_Postal_Code])
                                        VALUES(@Id, @Login, @CurrentSalary, @CurrentRate, @Currency, @Country,
                                        @Province, @Street, @City, @PostalCode)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    command.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", poco.Currency);
                    command.Parameters.AddWithValue("@Country", poco.Country);
                    command.Parameters.AddWithValue("@Province", poco.Province);
                    command.Parameters.AddWithValue("@Street", poco.Street);
                    command.Parameters.AddWithValue("@City", poco.City);
                    command.Parameters.AddWithValue("@PostalCode", poco.PostalCode);
                    

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select [Id],[Login],[Current_Salary],[Current_Rate], [Currency], [Country_Code], [State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code], Time_Stamp FROM Applicant_Profiles", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    if (!reader.IsDBNull(2))
                    {
                        poco.CurrentSalary = reader.GetDecimal(2);
                    }
                    else
                    {
                        poco.CurrentSalary = null;
                    }
                    if (!reader.IsDBNull(3))
                    {
                        poco.CurrentRate = reader.GetDecimal(3);
                    }
                    else
                    {
                        poco.CurrentRate = null;
                    }
                    if (!reader.IsDBNull(4))
                    {
                        poco.Currency = reader.GetString(4);
                    }
                    else
                    {
                        poco.Currency = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.Country = reader.GetString(5);
                    }
                    else
                    {
                        poco.Country = null;
                    }
                    if (!reader.IsDBNull(6))
                    {
                        poco.Province = reader.GetString(6);
                    }
                    else
                    {
                        poco.Province = null;
                    }
                    if (!reader.IsDBNull(7))
                    {
                        poco.Street = reader.GetString(7);
                    }
                    else
                    {
                        poco.Street = null;
                    }
                    if (!reader.IsDBNull(8))
                    {
                        poco.City = reader.GetString(8);
                    }
                    else
                    {
                        poco.City = null;
                    }
                    if (!reader.IsDBNull(9))
                    {
                        poco.PostalCode = reader.GetString(9);
                    }
                    else
                    {
                        poco.PostalCode = null;
                    }
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                
                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = "DELETE FROM Applicant_Profiles WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantProfilePoco poco in items)
                {
                    command.CommandText = @"UPDATE Applicant_Profiles 
                                        SET 
                                            [Login]=@Login,
                                            [Current_Salary]=@CurrentSalary,
                                            [Current_Rate]=@CurrentRate, 
                                            [Currency]=@Currency, 
                                            [Country_Code]=@Country, 
                                            [State_Province_Code]=@Province, 
                                            [Street_Address]=@Street, 
                                            [City_Town]=@City, 
                                            [Zip_Postal_Code]=@PostalCode
                                            WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Login", poco.Login);
                    command.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    command.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    command.Parameters.AddWithValue("@Currency", poco.Currency);
                    command.Parameters.AddWithValue("@Country", poco.Country);
                    command.Parameters.AddWithValue("@Province", poco.Province);
                    command.Parameters.AddWithValue("@Street", poco.Street);
                    command.Parameters.AddWithValue("@City", poco.City);
                    command.Parameters.AddWithValue("@PostalCode", poco.PostalCode);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
