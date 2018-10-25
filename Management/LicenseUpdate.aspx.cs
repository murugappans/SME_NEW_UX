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

using Ionic.Zip;
using System.Data.SqlClient;

namespace SMEPayroll.Management
{
    public partial class LicenseUpdate : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;



        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ShowLicense();

        }


        string filePath, filePath_xml = "";
        public void ShowLicense()
        {



            string zipFileName = "";
            string TargetDirectory = Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());
            //string TargetDirectory = @"C:\CERTIFICATE";
            foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory))
            {
                zipFileName = fileName;
            }
            if (System.IO.File.Exists(zipFileName))
            {
                using (ZipFile zip = ZipFile.Read(zipFileName))
                {
                    zip.Password = "!Secret1";
                    zip.ExtractAll(TargetDirectory, ExtractExistingFileAction.OverwriteSilently);
                }

                foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory + @"\CERTIFICATE"))
                {
                    if (fileName.Substring(fileName.Length - 3, 3) == "txt")
                    {
                        filePath = fileName;
                    }
                    if (fileName.Substring(fileName.Length - 3, 3) == "xml")
                    {
                        filePath_xml = fileName;
                    }


                }

                //...Read Data From TextFile and show data in data grid for Certification...
                DataSet Certificationinfo = Utility.GetDataSetFromTextFile(filePath);
                Session["Certificationinfo"] = Certificationinfo;


                DataSet Rightsinfo = GetxmlData(filePath_xml);

                #region Delete userRights table and load from xml
                string Rightsid = "0,";
                foreach (DataRow row in Rightsinfo.Tables[0].Rows)
                {
                    Rightsid = Rightsid + "" + row["Rightsid"] + ",";
                }

               // Response.Write(Rightsid.Substring(0, Rightsid.Length - 1));

                //Delete Previous Data  
                DataAccess.ExecuteStoreProc("Delete from UserRights");

                string sql = "insert into UserRights SELECT [Rightid],[RightName],[RightCategory],[RightSubCategory],[HeaderID],[RightOrder],[DisplayID],[ParentId],[GParentid],[ActualRightName],[Description],[PRODUCT]FROM [dbo].[UserRights_Temp]where Rightid in (" + Rightsid.Substring(0, Rightsid.Length - 1) + ")";
                int retVal = DataAccess.ExecuteStoreProc(sql);

                lblMsg.Text = "License updated Sucessfully..";
                #endregion
                //Delete Files Once Data Gets in Session
                try
                {
                    foreach (string dirName in System.IO.Directory.GetDirectories(TargetDirectory))
                    {
                        if (dirName != zipFileName)
                        {
                            System.IO.Directory.Delete(dirName, true);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }



        }

        private DataSet GetxmlData(string filePath_xml)
        {
            DataSet xmlData = new DataSet();
            xmlData.ReadXml(filePath_xml);

            return xmlData;
        }
    }
}
