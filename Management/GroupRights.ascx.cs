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

namespace SMEPayroll.Management
{
    public partial class GroupRights : System.Web.UI.UserControl
    {
        private int GroupID = 0;
        private string rightCategory = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rightsHeader.InnerText = "Rights For " + rightCategory;
                bindgrid();
            }
          
        }

        private DataSet RightDetails
        {
            get
            {
                string sSQL = "";

                DataSet ds = new DataSet();
                string Country = Session["Country"].ToString();
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SME")
                {
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null)and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 0 OR b.Product = 2 OR b.Product is null) and RightID Not In  ";
                    sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";

                    
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SMEA")
                {
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product = 6 OR b.Product is null)and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 0 OR b.Product = 2 OR b.Product = 6 OR b.Product is null) and RightID Not In  ";
                    sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2 OR b.Product = 6 OR b.Product is null))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";


                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SMEMC")
                {
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 2 OR b.Product = 4)and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 2 OR b.Product = 4) and RightID Not In  ";
                    sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 2 OR b.Product = 4))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";

                    
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS")
                {
                      //changes -->b.Product = 1 OR b.Product = 2) after union
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 1 OR b.Product = 2) and  RightID Not In  ";
                    sSQL = sSQL + " ((select a.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";
                }
                
               
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSA")
                {
                    //changes -->b.Product = 1 OR b.Product = 2) after union
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2 OR b.Product = 6) and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 1 OR b.Product = 2 OR b.Product =6 ) and  RightID Not In  ";
                    sSQL = sSQL + " ((select a.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2 OR b.Product =6 ))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSMC")
                {
                    //changes -->b.Product = 1 OR b.Product = 2) after union
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2 OR b.Product = 4) and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 1 OR b.Product = 2 OR b.Product =4 ) and  RightID Not In  ";
                    sSQL = sSQL + " ((select a.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2 OR b.Product =4 ))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSAMC")
                {
                    //changes -->b.Product = 1 OR b.Product = 2) after union
                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2 OR b.Product = 4 OR b.Product = 6) and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where (b.Product = 1 OR b.Product = 2 OR b.Product =4 OR b.Product = 6) and  RightID Not In  ";
                    sSQL = sSQL + " ((select a.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2 OR b.Product =4 OR b.Product = 6))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";
                }
                if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI")
                {

                    sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
                    sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
                    sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
                    sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2 OR b.Product = 3 OR b.Product = 4) and c.CountryID=" + Country;
                    sSQL = sSQL + " Union ";
                    sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
                    sSQL = sSQL + " Where RightID Not In  ";
                    sSQL = sSQL + " ((select a.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2 OR b.Product = 3 OR b.Product = 4))) ";
                    sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  rightid";

                }

                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        //private DataSet RightDetails
        //{
        //    get
        //    {
        //        string sSQL = "";

        //        DataSet ds = new DataSet();
        //        string Country = Session["Country"].ToString();
        //        if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SME")
        //        {
        //            sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
        //            sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null)and c.CountryID=" + Country;
        //            sSQL = sSQL + " Union ";
        //            sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            sSQL = sSQL + " Where RightID Not In  ";
        //            sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null))) ";
        //            sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";

        //            //sSQL = "select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description  from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null) order by  DisplayId";
        //        }
        //        if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI"))
        //        {
        //            //sSQL = "select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description  from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) order by  DisplayId";

        //            //sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            //sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) ";
        //            //sSQL = sSQL + " Union ";
        //            //sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            //sSQL = sSQL + " Where RightID Not In  ";
        //            //sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2))) ";
        //            //sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";


        //            //changes -->b.Product = 1 OR b.Product = 2) after union
        //            sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID inner join CountryRights  c On c.RightID=a.RightID";
        //            sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) and c.CountryID=" + Country;
        //            sSQL = sSQL + " Union ";
        //            sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            sSQL = sSQL + " Where RightID Not In  ";
        //            sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2))) ";
        //            sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";
        //        }



        //        ds = GetDataSet(sSQL);
        //        return ds;
        //    }
        //}
        //private DataSet RightDetails
        //{
        //    get
        //    {
        //                        string sSQL = "" ;

        //        DataSet ds = new DataSet();
        //        string Country ="301";
        //        if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SME")
        //        {
        //            sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID";
        //            sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null)";
        //            sSQL = sSQL + " Union ";
        //            sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            sSQL = sSQL + " Where RightID Not In  ";
        //            sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null))) ";
        //            sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";

        //            //sSQL = "select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description  from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 0 OR b.Product = 2 OR b.Product is null) order by  DisplayId";
        //        }
        //        if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI"))
        //        {
        //            //sSQL = "select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description  from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) order by  DisplayId";

        //            //sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            //sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID ";
        //            //sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            //sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2) ";
        //            //sSQL = sSQL + " Union ";
        //            //sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            //sSQL = sSQL + " Where RightID Not In  ";
        //            //sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 0 OR b.Product = 2))) ";
        //            //sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";


        //            //changes -->b.Product = 1 OR b.Product = 2) after union
        //            sSQL = " Select * From (Select GroupID = case when a.GroupID is not null then 'true' else 'False' end, b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid ";
        //            sSQL = sSQL + " from GroupRights a Inner Join UserRights b On a.RightID=b.RightID";
        //            sSQL = sSQL + " where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID;
        //            sSQL = sSQL + " And (b.Product = 1 OR b.Product = 2)";
        //            sSQL = sSQL + " Union ";
        //            sSQL = sSQL + " Select 'False' GroupID,b.RightID,b.ActualRightName, b.RightSubCategory,b.Description,b.displayid  From UserRights b  ";
        //            sSQL = sSQL + " Where RightID Not In  ";
        //            sSQL = sSQL + " ((select b.RightID from GroupRights a Inner Join UserRights b On a.RightID=b.RightID  where b.rightcategory='" + rightCategory + "' and a.GroupID = " + GroupID + " And (b.Product = 1 OR b.Product = 2))) ";
        //            sSQL = sSQL + " And b.rightcategory='" + rightCategory + "' ) D order by  DisplayId";
        //        }



        //        ds = GetDataSet(sSQL);
        //        return ds;
        //    }
        //}
        private void bindgrid()
        {
        RadGrid1.DataSource  =RightDetails;
        RadGrid1.DataBind();
        }
        protected bool GenerateBindString(object dataItem)
        {
            bool ret = false;


            // if column is null set checkbox.checked = false

            if ((DataBinder.Eval(dataItem, "GroupID")).ToString() == "False")
                ret = false;
            else // set checkbox.checked to boolean value in Status column
                ret = true;
            return ret;
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        
        public int MyGroupId
        {
            get
            {
                return GroupID;
            }
            set
            {
                GroupID = value;
            }
        }
        public string MyrightCategory
        {
            get
            {
                return rightCategory;
            }
            set
            {
                rightCategory = value;
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            foreach (Telerik.Web.UI.GridGroupHeaderItem groupHeader in RadGrid1.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.GroupHeader))
            {
                Telerik.Web.UI.GridItem[] children = groupHeader.GetChildItems();
                foreach (Telerik.Web.UI.GridItem child in children)
                {
                    if (child.Expanded)
                        child.Expanded = false;

                }
                //Telerik.Web.UI.GridItemType.GroupHeader
                //RadGrid1.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.GroupHeader)[0].Expanded = false;

            }
            foreach (Telerik.Web.UI.GridGroupHeaderItem groupHeader in RadGrid1.MasterTableView.GetItems(Telerik.Web.UI.GridItemType.GroupHeader))
            {
                // Telerik.Web.UI.GridItem[] children = groupHeader.GetChildItems();
                //count = children.Count();
                //groupHeader.DataCell.Text = "Count=" + count.ToString();
                //count = 0;

                if (this.ID == "Admin_userControl")
                {
                    RadGrid1.MasterTableView.ExpandCollapseColumn.Visible = true;
                    //RightSubCategory:

                    //groupHeader.DataCell.Text = "";
                    //groupHeader.DataCell.Text.Substring(17);

                    //  groupHeader.Expanded = false;
                }

                if (e.Item is Telerik.Web.UI.GridGroupHeaderItem)
                {
                    GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                    DataRowView groupDataRow = (DataRowView)e.Item.DataItem;
                    item.DataCell.Text = groupDataRow["RightSubCategory"].ToString();
                }

            }


        }
        protected void chkLinked_CheckedChanged(Object sender, EventArgs args)
        {
            CheckBox checkbox = (CheckBox)sender;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    int i = dataItem.ItemIndex;
                    // string strVar = (dataItem.Cells[i]).Text;
                    //if (i == 3)
                    //{
                    CheckBox cbox = (CheckBox)dataItem.Cells[3].Controls[1];

                    cbox.Checked = checkbox.Checked;
                    // }
                    //TextBox tb = (TextBox)dataItem.Cells[2].Controls[1];

                }
            }


        }

    }
}