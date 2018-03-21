using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Dal
{
    public class Init
    {
        #region ==查询列表==
        /// <summary>
        /// 查询列表。返回DataSet：[Init].*
        /// </summary>
        /// <param name="not_Iid">不包括的记录编号。</param>
        public static DataSet Select_List(string not_Iid)
        {
            if (not_Iid == null)
                not_Iid = "0";
            if (not_Iid == "")
                not_Iid = "0";

            string sql = "select * from [zzlh2017_Init] a where Type<>0 and Iid not in(" + not_Iid + ") order by Iid";
            DataSet ds = Dal.sqlH.getDs(sql);

            return ds;
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细。返回DataTable：[Init].*
        /// </summary>
        public static DataTable Select_Detail()
        {
            string sql = "select * from [zzlh2017_Init] a where Iid<100 order by Iid";
            DataTable dt = Dal.sqlH.getDt(sql);

            return dt;
        }
        #endregion

        #region ==修改==
        public static void Update(int Iid, string Iinfo)
        {
            if (Iinfo == null)
                Iinfo = "";

            string sql = "update [zzlh2017_Init] set Iinfo='" + Iinfo.Replace("'", "''") + "' where Iid=" + Iid;
            Dal.sqlH.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
