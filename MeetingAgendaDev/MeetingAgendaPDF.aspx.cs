using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Net;

namespace ClientMeetingAgenda
{
    public partial class MeetingAgendaPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void GeneratePDF()
        {
            string designationFileName = @"D:\Common\MeetingAgenda\test.pdf";

            if (File.Exists(designationFileName))
            {
                File.Delete(designationFileName);
            }

            FileStream fs = new FileStream(designationFileName, FileMode.Create);
            // Create an instance of the document class which represents the PDF document itself.  
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            HTMLWorker htmlparser = new HTMLWorker(document);
            // Create an instance to the PDF file by creating an instance of the PDF   
            // Writer class using the document and the filestrem in the constructor.  

            if (document == null)
            {
                document = new Document(PageSize.A4, 25, 25, 30, 30);
            }


            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            writer.PageEvent = new PDFFooter();


            // Open the document to enable you to write to the document  
            document.Open();



            BaseColor customBGGreenColor = new BaseColor(0, 150, 143);
            BaseColor customBGAshColor = new BaseColor(93, 103, 112);

            BaseFont baseFont = BaseFont.CreateFont(Server.MapPath(@"\CalibriFonts\Calibri.ttf"), "Identity-H", BaseFont.EMBEDDED);
            var fontHeader = new Font(baseFont, 12, Font.NORMAL, BaseColor.WHITE);
            var fontContent = new Font(baseFont, 12, Font.NORMAL);
            var fontContentGreen = new Font(baseFont, 12, Font.BOLD, customBGGreenColor);


            ////Font fontHeader = FontFactory.GetFont("Calibri", 10, Font.BOLD, BaseColor.WHITE);
            //Font fontHeaderUnderLine = FontFactory.GetFont("Calibri", 16, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
            //Font fontSubHeaderUnderLine = FontFactory.GetFont("Calibri", 14, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK);
            //Font fontSubHeader = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.BLACK);

            //Font fontSubHeaderWhite = FontFactory.GetFont("Calibri", 14, Font.BOLD, BaseColor.WHITE);
            ////Font fontContent = FontFactory.GetFont("Calibri Bold", 10, Font.NORMAL);
            //Font fontContentUnderLine = FontFactory.GetFont("Calibri", 11, Font.NORMAL | Font.UNDERLINE);
            //Font fontContentBold = FontFactory.GetFont("Calibri", 11, Font.BOLD);
            //Font fontContentBoldUnderLine = FontFactory.GetFont("Calibri", 11, Font.BOLD | Font.UNDERLINE);
            //Font fontSmallContent = FontFactory.GetFont("Calibri", 10, Font.NORMAL);
            //Font fontSmallContentBold = FontFactory.GetFont("Calibri", 10, Font.BOLD);



            float[] widths = new float[] { 28, 44, 28 };

            PdfPTable table = new PdfPTable(3);


            PdfPCell cell;

            Image image;

            // BaseColor customContentBaseColor = new BaseColor(230, 252, 251);

            table = new PdfPTable(3);
            table.WidthPercentage = 93f;
            table.HorizontalAlignment = 1;
            table.SetWidths(widths);

            document.NewPage();
            document.Add(new Paragraph(20, "\u00a0"));

            image = Image.GetInstance(Server.MapPath(@"\Images\Logo.jpg"));
            image.ScaleAbsolute(300f, 80f);
            cell = new PdfPCell(image, false);
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("CLIENT REVIEW MEETING AGENDA", new Font(baseFont, 20, Font.BOLD, customBGGreenColor)));
            cell.PaddingBottom = 15f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 1; //0=Left, 1=Center, 2=Right
            table.AddCell(cell);


            PdfPTable childTable1 = new PdfPTable(3);
            float[] widthsC1 = new float[] { 10, 60, 30 };
            childTable1.SetWidths(widthsC1);

            cell = new PdfPCell(new Phrase("CLIENT#", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("CLIENT NAME", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("352", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("Village of Roseville", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(new Phrase("11/11/2021", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable1.AddCell(cell);

            cell = new PdfPCell(childTable1);
            //cell.PaddingTop = 30f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable2 = new PdfPTable(4);
            float[] widthsC2 = new float[] { 20, 40, 20, 20 };
            childTable2.SetWidths(widthsC2);

            cell = new PdfPCell(new Phrase("ACCOUNT EXECUTIVE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("PHONE #", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING TYPE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("Heath Smedley", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("hsmedley@medicount.com", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("513-612-3157", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(new Phrase("In Person-CR", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable2.AddCell(cell);

            cell = new PdfPCell(childTable2);
            //cell.PaddingTop = 30f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable3 = new PdfPTable(3);
            float[] widthsC3 = new float[] { 25, 40, 35 };
            childTable3.SetWidths(widthsC3);

            cell = new PdfPCell(new Phrase("CALL IN NUMBER", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING ID/CODE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("MEETING WEB LINK", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("352256982", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("Test ID", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(new Phrase("Go to meeting", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable3.AddCell(cell);

            cell = new PdfPCell(childTable3);
            cell.PaddingTop = 15f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable4 = new PdfPTable(3);
            float[] widthsC4 = new float[] { 30, 30, 40 };
            childTable4.SetWidths(widthsC4);

            cell = new PdfPCell(new Phrase("ATTENDEES INVITED", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("NAME", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("TITLE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("EMAIL", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("Derrick Keylor", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("Chief", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("dkeylor@columbus.rr.com", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable4.AddCell(cell);

            cell = new PdfPCell(childTable4);
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("CLIENT REVENUE NUMBERS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable5 = new PdfPTable(6);
            float[] widthsC5 = new float[] { 20, 15, 20, 10, 20, 15 };
            childTable5.SetWidths(widthsC5);


            cell = new PdfPCell(new Phrase("YTD REVENUE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 31,298.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("YTD TRANSPORTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("115", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("REVENUE PER TRANSPORT", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 272.16", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable5.AddCell(cell);

            cell = new PdfPCell(childTable5);
            // cell.PaddingTop = 10f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable6 = new PdfPTable(4);
            float[] widthsC6 = new float[] { 35, 35, 15, 15 };
            childTable6.SetWidths(widthsC6);

            cell = new PdfPCell(new Phrase("REVIEW", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("START DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("END DATE", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("i.Charges, Payments,Adjustments and Write-offs", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("12 Month Comparison", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("11/01/2019", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("10/31/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("11/01/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("10/31/2021", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("ii.RPT and Collection Rates", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("6 Month Comparison", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("05/01/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("10/31/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("05/01/2021", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(new Phrase("10/31/2021", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable6.AddCell(cell);

            cell = new PdfPCell(childTable6);
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("POSITIVE / NEGATIVE COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Over the last 6 months they are up to 323 RPT on 73 transports, and 24,500 for overall revenue. ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 3;
            cell.MinimumHeight = 200f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("   ", fontContent));
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("CONTENT TO DISCUSS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("COMMENTS", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("ACTION TAKEN", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("1.Aging Review", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("2.Billing Rate Review", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Current Billing Rates", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable7 = new PdfPTable(5);
            float[] widthsC7 = new float[] { 10, 10, 11, 6, 7 };
            childTable7.SetWidths(widthsC7);

            cell = new PdfPCell(new Phrase("BLS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 500.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("BLS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 600.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS NE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("ALS2:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 700.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Non-Transport:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fontContent));
            //cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            //cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("Mileage:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("$ 12.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable7.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable7.AddCell(cell);

            cell = new PdfPCell(childTable7);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("b.   CUR (Customary andusual rates for thearea)", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Last Rate Change", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Started 08/01/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("3.Contract Status", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Enforce 08/01/2020", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Contract Current", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable8 = new PdfPTable(2);
            float[] widthsC8 = new float[] { 14, 14 };
            childTable8.SetWidths(widthsC8);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable8.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable8.AddCell(cell);


            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable8.AddCell(cell);


            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable8.AddCell(cell);

            cell = new PdfPCell(childTable8);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Enforce", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable9 = new PdfPTable(2);
            float[] widthsC9 = new float[] { 22, 22 };
            childTable9.SetWidths(widthsC9);

            cell = new PdfPCell(new Phrase("Renewal Date:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase("08/01/2024", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase("Current Rate:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(new Phrase("8.00", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable9.AddCell(cell);

            cell = new PdfPCell(childTable9);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("4.Personnel Changes:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable10 = new PdfPTable(2);
            float[] widthsC10 = new float[] { 15, 29 };
            childTable10.SetWidths(widthsC10);

            cell = new PdfPCell(new Phrase("Chief:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("Fiscal Officer:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("Authorized Official:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable10.AddCell(cell);

            cell = new PdfPCell(childTable10);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            Paragraph p1 = new Paragraph();
            Phrase ph1 = new Phrase("5.Demographic Changes" + "\n" + "\n", fontContentGreen);
            Phrase ph2 = new Phrase("\t" + "i.   Major Business Closed" + "\n" + "\n", fontContent);
            Phrase ph3 = new Phrase("\t" + "ii.  Nursing Home   Transports" + "\n" + "\n", fontContent);

            p1.Add(ph1);
            p1.Add(ph2);
            p1.Add(ph3);


            cell = new PdfPCell(p1);
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("6.New Business", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            //cell.BorderColor=
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("a.   Client Portal", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Usage", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable11 = new PdfPTable(2);
            float[] widthsC11 = new float[] { 14, 14 };
            childTable11.SetWidths(widthsC11);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable11.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable11.AddCell(cell);


            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable11.AddCell(cell);


            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable11.AddCell(cell);

            cell = new PdfPCell(childTable11);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("b.   Receiving alertson the home pageor client portal", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Alerts Received", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable12 = new PdfPTable(2);
            float[] widthsC12 = new float[] { 14, 14 };
            childTable12.SetWidths(widthsC12);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable12.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable12.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable12.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable12.AddCell(cell);

            cell = new PdfPCell(childTable12);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("c.   Medicare Ground Ambulance Data Collection System", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discussed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable13 = new PdfPTable(2);
            float[] widthsC13 = new float[] { 14, 14 };
            childTable13.SetWidths(widthsC13);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable13.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable13.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable13.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable13.AddCell(cell);

            cell = new PdfPCell(childTable13);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("d.   Client Patient Survey Program", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discussed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGGreenColor;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable14 = new PdfPTable(2);
            float[] widthsC14 = new float[] { 14, 14 };
            childTable14.SetWidths(widthsC14);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable14.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable14.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable14.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable14.AddCell(cell);

            cell = new PdfPCell(childTable14);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);


            PdfPTable childTable15 = new PdfPTable(8);
            float[] widthsC15 = new float[] { 28, 24, 5, 5, 5, 5, 14, 14 };
            childTable15.SetWidths(widthsC15);

            cell = new PdfPCell(new Phrase("e.   Signature Capture", fontContent));
            cell.PaddingBottom = 5f;
            cell.PaddingLeft = 10f;
            cell.Colspan = 1;
            cell.Rowspan = 6;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Patient Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Receiving Facility Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Crew Signature:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("YES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("NO", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("EPCR", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("Hard Copy", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable15.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable15.AddCell(cell);

            cell = new PdfPCell(childTable15);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("7. Month End Report Reconciliation Tutorial (report to run)", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable16 = new PdfPTable(2);
            float[] widthsC16 = new float[] { 14, 14 };
            childTable16.SetWidths(widthsC16);

            cell = new PdfPCell(new Phrase("Training Pending", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable16.AddCell(cell);

            cell = new PdfPCell(new Phrase("Training Completed", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable16.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable16.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable16.AddCell(cell);

            cell = new PdfPCell(childTable16);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable17 = new PdfPTable(5);
            float[] widthsC17 = new float[] { 28, 20, 12, 20, 20 };
            childTable17.SetWidths(widthsC17);

            cell = new PdfPCell(new Phrase("8.Client Review Intervals", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 3;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Quarterly", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Next Review Schedule Date", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("02/11/2022", fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Semi-Annual", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontContent));
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Change in ZOHO", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Yearly", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable17.AddCell(cell);

            cell = new PdfPCell(Image.GetInstance(Server.MapPath(@"\Images\tick.png")), false);
            //cell.PaddingTop = 3f;
            cell.Colspan = 1;
            //cell.Border = PdfPCell.RECTANGLE;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            childTable17.AddCell(cell);

            cell = new PdfPCell(childTable17);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable18 = new PdfPTable(5);
            float[] widthsC18 = new float[] { 28, 20, 20, 12, 20 };
            childTable18.SetWidths(widthsC18);

            cell = new PdfPCell(new Phrase("9.ePCR:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Rowspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("Name:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("Reconciliation of runs last performed", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("DATE:", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("11/05/2021", fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("ESO", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("By Whom", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(new Phrase("Chief", fontContent));
            cell.PaddingBottom = 5f;
            cell.Colspan = 2;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable18.AddCell(cell);

            cell = new PdfPCell(childTable18);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("  ", fontHeader));
            cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable19 = new PdfPTable(1);
            float[] widthsC19 = new float[] { 100 };
            childTable19.SetWidths(widthsC19);

            cell = new PdfPCell(new Phrase("OVERALL MEETING NOTES", fontHeader));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.BackgroundColor = customBGAshColor;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.MinimumHeight = 200f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable19.AddCell(cell);

            cell = new PdfPCell(childTable19);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            PdfPTable childTable20 = new PdfPTable(1);
            float[] widthsC20 = new float[] { 100 };
            childTable20.SetWidths(widthsC20);

            cell = new PdfPCell(new Phrase("Follow Up Action:", fontContentGreen));
            cell.PaddingBottom = 5f;
            cell.Colspan = 1;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable20.AddCell(cell);

            cell = new PdfPCell(new Phrase("Overall meeting had to take place over the phone as Chief got called out.  I talked to him and went over revenue numbers which he was pleased with.  No changes to rates, review intervals, muni demos, personnel, and contract.  No unusual follow up.  Nothing outstanding to report.", fontContent));
            cell.PaddingTop = 3f;
            cell.Colspan = 1;
            cell.MinimumHeight = 200f;
            cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            childTable20.AddCell(cell);

            cell = new PdfPCell(childTable20);
            //cell.PaddingTop = 10f;
            //cell.PaddingBottom = 10f;
            cell.Colspan = 3;
            cell.Border = 0;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);

            document.Add(table);


            document.Close();
            // Close the writer instance  
            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();

            byte[] bytes = File.ReadAllBytes(designationFileName);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase("Page - " + i.ToString(), fontContent), 568f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(designationFileName, bytes);





            System.Diagnostics.Process.Start(designationFileName);
        }

        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                //tabFot.SpacingAfter = 10F;
                //PdfPCell cell;
                //tabFot.TotalWidth = 300F;
                //cell = new PdfPCell(new Phrase("Header"));
                //tabFot.AddCell(cell);
                //tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                //base.OnEndPage(writer, document);
                //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
                //PdfPCell cell;
                //tabFot.TotalWidth = 300F;
                //cell = new PdfPCell(new Phrase("Footer"));
                //tabFot.AddCell(cell);
                //tabFot.WriteSelectedRows(0, -1, 150, document.Bottom, writer.DirectContent);

                base.OnEndPage(writer, document);

                PdfContentByte content;
                Rectangle rectangle;

                //Add border to page
                content = writer.DirectContent;
                rectangle = new Rectangle(document.PageSize);
                rectangle.Left += document.LeftMargin;
                rectangle.Right -= document.RightMargin;
                rectangle.Top -= document.TopMargin - 10f;
                rectangle.Bottom += document.BottomMargin;
                content.SetLineWidth(2);
                content.SetColorStroke(new BaseColor(0, 150, 143));
                content.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
                content.Stroke();





            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

    }
}