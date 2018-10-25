using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class AddAppraisal : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string varEmpCode = "";
        string strRepType;
        string sgroupname = "";
        protected string comp_id = "";
        static string  NeRecordId;

        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Session["Compid"] != null ? Session["Compid"].ToString() : "";
            varEmpCode = Session["EmpCode"] != null ? Session["EmpCode"].ToString() : "";
            if (!IsPostBack)
            {
                try
                {
                    DataSet ds = new DataSet();
                    string ssql = "Select emp_code, emp_name+ ' '  + emp_lname as EmpName, emp_name+ '_'  + emp_lname as EmpNamevalue from employee WHERE company_id =" + comp_id + " and termination_date is null";
                    ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Columns.Count > 0)
                    {
                       // drEmployeecode.DataSource = ds;
                        drempName.DataSource = ds;
                       // drEmployeecode.DataTextField = "emp_code";
                      //  drEmployeecode.DataValueField = "emp_code";
                        drempName.DataTextField = "EmpName";
                        drempName.DataValueField = "EmpNamevalue";
                       // drEmployeecode.DataBind();
                        drempName.DataBind();
                    }
                }
                catch (Exception ex)
                {

                    lblerror.Text = "Error :" + ex.Message;
                }
            }


        }

        protected void drempName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedvalue = drempName.SelectedValue.ToString();
            string empname = selectedvalue.Split('_')[0];
            string lname = selectedvalue.Split('_')[1];
            DataSet ds = new DataSet();
            string ssql = "Select emp_code,phone,hand_phone,email from employee WHERE company_id =" + comp_id + " and termination_date is null and emp_name='" + empname + "' and emp_lname='" + lname + "'";
            try
            {
                ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
                string emp_code = ds.Tables[0].Rows[0]["emp_code"].ToString();
               // drEmployeecode.SelectedValue = emp_code;
                txtempCode.Value = ds.Tables[0].Rows[0]["emp_code"].ToString();
                txtPhone.Value = ds.Tables[0].Rows[0]["phone"].ToString();
                txtHandPhone.Value = ds.Tables[0].Rows[0]["hand_phone"].ToString();
                txtEmail.Value = ds.Tables[0].Rows[0]["email"].ToString();
                //ssql = "Select * from emp_Objectives where EmpId =" + emp_code+ " and FromDate > '2017-12-1' and FromDate < '2018-12-1' ";
                //ds = DataAccess.FetchRS(CommandType.Text, ssql, null);


            }
            catch (Exception ex)
            {

                lblerror.Text = "Error :" + ex.Message;
            }

        }
        protected void btnQuickSave_Click(object sender, EventArgs e)
        {
            string strAppTitle = txtAppTitle.Value;
            string strDate = string.Format("{0:yyyy/MM/dd}", rdAppdate.SelectedDate);
            string strEmpcode = txtempCode.Value;
            string strtoday = string.Format("{0:yyyy/MM/dd H:mm}", DateTime.Now);
            string ssql = "INSERT INTO Emp_Appraisal ([EmpId] ,[ManagerId] ,[DueDate],[Title],[CreatedOn])";
            ssql += " VALUES("+strEmpcode+","+varEmpCode+",'"+ strDate + "','"+ strAppTitle + "','"+ strtoday + "')";
            int rval = DataAccess.ExecuteStoreProc(ssql);
           if(rval >= 1)
            {
                btnQuickSave.Enabled = false;
                lblerror.ForeColor = System.Drawing.Color.Green;
                lblerror.Text = "Success :  Appraisal has been Scheduled.. Proceed to Create Appraisl Form for Employee";
                tbsApp.SelectedIndex = 1;
                tbsAppraisal.SelectedIndex = 1;
                string sSQLCheck = "SELECT MAX(AppraisalId) AS LargestId FROM Emp_Appraisal";
                 NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null).ToString();
            }
        }

        [System.Web.Services.WebMethod]
        public static int ObjectiveSave( string objtitle , string drpval)
        {

            if (!NeRecordId.Equals(""))
            {
                
                string ssql = "INSERT INTO Objective_Setup ([AppraisalId],[Title],[RatingSystem])VALUES";
                ssql += "(" + NeRecordId + ",'" + objtitle + "','" + drpval + "')";
                int rval = DataAccess.ExecuteStoreProc(ssql);
                return 1;
            }
            return 0;
            //else
            //{
            //    lblerror.ForeColor = System.Drawing.Color.Red;
            //    lblerror.Text = "Please Add Appraisal First";
            //    return 0;
            //}



        }
        protected void drempName_DataBound(object sender, EventArgs e)
        {
            ListItem lst = new ListItem("-Select-", "0");

            drempName.Items.Insert(0, lst);
           // drEmployeecode.Items.Insert(0, lst);
        }

        protected void btnSaveObjevtives_Click(object sender, EventArgs e)
        {
            btnsaveobj.Enabled = false;
            btnSaveObjevtives.Enabled = false;
            lblerror.Text = "";
            tbsApp.SelectedIndex = 2;
            tbsAppraisal.SelectedIndex = 2;
            int count = 1;
            string Objtile, ratingsys, strhtml="";
            strhtml = "<div class='row'><div class='col-sm-1'><div class='form-group'><label>SNo.</label> </div></div><div class='col-sm-3'><div class='form-group'><label>Objective Title</label>";
            strhtml += "</div></div><div class='col-sm-3'><div class='form-group'><label>Rating and Remarks</label> </div></div></div>";
            try
            {
                DataSet ds = new DataSet();
                string ssql = "Select * from Objective_Setup where AppraisalId="+NeRecordId;
                lblAppEmpName.Text= drempName.SelectedValue.Replace('_',' ').ToString();
                lblAppTitle.Text = txtAppTitle.Value;
                ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
                if (ds.Tables.Count > 0 && ds.Tables[0].Columns.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                       
                        strhtml += "<div class='row'> <div class='col-sm-1'><div class='form-group'><label class=''>" + count + "</label></div>";
                        strhtml += "</div><div class='col-sm-3'><div class='form-group'><label class=''>" + dr["Title"] + "</label>";
                        strhtml += "</div></div><div class='col-sm-3'><div class='form-group'>";
                        switch (dr["RatingSystem"].ToString())
                        {
                            case "Rating5":
                                strhtml += "<div class='col-sm-1'>5</div><div class=col-sm-1><input id='" + dr["ObjsetupID"] + "' class='form-control input-sm'  style='width:15px' value='5' Name='objRating" + dr["ObjsetupID"] + "'  type='Radio'></input></div>";
                                strhtml += "<div class='col-sm-1'>4</div><div class=col-sm-1><input  class='form-control input-sm' value='4' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px'  type='Radio'></input></div>";
                                strhtml += "<div class='col-sm-1'>3</div><div class=col-sm-1><input  class='form-control input-sm' value='3' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                strhtml += "<div class='col-sm-1'>2</div><div class=col-sm-1><input class='form-control input-sm' value='2' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                strhtml += "<div class='col-sm-1'>1</div><div class=col-sm-1><input  class='form-control input-sm' value='1' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                break;
                            case "YN":
                                strhtml += "<div class='col-sm-1'>YES</div><div class=col-sm-4><input style='width:15px' Name='Obj" + dr["ObjsetupID"] + "' class='form-control input-sm' type='Radio' value='yes' id='" + dr["ObjsetupID"] + "'></input></div>";
                                strhtml += "<div class='col-sm-1'>NO</div><div class=col-sm-4><input style='width:15px' class='form-control input-sm' Value='No' checked='true' type='Radio' Name='Obj" + dr["ObjsetupID"] + "'></input></div>";
                                break;
                            case "Description":
                                strhtml += "<TextArea id='" + dr["ObjsetupID"] + "' class='form-control input-sm' ></TextArea>";
                                break;
                            case "Percentage":
                                strhtml += "<input id='" + dr["ObjsetupID"] + "'  class='form-control input-sm' onfocusout='numbertypefocuschange(this);' type='Number' min='1' max='100'></input>";
                                break;
                            default:
                                break;
                        }

                        strhtml += "</div></div><div class='col-sm-3'><input class='form-control input-sm clsRemark' id='rmrk" + dr["ObjsetupID"] + "' type='text'></input></div></div>";
                        count++;
                    } 
                  
           
                }
                dvViewForm.InnerHtml = strhtml;
            }
            catch (Exception)
            {

                throw;
            }
           
            
        }

        
    }
}