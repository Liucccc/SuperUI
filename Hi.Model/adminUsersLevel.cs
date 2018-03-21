using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    public class adminUsersLevel
    {
        #region==查询属性==
        /// <summary>
        /// 【查询】所需总参数
        /// </summary>
        public string s_Total_parameter { get; set; }

        /// <summary>
        /// 【查询】所需主参数
        /// </summary>
        public string s_Main_parameter { get; set; }

        /// <summary>
        /// 【查询】所需扩展参数
        /// </summary>
        public string s_Extended_parameter { get; set; }

        /// <summary>
        /// 【查询】 关键字。级别名称 Ltitle 模糊查询  可为空
        /// </summary>  
        public string s_Keywords { get; set; }

        /// <summary>
        /// 【查询】 排序方式。如：Alive desc,Aulid，可为空
        /// </summary>
        public string s_Order { get; set; }

        /// <summary>
        /// 【查询】 管理员屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>	
        public int s_Alive { get; set; }

        /// <summary>
        /// 【查询】 根据编号查询记录。
        /// </summary>
        public string s_Aulid { get; set; }
        #endregion

        #region==定位属性==
        /// <summary>
        /// 【定位】 根据Aulid查询详细记录。
        /// </summary>
        public int d_Aulid { get; set; }

        /// <summary>
        /// 【定位】 Aulid不包含
        /// </summary>	
        public int d_Aulid_not { get; set; }

        /// <summary>
        /// 【定位】 管理员屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>
        public int d_Alive { get; set; }
        #endregion

        #region ==字段属性==
        /// <summary>
        /// 【读取 添加 修改】 编号，主码 smallint(2)
        /// </summary>
        public int Aulid { get; set; }

        /// <summary>
        /// 【读取 添加 修改】 级别名称 varchar(50)
        /// </summary>
        public string Ltitle { get; set; }

        /// <summary>
        /// 【读取】 有效或无效 
        /// </summary>
        public bool Alive { get; set; }

        /// <summary>
        /// 【读取 添加 修改】 管理范围 varchar(50)	逗号分隔，0或1
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 【扩展】包含管理员数量
        /// </summary>
        public long c { get; set; }
        #endregion
    }
}