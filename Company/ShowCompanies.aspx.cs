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


namespace SMEPayroll.Company
{
    public partial class ShowCompanies : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            id = Utility.ToInteger(Session["Compid"].ToString());
            int iTotalEmployeesInDB = 0, iTotalEmployeesAllowed;
            string sSQL = "";

            string sKey = System.Configuration.ConfigurationManager.AppSettings["SYS_CONFIG"];
           if(IsPostBack)
            {
                Session["actionMessage"] = "";
            }
            string[] skey = new string[4];
            skey[0] = "0x59185499C345D05F92CED";
            skey[1] = "1FC2CF2BD2C8BCE8D3462EF";
            skey[2] = "0749EF3CDC4096C6EC516D5";
            skey[3] = "10115D05EA097524FB22C22";

            sKey = sKey.ToUpper().ToString();

            for (int i = 0; i <= 3; i++)
            {
                sKey = sKey.Replace(skey[i].ToUpper().ToString(), "");
            }

            sKey = sKey.Replace("X", "");

            iTotalEmployeesAllowed = Utility.ToInteger(sKey.Replace("X", ""));
            sSQL = "SELECT count(DISTINCT ic_pp_number) FROM employee WHERE company_id <> 1 and termination_date is null";
            System.Data.SqlClient.SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

            while (dr.Read())
            {
                iTotalEmployeesInDB = Utility.ToInteger(dr.GetValue(0));
            }

            if (Session["Certificationinfo"] == null)
            {
                string filePath =Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["CERTIFICATEFILE_PATH"]);
                //...Read Data From TextFile and show data in data grid for Certification...
                if (System.IO.File.Exists(filePath))
                {
                    DataSet Certificationinfo = Utility.GetDataSetFromTextFile(filePath);
                    Session["Certificationinfo"] = Certificationinfo;
                    //RadGridCertification.DataBind();
                }
            }
            if (Session["Certificationinfo"] != null)
            { 
                DataSet info =(DataSet)Session["Certificationinfo"];
                string RowsAllowed = info.Tables[0].Rows[12][1].ToString().Trim();
                iTotalEmployeesAllowed =Convert.ToInt32(RowsAllowed);
                lbltotalemp.Text = Convert.ToInt16(iTotalEmployeesAllowed).ToString();
                lbldbemp.Text = Convert.ToInt16(iTotalEmployeesInDB).ToString();
                lbldiff.Text = Convert.ToInt16(iTotalEmployeesAllowed - iTotalEmployeesInDB).ToString();
            }            
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem) e.Item;
                int companyId = 0;
                bool yes = int.TryParse(dataItem.GetDataKeyValue("Company_Id").ToString(), out companyId);
                if (companyId > 0)
                {
                    ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                    if (btn.Visible)
                        Response.Redirect("AddCompanyNew.aspx?compid=" + companyId.ToString());
                    else
                        e.Canceled = true;
                }
                else
                {
                    Response.Redirect("Error.aspx");
                }
            }
        }
        
        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);
                this.RadGrid1.MasterTableView.Rebind();
            }
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        private DataSet CompanyDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL="";
                if(id==1)
               sSQL = "SELECT company_id,[Company_code], [Company_name],phone,email,(select Country from Country where id = com.country)'Country' FROM [Company] com ";
                else
               sSQL = "SELECT company_id,[Company_code], [Company_name],phone,email,(select Country from Country where id = com.country)'Country' FROM [Company] com where com.company_id=" + id;
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.CompanyDetails;
        }

        protected void RadGrid1_ItemDataBound1(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Edit Company")) == false)
            {
                RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
                RadGrid1.ClientSettings.EnablePostBackOnRowClick = false;
            }
            if (((Utility.AllowedAction1(Session["Username"].ToString(), "Add Company")) == false)|| id !=1)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            }
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Delete Company")) == false)
                {
                    //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                }
           
            
        }

        protected void bnrefresh_Click(object sender, EventArgs e)
        {
            RadGrid1.Rebind();
        }

    }
}
