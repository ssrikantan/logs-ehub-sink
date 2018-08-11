using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogReceiverService.models.SearchLogs
{

    /// <summary>
    /// Contains the common schema along with the Properties array that is specific to
    /// Azure Search. It is not used in this sample, retained here only for Information
    /// </summary>
    public class Properties
    {
        public string Description { get; set; }
        public string Query { get; set; }
        public int Documents { get; set; }
        public string IndexName { get; set; }
    }

    public class Record
    {
        public DateTime time { get; set; }
        public string resourceId { get; set; }
        public string operationName { get; set; }
        public string operationVersion { get; set; }
        public string category { get; set; }
        public string resultType { get; set; }
        public int resultSignature { get; set; }
        public int durationMS { get; set; }
        public Properties properties { get; set; }
    }

    public class RootObject
    {
        public List<Record> records { get; set; }
    }
}
