using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if ((bool)Session["IsFirstLogin"] || (bool)Session["IsPasswordExpired"])
            {
                //liDashboard.Visible = false;
                liNewMA.Visible = false;
                liUsers.Visible = false;
                //liClients.Visible = false;
                liMAFileApproval.Visible = false;
                liReport.Visible = false;
                liLogout.Visible = false;
            }
            else
            {
                //liDashboard.Visible = true;
                liNewMA.Visible = true;
                //liClients.Visible = true;
                liMAFileApproval.Visible = true;
                liReport.Visible = true;
                liLogout.Visible = true;

                if(Session["Role"].ToString().Trim().ToUpper()!= "ADMINISTRATOR")
                {
                    liUsers.Visible = false;
                }
            }
        }

        protected void lnkNewMA_Click(object sender, EventArgs e)
        {
            Session["ssnMAID"] = null;
            Session["dtAttendeesInvited"] = null;
            Response.Redirect("frmMAPage1.aspx");
        }
    }
}