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

namespace SMEPayroll.Management
{
    public partial class AccomadationManagement : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        private object _dataItem = null;
        string sSQL = null;
        string AccCode, AccomodationName, AssetType, AssetAmount, AccAddressLine1, AccAddressLine2, EmpRent, AccPostalCode, AccAuthPerson1, AccAuthPersonPhone, AccAuthPerson2, AccAuthPerson2Phone, AccAssistantName, AccAssistantPhone, ArchitectCompanyName, ArchitectCompanyAddress, ArchitectCompanyPhone, ArchitectCompanyFax, ArchitectAuthPersonName, ArchitectAuthPersonEmail, Cooking, CookingType, ElectricCooking, CookingCost, Laundry, LaundryCost, Aircon, AirconCost, TotalRooms, Capacity, singleBedNo, DoubleBedNo, TripleBedNo, NEAApproval, NEADateOfApproval, NEADateOfExpiry, NEAAppRefNo, NEAAppCapacity, PUBapproval, PUBdateOfApproval, PUBDateOfExpiry, PUBAppRefNo, PUBappCapacity, PropertyType, PropertyMonthlyRental, PropertydateOfApproval, PropertyDateOfExpiry, PropertyApprRefNo, PropertyApprCapacity;
        int compID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            string transType = Request.QueryString["tType"].ToString();


            if (!Page.IsPostBack)
            {
                oHidden.Value = transType;
                if (transType == "1")
                {
                    AccCode = Request.QueryString["accCode"].ToString();
                    HiddenAcc.Value = AccCode;
                    bindTextBox();
                }

            }
        }
        private void bind_getTextBox()
        {
            AccomodationName = Utility.ToString(txtAccName.Text);
            AssetType = Utility.ToString(rdRent.SelectedValue.ToString());
            AssetAmount = Utility.ToString(Utility.ToDouble(txtAmount.Text));
            AccAddressLine1 = Utility.ToString(txtAccaddress.Text);
            AccAddressLine2 = Utility.ToString(txtAccaddress2.Text);
            AccPostalCode = Utility.ToString(txtpostalcode.Text);
            AccAuthPerson1 = Utility.ToString(txtMpersonName.Value);
            AccAuthPersonPhone = Utility.ToString(txtMpersonPhone.Value);
            AccAuthPerson2 = Utility.ToString(txtMperson2Name.Value);
            AccAuthPerson2Phone = Utility.ToString(txtMperson2Phone.Value);
            AccAssistantName = Utility.ToString(txtAsstName.Value);
            AccAssistantPhone = Utility.ToString(txtAsstPhone.Value);
            ArchitectCompanyName = Utility.ToString(txtArchCompName.Value);
            ArchitectCompanyAddress = Utility.ToString(txtArchCompAdd.Value);
            ArchitectCompanyPhone = Utility.ToString(txtArchPhone.Value);
            ArchitectCompanyFax = Utility.ToString(txtArchFax.Value);
            ArchitectAuthPersonName = Utility.ToString(txtArchContactPerson.Value);
            ArchitectAuthPersonEmail = Utility.ToString(txtArchEmail.Value);
            Cooking = Utility.ToString(rdCoook.SelectedValue);
            CookingType = Utility.ToString(rdGasType.SelectedValue);

            CookingCost = Utility.ToString(Utility.ToDouble(txtCookingCost.Text));
            Laundry = Utility.ToString(rdLaundry.SelectedValue);
            LaundryCost = Utility.ToString(Utility.ToDouble(txtLaundryCharge.Text));
            Aircon = Utility.ToString(rdAc.SelectedValue);
            AirconCost = Utility.ToString(Utility.ToDouble(txtACcharge.Text));
            TotalRooms = Utility.ToString(Utility.ToInteger(txtTotalRooms.Text));
            Capacity = Utility.ToString(Utility.ToInteger(txtCapacity.Text));
            singleBedNo = Utility.ToString(Utility.ToInteger(txtSingleBed.Text));
            DoubleBedNo = Utility.ToString(Utility.ToInteger(txtDoubleBed.Text));
            TripleBedNo = Utility.ToString(Utility.ToInteger(txtTripleBed.Text));
            NEAApproval = Utility.ToString(Utility.ToInteger(rdNea.SelectedValue));
            NEADateOfApproval = Utility.ToString(rdNeaApprovalDate.SelectedDate);
            NEADateOfExpiry = Utility.ToString(rdNeaExpiryDt.SelectedDate);
            NEAAppRefNo = Utility.ToString(txtNeaAppRef.Value);
            NEAAppCapacity = Utility.ToString(Utility.ToInteger(txtNeaApprCapacity.Value));
            PUBapproval = Utility.ToString(rdPub.SelectedValue);
            PUBdateOfApproval = Utility.ToString(rdPubApprDate.SelectedDate);
            PUBDateOfExpiry = Utility.ToString(rdPubExpiryDate.SelectedDate);
            PUBAppRefNo = Utility.ToString(txtPubAppRef.Value);
            PUBappCapacity = Utility.ToString(txtPubCapacity.Value);
            PropertyType = Utility.ToString(rdPropType.SelectedValue);
            PropertyMonthlyRental = Utility.ToString(txtPropertyRent.Value);
            PropertydateOfApproval = Utility.ToString(dtPropertyApprDate.SelectedDate);
            PropertyDateOfExpiry = Utility.ToString(dtPropertyExpDate.SelectedDate);
            PropertyApprRefNo = Utility.ToString(txtProRef.Value);
            PropertyApprCapacity = Utility.ToString(txtProCapacity.Value);
            EmpRent = Utility.ToString(txtEmpRent.Text);

        }
        private void updateDb(string transType)
        {
            SqlParameter[] sqlParam = new SqlParameter[48];
            int i = 0;
            string sqlQuery = "";
            int status = 0;
            sqlParam[i++] = new SqlParameter("@AccomodationName", AccomodationName);
            sqlParam[i++] = new SqlParameter("@AssetType", AssetType);
            sqlParam[i++] = new SqlParameter("@AssetAmount", AssetAmount);
            sqlParam[i++] = new SqlParameter("@AccAddressLine1", AccAddressLine1);
            sqlParam[i++] = new SqlParameter("@AccAddressLine2", AccAddressLine2);
            sqlParam[i++] = new SqlParameter("@AccPostalCode", AccPostalCode);
            sqlParam[i++] = new SqlParameter("@AccAuthPerson1", AccAuthPerson1);
            sqlParam[i++] = new SqlParameter("@AccAuthPersonPhone", AccAuthPersonPhone);
            sqlParam[i++] = new SqlParameter("@AccAuthPerson2", AccAuthPerson2);
            sqlParam[i++] = new SqlParameter("@AccAuthPerson2Phone", AccAuthPerson2Phone);
            sqlParam[i++] = new SqlParameter("@AccAssistantName", AccAssistantName);
            sqlParam[i++] = new SqlParameter("@AccAssistantPhone", AccAssistantPhone);
            sqlParam[i++] = new SqlParameter("@ArchitectCompanyName", ArchitectCompanyName);
            sqlParam[i++] = new SqlParameter("@ArchitectCompanyAddress", ArchitectCompanyAddress);
            sqlParam[i++] = new SqlParameter("@ArchitectCompanyPhone", ArchitectCompanyPhone);
            sqlParam[i++] = new SqlParameter("@ArchitectCompanyFax", ArchitectCompanyFax);
            sqlParam[i++] = new SqlParameter("@ArchitectAuthPersonName", ArchitectAuthPersonName);
            sqlParam[i++] = new SqlParameter("@ArchitectAuthPersonEmail", ArchitectAuthPersonEmail);
            sqlParam[i++] = new SqlParameter("@Cooking", Cooking);
            sqlParam[i++] = new SqlParameter("@CookingType", CookingType);
            sqlParam[i++] = new SqlParameter("@CookingCost", CookingCost);
            sqlParam[i++] = new SqlParameter("@Laundry", Laundry);
            sqlParam[i++] = new SqlParameter("@LaundryCost", LaundryCost);
            sqlParam[i++] = new SqlParameter("@Aircon", Aircon);
            sqlParam[i++] = new SqlParameter("@AirconCost", AirconCost);
            sqlParam[i++] = new SqlParameter("@TotalRooms", TotalRooms);
            sqlParam[i++] = new SqlParameter("@Capacity", Capacity);
            sqlParam[i++] = new SqlParameter("@singleBedNo", singleBedNo);
            sqlParam[i++] = new SqlParameter("@DoubleBedNo", DoubleBedNo);
            sqlParam[i++] = new SqlParameter("@TripleBedNo", TripleBedNo);
            sqlParam[i++] = new SqlParameter("@NEAApproval", NEAApproval);
            sqlParam[i++] = new SqlParameter("@NEADateOfApproval", NEADateOfApproval);
            sqlParam[i++] = new SqlParameter("@NEADateOfExpiry", NEADateOfExpiry);
            sqlParam[i++] = new SqlParameter("@NEAAppRefNo", NEAAppRefNo);
            sqlParam[i++] = new SqlParameter("@NEAAppCapacity", NEAAppCapacity);
            sqlParam[i++] = new SqlParameter("@PUBapproval", PUBapproval);
            sqlParam[i++] = new SqlParameter("@PUBdateOfApproval", PUBdateOfApproval);
            sqlParam[i++] = new SqlParameter("@PUBDateOfExpiry", PUBDateOfExpiry);
            sqlParam[i++] = new SqlParameter("@PUBAppRefNo", PUBAppRefNo);
            sqlParam[i++] = new SqlParameter("@PUBappCapacity", PUBappCapacity);
            sqlParam[i++] = new SqlParameter("@PropertyType", PropertyType);
            sqlParam[i++] = new SqlParameter("@PropertyMonthlyRental", PropertyMonthlyRental);
            sqlParam[i++] = new SqlParameter("@PropertydateOfApproval", PropertydateOfApproval);
            sqlParam[i++] = new SqlParameter("@PropertyDateOfExpiry", PropertyDateOfExpiry);
            sqlParam[i++] = new SqlParameter("@PropertyApprRefNo", PropertyApprRefNo);
            sqlParam[i++] = new SqlParameter("@PropertyApprCapacity", PropertyApprCapacity);
            sqlParam[i++] = new SqlParameter("@EmpRent", EmpRent);
            sqlParam[i++] = new SqlParameter("@CompId", compID);
            if (transType == "I")
            {
                sqlQuery = "Sp_InsertAccomadationDetails";
                status = DataAccess.ExecuteStoreProc(sqlQuery, sqlParam);
                if (status > 0)
                {
                    //Response.Write("<SCRIPT>alert('The data has been saved successfully.');</SCRIPT>");
                    Response.Redirect("..\\Management\\AccomadationInfo.aspx?Accommodation=Inserted");
                }
            }
            else if (transType == "U")
            {

                AccCode = HiddenAcc.Value;
                sqlQuery = "update [AccomodationMasterTable] set AccomodationName='" + AccomodationName + "', AssetType='" + AssetType + "',AssetAmount='" + AssetAmount + "',AccAddressLine1='" + AccAddressLine1 + "',AccAddressLine2='" + AccAddressLine2 + "',AccPostalCode='" + AccPostalCode + "',AccAuthPerson1='" + AccAuthPerson1 + "',AccAuthPersonPhone='" + AccAuthPersonPhone + "',AccAuthPerson2='" + AccAuthPerson2 + "',AccAuthPerson2Phone='" + AccAuthPerson2Phone + "',AccAssistantName='" + AccAssistantName + "',AccAssistantPhone='" + AccAssistantPhone + "',ArchitectCompanyName='" + ArchitectCompanyName + "',ArchitectCompanyAddress='" + ArchitectCompanyAddress + "',ArchitectCompanyPhone='" + ArchitectCompanyPhone + "',ArchitectCompanyFax='" + ArchitectCompanyFax + "',ArchitectAuthPersonName='" + ArchitectAuthPersonName + "',ArchitectAuthPersonEmail='" + ArchitectAuthPersonEmail + "',Cooking='" + Cooking + "',CookingType='" + CookingType + "',CookingCost='" + CookingCost + "',Laundry='" + Laundry + "',LaundryCost='" + LaundryCost + "',Aircon='" + Aircon + "',AirconCost='" + AirconCost + "',TotalRooms='" + TotalRooms + "',Capacity='" + Capacity + "',singleBedNo='" + singleBedNo + "',DoubleBedNo='" + DoubleBedNo + "',TripleBedNo='" + TripleBedNo + "',NEAApproval='" + NEAApproval + "',NEADateOfApproval='" + NEADateOfApproval + "',NEADateOfExpiry='" + NEADateOfExpiry + "',NEAAppRefNo='" + NEAAppRefNo + "',NEAAppCapacity='" + NEAAppCapacity + "',PUBapproval='" + PUBapproval + "',PUBdateOfApproval='" + PUBdateOfApproval + "',PUBDateOfExpiry='" + PUBDateOfExpiry + "',PUBAppRefNo='" + PUBAppRefNo + "',PUBappCapacity='" + PUBappCapacity + "',PropertyType='" + PropertyType + "',PropertyMonthlyRental='" + PropertyMonthlyRental + "',PropertydateOfApproval='" + PropertydateOfApproval + "',PropertyDateOfExpiry='" + PropertyDateOfExpiry + "',PropertyApprRefNo='" + PropertyApprRefNo + "' ,EmpRent ='" + EmpRent + "' where AccCode = '" + AccCode + "'";
                status = DataAccess.ExecuteNonQuery(sqlQuery, sqlParam);
                if (status > 0)
                {
                    //.Write("<SCRIPT>alert('The data has been saved successfully.');</SCRIPT>");
                    Response.Redirect("..\\Management\\AccomadationInfo.aspx?Accommodation=Updated");
                }
            }

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (oHidden.Value == "0")
            {
                bind_getTextBox();
                updateDb("I");
            }
            else
            {
                bind_getTextBox();
                updateDb("U");
            }
        }

        private void bindTextBox()
        {
            DataSet sqlDs = this.AccomadationDetails;

            if (sqlDs.Tables.Count > 0)
            {
                if (sqlDs.Tables[0].Rows.Count > 0)
                {
                    txtAccName.Text = sqlDs.Tables[0].Rows[0]["AccomodationName"].ToString();
                    rdRent.SelectedValue = sqlDs.Tables[0].Rows[0]["AssetType"].ToString();
                    txtAmount.Text = sqlDs.Tables[0].Rows[0]["AssetAmount"].ToString();
                    txtAccaddress.Text = sqlDs.Tables[0].Rows[0]["AccAddressLine1"].ToString();
                    txtAccaddress2.Text = sqlDs.Tables[0].Rows[0]["AccAddressLine2"].ToString();
                    txtpostalcode.Text = sqlDs.Tables[0].Rows[0]["AccPostalCode"].ToString();
                    txtMpersonName.Value = sqlDs.Tables[0].Rows[0]["AccAuthPerson1"].ToString();
                    txtMpersonPhone.Value = sqlDs.Tables[0].Rows[0]["AccAuthPersonPhone"].ToString();
                    txtMperson2Name.Value = sqlDs.Tables[0].Rows[0]["AccAuthPerson2"].ToString();
                    txtMperson2Phone.Value = sqlDs.Tables[0].Rows[0]["AccAuthPerson2Phone"].ToString();
                    txtAsstName.Value = sqlDs.Tables[0].Rows[0]["AccAssistantName"].ToString();
                    txtAsstPhone.Value = sqlDs.Tables[0].Rows[0]["AccAssistantPhone"].ToString();
                    txtArchCompName.Value = sqlDs.Tables[0].Rows[0]["ArchitectCompanyName"].ToString();
                    txtArchCompAdd.Value = sqlDs.Tables[0].Rows[0]["ArchitectCompanyAddress"].ToString();
                    txtArchPhone.Value = sqlDs.Tables[0].Rows[0]["ArchitectCompanyPhone"].ToString();
                    txtArchFax.Value = sqlDs.Tables[0].Rows[0]["ArchitectCompanyFax"].ToString();
                    txtArchContactPerson.Value = sqlDs.Tables[0].Rows[0]["ArchitectAuthPersonName"].ToString();
                    txtArchEmail.Value = sqlDs.Tables[0].Rows[0]["ArchitectAuthPersonEmail"].ToString();
                    rdCoook.SelectedValue = sqlDs.Tables[0].Rows[0]["Cooking"].ToString();
                    rdGasType.SelectedValue = sqlDs.Tables[0].Rows[0]["CookingType"].ToString();
                    txtCookingCost.Text = sqlDs.Tables[0].Rows[0]["CookingCost"].ToString();

                    rdLaundry.SelectedValue = sqlDs.Tables[0].Rows[0]["Laundry"].ToString();
                    txtLaundryCharge.Text = sqlDs.Tables[0].Rows[0]["LaundryCost"].ToString();
                    rdAc.SelectedValue = sqlDs.Tables[0].Rows[0]["Aircon"].ToString();
                    txtACcharge.Text = sqlDs.Tables[0].Rows[0]["AirconCost"].ToString();
                    txtTotalRooms.Text = sqlDs.Tables[0].Rows[0]["TotalRooms"].ToString();
                    txtCapacity.Text = sqlDs.Tables[0].Rows[0]["Capacity"].ToString();
                    txtSingleBed.Text = sqlDs.Tables[0].Rows[0]["singleBedNo"].ToString();
                    txtDoubleBed.Text = sqlDs.Tables[0].Rows[0]["DoubleBedNo"].ToString();
                    txtTripleBed.Text = sqlDs.Tables[0].Rows[0]["TripleBedNo"].ToString();
                    rdNea.SelectedValue = sqlDs.Tables[0].Rows[0]["NEAApproval"].ToString();

                    txtNeaAppRef.Value = sqlDs.Tables[0].Rows[0]["NEAAppRefNo"].ToString();
                    txtNeaApprCapacity.Value = sqlDs.Tables[0].Rows[0]["NEAAppCapacity"].ToString();
                    rdPub.SelectedValue = sqlDs.Tables[0].Rows[0]["PUBapproval"].ToString();
                    //rdPubApprDate.SelectedDate="";
                    //rdPubExpiryDate.SelectedDate="";
                    txtPubAppRef.Value = sqlDs.Tables[0].Rows[0]["PUBAppRefNo"].ToString();
                    txtPubCapacity.Value = sqlDs.Tables[0].Rows[0]["PUBappCapacity"].ToString();
                    rdPropType.SelectedValue = sqlDs.Tables[0].Rows[0]["PropertyType"].ToString();
                    txtPropertyRent.Value = sqlDs.Tables[0].Rows[0]["PropertyMonthlyRental"].ToString();
                    //dtPropertyApprDate.SelectedDate = "";
                    //dtPropertyExpDate.SelectedDate = "";
                    txtProRef.Value = sqlDs.Tables[0].Rows[0]["PropertyApprRefNo"].ToString();
                    txtProCapacity.Value = sqlDs.Tables[0].Rows[0]["PropertyApprCapacity"].ToString();
                    txtEmpRent.Text = sqlDs.Tables[0].Rows[0]["EmpRent"].ToString();
                }
            }

        }
        private DataSet AccomadationDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@compId", compID);
                parms1[1] = new SqlParameter("@AccCode", AccCode);
                parms1[2] = new SqlParameter("@type", 2);
                sSQL = "sp_GetAccomadationDetails";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                return ds;
            }

        }
    }
}
