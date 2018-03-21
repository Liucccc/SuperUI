using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Advertise
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
        /// 【查询】 主键，多个主键用逗号分割。可为空
        /// </summary>
        public string s_Aid { get; set; }
        #endregion

        #region ==定位属性==
        /// <summary>
        /// 【定位】根据Aid定位详细记录。
        /// </summary>	
        public int d_Aid { get; set; }
        #endregion

        #region ==字段属性==
        /// <summary>
        /// 【读取】编号 PK smallint(2)
        /// </summary>
        public int Aid { get; set; }

        /// <summary>
        /// 【读取】标题 varchar(50)
        /// </summary>
        public string Atitle { get; set; }

        /// <summary>
        /// 【读取 修改】链接 varchar(100)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 【读取 修改】图片 varcahr(100)
        /// </summary>
        public string Pic1 { get; set; }
        #endregion 
    }
}
