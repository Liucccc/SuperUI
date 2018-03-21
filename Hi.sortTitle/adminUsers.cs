using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace sortTitle
{
    public class adminUsers
    {
        public string st;
        public int Kind;
        public adminUsers()
        {
            Kind = Common.Functions.ConvertInt16(HttpContext.Current.Request.QueryString["k"], 0);
            switch (Kind)
            {
                case 0: st = "管理员"; break;
            }
        }
    }
}
