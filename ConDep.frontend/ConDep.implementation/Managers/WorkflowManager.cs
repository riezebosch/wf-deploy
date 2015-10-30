using ConDep.implementation.Extensions;
using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConDep.implementation.Managers
{
    public class WorkflowManager : IWorkflowManager
    {
        public void StartWorkflow(string name)
        {
            var xamlData = ReadXamlFile(name);

            DynamicActivity wf = ActivityXamlServices.Load(new StringReader(xamlData)) as DynamicActivity;
            Dictionary<string, object> wfParams = new Dictionary<string, object>();

            AutoResetEvent syncEvent = new AutoResetEvent(false);

            WorkflowApplication wfApp = new WorkflowApplication(wf);
            wfApp.Extensions.Add(new TracingExtension());
            // Handle the desired lifecycle events.
            wfApp.Completed = delegate (WorkflowApplicationCompletedEventArgs e)
            {
                syncEvent.Set();
            };

            // Start the workflow.
            wfApp.Run();

            syncEvent.WaitOne();
        }

        private Activity ReadActivityFromDll(string name)
        {
            var pickupDir = ConfigurationManager.AppSettings["XAMLPickupDirectory"];
            var fileLocation = Path.Combine(pickupDir, name);

            var assembly = Assembly.LoadFrom(fileLocation);
            
            
        }
    }
}
