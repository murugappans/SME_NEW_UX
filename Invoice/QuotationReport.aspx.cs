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
using System.Text;
using System.Globalization;

namespace SMEPayroll.Invoice
{
    public partial class QuotationReport : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string Text;
        public int compid;
        public string Quotation1;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                Bind();
            }

        }

        protected void Bind()
        {
            if (Session["Quotation"] != null)
            {
                Quotation1 = (string)Session["Quotation"];
            }

            #region Hourly
                DataSet ds = new DataSet();
                //string Str = "SELECT (select Trade from Trade where Id=Q.Trade)[Trade],[NH],[OT1],[OT2] FROM [QuoationMaster_hourly]Q where [QuotationNo]  ='" + Quotation1 + "'";
                //ds= DataAccess.FetchRS(CommandType.Text, Str, null);
                string  sSQL = "SP_Quotation_Pivot";//  3,10020
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = new SqlParameter("@Company_ID", compid);
                parms[1] = new SqlParameter("@QuotationNo", Quotation1);

                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1_Hor.DataSource = ds.Tables[0];
                    GridView1_Hor.DataBind();
                }
                else
                {
                    HourId.Visible = false;
                }
            #endregion

            #region Monthly
                DataSet ds_M = new DataSet();
                string Str_M = "SELECT (select Trade from Trade where Id=Q.Trade)[Trade],(select  (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) 'EMP_NAME' from Employee Where Emp_code=Q.[EmpCode])as EmpName,[Monthly],[Workingdays] ,[DailyRate] ,[OT1],[OT2]FROM [QuoationMaster_Monthly] Q where [company_id]='"+compid+"' AND [QuotationNo]='" + Quotation1 + "'";
                ds_M = DataAccess.FetchRS(CommandType.Text, Str_M, null);
                if (ds_M.Tables[0].Rows.Count > 0)
                {
                    GridView1_Mon.DataSource = ds_M.Tables[0];
                    GridView1_Mon.DataBind();
                }
                else
                {
                    MonthId.Visible = false;
                }
            #endregion

            #region Variables
                DataSet ds_V = new DataSet();
                string Str_V = "select (select VarName from dbo.Variable_Preference where Vid=QV.[VariableId])VarName,[Amount],[QuotationNo] from dbo.Quotation_Variable QV left join variable_preference VP on QV.VariableId=VP.Vid where QV.QuotationNo='" + Quotation1 + "' AND DailyOneTime <>'Daily' AND QV.Company_id='" + compid + "' ";
                //string Str_V = "SELECT (select VarName from dbo.Variable_Preference where Vid=QV.[VariableId])VarName,[Amount],[QuotationNo],[company_id] FROM [Quotation_Variable]QV  where [company_id]='" + compid + "' AND  [QuotationNo]='" + Quotation1 + "'";
                ds_V = DataAccess.FetchRS(CommandType.Text, Str_V, null);
                if (ds_V.Tables[0].Rows.Count > 0)
                {
                    GridView1_Var.DataSource = ds_V.Tables[0];
                    GridView1_Var.DataBind();
                }
                else
                {
                    adddetId.Visible = false;
                }
            #endregion


            #region Quotation Info
                string sqlQInfo = "SELECT [CreatedDate],[Prefix],[QuotationNo],[SalesRep],(select Sub_Project_Name from subproject where Sub_Project_ID=Q.[Project])as Project,[ClientId],[Remark],[company_id],[Text] FROM [Quotation_Info] Q where [company_id]='" + compid + "' AND  [QuotationNo]='" + Quotation1 + "'";
                
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlQInfo, null);
                if (dr1.Read())
                {
                    Text = dr1[0].ToString();
                    DateTime d=Convert.ToDateTime(dr1[0]);
                    lblCreatedate.Text = d.ToString("d", CultureInfo.CreateSpecificCulture("en-NZ"));
                    lblQuot.Text = dr1[1].ToString() + dr1[2].ToString();
                    lblsalesrep.Text = dr1[3].ToString();
                    lblProject.Text = dr1[4].ToString();
                    divid.InnerHtml = dr1[8].ToString();
                }
                
            #endregion




            #region Client Info
                string sql_ClientInfo = "SELECT [ContactPerson1],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[ClientName],[Phone1],[Phone2],[Fax],[Email],[ContactPerson2],[Remark] FROM [ClientDetails] where [company_id]='" + compid + "' AND [ClientID]=(select ClientId from Quotation_info where QuotationNo='" + Quotation1 + "')";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql_ClientInfo, null);
                if (dr.Read())
                {
                    
                    Label1.Text = Utility.ToString(dr.GetValue(0));
                    Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "BLOCK " + Utility.ToString(dr.GetValue(1)) + "" : "");
                    Label3.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "# " + Utility.ToString(dr.GetValue(3)) + "-" : "") + (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "") + " " + (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                    Label4.Text = (Utility.ToString(dr.GetValue(5)) != "" ? "SINGAPORE " + Utility.ToString(dr.GetValue(5)) + "" : "");

                    lblClientName.Text = (Utility.ToString(dr.GetValue(6)) != "" ?  Utility.ToString(dr.GetValue(6)) : "");
                    lblPhone.Text = (Utility.ToString(dr.GetValue(7)) != "" ?  Utility.ToString(dr.GetValue(7)) : "");
                    lblFax.Text = (Utility.ToString(dr.GetValue(9)) != "" ?  Utility.ToString(dr.GetValue(9)) : "");
                    lblEmail.Text = (Utility.ToString(dr.GetValue(10)) != "" ? Utility.ToString(dr.GetValue(10)) : "");


                }
            #endregion
        }
    }
}
