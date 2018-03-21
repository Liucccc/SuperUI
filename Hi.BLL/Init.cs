using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Bll
{
    /// <summary>
    /// [Init] 查询详细
    /// </summary>
    public class Init
    {
        #region ==根据ID返回Iinfo==
        /// <param name="Iid">编号</param>
        public static string getInfo(int Iid)
        {
            Model.Init m = new Model.Init();
            m.s_Iid = Iid;
            m = Bll.Init.Select_Detail(m);
            return m.Iinfo;
        }
        #endregion

        #region ==查询列表==
        /// <param name="model">Model.Init</param>
        public static DataSet Select_List(Model.Init model)
        {
            DataSet ds = Dal.Init.Select_List(model.s_not_Iid);
            return ds;
        }
        #endregion

        #region ==查询详细==
        public static Model.Init Select_Detail(Model.Init m)
        {
            int i;
            string[,] arr = Common.Para.Init;
            if (arr.GetLength(1) == 0)
            {
                DataTable dt = Dal.Init.Select_Detail();
                arr = new string[dt.Rows.Count + 1, 3];
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[i + 1, 0] = dt.Rows[i]["Ititle"].ToString();
                    arr[i + 1, 1] = dt.Rows[i]["Iinfo"].ToString();
                    arr[i + 1, 2] = dt.Rows[i]["Type"].ToString();
                    if (i + 1 == 14)
                        arr[i + 1, 1] = " " + arr[i + 1, 1];
                }
                dt.Dispose();

                Common.Para.Init = arr;
            }

            i = Common.Functions.ConvertInt16(m.s_Iid, -1);
            try
            {
                m.Ititle = arr[i, 0];
                m.Iinfo = arr[i, 1];
                m.Type = Common.Functions.ConvertInt16(arr[i, 2], 0);
            }
            catch
            {
                m.Ititle = "";
                m.Iinfo = "";
                m.Type = 0;
            }

            return m;
        }
        #endregion

        #region ==修改==
        public static void Update(Model.Init model)
        {
            Dal.Init.Update(model.s_Iid, model.Iinfo);
        }
        #endregion
    }
}
