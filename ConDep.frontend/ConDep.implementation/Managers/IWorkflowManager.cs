using ConDep.implementation.Persistence;
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
        void StartWorkflow(int id);
        void AddWorkflow(Workflow workflow);
        IEnumerable<Workflow> RecieveWorkflows();
        IList<TrackingRecord> StartWorkflow(string name);
    }
}
