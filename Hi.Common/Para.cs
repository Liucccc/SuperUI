using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
//���������ռ䡾���͡�
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    #region ժҪ
    /// <summary>
    /// Config ��ժҪ˵����
    /// </summary>
    #endregion
    public class Para
    {
        public static string siteHead = "";
        //public static string siteHead = "zzlh/";                   //��վͷ·��
        public static string adminUrl = "6h5y4m7d8u8r9b1a/";        //��̨·��                                        
        public static string dt_def = "1900-1-1";                   //Ĭ������
        public static string start_date = "2013-1-1";
        public static string cookie_admins = Common.Para.siteHead + "zzlh_Admins";                  //��̨����Ա��¼cookie��
        public static string cookie_Member = Common.Para.siteHead + "zzlh_Member";                  //ǰ̨cookie��
        public static string[] Email = { "jmail@topu.net", "smtp.ym.163.com", "jmail@topu.net", "topu.net" };
        public static string[,] Init = new string[0, 0];
        public static string[,] Info = new string[0, 0];
        public static string small_window = "true";    //��̨��������ʾС����
        public static bool pics_view = false;       //�Ƿ���ͼƬ��������ܣ������������ɾ����¼�е�ͼƬ����֮������ɾ����¼�е�ͼƬ
        public static string[] area_name = new string[] { "��ҳ����", "��ʦ���", "ҵ��Χ", "������ѯ", "������Ѷ", "��������", "���Ͱ���","���֪ʶ","�̳�֪ʶ","��ʦ����", "����", "ϵͳ����" };  //����Χ�������飩

        //Token ��Ч�ڣ����ӣ�
        public static long EffectiveTime = 1440;//1��
    }
}
