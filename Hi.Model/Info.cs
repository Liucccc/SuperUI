using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace Model
{
    public class Info
    {
        #region==查询属性==
        /// <summary>
        /// 搜索Iid。0-全部
        /// </summary>	
        public int s_Iid;
        #endregion

        #region ==字段属性==
        /// <summary>
        /// 【定位 读取】 PK  smallint(2)
        /// </summary>	
        public int Iid;

        /// <summary>
        /// 【修改 读取】varchar(50)
        /// </summary>	
        public string Ititle;

        /// <summary>
        /// 【修改 读取】text(16)
        /// </summary>	
        public string Iinfo;

        /// <summary>
        /// 【读取】smallint(2)
        /// </summary>	
        public int Type;
        #endregion
    }
}