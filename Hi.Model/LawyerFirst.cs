using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LawyerFirst
    {
        #region ==字段属性==
        /// <summary>
        /// 【读取】编号 bigint(8) PK，自加1
        /// </summary>
        public long Lid { get; set; }

        /// <summary>
        /// 【读取 修改】图片 varchar(100)
        /// </summary>
        public string Pic1 { get; set; }

        public string Lname { get; set; }
        public string Summary { get; set; }
        public string Linfo { get; set; }
        #endregion

        #region ==查询定位==
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
        /// 【查询】关键字。模糊查询 Lname 可为空
        /// </summary>
        public string s_Keywords { get; set; }

        /// <summary>
        /// 【查询】排序方式。如：Lid，可为空
        /// </summary>
        public string s_Order { get; set; }

        public string s_Lid { get; set; }

        public long d_Lid { get; set; }
        #endregion
    }
}
