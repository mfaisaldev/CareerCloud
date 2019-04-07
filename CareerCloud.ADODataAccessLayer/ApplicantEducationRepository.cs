﻿using CareerCloud.DataAccessLayer;
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
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
           
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantEducationPoco poco in items)
                {                    
                    command.CommandText = @"INSERT INTO Applicant_Educations 
                                        ([Id],[Applicant],[Major],[Certificate_Diploma], [Start_Date], [Completion_Date], [Completion_Percent])
                                        VALUES(@Id, @Applicant, @Major, @Certificate_Diploma, @Start_Date, @Completion_Date,
                                        @Completion_Percent)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Major", poco.Major);
                    command.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    command.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    command.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    command.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                
                SqlCommand command = new SqlCommand("Select Id, Applicant, Major, Certificate_Diploma, Start_Date, Completion_Date,Completion_Percent, Time_Stamp FROM Applicant_Educations", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                int position = 0;
                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                    {
                        poco.StartDate = reader.GetDateTime(4);
                    }
                    else
                    {
                        poco.StartDate = null;
                    }
                    if (!reader.IsDBNull(5))
                    {
                        poco.CompletionDate = reader.GetDateTime(5);
                    }
                    else
                    {
                        poco.CompletionDate = null;
                    }
                    if (!reader.IsDBNull(6))
                    {
                        poco.CompletionPercent = reader.GetByte(6);
                    }
                    else
                    {
                        poco.CompletionPercent = null;
                    }
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                
                foreach (ApplicantEducationPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Applicant_Educations WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantEducationPoco poco in items)
                {
                    command.CommandText = @"UPDATE Applicant_Educations 
                                        SET 
                                            [Applicant]=@Applicant,
                                            [Major]=@Major,
                                            [Certificate_Diploma]=@Certificate_Diploma, 
                                            [Start_Date]=@Start_Date, 
                                            [Completion_Date]=@Completion_Date, 
                                            [Completion_Percent]=@Completion_Percent
                                            WHERE Id = @Id ";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Major", poco.Major);
                    command.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    command.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    command.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    command.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
