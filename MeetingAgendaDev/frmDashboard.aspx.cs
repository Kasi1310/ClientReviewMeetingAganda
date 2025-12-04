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
    public partial class frmDashboard : System.Web.UI.Page
    {
        clsReport objclsReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Role"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                LoadGridview();
            }
        }

        private void LoadGridview()
        {
            DataSet ds = new DataSet();
            objclsReport = new clsReport();
            ds = objclsReport.SelectDashboardReport();

            if(ds!=null && ds.Tables.Count==6)
            {
                gvAEsReview.DataSource = ds.Tables[0];
                gvAEsReview.DataBind();

                gvAEsWeekly.DataSource = ds.Tables[1];
                gvAEsWeekly.DataBind();

                gvUpcomingMeeting.DataSource = ds.Tables[2];
                gvUpcomingMeeting.DataBind();

                gvClientAEs.DataSource = ds.Tables[3];
                gvClientAEs.DataBind();

                gvMeetingType.DataSource = ds.Tables[4];
                gvMeetingType.DataBind();

                gvAEsLastReview.DataSource = ds.Tables[5];
                gvAEsLastReview.DataBind();
            }
        }

        protected void gvAEsReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAEsReview.PageIndex = e.NewPageIndex;
            LoadGridview();
        }

        protected void gvAEsWeekly_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAEsWeekly.PageIndex = e.NewPageIndex;
            LoadGridview();
        }

        protected void gvUpcomingMeeting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUpcomingMeeting.PageIndex = e.NewPageIndex;
            LoadGridview();
        }

        protected void gvClientAEs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClientAEs.PageIndex = e.NewPageIndex;
            LoadGridview();
        }

        protected void gvMeetingType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMeetingType.PageIndex = e.NewPageIndex;
            LoadGridview();
        }

        protected void gvAEsLastReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAEsLastReview.PageIndex = e.NewPageIndex;
            LoadGridview();
        }
    }
}