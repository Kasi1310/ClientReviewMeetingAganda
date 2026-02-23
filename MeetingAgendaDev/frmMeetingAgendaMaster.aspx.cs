using ClientMeetingAgenda.App_Code;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClientMeetingAgenda
{
	public partial class frmMeetingAgendaMaster : System.Web.UI.Page
	{
		clsMeetingAgenda objclsMeetingAgenda;
		clsUsers objclsUsers;
		clsClientMaster objclsClientMaster;
		protected void Page_Load(object sender, EventArgs e)
		{
			
			if (Session["UserName"] == null || Session["Role"] == null)
			{
				ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Session expired');", true);
				Response.Redirect("frmLogin.aspx");
			}
			if (!IsPostBack)
			{
				objclsUsers = new clsUsers();
				objclsUsers.LoadAccExecDDL(ddlAccountExecutive);

				objclsClientMaster = new clsClientMaster();
				objclsClientMaster.LoadClientDDL(ddlClientNo, ddlClientName);

				LoadGridview();
				//LoadSignatureridview();
			}
			if (Session["FileDownload"] != null)
			{
				Session["FileDownload"] = null;
				//ClientScript.RegisterStartupScript(GetType(), "myscript", "alert('Your file generated successfully. Please click on view to download the pdf.');", true);
				lblPopUpMessage.Text = "Your file generated successfully. Please click on view to download the pdf.";
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
			}

			//divSearch1.Visible = false;
			//divSearch2.Visible = false;
		}

		private void LoadGridview()
		{
            
            objclsMeetingAgenda = new clsMeetingAgenda();
			DataTable dt = new DataTable();

			dt = objclsMeetingAgenda.SelectMeetingAgendaStatus("Grid", int.Parse(ddlClientName.SelectedValue.Trim()),
                ddlAccountExecutive.SelectedItem.Text.ToString().Trim()
                , ddlPDFStatus.SelectedValue.Trim(), ddlMeetingType.SelectedValue.Trim(), txtMeetingFromDate.Text.Trim(), txtMeetingToDate.Text.Trim());


			gvMAMaster.DataSource = dt;
			gvMAMaster.DataBind();

			ViewState["dtMAMaster"] = dt;
			ViewState["sortdr"] = "Desc";

		}
		
		private void LoadHistoryGridView()
		{
			objclsMeetingAgenda = new clsMeetingAgenda();
			objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
			gvHistory.DataSource = objclsMeetingAgenda.SelectMeetingAgendaHistory();
			gvHistory.DataBind();


		}

		private void LoadSurveyGridView()
		{
			objclsMeetingAgenda = new clsMeetingAgenda();
			objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
			gvSurvey.DataSource = objclsMeetingAgenda.SelectMeetingAgendaSurvey();
			gvSurvey.DataBind();


		}

		protected void gvMAMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvMAMaster.PageIndex = e.NewPageIndex;
			LoadGridview();
		}

		protected void gvMAMaster_Sorting(object sender, GridViewSortEventArgs e)
		{
			DataTable dtrslt = (DataTable)ViewState["dtMAMaster"];
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
				gvMAMaster.DataSource = dtrslt;
				gvMAMaster.DataBind();

			}
		}

		protected void gvMAMaster_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "cmdEdit")
			{
				Session["ssnMAID"] = int.Parse(e.CommandArgument.ToString());
				Session["ssnMode"] = "Edit";
				Response.Redirect("frmMAPage1.aspx");
			}
			if (e.CommandName == "cmdView")
			{
				//string designationFilePath = ConfigurationManager.AppSettings["upload.file.path"].ToString() + e.CommandArgument.ToString();
				string designationFilePath =  e.CommandArgument.ToString();

				Response.ContentType = "application/pdf";
				Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(designationFilePath));
				Response.TransmitFile(designationFilePath);
				Response.End();

				//System.Diagnostics.Process.Start(designationFilePath);
			}
			if (e.CommandName == "cmdComplete")
			{
				//SendSurveyMail(int.Parse(e.CommandArgument.ToString()));

				//Response.Redirect(Request.Url.AbsoluteUri);

				hdnMeetingAgendaID.Value = e.CommandArgument.ToString();
				hdnUserName.Value = Session["UserName"].ToString().Trim();
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenConfirmPopup();", true);

			}
			else if (e.CommandName == "cmdReOpen")
			{
				hdnMeetingAgendaID.Value = e.CommandArgument.ToString();
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenReOpenPopup();", true);
			}
			else if (e.CommandName == "cmdHistory")
			{
				hdnMeetingAgendaID.Value = e.CommandArgument.ToString();
				LoadHistoryGridView();
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenHistoryPopup();", true);
			}
			else if (e.CommandName == "cmdSurvey")
			{
				hdnMeetingAgendaID.Value = e.CommandArgument.ToString();
				LoadSurveyGridView();
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenSurveyPopup();", true);
			}
			else if (e.CommandName == "cmdDelete")
			{
				hdnMeetingAgendaID.Value = e.CommandArgument.ToString();
				ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenDeletePopup();", true);
			}
		}

		protected void gvMAMaster_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				HiddenField gvhdnIsCompleted = (e.Row.FindControl("gvhdnIsCompleted") as HiddenField);
				Label gvlblPDFStatus = (e.Row.FindControl("gvlblPDFStatus") as Label);
				LinkButton gvlnkEdit = (e.Row.FindControl("gvlnkEdit") as LinkButton);
				LinkButton gvlnkView = (e.Row.FindControl("gvlnkView") as LinkButton);
				LinkButton gvlnkReOpen = (e.Row.FindControl("gvlnkReOpen") as LinkButton);
				LinkButton gvlnkComplete = (e.Row.FindControl("gvlnkComplete") as LinkButton);

				Label gvlblView = (e.Row.FindControl("gvlblView") as Label);
				Label gvlblComplete = (e.Row.FindControl("gvlblComplete") as Label);

				Label gvlblDelete = (e.Row.FindControl("gvlblDelete") as Label);
				LinkButton gvlnkDelete = (e.Row.FindControl("gvlnkDelete") as LinkButton);

				if (gvlblPDFStatus.Text.ToUpper() == "CREATED")
				{
					gvlnkEdit.Visible = false;
					gvlnkView.Visible = true;


					if (Convert.ToBoolean(gvhdnIsCompleted.Value))
					{
						gvlblView.Visible = false;
						gvlnkComplete.Visible = false;
						gvlblComplete.Visible = false;
						gvlnkReOpen.Visible = false;

						gvlnkDelete.Visible = false;
						gvlblDelete.Visible = false;

						if (Session["Role"].ToString().Trim().ToUpper() == "ADMINISTRATOR")
						{
							gvlblComplete.Visible = true;
							gvlnkReOpen.Visible = true;
						}


					}
					else
					{
						gvlblView.Visible = true;
						gvlnkComplete.Visible = true;
						gvlblComplete.Visible = true;
						gvlnkReOpen.Visible = true;
						gvlnkDelete.Visible = true;
						gvlblDelete.Visible = true;
					}
				}
				else
				{
					gvlnkEdit.Visible = true;
					gvlnkView.Visible = false;
					gvlblView.Visible = false;
					gvlnkComplete.Visible = false;
					gvlblComplete.Visible = false;
					gvlnkReOpen.Visible = false;
				}
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			LoadGridview();
		}

		protected void btnClear_Click(object sender, EventArgs e)
		{
			Response.Redirect(Request.Url.AbsoluteUri);
		}

		protected void btnExport_Click(object sender, EventArgs e)
		{
			objclsMeetingAgenda = new clsMeetingAgenda();
			DataTable dt = new DataTable();

			dt = objclsMeetingAgenda.SelectMeetingAgendaStatus("Export", int.Parse(ddlClientName.SelectedValue.Trim()),
                ddlAccountExecutive.SelectedItem.Text.ToString().Trim()
                , ddlPDFStatus.SelectedValue.Trim(), ddlMeetingType.SelectedValue.Trim(), txtMeetingFromDate.Text.Trim(), txtMeetingToDate.Text.Trim());
			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dt, "Meeting Agenda");

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

		protected void ddlClientNo_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlClientName.SelectedValue = ddlClientNo.SelectedValue;
		}

		protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
		{
			ddlClientNo.SelectedValue = ddlClientName.SelectedValue;
		}

		protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

			gvHistory.PageIndex = e.NewPageIndex;
			LoadHistoryGridView();

			ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenHistoryPopup();", true);
		}


		protected void gvSurvey_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvSurvey.PageIndex = e.NewPageIndex;
			LoadSurveyGridView();

			ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenSurveyPopup();", true);
		}

		protected void btnDeleteSubmit_Click(object sender, EventArgs e)
		{
			objclsMeetingAgenda = new clsMeetingAgenda();
			objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
			objclsMeetingAgenda.DeleteMeetingAgenda(txtDeleteComment.Text.Trim());
			LoadGridview();
		}

		protected void btnReOpenSubmit_Click(object sender, EventArgs e)
		{
			objclsMeetingAgenda = new clsMeetingAgenda();
			objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
			objclsMeetingAgenda.FileName = "";
			objclsMeetingAgenda.LastUpdatedBy = Session["UserName"].ToString().Trim();
			objclsMeetingAgenda.UpdatePDFStatus(txtReOpenReason.Text.Trim());

			Response.Redirect(Request.Url.AbsoluteUri);
		}

		
	}
}