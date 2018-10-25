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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SMEPayroll.TimeSheet
{
    public partial class DragdropRoaster : System.Web.UI.Page
    {
        protected ArrayList EmployeeListAray;
      
        string sSQL, sSQL_date;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                //load roaster table
                LoadRoaster();

                //load employe table
                loadEmployee();
            }

            

        }


        #region Load Grid
        public void loadEmployee()
            {
                EmployeeListAray = new ArrayList();
                string sSQL = " select emp_code,emp_name from employee";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                EmployeeListAray = new ArrayList();
                while (dr.Read())
                {
                    EmployeeList opaylist = new EmployeeList();
                    opaylist.Emp_code = Utility.ToInteger(dr["emp_code"].ToString());
                    opaylist.Name = Utility.ToString(dr["emp_name"].ToString());
                    EmployeeListAray.Add(opaylist);
                }

            }

        public void LoadRoaster()
        {
           
            //load from DB
            DataSet ds = new DataSet();
            sSQL = "select * from Roaster_Pattern";//check with company name
            ds = GetDataSet(sSQL);

            //load header date
            DataSet ds_date = new DataSet();
            sSQL_date = "SELECT thedate FROM dbo.ExplodeDates('2013/07/01','2013/07/07') as Bdate";//change from selection
            ds_date = GetDataSet(sSQL_date);
            

            //Building table in run time
            string tablestring = "";
            tablestring = tablestring + "<table width=\"300px\" id=\"tblPage\" runat=\"server\">";
            
            //header
            tablestring = tablestring + "<tr><td>Pattern</td>";
            foreach (DataRow dr_date in ds_date.Tables[0].Rows)
            {
                tablestring = tablestring + "<td class=\"mark\">" + dr_date[0].ToString() + "</td>";
            }
            tablestring = tablestring + "</tr>";
            //End Header

            //data
            foreach (DataRow drrr in ds.Tables[0].Rows)
            {
                tablestring = tablestring + "<tr><td class=\"mark\">" + drrr["Pattern"].ToString() + "</td>";
                foreach (DataRow dr_date in ds_date.Tables[0].Rows)
                {
                    //tablestring = tablestring + " <td id=\"td" + drrr["Rid"].ToString() + "\" >  <div class=\"drag t3\" id=\"18\" >Drag and drop me!</div>   </td>";

                     EmployeeList Emp = GetEmployee(drrr["Rid"].ToString(), Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString()));
                    if (Emp.Emp_code != 0)
                    {
                        tablestring = tablestring + " <td id="+ drrr["Rid"].ToString() + "_" + Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString()) + "\" >  <div class=\"drag t3\" id=\"18\" >" + Emp.Name + "</div>   </td>";
                    }
                    else
                    {
                        tablestring = tablestring + " <td id=" + drrr["Rid"].ToString() + "_" + Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString()) + "\" >  </td>";
                    }
                }
                tablestring = tablestring + "</tr>";
            }

            //data end
            tablestring = tablestring + "</table>";



            TableDiv.InnerHtml = tablestring;
            //

   
        }

        





        private EmployeeList GetEmployee(string pattern, DateTime bDate)
        {
              EmployeeList _list = new EmployeeList();
               string returnval = "";
                string EmployeeName;
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string sql1 = "select Roster_ID as emp_code,(select emp_name from employee where emp_code=R.Roster_ID) Name from RosterDetail R  where Roster_Date= + Convert(datetime,'" + bDate.Date.ToString("dd/MM/yyyy", format) + "',103)  and Pattern='" + pattern + "'";
                
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr1.Read())
                {
                   
                    _list.Emp_code = Utility.ToInteger(dr1["emp_code"].ToString());
                    _list.Name = Utility.ToString(dr1["Name"].ToString());
                    
                }
                dr1.Close();

                return _list;
        }
	    #endregion

        #region Utility
            private static DataSet GetDataSet(string query)
            {
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, query, null);
                return ds;
            }
        #endregion

            #region javascript method-After drop update in database
            //http://www.aspsnippets.com/Articles/Calling-server-side-methods-using-JavaScript-and-JQuery-in-ASP.Net.aspx
            [System.Web.Services.WebMethod]
            public static string UpdateDB(string val)
            {

                 //strUpdateDelSQL = "Update ACTATEK_LOGS_PROXY set softdelete=2 Where (softdelete=0 and (" + strUpdateBuild + " 1=0))";
                 //DataAccess.ExecuteStoreProc(strUpdateDelSQL);

              
                string[] separators = { "_", "," };
                string[] split = val.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                string EmpCode = split[0].ToString();
                string pattern = split[1].ToString();
                string DateOfRost = split[2].ToString();

                //update in DB


                //

                return "sdfsd  " + EmpCode + " patern=" + pattern + "dateofrost=" + DateOfRost;
            }
            #endregion




        }

    #region Class
     public class EmployeeList
    {
        public int Emp_code ;
        public string Name;
    }

    
    #endregion
}
