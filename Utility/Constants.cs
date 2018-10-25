using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll
{
    public class Constants
    {
        

        public Constants()
        { 
        }
        public const string SESSION_EXPIRE = "Your sessoin has expired, please re-login.";
        public static string SERVER_IP
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SERVER_IP"];
            }
        }

        public static string MAIL_SERVER
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MAIL_SERVER"];
            }
        }

        public static string CONNECTION_STRIN
        {
            get
            {
                return "\\.kumar";
            }
        }
        public static string CONNECTION_STRING
        {
            get
            {
                string server = "server = " + Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_SERVER"]);
                string dbName = "database = " + Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_NAME"]);
                string uid = "uid = " + Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_UID"]);
                string pwd = "pwd = " + Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_PWD"]);
                string conn = "Pooling=false";
                string timeout = "Connection Timeout=600000";
                string conStr = server + ";" + dbName + ";" + uid + ";" + pwd + ";" + conn + ";" + timeout;
                return conStr;
            }
        }

        public static string EVEN_ROW_COLOR
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EVEN_ROW_COLOR"];
            }
        }

        public static string ODD_ROW_COLOR
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ODD_ROW_COLOR"];
            }
        }

        public static string TABLE_BORDER_COLOR
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TABLE_BORDER_COLOR"];
            }
        }

        public static string HEADING_COLOR
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["HEAD_COLOR"];
            }
        }

        public static string BASE_COLOR
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BASE_COLOR"];
            }
        }

        public static string DB_SERVER
        {
            get
            {
                return Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_SERVER"]);
            }
        }

        public static string DB_NAME
        {
            get
            {
                return Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_NAME"]);
            }
        }

        public static string DB_UID
        {
            get
            {
                return Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_UID"]);
            }
        }

        public static string DB_PWD
        {
            get
            {
                return Utility.ToString(System.Configuration.ConfigurationManager.AppSettings["DB_PWD"]);
            }
        }

        public static string FOOTER_TEXT
        {
            get
            {
                return "<A href='#' class=\"footernav\" onclick=\"window.open('http://www.anbgroup.com');\">Developed & Maintained by A&B Group</A>";
            }
        }


    }
}
