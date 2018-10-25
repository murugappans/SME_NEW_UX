using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class RecommendedAppraisal : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        static string varFileName = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        int compid;
        string varEmpCode = "";
        string dtFrom = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cmpEndDate.ValueToCompare = DateTime.Now.ToShortDateString();
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();


            }
        }
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["EmpCode"] != null)
                varEmpCode = Session["EmpCode"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
           
        }

        protected void imgbtnfetch_Click(object sender, ImageClickEventArgs e)
        {
            dtFrom = string.Format("{0:yyyy-MM-dd}",RadDatePickerFrom.SelectedDate);
            SqlDataSource2.SelectCommand = "select eo.*,e.emp_name as Employee,em.emp_name as Manager from Emp_Appraisal eo inner join employee e on e.emp_code = eo.EmpId inner join employee em on em.emp_code = eo.ManagerId where DueDate >= '" + dtFrom + "' and IsRecommend=1 ";
        }
    }
}