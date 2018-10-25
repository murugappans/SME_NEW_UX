using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class objDetails : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        string varEmpCode = "", objectivid = "", strMgrId = "";
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
            if (Session["EmpCode"] != null)
                varEmpCode = Session["EmpCode"].ToString();
            if (IsPostBack)
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
            object Mgrid = DataBinder.Eval(DataItem, "ManagerId");
            if (Mgrid != DBNull.Value)
            {
                strMgrId = Mgrid.ToString();
            }
            SqlDataSourceremarks.SelectCommand = "Select objRemark.*, emp.emp_name as Employee from ObjectiveRemarks objRemark inner join employee emp on objRemark.EmpId = emp.emp_code where ObjectiveId = " + objectivid + " and (EmpId = " + strMgrId + " or EmpId = " + varEmpCode + ") order by time asc";
        }
    }
}