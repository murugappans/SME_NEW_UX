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
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class AccomadationInfo : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        string _actionMessage = "";
        string sSQL = null;

        int compID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Accommodation = Request.QueryString["Accommodation"];
            if (Accommodation == "Inserted")
            {
                _actionMessage = "Success|Accommodation Info Inserted Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            else if (Accommodation == "Updated")
            {
                _actionMessage = "Success|Accommodation Info Updated Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                ViewState["actionMessage"] = "";
            }
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }



        }



        public object Dataitem
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

        protected static DataSet GetDataSet(string sqlQuery)
        {

            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            return ds;
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {

            RadGrid1.DataBind();
        }


        private DataSet AccomadationDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@compId", compID);
                parms1[1] = new SqlParameter("@AccCode", 1);
                parms1[2] = new SqlParameter("@type", 1);
                sSQL = "sp_GetAccomadationDetails";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                return ds;
            }

        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("..\\Management\\AccomadationManagement.aspx?accCode=" + dataItem["AccomadationCode"].Text + "&tType=1");


            }
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                
            }

        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType   == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                

                GridDataItem item = e.Item as GridDataItem;
                System.Web.UI.HtmlControls.HtmlInputButton Img = (System.Web.UI.HtmlControls.HtmlInputButton)item.FindControl("Image3");
                string AccCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccomadationCode"].ToString();
                int AccCount = Utility.ToInteger(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsedCapacity"].ToString());
                string strMediumUrl = "AccomadationGrid.aspx?accCode=" + AccCode + "&compId=" + compID;
                string strmsg = "javascript:ShowInsert('" + strMediumUrl + "');";

                if (AccCount > 0)
                {
                    Img.Disabled = false;
                    Img.Attributes.Add("onclick", strmsg);

                }
                else {
                    Img.Disabled = true ;
                    strmsg = "<script language='java script'> alert('No Check In Records'); </script>";
                    Img.Attributes.Add("onclick", strmsg);

                }
            }
        }


        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string accName = (userControl.FindControl("acctxtName1") as TextBox).Text.Trim();
            string accAddr1 = (userControl.FindControl("txtAccAdd1") as TextBox).Text.Trim();
            string accAddr2 = (userControl.FindControl("txtAccAdd2") as TextBox).Text.Trim();
            string accPostalCode = (userControl.FindControl("txtPostalCode") as TextBox).Text.Trim();
            string accRent = (userControl.FindControl("txtRent") as TextBox).Text.Trim();
            string accCapacity = (userControl.FindControl("txtCapacity") as TextBox).Text.Trim();
            bool accActive = (userControl.FindControl("chkActive") as CheckBox).Checked;
            sSQL = "Sp_InsertAccomadationDetails";
            int i = 0;
            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[i++] = new SqlParameter("@accName", accName);
            sqlParam[i++] = new SqlParameter("@accAddress1", accAddr1);
            sqlParam[i++] = new SqlParameter("@accAddress2", accAddr2);
            sqlParam[i++] = new SqlParameter("@accPostalCode", accPostalCode);
            sqlParam[i++] = new SqlParameter("@accCapacity", accCapacity);
            sqlParam[i++] = new SqlParameter("@accRent", accRent);
            sqlParam[i++] = new SqlParameter("@compId", compID);
            sqlParam[i++] = new SqlParameter("@accActive", accActive);
            int status = DataAccess.ExecuteStoreProc(sSQL, sqlParam);
            RadGrid1.Rebind();
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            int i;
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["AccomadationCode"];
            string id1 = id.ToString();
            string accName = (userControl.FindControl("acctxtName1") as TextBox).Text.Trim();
            string accAddr1 = (userControl.FindControl("txtAccAdd1") as TextBox).Text.Trim();
            string accAddr2 = (userControl.FindControl("txtAccAdd2") as TextBox).Text.Trim();
            string accPostalCode = (userControl.FindControl("txtPostalCode") as TextBox).Text.Trim();
            string accRent = (userControl.FindControl("txtRent") as TextBox).Text.Trim();
            string accCapacity = (userControl.FindControl("txtCapacity") as TextBox).Text.Trim();
            bool accActive = (userControl.FindControl("chkActive") as CheckBox).Checked;
            int bActive = (accActive == true) ? 1 : 0;

            sSQL = "UPDATE AccomadationDetails SET AccomadationName = '" + accName + "',AccomadationAddressLine1='" + accAddr1 + "' ,AccomadationAddressLine2='" + accAddr2 + "' ,AccomadationPostalCode='" + accPostalCode + "' ,Capacity='" + accCapacity + "' ,Rent='" + accRent + "' ,Active=" + bActive + " where AccomadationCode='" + id + "'";

            i = DataAccess.ExecuteStoreProc(sSQL);
            if (i == 1)
            {

                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>User record has been updated."));
                _actionMessage = "success|User record has been updated.";
                ViewState["actionMessage"] = _actionMessage;
            }

        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.AccomadationDetails;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = false ;
            RadGrid1.ExportSettings.FileName = "Accomodation";
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = false;
            RadGrid1.ExportSettings.FileName = "Accomodation";
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToWord();

        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.FileName = "Accomodation";
            RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 24)) + "mm");
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToPdf();
        }


        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            int AccCount = Utility.ToInteger(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UsedCapacity"].ToString());
            if (AccCount > 0)
            {
               // Response.Write("<script language = 'Javascript'>alert('It cannot be deleted.Some Employees are holding this Accomadation');</script>");
                _actionMessage = "Warning|It cannot be deleted.Some Employees are holding this Accommodation.";
                ViewState["actionMessage"] = _actionMessage;

            }
            else
            {
                try
                {
                    string AccCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccomadationCode"].ToString();
                    string sqlDelete = "Delete from AccomodationMasterTable where AccCode='" + AccCode+"'";
                    DataAccess.FetchRS(CommandType.Text, sqlDelete, null);
                    ///////Updated By Jammu Office////////
                    //ShowMessageBox("Deleted Successfully");
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Accomodation is Deleted Successfully."));
                    _actionMessage = "Success|Accommodation Info Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    ///////Updated By Jammu Office ends////////
                }
                 catch(Exception ex)
                {

                   // ShowMessageBox();
                    _actionMessage = "Warning|Some Error Occured. Please try again later.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }


        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
           
        }


    }


    

}
