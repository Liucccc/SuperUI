using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hi.Web.Controllers
{
    public class ArticleController : Controller
    {

        public sortTitle.Article st = new sortTitle.Article();
        public int cnum = 6;
        public int i;
        public string page_str;
        public List<Model.Article> l_article;
        public Model.Pages p;
        public long Rc = 0;
        public int Pages = 0;
        public string searchText = "";
        public int searching = 0;
        public string d1 = "";
        public string d2 = "";
        public int Ostatus;
        public int s_recommend;
        public int s_hot;
        public long Kind = 0;
        public string Asid = "";

        public Model.ArticleSort ms;
        public List<Model.ArticleSort> l_Article_Sort;
        Model.Article m;
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult List(int k)
        {
            return View();
        }

        public ActionResult GetJQGridJson(int rows, int page, string sidx, string sord, string queryJson = "")
        {
            Model.Article query = new Model.Article();
            if (!string.IsNullOrEmpty(queryJson))
                query = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Article>(queryJson);
            #region ==获得记录==
            l_article = new List<Model.Article>();
            p = new Model.Pages();
            p.Page = page;
            p.Ps = rows;
            m = new Model.Article();
            m.s_Alive = query.s_Alive;
            m.s_Hot = s_hot;
            m.s_Recommend = s_recommend;
            m.s_d1 = d1;
            m.s_d2 = d2;
            m.s_Keywords = query.s_Keywords;
            m.s_Kind = Kind.ToString();
            m.s_Asid = Asid;
            m.s_Order = sidx + " " + sord;
            m.s_Main_parameter = "Aid,Atitle,Url,Source,Author,Alive,Kind,Layer,Ainfo,Atime,Pic1,Pic2,Summary,Hot,Recommend,Asid,ieTitle,seoKeywords,seoDescription";
            m.s_Extended_parameter = "Stitle";
            l_article = Bll.Article.Select_List(m, ref p, ref Rc);
            #endregion

            return Json(new { page = p.Page.ToString(), total = p.Pc, records = Rc.ToString(), rows = l_article });
        }
    }

    /// <summary>
    /// 分页参数
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 查询条件Json
        /// </summary>
        public string conditionJson { get; set; }
    }

    //JQGrid数据
    public class JQGridData
    {
        /// <summary>
        /// 页码
        /// </summary>
        public string page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public string records { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RowsItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Userdata userdata { get; set; }
    }

    public class RowsItem
    {
        /// <summary>
        /// 主键值
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public List<string> cell { get; set; }
    }

    public class Userdata
    {
        /// <summary>
        /// 
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int tax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }
}