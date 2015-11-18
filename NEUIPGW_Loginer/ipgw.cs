using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Management;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;

/*
 <!--IPGWCLIENT_START SUCCESS=YES STATE=connected USERNAME=20155134 FIXRATE=NO SCOPE=domestic DEFICIT=NO BALANCE=31.0 IP=118.202.13.148 IPGWCLIENT_END-->
 
 */

namespace NEUIPGW_Loginer {
    public class ipgw {
        private string username;
        public string username_doer {
            get {
                return this.username;
            }
            set {
                this.username = value;
            }
        }

        private string password;
        public string password_doer {
            get {
                return this.password;
            }
            set {
                this.password = value;
            }
        }

        // 用户信息读取
        public Dictionary<String, String> inf;
        //private string SUCCESS;
        //private string STATE;
        //private string USERNAME;
        //private string FIXRATE;
        //private string SCOPE;
        //private string DEFICIT;
        //private string BALANCE;
        //private string IP;

        //public string SUCCESS_doer
        //{
        //    get
        //    {
        //        return this.SUCCESS;
        //    }
        //    set
        //    {
        //        this.SUCCESS = value;
        //    }
        //}

        //public string STATE_doer
        //{
        //    get
        //    {
        //        return this.STATE;
        //    }
        //    set
        //    {
        //        this.STATE = value;
        //    }
        //}

        //public string USERNAME_doer
        //{
        //    get
        //    {
        //        return this.USERNAME;
        //    }
        //    set
        //    {
        //        this.USERNAME = value;
        //    }
        //}

        //public string FIXRATE_doer
        //{
        //    get
        //    {
        //        return this.FIXRATE;
        //    }
        //    set
        //    {
        //        this.FIXRATE = value;
        //    }
        //}

        //public string SCOPE_doer
        //{
        //    get
        //    {
        //        return this.SCOPE;
        //    }
        //    set
        //    {
        //        this.SCOPE = value;
        //    }
        //}

        //public string DEFICIT_doer
        //{
        //    get
        //    {
        //        return this.DEFICIT;
        //    }
        //    set
        //    {
        //        this.DEFICIT = value;
        //    }
        //}

        //public string BALANCE_doer
        //{
        //    get
        //    {
        //        return this.BALANCE;
        //    }
        //    set
        //    {
        //        this.BALANCE = value;
        //    }
        //}

        //public string IP_doer
        //{
        //    get
        //    {
        //        return this.IP;
        //    }
        //    set
        //    {
        //        this.IP = value;
        //    }
        //}

        public void ipgwi(string usage)
        {
            string reqURL = string.Format("http://ipgw.neu.edu.cn/ipgw/ipgw.ipgw?uid={0}&password={1}&range=1&operation={2}&timeout={3}",  usage.Equals("disconnect") ? "" : this.username,  usage.Equals("disconnect") ? "" : this.password, usage, usage == "connect" ? 0 : 1);
            Uri req1 = new Uri(reqURL);
            HttpWebRequest req2 = (HttpWebRequest)WebRequest.Create(req1);
            req2.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
            req2.Accept = "*/*";
            req2.KeepAlive = false;
            req2.Proxy = null;
            req2.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");

            HttpWebResponse res = (HttpWebResponse)req2.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
            string rdr = sr.ReadToEnd();
            sr.Close();
            res.GetResponseStream().Close();
            //rdr = rdr.ToLower();

            Regex reg = new Regex(@"<!--IPGWCLIENT_START([\s\S]*?)IPGWCLIENT_END-->", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(rdr);
            string[] ele1 = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                ele1[i] = mc[i].Groups[0].Value;
            }
            inf = new Dictionary<string, string>();
            string[] ele2 = ele1[0].Split(' ');
            foreach (string p in ele2)
            {
                string[] pp = p.Split('=');
                if (pp.Length != 2)
                {
                    continue;
                }
                inf.Add(pp[0], pp[1]);
            }
        }
    }
}
