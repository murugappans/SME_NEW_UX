using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using efdata;

namespace SMEPayroll.ClaimCapping
{
    public partial class ApplyCliamForm : System.Web.UI.Page
    {
        protected CommonData CommonData;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["commandata"] != null)
            {
                CommonData = (CommonData)Session["commandata"];
            }

            //  Session["MultiCurrency"] = CommonData.CompanyExt.MultiCurrency;
        }
    }
}