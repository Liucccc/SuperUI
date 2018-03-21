using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL
{
    public class LawyerFirst
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
        public static List<Model.LawyerFirst> Select_List(Model.LawyerFirst m, ref Model.Pages p, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.LawyerFirst.Select_List(ref p, m.s_Main_parameter, m.s_Extended_parameter, m.s_Keywords, m.s_Lid, m.s_Order, ref Rc);
            DataRow dr;
            List<Model.LawyerFirst> l = new List<Model.LawyerFirst>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.LawyerFirst();
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Lid"))
                    m.Lid = Common.Functions.ConvertInt64(dr["Lid"]);
                if (Common.Functions.checkHave(return_value[0], "Pic1"))
                    m.Pic1 = Common.Functions.ConvertString(dr["Pic1"]);
                if (Common.Functions.checkHave(return_value[0], "Lname"))
                    m.Lname = dr["Lname"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Summary"))
                    m.Summary = Common.Functions.ConvertString(dr["Summary"]);
                if (Common.Functions.checkHave(return_value[0], "Linfo"))
                    m.Linfo = Common.Functions.ConvertString(dr["Linfo"]);
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
        public static Model.LawyerFirst Select_Detail(Model.LawyerFirst m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.LawyerFirst.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Lid);
            m = new Model.LawyerFirst();
            try
            {
                DataRow dr = dt.Rows[0];
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Lid"))
                    m.Lid = Common.Functions.ConvertInt64(dr["Lid"]);
                if (Common.Functions.checkHave(return_value[0], "Pic1"))
                    m.Pic1 = Common.Functions.ConvertString(dr["Pic1"]);
                if (Common.Functions.checkHave(return_value[0], "Lname"))
                    m.Lname = dr["Lname"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Summary"))
                    m.Summary = Common.Functions.ConvertString(dr["Summary"]);
                if (Common.Functions.checkHave(return_value[0], "Linfo"))
                    m.Linfo = Common.Functions.ConvertString(dr["Linfo"]);
                #endregion

            }
            catch
            {
                m.Lid = 0;
                m.Pic1 = "";
                m.Lname = "";
                m.Summary = "";
                m.Linfo = "";
            }
            dt.Dispose();
            return m;
        }
        #endregion

        #region ==修改基本资料==
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="m">d_Lid+修改项</param>
        public static void Modify(Model.LawyerFirst m)
        {
            Dal.LawyerFirst.Modify(m.d_Lid, m.Pic1, m.Lname, m.Summary, m.Linfo);
        }
        #endregion
    }
}
