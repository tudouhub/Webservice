using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace Webservice
{
    /// <summary>
    /// DormWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DormWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int Setemp( int empno, string name, string sex, int age, string dep, string room)
        {

            string sql = "update employee set empno=@empno,name=@name,sex=@sex,age=@age,dep=@dep,room=@room where ID=@ID ";


            SqlParameter[] pms = new SqlParameter[]
                {
           
            new SqlParameter("@empno",System.Data.SqlDbType.Int){Value=empno},
            new SqlParameter("@name",System.Data.SqlDbType.VarChar){Value=name},
            new SqlParameter("@sex",System.Data.SqlDbType.VarChar){Value=sex},
            new SqlParameter("@age",System.Data.SqlDbType.Int){Value=age},
            new SqlParameter("@dep",System.Data.SqlDbType.VarChar){Value=dep},
            new SqlParameter("room",System.Data.SqlDbType.VarChar){Value=room},
                };
            int row = SQLhelper.ExecuteNonQurey(sql, pms);
            return row;

        }
        //更新SQL
        //####End
        [WebMethod]
        public int AddEmp(int empno, string name, string sex, int age, string dep, string room)
        {

            string sql = "insert employee values(@empno,@name,@sex,@age,@dep,@room)";
            SqlParameter[] pms = new SqlParameter[]
                                                                           {
            new SqlParameter("@empno",System.Data.SqlDbType.Int){Value=empno },
            new SqlParameter("@name",System.Data.SqlDbType.VarChar){Value=name},
            new SqlParameter("@sex",System.Data.SqlDbType.VarChar){Value=sex},
            new SqlParameter("@age",System.Data.SqlDbType.Int){Value=age},
            new SqlParameter("@dep",System.Data.SqlDbType.VarChar){Value=dep},
            new SqlParameter("room",System.Data.SqlDbType.VarChar){Value=room},
                                                                              };
            return SQLhelper.ExecuteNonQurey(sql, pms);
        }
        //添加SQL语句
        //###end

        //查询sql返回datatable dt
        [WebMethod]
        public DataTable QureyDataTable(int empno)
        {
            string sql = "select * from employee where empno=@empno";
            SqlParameter pms = new SqlParameter("@empno", System.Data.SqlDbType.Int) { Value = empno };
            DataTable dt = SQLhelper.QueryDataSet(sql, pms).Tables[0];
            return dt;
        }//end 查询sql返回datatable dt
        //SQL分页
        [WebMethod]
        public DataTable PageSQL(int pageIndex,int pageNumber)
        {
            string sql = "selec * from (select * from (select row_number()over(oder by ID)as RowID,* from employee )as a where a.RowID between (@pageIndex-1)*@pageNumber+1 and @pageIndex*@pageNumber ";
            SqlParameter[] pms = new SqlParameter[]
            {
               new SqlParameter("@pageIndex",System.Data.SqlDbType.Int){Value=pageIndex },
               new SqlParameter("@pageNumber",System.Data.SqlDbType.Int){Value=pageNumber},
           };
            DataTable dt = SQLhelper.pageSQL(sql, pms);
            return dt;
        }
    }
}
