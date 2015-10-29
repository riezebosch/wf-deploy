using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Managers
{
    public interface IWorkflowManager
    {
        void StartWorkflow(string name);
    }
}
