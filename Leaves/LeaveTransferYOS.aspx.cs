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
using System.Net.Mail;
using System.IO;

namespace SMEPayroll.Leaves
{
    public partial class LeaveTransferYOS : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string _actionMessage = "";
        string strdate;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"].ToString());
            lblmsg.Visible = false;
            RadGrid1.Visible = true;

            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                rdTrdate.SelectedDate = System.DateTime.Now;
                //string sSQL = "select Distinct ID, empgroupname from Emp_Group Where Company_id={0} order by Empgroupname";
                //sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                //cmbEmpgroup.Items.Clear();
                //cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                //while (dr.Read())
                //{
                //    cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                //}
                //cmbEmpgroup.SelectedValue = "";
            }
            else
            {
               /* DataSet ds = new DataSet();
                ds = getDataSet("select id from leaves_allowed where leave_type=8 and leave_year='2010'");
                if (ds.Tables[0].Rows.Count <=0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Please set the Leaves Allowed for the Annual Leave";
                    RadGrid1.Visible = false;
                    Button2.Visible = false;
                }
                */
            }

        }

        private object _dataItem = null;

        public object Dataitem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {

            RadGrid1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            double previousyear = Convert.ToDouble(2010) - 1;
            string currentyear = "2010";
            double leavesallowed = 0;
            Button2.Visible = true;
            //string ssql9 = "select leaves_allowed from leaves_allowed where leave_year='" + currentyear + "' and leave_type=8 and group_id='" + cmbEmpgroup.SelectedItem.Value.ToString() + "'";
            //DataSet ds = new DataSet();
            //ds = DataAccess.FetchRS(CommandType.Text, ssql9, null);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    leavesallowed = Convert.ToInt16(ds.Tables[0].Rows[0]["leaves_allowed"]);
            //}

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int i = 1;
                        double LYA = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("LYA"));
                        double LYL = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("LYL"));
                        double empcode = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        double TotalLastYrLeaves = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("TLYLFT"));
                        double intrem = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("LYL")) - Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("MaxATT"));
                        double CurrentYrLeaves = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CYL"));
                        string strcustjd = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("CUSTJD"));
                        string RemLastYrLeaves = "";


                        leavesallowed = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("TAL"));
                        if (intrem >= 0)
                        {
                            RemLastYrLeaves = Convert.ToString(intrem);
                        }
                        else
                        {
                            RemLastYrLeaves = "0";
                        }
                        double leavesallowed1 = 0;
                        string ssql1 = "";
                        string ssql2 = "";
                        string ssql3 = "";

                        leavesallowed1 = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("YOSLA"));

                        try
                        {
                            int intmsg = 0;
                            //if (leavesallowed > 0)
                            //{
                            //    intmsg = intmsg + 1;
                            //    ssql1 = "delete From LeavesAllowedInYears where emp_id='" + empcode + "' and Years='" + LYA.ToString() + "'";
                            //    DataAccess.FetchRS(CommandType.Text, ssql1);

                            //    ssql2 = "Insert Into LeavesAllowedInYears (Years,Emp_ID,LeavesAvailable, LY_Leaves_Bal,LeaveType) Select Top 1 Actual_YOS, Emp_ID, LeavesAllowed, LY_Leaves_Bal,'" + Session["Leave_Model"].ToString() + "' From YOSLeavesAllowed Where Emp_ID = '" + empcode.ToString() + "' Order By Actual_YOS";
                            //    DataAccess.FetchRS(CommandType.Text, ssql2);

                            //    ssql1 = "delete from EmployeeLeavesAllowed where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8";
                            //    DataAccess.FetchRS(CommandType.Text, ssql1);

                            //    ssql2 = "insert into EmployeeLeavesAllowed (emp_id,leave_year,leave_type,leaves_allowed, LY_Leaves_Bal) values ('" + empcode + "','" + LYA.ToString() + "',8,'" + CurrentYrLeaves + "','" + TotalLastYrLeaves + "','" + Session["Leave_Model"].ToString() + "')";
                            //    DataAccess.FetchRS(CommandType.Text, ssql2);
                            //}
                            //if (leavesallowed1 > 0)
                            //{
                            //    intmsg = intmsg + 1;
                            //    ssql3 = "insert into leaves_forefited (emp_id,leave_year,leave_forefited,leave_allowed,leave_forward) values ('" + empcode + "','" + LYA.ToString() + "','" + RemLastYrLeaves + "','" + leavesallowed1 + "','" + TotalLastYrLeaves + "')";
                            //    DataAccess.FetchRS(CommandType.Text, ssql3);
                            //}

                            SqlParameter[] parms = new SqlParameter[9];
                            parms[0] = new SqlParameter("@empid",           Utility.ToInteger(empcode));
                            parms[1] = new SqlParameter("@leavemodel",      Utility.ToInteger(Session["Leave_Model"].ToString()));
                            parms[2] = new SqlParameter("@LYA",             Utility.ToDouble(LYA));
                            parms[3] = new SqlParameter("@CYL",             Utility.ToDouble(CurrentYrLeaves));
                            parms[4] = new SqlParameter("@TLYLFT",          Utility.ToDouble(TotalLastYrLeaves));
                            parms[5] = new SqlParameter("@YOSLA",           Utility.ToDouble(leavesallowed1));
                            parms[6] = new SqlParameter("@TAL",             Utility.ToDouble(leavesallowed));
                            parms[7] = new SqlParameter("@RemLastYrLeaves", Utility.ToDouble(RemLastYrLeaves));
                            parms[8] = new SqlParameter("@transferdate", Utility.ToDate(strcustjd));

                            int retVal = DataAccess.ExecuteStoreProc("Sp_yos_update", parms);
                            //lblmsg.Visible = true;
                            if (retVal >= 1)
                            {
                                //lblmsg.Text = "Leaves Transfered Successfully.";
                                _actionMessage = "sc|Leaves Transfered Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                //lblmsg.Text = "Leaves cannot be transfered.";
                                _actionMessage = "Warning|Leaves cannot be transfered.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                        catch (Exception ex)
                        {
                            string ErrMsg = ex.Message;
                            if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                            {
                                ErrMsg = "Unable to Transfer the Leaves.Please Try again!";
                                _actionMessage = "Warning|"+ErrMsg;
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                    }

                }
            }
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = e.Item as GridDataItem;
            //    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
            //    int empid = Convert.ToInt16(dataItem.Cells[4].Text);

            //    string ssql = "select leave_year from EmployeeLeavesAllowed where leave_year='2010' and emp_id='" + empid + "' And Leave_Type=8";
            //    DataSet ds = new DataSet();
            //    ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        chkBox.Enabled = false;
            //    }
            //    else
            //    {
            //        chkBox.Enabled = true;
            //    }
            //}

            // if LYL Anniversary is 0 then hide the row
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                int LYLAnniversary = Convert.ToInt16(dataItem.Cells[8].Text);

                if (LYLAnniversary <= 0)
                {
                    dataItem.Display = false;
                    //chkBox.Enabled = false;
                   
                }
                else
                {
                    dataItem.Display = true;
                    //chkBox.Enabled = true;
                }
            }
           

        }
    }
}
