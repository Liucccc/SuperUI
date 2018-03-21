using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Bll
{
    /// <summary>
    /// 固定广告位表
    /// 2016-09-22
    /// </summary>
    public class Advertise
    {
        #region ==查询列表（带分页）==
        /// <summary>
        /// 查询列表（带分页）。返回List
        /// </summary>
        /// <param name="m">s_系列</param>
        /// <param name="p">Model.Pages</param>
        public static List<Model.Advertise> Select_List(Model.Advertise m)
        {
            DataTable dt = Dal.Advertise.Select_List(m.s_Aid);
            DataRow dr;
            List<Model.Advertise> l = new List<Model.Advertise>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                m = new Model.Advertise();
                m.Aid = Common.Functions.ConvertInt16(dr["Aid"], 0);
                m.Atitle = dr["Atitle"].ToString();
                m.Url = dr["Url"].ToString();
                m.Pic1 = dr["Pic1"].ToString();
                l.Add(m);
            }
            dt.Dispose();

            return l;
        }
        #endregion

        #region ==查询详细==
        /// <summary>
        /// 查询详细。
        /// </summary>
        /// <param name="m">d_Aid</param>
        public static Model.Advertise Select_Detail(Model.Advertise m)
        {
            DataTable dt = Dal.Advertise.Select_Detail(m.d_Aid);
            m = new Model.Advertise();
            try
            {
                DataRow dr = dt.Rows[0];
                m.Aid = Common.Functions.ConvertInt16(dr["Aid"], 0);
                m.Atitle = dr["Atitle"].ToString();
                m.Url = dr["Url"].ToString();
                m.Pic1 = dr["Pic1"].ToString();
            }
            catch
            {
                m.Aid = 0;
                m.Atitle = "";
                m.Url = "";
                m.Pic1 = "";
            }
            dt.Dispose();

            return m;
        }
        #endregion

        #region ==修改基本信息==
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="m">d_Aid+修改项</param>
        public static void Modify(Model.Advertise m)
        {
            Dal.Advertise.Modify(m.d_Aid, m.Url, m.Pic1);
        }
        #endregion 
    }
}
