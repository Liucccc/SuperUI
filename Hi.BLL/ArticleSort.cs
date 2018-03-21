using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Bll
{
    public class ArticleSort
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = {  };

        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回记录总条数</param>
        public static List<Model.ArticleSort> Select_List(ref Model.Pages p, Model.ArticleSort m, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.ArticleSort.Select_List(ref p, ref Rc, m.s_Main_parameter, m.s_Extended_parameter, m.s_Keywords, m.s_Asid, m.s_Kind, m.s_Alive, m.s_Order);
            DataRow dr;
            List<Model.ArticleSort> l = new List<Model.ArticleSort>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.ArticleSort();

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Asid"))
                    m.Asid = Common.Functions.ConvertInt64(dr["Asid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Stitle"))
                    m.Stitle = dr["Stitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Layer"))
                    m.Layer = Common.Functions.ConvertInt64(dr["Layer"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Kind"))
                    m.Kind = Common.Functions.ConvertInt32(dr["Kind"], 0);
                #endregion
                l.Add(m);
            }

            dt.Dispose();
            return l;
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细
        /// </summary>
        /// <param name="m">d系列 + m.s_*_parameter</param>
        public static Model.ArticleSort Select_Detail(Model.ArticleSort m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.ArticleSort.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Asid, m.d_Alive);
            m = new Model.ArticleSort();
            try
            {
                DataRow dr = dt.Rows[0];

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Asid"))
                    m.Asid = Common.Functions.ConvertInt64(dr["Asid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Stitle"))
                    m.Stitle = dr["Stitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Layer"))
                    m.Layer = Common.Functions.ConvertInt64(dr["Layer"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Kind"))
                    m.Kind = Common.Functions.ConvertInt32(dr["Kind"], 0);
                #endregion

            }
            catch
            {
                m.Asid = 0;
                m.Stitle = "";
                m.Kind = 0;
                m.Layer = 0;
                m.Alive = true;
            }
            dt.Dispose();
            return m;
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Asid">要删除的编号，多个编号用，分隔</param>
        public static void Del(string Asid)
        {
            Common.Config cfg = new Common.Config();
            sortTitle.ArticleSort st = new sortTitle.ArticleSort();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_ArticleSort]", "Asid", Asid, 0, "", "Stitle", st.st, cfg.Conn, true);
            cfg.closeDb();
        }
        #endregion

        #region ==添加==
        /// <summary>
        /// 添加。返回新纪录主码值
        /// </summary>
        /// <param name="m">添加项</param>
        public static long Add(Model.ArticleSort m)
        {
            long Asid = Dal.ArticleSort.Add(m.Stitle, m.Kind);

            return Asid;
        }
        #endregion

        #region ==修改基本信息==
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="m">d_Asid+修改项</param>
        public static void Modify(Model.ArticleSort m)
        {
            Dal.ArticleSort.Modify(m.d_Asid, m.Stitle);
        }
        #endregion

        #region ==修改屏蔽状态==
        /// <summary>
        ///  修改屏蔽状态
        /// </summary>
        /// <param name="m">d_Asid</param>
        public static void Alive(Model.ArticleSort m)
        {
            Dal.ArticleSort.Alive(m.d_Asid);
        }
        #endregion

        #region ==修改序号==
        /// <summary>
        /// 修改序号
        /// </summary>
        /// <param name="m">d_Asid+Layer</param> 
        public static void Layer(Model.ArticleSort m)
        {
            Dal.ArticleSort.Layer(m.d_Asid, m.Layer);
        }
        #endregion
    }
}