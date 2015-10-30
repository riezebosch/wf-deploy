using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConDep.frontend.Models
{
    public class WorkflowRunViewModel
    {
        public int WorkflowId { get; set; }
        public int WorkflowRunId { get; set; }
        public DateTime RunTime { get; set; }
    }
}