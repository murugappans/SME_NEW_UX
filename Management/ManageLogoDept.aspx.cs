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
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class ManageLogoDept : System.Web.UI.Page
    {
        int compid;
        string strMessage = "";
        # region Style
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        
        # endregion Style

        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.ManageLogoDept_PreRender);
        }
        private void ManageLogoDept_PreRender(object sender, EventArgs e)
        {

            //if (strMessage.Length > 0)
            //{
            //    ShowMessageBox(strMessage);
            //    strMessage = "";
            //}
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




        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            ViewState["actionMessage"] = "";

            if (!IsPostBack)
            {
                DataSet ds_department = new DataSet();
                ds_department = getDataSet("select ID , DeptName from department where Company_id=" + compid + " ORDER BY DeptName");
                drpDept.DataSource = ds_department.Tables[0];
                drpDept.DataTextField = ds_department.Tables[0].Columns["DeptName"].ColumnName.ToString();
                drpDept.DataValueField = ds_department.Tables[0].Columns["ID"].ColumnName.ToString();
                drpDept.DataBind();
                drpDept.Items.Insert(0, "-select-");
            }
        }

        protected void drpDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image1.Src = "";
            string uploadpath = "../" + "Documents" + "/" + "Logo";
            if (drpDept.SelectedItem.Value != "-select-")
            {
                DataSet ds = new DataSet();
                ds = getDataSet("Select reverse(left(reverse(FileName), charindex('/', reverse(FileName)))) FileName,ID  From (	Select ID, Replace(Filename,'\\','/') FileName From Department) D where ID=" + drpDept.SelectedItem.Value);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString().Trim().Length > 0)
                    {
                        Image1.Src = uploadpath+ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {
            string uploadpath = "../" + "Documents" + "/" + "Logo/";
            string objFileName = "";
            if (drpDept.SelectedItem.Text == "-select-")
            {
                strMessage = "Please Select Deparment..";
                return;
            }

            if (file1.UploadedFiles.Count != 0)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName()))
                    {
                        objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                        file1.UploadedFiles[0].SaveAs(objFileName);
                        objFileName = file1.UploadedFiles[0].GetName();

                        string ssqlb = "Update Department Set FileName='" + Server.MapPath(uploadpath) + objFileName + "' Where ID=" + drpDept.SelectedItem.Value;
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        Image1.Src = uploadpath + objFileName;
                        ViewState["actionMessage"] = "Success|Logo for department uploaded.";

                    }
                    else
                    {
                        objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                        file1.UploadedFiles[0].SaveAs(objFileName);
                        objFileName = file1.UploadedFiles[0].GetName();

                        string ssqlb = "Update Department Set FileName='" + Server.MapPath(uploadpath) + objFileName + "' Where ID=" + drpDept.SelectedItem.Value;
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        Image1.Src = uploadpath+ objFileName;
                        ViewState["actionMessage"] = "Success|Logo for department uploaded.";
                    }
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                    file1.UploadedFiles[0].SaveAs(objFileName);
                    objFileName = file1.UploadedFiles[0].GetName();
                    string ssqlb = "Update Department Set FileName='" + Server.MapPath(uploadpath) + objFileName + "' Where ID=" + drpDept.SelectedItem.Value;
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    Image1.Src = uploadpath+objFileName;
                    ViewState["actionMessage"] = "Success|Logo for department uploaded.";
                }
            }
            else
            {
                strMessage = "Please Select Logo to Upload";
               
            }
        }
    }
}

