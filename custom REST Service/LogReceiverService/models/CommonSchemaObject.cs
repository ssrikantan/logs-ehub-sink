using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogReceiverService.models.CommonObject
{
        public class Record
        {
            //These are some of the properties from Azure diagnostic logs that are common across
            // all Azure Services. For a complete list of these schema attributes refer to
            // this link https://docs.microsoft.com/en-us/azure/monitoring-and-diagnostics/monitoring-diagnostic-logs-schema
            public DateTime time { get; set; }
            public string resourceId { get; set; }
            public string category { get; set; }
            public string operationName { get; set; }


            //This contains the schema that is specific to every Azure Service
            public Object properties { get; set; }
        }

        public class RootObject
        {
            public List<Record> records { get; set; }
        }
    
}
