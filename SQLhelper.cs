using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Webservice
{
    public static class SQLhelper
    {
        private static readonly string connstr = ConfigurationManager.ConnectionStrings["mssql"].ConnectionString;
       //SQL insert update delete 
        public static int ExecuteNonQurey(string sql,params SqlParameter[]pms)
        {
           
            using (SqlConnection conn=new SqlConnection(connstr)) 
            {
                using(SqlCommand cmd=new SqlCommand(sql, conn)) {
                    if (pms!=null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    conn.Open();
                    int row= cmd.ExecuteNonQuery();
                    return row;
                }
            }
           
        }
        //SQL insert update delete 
        //**end
        public static DataSet QueryDataSet(string sql,params SqlParameter[] pms)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                {
                    if (pms != null)
                    { adapter.SelectCommand.Parameters.AddRange(pms); }
                    adapter.Fill(ds);
                    return ds;
                       }
                  

            }
        }//返回dataset
        public static DataTable pageSQL(string sql,params SqlParameter[] pms)
        {
            using(SqlDataAdapter adapter=new SqlDataAdapter(sql, connstr))
            {
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddRange(pms);
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}