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
    public partial class ManageCertificateCategory : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string _actionMessage = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");         

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                RadGrid1.ExportSettings.FileName = "Certificate_Category_List";
            }
        }
        private object _dataItem = null;

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
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet CategoryDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "";
                int compID = Utility.ToInteger(Session["Compid"].ToString());
                sSQL = "SELECT CC.id, [Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory] CC  where Company_Id in ("+ compID + ")";

                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.CategoryDetails;
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.MasterTableView.ClearEditItems();
            }
        }
        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //Category_Name
            //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            //{
            //    GridEditableItem item = e.Item as GridEditableItem;
            //    GridTextBoxColumnEditor editor_Category_Name = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Category_Name");
            //    editor_Category_Name.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_CategoryName('" + editor_Category_Name.TextBoxControl.ClientID + "')");
            //}
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Certificate Category")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Company_ID"]);
                if (strSystemDefined == "-1")
                {
                    //e.Item.Cells[7].Controls[0].Visible = false;
                    //e.Item.Cells[8].Controls[0].Visible = false; //Comment by Sandi
                }
            }
         }   
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
           

            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                string CategoryName = (userControl.FindControl("txtCategoryName") as TextBox).Text;
                string ExpiryType = (userControl.FindControl("drpExpriy") as DropDownList).SelectedItem.Value;
                compid = Utility.ToInteger(Session["Compid"]);

                if (CategoryName == "")
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please enter Category Name."));
                    _actionMessage = "Warning|Please enter Category Name.";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
                else
                {
                    //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select count(*) from CertificateCategory where (Company_Id=-1 or Company_Id=" + compid + ") and Category_Name='" + CategoryName + "' and ColID=" + ExpiryType, null);
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select count(*) from CertificateCategory where (Company_Id=" + compid + ") and Category_Name='" + CategoryName + "' and ColID=" + ExpiryType, null);
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "0")
                        {
                            // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add this category.Reason:Certificate Category already Exists."));
                            _actionMessage = "Warning|Certificate Category already Exists.";
                            ViewState["actionMessage"] = _actionMessage;
                            e.Canceled = true;
                        }
                        else
                        {
                            string sSQL = "INSERT INTO CertificateCategory (Company_ID, Category_Name,COLID) VALUES (" + compid + ",'" + CategoryName + "'," + ExpiryType + ")";

                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Certificate Category is Added Successfully."));
                                _actionMessage = "Success|Certificate Category Added Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add the Certificate Category."));
                                _actionMessage = "Warning|Unable to add the Certificate Category.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                        }
                    }
                }
              

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to add record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];

                string CategoryName = (userControl.FindControl("txtCategoryName") as TextBox).Text;
                string ExpiryType = (userControl.FindControl("drpExpriy") as DropDownList).SelectedItem.Value;
                compid = Utility.ToInteger(Session["Compid"]);

                if (CategoryName == "")
                {
                    // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please enter Category Name."));
                    _actionMessage = "Warning|Please enter Category Name.";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
                else
                {


                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select count(*) from CertificateCategory where Company_Id=" + compid + " and Category_Name='" + CategoryName + "' and ColID=" + ExpiryType, null);
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "0")
                        {
                            // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add this category.Reason:Certificate Category already Exists."));
                            _actionMessage = "Warning|Certificate Category already Exists.";
                            ViewState["actionMessage"] = _actionMessage;
                            e.Canceled = true;
                        }

                        else
                        {

                            string sSQL = "UPDATE CertificateCategory SET Category_Name = '" + CategoryName + "',COLID=" + ExpiryType + " WHERE id = " + id.ToString();

                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Certificate Category is Updated Successfully."));
                                _actionMessage = "Success|Certificate Category Updated Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the Certificate Category."));
                                _actionMessage = "Warning|Unable to update the Certificate Category.";
                                ViewState["actionMessage"] = _actionMessage;
                            }

                        }

                    }

                       




                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to add record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];
                compid = Utility.ToInteger(Session["Compid"]);

                if (compid == -1)
                {
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Certificate Category."));
                }
                else
                {
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From  EmployeeCertificate Where CertificateCategoryID=" + id.ToString(), null);
                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Certificate Category. This Certificate Category is in use."));
                            _actionMessage = "Warning|Unable to delete the Certificate Category. This Certificate Category is in use.";
                            ViewState["actionMessage"] = _actionMessage;
                            e.Canceled = true;
                        }
                        else
                        {
                            string sSQL = "DELETE FROM CertificateCategory WHERE [id] =" + id.ToString();

                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Certificate Category is Deleted Successfully."));
                                _actionMessage = "Success|Certificate Category Deleted Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Certificate Category."));
                                _actionMessage = "Warning|Unable to delete the Certificate Category.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
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
                _actionMessage = "Warning|Unable to delete record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }     
       
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("107", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
    }
}

