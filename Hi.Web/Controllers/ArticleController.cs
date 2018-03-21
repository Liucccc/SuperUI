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

        public ActionResult List()
        {
            return View();
        }

        public ActionResult GetJQGridJson(int Ostatus, int s_hot, int s_recommend, string d1, string d2, string searchText, long Kind, string Asid)
        {
            #region ==获得记录==
            l_article = new List<Model.Article>();
            p = new Model.Pages();
            p.Page = Pages;
            p.Ps = Common.Functions.ConvertInt16(Bll.Init.getInfo(2), 18);
            m = new Model.Article();
            m.s_Alive = Ostatus;
            m.s_Hot = s_hot;
            m.s_Recommend = s_recommend;
            m.s_d1 = d1;
            m.s_d2 = d2;
            m.s_Keywords = searchText;
            m.s_Kind = Kind.ToString();
            m.s_Asid = Asid;
            m.s_Order = "Alive desc,Kind,Asid,Layer desc,Aid";
            m.s_Main_parameter = "Aid,Atitle,Url,Source,Author,Alive,Kind,Layer,Ainfo,Atime,Pic1,Pic2,Summary,Hot,Recommend,Asid,ieTitle,seoKeywords,seoDescription";
            m.s_Extended_parameter = "Stitle";
            l_article = Bll.Article.Select_List(m, ref p, ref Rc);
            #endregion
            return Json(l_article);
        }
    }
}