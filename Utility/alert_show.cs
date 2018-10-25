using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Xml;
using System.Text;

namespace SMEPayroll
{
    public  class alert_show
    {
        public  static void show_alert( Page type,string id)
        {
            string message;
            string message_type="E"; 

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/message.xml"));
            XmlNode messege_id;
            try
            {
                messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Message/MessageId[@id='" + id.ToString().Trim() + "']");
                message = messege_id.Attributes[2].Value.ToString();
                message_type = messege_id.Attributes[1].Value.ToString();
            }
            catch (Exception ex)
            {
                message = "Error";
            }

           

           // StringBuilder sbScript = new StringBuilder();

           

           // sbScript.Append("<script type='text/javascript' src='/scripts/show.js'>" + Environment.NewLine);

           // sbScript.Append(@"</script>");

           //HttpContext.Current.Response.Write(sbScript);


           type.ClientScript.RegisterStartupScript(type.GetType(), "myScript", "AnotherFunction('" + message +"','"+message_type+ "')", true);
        }
    }
    public class alert_msg
    {
        public static string show_alert( string id)
        {
            string message;
            string message_type = "E";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/message.xml"));
            XmlNode messege_id;
            try
            {
                messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Message/MessageId[@id='" + id.ToString().Trim() + "']");
                message = messege_id.Attributes[2].Value.ToString();
                message_type = messege_id.Attributes[1].Value.ToString();
            }
            catch (Exception ex)
            {
                message = "Error";
            }



            //StringBuilder sbScript = new StringBuilder();

            //sbScript.Append("<script type='text/javascript' src='/scripts/show.js'>" + Environment.NewLine);

            //sbScript.Append(@"</script>");

            //HttpContext.Current.Response.Write(sbScript);


            //type.ClientScript.RegisterStartupScript(type.GetType(), "myScript", "AnotherFunction('" + message + "','" + message_type + "')", true);

            return message;
        
        }
    }
}
