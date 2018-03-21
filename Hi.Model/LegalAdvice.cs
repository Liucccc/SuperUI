using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LegalAdvice
    {
        #region ==字段属性==
        public long Lid { get; set; }
        public string Ltitle { get; set; }
        public string Linfo { get; set; }
        public string Lname { get; set; }
        public string Tel { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public DateTime Ldate { get; set; }
        public int Ostatus { get; set; }
        public string Reply { get; set; }
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
        /// 【查询】关键字。模糊查询 Ltitle/Lname/Tel/QQ/Email 可为空
        /// </summary>
        public string s_Keywords { get; set; }

        /// <summary>
        /// 【查询】排序方式。如：Lid，可为空
        /// </summary>
        public string s_Order { get; set; }

        public string s_Lid { get; set; }
        public string s_Ostatus { get; set; }

        public string s_d1 { get; set; }
        public string s_d2 { get; set; }

        public long d_Lid { get; set; }
        #endregion
    }
}
