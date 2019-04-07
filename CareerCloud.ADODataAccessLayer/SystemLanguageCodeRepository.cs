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
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        //string connStr = @"Data Source=SAAD\SQLEXPRESS2014;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True";
        string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = @"INSERT INTO System_Language_Codes 
                                        ([LanguageID],[Name],[Native_Name])
                                        VALUES(@LanguageID, @Name, @NativeName)";

                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    command.Parameters.AddWithValue("@Name", poco.Name);
                    command.Parameters.AddWithValue("@NativeName", poco.NativeName);
                    
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

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[500];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand("Select LanguageID, Name, Native_Name FROM System_Language_Codes", conn);
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                int position = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);
                    
                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
            }
            return pocos.Where(a => a != null).ToList();
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = "DELETE FROM System_Language_Codes WHERE LanguageID=@LanguageID";
                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    conn.Open();
                    int RowDeleted = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    command.CommandText = @"UPDATE System_Language_Codes 
                                        SET [Name]=@Name,
                                            [Native_Name]=@NativeName
                                            WHERE LanguageID=@LanguageID";

                    command.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    command.Parameters.AddWithValue("@Name", poco.Name);
                    command.Parameters.AddWithValue("@NativeName", poco.NativeName);
                    
                    conn.Open();
                    int rowUpdated = command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }

}
