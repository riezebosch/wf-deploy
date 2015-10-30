using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Managers
{
    public interface IWorkflowManager
    {
        IList<TrackingRecord> StartWorkflow(string name);
    }
}
