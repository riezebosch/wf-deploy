using System;
using System.Activities.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Extensions
{
    public class TracingExtension : IWorkflowInstanceExtension
    {
        public List<string> Messages { get; set; }

        public IEnumerable<object> GetAdditionalExtensions()
        {
            throw new NotImplementedException();
        }

        public void SetInstance(WorkflowInstanceProxy instance)
        {
            throw new NotImplementedException();
        }
    }
}
