using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataStreams.Csv;
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Itenso.TimePeriod;
using System.Threading;

namespace SMEPayroll.TimeSheet
{
    public partial class upload_edit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSub_Click(object sender, EventArgs e)
        {
            string refid = Session["refid"].ToString();
            string ecode = Session["eid"].ToString();
            string compid = Session["Compid"].ToString();
            string objFileName = "";


            DateTime d;
            long filesize = FileUpload1.PostedFile.ContentLength;
            string uploadpath = "../" + "Documents" + "/" + compid + "/" + ecode + "/tsfile/";
            if (Strings.Right(FileUpload1.FileName.ToString(), 3) != "pdf")
            {
                return;
            }


            if (filesize > 1048576)
            {

                return;
            }
            if (FileUpload1.HasFile)
            {

                if (Directory.Exists(Server.MapPath(uploadpath)))
                {




                    objFileName = Server.MapPath(uploadpath) + "/" + refid + ".pdf";
                    FileUpload1.SaveAs(objFileName);
                  //  LinkButton1.Text = refid + ".pdf";
                  //  LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + ecode + "/tsfile/" + refid + ".pdf";

                }
                else
                {

                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    objFileName = Server.MapPath(uploadpath) + "/" + refid + ".pdf";
                    FileUpload1.SaveAs(objFileName);
                   // LinkButton1.Text = refid + ".pdf";
                  //  LinkButton1.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + ecode + "/tsfile/" + refid + ".pdf";
                }
                string str = "update TimeSheetMangment set FileName='" + refid + ".pdf' where RefId=" + refid;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str, null);


            }
            else
            {
                //strMessage = "Please Select File to Upload";
                // lblMsg.Text = "Please Select File to Upload";

            }
            Response.Redirect("../TimeSheet/TimesheetUpload.aspx");
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../TimeSheet/TimesheetUpload.aspx");

        }

    }
}