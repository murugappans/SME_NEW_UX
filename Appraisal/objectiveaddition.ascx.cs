using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class objectiveaddition : System.Web.UI.UserControl
    {
        string varEmpCode = "";
        private object _dataItem = null;
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
            varEmpCode = Session["EmpCode"]!=null? Session["EmpCode"].ToString():"";
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }
        void addition_DataBinding(object sender, EventArgs e)
        {
            DataSet ds_employee = new DataSet();
            string sSQL = "Select e.emp_name , e.emp_code from[employee] as e inner join workcardAssigmment as w on w.[EmployeeID] = e.emp_code and w.[SupervisorID] ="+ varEmpCode;
            ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpemployee.DataSource = ds_employee.Tables[0];
            drpemployee.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpemployee.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpemployee.DataBind();
            object Employeeid = DataBinder.Eval(DataItem, "EmpID");
            if (Employeeid != DBNull.Value)
            {
                drpemployee.SelectedValue = Employeeid.ToString();
            }
            object status = DataBinder.Eval(DataItem, "Status");
            if (status != DBNull.Value)
            {
                drstatus.SelectedValue = status.ToString();
            }
        }
        protected void drpemployee_DataBound(object sender, EventArgs e)
        {
            string sgroupname = Utility.ToString(Session["GroupName"]);
            drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));

        }
      
        protected void detailbind()
        {
        }

        }
}