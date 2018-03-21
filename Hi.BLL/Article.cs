using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dal;

namespace Bll
{
    public class Article
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = { "Stitle,Asid" };

        #region ==查询列表（带分页）。返回List==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回表记录总数量</param>
        public static List<Model.Article> Select_List(Model.Article m, ref Model.Pages p, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.Article.Select_List(ref p, m.s_Main_parameter, m.s_Extended_parameter, m.s_Keywords, m.s_Kind, m.s_Alive, m.s_d1, m.s_d2, m.s_Aid, m.s_Asid, m.s_Order, m.s_Hot, m.s_Recommend, ref Rc);
            DataRow dr;
            List<Model.Article> l = new List<Model.Article>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.Article();
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Aid"))
                    m.Aid = Common.Functions.ConvertInt64(dr["Aid"]);
                if (Common.Functions.checkHave(return_value[0], "Atitle"))
                    m.Atitle = Common.Functions.ConvertString(dr["Atitle"]);
                if (Common.Functions.checkHave(return_value[0], "Url"))
                    m.Url = dr["Url"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Source"))
                    m.Source = Common.Functions.ConvertString(dr["Source"]);
                if (Common.Functions.checkHave(return_value[0], "Author"))
                    m.Author = Common.Functions.ConvertString(dr["Author"]);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Hot"))
                    m.Hot = Convert.ToBoolean(dr["Hot"]);
                if (Common.Functions.checkHave(return_value[0], "Recommend"))
                    m.Recommend = Convert.ToBoolean(dr["Recommend"]);
                if (Common.Functions.checkHave(return_value[0], "Kind"))
                    m.Kind = Common.Functions.ConvertInt16(dr["Kind"], 0);
                if (Common.Functions.checkHave(return_value[0], "Layer"))
                    m.Layer = Common.Functions.ConvertInt64(dr["Layer"], 0);
                if (Common.Functions.checkHave(return_value[0], "Ainfo"))
                    m.Ainfo = dr["Ainfo"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Atime"))
                    m.Atime = Common.Functions.ConvertDateTime(dr["Atime"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Pic1"))
                    m.Pic1 = dr["Pic1"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Pic2"))
                    m.Pic2 = dr["Pic2"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Summary"))
                    m.Summary = dr["Summary"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Asid"))
                    m.Asid = Common.Functions.ConvertInt64(dr["Asid"]);
                if (Common.Functions.checkHave(return_value[0], "ieTitle"))
                    m.ieTitle = dr["ieTitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "seoKeywords"))
                    m.seoKeywords = dr["seoKeywords"].ToString();
                if (Common.Functions.checkHave(return_value[0], "seoDescription"))
                    m.seoDescription = dr["seoDescription"].ToString();
                #endregion

                #region 扩展参数
                if (Common.Functions.checkHave(return_value[1], "Stitle"))
                    m.Stitle = dr["Stitle"].ToString();
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
        public static Model.Article Select_Detail(Model.Article m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.Article.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Aid, m.d_Alive);
            m = new Model.Article();
            try
            {
                DataRow dr = dt.Rows[0];
                #region 主参数
                if (Common.Functions.checkHave(return_value[0], "Aid"))
                    m.Aid = Common.Functions.ConvertInt64(dr["Aid"]);
                if (Common.Functions.checkHave(return_value[0], "Atitle"))
                    m.Atitle = Common.Functions.ConvertString(dr["Atitle"]);
                if (Common.Functions.checkHave(return_value[0], "Url"))
                    m.Url = dr["Url"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Source"))
                    m.Source = Common.Functions.ConvertString(dr["Source"]);
                if (Common.Functions.checkHave(return_value[0], "Author"))
                    m.Author = Common.Functions.ConvertString(dr["Author"]);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Hot"))
                    m.Hot = Convert.ToBoolean(dr["Hot"]);
                if (Common.Functions.checkHave(return_value[0], "Recommend"))
                    m.Recommend = Convert.ToBoolean(dr["Recommend"]);
                if (Common.Functions.checkHave(return_value[0], "Kind"))
                    m.Kind = Common.Functions.ConvertInt16(dr["Kind"], 0);
                if (Common.Functions.checkHave(return_value[0], "Layer"))
                    m.Layer = Common.Functions.ConvertInt64(dr["Layer"], 0);
                if (Common.Functions.checkHave(return_value[0], "Ainfo"))
                    m.Ainfo = dr["Ainfo"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Atime"))
                    m.Atime = Common.Functions.ConvertDateTime(dr["Atime"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Pic1"))
                    m.Pic1 = dr["Pic1"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Pic2"))
                    m.Pic2 = dr["Pic2"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Summary"))
                    m.Summary = dr["Summary"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Asid"))
                    m.Asid = Common.Functions.ConvertInt64(dr["Asid"]);
                if (Common.Functions.checkHave(return_value[0], "ieTitle"))
                    m.ieTitle = dr["ieTitle"].ToString();
                if (Common.Functions.checkHave(return_value[0], "seoKeywords"))
                    m.seoKeywords = dr["seoKeywords"].ToString();
                if (Common.Functions.checkHave(return_value[0], "seoDescription"))
                    m.seoDescription = dr["seoDescription"].ToString();
                #endregion

                #region 扩展参数
                if (Common.Functions.checkHave(return_value[1], "Stitle"))
                    m.Stitle = dr["Stitle"].ToString();
                #endregion

            }
            catch
            {
                m.Aid = 0;
                m.Atitle = "";
                m.Url = "";
                m.Source = "";
                m.Author = "";
                m.Alive = false;
                m.Hot = false;
                m.Recommend = false;
                m.Alive = true;
                m.Kind = 0;
                m.Layer = 0;
                m.Ainfo = "";
                m.Atime = Convert.ToDateTime(Common.Para.dt_def);
                m.Pic1 = "";
                m.Pic2 = "";
                m.Summary = "";
                m.Asid = 0;
                m.ieTitle = "";
                m.seoKeywords = "";
                m.seoDescription = "";

            }
            dt.Dispose();
            return m;
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Aid">要删除的编号，多个编号用逗号分隔</param>
        public static void Del(string Aid)
        {
            Common.Config cfg = new Common.Config();
            sortTitle.Article st = new sortTitle.Article();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_Article]", "Aid", Aid, 2, "Ainfo", "Atitle", st.st, cfg.Conn, true);
            cfg.closeDb();
        }
        #endregion

        #region ==添加==
        /// <summary>
        /// 添加。返回新纪录主码值
        /// </summary>
        /// <param name="m">添加项</param>
        public static long Add(Model.Article m)
        {
            long Aid = Dal.Article.Add(m.Atitle, m.Url, m.Source, m.Author, m.Kind, m.Ainfo, m.Atime.ToString("yyyy-MM-dd HH:mm:ss"), m.Pic1, m.Pic2, m.Summary, m.Asid, m.ieTitle, m.seoKeywords, m.seoDescription);
            return Aid;
        }
        #endregion

        #region ==修改基本资料==
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="m">d_Aid+修改项</param>
        public static void Modify(Model.Article m)
        {
            Dal.Article.Modify(m.d_Aid, m.Atitle, m.Url, m.Source, m.Author, m.Ainfo, m.Atime.ToString("yyyy-MM-dd HH:mm:ss"), m.Pic1, m.Pic2, m.Summary, m.Asid, m.ieTitle, m.seoKeywords, m.seoDescription);
        }
        #endregion

        #region ==修改序号==
        /// <summary>
        /// 修改序号
        /// </summary>
        /// <param name="m">d_Aid+Layer</param> 
        public static void Layer(Model.Article m)
        {
            Dal.Article.Layer(m.d_Aid, m.Layer);
        }
        #endregion

        #region ==修改屏蔽状态==
        /// <summary>
        /// 修改屏蔽状态
        /// </summary>
        /// <param name="m">d_Aid</param>
        public static void Alive(Model.Article m)
        {
            Dal.Article.Alive(m.d_Aid);
        }
        #endregion

        #region ==修改热点文章状态==
        /// <summary>
        /// 修改热点文章状态
        /// </summary>
        /// <param name="m">d_Aid</param>
        public static void Hot(Model.Article m)
        {
            Dal.Article.Hot(m.d_Aid);
        }
        #endregion

        #region ==修改推荐文章状态==
        /// <summary>
        /// 修改推荐文章状态
        /// </summary>
        /// <param name="m">d_Aid</param>
        public static void Recommend(Model.Article m)
        {
            Dal.Article.Recommend(m.d_Aid);
        }
        #endregion

        #region ==返回Article数量==
        /// <summary>
        /// 返回Article数量
        /// kind 栏目
        /// Alive 是否屏蔽
        public static long GetCount(Model.Article m)
        {
            return Dal.Article.GetCount(m.s_Alive, m.s_Kind);
        }
        #endregion
    }
}
