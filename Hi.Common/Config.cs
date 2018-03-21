using System;
using System.Data;
//using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;

namespace Common
{
	public class Config
	{
        public SqlConnection Conn;      //数据库连接

        //public OleDbConnection Conn=new OleDbConnection();

		#region 连接数据库	connDb()
		public void connDb()
        {
            if (Conn == null)
                Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ConnectionString);
            if (Conn.State.ToString() == "Closed")
                Conn.Open();
		}
		#endregion

		#region 关闭数据库	closeDb()
        public void closeDb()
		{
            if (Conn != null)
            {
                if (Conn.State.ToString() == "Open")
                {
                    Conn.Close();
                    Conn.Dispose();
                    Conn = null;
                }
            }
		}
		#endregion
	}
}
