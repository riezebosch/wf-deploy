using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Persistence
{
    public class WorkflowRun
    {
        [Key]
        public int WorkflowRunId { get; set; }
        public int WorkflowId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
