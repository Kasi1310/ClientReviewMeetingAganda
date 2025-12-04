using ClientMeetingAgenda.App_Code;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
    public partial class frmReport : System.Web.UI.Page
    {
        clsUsers objclsUsers;
        clsClientMaster objclsClientMaster;
        clsReport objclsReport;

        DataTable dtReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["Role"] == null || Session["ssnReport"] == null)
            {
                Response.Redirect("frmLogin.aspx");
            }
            if (!IsPostBack)
            {
                objclsUsers = new clsUsers();
                objclsUsers.LoadAccExecDDL(ddlAccountExecutive);

                objclsClientMaster = new clsClientMaster();
                objclsClientMaster.LoadClientDDL(ddlClientNo, ddlClientName);
            }

            if (Session["ssnReport"].ToString() == "Report2" || Session["ssnReport"].ToString() == "Report3" || Session["ssnReport"].ToString() == "Report6")
            {
                divMeetingFromDate.Style.Add("display", "none");
                divMeetingToDate.Style.Add("display", "none");
            }
            LoadGrid();
        }
        private void LoadGrid()
        {
            DataSet ds = new DataSet();
            objclsReport = new clsReport();
            objclsReport.Mode = Session["ssnReport"].ToString().Trim();
            objclsReport.ClientID = ddlClientNo.SelectedValue.Trim();
            objclsReport.AEsID = ddlAccountExecutive.SelectedValue.Trim();
            objclsReport.MeetingType = ddlMeetingType.SelectedValue.Trim();
            objclsReport.MeetingFromDate = txtMeetingFromDate.Text.Trim();
            objclsReport.MeetingToDate = txtMeetingToDate.Text.Trim();

            ds = objclsReport.SelectDashboardReport();

            if (ds != null && ds.Tables.Count == 1)
            {
                dtReport = new DataTable();
                dtReport = ds.Tables[0];
                ViewState["dtReport"] = dtReport;
                ViewState["sortdr"] = "Asc";
                gvReview.DataSource = dtReport;
                gvReview.DataBind();
            }

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            if(ViewState["dtReport"]==null)
            {
                LoadGrid();
            }
            dtReport = new DataTable();
            dtReport = (DataTable)ViewState["dtReport"];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtReport, "Meeting Agenda");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Meeting_Agenda_" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void gvReview_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dtReport"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                gvReview.DataSource = dtrslt;
                gvReview.DataBind();

            }
        }

        protected void gvReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReview.PageIndex = e.NewPageIndex;
            LoadGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientNo.SelectedValue = ddlClientName.SelectedValue;
        }

        protected void ddlClientNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlClientName.SelectedValue = ddlClientNo.SelectedValue;
        }

        
    }
}