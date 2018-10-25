using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace SMEPayroll.TextTemplate
{
    partial class TextTemplate : ITemplate
    {
        protected LiteralControl lControl;
        protected RequiredFieldValidator validatorTextBox;
        protected TextBox textBox;
        protected CheckBox boolValue;

        private string colname;

        public TextTemplate(string cName)
        {
            colname = cName;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            //lControl = new LiteralControl();
            //lControl.ID = "lControl";
            //lControl.DataBinding += new EventHandler(lControl_DataBinding);

            textBox = new TextBox();
            textBox.ID = "txt" + colname;
            textBox.DataBinding += new EventHandler(textBox_DataBinding);
            textBox.Width = Unit.Pixel(30);
            textBox.Attributes.Add("onkeypress", "return isNumericKeyStrokeDecimal(event);");
            
            //validatorTextBox = new RequiredFieldValidator();
            //validatorTextBox.ControlToValidate = "txt" + colname;
            //validatorTextBox.ErrorMessage = "*";

            //boolValue = new CheckBox();
            //boolValue.ID = "bln" + colname;
            //boolValue.DataBinding += new EventHandler(boolValue_DataBinding);
            //boolValue.Enabled = false;

            //Table table = new Table();
            //TableRow row1 = new TableRow();

            //TableCell cell11 = new TableCell();
            //TableCell cell12 = new TableCell();
            //row1.Cells.Add(cell11);
            //row1.Cells.Add(cell12);
            //table.Rows.Add(row1);

            //cell11.Text = colname + ": ";
            //cell12.Controls.Add(lControl);

            container.Controls.Add(textBox);
            //container.Controls.Add(validatorTextBox);
            //container.Controls.Add(table);
            //container.Controls.Add(new LiteralControl("<br />"));

            //container.Controls.Add(boolValue);
        }

        //void boolValue_DataBinding(object sender, EventArgs e)
        //{
        //    CheckBox cBox = (CheckBox)sender;
        //    GridDataItem container = (GridDataItem)cBox.NamingContainer;
        //    //cBox.Checked = (bool)((DataRowView)container.DataItem)["Bool"];
        //}


        //public void lControl_DataBinding(object sender, EventArgs e)
        //{
        //    LiteralControl l = (LiteralControl)sender;
        //    GridDataItem container = (GridDataItem)l.NamingContainer;
        //    l.Text = ((DataRowView)container.DataItem)[colname].ToString() + "<br />";
        //}

        public void textBox_DataBinding(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridDataItem container = (GridDataItem)t.NamingContainer;
            t.Text = ((DataRowView)container.DataItem)[colname].ToString();
        }

    }
}
