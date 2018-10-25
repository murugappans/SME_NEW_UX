using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace SMEPayroll.Leaves
{
    public partial class EmailClaimApprove : System.Web.UI.Page
    {
        string _actionMessage = "";
        string trx_id, status, TabStatus, emp;
        protected void Page_Load(object sender, EventArgs e)
        {

            ViewState["actionMessage"] = "";
            trx_id = Request.QueryString["trx_id"];
            status = Request.QueryString["status"];
            emp = Request.QueryString["emp"];


            if (trx_id != "" && status != "")
            {

                updateStatus(trx_id, status, emp);

            }
            else
            {
                //Response.Write("Not Saved..Something went wrong");
                //lblMsg.Text = "Not Approved or Rejected..Something went wrong";
                _actionMessage = "Warning|Not Approved or Rejected..Something went wrong";
                ViewState["actionMessage"] = _actionMessage;
            }




        }

        private void updateStatus(string trx_id, string status, string emp)
        {
            try
            {
                string sqlleave = "select * from emp_additions where trx_id='" + trx_id + "'and emp_code='" + emp + "' ";
                SqlDataReader dr_leave = DataAccess.ExecuteReader(CommandType.Text, sqlleave, null);
                if (dr_leave.HasRows)
                {
                    if (dr_leave.Read())
                    {
                        TabStatus = dr_leave[11].ToString();
                    }

                    if (TabStatus == "Pending")
                    {
                        if (status == "approve")
                        {
                            string ssqlb = "Update emp_additions set claimstatus='Approved' where trx_id='" + trx_id + "' and emp_code='" + emp + "'  ";
                            DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                            //Response.Write("Approved Sucessfully");
                            //lblMsg.Text = "Approved Sucessfully";
                            _actionMessage = "sc|Approved Sucessfully";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else if (status == "reject")
                        {
                            string ssqlb = "Update emp_additions set claimstatus='Rejected' where trx_id='" + trx_id + "' and emp_code='" + emp + "'  ";
                            DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                            //Response.Write("Rejected Sucessfully");
                            //lblMsg.Text = "Rejected Sucessfully";
                            _actionMessage = "sc|Rejected Sucessfully";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                    else if (TabStatus == "Approved" || TabStatus == "Rejected")
                    {
                        //Response.Write("Already Approved or Rejected.");
                        //lblMsg.Text = "Already Approved or Rejected.";
                        _actionMessage = "Warning|Already Approved or Rejected.";
                        ViewState["actionMessage"] = _actionMessage;
                    }


                }
                else
                {
                    //lblMsg.Text = "Not Approved or Rejected..Something went wrong.";
                    _actionMessage = "Warning|Not Approved or Rejected..Something went wrong.";
                    ViewState["actionMessage"] = _actionMessage;
                    //Response.Write("Not Saved..Something went wrong.");
                }

            }
            catch (Exception E)
            {
                //Response.Write("Error:" + E.ToString());
                //string Err = "Error:" + E.ToString();
                //lblMsg.Text = Err;
                _actionMessage = "Warning|Error:"+ E.ToString();
                ViewState["actionMessage"] = _actionMessage;
            }


        }

    }
}
