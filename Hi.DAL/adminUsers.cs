using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Dal
{
    public class adminUsers
    {
        #region ==查询列表（带分页）==
        public static DataTable Select_List(ref Model.Pages p, string Keywords, string Auid, int Alive, string Aulid, string Order, string Main_parameter, string Extended_parameter, ref long Rc)
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
            if (Auid == null)
                Auid = "";
            if (Aulid == null)
                Aulid = "";
            if (Order == null)
                Order = "";
            if (Extended_parameter == null)
                Extended_parameter = "";
            if (Main_parameter == null)
                Main_parameter = "";

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = Keywords;
            sc.Parameters.Add("@Auid", SqlDbType.VarChar, 8000).Value = Auid;
            sc.Parameters.Add("@Aulid", SqlDbType.VarChar, 8000).Value = Aulid;
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Auid_not", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Auser", SqlDbType.VarChar, 20).Value = "";
            sc.Parameters.Add("@Passwd", SqlDbType.VarChar, 32).Value = "";
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = Order;
            sc.Parameters.Add("@Token", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@New_Token", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@New_Token_expiry", SqlDbType.VarChar, 100).Value = "";
            sc.Parameters.Add("@New_Token_IP", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
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
            #region ==分页==
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
            #endregion
            return dt;
            dt.Dispose();
        }
        #endregion

        #region ==查询详细==
        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter, long Auid, string Auser, int Alive, long Auid_not, string Token, string New_Token, string New_Token_expiry, string New_Token_IP)
        {
            #region ==null判断==
            if (Main_parameter == null)
                Main_parameter = "";
            if (Extended_parameter == null)
                Extended_parameter = "";
            if (Auser == null)
                Auser = "";
            if (Token == null)
                Token = "";
            if (New_Token == null)
                New_Token = "";
            if (New_Token_IP == null)
                New_Token_IP = "";
            if (New_Token_expiry == null)
                New_Token_expiry = DateTime.Now.ToString();
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Auid", SqlDbType.VarChar, 8000).Value = Auid.ToString();
            sc.Parameters.Add("@Aulid", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Auid_not", SqlDbType.VarChar, 8000).Value = Auid_not.ToString();
            sc.Parameters.Add("@Auser", SqlDbType.VarChar, 20).Value = Auser;
            sc.Parameters.Add("@Passwd", SqlDbType.VarChar, 32).Value = "";
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            sc.Parameters.Add("@Token", SqlDbType.VarChar, 8000).Value = Token;
            sc.Parameters.Add("@New_Token", SqlDbType.VarChar, 8000).Value = New_Token;
            sc.Parameters.Add("@New_Token_expiry", SqlDbType.VarChar, 100).Value = New_Token_expiry;
            sc.Parameters.Add("@New_Token_IP", SqlDbType.VarChar, 8000).Value = New_Token_IP;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
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

        #region ==登录查询Token==
        public static void Login(string Main_parameter, string Extended_parameter,string Auser, string Passwd, int Alive, string New_Token, string New_Token_expiry, string New_Token_IP, ref long Rc)
        {
            #region ==null判断==
            if (Passwd == null)
                Passwd = "";
            if (Auser == null)
                Auser = "";
            if (New_Token == null)
                New_Token = "";
            if (New_Token_IP == null)
                New_Token_IP = "";
            if (New_Token_expiry == null)
                New_Token_expiry = DateTime.Now.ToString();
            if (Extended_parameter == null)
                Extended_parameter = "";
            if (Main_parameter == null)
                Main_parameter = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Auid", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Aulid", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Alive", SqlDbType.SmallInt).Value = Alive;
            sc.Parameters.Add("@Auid_not", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Auser", SqlDbType.VarChar, 20).Value = Auser;
            sc.Parameters.Add("@Passwd", SqlDbType.Char, 32).Value = Passwd;
            sc.Parameters.Add("@Order", SqlDbType.VarChar, 200).Value = "";
            sc.Parameters.Add("@Token", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@New_Token", SqlDbType.VarChar, 8000).Value = New_Token;
            sc.Parameters.Add("@New_Token_expiry", SqlDbType.VarChar, 100).Value = New_Token_expiry;
            sc.Parameters.Add("@New_Token_IP", SqlDbType.VarChar, 8000).Value = New_Token_IP;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            IDataParameter parameters_Rc = new SqlParameter("@Rc", SqlDbType.BigInt, 8);
            parameters_Rc.Direction = ParameterDirection.Output;
            sc.Parameters.Add(parameters_Rc);
            SqlDataAdapter da = new SqlDataAdapter(sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Rc = Common.Functions.ConvertInt64(parameters_Rc.Value, 0);
            sc.Dispose();
            da.Dispose();
            cfg.closeDb();
            dt.Dispose();
        }
        #endregion

        #region ==添加==
        public static long Add(string Auser, string Passwd, int Aulid)
        {
            if (Auser == null)
                Auser = "";
            if (Passwd == null)
                Passwd = ""; 
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Auser", SqlDbType.VarChar, 20).Value = Auser;
            sc.Parameters.Add("@Passwd", SqlDbType.Char, 32).Value = Passwd;
            sc.Parameters.Add("@Aulid", SqlDbType.SmallInt).Value = Aulid;
            sc.Parameters.Add("@Auid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            sc.ExecuteNonQuery();
            long Auid = Common.Functions.ConvertInt64(sc.Parameters["@Auid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();

            return Auid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(long Auid, string Auser, int Aulid, string Passwd)
        {
            if (Auser == null)
                Auser = "";
            if (Passwd == null)
                Passwd = "";
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Auid", SqlDbType.BigInt).Value = Auid;
            sc.Parameters.Add("@Auser", SqlDbType.VarChar, 20).Value = Auser;
            sc.Parameters.Add("@Passwd", SqlDbType.Char, 32).Value = Passwd;        
            sc.Parameters.Add("@Aulid", SqlDbType.SmallInt).Value = Aulid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==修改屏蔽状态==
        public static void Alive(long Auid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Alive", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Auid", SqlDbType.SmallInt).Value = Auid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region =修改密码==
        public static void Passwd(long Auid, string Passwd)
        {
            if (Passwd == null) 
            {
                Passwd = "";
            }
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsers_Passwd", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Auid", SqlDbType.SmallInt).Value = Auid;
            sc.Parameters.Add("@Passwd", SqlDbType.Char, 32).Value = Passwd;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion
    }
}
