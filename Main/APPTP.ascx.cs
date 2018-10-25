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

namespace SMEPayroll.Main
{
    public partial class APPTP : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Appointment apt;

        public Appointment TargetAppointment
        {
            get
            {
                return apt;
            }

            set
            {
                apt = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            //StartingOn.Text = apt.Owner.UtcToDisplay(apt.Start).ToString();
            //FullText.Text = apt.Start.ToString();
            StartingOn.Text = DateTime.Now.ToString();//"hi there";
            //FullText.Text = ;  
            base.OnPreRender(e);
        }

    }
}