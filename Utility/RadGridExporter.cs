using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Web.UI;
using NPOI.HSSF.UserModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HPSF;
using System.IO;
using System.Drawing;
using NPOI.POIFS.FileSystem;
using NPOI.HSSF.Util;

/// <summary>
/// Summary description for RadGridExporter
namespace SMEPayroll
{
    public class RadGridExporter
    {
        private string _fileName;
        private HSSFWorkbook workbook;
        private HSSFPatriarch patriarch; //drawing patriarch - shape container
        private Dictionary<string, short> globalStyles; //this dictionary holds the global styles
        //supported types
        private GridItemType[] types = new GridItemType[] { GridItemType.AlternatingItem, GridItemType.Item, GridItemType.CommandItem, GridItemType.Header, GridItemType.SelectedItem, GridItemType.Footer };

        public string FileName
        {
            get
            {
                if (String.IsNullOrEmpty(_fileName))
                    _fileName = "Book1.xls"; //default name
                return _fileName;
            }
            set { _fileName = value; }
        }

        private void InitializeWorkbook()
        {
            workbook = new HSSFWorkbook();

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Telerik";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Telerik RadGrid NPOI Export Demo";
            workbook.SummaryInformation = si;
        }

        public void Export(RadGrid source)
        {
            if (source == null)
                throw new NullReferenceException();

            InitializeWorkbook();

            HSSFSheet sheet = workbook.CreateSheet("Sheet1");
            globalStyles = new Dictionary<string, short>();
            patriarch = sheet.CreateDrawingPatriarch();
            BuildTableStructure(source, sheet);

            HttpResponse response = source.Page.Response;
            response.Clear();
            response.Buffer = true;
            response.ClearContent();
            response.ClearHeaders();
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", FileName));
            response.Clear();
            response.BinaryWrite(WriteToStream().GetBuffer());
            response.End();              
        }

        //strips the unwanted text - mostly &nbsp; tags and empty spaces from grid templates
        private string StripText(string text)
        {
            text = text.Replace("&nbsp;", "").Trim();
            return String.IsNullOrEmpty(text) ? "" : text;
        }

        private MemoryStream WriteToStream()
        {
            MemoryStream file = new MemoryStream();
            workbook.Write(file);
            return file;
        }

        private static int LoadImage(string path, HSSFWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);

            int pictureType = 0;
            FileInfo fileInfo = new FileInfo(path);
            switch (fileInfo.Extension.ToUpper())
            {
                case ".JPG":
                case ".JPEG": pictureType = HSSFWorkbook.PICTURE_TYPE_JPEG; break;
                case ".PNG": pictureType = HSSFWorkbook.PICTURE_TYPE_PNG; break;
                case ".DIB":
                case ".BMP": pictureType = HSSFWorkbook.PICTURE_TYPE_DIB; break;
            }

            return wb.AddPicture(buffer, pictureType);
        }

        private void BuildTableStructure(RadGrid source, HSSFSheet sheet)
        {
            //Prepare item styles
            if (!source.MasterTableView.ItemStyle.IsEmpty)
                PrepareGlobalItemStyles(source.MasterTableView.ItemStyle, "ItemStyle");
            if (!source.MasterTableView.AlternatingItemStyle.IsEmpty)
                PrepareGlobalItemStyles(source.MasterTableView.AlternatingItemStyle, "AlternatingItemStyle");
            if (!source.MasterTableView.HeaderStyle.IsEmpty)
                PrepareGlobalItemStyles(source.MasterTableView.HeaderStyle, "HeaderStyle");
            if (!source.MasterTableView.CommandItemStyle.IsEmpty)
                PrepareGlobalItemStyles(source.MasterTableView.CommandItemStyle, "CommandItemStyle");
            if (!source.MasterTableView.FooterStyle.IsEmpty)
                PrepareGlobalItemStyles(source.MasterTableView.FooterStyle, "FooterStyle");

            //Adjust column widths
            for (int colNum = 0; colNum < source.MasterTableView.RenderColumns.Length; colNum++)
            {
                GridColumn col = source.MasterTableView.RenderColumns[colNum];
                if (col.Visible == false)
                    sheet.SetColumnWidth(colNum, 0);
                else
                    sheet.SetColumnWidth(colNum, (int)(col.HeaderStyle.Width.Value * 36.5)); //magic number!
            }

            //Build the table structure
            bool hasFooter = false;
            int rowNum;
            for (rowNum = 0; rowNum < source.MasterTableView.GetItems(types).Length; rowNum++)
            {
                GridItem item = source.MasterTableView.GetItems(types)[rowNum];

                if (item is GridFooterItem)
                {
                    hasFooter = true;
                    continue;
                }

                //Skips the invisible rows
                if (item.Visible == false || item.Display == false)
                    continue;

                CreateExcelRow(hasFooter ? rowNum - 1 : rowNum, item, sheet);
            }

            if (hasFooter) //add the footer row at the end of the table
            {
                GridFooterItem item = source.MasterTableView.GetItems(GridItemType.Footer)[0] as GridFooterItem;
                CreateExcelRow(rowNum - 1, item, sheet);
            }
        }

        private void CreateExcelRow(int rowNum, GridItem item, HSSFSheet sheet)
        {
            sheet.CreateRow(rowNum);

            for (int cellNum = 0; cellNum < item.Cells.Count; cellNum++)
            {
                TableCell cell = item.Cells[cellNum];

                //The command item is handled separately since its cells should be merged
                if (item is GridCommandItem)
                {
                    sheet.GetRow(rowNum).CreateCell(cellNum).SetCellValue(StripText(cell.Text));
                    sheet.AddMergedRegion(new NPOI.HSSF.Util.Region(0, 0, 0, item.OwnerTableView.RenderColumns.Length - 1));
                }

                if (cell.HasControls())
                {
                    foreach (Control ctrl in cell.Controls)
                    {
                        //Handle TextBoxe and Button controls
                        if ((ctrl is ITextControl || ctrl is IButtonControl) && !(ctrl is ImageButton))
                        {
                            string strippedText = ctrl is ITextControl ? StripText((ctrl as ITextControl).Text) : (ctrl as IButtonControl).Text;
                            if (!String.IsNullOrEmpty(strippedText))
                            {
                                if (sheet.GetRow(rowNum).GetCell(cellNum) == null)
                                    sheet.GetRow(rowNum).CreateCell(cellNum).SetCellValue(strippedText); //create new cell
                                else
                                {
                                    string oldText = sheet.GetRow(rowNum).GetCell(cellNum).StringCellValue; //get the existing cell text
                                    sheet.GetRow(rowNum).CreateCell(cellNum).SetCellValue(oldText + strippedText);  //append the text content of the control to the existing cell
                                }
                            }
                        }

                        //Handle Images (including ImageButton)
                        if (ctrl is System.Web.UI.WebControls.Image)
                        {
                            if (!(item is GridCommandItem))
                                sheet.GetRow(rowNum).CreateCell(cellNum);

                            System.Web.UI.WebControls.Image img = ctrl as System.Web.UI.WebControls.Image;

                            if (!String.IsNullOrEmpty(img.ImageUrl))
                            {
                                //expand the row height when the picture overflows
                                double imgHeight = img.Height.Value / 1.33333f; //assuming 75dpi
                                if (imgHeight > sheet.GetRow(rowNum).HeightInPoints)
                                    sheet.GetRow(rowNum).HeightInPoints = (float)imgHeight;

                                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, cellNum, rowNum, cellNum, rowNum);
                                anchor.AnchorType = 2;
                                string path = ctrl.Page.Server.MapPath(img.ImageUrl);
                                HSSFPicture picture = patriarch.CreatePicture(anchor, LoadImage(path, workbook));
                                picture.Resize();
                            }
                        }
                    }
                }
                else
                {
                    sheet.GetRow(rowNum).CreateCell(cellNum).SetCellValue(StripText(cell.Text));
                }

                //Set the global styles for each cell - ItemStyle, AlternatingItemStyle, etc
                //if (item.ItemType == GridItemType.Item && globalStyles.ContainsKey("ItemStyle"))
                //    sheet.GetRow(rowNum).GetCell(cellNum).CellStyle = workbook.GetCellStyleAt(globalStyles["ItemStyle"]);
                //if (item.ItemType == GridItemType.AlternatingItem && globalStyles.ContainsKey("AlternatingItemStyle"))
                //    sheet.GetRow(rowNum).GetCell(cellNum).CellStyle = workbook.GetCellStyleAt(globalStyles["AlternatingItemStyle"]);
                //if (item.ItemType == GridItemType.Header && globalStyles.ContainsKey("HeaderStyle"))
                //    sheet.GetRow(rowNum).GetCell(cellNum).CellStyle = workbook.GetCellStyleAt(globalStyles["HeaderStyle"]);
                //if (item.ItemType == GridItemType.CommandItem && globalStyles.ContainsKey("CommandItemStyle"))
                //    sheet.GetRow(rowNum).GetCell(cellNum).CellStyle = workbook.GetCellStyleAt(globalStyles["CommandItemStyle"]);
                //if (item.ItemType == GridItemType.Footer && globalStyles.ContainsKey("FooterStyle"))
                //    sheet.GetRow(rowNum).GetCell(cellNum).CellStyle = workbook.GetCellStyleAt(globalStyles["FooterStyle"]);
            }
        }

        private void PrepareGlobalItemStyles(GridTableItemStyle source, string styleName)
        {
            if (!source.BackColor.IsEmpty)
            {
                Color stBackColor = source.BackColor;
                Color stForeColor = source.ForeColor;

                HSSFPalette palette = workbook.GetCustomPalette();
                HSSFCellStyle itemStyle = workbook.CreateCellStyle();

                NPOI.HSSF.Util.HSSFColor backColor = palette.FindColor(stBackColor.R, stBackColor.G, stBackColor.B);
                if (backColor == null) //when there is no such color in the predefined palette...
                {
                    backColor = palette.FindSimilarColor(stBackColor.R, stBackColor.G, stBackColor.B); //...we will find a similar color
                }

                NPOI.HSSF.Util.HSSFColor foreColor = palette.FindColor(stForeColor.R, stForeColor.G, stForeColor.B);
                if (foreColor == null)
                {
                    foreColor = palette.FindSimilarColor(stForeColor.R, stForeColor.G, stForeColor.B);
                }

                itemStyle.FillForegroundColor = backColor.GetIndex();
                itemStyle.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;
                itemStyle.FillBackgroundColor = backColor.GetIndex();
                itemStyle.WrapText = false;
                itemStyle.VerticalAlignment = 1;

                HSSFFont font = workbook.CreateFont();
                font.Color = foreColor.GetIndex(); //font color
                itemStyle.SetFont(font);

                globalStyles.Add(styleName, itemStyle.Index); //persists the item style index in the dictionary
            }
        }
    }
}