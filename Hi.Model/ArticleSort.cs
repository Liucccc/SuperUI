using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ArticleSort
    {
        #region ==字段属性==
        /// <summary>
        /// 【读取】编号 PK，自加1 bigint(8)
        /// </summary>
        public long Asid { get; set; }

        /// <summary>
        /// 【读取 添加 修改】 姓名 varchar(50)
        /// </summary>
        public string Stitle { get; set; }

        /// <summary>
        /// 【读取】序号 bigint(8)
        /// </summary>
        public long Layer { get; set; }

        /// <summary>
        /// 【读取】 有效 bit(1)
        /// </summary>
        public bool Alive { get; set; }

        /// <summary>
        /// 【读取 添加】栏目 samllint(2)
        /// </summary>
        public int Kind { get; set; }
        #endregion


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
        /// 【查询】状态。0-全部 1-有效 2-屏蔽
        /// </summary>	
        public int s_Alive { get; set; }

        /// <summary>
        /// 【查询】 根据编号查询记录,多个编号用逗号分隔，可为空
        /// </summary>
        public string s_Asid { get; set; }

        /// <summary>
        /// 【查询】 根据栏目查询记录,多个编号用逗号分隔，可为空
        /// </summary>
        public string s_Kind { get; set; }

        /// <summary>
        /// 【查询】 关键字,模糊查询Stitle,可为空
        /// </summary> 
        public string s_Keywords { get; set; }

        /// <summary>
        /// 【查询】排序方式。如：Alive desc,Asid，可为空
        /// </summary>
        public string s_Order { get; set; }

        /// <summary>
        /// 【定位】 根据编号Asid查询详细记录。
        /// </summary>
        public long d_Asid { get; set; }

        /// <summary>
        /// 【定位】 管理员屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>	
        public int d_Alive { get; set; }
        #endregion
    }
}