using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Model
{
    public class TraceRecord
    {
        public IDictionary<string,string> Annotations { get; set; }

        public DateTime EventTime { get; set; }

        public long RecordNumber { get; set; }

        public Guid InstanceId { get; set; }

    }
}
