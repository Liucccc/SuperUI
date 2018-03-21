using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
//引用命名空间【泛型】
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    #region 摘要
    /// <summary>
    /// Config 的摘要说明。
    /// </summary>
    #endregion
    public class Para
    {
        public static string siteHead = "";
        //public static string siteHead = "zzlh/";                   //网站头路径
        public static string adminUrl = "6h5y4m7d8u8r9b1a/";        //后台路径                                        
        public static string dt_def = "1900-1-1";                   //默认日期
        public static string start_date = "2013-1-1";
        public static string cookie_admins = Common.Para.siteHead + "zzlh_Admins";                  //后台管理员登录cookie名
        public static string cookie_Member = Common.Para.siteHead + "zzlh_Member";                  //前台cookie名
        public static string[] Email = { "jmail@topu.net", "smtp.ym.163.com", "jmail@topu.net", "topu.net" };
        public static string[,] Init = new string[0, 0];
        public static string[,] Info = new string[0, 0];
        public static string small_window = "true";    //后台弹出层显示小窗口
        public static bool pics_view = false;       //是否有图片库浏览功能，如果有则不连带删除记录中的图片，反之则连带删除记录中的图片
        public static string[] area_name = new string[] { "首页管理", "律师简介", "业务范围", "法律咨询", "最新资讯", "婚姻法规", "典型案例","离婚知识","继承知识","律师人生", "其他", "系统设置" };  //管理范围名（数组）

        //Token 有效期（分钟）
        public static long EffectiveTime = 1440;//1天
    }
}
