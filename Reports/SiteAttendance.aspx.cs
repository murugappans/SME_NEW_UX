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

namespace SMEPayroll.Reports
{
    public partial class SiteAttendance : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);

            if (!IsPostBack)
            {
                LoadGrid();

                DateTime dt = DateTime.Now;
                cmbYear.SelectedValue = dt.Year.ToString();
                DropdownBind();
            }

            

        }

        public  string GetMonth()
        {
            string month = Convert.ToString(Request.QueryString["month"].ToString());
            return month;
        }

        //http://www.dotnetfunda.com/articles/article1649-how-to-work-with-the-nested-gridview-a-gridview-inside-another-gridview-a.aspx
       protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                GridView g2 = (GridView)e.Row.FindControl("GridView2");
                DataSet ds = new DataSet();

                string SubprojectId = Convert.ToString(GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                string sSQL = "sp_SiteAttendeanceReport"; 
                SqlParameter[] parms = new SqlParameter[2];
                //parms[0] = new SqlParameter("@ROWID", Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                parms[0] = new SqlParameter("@ROWID", Convert.ToInt32(Request.QueryString["RowId"].ToString()));
                parms[1] = new SqlParameter("@SubProjectId", SubprojectId);
                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                g2.DataSource = ds;
                g2.DataBind();
                g2.Visible = true;
              
            }
        }

     
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            //changing the header value
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int k = 4; k < e.Row.Cells.Count; k++)
                {
                    if (e.Row.Cells[k].Text.ToString() != " ")
                    {
                        string valdate = GetFormateDate(e.Row.Cells[k].Text.ToString());
                        e.Row.Cells[k].Text = Convert.ToString(valdate);
                        e.Row.Cells[k].Width = Unit.Pixel(70);
                        e.Row.Cells[k].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[k].Wrap = false;
                   
                    }
                }

             
            }
           

            //changing the cell value
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int j = 4; j < e.Row.Cells.Count; j++)
                {
                    if (e.Row.Cells[j].Text.ToString() !=" ")
                    {
                        string val = GetSubprojectID(e.Row.Cells[j].Text.ToString());
                        e.Row.Cells[j].Text = val;
                        e.Row.Cells[j].Width = Unit.Pixel(50);
                        e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[j].Wrap = false;

                    }
                }

            }

        }

        string Formatedate,day;
        private string GetFormateDate(string InputDate)
        {
            string sqlqry = "select  LEFT(datename (dw,'" + InputDate + "'),3) as NameOfDay,DAY('" + InputDate + "') as Day ";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlqry, null);
            while (dr.Read())
            {
                Formatedate = Utility.ToString(dr.GetValue(0));
                day = Utility.ToString(dr.GetValue(1));
            }

            return Formatedate + " <br /> [ " + day + " ]";
        }

        string subproj;
        private string GetSubprojectID(string Subprojectid)
        {
            if (Subprojectid != "&nbsp;")
            {
                string sqlqry = "select Sub_Project_ID from SubProject where ID='" + Subprojectid + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlqry, null);
                while (dr.Read())
                {
                    subproj = Utility.ToString(dr.GetValue(0));
                }

                return subproj;
            }
            else
            {
                return "";
            }
            
        }
       

        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            string sql = "";
            if (compID == 1)
            {
                sql = "select SP.ID as ID,Sub_Project_Name from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID";
            }
            else
            {
                  sql = "select SP.ID as ID,Sub_Project_Name from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID where Company_ID='" + compID + "' ";
            
            }
                ds = getDataSet(sql);

            GridView1.DataSource = ds;
            GridView1.DataBind();

        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            DropdownBind();
        }

        private void DropdownBind()
        {
            DataSet ds_month = new DataSet();
            ds_month = getDataSet("select ROWID,[MonthName] from PayrollMonthlyDetail where Year='" + cmbYear.SelectedValue + "' and  paytype='2'");
            cmbMonth.DataSource = ds_month.Tables[0];
            cmbMonth.DataTextField = ds_month.Tables[0].Columns["MonthName"].ColumnName.ToString();
            cmbMonth.DataValueField = ds_month.Tables[0].Columns["ROWID"].ColumnName.ToString();
            cmbMonth.DataBind();
            cmbMonth.Items.Insert(0, "-select-");
        }


        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            LoadGrid();
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {
            GridViewExportUtil.Export("Customers.xls", this.GridView1);
        }

        public class GridViewExportUtil
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="fileName"></param>
            /// <param name="gv"></param>
            public static void Export(string fileName, GridView gv)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader(
                    "content-disposition", string.Format("attachment; filename={0}", fileName));
                HttpContext.Current.Response.ContentType = "application/ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        //  Create a form to contain the grid
                        Table table = new Table();

                        //  add the header row to the table
                        if (gv.HeaderRow != null)
                        {
                            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                            table.Rows.Add(gv.HeaderRow);
                        }

                        //  add each of the data rows to the table
                        foreach (GridViewRow row in gv.Rows)
                        {
                            GridViewExportUtil.PrepareControlForExport(row);
                            table.Rows.Add(row);
                        }

                        //  add the footer row to the table
                        if (gv.FooterRow != null)
                        {
                            GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                            table.Rows.Add(gv.FooterRow);
                        }

                        //  render the table into the htmlwriter
                        table.RenderControl(htw);

                        //  render the htmlwriter into the response
                        HttpContext.Current.Response.Write(sw.ToString());
                        HttpContext.Current.Response.End();
                    }
                }
            }

            /// <summary>
            /// Replace any of the contained controls with literals
            /// </summary>
            /// <param name="control"></param>
            private static void PrepareControlForExport(Control control)
            {
                for (int i = 0; i < control.Controls.Count; i++)
                {
                    Control current = control.Controls[i];
                    if (current is LinkButton)
                    {
                        control.Controls.Remove(current);
                        control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                    }
                    else if (current is ImageButton)
                    {
                        control.Controls.Remove(current);
                        control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                    }
                    else if (current is HyperLink)
                    {
                        control.Controls.Remove(current);
                        control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                    }
                    else if (current is DropDownList)
                    {
                        control.Controls.Remove(current);
                        control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                    }
                    else if (current is CheckBox)
                    {
                        control.Controls.Remove(current);
                        control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                    }

                    if (current.HasControls())
                    {
                        GridViewExportUtil.PrepareControlForExport(current);
                    }
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
           
        }

    }
}
