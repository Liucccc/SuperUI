using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Article
    {
        #region ==字段属性==
        /// <summary>
        ///  【读取】编号 PK Identity(1,1) bigint(8)
        /// </summary>
        public long Aid {get; set;}

        /// <summary>
        /// 【添加 修改 读取】标题 varchar(1000)
        /// </summary>
        public string Atitle { get; set; }

        /// <summary>
        ///【添加 修改 读取】链接 varchar(1000)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 【添加 修改 读取】 来源 varchar(50)
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 【添加 修改 读取】 作者 varchar(50)
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 【读取】有效 bit(1)
        /// </summary>
        public bool Alive { get; set; }

        /// <summary>
        /// 【添加 读取】栏目 smallint(2)
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// 【读取】序号 bigint(8)
        /// </summary>
        public long Layer { get; set; }

        /// <summary>
        /// 【添加 修改 读取】内容 text(16)
        /// </summary>
        public string Ainfo { get; set; }

        /// <summary>
        /// 【添加 修改 读取】提交日期 datatime(8)
        /// </summary>
        public DateTime Atime { get; set; }

        /// <summary>
        /// 【添加 修改 读取】图片 varchar(100)
        /// </summary>
        public string Pic1 { get; set; }

        /// <summary>
        /// 【添加 修改 读取】缩略图 varchar(100)
        /// </summary>
        public string Pic2 { get; set; }

        /// <summary>
        /// 【添加 修改 读取】概述 varchar(8000)
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 【读取】热点文章 bit(1)
        /// </summary>
        public bool Hot { get; set; }

        /// <summary>
        /// 【读取】推荐文章 bit(1)
        /// </summary>
        public bool Recommend { get; set; }

        /// <summary>
        /// 【添加 修改 读取】 文章分类 bigint(8)
        /// </summary>
        public long Asid { get; set; }

        /// <summary>
        /// 【扩展】 文章分类名称
        /// </summary>
        public string Stitle { get; set; }

        /// <summary>
        ///【添加 修改 读取】副标题 varchar(100)
        /// </summary>
        public string ieTitle { get; set; }

        /// <summary>
        /// 【添加 修改 读取】关键字 varchar(8000)
        /// </summary>
        public string seoKeywords { get; set; }

        /// <summary>
        /// 【添加 修改 读取】描述 varchar(8000)
        /// </summary>
        public string seoDescription { get; set; }
        #endregion

        #region==查询定位属性==
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
        /// 【查询】关键字。模糊查询 Atitle/Source/Author 可为空
        /// </summary>
        public string s_Keywords { get; set; }

        /// <summary>
        /// 【查询】排序方式。如：Layer desc,Aid，可为空
        /// </summary>
        public string s_Order { get; set; }

        /// <summary>
        /// 【查询】查询栏目，多个用逗号分隔
        /// </summary>
        public string s_Kind { get; set; }

        /// <summary>
        /// 【查询】屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>
        public int s_Alive { get; set; }

        /// <summary>
        /// 【查询】提交日期 - 起始日期，可为空
        /// </summary>
        public string s_d1 { get; set; }

        /// <summary>
        /// 【查询】提交日期 - 结束日期，可为空
        /// </summary>
        public string s_d2 { get; set; }

        /// <summary>
        /// 【查询】根据编号查询。 多个Aid用逗号分隔，可为空
        /// </summary>
        public string s_Aid { get; set; }

        /// <summary>
        /// 【查询】热点文章状态。0-全部 1-热点 2-不是热点
        /// </summary>
        public int s_Hot { get; set; }

        /// <summary>
        /// 【查询】推荐文章状态。0-全部 1-推荐 2-不推荐
        /// </summary>
        public int s_Recommend { get; set; }

        /// <summary>
        /// 【查询】根据文章分类查询。 多个Asid用逗号分隔，可为空
        /// </summary>
        public string s_Asid { get; set; }

        /// <summary>
        /// 【定位】屏蔽状态。0-全部 1-有效 2-屏蔽
        /// </summary>
        public int d_Alive { get; set; }

        /// <summary>
        /// 【定位】根据Aid定位详细记录。
        /// </summary>
        public long d_Aid { get; set; }
        #endregion
    }
}
