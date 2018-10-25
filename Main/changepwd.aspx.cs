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

namespace SMEPayroll.Main
{
    public partial class changepwd : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int compid;
        string EmpCode = "", passwordenc = "", passworddec = "", sSQL = "";
        static string oldpwd = "";


        protected void Page_Load(object sender, EventArgs e)
        {
           // txtnpwd.Attributes.Add("value", txtnpwd.Text);  by murugan
           // txtcpwd.Attributes.Add("value", txtcpwd.Text);

            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            EmpCode = Session["EmpCode"].ToString();
            lblusername.Text = Utility.ToString(Session["Username"]);

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            sSQL = "select UserName,Password from employee where emp_code='" + EmpCode + "'";
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            lblusername.Text = Utility.ToString(ds.Tables[0].Rows[0]["UserName"]);
            passworddec = Utility.ToString(ds.Tables[0].Rows[0]["Password"]);
            oldpwd = encrypt.SyDecrypt(passworddec);
            if (txtopwd.Text == oldpwd && txtnpwd.Text == txtcpwd.Text)
            {
                passwordenc = encrypt.SyEncrypt(txtnpwd.Text).ToString();
                sSQL = "update employee set password='" + passwordenc + "' where emp_code='" + EmpCode + "'";
                int i = DataAccess.ExecuteStoreProc(sSQL);
                if (i == 0)
                {
                    lblerror.ForeColor = System.Drawing.Color.Red;
                    lblerror.Text = "Unable to update the record.Please try again!";
                }
                else
                {
                    lblerror.ForeColor = System.Drawing.Color.Green;
                    lblerror.Text = "Password has been changed successfully.";
                }
            }
            else if (txtopwd.Text != oldpwd)
            {
                lblerror.ForeColor = System.Drawing.Color.Red;
                lblerror.Text = "Please enter the correct Current Password.";
            }
            else if (txtnpwd.Text != txtcpwd.Text)
            {
                lblerror.ForeColor = System.Drawing.Color.Red;
                lblerror.Text = "Confirm Password does not match with the New Password.";
            }
           
            

        }

    }
}
