using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.ApplicationInsights;

namespace LogReceiverService.Controllers
{
    [Route("api/[controller]")]
    public class LogReceiverController
    {
        TelemetryClient telemetryClient = new TelemetryClient();

        /// <summary>
        /// This method is called from Azure Functions which has an input Trigger configured
        /// for Event Hubs. Diagnostics from different Azure Services are sent to a common Event
        /// Hub, from where Azure Functions sends it to this API.
        /// 
        /// The JSON Parsed in the request is based on the common Schema for the Diagnostic logs
        /// that applies across all Azure Services. The Service specific schema is contained within
        /// the properties of the common schema
        /// </summary>
        /// <param name="searchLogObject"></param>
       // POST api/logreceiver
       [HttpPost]
        public void Post([FromBody] models.CommonObject.RootObject searchLogObject)
        {
            telemetryClient.TrackEvent("Log receiver has received a diagnostics log message");
            try
            {
                telemetryClient.TrackTrace("The incoming log message " + searchLogObject.records[0].resourceId);
                string category = searchLogObject.records[0].category;
                string resourceId = searchLogObject.records[0].resourceId;
                telemetryClient.TrackTrace("Received "+ searchLogObject.records.Count+
                    "diagnostic logs in Category :"+category +
                    " , and the Resource Id is "+resourceId);
                if(resourceId.ToLower().Contains("microsoft.search"))
                {
                    telemetryClient.TrackTrace("This diagnostic log is from Azure Search"); 
                }
                else if(resourceId.ToLower().Contains("microsoft.documentdb"))
                {
                    telemetryClient.TrackTrace("This diagnostic log is from Azure Cosmos DB");
                }
                else
                {
                    telemetryClient.TrackTrace("Diagnostic log is from an unknown Service");
                }
                //This contains the JSON for the Azure Service specific properties
                Object props = searchLogObject.records[0].properties;
            }
            catch(Exception ex)
            {
                telemetryClient.TrackTrace("Exception parsing the Diagnostic Logs: "+ex.Message+ ", trace " + ex.StackTrace);
            }
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
