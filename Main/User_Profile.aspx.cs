using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEPayroll;

namespace SMEPayroll.Main
{
    public partial class User_Profile : System.Web.UI.Page
    {
        int compid = 0;
        string emp_code = "";
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
            emp_code = Session["EmpCode"].ToString();
            if (!IsPostBack)
            {
                string str = "select emp_name,emp_lname,block_no,street_name,level_no,unit_no,postal_code,foreignaddress1,foreignaddress2,foreignpostalcode,eme_cont_per,eme_cont_per_rel,eme_cont_per_ph1,eme_cont_per_ph2,eme_cont_per_add,eme_cont_per_rem,marital_status,phone,hand_phone,email from employee where emp_code=" + emp_code;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str, null);
                string strdesignation = "select d.Designation from employee e inner join designation d on d.id = e.desig_id where emp_code=" + emp_code;
                SqlDataReader drdesignation = DataAccess.ExecuteReader(CommandType.Text, strdesignation, null);
                if (dr.Read())
                {
                    string maritalstatus = dr["marital_status"].ToString();
                    empfirstname.Text = dr["emp_name"].ToString();
                    emplastname.Text = dr["emp_lname"].ToString();
                    txtblock.Text = dr["block_no"].ToString();
                    txtstreet.Text = dr["street_name"].ToString();
                    txtlevel.Text = dr["level_no"].ToString();
                    txtunit.Text = dr["unit_no"].ToString();
                    txtpin.Text = dr["postal_code"].ToString();
                    txtPhone.Text = dr["phone"].ToString();
                    txtHandPhone.Text = dr["hand_phone"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    cmbMaritalStatus.Items.FindByValue(maritalstatus).Selected = true;
                    txtfadd1.Text = dr["foreignaddress1"].ToString();
                    txtfadd2.Text = dr["foreignaddress2"].ToString();
                    txtEmeConPer.Text = dr["eme_cont_per"].ToString();
                    txtEmeConPerAdd.Text = dr["eme_cont_per_add"].ToString();
                    txtEmeConPerPh1.Text = dr["eme_cont_per_ph1"].ToString();
                    txtEmeConPerPh2.Text = dr["eme_cont_per_ph2"].ToString();
                    txtEmeConPerRel.Text = dr["eme_cont_per_rel"].ToString();
                    txtEmeConPerRem.Text = dr["eme_cont_per_rem"].ToString();
                    string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture";
                    if (Directory.Exists(Server.MapPath(uploadpath)))
                    {
                        string[] files = Directory.GetFiles(Server.MapPath(uploadpath), "*.*");
                        for (int i = 0; i < files.Length; i++)
                            files[i] = Path.GetFileName(files[i]);
                        if (File.Exists(Server.MapPath("../" + "Documents" + "/" + compid.ToString() + "/" + emp_code + "/" + "Picture/" + files[0])))
                        {
                            UploadedImage.ImageUrl = "../" + "Documents" + "/" + compid.ToString() + "/" + emp_code + "/" + "Picture/" + files[0];
                        }
                        else
                        {
                            UploadedImage.ImageUrl = "../Frames/Images/Employee/employee.png";
                        }
                    }
                    else
                    {
                        UploadedImage.ImageUrl = "../Frames/Images/Employee/employee.png";
                    }
                    lblEmployeeName.Text = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
                }
                if (drdesignation.Read())
                {
                    LblEmployeeDesignation.Text = drdesignation["Designation"].ToString();
                }
            }
        }
        protected void btnSavePersonalInfo_click(object sender, EventArgs e)
        {
            string emp_first_name = empfirstname.Text;
            string emp_last_name = emplastname.Text;
            string MaritalStatus = cmbMaritalStatus.SelectedValue;
            string txt_block = txtblock.Text;
            string txt_street = txtstreet.Text;
            string txt_level = txtlevel.Text;
            string txt_unit = txtunit.Text;
            string txt_pin = txtpin.Text;
            string txt_Phone = txtPhone.Text;
            string txt_HandPhone = txtHandPhone.Text;
            string txt_Email = txtEmail.Text;
            string txt_fadd1 = txtfadd1.Text;
            string txt_fadd2 = txtfadd2.Text;
            string txt_EmeConPer = txtEmeConPer.Text;
            string txt_EmeConPerAdd = txtEmeConPerAdd.Text;
            string txt_EmeConPerPh1 = txtEmeConPerPh1.Text;
            string txt_EmeConPerPh2 = txtEmeConPerPh2.Text;
            string txt_EmeConPerRel = txtEmeConPerRel.Text;
            string txt_EmeConPerRem = txtEmeConPerRem.Text;
           // string ssl = "Update employee set eme_cont_per='" + txt_EmeConPer + "',eme_cont_per_add='" + txt_EmeConPerAdd + "',eme_cont_per_ph1='" + txt_EmeConPerPh1 + "',eme_cont_per_ph2='" + txt_EmeConPerPh2 + "',eme_cont_per_rel='" + txt_EmeConPerRel + "',eme_cont_per_rem='" + txt_EmeConPerRem + "' where emp_code =" + emp_code;
            string ssl = "Update employee set foreignaddress1='" + txt_fadd1 + "',foreignaddress2='" + txt_fadd2 + "',emp_name='" + emp_first_name + "',emp_lname='" + emp_last_name + "',block_no='" + txt_block + "',street_name='" + txt_street + "',level_no='" + txt_level + "',unit_no='" + txt_unit + "',postal_code='" + txt_pin + "',phone='" + txt_Phone + "',hand_phone='" + txt_HandPhone + "',email='" + txt_Email + "',marital_status='" + MaritalStatus + "',eme_cont_per='" + txt_EmeConPer + "',eme_cont_per_add='" + txt_EmeConPerAdd + "',eme_cont_per_ph1='" + txt_EmeConPerPh1 + "',eme_cont_per_ph2='" + txt_EmeConPerPh2 + "',eme_cont_per_rel='" + txt_EmeConPerRel + "',eme_cont_per_rem='" + txt_EmeConPerRem + "' where emp_code =" + emp_code;
            DataAccess.ExecuteStoreProc(ssl);
           
           // string ssl = "Update employee set emp_name='" + emp_first_name + "',emp_lname='" + emp_last_name + "',block_no='" + txt_block + "',street_name='" + txt_street + "',level_no='" + txt_level + "',unit_no='" + txt_unit + "',postal_code='" + txt_pin + "',phone='" + txt_Phone + "',hand_phone='" + txt_HandPhone + "',email='" + txt_Email + "',marital_status='" + MaritalStatus + "' where emp_code =" + emp_code;
            //DataAccess.ExecuteStoreProc(ssl);
            _actionMessage = "Success|Personal Info Updated Successfully.";
            ViewState["actionMessage"] = _actionMessage;
            lblEmployeeName.Text = empfirstname.Text + " " + emplastname.Text;
            //Response.Redirect("../Main/User_Profile.aspx");

        }
        
        protected void btnSaveProfilePicture_click(object sender, EventArgs e)
        {
            string objFileName = "";
            string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture";
            if (FileUpload1.HasFile)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + FileUpload1.FileName))
                    {
                        _actionMessage = "Warning| File Already Exist with this name";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath(uploadpath));
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        objFileName = Server.MapPath(uploadpath) + "/" + FileUpload1.FileName;
                        FileUpload1.SaveAs(objFileName);
                        string imagepath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture/" + FileUpload1.FileName;
                        UploadedImage.ImageUrl = imagepath;
                        Response.Redirect("../Main/User_Profile.aspx");
                    }
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    objFileName = Server.MapPath(uploadpath) + "/" + FileUpload1.FileName;
                    FileUpload1.SaveAs(objFileName);
                    string imagepath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture/" + FileUpload1.FileName;
                    UploadedImage.ImageUrl = imagepath;
                    Response.Redirect("../Main/User_Profile.aspx");
                }
            }
        }
        protected void btncancel_click(object sender, EventArgs e)
        {
            string str = "select emp_name,emp_lname,block_no,street_name,level_no,unit_no,postal_code,foreignaddress1,foreignaddress2,foreignpostalcode,eme_cont_per,eme_cont_per_rel,eme_cont_per_ph1,eme_cont_per_ph2,eme_cont_per_add,eme_cont_per_rem,marital_status,phone,hand_phone,email from employee where emp_code=" + emp_code;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str, null);
            string strdesignation = "select d.Designation from employee e inner join designation d on d.id = e.desig_id where emp_code=" + emp_code;
            SqlDataReader drdesignation = DataAccess.ExecuteReader(CommandType.Text, strdesignation, null);
            if (dr.Read())
            {
                string maritalstatus = dr["marital_status"].ToString();
                empfirstname.Text = dr["emp_name"].ToString();
                emplastname.Text = dr["emp_lname"].ToString();
                txtblock.Text = dr["block_no"].ToString();
                txtstreet.Text = dr["street_name"].ToString();
                txtlevel.Text = dr["level_no"].ToString();
                txtunit.Text = dr["unit_no"].ToString();
                txtpin.Text = dr["postal_code"].ToString();
                txtPhone.Text = dr["phone"].ToString();
                txtHandPhone.Text = dr["hand_phone"].ToString();
                txtEmail.Text = dr["email"].ToString();
                cmbMaritalStatus.Items.FindByValue(maritalstatus).Selected = true;
                txtfadd1.Text = dr["foreignaddress1"].ToString();
                txtfadd2.Text = dr["foreignaddress2"].ToString();
                txtEmeConPer.Text = dr["eme_cont_per"].ToString();
                txtEmeConPerAdd.Text = dr["eme_cont_per_add"].ToString();
                txtEmeConPerPh1.Text = dr["eme_cont_per_ph1"].ToString();
                txtEmeConPerPh2.Text = dr["eme_cont_per_ph2"].ToString();
                txtEmeConPerRel.Text = dr["eme_cont_per_rel"].ToString();
                txtEmeConPerRem.Text = dr["eme_cont_per_rem"].ToString();
                string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture";
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    string[] files = Directory.GetFiles(Server.MapPath(uploadpath), "*.*");
                    for (int i = 0; i < files.Length; i++)
                        files[i] = Path.GetFileName(files[i]);
                    if (File.Exists(Server.MapPath("../" + "Documents" + "/" + compid.ToString() + "/" + emp_code + "/" + "Picture/" + files[0])))
                    {
                        UploadedImage.ImageUrl = "../" + "Documents" + "/" + compid.ToString() + "/" + emp_code + "/" + "Picture/" + files[0];
                    }
                    else
                    {
                        UploadedImage.ImageUrl = "../Frames/Images/Employee/employee.png";
                    }
                }
                else
                {
                    UploadedImage.ImageUrl = "../Frames/Images/Employee/employee.png";
                }
                lblEmployeeName.Text = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
            }
            if (drdesignation.Read())
            {
                LblEmployeeDesignation.Text = drdesignation["Designation"].ToString();
            }

        }
    }
}