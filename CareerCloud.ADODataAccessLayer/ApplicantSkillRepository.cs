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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
           
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantSkillPoco poco in items)
                {                    
                    command.CommandText = @"INSERT INTO Applicant_Skills 
                                        ([Id],[Applicant],[Skill],[Skill_Level], [Start_Month], 
                                        [Start_Year], [End_Month], [End_Year])
                                        VALUES(@Id, @Applicant, @Skill, @SkillLevel, @StartMonth,
                                        @StartYear, @EndMonth, 
                                        @EndYear)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1000];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("SELECT [Id],[Applicant],[Skill],[Skill_Level], [Start_Month], [Start_Year], [End_Month], [End_Year], Time_Stamp FROM Applicant_Skills", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while(reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);

                    poco.TimeStamp = (byte[])reader[8];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                
                foreach (ApplicantSkillPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Applicant_Skills WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (ApplicantSkillPoco poco in items)
                {
                    command.CommandText = @"UPDATE Applicant_Skills 
                                            SET 
                                            [Applicant]=@Applicant,
                                            [Skill]=@Skill,
                                            [Skill_Level]=@SkillLevel, 
                                            [Start_Month]=@StartMonth, 
                                            [Start_Year]=@StartYear, 
                                            [End_Month]=@EndMonth, 
                                            [End_Year]=@EndYear
                                            WHERE [Id] = @Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
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
