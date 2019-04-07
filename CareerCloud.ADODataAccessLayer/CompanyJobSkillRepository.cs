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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = @"INSERT INTO Company_Job_Skills 
                                        ([Id],[Job],[Skill],[Skill_Level], [Importance])
                                        VALUES(@Id, @Job, @Skill, @SkillLevel, @Importance)";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", poco.Importance);
                    
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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[5100];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select Id, Job, Skill, Skill_Level, Importance, Time_Stamp FROM Company_Job_Skills", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.Importance = reader.GetInt32(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = "DELETE FROM Company_Job_Skills WHERE Id=@Id";
                    command.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    command.CommandText = @"UPDATE Company_Job_Skills 
                                        SET 
                                            [Job]=@Job,
                                            [Skill]=@Skill,
                                            [Skill_Level]=@SkillLevel, 
                                            [Importance]=@Importance 
                                             WHERE Id= @Id";

                    command.Parameters.AddWithValue("@Id", poco.Id);
                    command.Parameters.AddWithValue("@Job", poco.Job);
                    command.Parameters.AddWithValue("@Skill", poco.Skill);
                    command.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
                    command.Parameters.AddWithValue("@Importance", poco.Importance);

                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
