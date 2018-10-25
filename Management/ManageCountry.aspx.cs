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

namespace SMEPayroll.Management
{
    public partial class ManageCountry : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
            //if (!IsPostBack)
            //{
            //    string Country = Request.QueryString["Country"];
            //    if (Country == "inserted")
            //    {
            //        _actionMessage = "Success|Country added successfully";
            //        ViewState["actionMessage"] = _actionMessage;
            //    }
            //    else
            //    {
            //        ViewState["actionMessage"] = "";
            //    }
            //}

        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor editor_Country = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Country");
        //        editor_Country.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Country('" + editor_Country.TextBoxControl.ClientID + "')");             
        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Country")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
       

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where country_id=" + id, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the country record.This country record is in use."));
                        _actionMessage = "Warning|Unable to delete the country record.This country record is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [country] WHERE [id] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Country record is Deleted Successfully."));
                            _actionMessage = "success|Country Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the country record."));
                            _actionMessage = "Warning|Unable to delete the country record.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            try
            {
                string sqlQry = "select * from country where Country='" + strtxtName.Trim() + "' ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Country already exists.";
                }
                else
                {
                    string ssql = "INSERT INTO [country] ([Country]) VALUES ('" + strtxtName.Trim() + "')";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Country added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'Country'"))
                    ErrMsg = "Please Enter Country.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
            try
            {
                string sqlQry = "select * from country where Country='" + strtxtName.Trim() + "' ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Country already exists.";
                }
                else
                {
                    string ssql = "UPDATE [country] SET [Country] = '" + strtxtName.Trim() + "' WHERE [id] = " + id + "";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Country Updated successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'Country'"))
                    ErrMsg = "Please Enter Country.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
    }
}
