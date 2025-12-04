using ClientMeetingAgenda.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class frmClientMaster : System.Web.UI.Page
    {
        DataTable dtDetails;
        clsClientMaster objclsClientMaster;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null || Session["Role"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                LoadClientMasterGrid();
            }
        }

        private void LoadClientMasterGrid()
        {
            objclsClientMaster = new clsClientMaster();

            gvClients.DataSource = objclsClientMaster.SelectClientMaster();
            gvClients.DataBind();
        }

        protected void gvClients_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClients.PageIndex = e.NewPageIndex;
            LoadClientMasterGrid();
        }


        protected void gvClients_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            objclsClientMaster = new clsClientMaster();
            if (e.CommandName == "cmdDelete")
            {
                objclsClientMaster.ID = int.Parse(e.CommandArgument.ToString());
                objclsClientMaster.LastUpdatedBy = Session["UserName"].ToString().Trim();
                objclsClientMaster.UpdateClientMaster();
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objclsClientMaster = new clsClientMaster();
            objclsClientMaster.ClientNo = txtClientNo.Text.Trim();
            objclsClientMaster.ClientName = txtClientName.Text.Trim();
            objclsClientMaster.LastUpdatedBy = Session["UserName"].ToString().Trim();

            string strOutput = objclsClientMaster.InsertClientMaster();

            if (strOutput != "")
            {
                ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('" + strOutput + "');", true);
            }
            else
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void gvClients_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton gvlnkDelete = (LinkButton)e.Row.FindControl("gvlnkDelete");

                gvlnkDelete.Attributes.Add("OnClientClick", string.Format("return confirm('Are you sure to {0}?')", gvlnkDelete.Text.Trim()));
            }
        }
    }
}