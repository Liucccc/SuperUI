using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model
{
	public class Init
    {
        #region==查询属性==
        /// <summary>
        /// 搜索Iid。0-全部
        /// </summary>	
        public int s_Iid { get; set; }

        /// <summary>
        /// 搜索Iid不包含
        /// </summary>	
        public string s_not_Iid { get; set; }
        #endregion

        #region ==字段属性==
        /// <summary>
        /// int
        /// </summary>	
        public int Iid { get; set; }

        /// <summary>
        /// string
        /// </summary>	
        public string Iinfo { get; set; }

        /// <summary>
        /// string
        /// </summary>	
        public string Ititle { get; set; }

        /// <summary>
        /// int
        /// </summary>	
        public int Type { get; set; }
        #endregion
    }
}