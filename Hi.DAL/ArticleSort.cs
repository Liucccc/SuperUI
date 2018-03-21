using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dal
{
    public class ArticleSort
    {
        #region ==查询列表（带分页）==
        public static DataTable Select_List(ref Model.Pages p, ref long Rc, string Main_parameter, string Extended_parameter, string Keywords, string Asid, string Kind, int Alive, string Order)
        {
            #region ==null判断==
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

            if (Main_parameter == null)
                Main_parameter = "";
            if (Extended_parameter == null)
                Extended_parameter = "";
            if (Keywords == null)
                Keywords = "";
            if (Asid == null)
                Asid = "";
            if (Kind == null)
                Kind = "";
            if (Order == null)
                Order = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_ArticleSort_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = Keywords;
            sc.Parameters.Add("@Asid", SqlDbType.VarChar, 8000).Value = Asid;
            sc.Parameters.Add("@Kind", SqlDbType.VarChar, 8000).Value = Kind;
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = Order;
            IDataParameter parameters_Rc = new SqlParameter("@Rc", SqlDbType.BigInt, 8);
            parameters_Rc.Direction = ParameterDirection.Output;
            sc.Parameters.Add(parameters_Rc);
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Rc = Common.Functions.ConvertInt64(parameters_Rc.Value, 0);
            da.Dispose();
            sc.Dispose();
            cfg.closeDb();

            #region 分页
            if (p.Ps == 0)
                p.page_str = "&nbsp;";
            else
            {
                int Pc = Common.Functions.ConvertInt16(Rc / p.Ps, 0);
                if (Rc % p.Ps != 0)
                    Pc++;
                p.page_str = Common.Functions.Pagination(Pc, p.Page, p.Tp, p.Pname, p.Previous, p.Next, p.pageName, p.inputHeight, p.sk, p.method);
            }
            #endregion

            return dt;
            dt.Dispose();
        }
        #endregion

        #region ==查询详细==
        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter, long Asid, int Alive)
        {
            #region ==null判断==
            if (Main_parameter == null)
                Main_parameter = "";
            if (Extended_parameter == null)
                Extended_parameter = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_ArticleSort_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Asid", SqlDbType.VarChar, 8000).Value = Asid.ToString();
            sc.Parameters.Add("@Kind", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            IDataParameter parameters_Rc = new SqlParameter("@Rc", SqlDbType.BigInt, 8);
            parameters_Rc.Direction = ParameterDirection.Output;
            sc.Parameters.Add(parameters_Rc);
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            sc.Dispose();
            cfg.closeDb();

            return dt;
            dt.Dispose();
        }
        #endregion

        #region ==添加==
        public static long Add(string Stitle, int Kind)
        {
            if (Stitle == null)
                Stitle = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_ArticleSort_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Kind", SqlDbType.SmallInt).Value = Kind;
            sc.Parameters.Add("@Stitle", SqlDbType.VarChar, 20).Value = Stitle;
            sc.Parameters.Add("@Asid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Asid = Common.Functions.ConvertInt64(sc.Parameters["@Asid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();

            return Asid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(long Asid, string Stitle)
        {
            if (Stitle == null)
                Stitle = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_ArticleSort_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Asid", SqlDbType.BigInt).Value = Asid;
            sc.Parameters.Add("@Stitle", SqlDbType.VarChar, 20).Value = Stitle;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改屏蔽状态==
        public static void Alive(long Asid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_ArticleSort_Alive", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Asid", SqlDbType.BigInt).Value = Asid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改序号==
        public static void Layer(long Asid, long Layer)
        {
            string sql = "update [zzlh2017_ArticleSort] set";
            sql += " Layer=" + Layer;
            sql += " where Asid=" + Asid;
            Dal.sqlH.ExecuteNonQuery(sql);
        }
        #endregion
    }
}