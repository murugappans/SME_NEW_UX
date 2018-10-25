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
using System.Net;

namespace SMEPayroll.Management
{
    public partial class EmailLeaveApproval : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string  PrimaryAddress, SecondaryAddress;
        int ApproveNeeded, Eid;
        int ClaimsApproveNeeded;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!IsPostBack)
            {
                SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select [Enable],[PrimaryAddress],[SecondaryAddress],[ClaimsEnable] from EmailApproval where company_id='" + Utility.ToInteger(Session["Compid"]) + "'", null);
                  if (dr_chk.HasRows)
                  {
                      if (dr_chk.Read())
                      {
                          ApproveNeeded = Convert.ToInt32(dr_chk[0]);
                          PrimaryAddress = Convert.ToString(dr_chk[1]);
                          SecondaryAddress = Convert.ToString(dr_chk[2]);
                           if (dr_chk[3] != DBNull.Value) 
                          {
                              ClaimsApproveNeeded = Convert.ToInt32(dr_chk[3]);
                          }
                          else
                          {
                              ClaimsApproveNeeded = 0;
                          }
                      }
                  }

                  #region Leave Checkbox
                      if (ApproveNeeded == 1)
                      {
                          cbkApproveEmail.Checked = true;
                      }
                      else
                      {
                          cbkApproveEmail.Checked = false;
                      }
                  #endregion

                  #region Claims Checkbox
                  if (ClaimsApproveNeeded == 1)
                  {
                      cbkClaimApproveEmail.Checked = true;
                  }
                  else
                  {
                      cbkClaimApproveEmail.Checked = false;
                  }
                  #endregion

                  if (PrimaryAddress != "")
                  {
                      txtPrimary.Text = PrimaryAddress;
                  }

                  if (PrimaryAddress != "")
                  {
                      txtSecondary.Text = SecondaryAddress;
                  }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                bool EnableApproval = false;

                if (cbkApproveEmail.Checked == true)
                {
                    EnableApproval = true;
                }
                else
                {
                    EnableApproval = false;
                }

                bool EnableClaimApproval = false;

                if (cbkClaimApproveEmail.Checked == true)
                {
                    EnableClaimApproval = true;
                }
                else
                {
                    EnableClaimApproval = false;
                }

                SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select  [EId],[Enable],[PrimaryAddress],[SecondaryAddress] from EmailApproval where company_id='"+ Utility.ToInteger(Session["Compid"])+"'", null);
                if (dr_chk.HasRows)
                {
                    if (dr_chk.Read())
                    {
                        Eid = Convert.ToInt32(dr_chk[0]);
                    }
                    string ssqlb = "UPDATE [dbo].[EmailApproval] SET  [claimsEnable]='" + EnableClaimApproval + "', [Enable] = '" + EnableApproval + "',[PrimaryAddress] = '" + txtPrimary.Text + "',[SecondaryAddress] = '" + txtSecondary.Text + "'  WHERE Eid='" + Eid + "'";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                }
                else
                {
                    string ssqlb = "INSERT INTO [dbo].[EmailApproval]([claimsEnable],[Enable],[PrimaryAddress],[SecondaryAddress],[Company_id]) VALUES ('" + EnableClaimApproval + "','" + EnableApproval + "','" + txtPrimary.Text + "','" + txtSecondary.Text + "','" + Utility.ToInteger(Session["Compid"]) + "')";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                }

                //lblError.Text = "Updated Sucessfully.";
                ViewState["actionMessage"] = "Success|Updated Successfully.";

            }
            catch(Exception ex)
            {
                // lblError.Text = ex.ToString();
                ViewState["actionMessage"] = "Warning|Some error occured. Try again later.";
            }

        }

       

        
    }
}
