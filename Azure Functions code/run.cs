using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;


public static async Task Run(string myEventHubMessage, TraceWriter log)
{
    log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage}");

    // This step to access the message coming in, deserializing it into a .NET object
    // is required only if you want to edit the data coming in. Else we can directly
    // pass the JSON String (myEventHubMessage) to the REST API Post operation performed below
    RootObject incomingData= Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(myEventHubMessage);
    int numRecords = incomingData.records.Count;
    log.Info($"The number of records in the log is: "+numRecords);

    try
    {
        HttpClient client = new HttpClient();
        Uri refUri = new Uri("http://logreceiverservice20180811092438.azurewebsites.net/api/logreceiver");

        //RootObject refObject = new RootObject();
        //refObject.records.Add(new Record
        //    {
        //        resourceId = "ID001",operationName="INDEXING",category="DEMOCATEOG"
        //    }
        //);
        HttpResponseMessage response = await client.PostAsync(refUri,new StringContent(myEventHubMessage, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
    }
    catch(Exception ex)
    {
        log.Info($"There was a problem sending to the REST API: "+ex.StackTrace);
    }

}

public class Record
{
    public DateTime time { get; set; }
    public string resourceId { get; set; }
    public string category { get; set; }
    public string operationName { get; set; }
    public Object properties { get; set; }
}

public class RootObject
{
    public List<Record> records { get; set; }
}
