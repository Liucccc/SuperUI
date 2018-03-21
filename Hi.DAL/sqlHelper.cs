using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Reflection;

namespace Dal
{
    public class sqlH
    {

        #region 方法

        public static void ExecuteNonQuery(string sql)
        {
            Common.Config cfg = new Common.Config();

            cfg.connDb();
            SqlCommand sc = new SqlCommand(sql, cfg.Conn);

            try
            {
                sc.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write(e.ToString() + "<br />" + sql);
            }
            finally
            {
                sc.Dispose();
                cfg.closeDb();
            }

        }

        public static SqlDataAdapter getDa(string sql)
        {
            Common.Config cfg = new Common.Config();
            

            cfg.connDb();

            SqlDataAdapter da = new SqlDataAdapter(sql, cfg.Conn);

            cfg.closeDb();

            return da;
        }

        public static DataSet getDs(string sql)
        {
            Common.Config cfg = new Common.Config();


            cfg.connDb();

            SqlDataAdapter da = new SqlDataAdapter(sql, cfg.Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();

            cfg.closeDb();

            return ds;
        }

        public static DataTable getDt(string sql)
        {
            Common.Config cfg = new Common.Config();


            cfg.connDb();

            SqlDataAdapter da = new SqlDataAdapter(sql, cfg.Conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();

            cfg.closeDb();

            return dt;
        }

        /// <summary>
        /// 将一条查询记录创建为一个实体类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T ExecuteDataReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            try
            {
                obj = Activator.CreateInstance<T>();
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                int fieldCount = dr.FieldCount;
                foreach (PropertyInfo propertyInfo in properties)
                {
                    string propertyName = propertyInfo.Name;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        string fieldName = dr.GetName(i);
                        if (string.Compare(propertyName, fieldName, true) == 0)
                        {
                            object value = dr.GetValue(i);
                            if (value != DBNull.Value)
                            {
                                propertyInfo.SetValue(obj, value, null);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("反射创建实体类方法出错！");
            }
            return obj;
        }
        /// <summary>
        /// 返回符合条件查询结果的泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<T> ExecuteList<T>(string cmdText, CommandType commandType, params SqlParameter[] array)
        {
            List<T> lists = new List<T>();
            try
            {
                Common.Config cfg = new Common.Config();
                cfg.connDb();
                using (cfg.Conn)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, cfg.Conn))
                    {
                        cmd.CommandType = commandType;
                        if (array != null)
                        {
                            cmd.Parameters.AddRange(array);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    T obj = ExecuteDataReader<T>(dr);
                                    if (obj != null)
                                    {
                                        lists.Add(obj);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("数据库操作异常！");
            }

            return lists;
        }
        /// <summary>
        /// 返回符合条件查询结果的实体类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T ExecuteEntity<T>(string cmdText, CommandType commandType, params SqlParameter[] array)
        {
            T obj = default(T);
            try
            {
                Common.Config cfg = new Common.Config();
                cfg.connDb();
                using (cfg.Conn)
                {
                    using (SqlCommand cmd = new SqlCommand(cmdText, cfg.Conn))
                    {
                        cmd.CommandType = commandType;
                        if (array != null)
                        {
                            cmd.Parameters.AddRange(array);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (dr != null)
                            {
                                while (dr.Read())
                                {
                                    obj = ExecuteDataReader<T>(dr);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
               
                throw new Exception("数据库操作异常！");
            }
            return obj;
        }
        /// <summary>
        /// 执行不查询操作数据库命令
        /// 支持事务操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, CommandType commandType, SqlTransaction transaction, params SqlParameter[] array)
        {
            int rowCount = -1;
            try
            {
                Common.Config cfg = new Common.Config();
                cfg.connDb();
                using (SqlCommand cmd = new SqlCommand(cmdText, cfg.Conn))
                {
                    cmd.CommandType = commandType;
                    if (array != null)
                    {
                        cmd.Parameters.AddRange(array);
                    }
                    cmd.Transaction = transaction;
                    rowCount = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                 throw new Exception("数据库操作异常！");
            }
            return rowCount;
        }
        /// <summary>
        /// 执行查询一行一列操作数据库命令
        /// 支持事务操作
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, CommandType commandType, SqlTransaction transaction, params SqlParameter[] array)
        {
            object rowCount = -1;
            try
            {
                Common.Config cfg = new Common.Config();
                cfg.connDb();
                using (SqlCommand cmd = new SqlCommand(cmdText, cfg.Conn))
                {
                    cmd.CommandType = commandType;
                    if (array != null)
                    {
                        cmd.Parameters.AddRange(array);
                    }
                    cmd.Transaction = transaction;
                    rowCount = cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw new Exception("数据库操作异常！");
            }
            return rowCount;
        }

        #endregion

    }
}
