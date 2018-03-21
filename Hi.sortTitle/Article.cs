using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace sortTitle
{
    public class Article
    {
        public string st;
        public int Kind;
        public Article()
        {
            Kind = Common.Functions.ConvertInt16(HttpContext.Current.Request.QueryString["k"], 0);
            switch (Kind)
            {
                case 1:
                    st = "业务范围";
                    break;
                case 2:
                    st = "最新资讯";
                    break;
                case 3:
                    st = "婚姻法规";
                    break;
                case 4:
                    st = "典型案例";
                    break;
                case 5:
                    st = "离婚知识";
                    break;
                case 6:
                    st = "继承知识";
                    break;
                case 7:
                    st = "友情链接";
                    break;
                case 8:
                    st = "律师人生";
                    break;
                case 51:
                    st = "首页热点文章轮播图";
                    break;
            }
        }
    }
}
