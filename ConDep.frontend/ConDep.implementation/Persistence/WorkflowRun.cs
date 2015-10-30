using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Persistence
{
    class WorkflowRun
    {
        public int TrackId { get; set; }
        public int WorkflowId { get; set; }
        public DateTime StartTime{ get; set; }
    }
}
