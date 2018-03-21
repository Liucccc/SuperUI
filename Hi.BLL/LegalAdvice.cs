using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class LegalAdvice
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = { };

        #region ==查询列表（带分页）。返回List==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回表记录总数量</param>
        public static List<Model.LegalAdvice> Select_List(Model.LegalAdvice m, ref Model.Pages p, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.LegalAdvice.Select_List(ref p, m.s_Main_parameter, m.s_Extended_parameter, m.s_Keywords, m.s_Lid, m.s_Order, m.s_Ostatus, m.s_d1, m.s_d2, ref Rc);
            DataRow dr;
            List<Model.LegalAdvice> l = new List<Model.LegalAdvice>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.LegalAdvice();
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Lid"))
                    m.Lid = Common.Functions.ConvertInt64(dr["Lid"]);
                if (Common.Functions.checkHave(return_value[0], "Ltitle"))
                    m.Ltitle = Common.Functions.ConvertString(dr["Ltitle"]);
                if (Common.Functions.checkHave(return_value[0], "Linfo"))
                    m.Linfo = dr["Linfo"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Lname"))
                    m.Lname = Common.Functions.ConvertString(dr["Lname"]);
                if (Common.Functions.checkHave(return_value[0], "Tel"))
                    m.Tel = Common.Functions.ConvertString(dr["Tel"]);
                if (Common.Functions.checkHave(return_value[0], "QQ"))
                    m.QQ = dr["QQ"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Email"))
                    m.Email = dr["Email"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Ldate"))
                    m.Ldate = Common.Functions.ConvertDateTime(dr["Ldate"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Ostatus"))
                    m.Ostatus = Common.Functions.ConvertInt16(dr["Ostatus"], 0);
                if (Common.Functions.checkHave(return_value[0], "Reply"))
                    m.Reply = dr["Reply"].ToString();
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
        /// <param name="m">d_系列</param>
        public static Model.LegalAdvice Select_Detail(Model.LegalAdvice m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.LegalAdvice.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Lid);
            m = new Model.LegalAdvice();
            try
            {
                DataRow dr = dt.Rows[0];
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Lid"))
                    m.Lid = Common.Functions.ConvertInt64(dr["Lid"]);
                if (Common.Functions.checkHave(return_value[0], "Ltitle"))
                    m.Ltitle = Common.Functions.ConvertString(dr["Ltitle"]);
                if (Common.Functions.checkHave(return_value[0], "Linfo"))
                    m.Linfo = dr["Linfo"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Lname"))
                    m.Lname = Common.Functions.ConvertString(dr["Lname"]);
                if (Common.Functions.checkHave(return_value[0], "Tel"))
                    m.Tel = Common.Functions.ConvertString(dr["Tel"]);
                if (Common.Functions.checkHave(return_value[0], "QQ"))
                    m.QQ = dr["QQ"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Email"))
                    m.Email = dr["Email"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Ldate"))
                    m.Ldate = Common.Functions.ConvertDateTime(dr["Ldate"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Ostatus"))
                    m.Ostatus = Common.Functions.ConvertInt16(dr["Ostatus"], 0);
                if (Common.Functions.checkHave(return_value[0], "Reply"))
                    m.Reply = dr["Reply"].ToString();
                #endregion

            }
            catch
            {
                m.Lid = 0;
                m.Ltitle = "";
                m.Linfo = "";
                m.Lname = "";
                m.Tel = "";
                m.QQ = "";
                m.Email = "";
                m.Ldate = Convert.ToDateTime(Common.Para.dt_def);
                m.Ostatus = 0;
                m.Reply = "";
            }
            dt.Dispose();
            return m;
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Lid">要删除的编号，多个编号用逗号分隔</param>
        public static void Del(string Lid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_LegalAdvice]", "Lid", Lid, 0, "", "Ltitle", "", cfg.Conn, true);
            cfg.closeDb();
        }
        #endregion

        #region ==添加==
        /// <summary>
        /// 添加。返回新纪录主码值
        /// </summary>
        /// <param name="m">添加项</param>
        public static long Add(Model.LegalAdvice m)
        {
            long Lid = Dal.LegalAdvice.Add(m.Ltitle, m.Linfo, m.Lname, m.Tel, m.QQ, m.Email);
            return Lid;
        }
        #endregion

        #region ==修改基本资料==
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="m">d_Lid+修改项</param>
        public static void Modify(Model.LegalAdvice m)
        {
            Dal.LegalAdvice.Modify(m.d_Lid, m.Reply);
        }
        #endregion
    }
}
