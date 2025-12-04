using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Attachments;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.Attachments.AttachmentsOperations;
using Info = Com.Zoho.Crm.API.Record.Info;

namespace ClientMeetingAgenda.App_Code
{
    public class clsAttachment
    {
        /// 
        /// This method is used to upload an attachment to a single record of a module with ID and print the response.
        /// 
        /// The API Name of the record's module
        /// The ID of the record to upload attachment
        /// The absolute file path of the file to be attached
        public string UploadAttachments(string moduleAPIName, long recordId, string absoluteFilePath)
        {
            //example
            //string moduleAPIName = "Leads";
            //long recordId = 34770615177002;
            //string absoluteFilePath = "/Users/use_name/Desktop/image.png";

            //Get instance of AttachmentsOperations Class that takes moduleAPIName and recordId as parameter
            AttachmentsOperations attachmentsOperations = new AttachmentsOperations(moduleAPIName, recordId);

            //Get instance of FileBodyWrapper class that will contain the request file
            Com.Zoho.Crm.API.Attachments.FileBodyWrapper fileBodyWrapper = new Com.Zoho.Crm.API.Attachments.FileBodyWrapper();

            //Get instance of StreamWrapper class that takes absolute path of the file to be attached as parameter
            StreamWrapper streamWrapper = new StreamWrapper(absoluteFilePath);

            //Set file to the FileBodyWrapper instance
            fileBodyWrapper.File = streamWrapper;

            //Call UploadAttachment method that takes FileBodyWrapper instance as parameter
            APIResponse<Com.Zoho.Crm.API.Attachments.ActionHandler> response = attachmentsOperations.UploadAttachment(fileBodyWrapper);

            if (response != null)
            {
                return response.StatusCode.ToString();
            }

            return "";

            //if (response != null)
            //{
            //    //Get the status code from response
            //    Console.WriteLine("Status Code: " + response.StatusCode);

            //    //Check if expected response is received
            //    if (response.IsExpected)
            //    {
            //        //Get object from response
            //        Com.Zoho.Crm.API.Attachments.ActionHandler actionHandler = response.Object;

            //        if (actionHandler is Com.Zoho.Crm.API.Attachments.ActionWrapper)
            //        {
            //            //Get the received ActionWrapper instance
            //            Com.Zoho.Crm.API.Attachments.ActionWrapper actionWrapper = (Com.Zoho.Crm.API.Attachments.ActionWrapper)actionHandler;

            //            //Get the list of obtained action responses
            //            List<Com.Zoho.Crm.API.Attachments.ActionResponse> actionResponses = actionWrapper.Data;

            //            foreach (Com.Zoho.Crm.API.Attachments.ActionResponse actionResponse in actionResponses)
            //            {
            //                //Check if the request is successful
            //                if (actionResponse is Com.Zoho.Crm.API.Attachments.SuccessResponse)
            //                {
            //                    //Get the received SuccessResponse instance
            //                    Com.Zoho.Crm.API.Attachments.SuccessResponse successResponse = (Com.Zoho.Crm.API.Attachments.SuccessResponse)actionResponse;

            //                    //Get the Status
            //                    Console.WriteLine("Status: " + successResponse.Status.Value);

            //                    //Get the Code
            //                    Console.WriteLine("Code: " + successResponse.Code.Value);

            //                    Console.WriteLine("Details: ");

            //                    if (successResponse.Details != null)
            //                    {
            //                        //Get the details map
            //                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
            //                        {
            //                            //Get each value in the map
            //                            Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
            //                        }
            //                    }

            //                    //Get the Message
            //                    Console.WriteLine("Message: " + successResponse.Message.Value);
            //                }
            //                //Check if the request returned an exception
            //                else if (actionResponse is Com.Zoho.Crm.API.Attachments.APIException)
            //                {
            //                    //Get the received APIException instance
            //                    Com.Zoho.Crm.API.Attachments.APIException exception = (Com.Zoho.Crm.API.Attachments.APIException)actionResponse;

            //                    //Get the Status
            //                    Console.WriteLine("Status: " + exception.Status.Value);

            //                    //Get the Code
            //                    Console.WriteLine("Code: " + exception.Code.Value);

            //                    Console.WriteLine("Details: ");

            //                    if (exception.Details != null)
            //                    {
            //                        //Get the details map
            //                        foreach (KeyValuePair<string, object> entry in exception.Details)
            //                        {
            //                            //Get each value in the map
            //                            Console.WriteLine(entry.Key + " : " + JsonConvert.SerializeObject(entry.Value));
            //                        }
            //                    }

            //                    //Get the Message
            //                    Console.WriteLine("Message: " + exception.Message.Value);
            //                }
            //            }
            //        }
            //        //Check if the request returned an exception
            //        else if (actionHandler is Com.Zoho.Crm.API.Attachments.APIException)
            //        {
            //            //Get the received APIException instance
            //            Com.Zoho.Crm.API.Attachments.APIException exception = (Com.Zoho.Crm.API.Attachments.APIException)actionHandler;

            //            //Get the Status
            //            Console.WriteLine("Status: " + exception.Status.Value);

            //            //Get the Code
            //            Console.WriteLine("Code: " + exception.Code.Value);

            //            Console.WriteLine("Details: ");

            //            if (exception.Details != null)
            //            {
            //                //Get the details map
            //                foreach (KeyValuePair<string, object> entry in exception.Details)
            //                {
            //                    //Get each value in the map
            //                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
            //                }
            //            }

            //            //Get the Message
            //            Console.WriteLine("Message: " + exception.Message.Value);
            //        }
            //    }
            //    else
            //    { //If response is not as expected

            //        //Get model object from response
            //        Model responseObject = response.Model;

            //        //Get the response object's class
            //        Type type = responseObject.GetType();

            //        //Get all declared fields of the response class
            //        Console.WriteLine("Type is: {0}", type.Name);

            //        PropertyInfo[] props = type.GetProperties();

            //        Console.WriteLine("Properties (N = {0}):", props.Length);

            //        foreach (var prop in props)
            //        {
            //            if (prop.GetIndexParameters().Length == 0)
            //            {
            //                Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
            //            }
            //            else
            //            {
            //                Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
            //            }
            //        }
            //    }
            //}
        }
    }
}