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
using System.Data.SqlClient;

namespace SMEPayroll.Payroll
{
    public partial class PayrollCeilingMaster : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
       int compid;
        DataSet compCeiling;
        DataSet compCeilingReplica;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            ViewState["actionMessage"] = "";
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
              compid = Utility.ToInteger(Session["Compid"].ToString());
             //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please dont try change leave code from (8 to 16),Because system generated leaves."));
             RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
             RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            
             string strCriling = "Select *,'AA'idNew from CeilingMaster Where CompanyID=" + compid;
             compCeiling = new DataSet();
             compCeiling = DataAccess.FetchRS(CommandType.Text,strCriling,null);
             compCeilingReplica = compCeiling.Clone();
             if (!IsPostBack)
             {
                 DataRow dr1 = compCeilingReplica.Tables[0].NewRow();
                 dr1["Id"] = -1;
                 dr1["Parameter"] = "C1";
                 dr1["AddType"] = "C1";
                 dr1["CeilingType"] = "-1";
                 dr1["CompanyID"] = compid;
                 dr1["idNew"] = "-1" + ":NH";
                 compCeilingReplica.Tables[0].Rows.Add(dr1);

                 DataRow dr2 = compCeilingReplica.Tables[0].NewRow();
                 dr2["Id"] = -1;
                 dr2["Parameter"] = "C2";
                 dr2["AddType"] = "C2";
                 dr2["CeilingType"] = "-1";
                 dr2["CompanyID"] = compid;
                 dr2["idNew"] = "-1" + ":OT1";
                 compCeilingReplica.Tables[0].Rows.Add(dr2);

                 DataRow dr3 = compCeilingReplica.Tables[0].NewRow();
                 dr3["Id"] = -1;
                 dr3["Parameter"] = "C3";
                 dr3["AddType"] = "C3";
                 dr3["CeilingType"] = "-1";
                 dr3["CompanyID"] = compid;
                 dr3["idNew"] = "-1" + ":OT2";
                 compCeilingReplica.Tables[0].Rows.Add(dr3);

                 DataRow dr4 = compCeilingReplica.Tables[0].NewRow();
                 dr4["Id"] = -1;
                 dr4["Parameter"] = "C4";
                 dr4["AddType"] = "C4";
                 dr4["CeilingType"] = "-1";
                 dr4["CompanyID"] = compid;
                 dr4["idNew"] = "-1" + ":Days";
                 compCeilingReplica.Tables[0].Rows.Add(dr4);

                 DataRow dr5 = compCeilingReplica.Tables[0].NewRow();
                 dr5["Id"] = -1;
                 dr5["Parameter"] = "C5";
                 dr5["AddType"] = "C5";
                 dr5["CeilingType"] = "-1";
                 dr5["CompanyID"] = compid;
                 dr5["idNew"] = "-1" + ":V1";
                 compCeilingReplica.Tables[0].Rows.Add(dr5);

                 DataRow dr6 = compCeilingReplica.Tables[0].NewRow();
                 dr6["Id"] = -1;
                 dr6["Parameter"] = "C6";
                 dr6["AddType"] = "C6";
                 dr6["CeilingType"] = "-1";
                 dr6["CompanyID"] = compid;
                 dr6["idNew"] = "-1" + ":V2";
                 compCeilingReplica.Tables[0].Rows.Add(dr6);


                 DataRow dr7 = compCeilingReplica.Tables[0].NewRow();
                 dr7["Id"] = -1;
                 dr7["Parameter"] = "C7";
                 dr7["AddType"] = "C7";
                 dr7["CeilingType"] = "-1";
                 dr7["CompanyID"] = compid;
                 dr7["idNew"] = "-1" + ":V3";
                 compCeilingReplica.Tables[0].Rows.Add(dr7);

                 DataRow dr8 = compCeilingReplica.Tables[0].NewRow();
                 dr8["Id"] = -1;
                 dr8["Parameter"] = "C8";
                 dr8["AddType"] = "C8";
                 dr8["CeilingType"] = "-1";
                 dr8["CompanyID"] = compid;
                 dr8["idNew"] = "-1" + ":V4";
                 compCeilingReplica.Tables[0].Rows.Add(dr8);

                 foreach (DataRow dr in compCeiling.Tables[0].Rows)
                 {
                     foreach (DataRow dr11 in compCeilingReplica.Tables[0].Rows)
                     {
                         if (dr["AddType"].ToString() == dr11["AddType"].ToString())
                         {
                             dr11.BeginEdit();
                             dr11["Id"] = dr["Id"];
                             dr11["CeilingType"] = dr["CeilingType"];
                             dr11["idNew"] = dr["Id"] + ":" + dr["Parameter"].ToString();
                             dr11["Parameter"] = dr["Parameter"];
                             dr11.AcceptChanges();
                         }
                     }
                 }
                 RadGrid1.DataSource = compCeilingReplica;
                 RadGrid1.DataBind();
                 Session["compCeilingReplica"] = compCeilingReplica;
             }
         }

        void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                //((Button)commandItem.FindControl("btnsubmit")).Enabled = false;  

                string deletCeilingMaster = "DELETE FROM CeilingMaster WHERE CompanyID=" + compid;
                int val1 = DataAccess.ExecuteNonQuery(deletCeilingMaster, null);
                string strInsertData = "";
                string strUpdateData = "";
                if (e.CommandName == "SubmitData")
                {

                    //Insert Data in CeilingMaster
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkCeilingHr = (CheckBox)dataItem["CeilingTypeHR"].Controls[1];
                            CheckBox chkCeilingRate = (CheckBox)dataItem["CeilingTypeRate"].Controls[1];
                            TextBox txtboxPara = (TextBox)dataItem.FindControl("txtParameter");

                            RadioButtonList radButt = (RadioButtonList)item.FindControl("radPara");
                            int ceiltype = -1;
                            if (radButt.Items[0].Selected)
                            {
                                ceiltype = 1;
                            }
                            if (radButt.Items[1].Selected)
                            {
                                ceiltype = 2;
                            }

                            if (radButt.Items[2].Selected)
                            {
                                ceiltype = -1;
                            }

                            if(strInsertData=="")
                            {
                                strInsertData = "INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID],[AddType])VALUES('" + txtboxPara.Text.Trim() + "'," + ceiltype + "," + compid + ",'" + dataItem["AddType"].Text.Trim() + "') ";
                            }
                            else
                            {
                                strInsertData = strInsertData + ";" +  " INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID],[AddType])VALUES('" + txtboxPara.Text.Trim() + "'," + ceiltype + "," + compid + ",'" + dataItem["AddType"].Text.Trim() + "')";
                            }

                            string strGetAddTypeId = "Select id from additions_types where code='" + dataItem["AddType"].Text.Trim() + "' and company_id=" + compid;

                            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text,strGetAddTypeId, null);
                            int idadd = 0;

                            while(dr.Read())
                            {
                                idadd = Convert.ToInt32(dr.GetValue(0).ToString());
                            }

                            if(strUpdateData=="")
                            {
                                strUpdateData = "Update additions_types SET [desc]='" + txtboxPara.Text.Trim() + "' WHERE id=" + idadd; 
                            }
                            else
                            {
                                strUpdateData = strUpdateData + "; " + "Update additions_types SET [desc]='" + txtboxPara.Text.Trim() + "' WHERE id=" + idadd;
                            }
                        }
                    }
                    try
                    {
                                string strData = strInsertData + ";" + strUpdateData + ";";
                                int val = DataAccess.ExecuteNonQuery(strData, null);

                                if (val > 0)
                                {
                                    string strCriling = "Select *,'AA'idNew from CeilingMaster Where CompanyID=" + compid;
                                    compCeiling = new DataSet();
                                    compCeiling = DataAccess.FetchRS(CommandType.Text, strCriling, null);

                                    if (Session["compCeilingReplica"] != null)
                                    {
                                        compCeilingReplica = (DataSet)Session["compCeilingReplica"];
                                    }
                                    foreach (DataRow dr in compCeiling.Tables[0].Rows)
                                    {
                                        foreach (DataRow dr1 in compCeilingReplica.Tables[0].Rows)
                                        {
                                            if (dr["AddType"].ToString() == dr1["AddType"].ToString())
                                            {
                                                dr1.BeginEdit();
                                                dr1["Id"] = dr["Id"];
                                                dr1["CeilingType"] = dr["CeilingType"];
                                                dr1["idNew"] = dr["Id"] + ":" + dr["Parameter"].ToString();
                                                dr1["Parameter"] = dr["Parameter"];
                                                dr1.AcceptChanges();
                                            }
                                        }
                                    }
                                    RadGrid1.DataSource = compCeilingReplica;
                                    RadGrid1.DataBind();
                                    RadGrid1.Rebind();
                                    Session["compCeilingReplica"] = compCeilingReplica;
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Records Updated Successfully."));
                            ViewState["actionMessage"] = "Success|Records Updated Successfully.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Addition Type Already Exist."));
                        ViewState["actionMessage"] = "Warning|Addition Type Already Exist.";
                    }

                }
            }
        }

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            //{
            //    GridEditableItem item = e.Item as GridEditableItem;
            //    GridTextBoxColumnEditor type = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("type");
            //    type.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_type('" + type.TextBoxControl.ClientID + "')");
            //}
        }
       
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            
            
            // check if the value is Null the uncheck the checkbox else check
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                if (item != null)
                {
                    CheckBox boxHr = (CheckBox)item.FindControl("chkCeilingHr");
                    CheckBox boxRate = (CheckBox)item.FindControl("chkCeilingRate");

                    RadioButtonList radButt = (RadioButtonList)item.FindControl("radPara");

                    if (boxHr != null)
                    {
                        if (item["CeilingType"].Text.ToString() == "1")
                        {
                            boxHr.Checked = true;
                        }
                        else if (item["CeilingType"].Text.ToString() == "-1")
                        {
                            boxHr.Checked = false;
                        }
                        else 
                        {
                            boxHr.Checked = false;
                        }
                    }
                    if (boxRate != null)
                    {
                        if (item["CeilingType"].Text.ToString() == "2")
                        {
                            boxRate.Checked = true;
                        }
                        else if (item["CeilingType"].Text.ToString() == "-2")
                        {
                            boxRate.Checked = false;
                        }
                        else
                        {
                            boxRate.Checked = false;
                        }

                    }

                    if (radButt != null)
                    {
                        if (item["CeilingType"].Text.ToString() == "1")
                        {
                            radButt.Items[0].Selected = true;
                            radButt.Items[1].Selected = false;
                            radButt.Items[2].Selected = false;
                        }
                        else if (item["CeilingType"].Text.ToString() == "2")
                        {
                            radButt.Items[0].Selected = false;
                            radButt.Items[1].Selected = true;
                            radButt.Items[2].Selected = false;
                        }
                        else if (item["CeilingType"].Text.ToString() == "-1")
                        {
                            radButt.Items[0].Selected = false;
                            radButt.Items[1].Selected = false;
                            radButt.Items[2].Selected = true;
                        }
                        else if (item["CeilingType"].Text.ToString() == "-2")
                        {
                            radButt.Items[0].Selected = false;
                            radButt.Items[1].Selected = false;
                            radButt.Items[2].Selected = true;
                        }
                        else
                        { 
                        
                        
                        }
                    
                    }
                }
            }
        }
     
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //try
            //{
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_leaves  where leave_type=" + id, null);
            //    if (dr.Read())
            //    {
            //        if (dr[0].ToString() != "0")
            //        {
            //            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type.This leave type is in use."));
            //        }
            //        else
            //        {
            //            string sSQL = "DELETE FROM [leave_types] WHERE [id] =" + id;

            //            int retVal = DataAccess.ExecuteStoreProc(sSQL);

            //            if (retVal == 1)
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Leave Type is Deleted Successfully."));

            //            }
            //            else
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type."));
            //            }

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    string ErrMsg = ex.Message;
            //    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
            //        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
            //    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
            //    e.Canceled = true;
            //}

        }

        //Text Change for Parameter Box etc
        protected void TextChanged(Object sender, System.EventArgs e)
        {
            TextBox txtBox= (TextBox)sender;
            int pk = -1;
            string para = "";
            char sep = ':';
            string[] tooltip = txtBox.ToolTip.Split(sep);
            if (tooltip.Length > 1)
            {
                pk = Convert.ToInt32(tooltip[0]);
                para = Convert.ToString(tooltip[1]);
            }
            string strUpdateVar = "Update additions_types SET [desc]='" + txtBox.Text.Trim() + "' WHERE code='" + txtBox.ToolTip.Trim().ToUpper() + "' AND company_id=" + compid;

            if (pk != -1)
            {
                strUpdateVar = strUpdateVar + ";" + "UPDATE [CeilingMaster] SET [Parameter]='" + txtBox.Text + "' WHERE ID=" + pk + "AND company_id=" + compid ;
            }
            DataAccess.FetchRS(CommandType.Text, strUpdateVar, null);

            string strCriling = "Select *,'AA'idNew from CeilingMaster Where CompanyID=" + compid;
            compCeiling = new DataSet();
            compCeiling = DataAccess.FetchRS(CommandType.Text, strCriling, null);

            if (Session["compCeilingReplica"] != null)
            {
                compCeilingReplica = (DataSet)Session["compCeilingReplica"];
            }
            foreach (DataRow dr in compCeiling.Tables[0].Rows)
            {
                foreach (DataRow dr1 in compCeilingReplica.Tables[0].Rows)
                {
                    if (dr["Parameter"].ToString() == dr1["Parameter"].ToString())
                    {
                        dr1.BeginEdit();
                        dr1["Id"] = dr["Id"];
                        dr1["CeilingType"] = dr["CeilingType"];
                        dr1["idNew"] = dr["Id"] + ":" + dr["Parameter"].ToString();                        
                        dr1["Parameter"] = dr["Parameter"];
                        dr1.AcceptChanges();
                    }
                }
            }
            RadGrid1.DataSource = compCeilingReplica;
            RadGrid1.DataBind();
            RadGrid1.Rebind();
            Session["compCeilingReplica"] = compCeilingReplica;

        }


        //Update in table 
        //whether it is need in paysip ornot
        protected void CheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            string strval = "";

            int pk = -1;
            string para = "";
            char sep =':';
            string[] tooltip = box.ToolTip.Split(sep);
            if (tooltip.Length > 1)
            {
                pk =Convert.ToInt32(tooltip[0]);
                para = Convert.ToString (tooltip[1]);
            }

            if (box.ID == "chkCeilingHr")
            {
                if (box.Checked && pk == -1)
                {

                    //Insert 
                    //string ssqlb = "UPDATE [Currency] SET [Selected] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                    //DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    strval = "INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID])VALUES('" + para+ "'," + 1  +"," + compid +")" ;
                   // DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (box.Checked && pk != -1)
                {
                    strval = "UPDATE [CeilingMaster] SET [CeilingType] =1 WHERE ID=" + pk;
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (!box.Checked && pk == -1)
                {
                    strval = "INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID])VALUES('" + para + "'," + -1 + "," + compid + ")";
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (!box.Checked && pk!= -1)
                {
                    strval = "UPDATE [CeilingMaster] SET [CeilingType] =-1 WHERE ID=" + pk;
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
            }

            if (box.ID == "chkCeilingRate")
            {
                if (box.Checked && pk == -1)
                {
                    //Insert 

                    //string ssqlb = "UPDATE [Currency] SET [Selected] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                    //DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    strval = "INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID])VALUES('" + para + "'," + 2 + "," + compid + ")";
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (box.Checked && pk != -1)
                {
                    strval = "UPDATE [CeilingMaster] SET [CeilingType] =2 WHERE ID=" + pk;
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (!box.Checked && pk == -1)
                {
                    strval = "INSERT [CeilingMaster]([Parameter],[CeilingType],[CompanyID])VALUES('" + para + "'," + -2 + "," + compid + ")";
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else if (!box.Checked && pk != -1)
                {
                    strval = "UPDATE [CeilingMaster] SET [CeilingType] =-2 WHERE ID=" + pk;
                    //DataAccess.FetchRS(CommandType.Text, strval, null);
                }
            }



            //if (box.Checked)
            //{
            //    string ssqlb = "UPDATE [Currency] SET [Selected] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
            //    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            //}
            //else
            //{
            //    string ssqlb = "UPDATE [Currency] SET [Selected] = CAST(NULL AS INT)  WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
            //    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            //}

            //string strCriling = "Select *,'AA'idNew from CeilingMaster Where CompanyID=" + compid;
            //compCeiling = new DataSet();
            //compCeiling = DataAccess.FetchRS(CommandType.Text, strCriling, null);

            //if (Session["compCeilingReplica"] != null)
            //{
            //    compCeilingReplica = (DataSet)Session["compCeilingReplica"];
            //}
            //foreach (DataRow dr in compCeiling.Tables[0].Rows)
            //{
            //    foreach (DataRow dr1 in compCeilingReplica.Tables[0].Rows)
            //    {
            //        if (dr["Parameter"].ToString() == dr1["Parameter"].ToString())
            //        {
            //            dr1.BeginEdit();
            //            dr1["Id"] = dr["Id"];
            //            dr1["CeilingType"] = dr["CeilingType"];
            //            dr1["idNew"] = dr["Id"] + ":" + dr["Parameter"].ToString();
            //            dr1.AcceptChanges();
            //        }
            //    }
            //}
            //RadGrid1.DataSource = compCeilingReplica;
            //RadGrid1.DataBind();
            //RadGrid1.Rebind();
            //Session["compCeilingReplica"] = compCeilingReplica;

        }

        protected bool GetPaySlip(object InPayslip)
        {
            //if (Convert.ToString(InPayslip) =="NULL")
            //{
            //    return false;
            //}
            //else
            //{
            //    return false;
            //}

            return false;

        }


        

    }
}
