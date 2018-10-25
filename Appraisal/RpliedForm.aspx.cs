using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class RpliedForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int count = 1;
            string strhtml = "";

            try
            {
                DataSet ds = new DataSet();
                DataSet dsapp = new DataSet();
                string sSQLCheck = "SELECT MAX(AppraisalId) AS LargestId FROM Emp_Appraisal where EmpId=318";
                string NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null).ToString();
                string ssql = "Select * from Objective_Setup where AppraisalId="+NeRecordId;
                string apssql = "Select Ea.*, e.emp_name as Employee,em.emp_name as Manager from Emp_Appraisal ea inner join employee e on e.emp_code = ea.EmpId inner join employee em on em.emp_code = ea.ManagerId where ea.EmpId=382 and ea.AppraisalId="+ NeRecordId; //To be changed with "Select Ea.*, e.emp_name as Employee,em.emp_name as Manager from Emp_Appraisal ea inner join employee e on e.emp_code = ea.EmpId inner join employee em on em.emp_code = ea.ManagerId where ea.EmpId=382 and status = 'Initiated'"
                dsapp = DataAccess.FetchRS(CommandType.Text, apssql, null);
                if (dsapp.Tables.Count > 0 && dsapp.Tables[0].Rows.Count > 0)
                {
                    lblAppEmpName.Text = dsapp.Tables[0].Rows[0]["Employee"].ToString();
                    lblAppTitle.Text = dsapp.Tables[0].Rows[0]["Title"].ToString();
                    Lblmanagwer.Text = dsapp.Tables[0].Rows[0]["Manager"].ToString();
                }

                ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
                if (ds.Tables.Count > 0 && ds.Tables[0].Columns.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        strhtml += "<div class='row'> <div class='col-sm-1'><div class='form-group'><label class=''>" + count + "</label></div>";
                        strhtml += "</div><div class='col-sm-5'><div class='form-group'><label class=''>" + dr["Title"] + "</label>";
                        strhtml += "</div></div><div class='col-sm-2'><div class='form-group'>";
                        switch (dr["RatingSystem"].ToString())
                        {
                            case "Rating5":
                                strhtml += "<label class=''> "+ lblAppEmpName.Text + " has rated (her/him)self " + dr["EmployeeReply"] + "</label>";
                                
                                break;
                            case "YN":
                                strhtml += "<label class=''>"+ lblAppEmpName.Text + " has replied " + dr["EmployeeReply"] + "</label>";
                                break;
                            case "Description":
                                strhtml += "<label class=''>" + dr["EmployeeReply"] + "</label>";
                                break;
                            case "Percentage":
                                strhtml += "<label class=''>" + lblAppEmpName.Text + " has replied " + dr["EmployeeReply"] + "%</label>";
                                break;
                            default:
                                break;
                        }

                        strhtml += "</div></div><div class='col-sm-4'> <label class=''>" + dr["Employee Remark"] + "</label> </div></div>";
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
