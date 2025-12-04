using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class frmReportMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Role"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
        }

        protected void lnkReport1_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report1";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport2_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report2";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport3_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report3";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport4_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report4";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport5_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report5";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport6_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report6";
            Response.Redirect("frmReport.aspx");
        }

        protected void lnkReport7_Click(object sender, EventArgs e)
        {
            Session["ssnReport"] = "Report7";
            Response.Redirect("frmReport.aspx");
        }
    }
}