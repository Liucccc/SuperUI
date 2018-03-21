using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace sortTitle
{
    public class ArticleSort
    {
        public string st;
        public int Kind;
        public ArticleSort()
        {
            Kind = Common.Functions.ConvertInt16(HttpContext.Current.Request.QueryString["k"], 0);
            switch (Kind)
            {
                case 5:
                    st = "离婚知识";
                    break;
                case 6:
                    st = "继承知识";
                    break;
            }
        }
    }
}
