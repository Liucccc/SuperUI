using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class Article
    {
        #region ==查询列表（带分页）==
        public static DataTable Select_List(ref Model.Pages p, string Main_parameter, string Extended_parameter, string Keywords, string Kind, int Alive, string d1, string d2, string Aid,string Asid, string Order, int Hot, int Recommend, ref long Rc)
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
            if (Kind == null)
                Kind = "";
            if (d1 == null)
                d1 = "";
            if (d2 == null)
                d2 = "";
            if (Aid == null)
                Aid = "";
            if (Asid == null)
                Asid = "";
            if (Order == null)
                Order = "";



            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = Order;
            sc.Parameters.Add("@Aid", SqlDbType.VarChar, 8000).Value = Aid;
            sc.Parameters.Add("@Asid", SqlDbType.VarChar, 8000).Value = Asid;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = Keywords;
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Hot", SqlDbType.SmallInt).Value = Hot;
            sc.Parameters.Add("@Recommend", SqlDbType.SmallInt).Value = Recommend;
            sc.Parameters.Add("@Kind", SqlDbType.VarChar, 8000).Value = Kind;
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

        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter, long Aid, int Alive)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            sc.Parameters.Add("@Aid", SqlDbType.VarChar, 8000).Value = Aid.ToString();
            sc.Parameters.Add("@Asid", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Hot", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Recommend", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Kind", SqlDbType.VarChar, 8000).Value = "";
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
        public static long Add(string Atitle, string Url, string Source, string Author, int Kind, string Ainfo, string Atime, string Pic1, string Pic2, string Summary,long Asid, string ieTitle, string seoKeywords, string seoDescription)
        {
            if (Atitle == null)
                Atitle = "";
            if (Url == null)
                Url = "";
            if (Source == null)
                Source = "";
            if (Author == null)
                Author = "";
            if (Ainfo == null)
                Ainfo = "";
            if (Atime == "0001-01-01 00:00:00")
                Atime = DateTime.Now.ToString();
            if (Pic1 == null)
                Pic1 = "";
            if (Pic2 == null)
                Pic2 = "";
            if (Summary == null)
                Summary = "";
            if (ieTitle == null)
                ieTitle = "";
            if (seoKeywords == null)
                seoKeywords = "";
            if (seoDescription == null)
                seoDescription = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Atitle", SqlDbType.VarChar, 1000).Value = Atitle;
            sc.Parameters.Add("@Url", SqlDbType.VarChar, 1000).Value = Url;
            sc.Parameters.Add("@Source", SqlDbType.VarChar, 50).Value = Source;
            sc.Parameters.Add("@Author", SqlDbType.VarChar, 50).Value = Author;
            sc.Parameters.Add("@Kind", SqlDbType.SmallInt).Value = Kind;
            sc.Parameters.Add("@Ainfo", SqlDbType.Text).Value = Ainfo;
            sc.Parameters.Add("@Atime", SqlDbType.VarChar, 20).Value = Atime;
            sc.Parameters.Add("@Pic1", SqlDbType.VarChar, 100).Value = Pic1;
            sc.Parameters.Add("@Pic2", SqlDbType.VarChar, 100).Value = Pic2;
            sc.Parameters.Add("@Summary", SqlDbType.VarChar, 8000).Value = Summary;
            sc.Parameters.Add("@Asid", SqlDbType.BigInt,8).Value = Asid;
            sc.Parameters.Add("@ieTitle", SqlDbType.VarChar, 100).Value = ieTitle;
            sc.Parameters.Add("@seoKeywords", SqlDbType.VarChar, 8000).Value = seoKeywords;
            sc.Parameters.Add("@seoDescription", SqlDbType.VarChar, 8000).Value = seoDescription;
            sc.Parameters.Add("@Aid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Aid = Common.Functions.ConvertInt64(sc.Parameters["@Aid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();
            return Aid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(long Aid, string Atitle, string Url,string Source, string Author, string Ainfo, string Atime, string Pic1, string Pic2, string Summary,long Asid, string ieTitle, string seoKeywords, string seoDescription)
        {
            if (Atitle == null)
                Atitle = "";
            if (Url == null)
                Url = "";
            if (Source == null)
                Source = "";
            if (Author == null)
                Author = "";
            if (Ainfo == null)
                Ainfo = "";
            if (Atime == "0001-01-01 00:00:00")
                Atime = DateTime.Now.ToString();
            if (Pic1 == null)
                Pic1 = "";
            if (Pic2 == null)
                Pic2 = "";
            if (Summary == null)
                Summary = "";
            if (ieTitle == null)
                ieTitle = "";
            if (seoKeywords == null)
                seoKeywords = "";
            if (seoDescription == null)
                seoDescription = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.BigInt).Value = Aid;
            sc.Parameters.Add("@Atitle", SqlDbType.VarChar, 1000).Value = Atitle;
            sc.Parameters.Add("@Url", SqlDbType.VarChar, 1000).Value = Url;
            sc.Parameters.Add("@Source", SqlDbType.VarChar, 50).Value = Source;
            sc.Parameters.Add("@Author", SqlDbType.VarChar, 50).Value = Author;
            sc.Parameters.Add("@Ainfo", SqlDbType.Text).Value = Ainfo;
            sc.Parameters.Add("@Atime", SqlDbType.VarChar, 20).Value = Atime;
            sc.Parameters.Add("@Pic1", SqlDbType.VarChar, 100).Value = Pic1;
            sc.Parameters.Add("@Pic2", SqlDbType.VarChar, 100).Value = Pic2;
            sc.Parameters.Add("@Summary", SqlDbType.Text).Value = Summary;
            sc.Parameters.Add("@Asid", SqlDbType.BigInt,8).Value = Asid;
            sc.Parameters.Add("@ieTitle", SqlDbType.VarChar, 100).Value = ieTitle;
            sc.Parameters.Add("@seoKeywords", SqlDbType.VarChar, 8000).Value = seoKeywords;
            sc.Parameters.Add("@seoDescription", SqlDbType.VarChar, 8000).Value = seoDescription;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改序号==
        public static void Layer(long Aid, long Layer)
        {
            string sql = "update [zzlh2017_Article] set";
            sql += " Layer=" + Layer;
            sql += " where Aid=" + Aid;
            Dal.sqlH.ExecuteNonQuery(sql);
        }
        #endregion

        #region ==修改屏蔽状态==
        public static void Alive(long Aid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Alive", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.BigInt).Value = Aid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改热点文章状态==
        public static void Hot(long Aid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Hot", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.BigInt).Value = Aid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改推荐文章状态==
        public static void Recommend(long Aid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Recommend", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aid", SqlDbType.BigInt).Value = Aid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==返回Article数量==
        public static long GetCount(int Alive, string Kind)
        {
            if (Kind == null)
                Kind = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Article_Count", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Kind", SqlDbType.VarChar, 8000).Value = Kind;
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@rc", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Rc = Common.Functions.ConvertInt64(sc.Parameters["@rc"].Value, 0);
            sc.Dispose();
            cfg.closeDb();
            return Rc;
        }
        #endregion
    }
}
