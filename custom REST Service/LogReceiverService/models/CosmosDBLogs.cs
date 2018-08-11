using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogReceiverService.models.CosmosDBLogs
{
    /// <summary>
    /// Contains the common schema along with the Properties array that is specific to
    /// Cosmons DB. It is not used in this sample, retained here only for Information
    /// </summary>
    public class Properties
    {
        public string activityId { get; set; }
        public string requestResourceType { get; set; }
        public string requestResourceId { get; set; }
        public string collectionRid { get; set; }
        public string statusCode { get; set; }
        public string duration { get; set; }
        public string userAgent { get; set; }
        public string clientIpAddress { get; set; }
        public string requestCharge { get; set; }
        public string requestLength { get; set; }
        public string responseLength { get; set; }
        public string resourceTokenUserRid { get; set; }
        public string region { get; set; }
        public string partitionId { get; set; }
    }

    public class Record
    {
        public DateTime time { get; set; }
        public string resourceId { get; set; }
        public string category { get; set; }
        public string operationName { get; set; }
        public Properties properties { get; set; }
    }

    public class RootObject
    {
        public List<Record> records { get; set; }
    }
}
