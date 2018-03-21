using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    public class adminUsers
    {
        #region ==查询属性==
        /// <summary>
        /// 【查询】所需总参数
        /// </summary>
        public string s_Total_parameter { get; set; }

        /// <summary>
        /// 【查询】主参数，多个参数逗号分隔，必须含有主键
        /// </summary>
        public string s_Main_parameter { get; set; }

        /// <summary>
        /// 【查询】扩展参数，多个参数逗号分隔，可为空
        /// </summary>
        public string s_Extended_parameter { get; set; }

        /// <summary>
        /// 【查询】管理员屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>	
        public int s_Alive { get; set; }

        /// <summary>
        /// 【查询】 根据编号查询记录,多个编号用逗号分隔，可为空
        /// </summary>
        public string s_Auid{ get; set; }

        /// <summary>
        /// 【查询】 关键字,模糊查询Auser,可为空
        /// </summary> 
        public string s_Keywords{ get; set; }

        /// <summary>
        /// 【查询】 级别权限,多个权限用逗号分隔，可为空
        /// </summary>
        public string s_Aulid{ get; set; }

        /// <summary>
        /// 【查询】排序方式。如：Alive desc,Auid，可为空
        /// </summary>
        public string s_Order{ get; set; }
        #endregion

        #region ==定位属性==
        /// <summary>
        /// 【定位】 根据编号Auid查询详细记录。
        /// </summary>
        public long d_Auid { get; set; }

        /// <summary>
        /// 【定位】 根据Auser查询详细记录。
        /// </summary>
        public string d_Auser { get; set; }

        /// <summary>
        /// 【定位】 根据密码Passwd(未加密)查询详细记录。
        /// </summary>
        public string d_Passwd { get; set; }

        /// <summary>
        /// 【定位】 管理员屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>	
        public int d_Alive { get; set; }

        /// <summary>
        /// 【定位】 级别权限Aulid不包含
        /// </summary>	
        public long d_Auid_not { get; set; }

        /// <summary>
        /// 【定位】登录令牌
        /// </summary>	
        public string d_Token { get; set; }

        /// <summary>
        /// 【定位】是否更新Token 1-更新 
        /// </summary>
        public int d_IsUpdateToken { get; set; }
        #endregion

        #region ==字段属性==
        /// <summary>
        /// 【读取】编号 PK，自加1 bigint(8)
        /// </summary>
        public long Auid{ get; set; }

        /// <summary>
        /// 【读取 添加 修改】 姓名 varchar(50)
        /// </summary>
        public string Auser { get; set; }

        /// <summary>
        /// 【读取 添加 修改】 密码(不需加密，添加修改的时候传原始数据) char(32)
        /// </summary>
        public string Passwd { get; set; }
       
        /// <summary>
        /// 【读取】 有效 bit(1)
        /// </summary>
        public bool Alive{ get; set; }

        /// <summary>
        /// 【读取 添加 修改】 级别权限 smallint(2)
        /// </summary>
        public int Aulid{ get; set; }

        /// <summary>
        /// 【扩展】 级别名称
        /// </summary>
        public string Ltitle { get; set; }

        /// <summary>
        /// 【扩展】 管理范围
        /// </summary>
        public string[] Areas { get; set; }

        /// <summary>
        /// 【读取】 登录令牌IP	varchar(50)
        /// </summary>
        public string Token_IP { get; set; }

        /// <summary>
        /// 【读取】 登录令牌 Varchar(500)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 【读取】 登录令牌有效期	dt(8)
        /// </summary>
        public DateTime Token_expiry { get; set; }
        #endregion
    }
}