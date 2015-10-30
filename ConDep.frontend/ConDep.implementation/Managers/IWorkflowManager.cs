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
        void AddWorkflow(Workflow workflow);
        IEnumerable<Workflow> RecieveWorkflows();
        IList<TrackingRecord> StartWorkflow(int id);
        IList<TrackingRecord> StartWorkflow(int id, Dictionary<string, object> wfParams);
        IList<string> GetArgumentList(int id);
        IEnumerable<WorkflowRun> Recieveruns(int id);
    }
}
