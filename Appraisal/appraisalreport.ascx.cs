using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Appraisal
{
    public partial class appraisalreport : System.Web.UI.UserControl
    {
        public string _empId { get; set; }
        public string _empname { get; set; }
        public string _Fromdate { get; set; }
        public string _Todate { get; set; }
        private string AppraisalId = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if(_empname != "")
            {
                lblHeading.Text = "Performance Report of " + _empname;
                if (_empId != null)
                {
                    try
                    {
                        string Ssql = "select avg(Performance) Average,count(*) total,sum(case when Status = 'Pending' then 1 else 0 end) PendingObjectives, sum(case when Status = 'Complete' then 1 else 0 end) CompleteObjectives,";
                        Ssql += "sum(case when Status = 'Incomplete' then 1 else 0 end) IncompleteObjectives,sum(case when Performance = 5 then 1 else 0 end) ExcellentCount,sum(case when Performance = 4 then 1 else 0 end) VeryGoodCount,";
                        Ssql += "sum(case when Performance = 3 then 1 else 0 end) GoodCount,sum(case when Performance = 2 then 1 else 0 end) SatisfactoryCount,sum(case when Performance = 1 then 1 else 0 end) PoorCount from emp_Objectives";
                        Ssql += " where EmpId =" + _empId + " and FromDate >='" + _Fromdate + "' and FromDate <='" + _Todate + "'";
                        DataSet dsProject = new DataSet();
                        dsProject = DataAccess.FetchRS(CommandType.Text, Ssql, null);
                        if (dsProject.Tables.Count > 0 && dsProject.Tables[0].Rows.Count > 0)
                        {

                            lblExcellent.Text = dsProject.Tables[0].Rows[0]["ExcellentCount"].ToString();
                            lblgood.Text = dsProject.Tables[0].Rows[0]["GoodCount"].ToString();
                            lblpoor.Text = dsProject.Tables[0].Rows[0]["PoorCount"].ToString();
                            lblverygood.Text = dsProject.Tables[0].Rows[0]["VeryGoodCount"].ToString();
                            lblsatisfactory.Text = dsProject.Tables[0].Rows[0]["SatisfactoryCount"].ToString();
                            lblTotal_number_Of_Objectives.Text = dsProject.Tables[0].Rows[0]["total"].ToString();
                            lblObjectives_Completed.Text = dsProject.Tables[0].Rows[0]["CompleteObjectives"].ToString();
                            lblObjectives_not_Completed_on_time.Text = dsProject.Tables[0].Rows[0]["IncompleteObjectives"].ToString();
                            lblObjectives_Pending.Text = dsProject.Tables[0].Rows[0]["PendingObjectives"].ToString();
                            LblAverage.Text = dsProject.Tables[0].Rows[0]["Average"].ToString();
                            if(dsProject.Tables[0].Rows[0]["Average"].ToString() == "")
                            {
                                btnrecommend.Enabled = false;                                
                                Pnlmsg.Controls.Add(new LiteralControl("<font color = 'Red'>Appraisal can not be Recommended. Reason:  No Objective Performance for this employee has been Rated.. </font> "));
                            }

                        }
                        string today = string.Format("{0:yyyy-MM-dd}",DateTime.Today);
                        
                        Ssql = "Select * from Emp_Appraisal where duedate > '"+today+"' and EmpId="+_empId;
                        dsProject.Clear();
                        dsProject = DataAccess.FetchRS(CommandType.Text, Ssql, null);
                        if (dsProject.Tables.Count > 0 && dsProject.Tables[0].Rows.Count > 0)
                        {
                            lblDuedate.Text= String.Format("{0:dd MMM, yyyy}",dsProject.Tables[0].Rows[0]["DueDate"]);
                            AppraisalId=dsProject.Tables[0].Rows[0]["AppraisalId"].ToString();
                            if(dsProject.Tables[0].Rows[0]["IsRecommend"] != DBNull.Value && Convert.ToInt32(dsProject.Tables[0].Rows[0]["IsRecommend"]) == 1)
                            {
                                btnrecommend.Text = "Recommended Already";
                                btnrecommend.Enabled = false;
                            }
                        }


                        }
                    catch (Exception ex)
                    {

                        string ErrMsg = ex.Message;
                        if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                            ErrMsg = "<font color = 'Red'>Error</font>";
                        Pnlmsg.Controls.Add(new LiteralControl("<font color = 'Red'>Error. Reason:</font> " + ErrMsg));
                       
                    }


                }
            }
        }

        protected void btnrecommend_Click(object sender, EventArgs e)
        {
            try
            {
                string ssql = "Update Emp_Appraisal set IsRecommend=1 where AppraisalId=" + AppraisalId;
                int rp = DataAccess.ExecuteStoreProc(ssql);
                btnrecommend.Text = "Recommended Already";
                btnrecommend.Enabled = false;
            }
            catch (Exception ex)
            {

                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be updated </font>";
                Pnlmsg.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update record. Reason:</font> " + ErrMsg));

            }

        }
    }
}