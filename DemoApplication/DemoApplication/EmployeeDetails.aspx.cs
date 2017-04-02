using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoApplication
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        testDBEntities objDb = new testDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllEmps();
                //SetInitialRow();
            }
        }
        public void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Work", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dr = dt.NewRow();
            dr["Id"] = string.Empty;
            dr["Name"] = string.Empty;
            dr["Work"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Status"] = string.Empty;
            dt.Rows.Add(dr);

            grdEmp.DataSource = dt;
            grdEmp.DataBind();
            ViewState["grdEmp"] = dt;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Button lb = (Button)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            Label lblId = (Label)grdEmp.Rows[rowID].FindControl("lblId") as Label;
            Label lblDescription = (Label)grdEmp.Rows[rowID].FindControl("lblDescription") as Label;
            TextBox txtName = (TextBox)grdEmp.Rows[rowID].FindControl("txtName") as TextBox;
            TextBox txtWork = (TextBox)grdEmp.Rows[rowID].FindControl("txtWork") as TextBox;
            DropDownList ddlStatus = (DropDownList)grdEmp.Rows[rowID].FindControl("ddlStatus") as DropDownList;
            Label lblstatus = (Label)grdEmp.Rows[rowID].FindControl("lblstatus") as Label;

            if (txtName.Text != "" && txtWork.Text != "" && ddlStatus.SelectedValue != "Select")
            {
                if (lblId.Text == "")
                {
                    EmpTbl emp = new EmpTbl();
                    emp.Name = txtName.Text;
                    emp.Work = txtWork.Text;
                    emp.Description = lblDescription.Text;
                    emp.Status = ddlStatus.SelectedValue;
                    objDb.EmpTbls.Add(emp);
                    objDb.SaveChanges();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert(' Data Saved Successfully');", true);
                }
                else if (lblId.Text != "")
                {
                    int value = Convert.ToInt32(lblId.Text);
                    EmpTbl emp = objDb.EmpTbls.Single(x => x.Id == value);
                    emp.Name = txtName.Text;
                    emp.Work = txtWork.Text;
                    emp.Status = ddlStatus.SelectedValue;
                    
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert(' Data updated Successfully');", true);
                    objDb.SaveChanges();
                }

            }
            else
            {
                if (txtName.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Pz Enter Name..');", true);
                    txtName.Focus();
                }
                else if (txtWork.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Pz Enter Work..');", true);
                    txtWork.Focus();
                }
                else if (ddlStatus.SelectedValue == "Select")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Pz Select Status..');", true);
                    ddlStatus.Focus();
                }
            }
            GetAllEmps();

        }

        protected void btnUpdateAll_Click(object sender, EventArgs e)
        {
            UpdatingAllMethod();
        }


        public void GetAllEmps()
        {
            var emps = objDb.EmpTbls.ToList();
            if (emps.Count > 0)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Id", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Work", typeof(string)));
                dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(string)));
                for (int i = 0; i < emps.Count; i++)
                {
                    dr = dt.NewRow();
                    dr["Id"] = emps[i].Id;
                    dr["Name"] = emps[i].Name;
                    dr["Work"] = emps[i].Work;
                    dr["Description"] = emps[i].Description;
                    dr["Status"] = emps[i].Status;
                    dt.Rows.Add(dr);
                }
                grdEmp.DataSource = dt;
                ViewState["grdEmp"] = dt;
                grdEmp.DataBind();
            }
            else
            { SetInitialRow(); }
        }
        int count = 0;
        public void UpdatingAllMethod()
        {

            if (grdEmp.Rows.Count > 0)
            {
                if (grdEmp.Rows.Count != 0)
                {
                    foreach (GridViewRow objrow in grdEmp.Rows)
                    {
                        
                        CheckBox chbDept = objrow.FindControl("chbDept") as CheckBox;
                        TextBox txtName = objrow.FindControl("txtName") as TextBox;
                        TextBox txtWork = objrow.FindControl("txtWork") as TextBox;
                        Label lblId = objrow.FindControl("lblId") as Label;
                        Label lblDescription = objrow.FindControl("lblDescription") as Label;
                        DropDownList ddlStatus = objrow.FindControl("ddlStatus") as DropDownList;
                        Label lblstatus = objrow.FindControl("lblstatus") as Label;
                        if (lblId.Text == "" && chbDept.Checked == true && txtName.Text != "" && txtWork.Text != "" && ddlStatus.SelectedValue != "Select")
                        {
                            EmpTbl obj = new EmpTbl();
                            obj.Name = txtName.Text;
                            obj.Work = txtWork.Text;
                            obj.Status = ddlStatus.SelectedValue;
                            objDb.EmpTbls.Add(obj);
                        }
                        else if (lblId.Text != "" && chbDept.Checked == true  && txtName.Text != "" && txtWork.Text != "" && ddlStatus.SelectedValue != "Select")
                        {
                            int value = Convert.ToInt32(lblId.Text);
                            EmpTbl emp = objDb.EmpTbls.Single(x => x.Id == value);
                            emp.Name = txtName.Text;
                            emp.Work = txtWork.Text;
                            emp.Status = ddlStatus.SelectedValue;
                            count++;
                        }
                        
                    }
                    
                    if (count > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Data updated Successfully...');", true);
                        objDb.SaveChanges();
                        GetAllEmps();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Plz Select Atleast one CheckBox...');", true);
                    }
                }
            }
        }

        protected void grdEmp_DataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    GridViewRow gr = grdEmp.SelectedRow;
                    Label lblstatus = (Label)e.Row.FindControl("lblstatus");
                    ddlStatus.SelectedValue = lblstatus.Text;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();

            }
        }

        protected void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            // Adding Empty Row Data into Customer Address Details
            try
            {
                int Count = (int)grdEmp.Rows.Count;
                DropDownList ddlStatus = (DropDownList)grdEmp.Rows[Count - 1].FindControl("ddlStatus") as DropDownList;
                TextBox txtName = (TextBox)grdEmp.Rows[Count - 1].FindControl("txtName") as TextBox;
                TextBox txtWork = (TextBox)grdEmp.Rows[Count - 1].FindControl("txtWork") as TextBox;

                if (txtName.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Employee Name..');", true);
                    txtName.Focus();
                }
                else if (txtWork.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Work..');", true);
                    txtWork.Focus();
                }
                else if (ddlStatus.SelectedValue== "Select")
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Select Status..');", true);
                    ddlStatus.Focus();
                }

                else
                {
                    AddNewRowToGrid();
                    int aa = (int)grdEmp.Rows.Count;
                    TextBox txtName1 = (TextBox)grdEmp.Rows[aa - 1].FindControl("txtName") as TextBox;
                    txtName1.Focus();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        public void AddNewRowToGrid()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["grdEmp"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["grdEmp"];
                    DataRow drCurrentRow = null;

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddlStatus = (DropDownList)grdEmp.Rows[rowIndex].FindControl("ddlStatus");
                            TextBox txtName = (TextBox)grdEmp.Rows[rowIndex].FindControl("txtName");
                            TextBox txtWork = (TextBox)grdEmp.Rows[rowIndex].FindControl("txtWork");
                            drCurrentRow = dtCurrentTable.NewRow();
                            dtCurrentTable.Rows[i - 1]["Status"] = ddlStatus.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Work"] = txtName.Text;
                            dtCurrentTable.Rows[i - 1]["Name"] = txtName.Text;
                            rowIndex += 1;
                        }

                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["grdEmp"] = dtCurrentTable;
                        grdEmp.DataSource = dtCurrentTable;
                        grdEmp.DataBind();

                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }
                
            }
            catch (Exception ex)
            {

                string msg = ex.Message.ToString();
            }
        }

        protected void lbRemove_Click(object sender, EventArgs e)
        {
            try
            {

                AddRemoveNewRowToGrid();
                LinkButton lb = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
                int rowID = gvRow.RowIndex;
                if (ViewState["grdEmp"] != null)
                {

                    DataTable dt = (DataTable)ViewState["grdEmp"];
                    if (dt.Rows.Count > 1)
                    {
                        //Remove the Selected Row data and reset row number
                        dt.Rows.Remove(dt.Rows[rowID]);
                    }

                    //Store the current data in ViewState for future reference
                    ViewState["grdEmp"] = dt;
                    grdEmp.DataSource = dt;
                    grdEmp.DataBind();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();

            }
        }

        public void AddRemoveNewRowToGrid()
        {
            try
            {
                int rowIndex = 0;

                if (ViewState["grdEmp"] != null)
                {
                    DataTable dtCurrentTable = null;
                    dtCurrentTable = (DataTable)ViewState["grdEmp"];
                    DataRow drCurrentRow = null;
                    int index = dtCurrentTable.Rows.Count;

                    if (dtCurrentTable.Rows.Count > 0)
                    {


                        DropDownList ddlStatus = (DropDownList)grdEmp.Rows[index - 1].FindControl("ddlStatus");
                        TextBox txtName = (TextBox)grdEmp.Rows[index - 1].FindControl("txtName");
                        TextBox txtWork = (TextBox)grdEmp.Rows[index - 1].FindControl("txtWork");
                        
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[index - 1]["Status"] = ddlStatus.SelectedValue;
                        dtCurrentTable.Rows[index - 1]["Name"] = txtName.Text;
                        dtCurrentTable.Rows[index - 1]["Work"] = txtWork.Text;
                        
                        rowIndex += 1;
                        ViewState["grdEmp"] = dtCurrentTable;
                        grdEmp.DataSource = dtCurrentTable;
                        grdEmp.DataBind();

                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }

        }


    }
}