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
using Microsoft.VisualBasic;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using IRAS.Appendix_A;

using IRAS.Appendix_B;

namespace IRAS
{

    public partial class IR8A_AMENDMENT : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy = "";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        protected string strendatedmy = "";
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        public string connection;

        private int year;

        string grossvalue;


        /// <summary>
        /// XML GENERATION
        /// </summary>


        //Response.Clear()
        //Response.ContentType = "Application/PDF"
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" & Request.QueryString("FILENAME"))
        //Response.WriteFile(Request.QueryString("FILENAME"))
        //Response.Flush()
        //Response.Close()


        //
        //ClientScript.RegisterStartupScript(this.GetType(), flname, "<script language='javascript'>fnOpen('" & flname & "');</script>");









        public void AppdixBXml(List<int> emp_codelist, FileHeaderST Fileheader)
        {

            List<A8B2009ST> _listA8B2009ST = new List<A8B2009ST>();

            A8BRECORDDETAILS single_details;

            SqlDataReader sqlDr = null;




            decimal SectionATrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionATrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionATrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionATrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionBTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionBTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionCTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionCTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerNonExemptTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerNonExemptTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionDTrailerGainsTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionDTrailerGainsTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionETrailerNonExemptGrandTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionETrailerNonExemptGrandTotalGrossAmountBefore2003 = 0.00m;
            decimal SectionETrailerGainsGrandTotalGrossAmountAfter2003 = 0.00m;
            decimal SectionETrailerGainsGrandTotalGrossAmountBefore2003 = 0.00m;



            A8BRECORDDETAILS blank_details = new A8BRECORDDETAILS();
            blank_details.CompanyIDType = "";
            blank_details.CompanyIDNo = "";
            blank_details.CompanyName = "";
            blank_details.PlanType = "";
            blank_details.DateOfGrant = "";
            blank_details.DateOfExercise = "";
            blank_details.Price = 0.00m;
            blank_details.OpenMarketValueAtDateOfGrant = 0.00m;
            blank_details.OpenMarketValueAtDateOfExercise = 0.00m;
            blank_details.NoOfShares = 0;
            blank_details.NonExemptGrossAmount = 0.00m;
            blank_details.GrossAmountGains = 0.00m;





            XmlWriterSettings wSettings = new XmlWriterSettings();
            wSettings.Indent = true;
            MemoryStream ms = new MemoryStream();
            XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration








            foreach (int emp_code in emp_codelist)
            {

                List<A8BRECORDDETAILS> _listdetails1 = new List<A8BRECORDDETAILS>();




                string sSQL = @"SELECT 
                                               k.ID as A8a2009ST_ID 
                                              ,K.[RecordType]
                                              ,K.[IDType]
                                              ,K.[IDNo]
                                              ,K.[NameLine1]
                                              ,K.[NameLine2]
                                              ,K.[Nationality]
                                              ,K.[Sex]
                                              ,k.[DateOfBirth],
                                               k.[DateOfIncorporation],
                                                 l.[ID]
                                              ,l.[CompanyIDType]
                                              ,l.[CompanyIDNo]
                                              ,l.[CompanyName]
                                              ,l.[PlanType]
                                              ,l.[DateOfGrant]
                                              ,l.[DateOfExercise]
                                              ,l.[Price]
                                              ,l.[OpenMarketValueAtDateOfGrant]
                                              ,l.[OpenMarketValueAtDateOfExercise]
                                              ,l.[NoOfShares]
                                              ,l.[NonExemptGrossAmount]
                                              ,l.[GrossAmountGains]
                                              ,l.[FK_A8A2009ST]
                                              ,l.[Total_i]
                                              ,l.[Total_j]
                                              ,l.[Total_k]
                                              ,l.[Total_L]
                                              ,l.[Total_M]
                                              ,l.[RecordNo]
                                              ,l.[Section]
                                              ,l.[G_Total]
                                              ,[G_Total_I]
                                              ,[G_Total_J]
                                              ,[G_Total_K]
                                              ,[G_Total_L]
                                              ,[G_Total_M]
                                        FROM  [A8B2009ST] as k join [A8BRECORDDETAILS] as l on k.ID =l.FK_A8A2009ST
                                        where k.emp_id='" + emp_code + "'and k.AppendixB_year=" + year + "and IS_AMMENDMENT=0";

                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                Session["fk"] = "";

                A8B2009ST _A8BST1 = new A8B2009ST();

                //_A8BST.Fileheader = fileheader;

                while (sqlDr.Read())
                {

                    _A8BST1.ID = Convert.ToInt32(sqlDr["ID"]);
                    _A8BST1.IDNo = Convert.ToString(sqlDr["IDNo"].ToString());
                    _A8BST1.IDType = Convert.ToString(sqlDr["IDType"].ToString());
                    _A8BST1.NameLine1 = Convert.ToString(sqlDr["NameLine1"].ToString());
                    _A8BST1.NameLine2 = Convert.ToString(sqlDr["NameLine2"].ToString());
                    _A8BST1.Nationality = Convert.ToString(sqlDr["Nationality"].ToString());
                    _A8BST1.RecordType = Convert.ToString(sqlDr["RecordType"].ToString());
                    _A8BST1.Sex = Convert.ToString(sqlDr["Sex"].ToString());
                    _A8BST1.DateOfBirth = Convert.ToString(sqlDr["DateOfBirth"].ToString());
                    _A8BST1.DateOfIncorporation = Convert.ToString(sqlDr["DateOfIncorporation"].ToString());
                    single_details = new A8BRECORDDETAILS();
                    single_details.CompanyIDType = "1";//ndo
                    single_details.CompanyIDNo = Convert.ToString(sqlDr["CompanyIDNo"].ToString());
                    single_details.CompanyName = Convert.ToString(sqlDr["CompanyName"].ToString());
                    single_details.PlanType = Convert.ToString(sqlDr["PlanType"].ToString());
                    single_details.DateOfGrant = Convert.ToString(sqlDr["DateOfGrant"].ToString());
                    single_details.DateOfExercise = Convert.ToString(sqlDr["DateOfExercise"].ToString());
                    single_details.Price = Convert.ToDecimal(sqlDr["Price"]);
                    single_details.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfGrant"]);
                    single_details.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfExercise"]);
                    single_details.NoOfShares = -1*Convert.ToInt32(sqlDr["NoOfShares"]);
                    single_details.NonExemptGrossAmount = Convert.ToDecimal(sqlDr["NonExemptGrossAmount"]);
                    single_details.GrossAmountGains = Convert.ToDecimal(sqlDr["GrossAmountGains"]);
                    single_details.RecordNo = Convert.ToString(sqlDr["RecordNo"].ToString());
                    single_details.Section = Convert.ToString(sqlDr["Section"].ToString());
                    single_details.FK_ID = Convert.ToInt32(sqlDr["ID"]);
                    single_details.G_Total_L = Convert.ToDecimal(sqlDr["G_Total_L"].ToString());
                    single_details.G_Total_M = Convert.ToDecimal(sqlDr["G_Total_M"].ToString());
                    single_details.G_Total_I = Convert.ToDecimal(sqlDr["G_Total_I"].ToString());
                    single_details.G_Total_J = Convert.ToDecimal(sqlDr["G_Total_J"].ToString());
                    single_details.G_Total_K = Convert.ToDecimal(sqlDr["G_Total_K"].ToString());
                    single_details.Is_Amentment = true;

                    // single_details.G_Total = Convert.ToDecimal(sqlDr["G_Total"].ToString());






                    if (!string.IsNullOrEmpty(single_details.CompanyIDNo))
                    {

                        _listdetails1.Add(single_details);
                    }
                    else
                    {
                        _listdetails1.Add(blank_details);
                    }



                }

                _A8BST1.A8BRECORDDETAILS = _listdetails1;



                if (_A8BST1.A8BRECORDDETAILS.Count > 0)
                {


                    _listA8B2009ST.Add(_A8BST1);
                }

            }






                 foreach (int emp_code in emp_codelist)
                 {

                List<A8BRECORDDETAILS> _listdetails2 = new List<A8BRECORDDETAILS>();




                string sSQL = @"SELECT 
                                               k.ID as A8a2009ST_ID 
                                              ,K.[RecordType]
                                              ,K.[IDType]
                                              ,K.[IDNo]
                                              ,K.[NameLine1]
                                              ,K.[NameLine2]
                                              ,K.[Nationality]
                                              ,K.[Sex]
                                              ,k.[DateOfBirth],
                                               k.[DateOfIncorporation],
                                               l.[ID]
                                              ,l.[CompanyIDType]
                                              ,l.[CompanyIDNo]
                                              ,l.[CompanyName]
                                              ,l.[PlanType]
                                              ,l.[DateOfGrant]
                                              ,l.[DateOfExercise]
                                              ,l.[Price]
                                              ,l.[OpenMarketValueAtDateOfGrant]
                                              ,l.[OpenMarketValueAtDateOfExercise]
                                              ,l.[NoOfShares]
                                              ,l.[NonExemptGrossAmount]
                                              ,l.[GrossAmountGains]
                                              ,l.[FK_A8A2009ST]
                                              ,l.[Total_i]
                                              ,l.[Total_j]
                                              ,l.[Total_k]
                                              ,l.[Total_L]
                                              ,l.[Total_M]
                                              ,l.[RecordNo]
                                              ,l.[Section]
                                              ,l.[G_Total]
                                              ,[G_Total_I]
                                              ,[G_Total_J]
                                              ,[G_Total_K]
                                              ,[G_Total_L]
                                              ,[G_Total_M]
                                        FROM  [A8B2009ST] as k join [A8BRECORDDETAILS] as l on k.ID =l.FK_A8A2009ST
                                        where k.emp_id='" + emp_code + "'and k.AppendixB_year=" + year + "and IS_AMMENDMENT=1";



                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                Session["fk"] = "";

                A8B2009ST _A8BST2 = new A8B2009ST();

                //_A8BST.Fileheader = fileheader;

                while (sqlDr.Read())
                {

                    _A8BST2.ID = Convert.ToInt32(sqlDr["ID"]);
                    _A8BST2.IDNo = Convert.ToString(sqlDr["IDNo"].ToString());
                    _A8BST2.IDType = Convert.ToString(sqlDr["IDType"].ToString());
                    _A8BST2.NameLine1 = Convert.ToString(sqlDr["NameLine1"].ToString());
                    _A8BST2.NameLine2 = Convert.ToString(sqlDr["NameLine2"].ToString());
                    _A8BST2.Nationality = Convert.ToString(sqlDr["Nationality"].ToString());
                    _A8BST2.RecordType = Convert.ToString(sqlDr["RecordType"].ToString());
                    _A8BST2.Sex = Convert.ToString(sqlDr["Sex"].ToString());
                    _A8BST2.DateOfBirth = Convert.ToString(sqlDr["DateOfBirth"].ToString());
                    _A8BST2.DateOfIncorporation = Convert.ToString(sqlDr["DateOfIncorporation"].ToString());
                    single_details = new A8BRECORDDETAILS();
                    single_details.CompanyIDType = "1";//ndo
                    single_details.CompanyIDNo = Convert.ToString(sqlDr["CompanyIDNo"].ToString());
                    single_details.CompanyName = Convert.ToString(sqlDr["CompanyName"].ToString());
                    single_details.PlanType = Convert.ToString(sqlDr["PlanType"].ToString());
                    single_details.DateOfGrant = Convert.ToString(sqlDr["DateOfGrant"].ToString());
                    single_details.DateOfExercise = Convert.ToString(sqlDr["DateOfExercise"].ToString());
                    single_details.Price = Convert.ToDecimal(sqlDr["Price"]);
                    single_details.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfGrant"]);
                    single_details.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfExercise"]);
                    single_details.NoOfShares = Convert.ToInt32(sqlDr["NoOfShares"]);
                    single_details.NonExemptGrossAmount = Convert.ToDecimal(sqlDr["NonExemptGrossAmount"]);
                    single_details.GrossAmountGains = Convert.ToDecimal(sqlDr["GrossAmountGains"]);
                    single_details.RecordNo = Convert.ToString(sqlDr["RecordNo"].ToString());
                    single_details.Section = Convert.ToString(sqlDr["Section"].ToString());
                    single_details.FK_ID = Convert.ToInt32(sqlDr["ID"]);
                    single_details.G_Total_L = Convert.ToDecimal(sqlDr["G_Total_L"].ToString());
                    single_details.G_Total_M = Convert.ToDecimal(sqlDr["G_Total_M"].ToString());
                    single_details.G_Total_I = Convert.ToDecimal(sqlDr["G_Total_I"].ToString());
                    single_details.G_Total_J = Convert.ToDecimal(sqlDr["G_Total_J"].ToString());
                    single_details.G_Total_K = Convert.ToDecimal(sqlDr["G_Total_K"].ToString());
                    single_details.Is_Amentment = false;
                    // single_details.G_Total = Convert.ToDecimal(sqlDr["G_Total"].ToString());






                    if (!string.IsNullOrEmpty(single_details.CompanyIDNo))
                    {

                        _listdetails2.Add(single_details);
                    }
                    else
                    {
                        _listdetails2.Add(blank_details);
                    }



                }


              










                _A8BST2.A8BRECORDDETAILS = _listdetails2;



                if (_A8BST2.A8BRECORDDETAILS.Count > 0)
                {


                    _listA8B2009ST.Add(_A8BST2);
                }

            }














            if (_listA8B2009ST.Count > 0)
            {



                xw.WriteStartDocument();


                // Write the root node
                xw.WriteStartElement("A8B2009", "http://www.iras.gov.sg/A8BDef2009");




                xw.WriteStartElement("A8BHeader");
                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                xw.WriteStartElement("FileHeaderST");



                xw.WriteStartElement("RecordType");
                xw.WriteString(Fileheader.RecordType);
                xw.WriteEndElement();

                xw.WriteStartElement("Source");
                xw.WriteString(Fileheader.Source);
                xw.WriteEndElement();


                xw.WriteStartElement("BasisYear");
                xw.WriteString(Fileheader.BasisYear.ToString());
                xw.WriteEndElement();
                xw.WriteStartElement("PaymentType");
                xw.WriteString(Fileheader.PaymentType);
                xw.WriteEndElement();
                xw.WriteStartElement("OrganizationID");
                xw.WriteString(Fileheader.OrganizationID);
                xw.WriteEndElement();
                xw.WriteStartElement("OrganizationIDNo");
                xw.WriteString(Fileheader.OrganizationIDNo);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonName");
                xw.WriteString(Fileheader.AuthorisedPersonName);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonDesignation");
                xw.WriteString(Fileheader.AuthorisedPersonDesignation);
                xw.WriteEndElement();
                xw.WriteStartElement("EmployerName");
                xw.WriteString(Fileheader.EmployerName);
                xw.WriteEndElement();
                xw.WriteStartElement("Telephone");
                xw.WriteString(Fileheader.Telephone);
                xw.WriteEndElement();
                xw.WriteStartElement("AuthorisedPersonEmail");
                xw.WriteString(Fileheader.AuthorisedPersonEmail);
                xw.WriteEndElement();
                xw.WriteStartElement("BatchIndicator");
                xw.WriteString("A");
                xw.WriteEndElement();
                xw.WriteStartElement("BatchDate");
                xw.WriteString(Fileheader.BatchDate);
                xw.WriteEndElement();
                xw.WriteStartElement("IncorporationDate");
                xw.WriteString(Fileheader.IncorporationDate);
                   xw.WriteEndElement();
                xw.WriteStartElement("DivisionOrBranchName");
                xw.WriteString(Fileheader.DivisionOrBranchName);
                xw.WriteEndElement();




                xw.WriteEndElement();//FT
                xw.WriteEndElement();//esubmission
                xw.WriteEndElement();//header




                xw.WriteStartElement("Details");



                foreach (A8B2009ST _A8B2009ST in _listA8B2009ST)
                {


                    xw.WriteStartElement("A8BRecord");
                    xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                    xw.WriteStartElement("A8B2009ST");

                    xw.WriteStartElement("RecordType", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.RecordType);
                    xw.WriteEndElement();

                    xw.WriteStartElement("IDType", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.IDType);
                    xw.WriteEndElement();

                    xw.WriteStartElement("IDNo", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.IDNo);
                    xw.WriteEndElement();

                    xw.WriteStartElement("NameLine1", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.NameLine1);
                    xw.WriteEndElement();


                    xw.WriteStartElement("NameLine2", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.NameLine2);
                    xw.WriteEndElement();


                    xw.WriteStartElement("Nationality", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.Nationality);//ndo
                    xw.WriteEndElement();

                    xw.WriteStartElement("Sex", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.Sex);
                    xw.WriteEndElement();



                    xw.WriteStartElement("DateOfBirth", "http://www.iras.gov.sg/A8B2009");
                    xw.WriteString(_A8B2009ST.DateOfBirth);
                    xw.WriteEndElement();

                    int k = 0;
                    int h = 0;


                    A8BRECORDDETAILS details;

                    decimal TL = 0.00m;
                    decimal TM = 0.00m;
                    decimal TI = 0.00m;
                    decimal TJ = 0.00m;
                    decimal TK = 0.00m;
                    decimal Gr_total_L = 0.00m;
                    decimal Gr_total_M = 0.00m;


                    for (int g = 1; g <= 60; g = g + 1)
                    {


                        k = k + 1;


                        if (k == 1 || k == 2 || k == 3 || k == 16 || k == 17 || k == 18 || k == 31 || k == 32 || k == 33 || k == 46 || k == 47 || k == 48)
                        {


                            details = _A8B2009ST.A8BRECORDDETAILS[h];
                            h = h + 1;


                            //if (k == 1 || k == 16 || k == 31 || k == 46)
                            //{
                            //    TL = details.G_Total_L;
                            //    TM = details.G_Total_M;
                            //    TI = details.G_Total_I;
                            //    TJ = details.G_Total_J;
                            //    TK = details.G_Total_K;
                            //}
                            //section A Trailer

                            if (k == 1 || k == 16 || k == 31 || k == 46)
                            {
                                TL = 0.00m;
                                TM = 0.00m;
                                TI = 0.00m;
                                TJ = 0.00m;
                                TK = 0.00m;

                            }


                            if (k == 1 || k == 2 || k == 3)
                            {

                                TL += details.Total_L;
                                TM += details.Total_M;
                                TI += details.Total_I;
                                TJ += details.Total_J;
                                TK += details.Total_K;

                                SectionATrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                SectionATrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                            }
                            //section B Trailer
                            if (k == 16 || k == 17 || k == 18)
                            {
                                TL += details.Total_L;
                                TM += details.Total_M;
                                TI += details.I_J_K;


                                SectionBTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                SectionBTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                SectionBTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                            }
                            // section C Trailer
                            if (k == 31 || k == 32 || k == 33)
                            {
                                TL += details.Total_L;
                                TM += details.Total_M;

                                TJ += details.I_J_K;


                                SectionCTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                SectionCTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                SectionCTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                            }
                            // section D Trailer
                            if (k == 46 || k == 47 || k == 48)
                            {
                                TL += details.Total_L;
                                TM += details.Total_M;

                                TK += details.I_J_K;

                                SectionDTrailerNonExemptTotalGrossAmountAfter2003 += details.Total_L;
                                SectionDTrailerGainsTotalGrossAmountAfter2003 += details.Total_M;
                                SectionDTrailerExemptTotalGrossAmountAfter2003 += details.I_J_K;
                            }


                        }
                        else
                        {
                            details = blank_details;
                        }




                        Gr_total_L += details.Total_L;
                        Gr_total_M += details.Total_M;

                        xw.WriteStartElement("Record" + k.ToString(), "http://www.iras.gov.sg/A8B2009");

                        xw.WriteStartElement("CompanyIDType", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyIDType.ToString());
                        xw.WriteEndElement();


                        xw.WriteStartElement("CompanyIDNo", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyIDNo.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("CompanyName", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.CompanyName);
                        xw.WriteEndElement();

                        xw.WriteStartElement("PlanType", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.PlanType);
                        xw.WriteEndElement();

                        xw.WriteStartElement("DateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.DateOfGrant);
                        xw.WriteEndElement();



                        xw.WriteStartElement("DateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.DateOfExercise);
                        xw.WriteEndElement();

                        xw.WriteStartElement("Price", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.Price.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("OpenMarketValueAtDateOfGrant", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.OpenMarketValueAtDateOfGrant.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("OpenMarketValueAtDateOfExercise", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.OpenMarketValueAtDateOfExercise.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("NoOfShares", "http://www.iras.gov.sg/SchemaTypes");
                        //if (details.Is_Amentment)
                        //{
                        //    xw.WriteString("-"+details.NoOfShares.ToString());
                        //}
                        //else
                        //{
                            xw.WriteString(details.NoOfShares.ToString());
                        //}
                        xw.WriteEndElement();

                        if (k > 15)
                        {
                            xw.WriteStartElement("ExemptGrossAmountUnderERIS", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(details.I_J_K.ToString());
                            xw.WriteEndElement();

                        }

                        xw.WriteStartElement("NonExemptGrossAmount", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.NonExemptGrossAmount.ToString());
                        xw.WriteEndElement();

                        xw.WriteStartElement("GrossAmountGains", "http://www.iras.gov.sg/SchemaTypes");
                        xw.WriteString(details.GrossAmountGains.ToString());
                        xw.WriteEndElement();


                        xw.WriteEndElement();

                        if (k == 15)
                        {
                            xw.WriteStartElement("SectionATotals", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteEndElement();

                        }

                        if (k == 30)
                        {
                            xw.WriteStartElement("SectionBTotals", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TI.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteEndElement();

                        }

                        if (k == 45)
                        {
                            xw.WriteStartElement("SectionCTotals", "http://www.iras.gov.sg/A8B2009");


                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TJ.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteEndElement();
                        }

                        if (k == 60)
                        {
                            xw.WriteStartElement("SectionDTotals", "http://www.iras.gov.sg/A8B2009");


                            xw.WriteStartElement("TotalGrossAmountExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TK.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TL.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountNonExemptBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(TM.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("TotalGrossAmountGainsBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            SectionETrailerNonExemptGrandTotalGrossAmountAfter2003 += Gr_total_L;
                            SectionETrailerGainsGrandTotalGrossAmountAfter2003 += Gr_total_M;

                            //SECTION E
                            xw.WriteEndElement();


                            xw.WriteStartElement("SectionE", "http://www.iras.gov.sg/A8B2009");

                            xw.WriteStartElement("NonExemptGrandTotalGrossAmountAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_L.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("NonExemptGrandTotalGrossAmountBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("GainsGrandTotalGrossAmountAfter2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString(Gr_total_M.ToString());
                            xw.WriteEndElement();

                            xw.WriteStartElement("GainsGrandTotalGrossAmountBefore2003", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("0.00");
                            xw.WriteEndElement();

                            xw.WriteStartElement("Remarks", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("");
                            xw.WriteEndElement();

                            xw.WriteStartElement("Filler", "http://www.iras.gov.sg/SchemaTypes");
                            xw.WriteString("");
                            xw.WriteEndElement();

                            xw.WriteEndElement();






                        }





                    }







                    xw.WriteEndElement();//a89st
                    xw.WriteEndElement();//esubmission

                    xw.WriteEndElement();//a8arecrd
                }

                xw.WriteEndElement();//details




                xw.WriteStartElement("A8BTrailer");

                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                xw.WriteStartElement("A8BTrailer2009ST");

                xw.WriteStartElement("RecordType");
                xw.WriteString("2");
                xw.WriteEndElement();

                xw.WriteStartElement("NoOfRecords");
                xw.WriteString(_listA8B2009ST.Count.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionATrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionATrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionATrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionATrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionATrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionBTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionBTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionBTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionCTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionCTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionCTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerNonExemptTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerNonExemptTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerNonExemptTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerNonExemptTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerGainsTotalGrossAmountAfter2003");
                xw.WriteString(SectionDTrailerGainsTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionDTrailerGainsTotalGrossAmountBefore2003");
                xw.WriteString(SectionDTrailerGainsTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerNonExemptGrandTotalGrossAmountAfter2003");
                xw.WriteString(SectionETrailerNonExemptGrandTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerNonExemptGrandTotalGrossAmountBefore2003");
                xw.WriteString(SectionETrailerNonExemptGrandTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerGainsGrandTotalGrossAmountAfter2003");
                xw.WriteString(SectionETrailerGainsGrandTotalGrossAmountAfter2003.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("SectionETrailerGainsGrandTotalGrossAmountBefore2003");
                xw.WriteString(SectionETrailerGainsGrandTotalGrossAmountBefore2003.ToString());
                xw.WriteEndElement();


                xw.WriteStartElement("Filler");
                xw.WriteString("Filler");
                xw.WriteEndElement();




                xw.WriteEndElement();//end A8Btrailer
                xw.WriteEndElement();//Esubmisition
                xw.WriteEndElement();//A8BTailer







                xw.WriteEndDocument();//end









                xw.Flush();




                Response.AddHeader("Content-Disposition", "attachment;filename=A8B.xml");
                Response.ContentType = "application/xml";

                Response.BinaryWrite(ms.ToArray());
                Response.End();
                ms.Close();

            }
            else
            {
                this.lblerror.Text = "Error";
            }

        }

        private bool check_Is_AppendixB(int year, int emp_id)
        {
            string stock_options = null;
            bool result = false;

            DataSet ds_ir8a = new DataSet();
            string sqlQuery = "select stock_options from employee_ir8a where emp_id =" + emp_id + " and ir8a_year='" + year + "'";
            ds_ir8a = getDataSetAPP(sqlQuery);
            if (ds_ir8a.Tables[0].Rows.Count > 0)
            {

                stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"].ToString();
            }

            if (!string.IsNullOrEmpty(stock_options))
            {
                if (stock_options == "Yes")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }


            return result;

        }

     private  void generate_appendixB_XML()
        {
            lblerror.Text = "";
            string bonusDate_f = "";
            string directDate_f = "";

            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("Emp_ID"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", year);
            parms[1] = new SqlParameter("@companyid", comp_id);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = year;
            xmlheader.PaymentType = "13";
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;


            //List<A8B2009ST> _A8B2009ST = new List<A8B2009ST>();    

            List<int> _listemp_id = new List<int>();





            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));
                        //int yearCode = Utility.ToInteger(cmbYear.SelectedValue);
                        if (check_Is_AppendixB(year, empid))
                        {

                            _listemp_id.Add(empid);
                        }
                    }
                }

            }

            if (_listemp_id.Count > 0)
            {
                AppdixBXml(_listemp_id, xmlheader);

            }
            else
            {
               this.lblerror.Text = "Please Select Atleast One Employee for Appendix B or you selected Employe not applicaple for Appendix B ";
            }


        }








        private double GetValue(DataSet ir8aEmpDetails, int i, string p)
        {
            try
            {
                #region Temp log
                //using (StreamWriter w = File.AppendText("c:\\log.txt"))
                //{
                //    w.WriteLine(p);
                //}
                #endregion
                return Convert.ToDouble(ir8aEmpDetails.Tables[0].Rows[i][p].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        //private void generate_ir8axml_ammendment()
        //{

        //    string emp_Id = "0";
        //    bool chkChecked = false;

        //    List<ir8a_Amendment> _ir8a_amentment = new List<ir8a_Amendment>();


        //    //SqlParameter[] parms = new SqlParameter[2];
        //    //parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue) - 1);
        //    //parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

        //    //dsFill = DataAccess.ExecuteSPDataSet("sp_ir8s_amendment", parms);




        //    foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (item is GridItem)
        //        {

        //            GridDataItem dataItem = (GridDataItem)item;
        //            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
        //            if (chkBox.Checked == true)
        //            {
        //                chkChecked = true;
        //                int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));

        //                using (ISession session = NHibernateHelper.GetCurrentSession())
        //                {



        //                    //int _year = Utility.ToInteger(cmbYear.SelectedValue) - 1;


        //                    IQuery query = session.CreateSQLQuery(@" SELECT * FROM [ir8a_Amendment]where Emp_ID=" + empid.ToString() + " AND IRYear=" + year.ToString()).AddEntity(typeof(ir8a_Amendment));


        //                    if (query.List<ir8a_Amendment>().Count > 0)
        //                    {
        //                        _ir8a_amentment.Add(query.List<ir8a_Amendment>()[0]);


        //                    }

        //                }






        //                emp_Id = emp_Id + "," + empid;
        //            }
        //        }

        //    }

        //    string SQL = "sp_EMP_IR8A_DETAILS_All";
        //    DataSet ir8aEmpDetails;
        //    string[] bonusDate1;
        //    string[] directDate1;



        //    //bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
        //    //directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

        //    //bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
        //    //directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

        //    if (chkChecked)
        //    {
        //        try
        //        {
        //            SqlParameter[] parms = new SqlParameter[5];
        //            parms[0] = new SqlParameter("@year", year);
        //            parms[1] = new SqlParameter("@companyid", comp_id);
        //            parms[2] = new SqlParameter("@EmpCode", emp_Id);
        //            parms[3] = new SqlParameter("@BonusDate", "12/10/2013");
        //            parms[4] = new SqlParameter("@DirectorsFeesDate", "12/10/2013");

        //            ir8aEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);

        //            for (int i = 0; i < ir8aEmpDetails.Tables[0].Rows.Count; i++)
        //            {
        //                string payment_from_date = ir8aEmpDetails.Tables[0].Rows[i][36].ToString();

        //                string payment_to_date = ir8aEmpDetails.Tables[0].Rows[i][37].ToString();

        //                string directorfee_abroval_date = ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"].ToString();

        //                string GrossCommissionPeriodFrom = ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodFrom"].ToString();

        //                string GrossCommissionPeriodTo = ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodTo"].ToString();


        //                for (int empColumn = 35; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 18; empColumn++)
        //                {
        //                    //string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());


        //                    if (empColumn < 49)
        //                    {
        //                        ir8aEmpDetails.Tables[0].Rows[i][empColumn] = 0;
        //                    }
        //                    else if (empColumn > 49 && empColumn < 65)
        //                    {
        //                        ir8aEmpDetails.Tables[0].Rows[i][empColumn] = "";
        //                    }
        //                    else if (empColumn > 65 && empColumn < 77)
        //                    {
        //                        ir8aEmpDetails.Tables[0].Rows[i][empColumn] = 0;
        //                    }
        //                    else
        //                    {
        //                        ir8aEmpDetails.Tables[0].Rows[i][empColumn] = "";
        //                    }
        //                }

        //                string strNationality = ir8aEmpDetails.Tables[0].Rows[i]["Nationality"].ToString();
        //                string strSQL = "";
        //                strSQL = "Select ir8a_code from Nationality where Nationality='" + strNationality + "'";
        //                DataSet dsNationality = DataAccess.FetchRS(CommandType.Text, strSQL, null);

        //                ir8aEmpDetails.Tables[0].Rows[i]["Nationality"] = dsNationality.Tables[0].Rows[0][0].ToString();

        //                decimal Others = _ir8a_amentment[i].Funds + _ir8a_amentment[i].TransAllow + _ir8a_amentment[i].EntAllow + _ir8a_amentment[i].OtherAllow + _ir8a_amentment[i].Commission + _ir8a_amentment[i].Pension;

        //                ir8aEmpDetails.Tables[0].Rows[i][36] = payment_from_date;
        //                ir8aEmpDetails.Tables[0].Rows[i][37] = payment_to_date;
        //                ir8aEmpDetails.Tables[0].Rows[i][38] = _ir8a_amentment[i].MBMF.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i][40] = _ir8a_amentment[i].EmployeeCPF.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i][43] = _ir8a_amentment[i].Bonus.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i][44] = _ir8a_amentment[i].DirectorFee.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["Salary"] = _ir8a_amentment[i].GrossPay.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i][45] = Others.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionAmount"] = _ir8a_amentment[i].Commission.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["Pension"] = _ir8a_amentment[i].Pension.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["TransportAllowance"] = _ir8a_amentment[i].TransAllow.ToString();

        //                ir8aEmpDetails.Tables[0].Rows[i]["EntertainmentAllowance"] = _ir8a_amentment[i].EntAllow.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["OtherAllowance"] = _ir8a_amentment[i].OtherAllow.ToString();
        //                ir8aEmpDetails.Tables[0].Rows[i]["EmployerContributionToPensionOrPFOutsideSg"] = _ir8a_amentment[i].Funds.ToString();




        //                if (_ir8a_amentment[i].DirectorFee > 0)
        //                {
        //                    ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"] = directorfee_abroval_date;



        //                }



        //                if (_ir8a_amentment[i].Commission > 0)
        //                {
        //                    ir8aEmpDetails.Tables[0].Rows[i]["DirectorsFeesApprovalDate"] = directorfee_abroval_date;
        //                    ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodFrom"] = GrossCommissionPeriodFrom;

        //                    ir8aEmpDetails.Tables[0].Rows[i]["GrossCommissionPeriodTo"] = GrossCommissionPeriodTo;
        //                }




        //                ir8aEmpDetails.Tables[0].Rows[i]["Amount"] = _ir8a_amentment[i].GrossPay + _ir8a_amentment[i].DirectorFee + _ir8a_amentment[i].Bonus + Others;



        //            }


        //            ir8aEmpDetails.AcceptChanges();
        //            overWriteIR8AXml();
        //            appendIR8AHeaderXml(ir8aEmpDetails);
        //            appendIR8ATemplateXml(ir8aEmpDetails);
        //            appendIR8ATrailerXml();





        //        }
        //        catch (Exception ex)
        //        {
        //            this.lblerror.Text = ex.Message.ToString();
        //        }
        //    }
        //    else
        //    {

        //        this.lblerror.Text = "Please Select Atleast One Employee";
        //    }



        //}




        private void overWriteIR8AXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/IR8aTemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8A_AMENDMENT.xml");
                if (File.Exists(sSource) == true)
                {
                    if (File.Exists(sDestn) == true)
                    {
                        File.Copy(sSource, sDestn, true);
                    }
                    else
                    {
                        File.Copy(sSource, sDestn);
                    }
                }

            }
            catch (FileNotFoundException exFile)
            {
                Response.Write(exFile.Message.ToString());
            }

        }
        private void appendIR8AHeaderXml(DataSet ir8aEmpDetails)
        {
            //int year = Utility.ToInteger(cmbYear.SelectedValue) - 1;

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = year.ToString();
            header["PaymentType"].InnerText = "08";
            header["OrganizationID"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "A";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            header["BatchDate"].InnerText = today_Date;
            xdoc.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            xdoc = null;
        }
        private void appendIR8ATemplateXml(DataSet ir8aEmpDetails)
        {


            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));


            XmlElement xelement = null;


            // xelement = document.CreateElement("Resources");
            document.PreserveWhitespace = true;
            //dirFeedate = Convert.ToDateTime(DircetorDate.SelectedDate);
            //bonusDeclataiondate = Convert.ToDateTime(BonusDate.SelectedDate);
            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {
                XmlNode section1 = document.CreateElement("IR8ARecord", "http://www.iras.gov.sg/IR8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("IR8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                // XmlNode section = document.CreateElement("IR8AST", "http://www.iras.gov.sg/IR8ADef");
                //for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 15; empColumn++)
                //r

                for (int empColumn = 16; empColumn < ir8aEmpDetails.Tables[0].Columns.Count - 18; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/IR8A");


                    if (ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString() != "")
                    {
                        key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
                    }
                    section3.AppendChild(key);

                }
                // document.DocumentElement.ChildNodes[1].AppendChild(section);



                section2.AppendChild(section3);
                section1.AppendChild(section2);

                document.DocumentElement.ChildNodes[1].AppendChild(section1);
            }
            document.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            document = null;
        }
        private void appendIR8ATrailerXml()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            xmlnsManager.AddNamespace("sm3", "http://www.iras.gov.sg/IR8A");
            XmlNode trailer;
            XmlNodeList trailer2;
            trailer = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8ATrailer/sm:ESubmissionSDSC/sm:IR8ATrailerST", xmlnsManager);

            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Salary", xmlnsManager);
            string Salary = "";
            string bonus = "";
            string directorFee = "";
            foreach (XmlNode salaryNode in trailer2)
            {
                Salary = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Salary) + Utility.ToDouble(salaryNode.InnerText.ToString())));
            }

            string trailerText = trailer.InnerText;

            trailer["RecordType"].InnerText = "2";
            trailer["NoOfRecords"].InnerText = trailer2.Count.ToString();
            trailer["TotalSalary"].InnerText = Salary;
            string Bonus = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Bonus", xmlnsManager);
            foreach (XmlNode TBonus in trailer2)
            {
                Bonus = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Bonus) + Utility.ToDouble(TBonus.InnerText.ToString())));
            }
            trailer["TotalBonus"].InnerText = Bonus;

            string DirectorsFee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:DirectorsFees", xmlnsManager);
            foreach (XmlNode DirectorsFees in trailer2)
            {
                DirectorsFee = Convert.ToString(Convert.ToInt64(Utility.ToDouble(DirectorsFee) + Utility.ToDouble(DirectorsFees.InnerText.ToString())));
            }
            trailer["TotalDirectorsFees"].InnerText = DirectorsFee;
            string OTHERS = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Others", xmlnsManager);
            foreach (XmlNode OTHER in trailer2)
            {
                OTHERS = Convert.ToString(Convert.ToInt64(Utility.ToDouble(OTHERS) + Utility.ToDouble(OTHER.InnerText.ToString())));
            }
            trailer["TotalOthers"].InnerText = OTHERS;

            trailer["TotalPayment"].InnerText = Convert.ToString(Convert.ToInt64(Utility.ToDouble(Salary) + Utility.ToDouble(Bonus) + Utility.ToInteger(DirectorsFee) + Utility.ToDouble(OTHERS)));

            string ExemptIncome = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:ExemptIncome", xmlnsManager);
            foreach (XmlNode ExemptIncomes in trailer2)
            {
                ExemptIncome = Convert.ToString(Convert.ToInt64(Utility.ToDouble(ExemptIncome) + Utility.ToDouble(ExemptIncomes.InnerText.ToString())));
            }
            trailer["TotalExemptIncome"].InnerText = ExemptIncome;

            string totalTaxBorneByEmployer = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployer", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployers in trailer2)
            {
                totalTaxBorneByEmployer = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalTaxBorneByEmployer) + Utility.ToDouble(totalTaxBorneByEmployers.InnerText.ToString())));
            }
            trailer["TotalIncomeForTaxBorneByEmployer"].InnerText = totalTaxBorneByEmployer;

            string totalTaxBorneByEmployee = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:IncomeForTaxBorneByEmployee", xmlnsManager);
            foreach (XmlNode totalTaxBorneByEmployees in trailer2)
            {
                totalTaxBorneByEmployee = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalTaxBorneByEmployee) + Utility.ToDouble(totalTaxBorneByEmployees.InnerText.ToString())));
            }
            trailer["TotalIncomeForTaxBorneByEmployee"].InnerText = totalTaxBorneByEmployee;
            string totalDonation = "";
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Donation", xmlnsManager);
            foreach (XmlNode totalDonations in trailer2)
            {
                totalDonation = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalDonation) + Utility.ToDouble(totalDonations.InnerText.ToString())));
            }
            trailer["TotalDonation"].InnerText = totalDonation;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:CPF", xmlnsManager);
            string cpf = "";
            foreach (XmlNode Tcpf in trailer2)
            {
                cpf = Convert.ToString(Convert.ToInt64(Utility.ToDouble(cpf) + Utility.ToDouble(Tcpf.InnerText.ToString())));
            }
            trailer["TotalCPF"].InnerText = cpf;

            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:Insurance", xmlnsManager);
            string totalInsurance = "";
            foreach (XmlNode insurancet in trailer2)
            {
                totalInsurance = Convert.ToString(Convert.ToInt64(Utility.ToDouble(totalInsurance) + Utility.ToDouble(insurancet.InnerText.ToString())));
            }
            trailer["TotalInsurance"].InnerText = totalInsurance;
            trailer2 = xdoc.SelectNodes("sm2:IR8A/sm2:Details/sm2:IR8ARecord/sm:ESubmissionSDSC/sm:IR8AST/sm3:MBF", xmlnsManager);
            string MBF = "";
            foreach (XmlNode MBFt in trailer2)
            {
                MBF = Convert.ToString(Convert.ToInt64(Utility.ToDouble(MBF) + Utility.ToDouble(MBFt.InnerText.ToString())));
            }
            trailer["TotalMBF"].InnerText = MBF;
            trailer["TotalExemptIncome"].InnerText = "0";
            trailer["Filler"].InnerText = "0";

            xdoc.Save(Server.MapPath("~/XML/IR8A_AMENDMENT.xml"));
            string FilePath = Server.MapPath("~/XML/IR8A_AMENDMENT.xml");

            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/XML";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();

            xdoc = null;
        }








        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }


        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridCommandItem item = e.Item as GridCommandItem;
            if (item != null)
            {
                Button btn = item.FindControl("btnsubmit") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                lblMessage.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            /* To disable Grid filtering options  */

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            lblerror.Text = "";
            comp_id = Utility.ToInteger(Session["Compid"]);

            if (!IsPostBack)
            {
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
            }

            year = Utility.ToInteger(cmbYear.SelectedValue) - 1;
            //Response.Write("<script l");

            connection = Session["ConString"].ToString();

            this.BonusDate.SelectedDate = DateTime.Now;
            this.DircetorDate.SelectedDate = DateTime.Now;


            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);

        }

        public void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            bindTable();
        }


        bool output;
        DataSet dsFill = new DataSet();
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {

            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@year", year);
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

            dsFill = DataAccess.ExecuteSPDataSet("sp_ir8s_amendment", parms);
            RadGrid1.DataSource = dsFill;
            RadGrid1.DataBind();

        }

        private void bindTable()
        {
            //if (chkId.Checked)
            //{
            //    sSQL = "sp_emp_yearearn_Temp";
            //}
            //else
            //{
            //    sSQL = "sp_emp_yearearn";
            //}
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@year", year);
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

            dsFill = DataAccess.ExecuteSPDataSet("sp_ir8s_amendment", parms);
            RadGrid1.DataSource = dsFill;

        }

        private void bindTable1()
        {
            sSQL = "sp_ir8s_amendment";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@year", year);
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));

            dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid1.DataSource = dsFill;

            // chkId.Checked = false;

        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected static DataSet getDataSetAPP(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        private bool check_Is_AppendixA(int year, int emp_id)
        {
            string benefits = null;
            bool result = false;

            DataSet ds_ir8a = new DataSet();
            string sqlQuery = "select benefits_in_kind from employee_ir8a where emp_id =" + emp_id + " and ir8a_year='" + year + "'";
            ds_ir8a = getDataSetAPP(sqlQuery);
            if (ds_ir8a.Tables[0].Rows.Count > 0)
            {

                benefits = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"].ToString();
            }

            if (!string.IsNullOrEmpty(benefits))
            {
                if (benefits == "Yes")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }


            return result;

        }


        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            A8AST orginal_appx_data;


            if (e.Item is GridDataItem)
            {
                if (e.CommandName == "AmendA")
                {
                    GridDataItem dataItem = (GridDataItem)e.Item;


                    if (check_Is_AppendixA(year, Utility.ToInteger(dataItem["Emp_ID"].Text)))
                    {
                        Response.Redirect("Ammend_AppA.aspx?empcode=" + dataItem["Emp_ID"].Text + "&year=" + year + "&name=" + dataItem["emp_name"].Text);


                    }
                    else
                    {
                        lblerror.Text = "No  Original record found for Appedix A";




                    }


                }
                else if (e.CommandName == "AmendB")
                {
                    GridDataItem dataItem = (GridDataItem)e.Item;


                    if (check_Is_AppendixB(year, Utility.ToInteger(dataItem["Emp_ID"].Text)))
                    {
                        Response.Redirect("AMD_APPB.aspx?empcode=" + dataItem["Emp_ID"].Text + "&year=" + year + "&name=" + dataItem["emp_name"].Text);


                    }
                    else
                    {
                        lblerror.Text = "No  Original record found for Appedix B";




                    }


                }


            }



            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                string stryear = year.ToString();
              
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                if (e.CommandName == "UpdateAll")
                                {
                                    string id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                                    string Emp_ID = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));

                                    TextBox txtGrossPay = (TextBox)dataItem.FindControl("txtGrossPay");
                                    TextBox txtBonus = (TextBox)dataItem.FindControl("txtBonus");
                                    TextBox txtDirectorFee = (TextBox)dataItem.FindControl("txtDirectorFee");
                                    TextBox txtCommission = (TextBox)dataItem.FindControl("txtCommission");
                                    TextBox txtPension = (TextBox)dataItem.FindControl("txtPension");
                                    TextBox txtTransAllow = (TextBox)dataItem.FindControl("txtTransAllow");
                                    TextBox txtEntAllow = (TextBox)dataItem.FindControl("txtEntAllow");
                                    TextBox txtOtherAllow = (TextBox)dataItem.FindControl("txtOtherAllow");
                                    TextBox txtEmployeeCPF = (TextBox)dataItem.FindControl("txtEmployeeCPF");
                                    TextBox txtFunds = (TextBox)dataItem.FindControl("txtFunds");
                                    TextBox txtMBMF = (TextBox)dataItem.FindControl("txtMBMF");


                                    double dbltxtGrossPay = Utility.ToDouble(txtGrossPay.Text);
                                    double dbltxtBonus = Utility.ToDouble(txtBonus.Text);
                                    double dbltxtDirectorFee = Utility.ToDouble(txtDirectorFee.Text);
                                    double dbltxtCommission = Utility.ToDouble(txtCommission.Text);
                                    double dbltxtPension = Utility.ToDouble(txtPension.Text);
                                    double dbltxtTransAllow = Utility.ToDouble(txtTransAllow.Text);
                                    double dbltxtEntAllow = Utility.ToDouble(txtEntAllow.Text);
                                    double dbltxtOtherAllow = Utility.ToDouble(txtOtherAllow.Text);
                                    double dbltxtEmployeeCPF = Utility.ToDouble(txtEmployeeCPF.Text);
                                    double dbltxtFunds = Utility.ToDouble(txtFunds.Text);
                                    double dbltxtMBMF = Utility.ToDouble(txtMBMF.Text);

                                    sSQL = "";


                                    if (chkId.Checked == false)//if importing
                                    {
                                        if ((id == "") && ((dbltxtGrossPay >= 0) || (dbltxtBonus >= 0) || (dbltxtDirectorFee >= 0) || (dbltxtCommission >= 0) || (dbltxtPension >= 0) || (dbltxtTransAllow >= 0) || (dbltxtEntAllow >= 0) || (dbltxtOtherAllow >= 0) || (dbltxtEmployeeCPF >= 0) || (dbltxtFunds >= 0) || (dbltxtMBMF >= 0)))
                                        {
                                            sSQL = "Insert into ir8a_Amendment (Emp_ID,IRYear,GrossPay,Bonus,DirectorFee,Commission,Pension,TransAllow,EntAllow,OtherAllow,EmployeeCPF,Funds,MBMF,ISAmendment) values(" + Emp_ID + ",'" + stryear + "'," + dbltxtGrossPay + "," + dbltxtBonus + "," + dbltxtDirectorFee + "," + dbltxtCommission + "," + dbltxtPension + "," + dbltxtTransAllow + "," + dbltxtEntAllow + "," + dbltxtOtherAllow + "," + dbltxtEmployeeCPF + "," + dbltxtFunds + "," + dbltxtMBMF + ",'A')";
                                        }
                                        else if ((id != ""))
                                        {
                                            sSQL = "Update ir8a_Amendment Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + ",ISAmendment='A' Where ID =" + id;
                                        }
                                    }
                                    else
                                    {//NDO
                                        sSQL = "Update ir8a_Amendment Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + ",ISAmendment=A Where Emp_id =" + Emp_ID + "AND [IRYear]=" + year.ToString();
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();
                                    }
                                }

                        //bindTable1();
                                //RadGrid1.DataBind();

                                else if (e.CommandName == "gen_ir8a_ammndment")
                                {
                                  //  generate_ir8axml_ammendment();
                                }


                                else if (e.CommandName == "gen_appA_ammndment")
                                {
                                    generate_appendixA_XML();
                                }
                                else if (e.CommandName == "gen_appB_ammndment")
                                {
                                    generate_appendixB_XML();
                                }
                            }

                            else
                            {
                                lblerror.Text= "Please Select Atleast One Employee";
                            }

            }
        }





                ((Button)commandItem.FindControl("btnsubmit")).Enabled = true;

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }


        #region Import from Excel
        protected void chkId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                FileUpload.Visible = true;

            }
            else
            {
                FileUpload.Visible = false;

            }
        }


        bool res;
        //protected bool ExcelImport()
        //{

        //    string strMsg = "";
        //    if (FileUpload.PostedFile != null) //Checking for valid file
        //    {
        //        string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
        //        string strorifilename = StrFileName;
        //        string StrFileType = FileUpload.PostedFile.ContentType;
        //        int IntFileSize = FileUpload.PostedFile.ContentLength;
        //        //Checking for the length of the file. If length is 0 then file is not uploaded.
        //        if (IntFileSize <= 0)
        //        {
        //            strMsg = "Please Select File to be uploaded";
        //            ShowMessageBox("Please Select File to be uploaded");
        //            res = false;
        //        }

        //        else
        //        {
        //            res = true;
        //            int RandomNumber = 0;
        //            RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

        //            string strTranID = Convert.ToString(RandomNumber);
        //            string[] FileExt = StrFileName.Split('.');
        //            string strExtent = "." + FileExt[FileExt.Length - 1];
        //            StrFileName = FileExt[0] + strTranID;
        //            string stfilepath = Server.MapPath(@"..\\Documents\\IR8A\" + StrFileName + strExtent);
        //            try
        //            {
        //                FileUpload.PostedFile.SaveAs(stfilepath);

        //                string filename = StrFileName + strExtent;
        //                ImportExcelTosqlServer(filename);



        //            }
        //            catch (Exception ex)
        //            {
        //                strMsg = ex.Message;
        //            }
        //        }

        //    }
        //    lblerror.Text = strMsg;

        //    return res;
        //}
        string col, Empcode = "", ICNUMBER, Empcode1, sQLFinal;
        int IRYear, j = 0;
        decimal GrossPay, Bonus, DirectorFee, Commission, Pension, TransAllow, EntAllow, OtherAllow, EmployeeCPF, Funds, MBMF;
        DataTable dt;
        public void ImportExcelTosqlServer(string filename)
        {
            dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            //SqlQuery.Append("INSERT INTO [dbo].[EmployeeEarning] ([IRYear],[Emp_ID],[GrossPay],[Bonus] ,[DirectorFee],[Commission],[Pension],[TransAllow],[EntAllow],[OtherAllow],[EmployeeCPF],[Funds] ,[MBMF]) VALUES ");
            try
            {


                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0)//skip the first 1 column
                        {
                            col = dt.Columns[i].ToString();

                            ICNUMBER = dr["IC"].ToString();


                            string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                            SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr_empcode.Read())
                            {
                                Empcode = dr_empcode["emp_code"].ToString();
                            }
                            else
                            {
                                Empcode = "";
                            }


                            if (Empcode != "")
                            {
                                if (j == 0)
                                {
                                    SqlQuery.Append("INSERT INTO [dbo].[EmployeeEarning_Temp] ([IRYear],[Emp_ID],[GrossPay],[Bonus] ,[DirectorFee],[Commission],[Pension],[TransAllow],[EntAllow],[OtherAllow],[EmployeeCPF],[Funds] ,[MBMF]) VALUES ");
                                    j++;
                                }

                                try
                                {
                                    IRYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());

                                    GrossPay = Convert.ToDecimal(Check(dr["GrossPay"].ToString()));
                                    Bonus = Convert.ToDecimal(Check(dr["Bonus"].ToString()));
                                    DirectorFee = Convert.ToDecimal(Check(dr["DirectorFee"].ToString()));
                                    Commission = Convert.ToDecimal(Check(dr["Commission"].ToString()));
                                    Pension = Convert.ToDecimal(Check(dr["Pension"].ToString()));
                                    TransAllow = Convert.ToDecimal(Check(dr["TransAllow"].ToString()));
                                    EntAllow = Convert.ToDecimal(Check(dr["EntAllow"].ToString()));
                                    OtherAllow = Convert.ToDecimal(Check(dr["OtherAllow"].ToString()));
                                    EmployeeCPF = Convert.ToDecimal(Check(dr["EmployeeCPF"].ToString()));
                                    Funds = Convert.ToDecimal(Check(dr["Funds"].ToString()));
                                    MBMF = Convert.ToDecimal(Check(dr["MBMF"].ToString()));
                                }
                                catch (Exception Ex)
                                {
                                    ShowMessageBox("Error for the IC: " + ICNUMBER + " , Error Message:" + Ex.Message.ToString());
                                    return;
                                }
                            }
                        }
                    }


                    if (Empcode != "")
                    {
                        SqlQuery.Append("(" + IRYear + "," + Empcode + "," + GrossPay + "," + Bonus + "," + DirectorFee + "," + Commission + "," + Pension + "," + TransAllow + "," + EntAllow + "," + OtherAllow + "," + EmployeeCPF + "," + Funds + "," + MBMF + "),");
                    }

                }


                int lenCount = Convert.ToInt32(SqlQuery.Length);
                if (lenCount > 0)//remove last comma
                {
                    SqlQuery.Remove(lenCount - 1, 1);
                }
                sQLFinal = SqlQuery.ToString();

                try
                {
                    DataAccess.FetchRS(CommandType.Text, "delete from EmployeeEarning_Temp ", null);
                    DataAccess.FetchRS(CommandType.Text, sQLFinal.ToString(), null);
                }
                catch (Exception exeError)
                {
                    ShowMessageBox("Error While Executing.Msg:" + exeError.Message.ToString());
                    return;
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message.ToString());
                return;
            }

        }
        //if there is no value then return 0
        private string Check(string p)
        {
            if (p == "")
                return "0";
            else
                return p;
        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/IR8A/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                string SheetName = "Sheet1";//here enter sheet name        
                oledbconn.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + SheetName + "$]", oledbconn);
                OleDbDataAdapter oledbda = new OleDbDataAdapter();
                oledbda.SelectCommand = cmdSelect;
                oledbda.Fill(dt);
                oledbconn.Close();
                oledbda = null;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt;
        }



        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }


        public Color ColorChange(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID, Datafield);//get gross value for each cell

            if (Convert.ToInt32(grossvalue) > 0)
                // return Color.Yellow;
                return Color.LightYellow;
            else
                return Color.White;

        }

        public string ToolTipValue(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID, Datafield);//get gross value for each cell
            return grossvalue;
        }


        private string GetGrossvalue(string Emp_ID, string Datafield)
        {
            if (chkId.Checked)
            {
                string sql_check = "select Top 1 " + Datafield + "  from [EmployeeEarning] where Emp_ID=" + Emp_ID + " and IRYear=" + cmbYear.SelectedValue + "";
                SqlDataReader dr_grossvalue = DataAccess.ExecuteReader(CommandType.Text, sql_check, null);
                if (dr_grossvalue.HasRows)
                {
                    if (dr_grossvalue.Read())
                    {
                        grossvalue = dr_grossvalue["" + Datafield + ""].ToString();
                    }
                }
            }
            return grossvalue;
        }

        #endregion
        public string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        private void generate_appendixA_XML()
        {


            lblerror.Text = "";


            string SQL = "sp_EMP_IR8A_DETAILS";
            DataSet AppendixAEmpDetails;
            string[] bonusDate1;
            string[] directDate1;



            bonusDate1 = BonusDate.SelectedDate.Value.ToString().Split('/');
            directDate1 = DircetorDate.SelectedDate.Value.ToString().Split('/');

            string bonusDate_f = bonusDate1[2].ToString().Remove(4, 9) + bonusDate1[1].ToString() + bonusDate1[0].ToString();
            string directDate_f = directDate1[2].ToString().Remove(4, 9) + directDate1[1].ToString() + directDate1[0].ToString();

            int emp_Id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[1].GetDataKeyValue("Emp_ID"));

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@year", year);
            parms[1] = new SqlParameter("@companyid", comp_id);
            parms[2] = new SqlParameter("@EmpCode", emp_Id);
            parms[3] = new SqlParameter("@BonusDate", bonusDate_f);
            parms[4] = new SqlParameter("@DirectorsFeesDate", directDate_f);
            AppendixAEmpDetails = DataAccess.ExecuteSPDataSet(SQL, parms);



            FileHeaderST xmlheader = new FileHeaderST();





            xmlheader.RecordType = "0";
            xmlheader.Source = "6";
            xmlheader.BasisYear = year;
            xmlheader.PaymentType = "08";//ND0
            xmlheader.OrganizationID = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationID"].ToString());
            xmlheader.OrganizationIDNo = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            xmlheader.AuthorisedPersonName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            xmlheader.AuthorisedPersonDesignation = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            xmlheader.EmployerName = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            xmlheader.Telephone = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            xmlheader.AuthorisedPersonEmail = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            xmlheader.BatchIndicator = "A";
            xmlheader.AddressOf_Employer = Utility.ToString(AppendixAEmpDetails.Tables[0].Rows[0]["CompanyAddress"].ToString());
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            today_Date = Convert.ToDateTime(today_Date.ToString()).ToString("yyyyMMdd", format);
            xmlheader.BatchDate = today_Date;

            List<A8AST> _A8AST = new List<A8AST>();

            //using (ISession session = NHibernateHelper.GetCurrentSession())
            //{







                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {

                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        if (chkBox.Checked == true)
                        {



                            int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));
                            //int yearCode = Utility.ToInteger(cmbYear.SelectedValue) - 1;


                            //IQuery query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + empid + "AND AppendixA_year=" + year + "and [IS_AMENDMENT]=1").AddEntity(typeof(A8AST));
                            //if (query.List().Count != 0)
                            //{
                            //    _A8AST.Add(query.List<A8AST>()[0]);

                            //}




                        }
                    }

           //     }

            }

            if (_A8AST.Count > 0)
            {


                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms, wSettings);// Write Declaration

                xw.WriteStartDocument();
                // Write the root node
                xw.WriteStartElement("A8A", "http://www.iras.gov.sg/A8ADef");


                xw.WriteStartElement("A8AHeader");

                xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");


                xw.WriteStartElement("FileHeaderST");



                xw.WriteStartElement("RecordType");
                xw.WriteString(xmlheader.RecordType);
                xw.WriteEndElement();

                xw.WriteStartElement("Source");
                xw.WriteString(xmlheader.Source);
                xw.WriteEndElement();

                xw.WriteStartElement("BasisYear");
                xw.WriteString(xmlheader.BasisYear.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("OrganizationID");
                xw.WriteString(xmlheader.OrganizationID);
                xw.WriteEndElement();

                xw.WriteStartElement("OrganizationIDNo");
                xw.WriteString(xmlheader.OrganizationIDNo);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonName");
                xw.WriteString(xmlheader.AuthorisedPersonName);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonDesignation");
                xw.WriteString(xmlheader.AuthorisedPersonDesignation);
                xw.WriteEndElement();

                xw.WriteStartElement("EmployerName");
                xw.WriteString(xmlheader.EmployerName);
                xw.WriteEndElement();

                xw.WriteStartElement("Telephone");
                xw.WriteString(xmlheader.Telephone);
                xw.WriteEndElement();

                xw.WriteStartElement("AuthorisedPersonEmail");
                xw.WriteString(xmlheader.AuthorisedPersonEmail);
                xw.WriteEndElement();

                xw.WriteStartElement("BatchIndicator");
                xw.WriteString(xmlheader.BatchIndicator);
                xw.WriteEndElement();

                xw.WriteStartElement("BatchDate");
                xw.WriteString(xmlheader.BatchDate);
                xw.WriteEndElement();

                xw.WriteStartElement("DivisionOrBranchName");
                xw.WriteString(xmlheader.DivisionOrBranchName);
                xw.WriteEndElement();

                xw.WriteEndElement(); ////end FileHeaderST
                xw.WriteEndElement(); //ESubmissionSDSC
                xw.WriteEndElement();//end A8Aheader

                xw.WriteStartElement("Details");






                foreach (A8AST details in _A8AST)
                {
                    xw.WriteStartElement("A8ARecord");

                    xw.WriteStartElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");

                    xw.WriteStartElement("A8AST");



                    xw.WriteStartElement("RecordType", "http://www.iras.gov.sg/A8A");

                    xw.WriteString(details.RecordType);

                    xw.WriteEndElement();


                    xw.WriteStartElement("IDType", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.IDType);
                    xw.WriteEndElement();
                    xw.WriteStartElement("IDNo", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.IDNo); xw.WriteEndElement();
                    xw.WriteStartElement("NameLine1", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NameLine1); xw.WriteEndElement();
                    xw.WriteStartElement("NameLine2", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NameLine2);
                    xw.WriteEndElement(); //
                    xw.WriteStartElement("ResidencePlaceValue", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ResidencePlaceValue.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine1", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine1.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine2", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine2.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("ResidenceAddressLine3", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(this.Truncate(details.ResidenceAddressLine3.TrimStart(), 30));
                    xw.WriteEndElement();
                    xw.WriteStartElement("OccupationFromDate", "http://www.iras.gov.sg/A8A");

                    string fromdate = Convert.ToDateTime(details.OccupationFromDate).ToString("yyyyMMdd");
                    xw.WriteString(fromdate);
                    xw.WriteEndElement();
                    xw.WriteStartElement("OccupationToDate", "http://www.iras.gov.sg/A8A");
                    string todate = Convert.ToDateTime(details.OccupationToDate).ToString("yyyyMMdd");
                    xw.WriteString(todate);
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfDays", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("AVOrRentByEmployer", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.AVOrRentByEmployer.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("RentByEmployee", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.RentByEmployee.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ValueFurnitureFitting", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ValueFurnitureFitting.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("HardOrsoftFurnitureItemsValue", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.HardOrsoftFurnitureItemsValue.ToString());
                    xw.WriteEndElement();
                    //xw.WriteStartElement("RefrigeratorValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.RefrigeratorValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfRefrigerators", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfRefrigerators.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("VideoRecorderValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.VideoRecorderValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfVideoRecorders", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfVideoRecorders.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("WashingMachineDryerDishWasherValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.WashingMachineDryerDishWasherValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfWashingMachines", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfWashingMachines.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfDryers", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfDryers.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfDishWashers", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfDishWashers.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("AirConditionerValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.AirConditionerValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfAirConditioners", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfAirConditioners.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfCentralACDining", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfCentralACDining.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfCentralACSitting", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfCentralACSitting.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfCentralACAdditional", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfCentralACAdditional.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("TVRadioAmpHiFiStereoElectriGuitarValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.TVRadioAmpHiFiStereoElectriGuitarValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfTVs", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfTVs.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfRadios", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfRadios.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfAmplifiers", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfAmplifiers.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfHiFiStereos", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfHiFiStereos.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfElectriGuitar", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfElectriGuitar.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("ComputerAndOrganValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.ComputerAndOrganValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfComputers", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfComputers.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfOrgans", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfOrgans.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("SwimmingPoolValue", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.SwimmingPoolValue.ToString());
                    //xw.WriteEndElement();
                    //xw.WriteStartElement("NoOfSwimmingPools", "http://www.iras.gov.sg/A8A");
                    //xw.WriteString(details.NoOfSwimmingPools.ToString());
                    //xw.WriteEndElement();
                    xw.WriteStartElement("UtilitiesTelPagerSuitCaseAccessories", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.UtilitiesTelPagerSuitCaseAccessories.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Telephone", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Telephone.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Pager", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Pager.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Suitcase", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Suitcase.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("GolfBagAndAccessories", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.GolfBagAndAccessories.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Camera", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Camera.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Servant", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ServantGardener.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Driver", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Driver.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("GardenerOrCompoundUpkeep", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.GardenerOrCompoundUpkeep.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("OtherBenefitsInKindValue", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.OtherBenefitsInKindValue.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("HotelAccommodationValue", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.HotelAccommodationValue.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("SelfWifeChildAbove20NoOfPersons", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.SelfWifeChildAbove20NoOfPersons.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("SelfWifeChildAbove20NoOfDays", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.SelfWifeChildAbove20NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("SelfWifeChildAbove20Value", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.SelfWifeChildAbove20Value.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween8And20NoOfPersons", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween8And20NoOfPersons.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween8And20NoOfDays", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween8And20NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween8And20Value", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween8And20Value.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween3And7NoOfPersons", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween3And7NoOfPersons.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween3And7NoOfDays", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween3And7NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBetween3And7Value", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBetween3And7Value.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBelow3NoOfPersons", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBelow3NoOfPersons.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBelow3NoOfDays", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBelow3NoOfDays.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("ChildBelow3Value", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.ChildBelow3Value.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Percent2OfBasic", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.Percent2OfBasic.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("CostOfLeavePassageAndIncidentalBenefits", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.CostOfLeavePassageAndIncidentalBenefits.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageSelf", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NoOfLeavePassageSelf.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageWife", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NoOfLeavePassageSpouse.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NoOfLeavePassageChildren", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NoOfLeavePassageChildren.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("OHQStatus", "http://www.iras.gov.sg/A8A");
                    if (details.CostOfLeavePassageAndIncidentalBenefits > 0.00m)
                    {
                        string X = "N";
                        if (details.OHQStatus)
                            X = "Y";
                        xw.WriteString(X);
                    }
                    else
                    {
                        xw.WriteString("");
                    }
                    xw.WriteEndElement();
                    xw.WriteStartElement("InterestPaidByEmployer", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.InterestPaidByEmployer.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("LifeInsurancePremiumsPaidByEmployer", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.LifeInsurancePremiumsPaidByEmployer.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("FreeOrSubsidisedHoliday", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.FreeOrSubsidisedHoliday.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("EducationalExpenses", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.EducationalExpenses.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("NonMonetaryAwardsForLongService", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NonMonetaryAwardsForLongService.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("EntranceOrTransferFeesToSocialClubs", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.EntranceOrTransferFeesToSocialClubs.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("GainsFromAssets", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.GainsFromAssets.ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("FullCostOfMotorVehicle", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.FullCostOfMotorVehicle.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("CarBenefit", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.CarBenefit.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("OthersBenefits", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.OthersBenefits.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("TotalBenefitsInKind", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.TotalBenefitsInKind.ToString()); xw.WriteEndElement();
                    xw.WriteStartElement("NoOfEmployeesSharingQRS", "http://www.iras.gov.sg/A8A");
                    xw.WriteString(details.NoOfEmployeesSharingQRS.ToString());
                    xw.WriteEndElement();
                    xw.WriteStartElement("Filler", "http://www.iras.gov.sg/A8A");
                    xw.WriteEndElement();
                    xw.WriteStartElement("Remarks", "http://www.iras.gov.sg/A8A");
                    xw.WriteEndElement();


                    xw.WriteEndElement();//record

                    xw.WriteEndElement();//esubmission

                    xw.WriteEndElement();//a8arecrd
                }














                xw.WriteEndElement();//details


                xw.WriteEndDocument();//a8a

                // Flush the write
                xw.Flush();

                Response.AddHeader("Content-Disposition", "attachment;filename=A8A_AMENDMENT.xml");
                Response.ContentType = "application/xml";

                Response.BinaryWrite(ms.ToArray());
                Response.End();
                ms.Close();
            }
        }
























    }
}
