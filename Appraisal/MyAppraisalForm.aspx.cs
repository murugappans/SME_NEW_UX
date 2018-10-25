using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
   public class Appraisaldata{
        public string remark { get; set; }
        public string Reply { get; set; }
        public int objID { get; set; }

    }
    public partial class MyAppraisalForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                            strhtml += "<div class='row'> <div class='col-sm-1 sno'><div class='form-group'><label class=''>" + count + "</label></div>";
                            strhtml += "</div><div class='col-sm-6'><div class='form-group'><label class=''>" + dr["Title"] + "</label>";
                            strhtml += "</div></div><div class='col-sm-2'><div class='form-group'>";
                            switch (dr["RatingSystem"].ToString())
                            {
                                case "Rating5":
                                    strhtml += "<div class='col-sm-1'>5</div><div class='col-sm-1 rating-section'><input id='" + dr["ObjsetupID"] + "' class='form-control input-sm'  style='width:15px' value='5' Name='objRating" + dr["ObjsetupID"] + "'  type='Radio'></input></div>";
                                    strhtml += "<div class='col-sm-1'>4</div><div class='col-sm-1 rating-section'><input  class='form-control input-sm' value='4' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px'  type='Radio'></input></div>";
                                    strhtml += "<div class='col-sm-1'>3</div><div class='col-sm-1 rating-section'><input  class='form-control input-sm' value='3' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                    strhtml += "<div class='col-sm-1'>2</div><div class='col-sm-1 rating-section'><input class='form-control input-sm' value='2' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                    strhtml += "<div class='col-sm-1'>1</div><div class='col-sm-1 rating-section'><input  class='form-control input-sm' value='1' Name='objRating" + dr["ObjsetupID"] + "'  style='width:15px' type='Radio'></input></div>";
                                    break;
                                case "YN":
                                    strhtml += "<div class='col-sm-1 cls-yes'>YES</div><div class='col-sm-4 rating-section'><input style='width:15px' Name='Obj" + dr["ObjsetupID"] + "' class='form-control input-sm' type='Radio' value='yes' id='" + dr["ObjsetupID"] + "'></input></div>";
                                    strhtml += "<div class='col-sm-1 cls-no'>NO</div><div class='col-sm-4 rating-section'><input style='width:15px' class='form-control input-sm' Value='No' checked='true' type='Radio' Name='Obj" + dr["ObjsetupID"] + "'></input></div>";
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
                    dvViewForm.InnerHtml += strhtml;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        public void change()
        {
            dvViewForm.InnerHtml = "";
        }
        [System.Web.Services.WebMethod]
        public static int FeedbackSend(Appraisaldata[] objdata)
        {
            string ssql="";
            foreach (Appraisaldata item in objdata)
            {

                ssql += "UPDATE [dbo].[Objective_Setup] SET [EmployeeReply] ='" + item.Reply + "' ,[Employee Remark] = '" + item.remark + "' WHERE ObjsetupID =" + item.objID;

            }

            int rly = DataAccess.ExecuteStoreProc(ssql);
            if(rly > 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}