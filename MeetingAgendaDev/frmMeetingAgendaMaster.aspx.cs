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

			dt = objclsMeetingAgenda.SelectMeetingAgendaStatus("Grid", int.Parse(ddlClientName.SelectedValue.Trim()), int.Parse(ddlAccountExecutive.SelectedValue.Trim())
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
				string designationFilePath = ConfigurationManager.AppSettings["upload.file.path"].ToString() + e.CommandArgument.ToString();

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

			dt = objclsMeetingAgenda.SelectMeetingAgendaStatus("Export", int.Parse(ddlClientName.SelectedValue.Trim()), int.Parse(ddlAccountExecutive.SelectedValue.Trim())
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

		//protected void btnOk_Click(object sender, EventArgs e)
		//{
		//    DataSet ds = new DataSet();
		//    DataTable dtAttendeesInvited = new DataTable();
		//    clsMeetingAgenda objclsMeetingAgenda = new clsMeetingAgenda();

		//    objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
		//    ds = objclsMeetingAgenda.SelectMeetingAgenda();

		//    if (ds == null || ds.Tables.Count != 3 || ds.Tables[0].Rows.Count == 0)
		//    {
		//        return;
		//    }

		//    dtAttendeesInvited = ds.Tables[1];

		//    if (SendSurveyMail(dtAttendeesInvited, ds.Tables[0].Rows[0]["MeetingDate"].ToString().Trim(), ds.Tables[0].Rows[0]["FileName"].ToString().Trim(), Convert.ToInt64(ds.Tables[0].Rows[0]["ZohoId"].ToString().Trim())))
		//    {
		//        clsSendMail objclsSendMail = new clsSendMail();
		//        string ToMailID = ConfigurationManager.AppSettings["MeetingAgenda.pdf.mail"].ToString();
		//        string Attachement = "";
		//        Attachement = ConfigurationManager.AppSettings["upload.file.path"].ToString() + ds.Tables[0].Rows[0]["FileName"].ToString().Trim();

		//        objclsSendMail.SendMail(ToMailID, "", ""
		//            , ds.Tables[0].Rows[0]["ClientNo"].ToString().Trim()+"_"+ ds.Tables[0].Rows[0]["ClientName"].ToString().Trim() +"_"+ "Client Review Meeting Agenda", MeetingAgendaMailBody(ds.Tables[0].Rows[0]["ClientName"].ToString().Trim(), ds.Tables[0].Rows[0]["ClientNo"].ToString().Trim(), ds.Tables[0].Rows[0]["MeetingDate"].ToString().Trim()), Attachement);


		//        Response.Redirect(Request.Url.AbsoluteUri);
		//    }
		//    else
		//    {
		//        ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
		//    }
		//}
		//private bool SendSurveyMail(DataTable dtAttendeesInvited, string MeetingDate, string FileName, long ZohoId)
		//{
		//    lblPopUpMessage.Text = "";
		//    bool isMailSend = false;
		//    int MailSendCount = 0;

		//    clsSendMail objclsSendMail = new clsSendMail();
		//    objclsMeetingAgenda = new clsMeetingAgenda();

		//    for (int i = 0; i < dtAttendeesInvited.Rows.Count; i++)
		//    {
		//        if (!Convert.ToBoolean(dtAttendeesInvited.Rows[i]["IsSurveyMailSend"].ToString().Trim()))
		//        {
		//            isMailSend = objclsSendMail.SendMail(dtAttendeesInvited.Rows[i]["Email"].ToString().Trim(), "", "", "Client Review Meeting - Quick survey", MailBody(dtAttendeesInvited.Rows[i]["ID"].ToString().Trim(), MeetingDate), "");
		//            if (isMailSend)
		//            {
		//                MailSendCount += 1;
		//                objclsMeetingAgenda.UpdateAttendesSurveyMailSendStatus(dtAttendeesInvited.Rows[i]["ID"].ToString().Trim());
		//            }
		//            else
		//            {
		//                lblPopUpMessage.Text = "Not able to send mail for " + dtAttendeesInvited.Rows[i]["Email"].ToString().Trim() + " attendee.";
		//                //ClientScript.RegisterStartupScript(GetType(), "myscript", "OpenMessagePopup();", true);
		//                //break;
		//            }
		//        }
		//        else
		//        {
		//            MailSendCount += 1;
		//        }
		//    }


		//    if (MailSendCount == dtAttendeesInvited.Rows.Count)
		//    {
		//        objclsMeetingAgenda = new clsMeetingAgenda();
		//        objclsMeetingAgenda.ID = int.Parse(hdnMeetingAgendaID.Value.Trim());
		//        objclsMeetingAgenda.LastUpdatedBy = Session["UserName"].ToString().Trim();
		//        objclsMeetingAgenda.UpdateMeetingAgendaCompleteStatus();

		//        try
		//        {
		//            if (ZohoId == 0)
		//            {
		//                objclsSendMail.SendMail("vanithac@medicount.com", "", "", "Client Review Meeting - Zoho upload error", "Zoho Id is empty<br />File Name:" + FileName + "<br />", "");
		//            }
		//            else
		//            {
		//                //string UploadStatus = UploadDocumentToZOHO(FileName, ZohoId);

		//                string UploadStatus = "200";

		//                if (UploadStatus != "200")
		//                {
		//                    objclsSendMail.SendMail("vanithac@medicount.com", "", "", "Client Review Meeting - Zoho upload error", "Upload Status Code:" + UploadStatus + "<br />File Name:" + FileName + "<br />", "");
		//                }
		//            }
		//        }
		//        catch (Exception ex)
		//        {
		//            objclsSendMail.SendMail("vanithac@medicount.com", "", "", "Client Review Meeting - Zoho upload error", "Error:" + ex.Message + "<br />File Name:" + FileName + "<br />", "");
		//            return true;
		//        }

		//        return true;
		//    }
		//    else
		//    {
		//        if (lblPopUpMessage.Text.Trim() == "")
		//        {
		//            lblPopUpMessage.Text = "Unable to upload the file to Zoho due to survey mail not send to all attendees.";
		//        }

		//        return false;
		//    }
		//}

		//private string MailBody(string ID, string MeetingDate)
		//{
		//    StringBuilder sb = new StringBuilder();

		//    string url = HttpContext.Current.Request.Url.AbsoluteUri;
		//    string imagePath = "";
		//    int lastIndex = url.LastIndexOf("/");
		//    //imagePath = url.Substring(0, lastIndex) + "/Images/Logo.jpg";

		//    url = url.Substring(0, lastIndex);
		//    lastIndex = url.LastIndexOf("/");

		//    url = "https://snapshots.medicount.com/frmSurvey.aspx?ID=" + System.Web.HttpUtility.UrlEncode(ID.ToString()); //CGCipher.Encrypt(ID.ToString(), "");


		//    imagePath = "https://snapshots.medicount.com/Images/";

		//    sb.AppendLine("<html>");
		//    sb.AppendLine("<head>");
		//    sb.AppendLine("<meta http-equiv=Content-Type content='text / html; charset = windows - 1252'>");
		//    sb.AppendLine("<meta name=Generator content='Microsoft Word 15(filtered)'>");
		//    sb.AppendLine("<style>");
		//    sb.AppendLine("@font-face {");
		//    sb.AppendLine("font-family: 'Cambria Math';");
		//    sb.AppendLine("panose-1: 2 4 5 3 5 4 6 3 2 4;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("");
		//    sb.AppendLine("@font-face {");
		//    sb.AppendLine("font-family: Calibri;");
		//    sb.AppendLine("panose-1: 2 15 5 2 2 2 4 3 2 4;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("p.MsoNormal, li.MsoNormal, div.MsoNormal {");
		//    sb.AppendLine("margin-top: 0in;");
		//    sb.AppendLine("margin-right: 0in;");
		//    sb.AppendLine("margin-bottom: 8.0pt;");
		//    sb.AppendLine("margin-left: 0in;");
		//    sb.AppendLine("line-height: 107%;");
		//    sb.AppendLine("font-size: 11.0pt;");
		//    sb.AppendLine("font-family: 'Calibri',sans-serif;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("");
		//    sb.AppendLine(".MsoChpDefault {");
		//    sb.AppendLine("font-family: 'Calibri',sans-serif;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("");
		//    sb.AppendLine(".MsoPapDefault {");
		//    sb.AppendLine("margin-bottom: 8.0pt;");
		//    sb.AppendLine("line-height: 107%;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("");
		//    sb.AppendLine("@page WordSection1 {");
		//    sb.AppendLine("size: 8.5in 11.0in;");
		//    sb.AppendLine("margin: 1.0in 1.0in 1.0in 1.0in;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("");
		//    sb.AppendLine("div.WordSection1 {");
		//    sb.AppendLine("page: WordSection1;");
		//    sb.AppendLine("}");
		//    sb.AppendLine("</style>");
		//    sb.AppendLine("</head>");
		//    sb.AppendLine("<body lang=EN-US style='word-wrap:break-word;'>");
		//    sb.AppendLine("<div class=WordSection1>");
		//    sb.AppendLine("<center>");
		//    sb.AppendLine("<table class=MsoTableGrid border=1 cellspacing=0 cellpadding=0 style='border-collapse:collapse;border:none;'>");
		//    sb.AppendLine("<tr style='height:429.1pt'>");
		//    sb.AppendLine("<td width=628 valign=top style='width:471.25pt;border:solid #009094 3.0pt;padding:0in 5.4pt 0in 5.4pt;height:429.1pt'>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center;line-height:normal'>");
		//    sb.AppendLine("<img width=548 height=147 id='Picture 1' src='" + imagePath + "Medicount_Logo_Quick.png'>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<span style='font-size:36.0pt;color:#009094'>");
		//    sb.AppendLine("Medicount needs your help!");
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<span style='font-size:28.0pt'>");
		//    sb.AppendLine("Please give us feedback on your recent");
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</p>");

		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<b>");
		//    sb.AppendLine("<span style='font-size:26.0pt;'>");
		//    sb.AppendLine("Client Review Meeting on " + MeetingDate);
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</b>");
		//    sb.AppendLine("</p>");


		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'><span style='font-size:14.0pt'>&nbsp;</span></p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<a target='_blank' href='" + url + "'");
		//    sb.AppendLine("<span style='font-size:36.0pt;color:windowtext;text-decoration:none'>");
		//    sb.AppendLine("<img border=0 width=266 height=44 id='Picture 2' src='" + imagePath + "QuickSurvey.png'>");
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</a>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'><span style='font-size:14.0pt'>&nbsp;</span></p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<span style='font-size:28.0pt'>");
		//    sb.AppendLine("Your responses will tell us areas needed for improvement.");
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<p class=MsoNormal style='margin-bottom:0in;line-height:normal'>");
		//    sb.AppendLine("<span style='font-size:14.0pt'>&nbsp;</span>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<p class=MsoNormal align=center style='margin-bottom:0in;text-align:center; line-height:normal'>");
		//    sb.AppendLine("<b>");
		//    sb.AppendLine("<span style='font-size:28.0pt;color:#009094'>");
		//    sb.AppendLine("Thank you.");
		//    sb.AppendLine("</span>");
		//    sb.AppendLine("</b>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("</td>");
		//    sb.AppendLine("</tr>");
		//    sb.AppendLine("</table>");
		//    sb.AppendLine("</center>");
		//    sb.AppendLine("<p class=MsoNormal>&nbsp;</p>");
		//    sb.AppendLine("</div>");
		//    sb.AppendLine("</body>");
		//    sb.AppendLine("</html>");

		//    return sb.ToString();
		//}

		//private string UploadDocumentToZOHO(string FileName, long ZohoId)
		//{
		//    clsInitialize objclsInitialize = new clsInitialize();
		//    clsAttachment objclsAttachment = new clsAttachment();

		//    objclsInitialize.SDKInitialize();
		//    return objclsAttachment.UploadAttachments("Accounts", ZohoId, ConfigurationManager.AppSettings["upload.file.path"].ToString() + FileName);

		//}

		//private static string MeetingAgendaMailBody(string ClientName, string ClientNo, string MeetingDate)
		//{
		//    StringBuilder sb = new StringBuilder();
		//    sb.AppendLine("<html>");
		//    sb.AppendLine("<head>");
		//    sb.AppendLine("<meta charset='utf-8' />");
		//    sb.AppendLine("<title></title>");
		//    sb.AppendLine("<style>.paraDesign {margin: 0in;font-size: 11.0pt;font-family: Calibri;}</style>");
		//    sb.AppendLine("</head>");
		//    sb.AppendLine("<body>");
		//    sb.AppendLine("<table border='0' cellpadding='0' width='741' style='width: 556.1pt; transform: scale(0.977887, 0.977887); transform-origin: left top;' min-scale='0.9778869778869779'>");
		//    sb.AppendLine("<tbody>");
		//    //sb.AppendLine("<tr style='height:8.15pt'>");
		//    //sb.AppendLine("<td style='padding:.75pt .75pt .75pt .75pt; height:8.15pt'>");
		//    //sb.AppendLine("<p class='paraDesign'><span style='font-size:14.0pt'>Hi, </span></p>");
		//    //sb.AppendLine("</td>");
		//    //sb.AppendLine("</tr>");
		//    sb.AppendLine("<tr style='height:8.15pt'>");
		//    sb.AppendLine("<td style='border:solid #009094 3.0pt;padding:0.1in 5.4pt 0in 5.4pt;'>");
		//    sb.AppendLine("<p class='paraDesign'>");
		//    sb.AppendLine("<span style='font-size:14.0pt'>You are receiving this report to review and ensure that any items relevant to you or your department are addressed. This review includes changes in officials (authorized personnel, Chief, fiscal officers), trends (RPT, runs), address and rate changes, or any other matters that require follow-up and discussion with the account executive or senior management.</span>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<br />");
		//    sb.AppendLine("<p class='paraDesign'>");
		//    sb.AppendLine("<span style='font-size:14.0pt'>Please contact the account executive if you have any questions about this report. Our collective responsibility to our clients and Medicount is to stay informed and ensure nothing falls through the cracks</span>");
		//    sb.AppendLine("</p>");
		//    sb.AppendLine("<br />");
		//    sb.AppendLine("<p class='paraDesign'><span style='font-size:14.0pt'>&nbsp;</span></p><p class='paraDesign'><span style='font-size:14.0pt'>Thank you</span></p><p class='paraDesign'><b><span style='font-size: 14pt; color: rgb(0, 144, 148) !important;'>Medicount Management, Inc.</span></b><b></b></p>");
		//    sb.AppendLine("</td>");
		//    sb.AppendLine("</tr>");
		//    sb.AppendLine("</tbody>");
		//    sb.AppendLine("</table>");
		//    sb.AppendLine("</body>");
		//    sb.AppendLine("</html>");

		//    return sb.ToString();
		//}

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

		protected void btnExport_Click1(object sender, EventArgs e)
		{

		}
	}
}