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

namespace SMEPayroll.TimeSheet
{
    public partial class TimesheetApprovalCopy : System.Web.UI.UserControl
    {
       // string refid="30032017104822";
        private object _dataItem = null;
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }


        protected void RadGrid1_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == "Time_Card_No")
            {
                e.Column.Visible = false;
                
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {  DataSet dt = new DataSet();

       
            object refid = DataBinder.Eval(DataItem, "RefId");

            if (refid != null)
            {
                string sql = @" select (select Sub_Project_Name from SubProject where SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID) ProjectName,
    
       convert(varchar(20), [TimeEntryStart]) [TimeEntryStart]
      ,convert(varchar(20),[TimeEntryEnd]) [TimeEntryEnd]
      ,[NH]
      ,[OT1]
      ,[OT2]
      ,[TotalHrsWrk]
      ,[Remarks],'' Time_Card_No from [ApprovedTimeSheet] where [RefID]= '" + refid.ToString() + "' union all select '','Total','' ,sum(cast([NH] as decimal(4,2))),sum(cast([OT1] as decimal(4,2))),sum(cast([OT2] as decimal(4,2))),sum(cast([TotalHrsWrk] as decimal(4,2))),''[Remarks],Time_Card_No from [ApprovedTimeSheet] where [RefID]= '" + refid.ToString() + "' group by Time_Card_No ";


                dt = DataAccess.FetchRS(CommandType.Text, sql, null);
            }
        
      if (dt.Tables.Count > 0)
      {
          this.RadGrid1.DataSource = dt.Tables[0];

          this.RadGrid1.DataBind();
      }



        }
    }
}