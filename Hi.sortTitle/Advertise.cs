using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace sortTitle
{
    public class Advertise
    {
        public string st;
        public int Kind;
        public Advertise()
        {
            Kind = Common.Functions.ConvertInt16(HttpContext.Current.Request.QueryString["k"], 0);
            switch (Kind)
            {
                case 1: st = "顶部Banner"; break;
                case 2: st = "首页中部Banner"; break;
                case 3: st = "首页婚姻法规Logo"; break;
                case 4: st = "首页起诉离婚Logo"; break;
                case 5: st = "首页子女抚养Logo"; break;
                case 6: st = "首页财产分割Logo"; break;
                case 7: st = "首页离婚案例Logo"; break;
                case 8: st = "首页涉外婚姻Logo"; break;
                case 9: st = "首页继承法规Logo"; break;
                case 10: st = "首页继承案例Logo"; break;
            }
        }
    }
}
