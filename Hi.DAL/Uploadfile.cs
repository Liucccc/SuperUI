using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Dal
{
    public class Uploadfile
    {

        #region ==查询列表（带分页）==
        public static DataTable Select_List(ref Model.Pages p, ref long Rc, string Main_parameter, string Extended_parameter, string Uid, string Uid_not, string Order)
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
            if (Uid == null)
                Uid = "";
            if (Uid_not == null)
                Uid_not = "";
            if (Order == null)
                Order = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Uploadfile_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Uid", SqlDbType.VarChar, 8000).Value = Uid;
            sc.Parameters.Add("@Uid_not", SqlDbType.VarChar, 8000).Value = Uid_not;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = Order;
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
                int Pc = Common.Functions.ConvertInt16(Rc / p.Ps, 0);
                if (Rc % p.Ps != 0)
                    Pc++;
                p.page_str = Common.Functions.Pagination(Pc, p.Page, p.Tp, p.Pname, p.Previous, p.Next, p.pageName, p.inputHeight, p.sk, p.method);
            }
            return dt;
        }
        #endregion

        #region ==查询详细==
        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter,long Uid, string Uid_not)
        {
            #region ==null判断==
            if (Extended_parameter == null)
                Extended_parameter = "";
            if (Main_parameter == null)
                Main_parameter = "";
            if (Uid_not == null)
                Uid_not = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_Uploadfile_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Uid", SqlDbType.VarChar, 8000).Value = Uid.ToString();
            sc.Parameters.Add("@Uid_not", SqlDbType.VarChar, 8000).Value = Uid_not;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            sc.Parameters.Add("@rc", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            sc.Dispose();
            cfg.closeDb();

            return dt;
        }
        #endregion

        #region ==添加==
        public static long Add(string Url)
        {
            if (Url == null)
                Url = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_Uploadfile_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Url", SqlDbType.VarChar, 200).Value = Url;
            sc.Parameters.Add("@Uid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Uid = Common.Functions.ConvertInt64(sc.Parameters["@Uid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();

            return Uid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(long Uid, string Url)
        {
            if (Url == null)
                Url = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_Uploadfile_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Uid", SqlDbType.BigInt).Value = Uid;
            sc.Parameters.Add("@Url", SqlDbType.VarChar, 200).Value = Url;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

    }
}
