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
using Telerik.Web.UI;
using System.Net.Mail;

namespace SMEPayroll.Management
{
    public partial class AssignUserRights : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string rCategory = "";
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!Page.IsPostBack)
            {
                DataSet RightsDs = this.RightDetails;
                for (int row = 0; row < RightsDs.Tables[0].Rows.Count; row++)
                {
                    rCategory = RightsDs.Tables[0].Rows[row]["RightCategory"].ToString();
                    AddTab(RightsDs.Tables[0].Rows[row]["RightCategory"].ToString());
                }

                int GroupID = Utility.ToInteger(Session["varGroupID"]);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select GroupName from usergroups where GroupId=" + GroupID, null);
                if (dr.Read())
                {
                    lblUserGroup.Text = "User Groups: " + dr[0].ToString().ToUpper();
                    if (dr[0].ToString() == "Super Admin")
                    {
                        Button1.Enabled = false;
                    }
                }

            }

        }
        private void AddTab(string tabName)
        {
            RadTab tab = new RadTab();
            tab.Text = tabName;
            RadTabStrip1.Tabs.Add(tab);

            RadPageView pageView = new RadPageView();

            pageView.ID = tabName;
            RadMultiPage1.PageViews.Add(pageView);

        }

        protected void RadMultiPage1_PageViewCreated(object sender, RadMultiPageEventArgs e)
        {

            string userControlName = "GroupRights.ascx";
            GroupRights userControl = (GroupRights)Page.LoadControl(userControlName);
            userControl.ID = e.PageView.ID + "_userControl";

            userControl.MyGroupId = Utility.ToInteger(Session["varGroupID"]);


            userControl.MyrightCategory = rCategory;

            e.PageView.Controls.Add(userControl);
        }
        private DataSet RightDetails
        {
            get
            {
                string sSQL = "";
                DataSet ds = new DataSet();
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SME")
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 0 OR Product = 2 OR Product is null) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SMEMC")
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And ( Product = 2 OR Product = 4 ) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS"))
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 1 OR Product = 2) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSA"))
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 1 OR Product = 2 OR Product = 4)  and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSAMC"))
                {
                    // sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)  And (Product = 1 OR Product = 2 OR Product = 4 OR Product = 6) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)  And (Product = 1 OR Product = 2 OR Product = 4 OR Product = 6) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1))  GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI"))
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 1 OR Product = 2 OR Product = 3 OR Product = 4) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SMEA")
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 0 OR Product = 2 OR Product = 6 OR Product is null) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSA"))
                {
                    sSQL = "SELECT  RightCategory,HeaderId FROM USERRIGHTS  WHERE (HeaderId IS NOT NULL and HeaderId !=0)   And (Product = 1 OR Product = 2 OR Product = 6) and ((MenuId>1000 and MenuId<1501) or (MenuId>2000 and MenuId<2501) or (MenuId>3000 and MenuId<3501) or (MenuId>4000 and MenuId<4501) or (MenuId>5000 and MenuId<5501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>6000 and MenuId<6501) or (MenuId>7000 and MenuId<7501) or (MenuId>8000 and MenuId<8501) or (MenuId>9000 and MenuId<9501) or (MenuId=1)) GROUP BY  RightCategory,HeaderId ORDER BY HeaderId";
                }
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int varGrpID = Utility.ToInteger(Session["varGroupID"]);

            int varRightID;

            DataAccess.ExecuteStoreProc("Usp_DelGroupRights " + varGrpID);

            foreach (Control ctr in this.Page.Controls)
            {
                if (ctr is System.Web.UI.HtmlControls.HtmlForm)
                {

                    foreach (Control chldCtr in ctr.Controls)
                    {
                        //Telerik.Web.UI.RadMultiPage
                        if (chldCtr is Telerik.Web.UI.RadMultiPage)
                        {
                            foreach (Telerik.Web.UI.RadPageView rmp in chldCtr.Controls)
                            {
                                if (rmp is Telerik.Web.UI.RadPageView)
                                {

                                    foreach (GroupRights rg in rmp.Controls)
                                    {
                                        if (rg is GroupRights)
                                        {
                                            RadGrid radGrid = (RadGrid)rg.FindControl("RadGrid1");

                                            foreach (GridItem item in radGrid.MasterTableView.Items)
                                            {
                                                if (item is GridItem)
                                                {
                                                    GridDataItem dataItem = (GridDataItem)item;
                                                    CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
                                                    if (chkBox.Checked == true)
                                                    {
                                                        varRightID = Utility.ToInteger(radGrid.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RightID"));
                                                        DataAccess.ExecuteStoreProc("Usp_InsGroupRights " + varGrpID + ", " + varRightID);
                                                        _actionMessage = "Success|Saved Successfully.";
                                                        ViewState["actionMessage"] = _actionMessage;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
