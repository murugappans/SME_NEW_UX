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
    public partial class DailyAttendance : System.Web.UI.Page
    {
        int comp_id;
        string sSQL="";


        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Utility.ToInteger(Session["Compid"]);
            lblMessage.Text = "";
            if (!IsPostBack)
            {
                BindGrid();
            }
        }



        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Management/DailyAttendanceReport.aspx");
        }


        #region RadGrid1

        #region LoadGrid
        private void BindGrid()
        {
             string sqlquery ="";
            DataSet ds = new DataSet();
            if (comp_id == 1)
            {
             sqlquery  = "select  SP.ID,Sub_Project_Name,'' as Total,''as Remark,'' as Shift,''as PIC from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID where Sp.Active=1";
            }
            else
            {
              sqlquery = "select  SP.ID,Sub_Project_Name,'' as Total,''as Remark,'' as Shift,''as PIC from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID where Sp.Active=1 AND Company_ID='" + comp_id + "'";
            }
            
              
            
            ds = GetDataSet(sqlquery);

            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }
        #endregion

        #region Events
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
            {
                GridCommandItem item = e.Item as GridCommandItem;
                if (item != null)
                {
                  Button  btn = item.FindControl("btnsubmit") as Button;
                    btn.Attributes.Add("onclick", "javascript:return validateform();");
                    //lblMessage.Visible = true;
                }
            }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                if (e.Item is GridCommandItem)
                {
                    GridCommandItem commandItem = (GridCommandItem)e.Item;
                    ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                  
                    if (e.CommandName == "UpdateAll")
                    {
                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    string id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                              
                                    TextBox txtTotal = (TextBox)dataItem.FindControl("txtTotal");
                                    TextBox txtRemark = (TextBox)dataItem.FindControl("txtRemark");

                                    DropDownList drpShift = (DropDownList)dataItem.FindControl("drpShift");
                                    TextBox txtPIC = (TextBox)dataItem.FindControl("txtPIC");

                                    int Total = Utility.ToInteger(txtTotal.Text);


                                    DateTime dt = new DateTime();
                                    dt = Convert.ToDateTime(RadDatePicker1.SelectedDate);


                                    //check if data exist already for the date
                                    string SQLExist = "select * from DailyAttendance where Date=CONVERT(DATETIME, '" + RadDatePicker1.SelectedDate.Value + "' , 103)  and ProjectId='" + id + "'";
                                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLExist, null);
                                    if (!dr.HasRows)
                                    {
                                        sSQL = sSQL + "INSERT INTO [dbo].[DailyAttendance]([ProjectId],[company_ID],[Total],[Date],[Remark],Shift,PIC) VALUES (" + id + "," + comp_id + ",'" + Total + "','" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "','" + txtRemark.Text.ToString() + "','" + drpShift.SelectedValue + "','" + txtPIC.Text.ToString() + "');";
                                        lblMessage.Text = "Inserted Sucessfully";
                                    }
                                    else
                                    {
                                        sSQL = sSQL + "UPDATE [dbo].[DailyAttendance]   SET [Total] = " + Total + ",[Remark] = '" + txtRemark.Text.ToString() + "',Shift='" + drpShift.SelectedValue + "',PIC='" + txtPIC.Text.ToString() + "' WHERE [Date] = '" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "' and [company_ID]='" + comp_id + "' and [ProjectId] = '" + id + "';";
                                        lblMessage.Text = "Updated Sucessfully";
                                    }
                                    
                                    
                                }
                            }
                        }



                        try
                        {
                            if (sSQL != "")
                                DataAccess.ExecuteStoreProc(sSQL);
                        }
                        catch (Exception msg)
                        {
                            lblMessage.Text = msg.Message.ToString();
                        }


                    }
                    ((Button)commandItem.FindControl("btnsubmit")).Enabled = true;

                }
            }
        #endregion

        #endregion


        #region Helper
             private static DataSet GetDataSet(string query)
            {
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, query, null);
                return ds;
            }
            #endregion


        }
}
