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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO Company_Locations 
                                        ([Id],[Company],[Country_Code],[State_Province_Code],
                                        [Street_Address], [City_Town], [Zip_Postal_Code])
                                        VALUES(@Id, @Company, @CountryCode, @Province,
                                        @Street, @City,
                                        @PostalCode)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            CompanyLocationPoco[] pocos = new CompanyLocationPoco[2000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select [Id],[Company],[Country_Code],[State_Province_Code], [Street_Address], [City_Town], [Zip_Postal_Code], Time_Stamp FROM Company_Locations", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                    {
                        poco.Province = reader.GetString(3);
                    }
                    else
                    {
                        poco.Province = null;
                    }
                    if (!reader.IsDBNull(4))
                    {
                        poco.Street = reader.GetString(4);
                    }
                    else
                    {
                        poco.Street = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.City = reader.GetString(5);
                    }
                    else
                    {
                        poco.City = null;
                    }
                    if (!reader.IsDBNull(6))
                    {
                        poco.PostalCode = reader.GetString(6);
                    }
                    else
                    {
                        poco.PostalCode = null;
                    }

                    poco.TimeStamp = (byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Company_Locations WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    command.CommandText = @"UPDATE Company_Locations 
                                        SET 
                                            [Company]=@Company,
                                            [Country_Code]=@CountryCode,
                                            [State_Province_Code]=@Province, 
                                            [Street_Address]=@Street, 
                                            [City_Town]=@City, 
                                            [Zip_Postal_Code]=@PostalCode
                                            WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Company", poco.Company);
                    command.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
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
