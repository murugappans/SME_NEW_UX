using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Telerik.Charting;
using System.Data;
using Telerik.Charting.Styles;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TestChart
{
    public partial class EmployeeChart : System.Web.UI.Page
    {
        private string strConn;
        private SqlDataAdapter Adpt;
        private DataTable dt=new DataTable();
        ChartSeries chartSeries ;
        private int totalemp;
        private double empPercentage;
        private string Chart_Type;
        private string strSql;
        private string YAxis;
        private string XAxis;
        private int c = 0;
        private int CompID;

        Color[] barColors = new Color[8]{
                   Color.Purple,
                   Color.SteelBlue,
                   Color.Aqua,
                   Color.Yellow,
                   Color.Navy,
                   Color.Green,
                   Color.Blue,
                   Color.Red
               };

        protected void Page_Load(object sender, EventArgs e)
        {
           
            strConn =SMEPayroll.Constants.CONNECTION_STRING;// "Data Source=CHEQUEPRO\\SUMON;Initial Catalog=TEST;User ID=sa;Password=anb@payroll";
            HttpRequest q = Request;
            CompID = int.Parse(q.QueryString["CompID"]);
          
            if (!Page.IsPostBack)
            {
               
                Chart_Type = "Pie";
                Session["Chart_Type"] = Chart_Type;

                RadComboBoxItem Emp = new RadComboBoxItem();
                Emp.Text = "Company";
                Emp.Value = "1";

                RadComboBoxItem Dept = new RadComboBoxItem();
                Dept.Text = "Employee";
                Dept.Value = "2";

                cboXAxis.Items.Add(Dept);
                cboXAxis.Items.Add(Emp);

                dt.Clear();
                Adpt = new SqlDataAdapter("SELECT d.DeptName as Department, Employee=Count(*), Allemp=(select COUNT(*)from employee) FROM employee as e inner join department as d on e.dept_id=d.id GROUP BY d.DeptName Order By Employee Desc", strConn);
                Adpt.Fill(dt);

                dt.Columns.Add("Percentage");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    totalemp = int.Parse(dt.Rows[i][2].ToString());
                    double emp = Int32.Parse(dt.Rows[i][1].ToString());
                    empPercentage = (emp / totalemp) * 100;
                    dt.Rows[i]["Percentage"] = Math.Round( empPercentage,2);
                }

                RadChart1.Series[0].Type = ChartSeriesType.Pie;
                RadChart1.DataSource = dt;
                RadChart1.Series[0].DataYColumn = "Employee";
                RadChart1.PlotArea.XAxis.DataLabelsColumn = "Department";
                RadChart1.PlotArea.XAxis.AddRange(1, dt.Rows.Count, 1);
                RadChart1.PlotArea.XAxis.AxisLabel.TextBlock.Text = "Department";
                RadChart1.DataBind();

                Session["dt"] = dt;
                chkPercentage.Checked = true;
                chkPercentage_CheckedChanged(sender, e);              
               
            }
       
            RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 550, 80, 50);            
         }

          protected void chkPercentage_CheckedChanged(object sender, EventArgs e)
        {

            if (chkPercentage.Checked == true)
            {
                dt = (DataTable)Session["dt"];
                RadChart1.DataSource = dt;
                RadChart1.Series[0].DataYColumn = "Percentage";
                YAxis = (String)Session["YAxis"];
                RadChart1.PlotArea.XAxis.DataLabelsColumn = dt.Columns[0].ColumnName.ToString();
                RadChart1.DataBind();
                RadChart1.PlotArea.YAxis.Appearance.CustomFormat = "0\\%";

            }

            else {

                dt = (DataTable)Session["dt"];
                RadChart1.DataSource = dt;
                RadChart1.Series[0].DataYColumn = "Employee";
                YAxis = (String)Session["YAxis"];
                RadChart1.PlotArea.XAxis.DataLabelsColumn = dt.Columns[0].ColumnName.ToString();
                RadChart1.DataBind();
                RadChart1.PlotArea.YAxis.Appearance.CustomFormat = null;
            
            }
            if (cboChartType.Text == "Area")
            {
                RadChart1.Series[0].Type = ChartSeriesType.Area;
                RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 150, 80, 50);
            }
            else if (cboChartType.Text == "Bar")
            {
                RadChart1.Series[0].Type = ChartSeriesType.Bar;
                RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 150, 80, 50);
            }
            else if (cboChartType.Text == "Line")
            {
                RadChart1.Series[0].Type = ChartSeriesType.Line;
                RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 150, 80, 50);
            }
            else if (cboChartType.Text == "Pie")
            {
                RadChart1.Series[0].Type = ChartSeriesType.Pie;
                RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 550, 80, 50); 
            }
           
               
        }

         
        protected void RadComboBox1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (cboChartType.Text)
            {
                case "Bar":
                    RadChart1.Series[0].Type = ChartSeriesType.Bar;
                    RadChart1.Legend.Visible = false;
                    break;
                case "Pie":
                    RadChart1.Series[0].Type = ChartSeriesType.Pie;
                    RadChart1.Legend.Visible = true;
                    break;
                case "Area":
                    RadChart1.Series[0].Type = ChartSeriesType.Area;
                    RadChart1.Legend.Visible = false;
                    break;
                case "Line":
                    RadChart1.Series[0].Type = ChartSeriesType.Line;
                    RadChart1.Legend.Visible = false;
                    break;
            }
            
            chkPercentage_CheckedChanged(sender, e);
        }

        protected void cboAxis_SelectedIndexChanged1(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (cboAxis.Text == "Employee")
            //{
            //   RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal;
            //}
            //if (cboAxis.Text == "Department")
            //{
            //   RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical;
            //}
        }

        protected void cboSkin_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (Page.IsPostBack)
            {
                RadChart1.ClearSkin();
                RadChart1.Skin = cboSkin.Text;
                RadChart1.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
                RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 150, 80, 50);
            }
        }

        protected void RadChart1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
        {
            e.SeriesItem.Name = dt.Rows[e.SeriesItem.Index][0].ToString();

            //Color color = CreateRandomColor();
            //e.SeriesItem.Appearance.FillStyle.MainColor = color;
        }
        private Color CreateRandomColor()
        {
            Random randonGen = new Random();
            Color randomColor = Color.FromArgb(randonGen.Next(255), randonGen.Next(255), randonGen.Next(255));

            return randomColor;
        }

       
        protected void cboYAxis_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //CompID = (int)Session["CompID"];

            if (cboYAxis.Text == "Nationality")
            {
                dt.Clear();
                strSql = " SELECT n.Nationality as Nationality, Employee=Count(*), Allemp=(select COUNT(*)from employee) FROM employee as e " +
                         " inner join Nationality as n on e.nationality_id=n.id where e.Company_Id=" + CompID + " " +
                         " Group By n.Nationality Order By Employee Desc";
               
                Adpt = new SqlDataAdapter(strSql, strConn);
                Adpt.Fill(dt);
                RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 360;
               
            }

            if (cboYAxis.Text == "Position")
            {
                dt.Clear();
                strSql = " SELECT d.Designation as Position, Employee=Count(*), Allemp=(select COUNT(*)from employee) FROM employee as e " +
                         " inner join designation as d on e.desig_id=d.id where e.Company_Id=" + CompID + " " +
                         " Group By d.Designation Order By Employee Desc";
                //strConn = "Data Source=CHEQUEPRO\\SUMON;Initial Catalog=TEST;User ID=sa;Password=anb@payroll";
                Adpt = new SqlDataAdapter(strSql, strConn);
                Adpt.Fill(dt);
                RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 325;

            }
            if (cboYAxis.Text == "Department")
            {
                dt.Clear();
                //strConn = "Data Source=CHEQUEPRO\\SUMON;Initial Catalog=TEST;User ID=sa;Password=anb@payroll";
                strSql = "SELECT d.DeptName as Department, Employee=Count(*), Allemp=(select COUNT(*)from employee) FROM employee as e inner join department as d on e.dept_id=d.id where e.Company_Id=" + CompID + " GROUP BY d.DeptName Order By Employee Desc";
                Adpt = new SqlDataAdapter(strSql, strConn);
                Adpt.Fill(dt);
                RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 360;
            }

            if (cboYAxis.Text == "Employment Type")
            {
                dt.Clear();
                //strConn = "Data Source=CHEQUEPRO\\SUMON;Initial Catalog=TEST;User ID=sa;Password=anb@payroll";
                strSql="SELECT e.emp_type as EmployeeType, Employee=Count(*), Allemp=(select COUNT(*)from employee) FROM employee as e where e.Company_Id=" + CompID + " Group By e.emp_type Order By Employee Desc";
                Adpt = new SqlDataAdapter(strSql, strConn);
                Adpt.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == "SC")
                    {
                        dt.Rows[i][0] = "Singapore Citizen";
                    }
                    if (dt.Rows[i][0].ToString() == "WP")
                    {
                        dt.Rows[i][0] = "Work Permit";
                    }
                    if (dt.Rows[i][0].ToString() == "EP")
                    {
                        dt.Rows[i][0] = "Employment Pass";
                    }
                    if (dt.Rows[i][0].ToString() == "SPR")
                    {
                        dt.Rows[i][0] = "Singapore PR";
                    }
                    if (dt.Rows[i][0].ToString() == "OT")
                    {
                        dt.Rows[i][0] = "Others";
                    }
                    if (dt.Rows[i][0].ToString() == "DP")
                    {
                        dt.Rows[i][0] = "Dependant Pass";
                    }
                }
                RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 360;
            }

            dt.Columns.Add("Percentage");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totalemp = int.Parse(dt.Rows[i][2].ToString());
                double emp = Int32.Parse(dt.Rows[i][1].ToString());
                empPercentage = (emp / totalemp) * 100;  
                dt.Rows[i]["Percentage"] = Math.Round(empPercentage, 2);
            }
            RadChart1.DataSource = dt;
            RadChart1.Series[0].DataYColumn = "Employee";
            RadChart1.PlotArea.XAxis.DataLabelsColumn = dt.Columns[0].ColumnName.ToString();
            RadChart1.PlotArea.XAxis.AddRange(1, dt.Rows.Count, 1);
            //RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color =
            //System.Drawing.Color.DarkBlue;

            RadChart1.DataBind();

            Session["dt"] = dt;
            Session["YAxis"] = cboYAxis.Text;
            RadChart1.PlotArea.Appearance.Dimensions.Margins = new ChartMargins(70, 150, 80, 50);
            chkPercentage_CheckedChanged(sender, e);
          

        }
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
          
            RadChart1.Save(Server.MapPath(@"~/image.png"), System.Drawing.Imaging.ImageFormat.Png);

            makePDF();

        }

        private MemoryStream drawChart()
        {
            MemoryStream mStream = new MemoryStream();

            RadChart1.Save(mStream, ImageFormat.Jpeg);

            return mStream;
        }

        void makePDF()
	 {
	 Response.ContentType = "application/pdf";
 
     Response.AddHeader("content-disposition", "attachment;filename=ExportChart.pdf");
	 
	 Response.Cache.SetCacheability(HttpCacheability.NoCache);

     string imageFilePath = Server.MapPath(@"~/image.png");
	 
	 iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
	 
	 // Page site and margin left, right, top, bottom is defined
	 Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
	 
	 //Resize image depend upon your need
	 //For give the size to image
	 jpg.ScaleToFit(550, 770);
	 
     ////If you want to choose image as background then,
	 
     //jpg.Alignment = iTextSharp.text.Image.ALIGN_TOP;
	 
     ////If you want to give absolute/specified fix position to image.
     //jpg.SetAbsolutePosition(10, 10);
	 
	 PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
	 
	 pdfDoc.Open();
	 
	 pdfDoc.NewPage();
	 
	 Paragraph paragraph = new Paragraph("this is the testing text for demonstrate the image is in background \n\n\n this is the testing text for demonstrate the image is in background");
	 
	 pdfDoc.Add(jpg);
 
	 //pdfDoc.Add(paragraph);
 
	 pdfDoc.Close();
	 
	 Response.Write(pdfDoc);
	 
	 Response.End();
	 }
                
            
    }
}