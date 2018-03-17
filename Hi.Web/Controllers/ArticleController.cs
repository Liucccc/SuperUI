using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hi.Web.Controllers
{
    public class ArticleController : Controller
    {
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
    }
}