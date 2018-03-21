using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model{
    //Page
	public class Pages
    {
        private int _pageStyle;
        /// <summary>
        /// 分页样式。0-后台默认 1-前台默认
        /// </summary>
        public int PageStyle
        {
            get { return _pageStyle; }
            set { _pageStyle = value; }
        } 

        private int _Pc;
        /// <summary>
        /// 获得总页数
        /// </summary>	
        public int Pc
        {
            get { return _Pc; }
            set { _Pc = value; }
        }

        private int _Page;
        /// <summary>
        /// 当前页。不分页时可输入任意数
        /// </summary>	
        public int Page
        {
            get { return _Page; }
            set { _Page = value; }
        }

        private int _Ps;
        /// <summary>
        /// 每页数量。0-不分页
        /// </summary>	
        public int Ps
        {
            get { return _Ps; }
            set { _Ps = value; }
        }

        private string _Tp;
        /// <summary>
        /// 不自动传递的地址栏参数。多个参数用||分隔。可为空。
        /// </summary>	
        public string Tp
        {
            get { return _Tp; }
            set { _Tp = value; }
        }

        private string _Pname;
        /// <summary>
        /// 页码参数name。可为空。
        /// </summary>	
        public string Pname
        {
            get { return _Pname; }
            set { _Pname = value; }
        }

        private string _Previous;
        /// <summary>
        /// “上一页”。可为空。
        /// </summary>	
        public string Previous
        {
            get { return _Previous; }
            set { _Previous = value; }
        }

        private string _Next;
        /// <summary>
        /// “下一页”。可为空。
        /// </summary>	
        public string Next
        {
            get { return _Next; }
            set { _Next = value; }
        }

        private string _First;
        /// <summary>
        /// “首页”。pageStyle=1时专用。可为空。
        /// </summary>	
        public string First
        {
            get { return _First; }
            set { _First = value; }
        }

        private string _Last;
        /// <summary>
        /// “尾页”。pageStyle=1时专用。可为空。
        /// </summary>	
        public string Last
        {
            get { return _Last; }
            set { _Last = value; }
        }

        private string _pageName;
        /// <summary>
        /// “页”。可为空。
        /// </summary>	
        public string pageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }

        private int _inputHeight;
        /// <summary>
        /// 文本框的高。pageStyle=0时专用。可为空。
        /// </summary>	
        public int inputHeight
        {
            get { return _inputHeight; }
            set { _inputHeight = value; }
        }

        private int _c;
        /// <summary>
        /// 页码显示数量。pageStyle=1时专用。最好用奇数。
        /// </summary>	
        public int c
        {
            get { return _c; }
            set { _c = value; }
        }

        private string _sk;
        /// <summary>
        /// 容器样式。pageStyle=0时专用。可为空。
        /// </summary>	
        public string sk
        {
            get { return _sk; }
            set { _sk = value; }
        }

        private string _method;
        /// <summary>
        /// 表单提交方式post/get。pageStyle=0时专用。可为空。
        /// </summary>	
        public string method
        {
            get { return _method; }
            set { _method = value; }
        }



        private string _page_str;
        /// <summary>
        /// 生成的结果字符串。
        /// </summary>	
        public string page_str
        {
            get { return _page_str; }
            set { _page_str = value; }
        }
		   
    }
}

