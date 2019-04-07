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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
           
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {                    
                    command.CommandText = @"INSERT INTO Applicant_Work_History 
                                        ([Id],[Applicant],[Company_Name],[Country_Code], [Location], [Job_Title], 
                                        [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_year])
                                        VALUES(@Id, @Applicant, @CompanyName, @CountryCode, @Location, @JobTitle,
                                        @JobDescription,@StartMonth,@StartYear,@EndMonth,@EndYear)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    command.Parameters.AddWithValue("@Location", poco.Location);
                    command.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select [Id],[Applicant],[Company_Name],[Country_Code], [Location], [Job_Title], [Job_Description], [Start_Month], [Start_Year], [End_Month], [End_year], Time_Stamp FROM Applicant_Work_History", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);

                    poco.TimeStamp = (byte[])reader[11];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Applicant_Work_History WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    command.CommandText = @"UPDATE Applicant_Work_History 
                                        SET 
                                            [Applicant]=@Applicant,
                                            [Company_Name]=@CompanyName,
                                            [Country_Code]=@CountryCode, 
                                            [Location]=@Location, 
                                            [Job_Title]=@JobTitle, 
                                            [Job_Description]=@JobDescription, 
                                            [Start_Month]=@StartMonth, 
                                            [Start_Year]=@StartYear, 
                                            [End_Month]=@EndMonth, 
                                            [End_Year]=@EndYear
                                            WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    command.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    command.Parameters.AddWithValue("@Location", poco.Location);
                    command.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    command.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    command.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    command.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    command.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    command.Parameters.AddWithValue("@EndYear", poco.EndYear);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
