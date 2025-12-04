using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Layouts;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.Record.RecordOperations;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using Info = Com.Zoho.Crm.API.Record.Info;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;

namespace ClientMeetingAgenda.App_Code
{
    public class clsRecord
    {
        /// 

        /// This method is used to get all the records of a module and print the response.
        /// 

        /// The API Name of the module to obtain records.
        public static void GetRecords(string moduleAPIName)
        {
            //example
            //string moduleAPIName = "Leads";

            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();

            ParameterMap paramInstance = new ParameterMap();

            //paramInstance.Add(GetRecordsParam.APPROVED, "both");

            //paramInstance.Add(GetRecordsParam.CONVERTED, "both");

            //paramInstance.Add(GetRecordsParam.CVID, "34770610087501");

            List<string> ids = new List<string>() { "1234543", "12345432" };

            foreach (string id in ids)
            {
                paramInstance.Add(GetRecordsParam.IDS, id);
            }

            //paramInstance.Add(GetRecordsParam.UID, "34770615181008");

            //List<string> fieldNames = new List<string>() { "Company", "Email" };

            //foreach (string fieldName in fieldNames)
            //{
            //    paramInstance.Add(GetRecordsParam.FIELDS, fieldName);
            //}

            //paramInstance.Add(GetRecordsParam.SORT_BY, "Email");

            //paramInstance.Add(GetRecordsParam.SORT_ORDER, "desc");

            //paramInstance.Add(GetRecordsParam.PAGE, 1);

            //paramInstance.Add(GetRecordsParam.PER_PAGE, 1);

            //DateTimeOffset startdatetime = new DateTimeOffset(new DateTime(2019, 11, 20, 10, 0, 01, DateTimeKind.Local));

            //paramInstance.Add(GetRecordsParam.STARTDATETIME, startdatetime);

            //DateTimeOffset enddatetime = new DateTimeOffset(new DateTime(2019, 12, 20, 10, 0, 01, DateTimeKind.Local));

            //paramInstance.Add(GetRecordsParam.ENDDATETIME, enddatetime);

            //paramInstance.Add(GetRecordsParam.TERRITORY_ID, "34770613051357");

            //paramInstance.Add(GetRecordsParam.INCLUDE_CHILD, "true");

            HeaderMap headerInstance = new HeaderMap();

            //DateTimeOffset ifmodifiedsince = new DateTimeOffset(new DateTime(2019, 05, 15, 12, 0, 0, DateTimeKind.Local));

            //headerInstance.Add(GetRecordsHeader.IF_MODIFIED_SINCE, ifmodifiedsince);

            //headerInstance.Add(GetRecordsHeader.X_EXTERNAL, "Leads.External");

            //Call GetRecords method that takes moduleAPIName, paramInstance and headerInstance  as parameter.
            APIResponse<ResponseHandler> response = recordOperations.GetRecords(moduleAPIName, paramInstance, headerInstance);

            if (response != null)
            {
                //Get the status code from response
                Console.WriteLine("Status Code: " + response.StatusCode);

                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }

                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    ResponseHandler responseHandler = response.Object;

                    if (responseHandler is ResponseWrapper)
                    {
                        //Get the received ResponseWrapper instance
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                        //Get the obtained Record instances
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            Console.WriteLine("Record ID: " + record.Id);

                            //Get the createdBy User instance of each Record
                            User createdBy = record.CreatedBy;

                            //Check if createdBy is not null
                            if (createdBy != null)
                            {
                                //Get the ID of the createdBy User
                                Console.WriteLine("Record Created By User-ID: " + createdBy.Id);

                                //Get the name of the createdBy User
                                Console.WriteLine("Record Created By User-Name: " + createdBy.Name);

                                //Get the Email of the createdBy User
                                Console.WriteLine("Record Created By User-Email: " + createdBy.Email);
                            }

                            //Get the CreatedTime of each Record
                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);

                            //Get the modifiedBy User instance of each Record
                            User modifiedBy = record.ModifiedBy;

                            //Check if modifiedBy is not null
                            if (modifiedBy != null)
                            {
                                //Get the ID of the modifiedBy User
                                Console.WriteLine("Record Modified By User-ID: " + modifiedBy.Id);

                                //Get the name of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Name: " + modifiedBy.Name);

                                //Get the Email of the modifiedBy User
                                Console.WriteLine("Record Modified By User-Email: " + modifiedBy.Email);
                            }

                            //Get the ModifiedTime of each Record
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            //Get the list of Tag instance each Record
                            List<Tag> tags = record.Tag;

                            //Check if tags is not null
                            if (tags != null)
                            {
                                foreach (Tag tag in tags)
                                {
                                    //Get the Name of each Tag
                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                    //Get the Id of each Tag
                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                }
                            }

                            //To Get particular field value 
                            Console.WriteLine("Record Field Value: " + record.GetKeyValue("Last_Name"));// FieldApiName

                            Console.WriteLine("Record KeyValues: ");

                            //Get the KeyValue map
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                string keyName = entry.Key;

                                Object value = entry.Value;

                                if (value != null)
                                {
                                    if (value is IList && ((IList)value).Count > 0)
                                    {
                                        IList dataList = (IList)value;

                                        if (dataList.Count > 0)
                                        {
                                            if (dataList[0] is FileDetails)
                                            {
                                                List<FileDetails> fileDetails = (List<FileDetails>)value;

                                                foreach (FileDetails fileDetail in fileDetails)
                                                {
                                                    //Get the Extn of each FileDetails
                                                    Console.WriteLine("Record FileDetails Extn: " + fileDetail.Extn);

                                                    //Get the IsPreviewAvailable of each FileDetails
                                                    Console.WriteLine("Record FileDetails IsPreviewAvailable: " + fileDetail.IsPreviewAvailable);

                                                    //Get the DownloadUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DownloadUrl: " + fileDetail.DownloadUrl);

                                                    //Get the DeleteUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails DeleteUrl: " + fileDetail.DeleteUrl);

                                                    //Get the EntityId of each FileDetails
                                                    Console.WriteLine("Record FileDetails EntityId: " + fileDetail.EntityId);

                                                    //Get the Mode of each FileDetails
                                                    Console.WriteLine("Record FileDetails Mode: " + fileDetail.Mode);

                                                    //Get the OriginalSizeByte of each FileDetails
                                                    Console.WriteLine("Record FileDetails OriginalSizeByte: " + fileDetail.OriginalSizeByte);

                                                    //Get the PreviewUrl of each FileDetails
                                                    Console.WriteLine("Record FileDetails PreviewUrl: " + fileDetail.PreviewUrl);

                                                    //Get the FileName of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileName: " + fileDetail.FileName);

                                                    //Get the FileId of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileId: " + fileDetail.FileId);

                                                    //Get the AttachmentId of each FileDetails
                                                    Console.WriteLine("Record FileDetails AttachmentId: " + fileDetail.AttachmentId);

                                                    //Get the FileSize of each FileDetails
                                                    Console.WriteLine("Record FileDetails FileSize: " + fileDetail.FileSize);

                                                    //Get the CreatorId of each FileDetails
                                                    Console.WriteLine("Record FileDetails CreatorId: " + fileDetail.CreatorId);

                                                    //Get the LinkDocs of each FileDetails
                                                    Console.WriteLine("Record FileDetails LinkDocs: " + fileDetail.LinkDocs);
                                                }
                                            }
                                            else if (dataList[0] is InventoryLineItems)
                                            {
                                                List<InventoryLineItems> productDetails = (List<InventoryLineItems>)value;

                                                foreach (InventoryLineItems productDetail in productDetails)
                                                {
                                                    LineItemProduct lineItemProduct = productDetail.Product;

                                                    if (lineItemProduct != null)
                                                    {
                                                        Console.WriteLine("Record ProductDetails LineItemProduct ProductCode: " + lineItemProduct.ProductCode);

                                                        Console.WriteLine("Record ProductDetails LineItemProduct Currency: " + lineItemProduct.Currency);

                                                        Console.WriteLine("Record ProductDetails LineItemProduct Name: " + lineItemProduct.Name);

                                                        Console.WriteLine("Record ProductDetails LineItemProduct Id: " + lineItemProduct.Id);
                                                    }

                                                    Console.WriteLine("Record ProductDetails Quantity: " + productDetail.Quantity);

                                                    Console.WriteLine("Record ProductDetails Discount: " + productDetail.Discount);

                                                    Console.WriteLine("Record ProductDetails TotalAfterDiscount: " + productDetail.TotalAfterDiscount);

                                                    Console.WriteLine("Record ProductDetails NetTotal: " + productDetail.NetTotal);

                                                    if (productDetail.Book != null)
                                                    {
                                                        Console.WriteLine("Record ProductDetails Book: " + productDetail.Book);
                                                    }

                                                    Console.WriteLine("Record ProductDetails Tax: " + productDetail.Tax);

                                                    Console.WriteLine("Record ProductDetails ListPrice: " + productDetail.ListPrice);

                                                    Console.WriteLine("Record ProductDetails UnitPrice: " + productDetail.UnitPrice);

                                                    Console.WriteLine("Record ProductDetails QuantityInStock: " + productDetail.QuantityInStock);

                                                    Console.WriteLine("Record ProductDetails Total: " + productDetail.Total);

                                                    Console.WriteLine("Record ProductDetails ID: " + productDetail.Id);

                                                    Console.WriteLine("Record ProductDetails ProductDescription: " + productDetail.ProductDescription);

                                                    List<LineTax> lineTaxes = productDetail.LineTax;

                                                    foreach (LineTax lineTax in lineTaxes)
                                                    {
                                                        Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                        Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                        Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                        Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);
                                                    }
                                                }
                                            }
                                            else if (dataList[0] is Tag)
                                            {
                                                List<Tag> tagList = (List<Tag>)value;

                                                foreach (Tag tag in tagList)
                                                {
                                                    //Get the Name of each Tag
                                                    Console.WriteLine("Record Tag Name: " + tag.Name);

                                                    //Get the Id of each Tag
                                                    Console.WriteLine("Record Tag ID: " + tag.Id);
                                                }
                                            }
                                            else if (dataList[0] is PricingDetails)
                                            {
                                                List<PricingDetails> pricingDetails = (List<PricingDetails>)value;

                                                foreach (PricingDetails pricingDetail in pricingDetails)
                                                {
                                                    Console.WriteLine("Record PricingDetails ToRange: " + pricingDetail.ToRange);

                                                    Console.WriteLine("Record PricingDetails Discount: " + pricingDetail.Discount);

                                                    Console.WriteLine("Record PricingDetails ID: " + pricingDetail.Id);

                                                    Console.WriteLine("Record PricingDetails FromRange: " + pricingDetail.FromRange);
                                                }
                                            }
                                            else if (dataList[0] is Participants)
                                            {
                                                List<Participants> participants = (List<Participants>)value;

                                                foreach (Participants participant in participants)
                                                {
                                                    Console.WriteLine("Record Participants Name: " + participant.Name);

                                                    Console.WriteLine("Record Participants Invited: " + participant.Invited);

                                                    Console.WriteLine("Record Participants ID: " + participant.Id);

                                                    Console.WriteLine("Record Participants Type: " + participant.Type);

                                                    Console.WriteLine("Record Participants Participant: " + participant.Participant);

                                                    Console.WriteLine("Record Participants Status: " + participant.Status);
                                                }
                                            }
                                            else if (dataList[0] is LineTax)
                                            {
                                                List<LineTax> lineTaxes = (List<LineTax>)dataList;

                                                foreach (LineTax lineTax in lineTaxes)
                                                {
                                                    Console.WriteLine("Record ProductDetails LineTax Percentage: " + lineTax.Percentage);

                                                    Console.WriteLine("Record ProductDetails LineTax Name: " + lineTax.Name);

                                                    Console.WriteLine("Record ProductDetails LineTax Id: " + lineTax.Id);

                                                    Console.WriteLine("Record ProductDetails LineTax Value: " + lineTax.Value);
                                                }
                                            }
                                            else if (dataList[0].GetType().FullName.Contains("Choice"))
                                            {
                                                Console.WriteLine(keyName);

                                                Console.WriteLine("values");

                                                foreach (object choice in dataList)
                                                {
                                                    Type type = choice.GetType();

                                                    PropertyInfo prop = type.GetProperty("Value");

                                                    Console.WriteLine(prop.GetValue(choice));
                                                }
                                            }
                                            else if (dataList[0] is Comment)
                                            {
                                                List<Comment> comments = (List<Comment>)dataList;

                                                foreach (Comment comment in comments)
                                                {
                                                    Console.WriteLine("Record Comment CommentedBy: " + comment.CommentedBy);

                                                    Console.WriteLine("Record Comment CommentedTime: " + comment.CommentedTime.ToString());

                                                    Console.WriteLine("Record Comment CommentContent: " + comment.CommentContent);

                                                    Console.WriteLine("Record Comment Id: " + comment.Id);
                                                }
                                            }
                                            else if (dataList[0] is Attachment)
                                            {
                                                //Get the list of obtained Attachment instances
                                                List<Attachment> attachments = (List<Attachment>)dataList; ;

                                                foreach (Attachment attachment in attachments)
                                                {
                                                    //Get the owner User instance of each attachment
                                                    User owner = attachment.Owner;

                                                    //Check if owner is not null
                                                    if (owner != null)
                                                    {
                                                        //Get the Name of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Name: " + owner.Name);

                                                        //Get the ID of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-ID: " + owner.Id);

                                                        //Get the Email of the Owner
                                                        Console.WriteLine("Record Attachment Owner User-Email: " + owner.Email);
                                                    }

                                                    //Get the modified time of each attachment
                                                    Console.WriteLine("Record Attachment Modified Time: " + attachment.ModifiedTime.ToString());

                                                    //Get the name of the File
                                                    Console.WriteLine("Record Attachment File Name: " + attachment.FileName);

                                                    //Get the created time of each attachment
                                                    Console.WriteLine("Record Attachment Created Time: " + attachment.CreatedTime.ToString());

                                                    //Get the Attachment file size
                                                    Console.WriteLine("Record Attachment File Size: " + attachment.Size.ToString());

                                                    //Get the parentId Record instance of each attachment
                                                    Com.Zoho.Crm.API.Record.Record parentId = attachment.ParentId;

                                                    //Check if parentId is not null
                                                    if (parentId != null)
                                                    {
                                                        //Get the parent record Name of each attachment
                                                        Console.WriteLine("Record Attachment parent record Name: " + parentId.GetKeyValue("name"));

                                                        //Get the parent record ID of each attachment
                                                        Console.WriteLine("Record Attachment parent record ID: " + parentId.Id);
                                                    }

                                                    //Get the attachment is Editable
                                                    Console.WriteLine("Record Attachment is Editable: " + attachment.Editable.ToString());

                                                    //Get the file ID of each attachment
                                                    Console.WriteLine("Record Attachment File ID: " + attachment.FileId);

                                                    //Get the type of each attachment
                                                    Console.WriteLine("Record Attachment File Type: " + attachment.Type);

                                                    //Get the seModule of each attachment
                                                    Console.WriteLine("Record Attachment seModule: " + attachment.SeModule);

                                                    //Get the modifiedBy User instance of each attachment
                                                    modifiedBy = attachment.ModifiedBy;

                                                    //Check if modifiedBy is not null
                                                    if (modifiedBy != null)
                                                    {
                                                        //Get the Name of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Name: " + modifiedBy.Name);

                                                        //Get the ID of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-ID: " + modifiedBy.Id);

                                                        //Get the Email of the modifiedBy User
                                                        Console.WriteLine("Record Attachment Modified By User-Email: " + modifiedBy.Email);
                                                    }

                                                    //Get the state of each attachment
                                                    Console.WriteLine("Record Attachment State: " + attachment.State);

                                                    //Get the ID of each attachment
                                                    Console.WriteLine("Record Attachment ID: " + attachment.Id);

                                                    //Get the createdBy User instance of each attachment
                                                    createdBy = attachment.CreatedBy;

                                                    //Check if createdBy is not null
                                                    if (createdBy != null)
                                                    {
                                                        //Get the name of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Name: " + createdBy.Name);

                                                        //Get the ID of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-ID: " + createdBy.Id);

                                                        //Get the Email of the createdBy User
                                                        Console.WriteLine("Record Attachment Created By User-Email: " + createdBy.Email);
                                                    }

                                                    //Get the linkUrl of each attachment
                                                    Console.WriteLine("Record Attachment LinkUrl: " + attachment.LinkUrl);
                                                }
                                            }
                                            else if (dataList[0] is Com.Zoho.Crm.API.Record.Record)
                                            {
                                                List<Com.Zoho.Crm.API.Record.Record> recordList = (List<Com.Zoho.Crm.API.Record.Record>)dataList;

                                                foreach (Com.Zoho.Crm.API.Record.Record record1 in recordList)
                                                {
                                                    //Get the details map
                                                    foreach (KeyValuePair<string, object> entry1 in record1.GetKeyValues())
                                                    {
                                                        //Get each value in the map
                                                        Console.WriteLine(entry1.Key + ": " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                            }
                                        }
                                    }
                                    //else if (value is Layout)
                                    //{
                                    //    Layout layout = (Layout)value;

                                    //    if (layout != null)
                                    //    {
                                    //        Console.WriteLine("Record " + keyName + " ID: " + layout.Id);

                                    //        Console.WriteLine("Record " + keyName + " Name: " + layout.Name);
                                    //    }
                                    //}
                                    else if (value is User)
                                    {
                                        User user = (User)value;

                                        if (user != null)
                                        {
                                            Console.WriteLine("Record " + keyName + " User-ID: " + user.Id);

                                            Console.WriteLine("Record " + keyName + " User-Name: " + user.Name);

                                            Console.WriteLine("Record " + keyName + " User-Email: " + user.Email);
                                        }
                                    }
                                    else if (value is Consent)
                                    {
                                        Consent consent = (Consent)value;

                                        //Get the Owner User instance of each attachment
                                        User owner = consent.Owner;

                                        //Check if owner is not null
                                        if (owner != null)
                                        {
                                            //Get the name of the owner User
                                            Console.WriteLine("Record Consent Owner Name: " + owner.Name);

                                            //Get the ID of the owner User
                                            Console.WriteLine("Record Consent Owner ID: " + owner.Id);

                                            //Get the Email of the owner User
                                            Console.WriteLine("Record Consent Owner Email: " + owner.Email);
                                        }

                                        Console.WriteLine("Record Consent ContactThroughEmail: " + consent.ContactThroughEmail);

                                        Console.WriteLine("Record Consent ContactThroughSocial: " + consent.ContactThroughSocial);

                                        Console.WriteLine("Record Consent ContactThroughSurvey: " + consent.ContactThroughSurvey);

                                        Console.WriteLine("Record Consent ContactThroughPhone: " + consent.ContactThroughPhone);

                                        Console.WriteLine("Record Consent MailSentTime: " + consent.MailSentTime.ToString());

                                        Console.WriteLine("Record Consent ConsentDate: " + consent.ConsentDate.ToString());

                                        Console.WriteLine("Record Consent ConsentRemarks: " + consent.ConsentRemarks);

                                        Console.WriteLine("Record Consent ConsentThrough: " + consent.ConsentThrough);

                                        Console.WriteLine("Record Consent DataProcessingBasis: " + consent.DataProcessingBasis);
                                    }
                                    else if (value is Com.Zoho.Crm.API.Record.Record)
                                    {
                                        Com.Zoho.Crm.API.Record.Record recordValue = (Com.Zoho.Crm.API.Record.Record)value;

                                        Console.WriteLine("Record " + keyName + " ID: " + recordValue.Id);

                                        Console.WriteLine("Record " + keyName + " Name: " + recordValue.GetKeyValue("name"));
                                    }
                                    else if (value.GetType().FullName.Contains("Choice"))
                                    {
                                        Type type = value.GetType();

                                        PropertyInfo prop = type.GetProperty("Value");

                                        Console.WriteLine(keyName + ": " + prop.GetValue(value));
                                    }
                                    else if (value is RemindAt)
                                    {
                                        Console.WriteLine(keyName + ": " + ((RemindAt)value).Alarm);
                                    }
                                    else if (value is RecurringActivity)
                                    {
                                        Console.WriteLine(keyName);

                                        Console.WriteLine("RRULE" + ": " + ((RecurringActivity)value).Rrule);
                                    }
                                    else
                                    {
                                        //Get each value in the map
                                        Console.WriteLine(keyName + ": " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }

                        //Get the Object obtained Info instance
                        Info info = responseWrapper.Info;

                        //Check if info is not null
                        if (info != null)
                        {
                            if (info.PerPage != null)
                            {
                                //Get the PerPage of the Info
                                Console.WriteLine("Record Info PerPage: " + info.PerPage);
                            }

                            if (info.Count != null)
                            {
                                //Get the Count of the Info
                                Console.WriteLine("Record Info Count: " + info.Count);
                            }

                            if (info.Page != null)
                            {
                                //Get the Page of the Info
                                Console.WriteLine("Record Info Page: " + info.Page);
                            }

                            if (info.MoreRecords != null)
                            {
                                //Get the MoreRecords of the Info
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords);
                            }
                        }
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Status
                        Console.WriteLine("Status: " + exception.Status.Value);

                        //Get the Code
                        Console.WriteLine("Code: " + exception.Code.Value);

                        Console.WriteLine("Details: ");

                        //Get the details map
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            //Get each value in the map
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }

                        //Get the Message
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                { //If response is not as expected

                    //Get model object from response
                    Model responseObject = response.Model;

                    //Get the response object's class
                    Type type = responseObject.GetType();

                    //Get all declared fields of the response class
                    Console.WriteLine("Type is: {0}", type.Name);

                    PropertyInfo[] props = type.GetProperties();

                    Console.WriteLine("Properties (N = {0}):", props.Length);

                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }
    }
}
