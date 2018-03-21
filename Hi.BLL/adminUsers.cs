using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Bll
{
    /// <summary>
    /// 管理员表
    /// 2016-09-22
    /// </summary>
    public class adminUsers
    {
        /// <summary>
        /// 扩展参数需要补充主参数 参照
        /// </summary>
        public static string[] demand = { "Area,Aulid", "Ltitle,Aulid" };

        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        /// <param name="Rc">返回记录总条数</param>
        public static List<Model.adminUsers> Select_List(ref Model.Pages p, Model.adminUsers m, ref long Rc)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            DataTable dt = Dal.adminUsers.Select_List(ref p, m.s_Keywords, m.s_Auid, m.s_Alive, m.s_Aulid, m.s_Order, m.s_Main_parameter, m.s_Extended_parameter, ref Rc);
            DataRow dr;
            List<Model.adminUsers> l = new List<Model.adminUsers>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.adminUsers();

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Auid"))
                    m.Auid = Common.Functions.ConvertInt64(dr["Auid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Auser"))
                    m.Auser = dr["Auser"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Passwd"))
                    m.Passwd = dr["Passwd"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Aulid"))
                    m.Aulid = Common.Functions.ConvertInt32(dr["Aulid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Token"))
                    m.Token = dr["Token"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Token_expiry"))
                    m.Token_expiry = Common.Functions.ConvertDateTime(dr["Token_expiry"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Token_IP"))
                    m.Token_IP = dr["Token_IP"].ToString();
                #endregion

                #region ==扩展参数==
                if (Common.Functions.checkHave(return_value[1], "Area"))
                    m.Areas = dr["Area"].ToString().Split(',');
                if (Common.Functions.checkHave(return_value[1], "Ltitle"))
                    m.Ltitle = dr["Ltitle"].ToString();
                #endregion

                l.Add(m);
            }

            dt.Dispose();
           return l;       
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细
        /// </summary>
        /// <param name="m">d系列 + m.s_*_parameter</param>
        public static Model.adminUsers Select_Detail(Model.adminUsers m)
        {
            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter(m.s_Total_parameter, m.s_Main_parameter, m.s_Extended_parameter, demand);
            m.s_Main_parameter = return_value[0];
            m.s_Extended_parameter = return_value[1];
            #endregion

            #region 声明变量
            //ip
            string Token_Ip = "";
            //有效期
            string Token_expiry = "";
            //新生成的Token
            string Token = "";
            #endregion

            #region 如果d_IsUpdaeToken=1 更新Token，则获取Token
            if (m.d_IsUpdateToken == 1)
            {
                Token = GetToken(m.d_Auser, ref Token_expiry, ref Token_Ip);
            }
            #endregion

            DataTable dt = Dal.adminUsers.Select_Detail(m.s_Main_parameter, m.s_Extended_parameter, m.d_Auid, m.d_Auser, m.d_Alive, m.d_Auid_not, m.d_Token, Token, Token_expiry, Token_Ip);
            m = new Model.adminUsers();
            try
            {
                DataRow dr = dt.Rows[0];   

                #region ==主参数==
                if (Common.Functions.checkHave(return_value[0], "Auid"))
                    m.Auid = Common.Functions.ConvertInt64(dr["Auid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Auser"))
                    m.Auser = dr["Auser"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Passwd"))
                    m.Passwd = dr["Passwd"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Aulid"))
                    m.Aulid = Common.Functions.ConvertInt32(dr["Aulid"], 0);
                if (Common.Functions.checkHave(return_value[0], "Alive"))
                    m.Alive = Convert.ToBoolean(dr["Alive"]);
                if (Common.Functions.checkHave(return_value[0], "Token"))
                    m.Token = dr["Token"].ToString();
                if (Common.Functions.checkHave(return_value[0], "Token_expiry"))
                    m.Token_expiry = Common.Functions.ConvertDateTime(dr["Token_expiry"], Convert.ToDateTime(Common.Para.dt_def));
                if (Common.Functions.checkHave(return_value[0], "Token_IP"))
                    m.Token_IP = dr["Token_IP"].ToString();
                #endregion

                #region ==扩展参数==
                if (Common.Functions.checkHave(return_value[1], "Area"))
                    m.Areas = dr["Area"].ToString().Split(',');
                if (Common.Functions.checkHave(return_value[1], "Ltitle"))
                    m.Ltitle = dr["Ltitle"].ToString();
                #endregion

            }
            catch
            {
                m.Auid = 0;
                m.Auser = "";
                m.Passwd = "";
                m.Aulid = 0;
                m.Alive = true;
                m.Areas =new string[0] ;
                m.Ltitle = "";
            }
            dt.Dispose();
            return m;
        }
        #endregion

        #region ==姓名+原始密码获取Token (登录使用)==
        /// <summary>
        /// 姓名+原始密码获取Token，(登录使用) 返回Token  -1为姓名或者密码错误或者账号屏蔽
        /// </summary>
        /// <param name="m">Auser+Passwd(原始密码)</param>
        /// <returns>返回Token  -1为姓名或者密码错误或者账号屏蔽</returns>
        public static string Select_Login(Model.adminUsers m)
        {

            #region 声明变量
            long Rc = 0;
            string result = "-1";
            //ip
            string Token_Ip = "";
            //有效期
            string Token_expiry = "";
            //新生成的Token
            string Token = "";
            #endregion

            #region 非空验证
            if (m.Auser == "" || m.Passwd == "")
            {
                return result;
            }
            #endregion

            #region 获取Token
            Token = GetToken(m.Auser, ref Token_expiry, ref Token_Ip);
            #endregion

            string Main = "Aulid,Auser,Auid,Token,Token_expiry,Token_IP";
            string Extended = "";

            #region ==返回参数处理==
            string[] return_value = Common.Functions.Parameters_Filter("", Main, Extended, demand);
            Main = return_value[0];
            Extended = return_value[1];
            #endregion

            m.Passwd = Common.Functions.updatePasswd(m.Passwd);
            Dal.adminUsers.Login(Main, Extended, m.Auser, m.Passwd, 1, Token, Token_expiry, Token_Ip, ref Rc);

            if (Rc > 0)
            {
                return Token;
            }
            return result;
        }
        #endregion

        #region 根据姓名生成Token，返回Token，输出Token的有效期和ip
        /// <summary>
        /// 根据邮箱生成Token，返回Token，输出Token的有效期和ip
        /// </summary>
        /// <param name="Auser">姓名</param>
        /// <param name="Token_expiry">输出参数Token有效期</param>
        /// <param name="Token_Ip">输出参数 Token对应的ip</param>
        /// <returns>生成的Token</returns>
        private static string GetToken(string Auser, ref string Token_expiry, ref string Token_Ip)
        {

            #region 获得Ip
            Token_Ip = Common.Functions.getIp();
            #endregion

            #region 生成的Token
            List<string> ParameterList = new List<string>();
            ParameterList.Add("Ip=" + Token_Ip + "");
            ParameterList.Add("Auser=" + Auser + "");
            string New_Token = Bll.Cookie.BuildToken(ParameterList);
            #endregion

            #region Token的有效期
            Token_expiry = DateTime.Now.AddMinutes(Common.Para.EffectiveTime).ToString();
            #endregion

            return New_Token;
        }
        #endregion

        #region ==删除==
        ///<summary>
        /// 删除
        /// </summary>
        /// <param name="Auid">要删除的编号，多个编号用，分隔</param>
        public static void Del(string Auid)
        {
            Common.Config cfg = new Common.Config();
            sortTitle.adminUsers st = new sortTitle.adminUsers();
            cfg.connDb();
            Common.Functions.recordDel("[zzlh2017_adminUsers]", "Auid", Auid, 0, "", "Auser", st.st, cfg.Conn, true);
            cfg.closeDb();
        }
        #endregion

        #region ==添加==
        /// <summary>
        /// 添加。返回新纪录主码值
        /// </summary>
        /// <param name="m">添加项</param>
        public static long Add(Model.adminUsers m)
        {
            long Auid = Dal.adminUsers.Add(m.Auser,Common.Functions.updatePasswd(m.Passwd),m.Aulid);

            return Auid;
        }
        #endregion

        #region ==修改基本信息==
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="m">d_Auid+修改项</param>
        public static void Modify(Model.adminUsers m)
        {
            Dal.adminUsers.Modify(m.d_Auid, m.Auser, m.Aulid,  Common.Functions.updatePasswd(m.Passwd));
        }
        #endregion

        #region ==修改屏蔽状态==
        /// <summary>
        ///  修改屏蔽状态
        /// </summary>
        /// <param name="m">d_Auid</param>
        public static void Alive(Model.adminUsers m)
        {
            Dal.adminUsers.Alive(m.d_Auid);
        }
        #endregion

        #region ==修改密码==
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="m">d_Auid+Passwd</param>
        public static void Passwd(Model.adminUsers m)
        {
            Dal.adminUsers.Passwd(m.d_Auid,Common.Functions.updatePasswd(m.Passwd)) ;
        }
        #endregion
    }
}
