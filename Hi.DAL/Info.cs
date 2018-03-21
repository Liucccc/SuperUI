using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Dal
{
    public class Info
    {
        #region ==查询列表==
        /// <summary>
        /// 查询列表。返回DataSet：[Init].*
        /// </summary>
        /// <param name="Iid">编号查询。0为全部</param>
        public static DataSet Select_List(int Iid)
        {
            string sql = "select * from [zzlh2017_Info] a where Type<>0";
            if (Iid != 0)
                sql += " and Iid=" + Iid;
            sql += "order by Iid";
            DataSet ds = Dal.sqlH.getDs(sql);

            return ds;
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细。返回DataTable：[Info].*
        /// </summary>
        public static DataTable Select_Detail()
        {
            string sql = "select * from [zzlh2017_Info] a where Iid<100 order by Iid";
            DataTable dt = new DataTable();
            dt = sqlH.getDt(sql);

            return dt;
        }
        #endregion

        #region ==修改==
        public static void Update(int Iid, string Iinfo)
        {
            if (Iinfo == null)
                Iinfo = "";

            string sql = "update [zzlh2017_Info] set Iinfo='" + Iinfo.Replace("'", "''") + "' where Iid=" + Iid;
            Dal.sqlH.ExecuteNonQuery(sql);
        }
        #endregion
    }
}
