using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Web;

namespace ClientMeetingAgenda
{
    public partial class frmReadTxtFromPDF : System.Web.UI.Page
    {

        string FileNameWithoutExtension = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GetTextFromPDF();
        }

        private void GetTextFromPDF()
        {


            StringBuilder text = new StringBuilder();
            string FilePath = @"D:\Vanitha\TextToPDF\";
            string OutputPath = @"D:\Vanitha\TextToPDF_Output\Output\";
            
            foreach (var Folder in System.IO.Directory.GetDirectories(FilePath))
            {
                DirectoryInfo d = new DirectoryInfo(Folder); //Assuming Test is your Folder

                string FolderName = d.Name;

                FileInfo[] Files = d.GetFiles("*.pdf");

                DataTable dt = new DataTable();
                dt.Columns.Add("File Name", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("DOB", typeof(string));
                dt.Columns.Add("Incident #", typeof(string));
                dt.Columns.Add("Call #", typeof(string));
                //dt.Columns.Add("Crew Member1", typeof(string));
                //dt.Columns.Add("Crew Member2", typeof(string));
                //dt.Columns.Add("Crew Member3", typeof(string));

                dt.Columns.Add("Type of Person Signing", typeof(string));
                dt.Columns.Add("Signature Reason", typeof(string));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Printed Name", typeof(string));
                dt.Columns.Add("Signature Date", typeof(string));

                dt.Columns.Add("Type of Person Signing1", typeof(string));
                dt.Columns.Add("Signature Reason1", typeof(string));
                dt.Columns.Add("Status1", typeof(string));
                dt.Columns.Add("Printed Name1", typeof(string));
                dt.Columns.Add("Signature Date1", typeof(string));

                dt.Columns.Add("Type of Person Signing2", typeof(string));
                dt.Columns.Add("Signature Reason2", typeof(string));
                dt.Columns.Add("Status2", typeof(string));
                dt.Columns.Add("Printed Name2", typeof(string));
                dt.Columns.Add("Signature Date2", typeof(string));

                dt.Columns.Add("Type of Person Signing3", typeof(string));
                dt.Columns.Add("Signature Reason3", typeof(string));
                dt.Columns.Add("Status3", typeof(string));
                dt.Columns.Add("Printed Name3", typeof(string));
                dt.Columns.Add("Signature Date3", typeof(string));

                try
                {

                    for (int iPdf = 0; iPdf < Files.Length; iPdf++)
                    {
                        
                        var pages = new List<PdfPage>();
                        string Name = "";
                        string DOB = "";
                        string IncidentNo = "";
                        string CallNo = "";
                        //string CrewMember1 = "";
                        //string CrewMember2 = "";
                        //string CrewMember3 = "";

                        string TypeOfPersonSigning = "";
                        string SignatureReason = "";
                        string Status = "";
                        string PrintedName = "";
                        string SignatureDate = "";


                        string TypeOfPersonSigning1 = "";
                        string SignatureReason1 = "";
                        string Status1 = "";
                        string PrintedName1 = "";
                        string SignatureDate1 = "";

                        string TypeOfPersonSigning2 = "";
                        string SignatureReason2 = "";
                        string Status2 = "";
                        string PrintedName2 = "";
                        string SignatureDate2 = "";


                        string TypeOfPersonSigning3 = "";
                        string SignatureReason3 = "";
                        string Status3 = "";
                        string PrintedName3 = "";
                        string SignatureDate3 = "";


                        string TypeOfPersonSigning4 = "";
                        string SignatureReason4 = "";
                        string Status4 = "";
                        string PrintedName4 = "";
                        string SignatureDate4 = "";

                        //bool isFirstName = true;
                        int noOfTypeOfPersonSigning = 1;
                        int noOfSignatureReason = 1;
                        int noOfStatus = 1;
                        int noOfPrintedName = 1;
                        int noOfSignatureDate = 1;

                        FileNameWithoutExtension = "";

                        FileNameWithoutExtension = Files[iPdf].Name.Replace(".pdf", "");



                        using (PdfReader reader = new PdfReader(d + "\\" + Files[iPdf]))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                //text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                                pages.Add(new PdfPage()
                                {
                                    content = PdfTextExtractor.GetTextFromPage(reader, i)
                                });

                            }
                        }

                        //use linq to create the rows and words by splitting on newline and space
                        pages.ForEach(x => x.rows = x.content.Split('\n').Select(y =>
                            new PdfRow()
                            {
                                content = y,
                                words = y.Split(' ').ToList()
                            }
                        ).ToList());

                        for (int iPage = 0; iPage < pages.Count; iPage++)
                        {
                            for (int iRow = 0; iRow < pages[iPage].rows.Count; iRow++)
                            {
                                if (pages[iPage].rows[iRow].words.Count >= 4)
                                {
                                    if (pages[iPage].rows[iRow].words[0].ToString() == "Name:" && pages[iPage].rows[iRow].words[3].ToString() == "Age:")
                                    {
                                        Name = pages[iPage].rows[iRow].words[1].ToString() + " " + pages[iPage].rows[iRow].words[2].ToString();
                                    }
                                    if (pages[iPage].rows[iRow].words[0].ToString() == "Name:" && pages[iPage].rows[iRow].words[4].ToString() == "Age:")
                                    {
                                        Name = pages[iPage].rows[iRow].words[1].ToString() + " " + pages[iPage].rows[iRow].words[2].ToString() + " " + pages[iPage].rows[iRow].words[3].ToString();
                                    }
                                }
                                if (pages[iPage].rows[iRow].words.Count > 7)
                                {
                                    if (pages[iPage].rows[iRow].words[6].ToString() == "D.O.B.:")
                                    {
                                        DOB = pages[iPage].rows[iRow].words[7].ToString();
                                    }
                                    if (pages[iPage].rows[iRow].words[7].ToString() == "D.O.B.:")
                                    {
                                        DOB = pages[iPage].rows[iRow].words[8].ToString();
                                    }
                                }

                                if (pages[iPage].rows[iRow].words[0].ToString() == "Incident" && pages[iPage].rows[iRow].words[1].ToString() == "#:"
                                        && pages[iPage].rows[iRow].words[3].ToString() == "Call" && pages[iPage].rows[iRow].words[4].ToString() == "#:")
                                {
                                    IncidentNo = pages[iPage].rows[iRow].words[2].ToString();
                                    CallNo = pages[iPage].rows[iRow].words[5].ToString();
                                }
                                //try
                                //{
                                //    if (pages[iPage].rows[iRow].words[0].ToString() == "Crew" && pages[iPage].rows[iRow].words[1].ToString() == "Member")
                                //    {
                                //        try
                                //        {
                                //            CrewMember1 = pages[iPage].rows[iRow + 1].words[0].ToString() + " " + pages[iPage].rows[iRow + 1].words[1].ToString();
                                //            if (pages[iPage].rows[iRow + 2].words[0].ToString() != "Billing")
                                //            {
                                //                CrewMember2 = pages[iPage].rows[iRow + 2].words[0].ToString() + " " + pages[iPage].rows[iRow + 2].words[1].ToString();

                                //                if (pages[iPage].rows[iRow + 3].words[0].ToString() != "Billing")
                                //                {
                                //                    CrewMember3 = pages[iPage].rows[iRow + 3].words[0].ToString() + " " + pages[iPage].rows[iRow + 3].words[1].ToString();
                                //                }
                                //            }
                                //        }
                                //        catch
                                //        {

                                //            CrewMember1 = pages[iPage + 1].rows[0].words[0].ToString() + " " + pages[iPage + 1].rows[0].words[1].ToString();
                                //            if (pages[iPage+1].rows[1].words[0].ToString() != "Billing")
                                //            {
                                //                CrewMember2 = pages[iPage + 1].rows[1].words[0].ToString() + " " + pages[iPage + 1].rows[1].words[1].ToString();
                                //            }
                                //        }
                                //    }
                                //}
                                //catch (Exception ex)
                                //{
                                //    if (pages[iPage].rows[iRow + 1].words[0].ToString() == "Member")
                                //    {
                                //        try
                                //        {
                                //            CrewMember1 = pages[iPage].rows[iRow + 2].words[0].ToString() + " " + pages[iPage].rows[iRow + 3].words[0].ToString();
                                //            CrewMember2 = pages[iPage].rows[iRow + 4].words[0].ToString() + " " + pages[iPage].rows[iRow + 4].words[1].ToString();

                                //            if (pages[iPage].rows[iRow + 6].words[0].ToString() != "Billing")
                                //            {
                                //                CrewMember3 = pages[iPage].rows[iRow + 6].words[0].ToString() + " " + pages[iPage].rows[iRow + 7].words[0].ToString();
                                //            }
                                //        }
                                //        catch (Exception ex1)
                                //        {

                                //        }
                                //    }
                                //}
                                if (pages[iPage].rows[iRow].words[0].ToString() == "Type" && pages[iPage].rows[iRow].words[1].ToString() == "of"
                                        && pages[iPage].rows[iRow].words[2].ToString() == "Person" && pages[iPage].rows[iRow].words[3].ToString() == "Signing:")
                                {

                                    TypeOfPersonSigning = "";
                                    for (int iWord = 4; iWord < pages[iPage].rows[iRow].words.Count; iWord++)
                                    {
                                        TypeOfPersonSigning = TypeOfPersonSigning + " " + pages[iPage].rows[iRow].words[iWord].ToString();
                                    }
                                    if (noOfTypeOfPersonSigning==1)
                                    {
                                        TypeOfPersonSigning1 = TypeOfPersonSigning;
                                        noOfTypeOfPersonSigning = 2;
                                    }
                                    else if (noOfTypeOfPersonSigning == 2)
                                    {
                                        TypeOfPersonSigning2 = TypeOfPersonSigning;
                                        noOfTypeOfPersonSigning = 3;
                                    }
                                    else if (noOfTypeOfPersonSigning == 3)
                                    {
                                        TypeOfPersonSigning3 = TypeOfPersonSigning;
                                        noOfTypeOfPersonSigning = 4;
                                    }
                                    else if (noOfTypeOfPersonSigning == 4)
                                    {
                                        TypeOfPersonSigning4 = TypeOfPersonSigning;
                                        noOfTypeOfPersonSigning = 5;
                                    }
                                }

                                if (pages[iPage].rows[iRow].words[0].ToString() == "Signature" && pages[iPage].rows[iRow].words[1].ToString() == "Reason:")
                                {

                                    SignatureReason = "";
                                    if (pages[iPage].rows[iRow].words.Count == 2)
                                    {
                                        for (int iWord = 0; iWord < pages[iPage].rows[iRow + 1].words.Count; iWord++)
                                        {
                                            SignatureReason = SignatureReason + " " + pages[iPage].rows[iRow + 1].words[iWord].ToString();
                                        }
                                    }
                                    else
                                    {
                                        for (int iWord = 2; iWord < pages[iPage].rows[iRow].words.Count; iWord++)
                                        {
                                            SignatureReason = SignatureReason + " " + pages[iPage].rows[iRow].words[iWord].ToString();
                                        }
                                    }
                                    if (noOfSignatureReason==1)
                                    {
                                        SignatureReason1 = SignatureReason;
                                        noOfSignatureReason = 2;
                                    }
                                    else if (noOfSignatureReason == 2)
                                    {
                                        SignatureReason2 = SignatureReason;
                                        noOfSignatureReason = 3;
                                    }
                                    else if (noOfSignatureReason == 3)
                                    {
                                        SignatureReason3 = SignatureReason;
                                        noOfSignatureReason = 4;
                                    }
                                    else if (noOfSignatureReason == 4)
                                    {
                                        SignatureReason4 = SignatureReason;
                                        noOfSignatureReason = 5;
                                    }
                                }
                                if (pages[iPage].rows[iRow].words[0].ToString() == "Status:")
                                {
                                    Status = "";
                                    for (int iWord = 1; iWord < pages[iPage].rows[iRow].words.Count; iWord++)
                                    {
                                        Status = Status + " " + pages[iPage].rows[iRow].words[iWord].ToString();
                                    }
                                    if (noOfStatus==1)
                                    {
                                        Status1 = Status;
                                        noOfStatus = 2;
                                    }
                                    else if (noOfStatus == 2)
                                    {
                                        Status2 = Status;
                                        noOfStatus = 3;
                                    }
                                    else if (noOfStatus == 3)
                                    {
                                        Status3 = Status;
                                        noOfStatus = 4;
                                    }
                                    else if (noOfStatus == 4)
                                    {
                                        Status4 = Status;
                                        noOfStatus = 5;
                                    }
                                }
                                if (pages[iPage].rows[iRow].words[0].ToString() == "Printed" && pages[iPage].rows[iRow].words[1].ToString() == "Name:")
                                {
                                    PrintedName = "";
                                    for (int iWord = 2; iWord < pages[iPage].rows[iRow].words.Count; iWord++)
                                    {
                                        PrintedName = PrintedName + " " + pages[iPage].rows[iRow].words[iWord].ToString();
                                    }

                                    if (noOfPrintedName==1)
                                    {
                                        PrintedName1 = PrintedName;
                                        noOfPrintedName = 2;
                                    }
                                    else if (noOfPrintedName == 2)
                                    {
                                        PrintedName2 = PrintedName;
                                        noOfPrintedName = 3;
                                    }
                                    else if (noOfPrintedName == 3)
                                    {
                                        PrintedName3 = PrintedName;
                                        noOfPrintedName = 4;
                                    }
                                    else if (noOfPrintedName == 4)
                                    {
                                        PrintedName4 = PrintedName;
                                        noOfPrintedName = 5;
                                    }
                                }
                                if (pages[iPage].rows[iRow].words[0].ToString() == "Signature" && pages[iPage].rows[iRow].words[1].ToString() == "Date:")
                                {
                                    SignatureDate = "";
                                    for (int iWord = 2; iWord < pages[iPage].rows[iRow].words.Count; iWord++)
                                    {
                                        SignatureDate = SignatureDate + " " + pages[iPage].rows[iRow].words[iWord].ToString();
                                    }

                                    if (noOfSignatureDate==1)
                                    {
                                        SignatureDate1 = SignatureDate;
                                        noOfSignatureDate = 2;
                                    }
                                    else if (noOfSignatureDate == 2)
                                    {
                                        SignatureDate2 = SignatureDate;
                                        noOfSignatureDate = 3;
                                    }
                                    else if (noOfSignatureDate == 3)
                                    {
                                        SignatureDate3 = SignatureDate;
                                        noOfSignatureDate = 4;
                                    }
                                    else if (noOfSignatureDate == 4)
                                    {
                                        SignatureDate4 = SignatureDate;
                                        noOfSignatureDate = 5;
                                    }
                                }
                            }
                        }


                        DataRow dr = dt.NewRow();
                        dr[0] = FileNameWithoutExtension;
                        dr[1] = Name;
                        dr[2] = DOB;
                        dr[3] = IncidentNo;
                        dr[4] = CallNo;
                        //dr[5] = CrewMember1;
                        //dr[6] = CrewMember2;
                        //dr[7] = CrewMember3;

                        dr[5] = TypeOfPersonSigning1;
                        dr[6] = SignatureReason1;
                        dr[7] = Status1;
                        dr[8] = PrintedName1;
                        dr[9] = SignatureDate1;

                        dr[10] = TypeOfPersonSigning2;
                        dr[11] = SignatureReason2;
                        dr[12] = Status2;
                        dr[13] = PrintedName2;
                        dr[14] = SignatureDate2;

                        dr[15] = TypeOfPersonSigning3;
                        dr[16] = SignatureReason3;
                        dr[17] = Status3;
                        dr[18] = PrintedName3;
                        dr[19] = SignatureDate3;

                        dr[20] = TypeOfPersonSigning4;
                        dr[21] = SignatureReason4;
                        dr[22] = Status4;
                        dr[23] = PrintedName4;
                        dr[24] = SignatureDate4;

                        dt.Rows.Add(dr);




                    }


                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Sheet1");

                        wb.SaveAs(OutputPath + FolderName + ".xlsx");

                        ////wb.Worksheet(5).Row(1).Delete();
                        ////wb.Worksheet(5).FirstRow().Delete();
                        //HttpContext.Current.Response.Clear();
                        //HttpContext.Current.Response.Buffer = true;
                        //Response.Charset = "";
                        //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        //Response.AddHeader("content-disposition", "attachment;filename= GaryIn_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xlsx");
                        //using (MemoryStream MyMemoryStream = new MemoryStream())
                        //{
                        //    wb.SaveAs(MyMemoryStream);
                        //    MyMemoryStream.WriteTo(Response.OutputStream);
                        //    HttpContext.Current.Response.Flush();
                        //    //HttpContext.Current.Response.End();
                        //    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                        //    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                        //}
                    }


                    //find the rows in all pages containing a word
                    //var myRows = pages.SelectMany(r => r.rows).Where(x => x.words.Any(y => y == "Name")).ToList();
                }
                catch (Exception ex)
                {
                }
            }


        }

        class PdfPage
        {
            public string content { get; set; }
            public List<PdfRow> rows { get; set; }
        }


        class PdfRow
        {
            public string content { get; set; }
            public List<string> words { get; set; }
        }
    }
}