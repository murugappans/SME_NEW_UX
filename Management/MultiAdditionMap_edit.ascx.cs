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
    public partial class MultiAdditionMap_edit : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource_AddType.ConnectionString = Session["ConString"].ToString();
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(addition_DataBinding);
        }

        void addition_DataBinding(object sender, EventArgs e)
        {
            object addcpf = DataBinder.Eval(DataItem, "Additions_id");

            if (addcpf.ToString() != "")
            {
                drpVariable.SelectedValue = addcpf.ToString().Trim();
            }
        }

    }
}