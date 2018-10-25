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
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class Common_Roster_Settings : System.Web.UI.Page
    {
        int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                string sql = "";
                sql = "select * from  common_roster_settings where company_id=" + compid;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if(dr.Read())
                {
                txtEarlyIn.Text =dr["early_in"].ToString();
                txtLateIn .Text =dr["late_in"].ToString();
                txtEarlyOut.Text =dr["early_out"].ToString();
                txtBreakStart.Text=dr["breakTime_start"].ToString();
                txtBreakHrs.Text =dr["breakTime_hrs"].ToString();
                txtBreakOTstart.Text =dr["breakTime_OT_start"].ToString();
                txtBreakOThrs.Text=dr["breakTime_OT_hrs"].ToString();

                txtTotalFlexiHours.Text=dr["total_flexi_hrs"].ToString();
                txtFlexiBreakTimeStart.Text=dr["flexi_breakTime_start"].ToString();
                txtFlexiBreakTimeHrs.Text=dr["flexi_breakTime_hrs"].ToString();
                txtFlexiBreakTimeOT.Text=dr["flexi_breakTime_OT_start"].ToString();
                txtFlexiBreakTimeOThrs.Text = dr["flexi_breakTime_OT_hrs"].ToString();

                }
            }
        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
                string sql = "";
                sql = "select * from  common_roster_settings where company_id=" + compid;
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text, sql);
                int Earlyin= txtEarlyIn.Text.Length == 0 ? 0 : Utility.ToInteger(txtEarlyIn.Text);
                int LateIn=txtLateIn.Text.Length == 0 ? 0 : Utility.ToInteger(txtLateIn.Text);
                int EarlyOut= txtEarlyOut.Text.Length == 0 ? 0 : Utility.ToInteger(txtEarlyOut.Text);
                int BreakStart=txtBreakStart.Text.Length == 0 ? 0 : Utility.ToInteger(txtBreakStart.Text);
                int BreakHrs=txtBreakHrs.Text.Length == 0 ? 0 : Utility.ToInteger(txtBreakHrs.Text);
                int BreakOTstart=txtBreakOTstart.Text.Length == 0 ? 0 : Utility.ToInteger(txtBreakOTstart.Text);
                int BreakOThrs=txtBreakOThrs.Text.Length == 0 ? 0 : Utility.ToInteger(txtBreakOThrs.Text);
                
                int totalFlexiHours=txtTotalFlexiHours.Text.Length == 0 ? 0 : Utility.ToInteger(txtTotalFlexiHours.Text);
                int FlexiBreakTimeStart=txtFlexiBreakTimeStart.Text.Length == 0 ? 0 : Utility.ToInteger(txtFlexiBreakTimeStart.Text);
                int FlexiBreakTimeHrs=txtFlexiBreakTimeHrs.Text.Length == 0 ? 0 : Utility.ToInteger(txtFlexiBreakTimeHrs.Text);
                int FlexiBreakTimeOT=txtFlexiBreakTimeOT.Text.Length == 0 ? 0 : Utility.ToInteger(txtFlexiBreakTimeOT.Text);
                int FlexiBreakTimeOThrs=txtFlexiBreakTimeOThrs.Text.Length == 0 ? 0 : Utility.ToInteger(txtFlexiBreakTimeOThrs.Text);




                if (ds.Tables[0].Rows.Count > 0)
                {
                    string usql = "update common_roster_settings set early_in=" + Earlyin + ",late_in=" + LateIn + ",early_out=" + EarlyOut + ",breakTime_start=" + BreakStart + ",breakTime_hrs=" + BreakHrs + ",breakTime_OT_start=" + BreakOTstart + ",breakTime_OT_hrs=" + BreakOThrs + ",total_flexi_hrs=" + totalFlexiHours + ",flexi_breakTime_start=" + FlexiBreakTimeStart + ",flexi_breakTime_hrs=" + FlexiBreakTimeHrs + ",flexi_breakTime_OT_start=" + FlexiBreakTimeOT + ",flexi_breakTime_OT_hrs=" + FlexiBreakTimeOThrs + " where company_id=" + compid;
                    DataAccess.ExecuteNonQuery(usql, null);
                    ShowMessageBox("Successfully Updated ");
                }
                else {
                    string insql = "insert into common_roster_settings VALUES(" + Earlyin + "," + LateIn + "," + EarlyOut + "," + BreakStart + "," + BreakHrs + "," + BreakOTstart + "," + BreakOThrs;
                    insql = insql + "," + totalFlexiHours + "," + FlexiBreakTimeStart + "," + FlexiBreakTimeHrs + "," + FlexiBreakTimeOT + "," + FlexiBreakTimeOThrs + "," + compid + ")";
                    DataAccess.ExecuteNonQuery(insql, null);
                    ShowMessageBox("Successfully Saved");
                
                }
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }

    }
}
