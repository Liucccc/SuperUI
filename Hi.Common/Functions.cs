using System;
using System.Data;
//using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
//using System.Web.Util;
//using jmail;
using System.Web.Mail;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Drawing;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Common
{
    public class Functions
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        private Common.Config cfg = new Config();
        
        #region ==���������==
        /// <summary>
        /// ��������롣�����ַ���
        /// </summary>
        /// <param name="n">λ��������+��ĸ�����ʱ�����ż��</param>
        /// <param name="Kind">�������ࡣ1-������ 2-����д��ĸ 3-��Сд��ĸ 4-����+��д��ĸ 5-����+Сд��ĸ 6-��д��ĸ��Сд��ĸ 7-����+����д��ĸ��Сд��ĸ</param>
        /// <returns></returns>
        public static string createRandomStr(int n, int Kind)
        {
            Random r = new Random();
            string code = "";
            int a = 0;
            switch (Kind)
            {
                case 1:
                    for (int i = 0; i < n; i++)
                        code += r.Next(1, 10).ToString();
                    break;
                case 2:
                    for (int i = 0; i < n; i++)
                    {
                        code += ((char)r.Next(65, 91)).ToString();
                    }
                    break;
                case 3:
                    for (int i = 0; i < n; i++)
                    {
                        code += ((char)r.Next(97, 123)).ToString();
                    }
                    break;
                case 4:
                    for (int i = 0; i < n / 2; i++)
                    {
                        code += r.Next(1, 10).ToString() + ((char)r.Next(65, 91)).ToString();
                    }
                    break;
                case 5:
                    for (int i = 0; i < n / 2; i++)
                    {
                        code += r.Next(1, 10).ToString() + ((char)r.Next(97, 123)).ToString();
                    }
                    break;
                case 6:
                    for (int i = 0; i < n; i++)
                    {
                        a = r.Next(65, 123);
                        if (a > 90 && a < 97)
                        {
                            i--;
                            continue;
                        }
                        code += ((char)a).ToString();
                    }
                    break;
                case 7:
                    for (int i = 0; i < n / 2; i++)
                    {
                        a = r.Next(65, 123);
                        if (a > 90 && a < 97)
                        {
                            i--;
                            continue;
                        }
                        code += r.Next(1, 10).ToString() + ((char)a).ToString();
                    }
                    break;
            }
            return code;
        }
        #endregion

        #region ==����json��ʽ�ַ���==
        /// <summary>
        /// �������б� ����json��ʽ�ַ���
        /// </summary>
        /// <param name="cfg">���ݿ����Ӷ���</param>
        /// <param name="sql">sql���</param>
        public static string BindStrByIdAndSql(Config cfg, string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, cfg.Conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            sda.Dispose();
            ds.Dispose();
            if (dt == null)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder("[");

            foreach (DataRow row in dt.Rows)
            {
                //string id=dt.Columns[0].ColumnName;
                //string value = dt.Columns[1].ColumnName;
                sb.Append("{id:'" + row[0] + "',name:'" + row[1] + "'},");
            }
            dt.Dispose();
            ds.Dispose();
            sb.Append("]");
            sb.Replace(",]", "]");
            return sb.ToString();
        }
        #endregion

        #region ==alert some words==
        /// <summary>
        /// alert �� window.location
        /// </summary>
        /// <param name="str">alert����</param>
        /// <param name="url">ת���ַ�����ַ���Ϊ��ת��</param>
        /// <param name="writeEnd">�Ƿ����End()</param>
        public static void Alert(string str, string url, bool writeEnd)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">alert(\"" + str + "\");");
            if (url != "")
                HttpContext.Current.Response.Write("window.location=\"" + url + "\";");
            HttpContext.Current.Response.Write("</script>");
            if (writeEnd)
                HttpContext.Current.Response.End();
        }
        public static void PromptAlert(System.Web.UI.Page page, string str)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ymPrompt.alert('" + str.ToString() + "');</script>");
        }
        #endregion

        #region ==�ļ����� bool DownloadFile(HttpResponse response, string serverPath, string encode)==
        /// <summary>
        /// �ļ�����
        /// </summary>
        /// <param name="response">HttpResponse response</param>
        /// <param name="serverPath">�ļ���ַ����Ӳ��·��������Server.MapPath��</param>
        /// <param name="encode">�ļ�����</param>
        public static bool DownloadFile(HttpResponse response, string serverPath, string encode)
        {

            FileStream fs = null;
            try
            {
                HttpContext.Current.Response.Clear();
                fs = File.OpenRead(HttpContext.Current.Server.MapPath(serverPath));
                byte[] buffer = new byte[1024];
                long count = 1024;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
                HttpContext.Current.Response.AddHeader("Accept-Charset", encode);
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(HttpContext.Current.Server.MapPath(serverPath)));//����ʱҪ�����Ĭ���ļ���
                HttpContext.Current.Response.AddHeader("Content-Length", fs.Length.ToString());
                while (count == 1024)
                {
                    count = fs.Read(buffer, 0, 1024);
                    HttpContext.Current.Response.BinaryWrite(buffer);
                }
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write(e.ToString());
                return false;
            }
        }
        #endregion

        #region ==����ļ����� string getCode(string path)==
        /// <summary>
        /// ����ļ�����
        /// </summary>
        /// <param name="path">�ļ���ַ����Ӳ��·��������Server.MapPath��</param>
        /// <returns>Server.HtmlEncode(str)</returns>
        public static string getCode(string path)
        {
            string str = "";
            if (File.Exists(HttpContext.Current.Server.MapPath(path)))
            {
                str = File.ReadAllText(HttpContext.Current.Server.MapPath(path), Encoding.Unicode);
            }
            return HttpContext.Current.Server.HtmlEncode(str);
        }
        #endregion

        #region ==ɾ����¼ bool recordDel(string t,string pk_n,string pk_v,int pic_c,string info_n,string column_n,string sortTitle,SqlConnection conn,bool del)==
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="t">����</param>
        /// <param name="pk_n">�����ֶ���</param>
        /// <param name="pk_v">����ֵ</param>
        /// <param name="pic_c">ͼƬ������û��д0</param>
        /// <param name="info_n">ʹ�ñ༭�����ֶ���������ֶ��ö��ŷָ�</param>
        /// <param name="column_n">�������ֶ�������Ҫ��Ϊ��д�������¼���粻д��û�ã���������Ҫдһ���ֶ���</param>
        /// <param name="sortTitle">�˷���ķ������</param>
        /// <param name="conn">conn</param>
        /// <param name="del">true-ɾ����¼��false-��ɾ����¼����isDel��ΪTrue</param>
        /// <returns>true</returns>
        public static bool recordDel(string t, string pk_n, string pk_v, int pic_c, string info_n, string column_n, string sortTitle, SqlConnection conn, bool del)
        {
            string sql;
            int i;
            SqlCommand sc;
            SqlDataReader dr = null;
            sql = "select " + column_n;
            if (info_n != "")
            {
                sql += "," + info_n;
            }
            for (i = 1; i <= pic_c; i++)
            {
                sql += ",Pic" + i;
            }
            sql += " from " + t + " where " + pk_n + " in(" + pk_v + ")";
            sc = new SqlCommand(sql, conn);
            try
            {
                dr = sc.ExecuteReader();
            }
            catch
            {
                HttpContext.Current.Response.Write(sql.ToString());
                HttpContext.Current.Response.End();
            }

            while (dr.Read())
            {
                if (!Para.pics_view)
                {
                    for (i = 1; i <= pic_c; i++)
                    {
                        del_file("/" + Common.Para.siteHead + dr["Pic" + i].ToString().Trim());
                    }
                }
                if (info_n != "")
                {
                    string[] a = Regex.Split(info_n, ",");
                    for (i = 0; i < a.Length; i++)
                        del_url(dr[a[i]].ToString());
                }
            }
            dr.Close();
            dr.Dispose();
            sc.Dispose();
            if (del)
                sql = "delete from " + t + " where " + pk_n + " in(" + pk_v + ")";
            else
                sql = "update " + t + " set isDel=1 where " + pk_n + " in(" + pk_v + ")";
            sc = new SqlCommand(sql, conn);
            sc.ExecuteNonQuery();
            sc.Dispose();
            sc = null;

            return true;
        }
        #endregion

        #region ==���ݲ��� string transParameters(string Para) Para�������ݵĲ������� || �ָ�==
        /// <summary>
        /// �Զ����ݵ�ַ����������ַ����.aspx?id=1��n=abc��page=5��s=def -- func.transParameters("n||s")�ķ���ֵ��id=1��page=5
        /// </summary>
        /// <param name="Para">�����ݵĲ�����</param>
        public static string transParameters(string Para)
        {
            string[] addrPara;	//��ַ��ȫ������
            string exPara;	//���ݹ����Ĳ���
            string str;
            int i;

            #region  ==��õ�ַ��ȫ������==
            addrPara = HttpContext.Current.Request.QueryString.AllKeys;
            string[] v = new string[addrPara.Length];
            for (i = 0; i < addrPara.Length; i++)
            {
                v[i] = convers(HttpContext.Current.Request.QueryString[i]);
            }
            #endregion

            #region ==��ô��ݹ����Ĳ���==
            exPara = "<" + Para.Replace("||", "><") + ">";
            #endregion

            #region ==������յ�ַ������==
            str = "";
            for (i = 0; i < addrPara.Length; i++)
            {
                if (addrPara[i] == null || addrPara[i].ToString().ToLower() == "__viewstate")
                {
                    continue;
                }
                if (exPara.IndexOf("<" + addrPara[i].ToString() + ">") == -1)
                {
                    if (str != "")
                    {
                        str += "&";
                    }
                    str += addrPara[i].ToString() + "=" + v[i];
                }
            }
            #endregion

            return str;
        }
        #endregion

        #region ==ʡ���ַ��� string shenglue(string text,int length,bool more)==
        /// <summary>
        /// ʡ���ַ���
        /// </summary>
        /// <param name="text">ԭʼ�ַ���</param>
        /// <param name="length">�ֽڳ��ȣ�һ������=2���ֽڣ�</param>
        /// <param name="more">�Ƿ���ʾʡ�Ժ�...</param>
        public static string shenglue(string text, int length, bool more)
        {
            int num;
            int i;
            string str = "";
            byte[] chars = Encoding.ASCII.GetBytes(text);
            num = 0;
            for (i = 0; i < chars.Length; i++)
            {
                if ((int)chars[i] == 63)
                {
                    num += 2;
                }
                else
                {
                    num += 1;
                }
                if (num == length && i < chars.Length - 1)
                {
                    str = text.Substring(0, i + 1);
                    if (more)
                    {
                        str += "...";
                    }
                    break;
                }
                else if (num > length)
                {
                    str = text.Substring(0, i);
                    if (more)
                    {
                        str += "...";
                    }
                    break;
                }
            }
            if (str == "")
            {
                str = text;
            }
            return str;
        }
        #endregion

        #region ==�ж��ַ������� int strLength(string text)==
        /// <summary>
        /// �ж��ַ������ȣ�ռ�õ��ֽ�����һ������ռ�������ֽڣ�
        /// </summary>
        /// <param name="text">�ַ���</param>
        public static int strLength(string text)
        {
            int num = 0;
            int i;
            byte[] chars = Encoding.ASCII.GetBytes(text);
            for (i = 0; i < chars.Length; i++)
            {
                if ((int)chars[i] == 63)
                {
                    num += 2;
                }
                else
                {
                    num += 1;
                }
            }
            return num;
        }
        #endregion

        #region ==���Email�Ϸ��� bool checkEmail(string email)==
        /// <summary>
        /// ������֤Email�ĺϷ��ԡ�trueΪ�Ϸ�
        /// </summary>
        public static bool checkEmail(string email)
        {

            Regex r = new Regex(@"^([\w^_])+([-]?[\w]+)*[\@]([\w^_])+([-][\w]+)*([.][\w]+)+$");
            Match m = r.Match(email);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ==���˷Ƿ��ַ� string unchar(string text)==
        /// <summary>
        /// ������֤�Ƿ��ַ���trueΪ�Ϸ�
        /// </summary>
        public static bool unchar(string text)
        {
            Regex r = new Regex(@"^([\w^_]+(\w)*[-\w]?)+$");
            Match m = r.Match(text);
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ==�жϱ����Ƿ�Ϊ��ֵ��(��������Ҫ��) bool isNum(string str)==
        /// <summary>
        /// ��֤�Ƿ�Ϊ��ֵ�ͱ�����trueΪ��
        /// </summary>
        public static bool isNum(string str)
        {
            try
            {
                int a = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ==һά������������ string[] arrayRank(string[] arr,int l,int u)==
        /// <summary>
        /// һά�����������������㷨��
        /// </summary>
        /// <param name="arr">ԭʼ����</param>
        /// <param name="l">��С����</param>
        /// <param name="u">������</param>
        public static string[] arrayRank(string[] arr, int l, int u)
        {
            int i = l;
            int j = u;
            int k;
            string t;
            int limit = 12;
            string middle;		//�����м�Ǳ��ֵ
            bool isNumFlag = true;     //����ֵ�Ƿ�Ϊ��ֵ��

            #region ==�ж�����ֵ�Ƿ�Ϊ������==
            for (k = l; k <= u; k++)
            {
                if (!isNum(arr[k]))
                {
                    isNumFlag = false;
                    break;
                }
            }
            #endregion

            if (u - l + 1 <= limit)
            {
                #region ==�Ծ����ٵ��������ð������==
                for (j = l; j <= u; j++)
                {
                    for (i = l + 1; i <= u; i++)
                    {
                        if (isNumFlag)
                        {
                            if (Convert.ToInt64(arr[i]) < Convert.ToInt64(arr[i - 1]))
                            {
                                t = arr[i];
                                arr[i] = arr[i - 1];
                                arr[i - 1] = t;
                            }
                        }
                        else
                        {
                            if (arr[i].CompareTo(arr[i - 1]) < 0)
                            {
                                t = arr[i];
                                arr[i] = arr[i - 1];
                                arr[i - 1] = t;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ==��������в������==
                middle = arr[(l + u) / 2];	//�м���ֵ
                while (true)		//����ѭ��
                {
                    //==��������С�Ǳ꿪ʼѰ�ұ��м���ֵ��ĵ�==
                    if (isNumFlag)
                    {
                        while (i < u && Convert.ToInt64(arr[i]) < Convert.ToInt64(middle))
                        {
                            i += 1;
                        }
                    }
                    else
                    {
                        while (i < u && arr[i].CompareTo(middle) < 0)
                        {
                            i += 1;
                        }
                    }

                    //==���������Ǳ꿪ʼѰ�ұ��м���ֵС�ĵ�==
                    if (isNumFlag)
                    {
                        while (j > l && Convert.ToInt64(arr[j]) > Convert.ToInt64(middle))
                        {
                            j -= 1;
                        }
                    }
                    else
                    {
                        while (j > l && arr[j].CompareTo(middle) > 0)
                        {
                            j -= 1;
                        }
                    }

                    //==����������м���ֵ��ĵ㲻���ڱ������м���ֵС�ĵ㣬�����������ֵ��������һ�������Ѱ��==						
                    if (i <= j)
                    {
                        t = arr[i];
                        arr[i] = arr[j];
                        arr[j] = t;
                        i += 1;
                        j -= 1;
                    }

                    //==�����С�Ǳ꿪ʼѰ�ҵĵ���ڴӴ�Ǳ꿪ʼѰ�ҵĵ㣬�˳�ѭ��==
                    if (i > j)
                    {
                        break;
                    }
                }

                //==�ٴ�����==
                if (i < u)
                {
                    arrayRank(arr, i, u);
                }
                if (j > l)
                {
                    arrayRank(arr, l, j);
                }
                #endregion
            }

            return arr;
        }
        #endregion

        #region ==��ά������������ string[,] arrayRank2(string[,] arr, int c, int l,int u)==
        /// <summary>
        /// ��ά������������
        /// </summary>
        /// <param name="arr">ԭʼ����</param>
        /// <param name="c">���������� ���</param>
        /// <param name="l">��С����</param>
        /// <param name="u">������</param>
        public static string[,] arrayRank2(string[,] arr, int c, int l, int u)
        {
            int i = l;
            int j = u;
            int k;
            string t;
            int limit = 12;
            string middle;		//�����м�Ǳ��ֵ
            bool isNumFlag = true;     //����ֵ�Ƿ�Ϊ��ֵ��

            #region ==�ж�����ֵ�Ƿ�Ϊ������==
            for (k = l; k <= u; k++)
            {
                if (!isNum(arr[k, c]))
                {
                    isNumFlag = false;
                    break;
                }
            }
            #endregion

            if (u - l + 1 <= limit)
            {
                #region ==�Ծ����ٵ��������ð������==
                for (j = l; j <= u; j++)
                {
                    for (i = l + 1; i <= u; i++)
                    {
                        if (isNumFlag)
                        {
                            if (Convert.ToInt64(arr[i, c]) < Convert.ToInt64(arr[i - 1, c]))
                            {
                                t = arr[i, c];
                                arr[i, c] = arr[i - 1, c];
                                arr[i - 1, c] = t;
                            }
                        }
                        else
                        {
                            if (arr[i, c].CompareTo(arr[i - 1, c]) < 0)
                            {
                                t = arr[i, c];
                                arr[i, c] = arr[i - 1, c];
                                arr[i - 1, c] = t;
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region ==��������в������==
                middle = arr[c, (l + u) / 2];	//�м���ֵ
                while (true)		//����ѭ��
                {
                    //==��������С�Ǳ꿪ʼѰ�ұ��м���ֵ��ĵ�==
                    if (isNumFlag)
                    {
                        while (i < u && Convert.ToInt64(arr[i, c]) < Convert.ToInt64(middle))
                        {
                            i += 1;
                        }
                    }
                    else
                    {
                        while (i < u && arr[i, c].CompareTo(middle) < 0)
                        {
                            i += 1;
                        }
                    }

                    //==���������Ǳ꿪ʼѰ�ұ��м���ֵС�ĵ�==
                    if (isNumFlag)
                    {
                        while (j > l && Convert.ToInt64(arr[j, c]) > Convert.ToInt64(middle))
                        {
                            j -= 1;
                        }
                    }
                    else
                    {
                        while (j > l && arr[j, c].CompareTo(middle) > 0)
                        {
                            j -= 1;
                        }
                    }

                    //==����������м���ֵ��ĵ㲻���ڱ������м���ֵС�ĵ㣬�����������ֵ��������һ�������Ѱ��==						
                    if (i <= j)
                    {
                        t = arr[i, c];
                        arr[i, c] = arr[j, c];
                        arr[j, c] = t;
                        i += 1;
                        j -= 1;
                    }

                    //==�����С�Ǳ꿪ʼѰ�ҵĵ���ڴӴ�Ǳ꿪ʼѰ�ҵĵ㣬�˳�ѭ��==
                    if (i > j)
                    {
                        break;
                    }
                }

                //==�ٴ�����==
                if (i < u)
                {
                    arrayRank2(arr, c, i, u);
                }
                if (j > l)
                {
                    arrayRank2(arr, c, l, j);
                }
                #endregion
            }

            return arr;
        }
        #endregion

        #region==һά���鷴������ string[] arrayRev(string[] arr,int l,int u)==
        /// <summary>
        /// һά���鷴������
        /// </summary>
        /// <param name="arr">ԭ����</param>
        /// <param name="l">��С����</param>
        /// <param name="u">������</param>
        public static string[] arrayRev(string[] arr, int l, int u)
        {
            string[] t = new string[u + 1];
            int i;
            for (i = l; i <= u; i++)
            {
                t[u - i + l] = arr[i];
            }
            return t;
        }
        #endregion

        #region==��ά���鷴������ string[,] arrayRev2(string[,] arr,int l,int u)==
        /// <summary>
        /// ��ά���鷴������
        /// </summary>
        /// <param name="arr">ԭ����</param>
        /// <param name="l">��С����</param>
        /// <param name="u">������</param>
        public static string[,] arrayRev2(string[,] arr, int l, int u)
        {
            string[,] t = new string[u + 1, arr.GetLength(1)];
            int i, j;
            for (i = l; i <= u; i++)
            {
                for (j = 0; j < t.GetLength(1); j++)
                {
                    t[u - i + l, j] = arr[i, j];
                }
            }
            return t;
        }
        #endregion

        #region ==ת������ĵ����źͼ�����  string convers(string text)==
        /// <summary>
        /// ת������ĵ����źͼ����ţ����ձ�����ַ��ʱʹ��
        /// </summary>
        public static string convers(string text)
        {
            if (text == null)
                return "";
            else
            {
                //ȫ���滻�ַ� ��ֹsqlע��
                return text.Replace("'", "&acute;").Replace("<", "&lt;").Replace("\"", "&quot;");

            }
        }
        #endregion

        #region==�ÿո����ַ��� string split_space(string text)==
        /// <summary>
        /// �ÿո����ַ�����split_space("abc")��ֵΪ��a b c
        /// </summary>
        public static string split_space(string text)
        {
            int i;
            string str = "";
            byte[] b = Encoding.ASCII.GetBytes(text);
            char[] c = text.ToCharArray();
            for (i = 0; i < b.Length; i++)
            {
                //HttpContext.Current.Response.Write(b[i].ToString() + "<br />");
                if (i > 0)
                {
                    if ((Convert.ToInt32(b[i]) == 63 && Convert.ToInt32(b[i - 1]) != 32) || Convert.ToInt32(b[i - 1]) == 63)
                        str += "&nbsp;";
                    else if (Convert.ToInt32(b[i]) == 32)
                        str += "&nbsp;";
                }
                if (Convert.ToInt32(b[i]) != 32)
                    str += c[i].ToString();
            }
            return str;
        }
        #endregion

        #region==��֤ҳ��Ϸ��� int pageValid(int Pc,int Page)==
        /// <summary>
        /// ��֤ҳ��Ϸ��ԣ������漰����ǰҳ�ķ����������֤�����õ���ʹ�á�
        /// </summary>
        /// <param name="Pc">��ҳ��</param>
        /// <param name="Page">��ǰҳ</param>
        public static int pageValid(int Pc, int Page)
        {
            if (Page > Pc)
            {
                Page = Pc;
            }
            if (Page < 1)
            {
                Page = 1;
            }
            return Page;
        }
        #endregion

        #region==��̨��ҳ��ʾ string Pagination(intPc,int Page,string Tp,string Pname,string Previous,string Next,string pageName, string method)==
        /// <summary>
        /// ��̨��ҳ��ʾ
        /// </summary>
        /// <param name="Pc">��ҳ��</param>
        /// <param name="Page">��ǰҳ��</param>
        /// <param name="Tp">���Զ����ݵĵ�ַ�����������������||�ָ�</param>
        /// <param name="Pname">ҳ�����name</param>
        /// <param name="Previous">��һҳ</param>
        /// <param name="Next">��һҳ</param>
        /// <param name="pageName">ҳ</param>
        /// <param name="inputHeight">�ı���ĸ�</param>
        /// <param name="sk">������ʽ</param>
        /// <param name="method">���ύ��ʽpost/get</param>
        /// <returns></returns>
        public static string Pagination(int Pc, int Page, string Tp, string Pname, string Previous, string Next, string pageName, int inputHeight, string sk, string method)
        {
            string str;
            Page = pageValid(Pc, Page);        //��֤ҳ��Ϸ���

            /*
            if (Tp == "")
                Tp += "||";
            Tp += Pname;
            */

            #region==��÷�ҳ�ַ���==
            if (Pc <= 1)
                str = "&nbsp;";
            else
            {
                str = "<div class=\"" + sk + "\">";
                if (method == "post")
                {
                    str += "&nbsp;<input class='skip' style=\"line-height:12px; font-size:9pt; padding:0px; width:20px\" name=\"page\" type=\"text\" id=\"page\"";
                    str += " value=\"" + Page + "\" onKeyDown=\"if((event.which == 13)||(event.keyCode == 13))";
                    str += "{document.getElementById('form1').submit();}\">";
                    str += "&nbsp;/&nbsp;" + Pc + "&nbsp;" + pageName + "&nbsp;&nbsp;";

                    if (Page == 1)
                        str += "<font color=\"#999999\">" + Previous + "</font>";
                    else
                    {
                        str += "<a href='' onclick='document.getElementById(\"page\").value=" + (Page - 1) + ";document.getElementById(\"form1\").submit();return false;'";
                        str += ">" + Previous + "</a>";
                    }

                    str += "&nbsp;&nbsp;";

                    if (Page == Pc)
                        str += "<font color=\"#999999\">" + Next + "</font>";
                    else
                    {
                        str += "<a href='' onclick='document.getElementById(\"page\").value=" + (Page + 1) + ";document.getElementById(\"form1\").submit();return false';";
                        str += ">" + Next + "</a>";
                    }
                }
                else
                {
                    str += "&nbsp;<input class='skip' style=\"line-height:12px; font-size:9pt; padding:0px; width:20px\" name=\"page\" type=\"text\" id=\"page\"";
                    str += " value=\"" + Page + "\" onKeyDown=\"if((event.which == 13)||(event.keyCode == 13))";
                    str += "{var pixt=window.location.href;var pix=pixt.indexOf('?');if(pix>0){pixt=pixt.substr(0,pix);}window.location.href=pixt+'?" + Pname + "='+this.value";
                    //str += "{window.navigate('?" + Pname + "=' + this.value";
                    str += " + '&" + transParameters(Tp) + "'";
                    str += ";}\">";
                    str += "&nbsp;/&nbsp;" + Pc + "&nbsp;" + pageName + "&nbsp;&nbsp;";

                    if (Page == 1)
                        str += "<font color=\"#999999\">" + Previous + "</font>";
                    else
                    {
                        str += "<a href=\"?" + Pname + "=" + (Page - 1).ToString();
                        str += "&" + transParameters(Tp);
                        str += "\">" + Previous + "</a>";
                    }

                    str += "&nbsp;&nbsp;";

                    if (Page == Pc)
                        str += "<font color=\"#999999\">" + Next + "</font>";
                    else
                    {
                        str += "<a href=\"?" + Pname + "=" + (Page + 1).ToString();
                        str += "&" + transParameters(Tp);
                        str += "\">" + Next + "</a>";
                    }
                }
                str += "</div>";
            }
            #endregion

            return str;
        }
        #endregion

        #region==ǰ̨��ҳ��ʾ string Pagination1(int Pc, int Page, string Tp, string Pname, string Previous, string Next, string pageName, string sk, int c)==
        /// <summary>
        /// ǰ̨��ҳ��ʾ
        /// </summary>
        /// <param name="Pc">��ҳ��</param>
        /// <param name="Page">��ǰҳ��</param>
        /// <param name="Tp">���Զ����ݵĵ�ַ�����������������||�ָ�</param>
        /// <param name="Pname">ҳ�����name</param>
        /// <param name="Previous">��һҳ</param>
        /// <param name="Next">��һҳ</param>
        /// <param name="pageName">ҳ</param>
        /// <param name="sk">������ʽ</param>
        /// <param name="start">��ʾҳ������</param>
        /// <returns></returns>
        public static string Pagination1(int Pc, int Page, string Tp, string Pname, string First, string Last, string Previous, string Next, string pageName, string sk, int c)
        {
            string str = "";
            int i;
            int j;
            int k;
            Tp += "||" + Pname;
            Page = pageValid(Pc, Page);        //��֤ҳ��Ϸ���

            #region ==�����ʾ��Χ��ʼҳ==
            int c2=Common.Functions.ConvertInt16(c / 2, 2);
            for (j = Page - c2; j < 1; j++) { }
            for (k = j; k > 1 && j - k < c2 - (Pc - Page); k--) { };
            #endregion

            #region ==��÷�ҳ�ַ���==
            str += "<div class=\"" + sk + "\" id=\"" + sk + "\"><table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
            str += "<tr><td><ul>";
            str += "<li><a href=\"?" + Pname + "=1&" + transParameters(Tp) + "\">" + First + "</a></li>";
            str += "<li><a href=\"?" + Pname + "=" + (Page - 1) + "&" + transParameters(Tp) + "\">" + Previous + "</a></li>";
            for (i = k; i < k + c; i++)
            {
                if (i > Pc)
                    break;
                str += "<li";
                if (i == Page)
                    str += " class=\"now\"";
                str += ">";
                if (i != Page)
                    str += "<a href=\"?" + Pname + "=" + i + "&" + transParameters(Tp) + "\">";
                str += i + "</a></li>";
            }
            str += "<li><a href=\"?" + Pname + "=" + (Page + 1) + "&" + transParameters(Tp) + "\"";
            if (Page == Pc)
                str += "onclick=\"return false;\"";
            str += ">" + Next + "</a></li>";
            str += "<li><a href=\"?" + Pname + "=" + Pc + "&" + transParameters(Tp) + "\">" + Last + "</a></li>";
            str += "</ul></td></tr></table></div>";
            #endregion

            return str;
        }
        #endregion

        //#region ==��JMAIL�����ʼ� bool JmailSend==
        ///// <summary>
        ///// ��JMAIL�����ʼ�
        ///// </summary>
        ///// <param name="Subject">����</param>
        ///// <param name="Body">���ݣ�isHtmlΪfalseʱ��ʾ��</param>
        ///// <param name="isHtml">�Ƿ�ʹ��html��ʽ</param>
        ///// <param name="HtmlBody">���ݣ�isHtmlΪtrueʱ��ʾ��</param>
        ///// <param name="MailTo">�ռ����ʼ���ַ</param>
        ///// <param name="From">�������ʼ���ַ</param>
        ///// <param name="FromName">��������ʾ����</param>
        ///// <param name="Smtp">SMTP��������ַ</param>
        ///// <param name="Username">�������û���</param>
        ///// <param name="Password">����������</param>
        ///// <param name="Reply">�ռ��˻ظ��ʼ�ʱ�Ļظ���ַ</param>
        ///// <param name="Bcc">���ܳ��͵�ַ</param>
        //public static void JmailSend(string Subject, string Body, bool isHtml, string HtmlBody, string MailTo, string From, string FromName, string Smtp, string Username, string Password, string Reply, string[] Bcc)
        //{
        //    bool sed = true;
        //    //POP3  pop3=new POP3();
        //    Message JmailMsg = new Message();
        //    JmailMsg.Silent = true;
        //    JmailMsg.Logging = true;
        //    JmailMsg.Charset = "gb2312";
        //    //JmailMsg.AppendHTML(HtmlBody);
        //    JmailMsg.MailServerUserName = Username;
        //    JmailMsg.MailServerPassWord = Password;
        //    JmailMsg.AddRecipient(MailTo, "", "");
        //    JmailMsg.From = From;
        //    JmailMsg.FromName = FromName;

        //    JmailMsg.Charset = "gb2312";
        //    JmailMsg.Logging = true;
        //    JmailMsg.Silent = true;

        //    JmailMsg.ReplyTo = Reply;
        //    for (int i = 0; i < Bcc.Length; i++)
        //    {
        //        JmailMsg.AddRecipientBCC(Bcc[i], "");
        //    }

        //    JmailMsg.Subject = Subject;
        //    JmailMsg.Body = Body;
        //    if (isHtml)
        //        JmailMsg.HTMLBody = HtmlBody;

        //    if (!JmailMsg.Send(Smtp, false))
        //        sed = false;

        //    JmailMsg.Close();
        //}
        //#endregion

        #region==������֤�� string MakeValid(int m)==
        /// <summary>
        /// ������֤��
        /// </summary>
        /// <param name="m">��֤��λ��</param>
        public static string MakeValid(int m)
        {
            string[] validcode = new string[m];		//��֤��
            int rndnum;					            //�����

            Random random = new Random();

            for (int i = 0; i < m; )
            {
                rndnum = random.Next(49, 89);
                if (!((rndnum > (byte)'9') && (rndnum < (byte)'A')) && (rndnum <= (byte)'Z') && (rndnum != (byte)'0') && (rndnum != (byte)'1') && (rndnum != (byte)'O'))
                {
                    char t = (char)rndnum;
                    validcode[i++] = t.ToString();
                }
            }
            string tt = string.Join("", validcode);
            if (HttpContext.Current.Session["validcode"] == null)
                HttpContext.Current.Session.Add("validcode", tt);
            else
                HttpContext.Current.Session["validcode"] = tt;
            return String.Join("", validcode);
        }
        #endregion

        #region==MD5���� string md5(string str,int code)==
        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="str">Ҫ���ܵ��ַ���</param>
        /// <param name="code">λ����16����32</param>
        public static string md5(string str, int code)
        {
            if (code == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }

            else if (code == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            else
            {
                return "error";
            }
        }
        #endregion

        #region==SHA1���� string sha1(string str)==
        /// <summary>
        /// SHA1����
        /// </summary>
        /// <param name="str">Ҫ���ܵ��ַ���</param>
        public static string sha1(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();
        }
        #endregion

        #region ==�����ϴ��ļ�·�� public bool save_url(string content)==
        /// <summary>
        /// ʹ�ñ༭�����ֶΣ�����¼�¼ʱʹ�á�KE�༭����ʱ������
        /// </summary>
        public static bool save_url(string content)
        {
            /*
            Common.Config cfg = new Config();
            string t = content;
            int start = 0;
            int over = -1;
            string instr_str;
            string url_str;
            bool flag = false;
            string sql;

            if (t == "")
                t = "��";

            instr_str = Common.Para.siteHead + "kindeditor/attached";

            cfg.connDb();

            while (true)
            {
                start = t.IndexOf(instr_str, start);
                if (start == -1)
                    break;
                over = t.IndexOf("\"", start);
                url_str = t.Substring(start, over - start);
                start = over;

                sql = "select Url from [za2012_Uploadfile]";
                SqlDataAdapter da = new SqlDataAdapter(sql, cfg.Conn);
                SqlCommandBuilder scb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow drow = ds.Tables[0].NewRow();
                drow[0] = url_str;
                ds.Tables[0].Rows.Add(drow);
                da.Update(ds);

                ds.Dispose();
                scb.Dispose();
                da.Dispose();
            }

            cfg.closeDb();
            */

            return true;
        }
        #endregion

        #region ==ɾ���ض��ϴ��ļ� bool del_url(string content)==
        /// <summary>
        /// ʹ�ñ༭�����ֶΣ�ɾ����¼ʱʹ�á�KE�༭����ʱ�����ء�
        /// </summary>
        public static bool del_url(string content)
        {
            /*
             
            if (content == null)
                return false;
            else if (content == "")
                return false;

            string t = content;
            int start = 0;
            int over;
            string url_str;
            string sql;
            Config cfg = new Config();
            string instr_str = Common.Para.siteHead + "kindeditor/attached";

            cfg.connDb();

            while (true)
            {
                start = t.IndexOf(instr_str, start);
                if (start == -1)
                    break;
                over = t.IndexOf("\"", start);
                url_str = t.Substring(start, over - start);
                start = over;

                sql = "select count(Uid) as c from [za2012_Uploadfile] where url='" + url_str + "'";
                SqlCommand sc = new SqlCommand(sql, cfg.Conn);
                SqlDataReader dr = sc.ExecuteReader();
                dr.Read();
                if (Convert.ToInt64(dr[0].ToString().Trim()) == 1)
                    del_file("/" + url_str);
                dr.Close();
                dr.Dispose();
                dr = null;
                sc.Dispose();
                sc = null;
                sql = "delete from [za2012_Uploadfile] where Uid in (select top 1 Uid from [za2012_Uploadfile] where url='" + url_str + "')";
                sc = new SqlCommand(sql, cfg.Conn);
                sc.ExecuteNonQuery();
                sc.Dispose();
                sc = null;
            }

            cfg.closeDb();

            */

            return true;
        }
        #endregion

        #region ==ɾ�������ϴ��ļ� bool del_other(int start)==
        /// <summary>
        /// ɾ�������ļ�ʱʹ�á�KE�༭����ʱ������
        /// </summary>
        public static bool del_other(int start)
        {
            string url;
            /*
            Common.Config cfg = new Config();
            url = "/" + Common.Para.siteHead + "kindeditor/attached";
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath(url));
            int cycle = 25;
            int i = 1;
            int del = 0;
            bool flag = false;
            string sql;
            SqlDataReader dr;
            SqlCommand sc;

            cfg.connDb();

            foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
            {
                if (i > start + cycle - 1)
                {
                    HttpContext.Current.Response.Redirect("clean.aspx?start=" + (i - del));
                }
                if (fsi is FileInfo)
                {
                    FileInfo fi = (FileInfo)fsi;
                    sql = "select Uid from [za2012_Uploadfile] where url='" + url + fi.Name + "'";
                    sc = new SqlCommand(sql, cfg.Conn);
                    dr = sc.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath("/" + url + fi.Name));
                        }
                        catch
                        {
                            dr.Close();
                            dr.Dispose();
                            sc.Dispose();
                            continue;
                        }
                        finally
                        {
                            del++;
                            dr.Close();
                            dr.Dispose();
                            dr = null;
                            sc.Dispose();
                            sc = null;
                        }

                    }
                    else
                    {
                        dr.Close();
                        dr.Dispose();
                        sc.Dispose();
                    }
                }
                i++;
            }

            cfg.closeDb();
            */

            try
            {
                url = "/" + Common.Para.siteHead + "Uploadfile/temp";
                Directory.Delete(HttpContext.Current.Server.MapPath(url), true);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(url));
            }
            catch { }


            //HttpContext.Current.Response.Write("<script language=\"javascript\">alert(\"�����ļ��ѱ�ɾ��\");window.navigate(\"space.aspx\");</script>");


            return true;
        }
        #endregion

        #region ==�����ļ�·�� bool update_url(string news,string old)==
        /// <summary>
        /// ʹ�ñ༭�����ֶΣ����¼�¼ʱʹ�á�
        /// </summary>
        public static bool update_url(string news, string old)
        {
            #region ==�����´��ļ�==
            save_url(news);
            #endregion

            #region ==ɾ�������ļ�==
            del_url(old);
            #endregion

            return true;
        }
        #endregion

        #region ==ɾ��ָ���ļ� bool del_file(string filename)==
        /// <summary>
        /// ɾ��ָ���ļ���ɾ����¼ʱ�Զ����ø÷���ɾ�����ͼƬ�����õ������ã�
        /// </summary>
        /// <param name="filename">�ļ���ַ����Ӳ�̵�ַ������Server.MapPath��</param>
        public static bool del_file(string filename)
        {
            if (filename != null && !Common.Para.pics_view)
            {
                if (filename != "")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(filename)))
                    {
                        try
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(filename));
                        }
                        catch { return false; }
                    }
                }
            }
            return true;
        }
        #endregion

        #region ==����ļ������� string getFilename(string filepath, string dir)==
        /// <summary>
        /// ���ݵ�ǰ���ڣ�Ϊ���ϴ����ļ������µ�����ļ���������·����������siteHead���磺Uploadfile/Article/201408082341.jpg
        /// </summary>
        /// <param name="filepath">�ϴ����ļ�����Ϊ�˻���ļ���׺��</param>
        /// <param name="dir">�����·�������ô�siteHead��������/�������磺Uploadfile/Article/</param>
        public static string getFilename(string filepath, string dir)
        {
            Random r = new Random();
            r.Next(1000, 9999);
            return getFilename(filepath, dir, r);
        }
        #endregion

        #region ==����ļ��� string getFilename(string filepath, string dir, Random r)==
        /// <summary>
        /// ���ݵ�ǰ���ڣ�Ϊ���ϴ����ļ������µ�����ļ���������·����������siteHead���磺Uploadfile/Article/201408082341.jpg
        /// </summary>
        /// <param name="filepath">�ϴ����ļ�����Ϊ�˻���ļ���׺��</param>
        /// <param name="dir">�����·�������ô�siteHead��������/�������磺Uploadfile/Article/</param>
        /// <param name="r">ʵ�����������</param>
        public static string getFilename(string filepath, string dir, Random r)
        {

            //ȡ���ļ���(����·��)�����һ��"."������
            int intExt = filepath.LastIndexOf(".");

            //ȡ���ļ���չ��
            string strExt = filepath.Substring(intExt);

            //�����Զ��������ں��������ͬΪ�ļ�����,ȷ���ļ������ظ�
            DateTime datNow = DateTime.Now;
            string strNewName;
            while (true)
            {
                strNewName = datNow.Year.ToString() + datNow.Month.ToString() + datNow.Day.ToString() + r.Next(1000, 9999) + strExt;
                //HttpContext.Current.Response.Write(strNewName + "<br />");
                if (!File.Exists("/" + Common.Para.siteHead + dir + strNewName))
                    break;
            }
            return dir + strNewName;
        }
        #endregion

        #region ==����Convert==
        public static long ConvertInt64(object str)
        {
            return ConvertInt64(str, 0);
        }
        public static long ConvertInt64(object str, int i)
        {
            try
            {
                return Convert.ToInt64(str);
            }
            catch
            {
                return i;
            }
        }
        public static int ConvertInt32(object str)
        {
            return ConvertInt32(str, 0);
        }
        public static int ConvertInt32(object str, int i)
        {
            try
            {
                return Convert.ToInt32(str + "");
            }
            catch
            {
                return i;
            }
        }
        public static int ConvertInt16(object str, int i)
        {
            try
            {
                return Convert.ToInt16(str);
            }
            catch
            {
                return i;
            }
        }
        public static float ConvertSingle(object str)
        {
            return ConvertSingle(str, 0);
        }
        public static float ConvertSingle(object str, int i)
        {
            try
            {
                return Convert.ToSingle(str);
            }
            catch
            {
                return i;
            }
        }
        public static DateTime ConvertDateTime(object str)
        {
            return ConvertDateTime(str, Convert.ToDateTime(Common.Para.dt_def));
        }
        public static DateTime ConvertDateTime(object str, DateTime i)
        {
            try
            {
                return Convert.ToDateTime(str);
            }
            catch
            {
                return i;
            }
        }
        public static Decimal ConvertDecimal(object str)
        {
            return ConvertDecimal(str, 0);
        }
        public static Decimal ConvertDecimal(object str, Decimal i)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return i;
            }
        }

        public static string ConvertString(object str)
        {
            return ConvertString(str, "");
        }
        public static string ConvertString(object str, string s)
        {
            try
            {
                return str.ToString();
            }
            catch
            {
                return s;
            }
        }
        #endregion

        #region ==�ӱ��л�ȡ��Ϣ==
        public static string getValue(string name, HttpRequest rq)
        {
            //return convers(rq.Params[name]);
            return rq.Params[name];
        }

        public static string getValue(string name, HttpRequest rq, int method)
        {
            if (method == 1)
            {
                //return convers(rq.Form[name]);
                return rq.Form[name];
            }
            else
            {
                //return convers(rq.QueryString[name]);
                return rq.QueryString[name];
            }
        }
        #endregion

        #region ������datatable��ֳ�����datatable    ������ż���в��
        public static void SplitDatatable(DataTable dt, ref DataTable dt1, ref DataTable dt2)
        {
            int count = dt.Rows.Count;
            dt1 = dt.Copy();
            dt2 = dt.Copy();
            //�õ�������
            for (int i = 1; i <= count / 2; i++)
                dt1.Rows.RemoveAt(i);
            //�õ�ż����
            int tcount = count;
            if (count % 2 == 1)
            {
                tcount++;
            }
            for (int i = 0; i <= tcount / 2 - 1; i++)
                dt2.Rows.RemoveAt(i);
        }
        #endregion

        #region ==��ͼƬ������ͼƬˮӡ==
        /// <summary>    
        /// ��ͼƬ������ͼƬˮӡ
        /// </summary>    
        /// <param name="Path">ԭ������ͼƬ·��</param>   
        /// <param name="Path_syp">���ɵĴ�ͼƬˮӡ��ͼƬ·��</param>   
        /// <param name="Path_sypf">ˮӡͼƬ·��</param>
        /// <param name="position">ˮӡλ��1-���ϣ�2-���ϣ�3-���ϣ�4-���У�5-�м䣬6-���У�7-���£�8-���£�9-���� </param>
        /// <param name="padding">�߾�:Ĭ��10����</param>
        public static void addWaterMark(string Path, string Path_syp, string Path_sypf, int position, int padding)
        {
            try
            {
                System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
                System.Drawing.Image image = System.Drawing.Image.FromFile(Path);

                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                int x = 0;
                int y = 0;
                int width = copyImage.Width;
                int height = copyImage.Height;
                switch (position)
                {
                    case 1:
                        x = 0 + padding;
                        y = 0 + padding;
                        break;
                    case 2:
                        x = image.Width / 2 - copyImage.Width / 2;
                        y = 0 + padding;
                        break;
                    case 3:
                        x = image.Width - copyImage.Width - padding;
                        y = 0 + padding;
                        break;
                    case 4:
                        x = padding;
                        y = image.Height / 2 - copyImage.Height / 2;
                        break;
                    case 5:
                        x = image.Width / 2 - copyImage.Width / 2;
                        y = image.Height / 2 - copyImage.Height / 2;
                        break;
                    case 6:
                        x = image.Width - copyImage.Width - padding;
                        y = image.Height / 2 - copyImage.Height / 2;
                        break;
                    case 7:
                        x = padding;
                        y = image.Height - copyImage.Height - padding;
                        break;
                    case 8:
                        x = image.Width / 2 - copyImage.Width / 2;
                        y = image.Height - copyImage.Height - padding;
                        break;
                    case 9:
                        x = image.Width - copyImage.Width - padding;
                        y = image.Height - copyImage.Height - padding;
                        break;
                }
                System.Drawing.Rectangle r = new System.Drawing.Rectangle(x, y, width, height);
                g.DrawImage(copyImage, r, 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();

                image.Save(Path_syp);
                image.Dispose();
            }
            catch (Exception)
            {
                File.Move(Path, Path_syp);
            }
        }
        #endregion
        
        #region ==��������ͼ==
        /// <summary> 
        /// ��������ͼ 
        /// </summary> 
        /// <param name="originalImagePath">Դͼ·��(ҪServer.MapPath())</param> 
        /// <param name="thumbnailPath">����ͼ·��(ҪServer.MapPath())</param> 
        /// <param name="width">����ͼ���</param> 
        /// <param name="height">����ͼ�߶�</param> 
        /// <param name="mode">��������ͼ�ķ�ʽ:HWָ���߿�����(���ܱ���);Wָ�����߰����� Hָ���ߣ������� Cutָ���߿�ü�(������)</param>���� 
        /// <param name="imageType">Ҫ����ͼ����ĸ�ʽ(gif,jpg,bmp,png) Ϊ�ջ�δ֪���Ͷ���Ϊjpg</param>���� 
        public static void MakeThumb(string originalImagePath, string thumbnailPath, int width, int height, string mode, string imageType)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://ָ���߿����ţ����ܱ��Σ����������������� 
                    break;
                case "W"://ָ�����߰������������������������� 
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://ָ���ߣ������� 
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://ָ���߿�ü��������Σ����������������� 
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //�½�һ��bmpͼƬ 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //�½�һ������ 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //���ø�������ֵ�� 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //���ø�����,���ٶȳ���ƽ���̶� 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //��ջ�������͸������ɫ��� 
            g.Clear(Color.Transparent);
            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������ 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
            new Rectangle(x, y, ow, oh),
            GraphicsUnit.Pixel);
            try
            {
                //��jpg��ʽ��������ͼ 
                switch (imageType.ToLower())
                {
                    case "gif":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "jpg":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "bmp":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "png":
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                }
            }
            catch (System.Exception e)
            {
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region ==�ж϶��ŷָ����������Ƿ���ĳһ����==
        /// <summary>
        /// �ж϶��ŷָ����������Ƿ���ĳһ����
        /// </summary>
        /// <param name="str">���Ƚ��ַ���������Ϊ���ŷָ��Ķ�����룬Ҳ�����Ǽ����Ű�Χ�Ķ������</param>
        /// <param name="Pat">��������</param>
        public static bool checkHave(string str, string Pat)
        {
            if (str == "" || str == null)
                return false;
            str = "<" + str.Replace(",", "><") + ">";
            if (str.IndexOf("<" + Pat + ">") > -1)
                return true;
            else
                return false;
        }
        #endregion

        #region ==����ԭ����MD5���ܺ���ַ�==
        /// <summary>
        /// ����ԭ����MD5���ܺ���ַ�
        /// </summary>
        /// <param name="Passwd">ԭ���룬�ͻ��˽��ա�</param>
        /// <returns></returns>
        public static string updatePasswd(string Passwd)
        {
            if (Passwd == "" || Passwd == null)
                return "";
            string pwd = Common.Functions.md5(Passwd, 32);
            string repwd = Para.adminUrl.Substring(0, Para.adminUrl.Length - 1);
            return pwd = pwd.Substring(0, 8) + repwd + pwd.Remove(0, 24);
        }
        #endregion

        #region ==���������첽����==
        /// <summary>
        /// ���������첽����
        /// </summary>
        /// <param name="url">�������˴���ҳ���ַ��httpȫ·�������Դ���ַ������</param>
        /// <param name="method">���󷽷���GET/POST</param>
        public static string getXMLHTTP(string url, string method)
        {
            return getXMLHTTP(url, method, "", "", "");
        }
        /// <summary>
        /// ���������첽����
        /// </summary>
        /// <param name="url">�������˴���ҳ���ַ��httpȫ·�������Դ���ַ������</param>
        /// <param name="method">���󷽷���GET/POST</param>
        /// <param name="Para">post������һ���ַ���</param>
        public static string getXMLHTTP(string url, string method, string Para)
        {
            return getXMLHTTP(url, method, Para, "", "");
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // ���ǽ���    
            return true;
        }  
        /// <summary>
        /// ���������첽����
        /// </summary>
        /// <param name="url">�������˴���ҳ���ַ��httpȫ·�������Դ���ַ������</param>
        /// <param name="method">���󷽷���GET/POST</param>
        /// <param name="Para">post������һ���ַ���</param>
        /// <param name="cert_path">֤������·�����磺@"e:\\abc.pfx" �� Server.MapPath("/abc.pfx")</param>
        /// <param name="cert_passwd">֤������</param>
        public static string getXMLHTTP(string url, string method, string Para, string cert_path, string cert_passwd)
        {
            string responseFromServer;
            X509Certificate2 cert;
            try
            {
                cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(cert_path, cert_passwd, X509KeyStorageFlags.MachineKeySet);
            }
            catch {
                cert = null;
            }
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
            if (Para == "")
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = method;
                WebResponse rep = req.GetResponse();
                Stream webstream = rep.GetResponseStream();
                StreamReader sr = new StreamReader(webstream);
                responseFromServer = sr.ReadToEnd();
            }
            else
            {
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = method;

                if (cert != null)
                    request.ClientCertificates.Add(cert);
                // Create POST data and convert it to a byte array.
                string postData = Para;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }

            return responseFromServer;
        }
        #endregion

        #region ==��ʽ���۸�����ֻ��ʾ�������֣�С����������0�������ʾ��λС����==
        /// <summary>
        /// ��ʽ���۸�����ֻ��ʾ�������֣�С����������0�������ʾ��λС����
        /// </summary>
        /// <param name="Price">�۸�</param>
        /// <returns></returns>
        public static string formatPrice(string Price)
        {
            string str = "";
            if (Price.Split('.').Length == 1)
                str = Price;
            else if (Convert.ToInt32(Price.Split('.')[1]) == 0)
                str = Convert.ToInt32(Price.Split('.')[0]).ToString("0");
            else
                str = Convert.ToDecimal(Price).ToString("0.00").TrimEnd('0');

            return str;
        }
        #endregion

        #region ==����ʱ���==
        /// <summary>
        /// ����ʱ���
        /// </summary>
        /// <param name="dt">���ڡ�ע��ʱ�������DateTime.UtcNow</param>
        public static string create_timestamp(DateTime dt)
        {
           return ((dt.Ticks - Convert.ToDateTime("1970-1-1").Ticks) / 10000000).ToString();
        }
        #endregion

        //#region ==��÷�������Mac��ַ==
        ///// <summary>
        ///// ��÷�������Mac��ַ
        ///// </summary>
        //public static string GetMacAddress_Server()
        //{
        //    string mac = "";
        //    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection moc = mc.GetInstances();
        //    foreach (ManagementObject mo in moc)
        //    {
        //        if ((bool)mo["IPEnabled"] == true)
        //        {
        //            mac = mo["MacAddress"].ToString();
        //            break;
        //        }
        //    }
        //    return mac;
        //}
        //#endregion

        #region ==��ÿͻ��˵�Mac��ַ==
        /// <summary>
        /// ��ÿͻ��˵�Mac��ַ
        /// </summary>
        public static string GetMacAddress_Browser()
        {
            // �ڴ˴������û������Գ�ʼ��ҳ��
            try
            {
                string strClientIP = HttpContext.Current.Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //Ŀ�ĵص�ip 
                Int32 lhost = inet_addr("");   //���ط�������ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }
                string mac_dest = "";
                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = ":" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
                return mac_dest;
            }
            catch (Exception err)
            {
                return "";
            }

        }
        #endregion

        //#region ==��ÿͻ��˵�Mac��ַ2==
        ///// <summary>
        ///// ��ÿͻ��˵�Mac��ַ2
        ///// </summary>
        //public static string GetMacAddress_Browser2()
        //{
        //    // �ڴ˴������û������Գ�ʼ��ҳ��
        //    try
        //    {
        //        string mac = "";
        //        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //        ManagementObjectCollection moc = mc.GetInstances();
        //        foreach (ManagementObject mo in moc)
        //        {
        //            if ((bool)mo["IPEnabled"] == true)
        //            {
        //                mac = mo["MacAddress"].ToString();
        //                break;
        //            }
        //        }
        //        moc = null;
        //        mc = null;
        //        return mac + "aaa";
        //    }
        //    catch
        //    {
        //        return "error";
        //    }  

        //}
        //#endregion

        #region ��base64��ʽ��ͼƬ���浽ָ���ļ�����
         /// <summary>
        /// ��base64��ʽ��ͼƬ���浽ָ���ļ�����
        /// </summary>
        /// <param name="txtFileName">Ҫ��ͼƬ�����·��</param>
        /// <param name="base64">base64����</param>
        public static void Base64StringToImage(string filepath, string base64)
        {
            //��base64ת��Ϊbyte���飺
            byte[] data = Convert.FromBase64String(base64);
            try
            {
                FileStream ifs = new FileStream(System.Web.HttpContext.Current.Server.MapPath(filepath), FileMode.Create, FileAccess.Write);
                ifs.Write(data, 0, data.Length);
                ifs.Close();
            }
            catch
            {
            }
        }
        #endregion

        #region ��֤���ںϷ���
        /// <summary>
        /// ��֤���ںϷ���
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool validDay(string d)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(d);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region ==Base64==

        #region URL��64λ����
        public static string Base64Encrypt(string sourthUrl)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            string eurl = HttpUtility.UrlEncode(sourthUrl);
            eurl = Convert.ToBase64String(encoding.GetBytes(eurl));
            return eurl;
        }
        #endregion

        #region URL��64λ����
        public static string Base64Decrypt(string eStr)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] buffer = Convert.FromBase64String(eStr);
            string sourthUrl = encoding.GetString(buffer);
            sourthUrl = HttpUtility.UrlDecode(sourthUrl);
            return sourthUrl;
        }
        #endregion

        #region �Ƿ���Base64�ַ���
        /// <summary>
        /// �Ƿ���Base64�ַ���
        /// </summary>
        /// <param name="eStr"></param>
        /// <returns></returns>
        public static bool IsBase64(string eStr)
        {
            if ((eStr.Length % 4) != 0)
            {
                return false;
            }
            if (!Regex.IsMatch(eStr, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase))
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion

        #region ==��֤���ŷָ��Ķ�������Ϊ�Ϸ�long��������==
        /// <summary>
        /// ��֤���ŷָ��Ķ�������Ϊ�Ϸ�long��������
        /// </summary>
        /// <param name="pk">���ŷָ��Ķ������ַ���</param>
        public static string returnConvertPK(string pk)
        {
            if (pk == null)
            {
                return "";
            }
            int i = 0;
            int u;
            string[] arr;
            string str;
            arr = pk.Split(',');
            u = arr.Length;
            str = "";
            for (; i < u; i++)
            {
                if (Common.Functions.ConvertInt64(arr[i], 0) == 0)
                    continue;
                if (i > 0)
                    str += ",";
                str += arr[i].ToString();
            }
            return str;
        }
        #endregion

        #region ==���ݻ����ѯ�б�==
        /// <summary>
        /// ���ݻ����ѯ�б�
        /// </summary>
        /// <param name="model">����������</param>
        /// <param name="orders">���в�ѯ��</param>
        /// <param name="obj">��������ķ���תΪobject����</param>
        public static void Order_list(string model, string[] orders, ref object obj)
        {
            string code = "";
            int orders_i = 0;
            int orders_len = orders.Length;

            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(Environment.NewLine);
            //sb.Append("using Model;");
            //sb.Append(Environment.NewLine);
            sb.Append("using System.Collections.Generic;");
            sb.Append(Environment.NewLine);
            sb.Append("namespace temp_namespace");
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("     public class temp_class");
            sb.Append(Environment.NewLine);
            sb.Append("     {");
            sb.Append(Environment.NewLine);
            sb.Append("         public List<" + model + "> temp_method(object obj)");
            sb.Append(Environment.NewLine);
            sb.Append("         {");
            sb.Append(Environment.NewLine);

            code = "";
            code += "List<" + model + "> l=(List<" + model + ">)obj;";
            code += "\nl.Sort((Comparison<" + model + ">)delegate(" + model + " x, " + model + " y)";
            code += "\n{\n if (x == null)"
                + "\n{"
                + "\nif (y == null)"
                + "\nreturn 0;"
                + "\nelse"
                + "\nreturn -1;"
                + "\n}"
                + "\nelse"
                + "\n{"
                + "\nif (y == null)"
                + "\n{"
                + "\nreturn 1;"
                + "\n}"
                + "\nelse"
                + "\n{"
                + "\nint value = 0;";

            string[] order = orders[0].Split(' ');

            if (order.Length == 2 && order[1].ToLower() == "desc")
                code += "value=y." + order[0] + ".CompareTo(x." + order[0] + ");";
            else
                code += "value=x." + order[0] + ".CompareTo(y." + order[0] + ");";

            for (orders_i = 1; orders_i < orders_len; orders_i++)
            {
                code += "\nif (value == 0)";

                order = orders[orders_i].Split(' ');

                if (order.Length == 2 && order[1].ToLower() == "desc")
                    code += "value=y." + order[0] + ".CompareTo(x." + order[0] + ");";
                else
                    code += "value=x." + order[0] + ".CompareTo(y." + order[0] + ");";
            }

            code += "\nreturn value;"
                + "\n}"
                + "\n}"
                + "\n});"
                + "\nreturn l;";


            sb.Append(code);

            sb.Append(Environment.NewLine);
            sb.Append("         }");
            sb.Append(Environment.NewLine);
            sb.Append("     }");
            sb.Append(Environment.NewLine);
            sb.Append("}");


            code = sb.ToString();
            string[] dll = { "Bin/Model.dll" };
            object[] objs = { obj };
            obj = DoCompile(code, objs,dll);
        }
        #endregion

        #region ==��̬����==
        /// <summary>
        /// ��̬����
        /// </summary>
        /// <param name="eval_str">Ҫִ�еĶ�̬������ַ���</param>
        /// <param name="objs">��̬������ִ�з�����Ҫ�Ĳ�������</param>
        /// <param name="dll">��̬��������Ҫ��dll�ļ�����Ŀ¼Bin�ļ����еģ�</param>
        public static object DoCompile(string eval_str, object[] objs,string[] dll)
        {
            // 1.CSharpCodePrivoder  
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();


            // 2.ICodeComplier,�Ѿ���ʱ,����ֱ�ӵ���CompileAssemblyFromSource  
            // ICodeComplier objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();  


            // 3.CompilerParameters  CompilerParameters.ReferencedAssemblies
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");//����dll  
            for (int i = 0; i < dll.Length; i++)
            {
                objCompilerParameters.ReferencedAssemblies.Add(HttpContext.Current.Server.MapPath("/" + Common.Para.siteHead + dll[i]));//����dll  
            }
            
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;


            // 4.CompilerResults,����  
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, new string[] { eval_str });


            if (cr.Errors.HasErrors)
            {
                HttpContext.Current.Response.Write("�������");
                foreach (CompilerError err in cr.Errors)
                {
                    HttpContext.Current.Response.Write(err.Line + ":" + err.ErrorText);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                // ͨ�����䣬����HelloWorld��ʵ��  
                Assembly objAssembly = cr.CompiledAssembly;
                object obj_temp_Method = objAssembly.CreateInstance("temp_namespace.temp_class");
                MethodInfo objMI = obj_temp_Method.GetType().GetMethod("temp_method");
                // ����ִ��  
                return objMI.Invoke(obj_temp_Method, objs);
            }

            return null;
        }
        #endregion

        #region ==��֤ǩ������==
        ///// <summary>
        ///// ��֤ǩ���������أ�����֤���ݿ�
        ///// </summary>
        ///// <param name="signature">ǩ��</param>
        ///// <param name="non_str">�����</param>
        ///// <param name="stamp">ʱ���</param>
        ///// <param name="ParameterList">ƴ�ӺõĲ�����ֵ��</param>
        //public static string VerifySigned(string signature, string non_str, string stamp, List<string> ParameterList)
        //{
        //    return VerifySigned(signature, non_str, stamp, ParameterList, 1);
        //}
        ///// <summary>
        ///// ��֤ǩ������
        ///// </summary>
        ///// <param name="signature">ǩ��</param>
        ///// <param name="non_str">�����</param>
        ///// <param name="stamp">ʱ���</param>
        ///// <param name="ParameterList">ƴ�ӺõĲ�����ֵ��</param>
        ///// <param name="IsOperationDB">��Ҫ��֤���ݿ��¼��1-����֤ 0-��֤</param>
        //public static string VerifySigned(string signature, string non_str, string stamp, List<string> ParameterList, int IsOperationDB)
        //{
        //    string result = sign_topu.Sign.VerifySigned(signature, non_str, stamp, ParameterList, IsOperationDB);
        //    if (result == "FAIL")
        //        result = "30001";

        //    if (result != "SUCCESS")
        //    {
        //        returnResult(result);
        //    }

        //    return result;
        //}
        #endregion

        #region ==��������뵽ҳ��==
        /// <summary>
        /// ��������뵽ҳ��
        /// </summary>
        /// <param name="resultCode">������</param>
        public static void returnResult(string resultCode)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write("{\"error\":\"" + resultCode + "\"}");
            HttpContext.Current.Response.End();
        }
        #endregion

        #region ==����BLL�㴦����������ܲ����Ļ����������&������������û���ܲ����򲹳�������==
        public static string[] Parameters_Filter(string P_total, string P_main, string P_extend, string[] demand)
        {
            if (P_total == null)
                P_total = "";
            if (P_main == null)
                P_main = "";
            if (P_extend == null)
                P_extend = "";

            if (P_total == "")
            {
                string[] return_arr = new string[2];
                return_arr[0] = Supply_Parameter_Main(P_main, P_extend, demand);
                return_arr[1] = P_extend;
                return return_arr;
            }
            else
            {
                return Parameter_Split(P_total, demand);
            }
        }
        #endregion

        #region ==������չ���������������������==
        /// <summary>
        /// ������չ���������������������������������
        /// </summary>
        /// <param name="Main">ԭ��������</param>
        /// <param name="Extended">��չ����</param> "a,b,c,f"
        /// <param name="demand">��չ��������Ҫ������  ��"��a,��1,��2","��b,��1","��c,��1","��f,��1"��</param>
        private static string Supply_Parameter_Main(string Main, string Extended, string[] demand)
        {
            // �����չ����Ϊ�ջ��ֲ���Ϊ�գ�ֱ���˳�
            if (Extended == "" || demand.Length == 0)
                return Main;

            #region ==����demand������չ�������ڣ��Ҷ�Ӧ�����������ڣ��򲹳䵽�������б�==
            string[] Paras_arr;
            int i, len;
            foreach (string Paras in demand)
            {
                Paras_arr = Paras.Split(',');

                // �����������2����continue
                if (Paras_arr.Length < 2)
                    continue;

                if (checkHave(Extended, Paras_arr[0]))
                {
                    i = 1;
                    len = Paras_arr.Length;
                    for (; i < len; i++)
                    {
                        if (!checkHave(Main, Paras_arr[i]))
                        {
                            if (Main != "")
                                Main += ",";
                            Main += Paras_arr[i];
                        }
                    }
                }
            }

            return Main;
            #endregion
        }
        #endregion

        #region ==���ܲ�����ֳ�����������չ������������չ������Ҫ���������䵽������==
        /// <summary>
        /// ���ܲ�����ֳ�����������չ������������չ������Ҫ���������䵽����������������[������,��չ����]
        /// </summary>
        /// <param name="parameter">�ܲ��������ŷָ�</param> a,b,c,d,e
        /// <param name="demand">��չ��������Ҫ������  ��"��a,��1,��2","��b,��1","��c,��1","��f,��1"��</param>
        private static string[] Parameter_Split(string parameter, string[] demand)
        {
            string[] return_arr = { "", "" };

            // �ܲ���Ϊ��
            if (parameter == "")
                return return_arr;

            // ��ֲ���Ϊ��
            if (demand.Length == 0)
            {
                return_arr[0] = parameter;
                return return_arr;
            }

            #region ==����parameter���ж�ÿ�������Ƿ�Ϊ��չ���������ǽ������������ǵĻ��ж��Ƿ�ҪΪ�������������==
            string Main = "", Extend = "";
            string[] Para_arr = parameter.Split(','); // ����������

            // ����ֲ����е�������Ĩȥ��ֻ������չ����
            List<string> demand_extend = new List<string>();
            int i = 0, len = demand.Length;
            string temp; // ��ʱʹ��
            for (; i < len; i++)
            {
                temp = demand[i].Split(',')[0];
                demand_extend.Add("<" + temp + ">");
            }
            #endregion

            i = 0;
            len = Para_arr.Length;
            int j, len2;
            int index;
            string[] demand_arr;
            for (; i < len; i++)
            {

                index = demand_extend.FindIndex(delegate(string T) { return T == "<" + Para_arr[i] + ">"; });
                if (index != -1 && !checkHave(Extend, Para_arr[i]))
                {   // ���(�ò���������չ����&&Extend���޴˲���)������Ӳ�����Extend�����ж���Ҫ�������������Ƿ���Ҫ��ӵ�������
                    if (Extend != "")
                        Extend += ",";
                    Extend += Para_arr[i];

                    j = 1;
                    demand_arr = demand[index].Split(',');
                    len2 = demand_arr.Length;
                    for (; j < len2; j++)
                    {
                        if (!checkHave(Main, demand_arr[j]))
                        {
                            if (Main != "")
                                Main += ",";
                            Main += demand_arr[j];
                        }
                    }
                }
                else if (index == -1 && !checkHave(Main, Para_arr[i]))
                {   // ���(�ò�����������չ����&&Main���޴˲���)������Ӳ�����Main
                    if (Main != "")
                        Main += ",";
                    Main += Para_arr[i];
                }
            }

            return_arr[0] = Main;
            return_arr[1] = Extend;
            return return_arr;
        }
        #endregion

        #region ���Ip
        /// <summary>
        /// ���Ip k=0 ��֤��׿��ƻ���ֻ�����ȡip������һ�� 1-����֤ ip�� ServerVariables["HTTP_X_FORWARDED_FOR"] �л�ȡ
        /// </summary>
        /// <param name="k"></param>
        public static string getIp(int k)
        {
            string IpAddr = "";
            HttpRequest request = HttpContext.Current.Request;

            if (k == 0)
            {
                string agent = HttpContext.Current.Request.UserAgent;
                try
                {
                    //��׿ϵͳ
                    if (agent.Contains("Android"))
                    {
                        IpAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                    }
                    //ƻ������Pc��
                    else
                    {
                        IpAddr = request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                catch
                {
                    IpAddr = request.ServerVariables["REMOTE_ADDR"];
                }
            }
            else if (k == 1)
            {
                IpAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return IpAddr;
        }
        /// <summary>
        /// ���Ip
        /// </summary>
        /// <returns></returns>
        public static string getIp()
        {
            return getIp(0);
        }
        #endregion
    }
}
