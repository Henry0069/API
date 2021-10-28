using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;

using Entity;
using Interface;


namespace Repository
{
    public class ApiRepository : IApiRepository
    {
        NpgsqlConnection cnx = new NpgsqlConnection();
        NpgsqlCommand com = null;
        NpgsqlDataReader dr = null;
        string sql = "";
        private readonly IConfiguration _Configuration;
        string myDb1ConnectionString = null;
        public ApiRepository(IConfiguration Configuration)
        {
             //TODO Revert conexção do banco no json.
             _Configuration = Configuration;
             myDb1ConnectionString = _Configuration.GetConnectionString("myDb1");
        }
    
        public async Task<IEnumerable<Info>> GetData()
        {
            cnx = new NpgsqlConnection(myDb1ConnectionString);
            
            sql = "SELECT * from APIData";
            var result = new List<Info>();
            try
            {
                if(cnx.State == ConnectionState.Closed)
                {  
                    await cnx.OpenAsync();
                }

                com = new NpgsqlCommand(sql,cnx);

                dr = com.ExecuteReader();

                while(dr.Read())
                {
                    result.Add(new Info() 
                    {  _ID_ = Convert.ToInt16(dr["_id_"]), 
                       _AGE_ = Convert.ToInt32(dr["_age_"]), 
                       _CITY_ = dr["_city_"].ToString(),
                       _NAME_ = dr["_name_"].ToString() 

                    });
                }

            }
            catch(SqlException ex)
            {
               if(ex != null)
               {
                   Console.WriteLine(ex);
               }
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    await cnx.CloseAsync();
                }
                cnx.Dispose();
            }

            return result; 

        }

          public async Task<string> InsertData(Info info)
        {
            cnx = new NpgsqlConnection(myDb1ConnectionString);

            sql = "INSERT INTO APIData (_name_, _city_, _age_) VALUES (@_NAME_, @_CITY_,@_AGE_)";            
            string x = "";

            try
            {
                if(cnx.State == ConnectionState.Closed)
                {  
                    await cnx.OpenAsync();
                }
                
                com = new NpgsqlCommand(sql,cnx);
                com.Parameters.Add(new SqlParameter ("@_NAME_", info._NAME_));
                com.Parameters.Add(new SqlParameter ("@_CITY_", info._CITY_));
                com.Parameters.Add(new SqlParameter ("@_AGE_", info._AGE_));

                dr = com.ExecuteReader();

                if(dr.Read() != true)
                {
                    x = "Insert was succeful realized!";
                }
                else
                {
                    x = "Insert has problems within the process";
                }

            }
             catch(SqlException ex)
            {
               if(ex != null)
               {
                   Console.WriteLine(ex);
               }
            }
             finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    await cnx.CloseAsync();
                }
                cnx.Dispose();
            }
           
            return x;
        } 
 
           public async Task<string> DeleteData(int Id)
        {
            cnx = new NpgsqlConnection(myDb1ConnectionString);

            sql = "DELETE FROM APIData Where _id_ = @_ID_";           
            string x = "";

            try
            {
                if(cnx.State == ConnectionState.Closed)
                {  
                    await cnx.OpenAsync();
                }
                
                com = new NpgsqlCommand(sql,cnx);
                com.Parameters.Add(new SqlParameter ("@_ID_", Id));
          
                dr = com.ExecuteReader();

                if(dr.Read() != true)
                {
                    x = "Delete was succeful realized!";
                }
                else
                {
                    x = "Delete has problems within the process";
                }

            }
             catch(SqlException ex)
            {
               if(ex != null)
               {
                   Console.WriteLine(ex);
                   x = ex.Message;
               }
            }
             finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    await cnx.CloseAsync();
                }
                cnx.Dispose();
            }
           
            return x;
        } 

         public async Task<string> UpdateData(string Name, string City, int Age, int Id)
        {
            cnx = new NpgsqlConnection(myDb1ConnectionString);

            
            sql = "UPDATE APIData SET ";
            var add = " Where _id_ = @_ID_ ";   

            string x = "";
 
            try
            {
                if(cnx.State == ConnectionState.Closed)
                {  
                    await cnx.OpenAsync();
                }
              
                if(Name != null)
                {
                    sql += " _name_ = @_NAME_ ";
                }
                if(City != null)
                {
                    if(Name != null)
                    {
                        sql += ",";
                    }
                    else
                    {

                    }

                    sql += " _city_ = @_CITY_ ";
                }
                if(Age != 0)
                {
                    if(Name != null || City != null)
                    {
                       sql += ",";
                    }
                    else
                    {

                    }

                    sql += " _age_ = @_AGE_ ";
                    
                }

                com = new NpgsqlCommand(sql + add, cnx); 

                com.Parameters.Add(new SqlParameter ("@_ID_", Id));
                if(Age != 0)
                {
                com.Parameters.Add(new SqlParameter ("@_AGE_", Age));
                }
                if(City != null)
                { 
                com.Parameters.Add(new SqlParameter ("@_CITY_", City));
                }
                if(Name != null)
                { 
                com.Parameters.Add(new SqlParameter ("@_NAME_", Name));
                }

                dr = com.ExecuteReader();

                if(dr.Read() != true)
                {
                    x = "Update was succeful realized!";
                }
                else
                {
                    x = "Update had problems within the process";
                }

            }
             catch(SqlException ex)
            {
               if(ex != null)
               {
                   Console.WriteLine(ex);
                   x = ex.Message;
               }
            }
             finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    await cnx.CloseAsync();
                }
                cnx.Dispose();
            }

            return x;
        }  
    }
}
