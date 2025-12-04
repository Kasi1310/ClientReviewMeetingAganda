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
    public class clsRecords
    {
        public string SearchRecords()
        {
            //example
            string moduleAPIName = "Leads";
            //Get instance of RecordOperations Class
            RecordOperations recordOperations = new RecordOperations();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(SearchRecordsParam.CRITERIA, "((Last_Name:starts_with:Last Name) and (Company:starts_with:fasf\\(123\\) K))");
            paramInstance.Add(SearchRecordsParam.EMAIL, "abc@zoho.com");
            paramInstance.Add(SearchRecordsParam.PHONE, "234567890");
            paramInstance.Add(SearchRecordsParam.WORD, "First Name Last Name");
            paramInstance.Add(SearchRecordsParam.CONVERTED, "both");
            paramInstance.Add(SearchRecordsParam.APPROVED, "both");
            paramInstance.Add(SearchRecordsParam.PAGE, 1);
            paramInstance.Add(SearchRecordsParam.PER_PAGE, 2);
            HeaderMap headerInstance = new HeaderMap();
            //Call SearchRecords method that takes moduleAPIName and ParameterMap Instance as parameter
            APIResponse<ResponseHandler> response = recordOperations.SearchRecords(moduleAPIName, paramInstance, headerInstance);
            if (response != null)
            {
                //Get the status code from response

                //if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                //{
                //    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");

                //}
                //Check if expected response is received
                if (response.IsExpected)
                {
                    //Get the object from response
                    ResponseHandler responseHandler = response.Object;
                    if (responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                        //Get the obtained Record instance
                        List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;
                        foreach (Com.Zoho.Crm.API.Record.Record record in records)
                        {
                            //Get the ID of each Record
                            return "Record ID: " + record.Id;
                        }
                        return "Record ID: ";
                    }
                    //Check if the request returned an exception
                    else if (responseHandler is APIException)
                    {
                        //Get the received APIException instance
                        APIException exception = (APIException)responseHandler;

                        //Get the Message
                        return "Message: " + exception.Message.Value;
                    }
                    return "Status Code: " + response.StatusCode;
                }
                else
                {
                    return "error";
                }

                return "";
            }
            return "";
        }
    }
}