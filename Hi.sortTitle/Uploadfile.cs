using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace sortTitle
{

    public class Uploadfile
    {
        public string st;
        public int Kind;
        public Uploadfile()
        {
            Kind = Common.Functions.ConvertInt16(HttpContext.Current.Request.QueryString["k"], 0);
            switch (Kind)
            {
                case 0: st = "可视化上传文件"; break;
            }
        }
    }
}
