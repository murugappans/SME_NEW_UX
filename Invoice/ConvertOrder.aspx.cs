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

namespace SMEPayroll.Invoice
{
    public partial class ConvertOrder : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        public string CreatedDate;
        static string varFileName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {

               

                LoadOrder_Info();
               // LoadAddressForClient();

                if (Request.QueryString["From"] != "" && Request.QueryString["From"] != null)
                {
                    //check already for the same quotation order is created
                    //string SQLconfirm = "select * from order_info where QuotationNo='" + Request.QueryString["From"] + "'";
                    string SQLconfirm = "select Top 1 CreatedDate from order_info where quotationNo='" + Request.QueryString["From"] + "' order by Oid Desc";
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQLconfirm, null);
                    if (dr1.HasRows)
                    {
                        if (dr1.Read())
                        {
                            DateTime d = Convert.ToDateTime(dr1.GetDateTime(dr1.GetOrdinal("CreatedDate")));
                            CreatedDate = d.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ"));
                            //CreatedDate = Convert.ToString(dr1.GetDateTime(dr1.GetOrdinal("CreatedDate")));
                        }

                        //ShowMessageBox("Order Created already for this Quotation.  You  are  still  modifing and creating New Order");
                        lblError.Text = "Order Created already for this Quotation on " + CreatedDate + ".  You  are  still  modifing and creating New Order";
                    }
                    else
                    {

                        lblError.Text = "You are in Order Page";
                    }
                }
                
            }

        }

        protected void LoadAddressForClient(string ClientId)
        {

            string sSQLClientAdd = "select [ContactPerson1],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[ClientName],[Phone1],[Phone2],[Fax],[Email],[ContactPerson2],[Remark] from clientdetails where clientID='" + ClientId + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLClientAdd, null);
                while (dr.Read())
                {
                    //Label1.Text = Utility.ToString(dr.GetValue(0));
                    //Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "BLOCK " + Utility.ToString(dr.GetValue(1)) + "" : "");
                    //Label3.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "# " + Utility.ToString(dr.GetValue(3)) + "-" : "") + (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "") + " " + (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                    //Label4.Text = (Utility.ToString(dr.GetValue(5)) != "" ? "SINGAPORE " + Utility.ToString(dr.GetValue(5)) + "" : "");

                    Label1.Text = Utility.ToString(dr.GetValue(0));
                    Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? Utility.ToString(dr.GetValue(1)) : "");
                    Label3.Text = (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                    Label4.Text = (Utility.ToString(dr.GetValue(3)) != "" ? Utility.ToString(dr.GetValue(3)) : "");
                    Label5.Text = (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "");
                    Label6.Text = (Utility.ToString(dr.GetValue(5)) != "" ? Utility.ToString(dr.GetValue(5)) : "");
                }
          
        }

        public string ClientId;
        protected void LoadOrder_Info()
        {
            try
            {
                string OrderNo =Request.QueryString["orderNo"];

                string SQL = "SELECT [Prefix],[OrderNo],(select Clientname from dbo.ClientDetails where ClientId=O.[ClientId])as Client,[CreatedDate],[Remark],[SalesRep],[QuotationNo],[EffectiveDate],[Documentpath],[company_id],(select Sub_Project_Name from subproject where Sub_project_Id=O.[Project]) as  [Project],[ClientId] FROM [dbo].[Order_Info] O where [OrderNo]='" + OrderNo + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                if (dr.Read())
                {
                    lblOrder.Text = dr[0].ToString() + dr[1].ToString();
                    lblClient.Text = dr[2].ToString();
                    DateTime d = DateTime.Parse(dr[3].ToString());
                    lblCreateDate.Text = d.ToString("dd/MM/yyyy");
                    txtRemarks.Text = dr[4].ToString();
                    lblSalesRep.Text = dr[5].ToString();


                    //Effective date and Document path  
                    if(dr[7].ToString() != "" && dr[7].ToString()!="01/01/1900 00:00:00")
                    {
                        DateTime Edate= DateTime.Parse(dr[7].ToString());
                        datePickerEffectiveDate.SelectedDate = Edate;
                    }
                    if (dr[8].ToString() != "")
                    {
                        linkDocument.NavigateUrl = dr[8].ToString();
                    }

                    lblProject.Text = dr[10].ToString();
                    ClientId = dr[11].ToString();
                }
                dr.Close();

                LoadAddressForClient(ClientId);
                
            }
            catch
            {
                throw;
            }
        }

        string Edate;
        protected void btnConvOrdSave_Click(object sender, EventArgs e)
        {
            try
            {

                string OrderNo1 = Request.QueryString["orderNo"];

                DateTime dt = new DateTime();

                if (datePickerEffectiveDate.SelectedDate == null)
                {
                    Edate = null;
                }
                else
                {
                    dt = Convert.ToDateTime(datePickerEffectiveDate.SelectedDate);
                    Edate = dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year;
                }
                //Path
                string uploadpath = "../" + "Invoice/Document/Order" + "/" + OrderNo1 + "";

                if (RadUpload1.UploadedFiles.Count != 0)
                {
                    if (Directory.Exists(Server.MapPath(uploadpath)))
                    {
                        if (File.Exists(Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName()))
                        {
                            string sMsg = "File Already Exist";
                            sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                            Response.Write(sMsg);
                            return;
                        }
                        else
                        {
                            varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                            RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                            varFileName = RadUpload1.UploadedFiles[0].GetName();
                        }
                    }
                    else

                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                     varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                    RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                    varFileName = RadUpload1.UploadedFiles[0].GetName();
                }


                string path = uploadpath + "/" + varFileName;

                if (varFileName == "")
                {
                    path = "";
                }
                string sqlUpdate = "UPDATE [dbo].[Order_Info] SET [Documentpath] ='" + path + "' ,[EffectiveDate] ='" + Edate + "' where [OrderNo]=" + Convert.ToInt32(OrderNo1) + "";
                DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);
                //lblError.Text = "Quotation Updated Successfully";


                Server.Transfer("Order.aspx");
                //LoadOrder_Info();
            }
            catch
            {
                throw;
            }
        }


        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            RadGrid grid = sender as RadGrid;
            if (RadGrid1.MasterTableView.Items.Count == 0)
            {
                RadGrid1.Visible = false;
                HourTd.Visible = false;
            }
        }

        protected void RadGrid2_PreRender(object sender, EventArgs e)
        {
            RadGrid grid = sender as RadGrid;
            if (RadGrid2.MasterTableView.Items.Count == 0)
            {
                RadGrid2.Visible = false;
                MonthTd.Visible = false;
            }
        }

        protected void RadGrid3_PreRender(object sender, EventArgs e)
        {
            RadGrid grid = sender as RadGrid;
            if (RadGrid3.MasterTableView.Items.Count == 0)
            {
                RadGrid3.Visible = false;
                VarTd.Visible = false;
            }
        }


    }
}
