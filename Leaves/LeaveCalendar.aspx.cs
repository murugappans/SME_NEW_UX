using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace SMEPayroll.Leaves
{
    public partial class LeaveCalendar : Page
    {
        private const string ProviderSessionKey = "Telerik.Web.Examples.Scheduler.Outlook2007.DefaultCS";
        private Dictionary<int, string> checkBoxDepts;
        private Dictionary<int, string> checkBoxLeaves;
        DataSet leaveDs = new DataSet();
        DataSet leaveType = new DataSet();
        DataSet empLeaveType = new DataSet();
        string _actionMessage = "";
        string CompId = null;
        XmlSchedulerProvider Provider
        {
            get
            {
                XmlSchedulerProvider provider = (XmlSchedulerProvider)Session[ProviderSessionKey];
                if (Session[ProviderSessionKey] == null || !IsPostBack)
                {
                    provider = new XmlSchedulerProvider(Server.MapPath("~/Xml/Appointments_Outlook.xml"), false);
                    Session[ProviderSessionKey] = provider;
                }

                return provider;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (Session["Compid"] != null)
                CompId = Session["Compid"].ToString();
            else
                CompId = "1";
            string SQL = "SELECT DISTINCT DeptName,ID FROM dbo.DEPARTMENT WHERE Company_Id = " + CompId;
            leaveDs = DataAccess.FetchRS(CommandType.Text, SQL, null);

            SQL = "SELECT ID,Type,CODE FROM dbo.LEAVE_TYPES  ORDER BY ID";
            leaveType = DataAccess.FetchRS(CommandType.Text, SQL, null);

            addDynamicCheckBox(leaveDs, leaveType);
            SQL = " SELECT DISTINCT EMP_NAME,APPROVER,START_DATE,END_DATE,CASE  DATEDIFF ( dd , START_DATE , END_DATE )WHEN '0' THEN CAST('0.5' AS VARCHAR)ELSE CAST(DATEDIFF( dd , START_DATE , END_DATE )AS NVARCHAR)END As NO_OF_DAYS ,HAND_PHONE ,LEAVE_TYPE,TIMESESSION,TYPE,DEPTNAME,DEPT_ID FROM EMP_LEAVES INNER JOIN dbo.EMPLOYEE ON EMP_ID = EMP_CODE INNER JOIN LEAVE_TYPES ON lEAVE_TYPE = ID INNER JOIN department ON DEPT_ID = department.ID WHERE STATUS='APPROVED' AND  department.Company_Id = " + CompId;

            empLeaveType = DataAccess.FetchRS(CommandType.Text, SQL, null);
            if (!Page.IsPostBack)
            {
                overWriteOutlookXml();
                appendOutlookTemplateXml(leaveDs, leaveType);

                appendOutlookXml(empLeaveType);
            }
            RadScheduler1.Provider = Provider;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            checkBoxDepts = new Dictionary<int, string>();
            checkBoxLeaves = new Dictionary<int, string>();
            for (int deptNo = 0; deptNo < leaveDs.Tables[0].Rows.Count; deptNo++)
            {
                checkBoxDepts.Add(Convert.ToInt16(leaveDs.Tables[0].Rows[deptNo]["ID"].ToString()), "chk" + leaveDs.Tables[0].Rows[deptNo]["DeptName"].ToString());
            }
            for (int leaveNo = 0; leaveNo < leaveType.Tables[0].Rows.Count; leaveNo++)
            {
                checkBoxLeaves.Add(Convert.ToInt16(leaveType.Tables[0].Rows[leaveNo]["ID"].ToString()), "chk" + leaveType.Tables[0].Rows[leaveNo]["Type"].ToString());
            }
            if (!IsPostBack)
            {
                RadScheduler1.SelectedDate = System.DateTime.Today;

            }
        }

        protected void RadToolTipManager2_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            int aptId;
            Appointment apt;
            if (!int.TryParse(e.Value, out aptId))//The appoitnment is occurrence and FindByID expects a string  
                apt = RadScheduler1.Appointments.FindByID(e.Value);
            else //The appointment is not occurrence and FindByID expects an int  
                apt = RadScheduler1.Appointments.FindByID(aptId);

            LeaveEmp leaveToolTip = (LeaveEmp)LoadControl("LeaveEmp.ascx");
            leaveToolTip.TargetAppointment = apt;
            e.UpdatePanel.ContentTemplateContainer.Controls.Add(leaveToolTip);
           
        }

        private bool IsAppointmentRegisteredForTooltip(Appointment apt)
        {
            foreach (ToolTipTargetControl targetControl in RadToolTipManager2.TargetControls)
            {
                if (apt.DomElements.Contains(targetControl.TargetControlID))
                {
                    return true;
                }
            }

            return false;
        }

        protected void RadScheduler1_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            if (e.Appointment.Visible && !IsAppointmentRegisteredForTooltip(e.Appointment))
            {
                string id = e.Appointment.ID.ToString();

                foreach (string domElementID in e.Appointment.DomElements)
                {
                    RadToolTipManager2.TargetControls.Add(domElementID, id, true);
                }
            }
        }


        protected void CheckBoxes_CheckedChanged(object sender, EventArgs e)
        {
            RadScheduler1.Rebind();
        }
        private void appendOutlookXml(DataSet leaveDs)
        {
            DateTime leaveStartdate;
            DateTime leaveEnddate;
            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/Appointments_Outlook.xml"));
            for (int leaveNo = 0; leaveNo < leaveDs.Tables[0].Rows.Count; leaveNo++)
            {
                XmlNode section = document.CreateElement("Appointment");
                XmlNode key = document.CreateElement("ID");
                key.InnerText = Convert.ToString(leaveNo + 1);
                section.AppendChild(key);

                key = document.CreateElement("Subject");
                //key.InnerText = leaveDs.Tables[0].Rows[leaveNo]["EMP_NAME"].ToString() + "/ Leave On :" + leaveDs.Tables[0].Rows[leaveNo]["START_DATE"].ToString() + "/ No Of Days :" + leaveDs.Tables[0].Rows[leaveNo]["NO_OF_DAYS"].ToString() + "/ Approved By :" + leaveDs.Tables[0].Rows[leaveNo]["APPROVER"].ToString() + "/ Phone :" + leaveDs.Tables[0].Rows[leaveNo]["Hand_Phone"].ToString();
                key.InnerText = leaveDs.Tables[0].Rows[leaveNo]["EMP_NAME"].ToString();
                section.AppendChild(key);

                leaveStartdate = Convert.ToDateTime(leaveDs.Tables[0].Rows[leaveNo]["START_DATE"].ToString());
                leaveEnddate = Convert.ToDateTime(leaveDs.Tables[0].Rows[leaveNo]["End_DATE"].ToString());

                key = document.CreateElement("Start");
                key.InnerText = leaveStartdate.ToString("yyyy-MM-dd") + "T09:00Z";

                section.AppendChild(key);

                key = document.CreateElement("End");
                key.InnerText = leaveEnddate.ToString("yyyy-MM-dd") + "T20:00Z";

                section.AppendChild(key);

                key = document.CreateElement("RecurrenceRule");
                key.InnerText = "0";
                section.AppendChild(key);

                key = document.CreateElement("Resources");
                XmlElement xmlEleNew = document.CreateElement("Department");
                xmlEleNew.SetAttribute("Key", leaveDs.Tables[0].Rows[leaveNo]["DEPT_ID"].ToString());
                key.AppendChild(xmlEleNew);
                xmlEleNew = document.CreateElement("Leave");
                xmlEleNew.SetAttribute("Key", leaveDs.Tables[0].Rows[leaveNo]["lEAVE_TYPE"].ToString());
                key.AppendChild(xmlEleNew);
                section.AppendChild(key);

                document.DocumentElement.AppendChild(section);
            }
            document.Save(Server.MapPath("~/XML/Appointments_Outlook.xml"));
        }



        private void overWriteOutlookXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/OutlookTemplate.xml");
                string sDestn = Server.MapPath("~/XML/Appointments_Outlook.xml");
                if (File.Exists(sSource) == true)
                {
                    if (File.Exists(sDestn) == true)
                    {
                        File.Copy(sSource, sDestn, true);
                    }
                    else
                    {
                        File.Move(sSource, sDestn);
                    }
                }
            }
            catch (FileNotFoundException exFile)
            {
                Response.Write(exFile.Message.ToString());
            }

        }

        private void appendOutlookTemplateXml(DataSet leaveDs, DataSet leaveType)
        {
            DateTime leaveStartdate;
            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/Appointments_Outlook.XML"));
            XmlElement xelement = null;
            xelement = document.CreateElement("Resources");
            for (int leaveNo = 0; leaveNo < leaveDs.Tables[0].Rows.Count; leaveNo++)
            {


                XmlNode section = document.CreateElement("Department");
                XmlNode key = document.CreateElement("Key");


                //  key.InnerText = Convert.ToString(leaveNo + 1);
                key.InnerText = leaveDs.Tables[0].Rows[leaveNo]["ID"].ToString();
                section.AppendChild(key);

                key = document.CreateElement("Text");
                key.InnerText = leaveDs.Tables[0].Rows[leaveNo]["DeptName"].ToString();

                section.AppendChild(key);
                xelement.AppendChild(section);

            }
            for (int leaveNo = 0; leaveNo < leaveType.Tables[0].Rows.Count; leaveNo++)
            {


                XmlNode section = document.CreateElement("Leave");
                XmlNode key = document.CreateElement("Key");


                key.InnerText = leaveType.Tables[0].Rows[leaveNo]["ID"].ToString();
                section.AppendChild(key);

                key = document.CreateElement("Text");
                key.InnerText = leaveType.Tables[0].Rows[leaveNo]["Type"].ToString();
                section.AppendChild(key);
                xelement.AppendChild(section);

            }
            document.DocumentElement.AppendChild(xelement);
            document.Save(Server.MapPath("~/XML/Appointments_Outlook.XML"));
        }



        protected void addDynamicCheckBox(DataSet leaveDs, DataSet leaveType)
        {

            for (int deptNo = 0; deptNo < leaveDs.Tables[0].Rows.Count; deptNo++)
            {
                CheckBox ckbox = new CheckBox();
                ckbox.ID = "chk" + leaveDs.Tables[0].Rows[deptNo]["DeptName"].ToString();
                ckbox.Text = leaveDs.Tables[0].Rows[deptNo]["DeptName"].ToString();
                if (!Page.IsPostBack)
                    ckbox.Checked = true;
                ckbox.AutoPostBack = true;
                ckbox.CheckedChanged += new EventHandler(this.CheckBoxes_CheckedChanged);

                RadPanelItem panelItem = (RadPanelItem)PanelBar.FindItemByValue("ChkDepartments");

                System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                dynDiv.Controls.Add(ckbox);
                panelItem.Controls.Add(dynDiv);
                //PanelBar.Items[0].Items[0].Controls.Add(ckbox);
            }
            for (int leaveNo = 0; leaveNo < leaveType.Tables[0].Rows.Count; leaveNo++)
            {
                CheckBox ckbox = new CheckBox();
                ckbox.ID = "chk" + leaveType.Tables[0].Rows[leaveNo]["Type"].ToString();
                ckbox.Text = leaveType.Tables[0].Rows[leaveNo]["Type"].ToString();
                if (!Page.IsPostBack)
                    ckbox.Checked = true;
                ckbox.AutoPostBack = true;
                ckbox.CheckedChanged += new EventHandler(this.CheckBoxes_CheckedChanged);
                RadPanelItem panelItem = (RadPanelItem)PanelBar.FindItemByValue("ChkLeaves");
                System.Web.UI.HtmlControls.HtmlGenericControl dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                dynDiv.Controls.Add(ckbox);
                panelItem.Controls.Add(dynDiv);
            }
        }


        protected void RadScheduler1_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            e.Appointment.Visible = false;
            //e.Appointment.ID = empLeaveType.Tables[0].Rows["EMP_NAME"].ToString();

            foreach (int key in checkBoxDepts.Keys)
            {
                CheckBox chkBox = PanelBar.Items[0].Items[0].FindControl(checkBoxDepts[key]) as CheckBox;
                if (chkBox.Checked)
                {
                    Resource userRes = e.Appointment.Resources.GetResource("Department", key.ToString());
                    if (userRes != null)
                    {
                        foreach (int subKey in checkBoxLeaves.Keys)
                        {
                            CheckBox subchkBox = PanelBar.Items[1].Items[0].FindControl(checkBoxLeaves[subKey]) as CheckBox;
                            if (subchkBox.Checked)
                            {
                                Resource userLeave = e.Appointment.Resources.GetResource("Leave", subKey.ToString());
                                if (userLeave != null)
                                {
                                    e.Appointment.Visible = true;
                                }
                            }
                        }

                    }
                }
            }
        }

    }


}
