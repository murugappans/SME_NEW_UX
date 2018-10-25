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
using System.Data.SqlClient;
using System.Data.Sql;
using Telerik.Web.UI;

namespace SMEPayroll.Management
{
    public partial class ManageProject : System.Web.UI.Page
    {
        private object _dataItem = null;
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
            {
                Response.Redirect("../SessionExpire.aspx");
            }

            if (!Page.IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Project_List";
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

            //SessionDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Project")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From SubProject Where Parent_Project_ID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project. This Project is in use."));
                        _actionMessage = "Warning|Unable to delete the Project. This Project is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Project] WHERE [id] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Project is Deleted Successfully."));
                            _actionMessage = "success|Project Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project."));
                            _actionMessage = "Warning|Unable to delete the Project.";
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
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Project_ID"))
                    ErrMsg = "Project ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Project_Name'"))
                    ErrMsg = "Please Enter Project Name";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Project_ID'"))
                    ErrMsg = "Please Enter Project ID";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Project added successfully.");
                _actionMessage = "Success|Project added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Project_ID"))
                    ErrMsg = "Project ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Project_Name'"))
                    ErrMsg = "Please Enter Project Name";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Project_ID'"))
                    ErrMsg = "Please Enter Project ID";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Project updated successfully.");
                _actionMessage = "Success|Project updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            //obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("107", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }




        public object DataItem
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

        

        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    this.DataBinding += new EventHandler(addition_DataBinding);
        //}
        //void addition_DataBinding(object sender, EventArgs e)
        //{
            


        //}
       
        protected void drplocname_DataBound(object sender, EventArgs e)
        {
            //string sSQL;
            //DataSet ds_locname = new DataSet();
            //sSQL = "SELECT [Location_ID] FROM [Project]";
            //ds_locname = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            //drplocname.DataSource = ds_locname.Tables[0];
            //drplocname.DataTextField = ds_locname.Tables[0].Columns["Location_ID"].ColumnName.ToString();
            //drplocname.DataValueField = ds_locname.Tables[0].Columns["Location_ID"].ColumnName.ToString();
            //drplocname.DataBind();
            //object addition_type = DataBinder.Eval(DataItem, "id");
            //if (addition_type != DBNull.Value)
            //{
            //    drplocname.SelectedValue = addition_type.ToString();
            //}

            //drplocname.Items.Insert(0, new ListItem("-select-", "-select-"));


            //DataTable subjects = new DataTable();

            //using (SqlConnection con = new SqlConnection(Session["ConString"].ToString()))
            //{

            //    try
            //    {
            //        SqlDataAdapter adapter = new SqlDataAdapter("SELECT [id], [desc] FROM [additions_types]", con);
            //        adapter.Fill(subjects);

            //        drplocname.DataSource = subjects;
            //        drplocname.DataTextField = "SubjectNamne";
            //        drplocname.DataValueField = "SubjectID";
            //        drplocname.DataBind();
            //    }
            //    catch (Exception ex)
            //    {
            //        // Handle the error
            //    }

            //}

            //// Add the initial item - you can add this even if the options from the
            //// db were not successfully loaded
            //drplocname.Items.Insert(0, new ListItem("<Select Subject>", "0"));



            //string constr = ConfigurationManager.ConnectionStrings[Session["ConString"].ToString()].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT [id], [desc] FROM [additions_types]"))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = con;
            //        con.Open();
            //        drplocname.DataSource = cmd.ExecuteReader();
            //        drplocname.DataTextField = "desc";
            //        drplocname.DataValueField = "id";
            //        drplocname.DataBind();
            //        con.Close();
            //    }
            //}
            //drplocname.Items.Insert(0, new ListItem("--Select Customer--", "0"));


            //BindContrydropdown();

            binddrplocation(sender);

        }


        //protected void BindContrydropdown()
        //{
        //    //conenction path for database 
        //    using (SqlConnection con = new SqlConnection(Session["ConString"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * FROM additions_types", con);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        drplocname.DataSource = ds;
        //        drplocname.DataTextField = "[desc]";
        //        drplocname.DataValueField = "[id]";
        //        drplocname.DataBind();
        //        drplocname.Items.Insert(0, new ListItem("--Select--", "0"));
        //        con.Close();
        //    }
        //}


        protected void binddrplocation(object sender)
        {
            DropDownList _drplocname = sender as DropDownList;
            if (_drplocname.DataSource == null)
            {
                string sSQL;
                DataSet ds_locname = new DataSet();
                sSQL = "Select P.Location_ID,L.Location_Name From Project P Inner Join Location L On P.Location_ID = L.ID";
                ds_locname = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                _drplocname.DataSource = ds_locname.Tables[0];
                _drplocname.DataTextField = ds_locname.Tables[0].Columns["Location_Name"].ColumnName.ToString();
                _drplocname.DataValueField = ds_locname.Tables[0].Columns["Location_ID"].ColumnName.ToString();
                _drplocname.DataBind();
                object addition_type = DataBinder.Eval(DataItem, "id");
                if (addition_type != DBNull.Value)
                {
                    //drplocname.SelectedValue = addition_type.ToString();
                }
            }
        }




    }
}
