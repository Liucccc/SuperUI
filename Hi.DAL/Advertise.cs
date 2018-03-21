using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class Advertise
    {
        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回DataTable：[Advertise].*
        /// </summary>
        /// <param name="s_Aid">s_Aid</param>
        /// <param name="not_Aid">Aid不包含的</param>
        /// <returns></returns>
        public static DataTable Select_List(string s_Aid)
        {

            if (s_Aid == null)
                s_Aid = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Advertise_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.VarChar, 8000).Value = s_Aid;
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sc.Dispose();
            da.Dispose();
            cfg.closeDb();
            return dt;
        }
        #endregion

        #region ==查询详细==

        public static DataTable Select_Detail(int Aid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Advertise_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.VarChar, 8000).Value = Aid.ToString();
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sc.Dispose();
            da.Dispose();
            cfg.closeDb();
            return dt;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(int Aid, string Url, string Pic1)
        {
            if (Url == null)
                Url = "";
            if (Pic1 == null)
                Pic1 = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_Advertise_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.SmallInt).Value = Aid;
            sc.Parameters.Add("@Url", SqlDbType.VarChar, 100).Value = Url;
            sc.Parameters.Add("@Pic1", SqlDbType.VarChar, 100).Value = Pic1;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion
    }
}
