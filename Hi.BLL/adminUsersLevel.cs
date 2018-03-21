using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Bll
{
    /// <summary>
    /// 管理员级别表
    /// 2016-09-22
    /// </summary>
    public class adminUsersLevel
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = { "c,Aulid" };

        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回记录总条数</param>
        public static List<Model.adminUsersLevel> Select_List(Model.adminUsersLevel m, ref Model.Pages p, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.adminUsersLevel.Select_List(ref p, ref Rc, m.s_Main_parameter, m.s_Extended_parameter, m.s_Keywords, m.s_Aulid, m.s_Alive, m.s_Order);
            DataRow dr;
            List<Model.adminUsersLevel> l = new List<Model.adminUsersLevel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.adminUsersLevel();

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Aulid"))
                    m.Aulid = Common.Functions.ConvertInt16(dr["Aulid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Ltitle"))
                    m.Ltitle = dr["Ltitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Area"))
                    m.Area = dr["Area"].ToString();

                #endregion

                #region ==扩展参数==
                if (Common.Functions.checkHave(return_value[1], "c"))
                    m.c = Common.Functions.ConvertInt64(dr["c"], 0);
                #endregion

                l.Add(m);
            }
            dt.Dispose();

            return l;
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细。
        /// </summary>
        /// <param name="m">d系列 + m.s_*_parameter</param>
        public static Model.adminUsersLevel Select_Detail(Model.adminUsersLevel m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.adminUsersLevel.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter,m.d_Aulid, m.d_Aulid_not, m.d_Alive);
            m = new Model.adminUsersLevel();
            try
            {
                DataRow dr = dt.Rows[0];
                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Aulid"))
                    m.Aulid = Common.Functions.ConvertInt16(dr["Aulid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Ltitle"))
                    m.Ltitle = dr["Ltitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Area"))
                    m.Area = dr["Area"].ToString();

                #endregion

                #region ==扩展参数==
                if (Common.Functions.checkHave(return_value[1], "c"))
                    m.c = Common.Functions.ConvertInt64(dr["c"], 0);
                #endregion
            }
            catch
            {
                m.Aulid = 0;
                m.Area = "";
                m.Alive = true;
                m.Ltitle = "";
                m.c = 0;
            }
            dt.Dispose();

            return m;
        }
        #endregion

        #region ==添加==
        /// <summary>
        /// 添加。返回新纪录主码值
        /// </summary>
        /// <param name="m">添加项</param>
        public static int Add(Model.adminUsersLevel m)
        {
            int Aulid = Dal.adminUsersLevel.Add(m.Aulid, m.Ltitle, m.Area);

            return Aulid;
        }
        #endregion

        #region ==修改基本资料==
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="m">d_Aulid+修改项</param>
        public static void Modify(Model.adminUsersLevel m)
        {
            Dal.adminUsersLevel.Modify(m.d_Aulid,m.Aulid, m.Ltitle, m.Area);
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Aulid">要删除的编号，多个编号用，分隔</param>
        public static void Del(string Aulid)
        {
            Common.Config cfg = new Common.Config();
            sortTitle.adminUsersLevel st = new sortTitle.adminUsersLevel();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_adminUsersLevel]", "Aulid", Aulid, 0, "", "Ltitle", st.st, cfg.Conn, true);
            Common.Functions.recordDel("[zzlh2017_adminUsers]", "Aulid", Aulid, 0, "", "Auser", st.st, cfg.Conn, true);
            cfg.closeDb();
        }
        #endregion

        #region ==修改屏蔽状态==
        /// <summary>
        /// 修改屏蔽状态
        /// </summary>
        /// <param name="m">d_Aulid</param>
        public static void Alive(Model.adminUsersLevel m)
        {
            Dal.adminUsersLevel.Alive(m.d_Aulid);
        }
        #endregion

        #region ==获得新编号==
        /// <summary>
        /// 获得新编号
        /// </summary>
        public static int newPk()
        {
            string sql = "select isnull(max(Aulid)+1,1) as m from [zzlh2017_adminUsersLevel]";
            DataTable dt = Dal.sqlH.getDt(sql);
            int i = Common.Functions.ConvertInt16(dt.Rows[0][0], 1);
            dt.Dispose();

            return i;
        }
        #endregion
    }
}
