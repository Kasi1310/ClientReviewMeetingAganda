using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClientMeetingAgenda.App_Code;

namespace ClientMeetingAgenda
{
    public partial class frmUsers : System.Web.UI.Page
    {
        DataTable dtDetails;

        clsUsers objclsUsers;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null || Session["Role"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                LoadUserGrid();
            }
        }

        private void LoadUserGrid()
        {
            objclsUsers = new clsUsers();

            gvUsers.DataSource = objclsUsers.SelectUsers("SELECTALL");
            gvUsers.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objclsUsers = new clsUsers();
            objclsUsers.Name = txtName.Text.Trim();
            objclsUsers.Role = ddlRole.SelectedValue.Trim();
            objclsUsers.Phone = txtPhone.Text.Trim();
            objclsUsers.LastUpdatedBy = Session["UserName"].ToString().Trim();
            if (btnSubmit.Text.Trim() == "Add")
            {
                objclsUsers.UserName = txtUserName.Text.Trim();
                objclsUsers.Password = CGCipher.Encrypt(txtPassword.Text.Trim(), "Medicount");

                int ID = objclsUsers.InsertUsers();
                if (ID == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('User Name already exists');", true);
                }
                else
                {
                    Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            else
            {
                objclsUsers.ID = int.Parse(hdnUserID.Value.Trim());
                objclsUsers.UpdateUsers("UPDATE");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            LoadUserGrid();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            objclsUsers = new clsUsers();
            //int rowIndex = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = gvUsers.Rows[rowIndex];

            //HiddenField gvhdnEmpID = (HiddenField)row.FindControl("gvhdnEmpID");

            objclsUsers = new clsUsers();
            dtDetails = new DataTable();

            if (e.CommandName == "cmdEdit")
            {
                hdnUserID.Value = e.CommandArgument.ToString();
                objclsUsers.ID = int.Parse(e.CommandArgument.ToString());
                dtDetails = objclsUsers.SelectUsers("SELECTBYID");
                if (dtDetails != null && dtDetails.Rows.Count != 0)
                {
                    txtName.Text = dtDetails.Rows[0]["Name"].ToString().Trim();
                    txtUserName.Text = dtDetails.Rows[0]["UserName"].ToString().Trim();

                    ddlRole.SelectedValue = dtDetails.Rows[0]["Role"].ToString().Trim();
                    txtPhone.Text= dtDetails.Rows[0]["Phone"].ToString().Trim();

                    txtUserName.Attributes.Add("disabled", "disabled");
                    txtPassword.Attributes.Add("disabled", "disabled");
                    //txtPassword.Attributes.Add("style", "display:none");
                    btnSubmit.Text = "Update";
                }
            }
            else if (e.CommandName == "cmdDelete")
            {
                objclsUsers.ID = int.Parse(e.CommandArgument.ToString());
                objclsUsers.LastUpdatedBy = Session["UserName"].ToString().Trim();
                objclsUsers.UpdateUsers("DELETE");
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}