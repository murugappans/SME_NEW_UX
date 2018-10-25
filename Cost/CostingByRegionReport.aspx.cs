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

namespace SMEPayroll.Cost
{

    public partial class CostingByRegionReport : System.Web.UI.Page
    {
        protected int comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            comp_id = Utility.ToInteger(Session["Compid"]);


            if (!IsPostBack)
            {
                LoadEmpGrid();
                LoadProject();
            }

            Error.Text = "";
        }

        private void LoadProject()
        {
            string sqlSelect;
            DataSet empDs;
            sqlSelect = "select Bid,BusinessUnit from Cost_Region where Company_ID=" + comp_id + "";
            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            if (empDs.Tables[0].Rows.Count > 0)
            {
                RadGrid2.DataSource = empDs.Tables[0];

            }
        }

        private void LoadEmpGrid()
        {
            string sqlSelect;
            DataSet empDs;
            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " and  Cost_Region >0  ORDER BY EMP_NAME";
            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            if (empDs.Tables[0].Rows.Count > 0)
            {
                RadGrid1.DataSource = empDs.Tables[0];

            }
        }
        protected void GenerateRpt_Click(object sender, EventArgs e)
        {
            #region Getting Emp_code
            string strEmployee = "0";
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        //grid1++;
                        strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }
            #endregion

            #region getting SubProjectid
            string subprojectid = "0";
            foreach (GridItem item in RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        //grid1++;
                        subprojectid = subprojectid + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }
            #endregion

            //validation
            if (strEmployee == "0")
            {
                Error.Text = "Please Select Employee";
                return;
            }
            if (subprojectid == "0")
            {
                Error.Text = "Please Select Project";
                return;
            }

            try
            {
                if (Convert.ToString(dtp1.SelectedDate.Value) == "")
                {
                    Error.Text = "Please Select Date";
                    return;
                }
            }
            catch (Exception exx)
            {
                Error.Text = "Please Select Date";
                return;

            }


            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(dtp1.SelectedDate.Value);
            int m = dt.Date.Month;
            int y = dt.Date.Year;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sSQL = "sp_CostingByRegionReport";
            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = new SqlParameter("@compid", comp_id);
            parms[1] = new SqlParameter("@month", m);
            parms[2] = new SqlParameter("@year", y);
            parms[3] = new SqlParameter("@AsDate", dtp1.SelectedDate.Value);
            parms[4] = new SqlParameter("@emp_code", strEmployee);
            parms[5] = new SqlParameter("@BusinessUnit", subprojectid);

            DataSet rptDs = new DataSet();
            rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);

            Session["CostingDataset1"] = rptDs;

            Response.Redirect("../Cost/CustomCostingReportNew_BusinessUnit.aspx?cat=Cost_Region");


        }






    }


}
