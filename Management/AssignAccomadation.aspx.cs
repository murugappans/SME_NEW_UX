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
using Telerik.Web.UI;
using System.Net.Mail;
using System.Globalization;

namespace SMEPayroll.Management
{
    public partial class AssignAccomadation : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string sSQL;
        DataSet dsAccDetails;
        DataSet dsAccDetailsTop1;
        static string empcode = "";
        int row = 0;
        static int compid;
        static string username = "";
        string strDormetry = null;
        string strCheckIndate = null;
        string strCheckOutdate = null;
        DateTime newAccdate;
        DateTime newchkDate;
        DateTime oldAccdate;
        DateTime oldAccCheckOut;


        string emp_code = null;
        string eCode = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AssignAccomadation));

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            string empname = Utility.ToString(Session["Emp_Name"]);
            emp_code = Utility.ToString(Session["EmpCode"]);
           
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                empcode = emp_code;
                 eCode = Request.QueryString["empcode"];
                BindEmp();
                FillDormetryCombo();
                
            }
        }
        protected static DataSet getDataSet(string sSqlStr)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSqlStr, null);
            return ds;
        }
        private void FillDormetryCombo()
        {
            DataSet ds_leave = new DataSet();
            string sSql = "sp_GetAccomadationDetails";
            DataSet ds_Dormetry;

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@compId", Utility.ToInteger(compid));
            param[1] = new SqlParameter("@AccCode", 1);
            param[2] = new SqlParameter("@type", Utility.ToInteger("1"));

            ds_Dormetry = DataAccess.FetchRS(CommandType.StoredProcedure, sSql, param);

            drpDormetry.DataSource = ds_Dormetry.Tables[0];
            drpDormetry.DataTextField = ds_Dormetry.Tables[0].Columns["AccomadationName"].ColumnName.ToString();
            drpDormetry.DataValueField = ds_Dormetry.Tables[0].Columns["AccomadationCode"].ColumnName.ToString();
            drpDormetry.DataBind();
        }

        protected void imgbtnsave_Click(object sender, EventArgs e)
        {
            newAccdate = RadDatePicker2.SelectedDate.Value;
            string newDate = null;
            string newCheckOutDate = null;
            string strDormetry = null;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strNewDormetry = null;
            strNewDormetry = drpDormetry.SelectedValue;
            int chkIn;
            int chkOut;
            if (empAccomadationDetails.Tables[0].Rows.Count == 0)
            {
                chkIn = 1;
                chkOut = 0;
                strDormetry = strNewDormetry;
                newDate = RadDatePicker2.SelectedDate.ToString();
                newDate = newDate.Remove(10);
            }
            else
            {
                strDormetry = empAccomadationDetails.Tables[0].Rows[0]["AccomadationCode"].ToString();
                strCheckIndate = empAccomadationDetails.Tables[0].Rows[0]["EffectiveCheckInDate"].ToString();
                strCheckOutdate = empAccomadationDetails.Tables[0].Rows[0]["EffectiveCheckOutDate"].ToString();

                newDate = RadDatePicker2.SelectedDate.ToString();
                newDate = newDate.Remove(10);
                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                newAccdate = DateTime.Parse(newAccdate.ToString(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
              

                newCheckOutDate = Utility.ToString(Convert.ToDateTime(newAccdate.ToString()).AddDays(-1).ToString("dd/MM/yyyy", format));
                if (strCheckOutdate == "")
                {
                    if (drpCheckIN.SelectedValue == "1")
                    {
                        chkIn = 1;
                        chkOut = 2;
                    }
                    else
                    {
                        chkIn = 0;
                        chkOut = 2;
                    }
                }
                else
                {
                    chkIn = 1;
                    chkOut = 0;
                }
            }
            sSQL = "sp_UpdateEmpCheckInCheckOutDetails";
            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[0] = new SqlParameter("@empCode", drpname.SelectedValue);
            sqlParam[1] = new SqlParameter("@accCode", strDormetry);
            sqlParam[2] = new SqlParameter("@checkInDate", newDate);
            sqlParam[3] = new SqlParameter("@checkOutDate", newCheckOutDate);
            sqlParam[4] = new SqlParameter("@NewAccCode", strNewDormetry);
            sqlParam[5] = new SqlParameter("@checkIn", chkIn);
            sqlParam[6] = new SqlParameter("@checkOut", chkOut);
            sqlParam[7] = new SqlParameter("@emp_supervisor", emp_code);
            int status = DataAccess.ExecuteStoreProc(sSQL, sqlParam);

            ViewState["actionMessage"] = "Success| Accomodation assigned to employee successfully.";
            RadGrid1.Rebind();
        }
        [AjaxPro.AjaxMethod]
        public string verifyCheckInDate(string accDate, string empCode,string  checkIn)
        {
            newAccdate = Convert.ToDateTime(accDate);
            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
            newAccdate = DateTime.Parse(accDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
           
            sSQL = "sp_GetEmpCheckInCheckOutDetails";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@empCode", empCode);
            sqlParam[1] = new SqlParameter("@NoRecords", 1);
            bool checkOut = false;
            dsAccDetailsTop1 = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, sqlParam);

            if (dsAccDetailsTop1.Tables[0].Rows.Count > 0)
            {
                if (dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckOutDate"].ToString() != "")
                {
                    oldAccdate = DateTime.Parse(dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckInDate"].ToString(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    oldAccCheckOut = DateTime.Parse(dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckOutDate"].ToString(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    checkOut = true;
                }
                else
                {
                    oldAccdate = DateTime.Parse(dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckInDate"].ToString(), culture, System.Globalization.DateTimeStyles.AssumeLocal);
                }
                if (checkOut)
                {
                    if ((newAccdate > oldAccdate) && (newAccdate >= oldAccCheckOut))
                        return "True";
                    else
                        return "False";
                }
                else
                {
                    if (checkIn == "1")
                    {
                        if (newAccdate > oldAccdate)
                            return "True";
                        else
                            return "False";
                    }
                    if (checkIn == "2")
                    {
                        if (newAccdate >= oldAccdate)
                            return "True";
                        else
                            return "False";
                    }
                    return "True";
                }
            }
            else
                return "True";

        }

        [AjaxPro.AjaxMethod]
        public string validateCheckInCheckOut(string empCode,string strCheckInType)
        {
            sSQL = "sp_GetEmpCheckInCheckOutDetails";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@empCode", empCode);
            sqlParam[1] = new SqlParameter("@NoRecords", 1);
            dsAccDetailsTop1 = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, sqlParam);
            if (dsAccDetailsTop1.Tables[0].Rows.Count > 0)
            {
               // if (dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckOutDate"].ToString() == "")
                    return "True";
                //else
                //    return "False";
            }
            else
            {
                if (strCheckInType == "2")
                 return "False";
                else
                    return "True";
            } 
        }

        [AjaxPro.AjaxMethod]
        public string validateDormetry(string empCode, string AccCode, string chkIn)
        {
            //string[] stringSplit = empCodeStr.Split('_');
            //string empCode = stringSplit[0];
            //string AccCode = stringSplit[1];
            //string chkIn = stringSplit[2];
            sSQL = "sp_GetEmpCheckInCheckOutDetails";
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@empCode", empCode);
            sqlParam[1] = new SqlParameter("@NoRecords", 1);
            dsAccDetailsTop1 = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, sqlParam);
            string rtrValue = "True";
            if (dsAccDetailsTop1.Tables[0].Rows.Count > 0)
            {
                if (chkIn == "1")
                    if (dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckOutDate"].ToString().Trim() == "")
                    {
                        if (dsAccDetailsTop1.Tables[0].Rows[0]["AccomadationCode"].ToString().Trim() == AccCode.Trim())
                            return "False";
                        else
                            return "True";
                    }
                    else
                    {

                        return "True";
                    }
                if (chkIn == "2")
                    if (dsAccDetailsTop1.Tables[0].Rows[0]["effectiveCheckOutDate"].ToString().Trim() == "")
                    {
                        if (dsAccDetailsTop1.Tables[0].Rows[0]["AccomadationCode"].ToString().Trim() != AccCode.Trim())
                            return "False";
                        else
                            return "True";
                    }
                    else
                    {
                        return "False";
                    }
            }
            return "True";
        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            // RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            if (!this.IsPostBack)
            {
                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);
                this.RadGrid1.MasterTableView.Rebind();
            }

        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
            if (e.CommandName == "Delete")
            {
                sSQL = "sp_DelteEmpCheckInCheckOutDetails";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@id", id);
                int status = DataAccess.ExecuteStoreProc(sSQL, sqlParam);
                if (status > 0)
                    ViewState["actionMessage"] = "Success|Record Deleted Successfully.";
              //  Response.Write("<script language='javascript'> alert('Record Deleted Successfully..')</script>");
            }

            
        }
        


        protected void drpDormetry_DataBound(object sender, EventArgs e)
        {
            drpDormetry.Items.Insert(0, "- Select -");
        }


        private void BindEmp()
        {
            DataSet ds_employee = new DataSet();

            string sgroupname = Utility.ToString(Session["GroupName"]);
            string sUserName = Utility.ToString(Session["Username"]);
            string varEmpCode = Session["EmpCode"].ToString();
            
            sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and  company_id=" + compid + " order by emp_name";
            ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpname.DataSource = ds_employee.Tables[0];
            drpname.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpname.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpname.DataBind();
            drpname.SelectedValue = eCode.ToString();
            drpname.Enabled = false;
        }

        public DataSet empAccomadationDetails
        {
            get
            {
                sSQL = "sp_GetEmpCheckInCheckOutDetails";
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@empCode", drpname.SelectedValue);
                sqlParam[1] = new SqlParameter("@NoRecords", -1);
                dsAccDetails = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, sqlParam);
                return dsAccDetails;
            }
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.ItemIndex != 0)
                (e.Item as GridEditableItem)["DeleteColumn"].Controls[0].Visible = false;
        }
    }
}
