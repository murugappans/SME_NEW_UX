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

namespace SMEPayroll.Management
{
    public partial class JobTitleEdit : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        private object _dataItem2 = null;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource_Cat.ConnectionString = Session["ConString"].ToString();
        }

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
        public object DataItem2
        {
            get
            {
                return this._dataItem2;
            }
            set
            {
                this._dataItem2 = value;
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }

        void addition_DataBinding(object sender, EventArgs e)
        {
            object addcpf = DataBinder.Eval(DataItem, "cat_id");

            if (addcpf.ToString() != "")
            {
                drpVariable.SelectedValue = addcpf.ToString().Trim();
            }
            object addcpf2 = DataBinder.Eval(DataItem, "cat_title");

            if (addcpf2.ToString() != "")
            {
                txtTitle.Text  = addcpf2.ToString().Trim();
            }
        }

    }
}