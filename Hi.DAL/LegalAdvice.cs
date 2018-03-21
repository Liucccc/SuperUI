using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class LegalAdvice
    {
        #region ==查询列表（带分页）==
        public static DataTable Select_List(ref Model.Pages p, string Main_parameter, string Extended_parameter, string Keywords, string Lid, string Order, string Ostatus, string d1, string d2, ref long Rc)
        {
            if (p.Page < 1)
                p.Page = 1;
            if (p.Pname == null)
                p.Pname = "p";
            if (p.Tp == null)
                p.Tp = p.Pname;
            if (p.Previous == null)
                p.Previous = "上一页";
            if (p.Next == null)
                p.Next = "下一页";
            if (p.pageName == null)
                p.pageName = "页";
            if (p.sk == null)
                p.sk = "";
            if (p.method == null)
                p.method = "get";
            if (Keywords == null)
                Keywords = "";
            if (Lid == null)
                Lid = "";
            if (Order == null)
                Order = "";
            if (Ostatus == null)
                Ostatus = "";
            if (d1 == null)
                d1 = "";
            if (d2 == null)
                d2 = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_LegalAdvice_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = Order;
            sc.Parameters.Add("@Lid", SqlDbType.VarChar, 8000).Value = Lid;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = Keywords;
            sc.Parameters.Add("@Ostatus", SqlDbType.VarChar, 8000).Value = Ostatus;
            sc.Parameters.Add("@d1", SqlDbType.VarChar, 20).Value = d1;
            sc.Parameters.Add("@d2", SqlDbType.VarChar, 20).Value = d2;
            sc.Parameters.Add("@rc", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            Rc = Common.Functions.ConvertInt64(sc.Parameters["@rc"].Value, 0);
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            sc.Dispose();
            cfg.closeDb();
            if (p.Ps == 0)
                p.page_str = "&nbsp;";
            else
            {
                p.Pc = Common.Functions.ConvertInt16(Rc / p.Ps, 0);
                if (Rc % p.Ps != 0)
                    p.Pc++;
                if (p.PageStyle == 0)
                    p.page_str = Common.Functions.Pagination(p.Pc, p.Page, p.Tp, p.Pname, p.Previous, p.Next, p.pageName, p.inputHeight, p.sk, p.method);
                else if (p.PageStyle == 1)
                {
                    p.page_str = Common.Functions.Pagination1(p.Pc, p.Page, p.Tp, p.Pname, p.First, p.Last, p.Previous, p.Next, p.pageName, p.sk, p.c);
                }
            }
            return dt;
            dt.Dispose();
        }
        #endregion

        #region ==查询详细==

        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter, long Lid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_LegalAdvice_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            sc.Parameters.Add("@Lid", SqlDbType.VarChar, 8000).Value = Lid.ToString();
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Ostatus", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@d1", SqlDbType.VarChar, 20).Value = "";
            sc.Parameters.Add("@d2", SqlDbType.VarChar, 20).Value = "";
            sc.Parameters.Add("@rc", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sc.Dispose();
            da.Dispose();
            cfg.closeDb();
            return dt;
            dt.Dispose();
        }
        #endregion

        #region ==添加==
        public static long Add(string Ltitle, string Linfo, string Lname, string Tel, string QQ, string Email)
        {
            if (Ltitle == null)
                Ltitle = "";
            if (Linfo == null)
                Linfo = "";
            if (Lname == null)
                Lname = "";
            if (Tel == null)
                Tel = "";
            if (QQ == null)
                QQ = "";
            if (Email == null)
                Email = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_LegalAdvice_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Ltitle", SqlDbType.VarChar, 100).Value = Ltitle;
            sc.Parameters.Add("@Linfo", SqlDbType.VarChar, 8000).Value = Linfo;
            sc.Parameters.Add("@Lname", SqlDbType.VarChar, 50).Value = Lname;
            sc.Parameters.Add("@Tel", SqlDbType.VarChar, 50).Value = Tel;
            sc.Parameters.Add("@QQ", SqlDbType.VarChar, 50).Value = QQ;
            sc.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;
            sc.Parameters.Add("@Lid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Lid = Common.Functions.ConvertInt64(sc.Parameters["@Lid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();
            return Lid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(long Lid, string Reply)
        {
            if (Reply == null)
                Reply = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_LegalAdvice_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Lid", SqlDbType.BigInt).Value = Lid;
            sc.Parameters.Add("@Reply", SqlDbType.VarChar, 8000).Value = Reply;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion
    }
}
