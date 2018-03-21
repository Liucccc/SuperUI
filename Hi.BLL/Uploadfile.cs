using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Bll
{
    public class Uploadfile
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = new string[0];

        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回记录总条数</param>
        public static List<Model.Uploadfile> Select_List(Model.Uploadfile m, ref Model.Pages p, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.Uploadfile.Select_List(ref p, ref Rc, m.s_Main_parameter, m.s_Extended_parameter, m.s_Uid, m.s_Uid_not, m.s_Order);
            DataRow dr;
            List<Model.Uploadfile> l = new List<Model.Uploadfile>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.Uploadfile();
                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Uid"))
                    m.Uid = Common.Functions.ConvertInt16(dr["Uid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Url"))
                    m.Url = dr["Url"].ToString();
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
        public static Model.Uploadfile Select_Detail(Model.Uploadfile m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.Uploadfile.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Uid, m.s_Uid_not);
            m = new Model.Uploadfile();
            try
            {
                DataRow dr = dt.Rows[0];

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Uid"))
                    m.Uid = Common.Functions.ConvertInt16(dr["Uid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Url"))
                    m.Url = dr["Url"].ToString();
                #endregion
            }
            catch
            {
                m.Uid = 0;
                m.Url = "";
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
        public static int Add(Model.Uploadfile m)
        {
            int Aulid = Common.Functions.ConvertInt32(Dal.Uploadfile.Add(m.Url),0);

            return Aulid;
        }
        #endregion

        #region ==修改基本资料==
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="m">d_Uid+修改项</param>
        public static void Modify(Model.Uploadfile m)
        {
            Dal.Uploadfile.Modify(m.d_Uid, m.Url);
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Aulid">要删除的编号，多个编号用，分隔</param>
        public static void Del(string Uid)
        {
            Common.Config cfg = new Common.Config();
            sortTitle.Uploadfile st = new sortTitle.Uploadfile();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_Uploadfile]", "Uid", Uid, 0, "", "Url", st.st, cfg.Conn, true);

            cfg.closeDb();
        }
        #endregion

    }
}
