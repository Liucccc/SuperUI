using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Bll
{
    /// <summary>
    /// 后台 Cookie操作
    /// </summary>
    public class Cookie
    {
        #region 取登录Cookie中的值
        public static string getCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Common.Para.cookie_admins];
            try
            {
                name = name.ToLower();
                if (name == "muser")
                {
                    return HttpContext.Current.Server.UrlDecode(cookie.Values[name]);
                }
                return cookie.Values[name];
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region  生成Token
        public static string BuildToken(List<string> ParameterList)
        {
            #region 声明变量
            //Token
            string Token = "";
            #endregion

            #region 参数验证
            if (ParameterList == null)
            {
                ParameterList = new List<string>();
            }
            #endregion

            #region 生成随机数
            //随机数
            string non_str = Common.Functions.createRandomStr(32, 5);
            #endregion

            #region 生成时间戳
            string stamp = Common.Functions.create_timestamp(DateTime.UtcNow);
            #endregion

            #region 把随机数，时间戳，添加到ParameterList中
            //随机数
            ParameterList.Add("non_str=" + non_str + "");
            //时间戳
            ParameterList.Add("stamp=" + stamp + "");
            #endregion

            #region 排序ParameterList
            ParameterList.Sort();
            #endregion

            #region 拼接参数
            for (int i = 0; i < ParameterList.Count; i++)
            {
                Token += ParameterList[i];
            }
            #endregion

            #region Sha 加密
            Token = Common.Functions.sha1(Token);
            #endregion

            return Token;
        }
        #endregion

        #region 验证后台登录==

        #region
        /// <summary>
        /// 验证后台登录，没有登录跳转到登录页面，登录了修改数据库Token 和 cookie中的Token 返回用户详细  
        /// </summary>
        /// <param name="IsUpdateToken">true 更新Token false 不需要更新Token</param>
        /// <returns>用户详细  </returns>
        public static Model.adminUsers checkSession(bool IsUpdateToken)
        {
            Model.adminUsers m = new Model.adminUsers();
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Common.Para.cookie_admins];
            if (cookie == null)
            {
                HttpContext.Current.Response.Redirect("/" + Common.Para.siteHead + "6h5y4m7d8u8r9b1a/location.htm");
                return m;
            }
            //获得cookie token 值
            string adminUsers_token = Common.Functions.convers(cookie.Values["adminUsers_token"]);
            string adminUsers_Auser = Common.Functions.convers(cookie.Values["adminUsers_Auser"]);
            if (adminUsers_token == "")
            {
                HttpContext.Current.Response.Redirect("/" + Common.Para.siteHead + "6h5y4m7d8u8r9b1a/location.htm");
                return m;
            }
            m.d_Token = adminUsers_token;
            m.d_Alive = 1;
            m.d_Auser = adminUsers_Auser;
            #region 更新Token 并返回用户详细
            if (IsUpdateToken)
            {
                m.d_IsUpdateToken = 1;
            }
            #endregion

            m.s_Main_parameter = "Auid,Auser,Token,Token_expiry,Token_IP,Alive,Aulid";
            m.s_Extended_parameter = "Area,Ltitle";

            m = Bll.adminUsers.Select_Detail(m);
            if (m.Auid == 0)
            {
                HttpContext.Current.Response.Redirect("/" + Common.Para.siteHead + "6h5y4m7d8u8r9b1a/location.htm");
                return m;
            }
            #region 保存cookie
            if (IsUpdateToken)
            {
                HttpContext.Current.Response.Clear();
                HttpCookie Save_cookie = new HttpCookie(Common.Para.cookie_admins);
                Save_cookie.Values.Add("adminUsers_token", m.Token);
                Save_cookie.Values.Add("adminUsers_Auser", m.Auser);
                HttpContext.Current.Response.AppendCookie(Save_cookie);
            }
            #endregion

            return m;
        }
        #endregion

        #region
        /// <summary>
        /// 重载checkSession 验证 cookie中Token 并且更新新的Token 返回详细
        /// </summary>
        public static Model.adminUsers checkSession()
        {
            return checkSession(false);
        }
        #endregion
        #endregion
    }
}