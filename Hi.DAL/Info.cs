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
        #region ==��ѯ�б�==
        /// <summary>
        /// ��ѯ�б�����DataSet��[Init].*
        /// </summary>
        /// <param name="Iid">��Ų�ѯ��0Ϊȫ��</param>
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

        #region ==��ѯ��ϸ==
        /// <summary>
        /// ��ѯ��ϸ������DataTable��[Info].*
        /// </summary>
        public static DataTable Select_Detail()
        {
            string sql = "select * from [zzlh2017_Info] a where Iid<100 order by Iid";
            DataTable dt = new DataTable();
            dt = sqlH.getDt(sql);

            return dt;
        }
        #endregion

        #region ==�޸�==
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
