using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class objectiveDetails : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        string varEmpCode = "", objectivid = "", stremplyeId = "";
        public object DataItem
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["EmpCode"] != null)
            varEmpCode = Session["EmpCode"].ToString();   
            if(IsPostBack)
            {
                int i = 0;
            }      
            
            SqlDataSourceremarks.ConnectionString = Session["ConString"].ToString();
            
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }
        void addition_DataBinding(object sender, EventArgs e)
        {
            object Employeename = DataBinder.Eval(DataItem, "Employee");
            if (Employeename != DBNull.Value)
            {
                lblemployee_name.Text = Employeename.ToString();
            }
            object Employeeid = DataBinder.Eval(DataItem, "EmpID");
            if (Employeeid != DBNull.Value)
            {
                stremplyeId = Employeeid.ToString();

               
            }
            object per = DataBinder.Eval(DataItem, "Performance");
            if (per != DBNull.Value)
            {
                drperformance.SelectedValue = per.ToString();
            }
            object status = DataBinder.Eval(DataItem, "Status");
            if (status != DBNull.Value)
            {
                drstatus.SelectedValue = status.ToString();
            }
            object objectiveid = DataBinder.Eval(DataItem, "ObjectiveId");
            if (objectiveid != DBNull.Value)
            {
                objectivid = objectiveid.ToString();
            }
            SqlDataSourceremarks.SelectCommand = "Select objRemark.*, emp.emp_name as Employee from ObjectiveRemarks objRemark inner join employee emp on objRemark.EmpId = emp.emp_code where ObjectiveId = " + objectivid + " and (EmpId = " + stremplyeId + " or EmpId = " + varEmpCode + ") order by time asc";
        }
        }
}