using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;

namespace Dal
{
    public class adminUsersLevel
    {

        #region ==查询列表（带分页）==   
        public static DataTable Select_List(ref Model.Pages p, ref long Rc, string Main_parameter, string Extended_parameter, string Keywords, string Aulid, int Alive, string Order)
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
            if (Aulid == null)
                Aulid = "";            
            if (Order == null)
                Order = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsersLevel_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = p.Page;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = p.Ps;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = Keywords;
            sc.Parameters.Add("@Aulid", SqlDbType.VarChar, 8000).Value = Aulid;
            sc.Parameters.Add("@Aulid_not", SqlDbType.VarChar, 8000).Value = "";
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
        public static DataTable Select_Detail(string Main_parameter, string Extended_parameter, int Aulid, int Aulid_not, int Alive)
        {
            #region ==null判断==
            if (Main_parameter == null)
                Main_parameter = "";
            if (Extended_parameter == null)
                Extended_parameter = "";
            #endregion

            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsersLevel_Select", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Page", SqlDbType.Int).Value = 0;
            sc.Parameters.Add("@Ps", SqlDbType.SmallInt).Value = 0;
            sc.Parameters.Add("@Main_parameter", SqlDbType.VarChar, 8000).Value = Main_parameter;
            sc.Parameters.Add("@Extended_parameter", SqlDbType.VarChar, 8000).Value = Extended_parameter;
            sc.Parameters.Add("@Keywords", SqlDbType.VarChar, 8000).Value = "";
            sc.Parameters.Add("@Aulid", SqlDbType.VarChar,8000).Value = Aulid.ToString();
            sc.Parameters.Add("@Aulid_not", SqlDbType.VarChar, 8000).Value = Aulid_not.ToString();
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
        public static int Add(int Aulid, string Ltitle, string Area)
        {
            if (Ltitle == null)
                Ltitle = "";
            if (Area == null)
                Area = "";

            Area = getArea(Area);
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_adminUsersLevel_Add", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Area", SqlDbType.VarChar, 50).Value = Area;
            sc.Parameters.Add("@Ltitle", SqlDbType.VarChar, 20).Value = Ltitle;
            sc.Parameters.Add("@Aulid", SqlDbType.SmallInt).Value = Aulid;
            sc.ExecuteNonQuery();
            Aulid = Common.Functions.ConvertInt16(sc.Parameters["@Aulid"].Value, 0);
            sc.Dispose();
            cfg.closeDb();

            return Aulid;
        }
        #endregion

        #region ==修改基本信息==
        public static void Modify(int Aulid_old,int Aulid, string Ltitle, string Area)
        {
            if (Ltitle == null)
                Ltitle = "";
            if (Area == null)
                Area = "-1";
            
            Area = getArea(Area);
            Common.Config cfg = new Common.Config();
            cfg.connDb();

            SqlCommand sc = new SqlCommand("zzlh2017_adminUsersLevel_Modify", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure; 
            sc.Parameters.Add("@Aulid_old", SqlDbType.SmallInt).Value = Aulid_old;
            sc.Parameters.Add("@Aulid", SqlDbType.SmallInt).Value = Aulid;
            sc.Parameters.Add("@Ltitle", SqlDbType.VarChar, 20).Value = Ltitle;
            sc.Parameters.Add("@Area", SqlDbType.VarChar, 50).Value = Area;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion    

        #region ==修改屏蔽状态==
        public static void Alive(int Aulid)
        {
            Common.Config cfg = new Common.Config();
            cfg.connDb();
            SqlCommand sc = new SqlCommand("zzlh2017_adminUsersLevel_Alive", cfg.Conn);
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add("@Aulid", SqlDbType.SmallInt).Value = Aulid;
            sc.ExecuteNonQuery();
            sc.Dispose();
            cfg.closeDb();
        }
        #endregion

        #region ==获得管理范围==
        protected static string getArea(string Area)
        {
            if (Area == null)
                Area = "-1";
            string a = "";
            for (int i = 0; i < Common.Para.area_name.Length; i++)
            {
                if (i > 0)
                    a += ",";
                if (Common.Functions.checkHave(Area, i.ToString()))
                    a += "1";
                else
                    a += "0";
            }
            return a;
        }
        #endregion
       
    }
}