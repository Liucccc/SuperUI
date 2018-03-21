using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    //Uploadfile
    public class Uploadfile
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
        /// 【查询】 根据Uid查询记录,多个Uid用逗号分隔，可为空。
        /// </summary>
        public string s_Uid { get; set; }

        /// <summary>
        /// 【查询】 Uid不包含
        /// </summary>	
        public string s_Uid_not { get; set; }

        /// <summary>
        /// 【查询】 排序方式。如：Uid desc，可为空
        /// </summary>
        public string s_Order { get; set; }
        #endregion

        #region==定位属性==
        /// <summary>
        /// 【定位】 根据Uid查询详细记录。
        /// </summary>
        public long d_Uid { get; set; }
        #endregion

        #region ==字段属性==
        /// <summary>
        /// 【读取】 文件编号 主键 自增1
        /// </summary>
        public long Uid { get; set; }

        /// <summary>
        /// 【读取 添加 修改】 文件路径
        /// </summary>	
        public string Url { get; set; }
        #endregion
    }
}