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
using System.Data.SqlClient;

namespace SMEPayroll.Management
{
    public partial class WorkflowManagement : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string strWF = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
           int comp_id = Utility.ToInteger(Session["Compid"]);

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }

            if (strWF == "1" || strWF == "-1")
            {
                //tblWF1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                //tblWF2.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //tb1rw2.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //tb1rw1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                //tr2.Visible = true;
                //tr1.Visible = false;
                ////radWF11.Visible = true;
                //radWf11.Visible = true;
                //radWf2.Visible = false;
                //rw1.Visible = true;
                //rw2.Visible = true;
                //rw3.Visible = false;
                //rw4.Visible = false;
            }
            else
            {
                    //tr1.Visible = true;
                    //tr2.Visible = false;
                    ////tblWF2.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                    ////tblWF1.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                    ////tb1rw1.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                    ////tb1rw2.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                    //radWf2.Visible = true;
                    //radWf11.Visible = false;
                    //rw3.Visible = true;
                    //rw4.Visible = true;
                    //rw1.Visible = false;
                    //rw2.Visible = false;

                    tdWF1_1.Disabled = true;
                    tdWF1_2.Disabled = true;

                    tdWF1_3.Disabled = true;
                    tdWF1_4.Disabled = true;

                    a1.Disabled = true;
                    a2.Disabled = true;
                    
            }

        }


        public static bool CheckWorkflowEnable()
        {
            bool stat=false;
            string sql = "select ID,WorkFlowName from EmployeeWorkFlow  Where Company_Id='" + Utility.ToInteger(HttpContext.Current.Session["Compid"]) + "'";
            SqlDataReader dr_alias = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            while (dr_alias.Read())
            {
                //companyAliasName = Utility.ToString(dr_alias.GetValue(0));
                stat = true;
            }

            //return false;
            return stat;
        }


    }
}
