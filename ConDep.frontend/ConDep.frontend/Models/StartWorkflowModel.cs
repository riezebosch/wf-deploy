using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConDep.frontend.Models
{
    public class StartWorkflowModel
    {
        public int Id { get; set; }
        public IList<string> StartingParams { get; set; }
    }
}