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
    /// [Info] 查询详细
    /// </summary>
    public class Info
    {
        #region ==查询列表==
        /// <param name="model">Model.Info</param>
        public static DataSet Select_List(Model.Info model)
        {
            DataSet ds = Dal.Info.Select_List(model.s_Iid);
            return ds;
        }
        #endregion

        #region ==根据ID返回Iinfo==
        /// <param name="Iid">编号</param>
        public static string getInfo(int Iid)
        {
            Model.Info m = new Model.Info();
            m.s_Iid = Iid;
            m = Bll.Info.Select_Detail(m);
            return m.Iinfo;
        }
        #endregion

        #region ==查询详细==
        public static Model.Info Select_Detail(Model.Info model)
        {
            int i;
            string[,] arr = Common.Para.Info;
            if (arr.GetLength(1) == 0)
            {
                DataTable dt = Dal.Info.Select_Detail();
                arr = new string[dt.Rows.Count + 1, 3];
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[i + 1, 0] = dt.Rows[i]["Ititle"].ToString();
                    arr[i + 1, 1] = dt.Rows[i]["Iinfo"].ToString();
                    arr[i + 1, 2] = dt.Rows[i]["Type"].ToString();
                }
                dt.Dispose();

                Common.Para.Info = arr;
            }

            i = Common.Functions.ConvertInt16(model.s_Iid, -1);
            try
            {
                model.Ititle = arr[i, 0];
                model.Iinfo = arr[i, 1];
                model.Type = Common.Functions.ConvertInt16(arr[i, 2], 0);
            }
            catch
            {
                model.Ititle = "";
                model.Iinfo = "";
                model.Type = 0;
            }

            return model;
        }
        #endregion

        #region ==修改==
        public static void Update(Model.Info model)
        {
            Dal.Info.Update(model.s_Iid, model.Iinfo);
        }
        #endregion
    }
}
