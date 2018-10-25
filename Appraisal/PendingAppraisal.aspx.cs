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
using efdata;
using System.Text;
using AuditLibrary;//Added by Jammu Office
using System.Linq;
using SMEPayroll.Leaves;

namespace SMEPayroll.Appraisal
{
    public partial class PendingAppraisal : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string s = "", varEmpName = "";
        int compid;
        DataSet dsleaves;
        string email;
        public string WorkFlowName;
        int appLevel;
        int lcount = 0;
        string ecode;
        string loginEmpCode = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varEmpName = Session["Emp_Name"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //SqlDataSource2.ConnectionString = Session["ConString"].ToString(); //Added by Sandi on 27/03/2014
            loginEmpCode = Utility.ToString(Session["EmpCode"]);
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office


            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all")) == false)
                {
                    TextBox1.Text = "noapprover";
                }
                else
                {
                    TextBox1.Text = "approver";
                }
                compid = Utility.ToInteger(Session["Compid"]);

                if ((string)Session["EmpCode"] != "0")//if user login
                {
                    s = Session["Username"].ToString();
                    string strSql = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + s + "'";
                    DataSet leaveset = new DataSet();
                    leaveset = getDataSet(strSql);
                    lblsuper.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_name"]);
                }
                else
                {
                    lblsuper.Text = "-";
                }

            }

           
        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        
        private bool checkemp(string _Approverflag, string _empcode, int _trxid)
        {
            bool result = false;
            if (_Approverflag == "MultiLevel")
            {
                string text = "Select WL.ID,WL.RowID, WorkFlowName ";
                text += "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                text += "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) ";
                text = text + "And Company_ID=" + Utility.ToInteger(this.Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                text = text + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT Leave_supervisor FROM dbo.employee   WHERE emp_code=" + _empcode + " ) ";
                text += "Order By WF.WorkFlowName, WL.RowID";
                this.dsleaves = new DataSet();
                this.dsleaves = DataAccess.FetchRS(CommandType.Text, text, null);
                int num = 0;
                if (this.dsleaves.Tables.Count > 0)
                {
                    if (this.dsleaves.Tables[0].Rows.Count > 0)
                    {
                        num = Utility.ToInteger(this.dsleaves.Tables[0].Rows[0][1]);
                        this.WorkFlowName = Utility.ToString(this.dsleaves.Tables[0].Rows[0][2]);
                    }
                }
                if (num != 0)
                {
                    SqlDataReader sqlDataReader = DataAccess.ExecuteReader(CommandType.Text, "SELECT [STATUS] FROM emp_leaves WHERE trx_id=" + _trxid, null);
                    int num2 = 0;
                    while (sqlDataReader.Read())
                    {
                        if (Utility.ToString(sqlDataReader.GetValue(0)) == "Approved" || Utility.ToString(sqlDataReader.GetValue(0)) == "Open" || Utility.ToString(sqlDataReader.GetValue(0)) == "")
                        {
                            num2 = num;
                            for (int i = num; i >= 1; i--)
                            {
                                if (i == 0)
                                {
                                }
                            }
                        }
                        else
                        {
                            int num3 = Utility.ToInteger(sqlDataReader.GetValue(0));
                            num2 = num3 - 1;
                        }
                    }
                    string str = this.Session["Username"].ToString();
                    string sSQL = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + str + "'";
                    DataSet dataSet = new DataSet();
                    dataSet = PendingAppraisal.getDataSet(sSQL);
                    string a = "";
                    if (dataSet.Tables.Count > 0)
                    {
                        a = dataSet.Tables[0].Rows[0][0].ToString();
                    }
                    string text2 = "L" + num2;
                    string text3 = "SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea ";
                    text3 += "  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.ID=(select payrollGroupid  from EmployeeWorkFlowLevel ";
                    text3 = string.Concat(new object[]
                    {
                        text3,
                        "where rowid='",
                        num2,
                        "'  and workflowid=(select id from EmployeeWorkFlow where workflowname='",
                        this.WorkFlowName,
                        "' and Company_id='",
                        Utility.ToInteger(this.Session["Compid"]).ToString(),
                        "'))"
                    });
                    text3 = text3 + "union select distinct userId from MasterCompany_User where companyid='" + Utility.ToInteger(this.Session["Compid"]).ToString() + "'";
                    SqlDataReader sqlDataReader2 = DataAccess.ExecuteReader(CommandType.Text, text3, null);
                    ArrayList arrayList = new ArrayList();
                    while (sqlDataReader2.Read())
                    {
                        arrayList.Add(sqlDataReader2.GetValue(0));
                    }
                    foreach (object current in arrayList)
                    {
                        if (a == current.ToString())
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }




      
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem gridDataItem = (GridDataItem)e.Item;
            //    int trxid = Utility.ToInteger(gridDataItem.GetDataKeyValue("trx_id").ToString());
            //    string text = Utility.ToString(gridDataItem.GetDataKeyValue("Approver").ToString());
            //    string empcode = Utility.ToString(gridDataItem.GetDataKeyValue("emp_id").ToString());
            //    if (text == "MultiLevel")
            //    {
            //        gridDataItem.Display = (this.checkemp(text, empcode, trxid));
            //    }
            //}

            //if (((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves")) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all"))) == false)
            //{
            //    RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            //}
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
        protected void RadCalendar1_DayRender(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {

        }
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
        protected void radApprovedLeave_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "OpenAppraisal")
            {
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                int aid = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                int emp_code = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["EmpId"]);

                Response.Write("<script>window.open('../Appraisal/ManagerAppraisal.aspx','_self');</script>");


            }
        }
    }
}


