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

namespace SMEPayroll.Invoice
{
    public partial class ClientManagement : System.Web.UI.Page
    {
        string ClientID;
        string comp_id = "";
        string _actionMessage = "";
        int lastClientId;

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            comp_id = Session["Compid"].ToString();

           

        }


      



        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {


            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox combo1 = (RadComboBox)item.FindControl("rcbMyList2");

                TextBox ClientTxt = (TextBox)item.FindControl("txtClient");
                TextBox BlockTxt = (TextBox)item.FindControl("txtBlock");
                TextBox StreetTxt = (TextBox)item.FindControl("txtStreet");
                TextBox LevelTxt = (TextBox)item.FindControl("txtLevel");
                TextBox UnitTxt = (TextBox)item.FindControl("txtUnit");
                TextBox PostalTxt = (TextBox)item.FindControl("txtPostal");
                TextBox Phone1Txt = (TextBox)item.FindControl("txtPhone1");
                TextBox Phone2Txt = (TextBox)item.FindControl("txtPhone2");
                TextBox FaxTxt = (TextBox)item.FindControl("txtFax");
                TextBox mailTxt = (TextBox)item.FindControl("txtmail");
                TextBox ContactPerson1Txt = (TextBox)item.FindControl("txtContactPerson1");
                TextBox ContactPerson2Txt = (TextBox)item.FindControl("txtContactPerson2");
                TextBox RemarkTxt = (TextBox)item.FindControl("txtRemark");

                string ssqlinsert = "INSERT INTO [dbo].[ClientDetails]([ClientName],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[Phone1],[Phone2],[Fax],[Email],[ContactPerson1],[ContactPerson2],[Remark],[company_id]) VALUES ('" + ClientTxt.Text + "','" + BlockTxt.Text + "','" + StreetTxt.Text + "','" + LevelTxt.Text + "','" + UnitTxt.Text + "','" + PostalTxt.Text + "','" + Phone1Txt.Text + "','" + Phone2Txt.Text + "','" + FaxTxt.Text + "','" + mailTxt.Text + "','" + ContactPerson1Txt.Text + "','" + ContactPerson2Txt.Text + "','" + RemarkTxt.Text.Replace("'","''") + "','" + comp_id + "')";
                // DataAccess.FetchRS(CommandType.Text, ssqlinsert, null);
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(ssqlinsert);
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Already Exist.";
                    }
                        //ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to add record. Reason:" + ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    
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
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);

        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {


            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox combo = (RadComboBox)item.FindControl("rcbMyList2");

                TextBox ClientTxt = (TextBox)item.FindControl("txtClient");
                TextBox BlockTxt = (TextBox)item.FindControl("txtBlock");
                TextBox StreetTxt = (TextBox)item.FindControl("txtStreet");
                TextBox LevelTxt = (TextBox)item.FindControl("txtLevel");
                TextBox UnitTxt = (TextBox)item.FindControl("txtUnit");
                TextBox PostalTxt = (TextBox)item.FindControl("txtPostal");
                TextBox Phone1Txt = (TextBox)item.FindControl("txtPhone1");
                TextBox Phone2Txt = (TextBox)item.FindControl("txtPhone2");
                TextBox FaxTxt = (TextBox)item.FindControl("txtFax");
                TextBox mailTxt = (TextBox)item.FindControl("txtmail");
                TextBox ContactPerson1Txt = (TextBox)item.FindControl("txtContactPerson1");
                TextBox ContactPerson2Txt = (TextBox)item.FindControl("txtContactPerson2");
                TextBox RemarkTxt = (TextBox)item.FindControl("txtRemark");

                object ClientID1 = item.OwnerTableView.DataKeyValues[item.ItemIndex]["ClientID"];
                int ClientID = Convert.ToInt32(ClientID1);

                try
                {
                    string ssqla = "UPDATE [ClientDetails] SET [ClientName] ='" + ClientTxt.Text + "',[Block] ='" + BlockTxt.Text + "',[StreetBuilding]='" + StreetTxt.Text + "',[Level]='" + LevelTxt.Text + "',[Unit] ='" + UnitTxt.Text + "',[PostalCode]='" + PostalTxt.Text + "' ,[Phone1]='" + Phone1Txt.Text + "',[Phone2]='" + Phone2Txt.Text + "',[Fax]='" + FaxTxt.Text + "',[Email]='" + mailTxt.Text + "',[ContactPerson1]='" + ContactPerson1Txt.Text + "',[ContactPerson2]='" + ContactPerson2Txt.Text + "',[Remark]='" + RemarkTxt.Text.Replace("'", "''") + "'  WHERE ClientID='" + ClientID + "' AND company_id='" + comp_id + "'";
                    DataAccess.FetchRS(CommandType.Text, ssqla, null);
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Already Exist.";
                    }
                        //ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to add record. Reason:"+ ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    
                }
            }
        }






        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ClientID"]);

                string sSQL = "DELETE FROM [ClientDetails] WHERE [ClientID] ='" + id +"' AND company_id='" + comp_id + "'";

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Client  Deleted Successfully."));
                    _actionMessage = "Success|Client  Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;

                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Client."));
                    _actionMessage = "Warning|Unable to delete the Client.";
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";

                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }




       
    }
}
