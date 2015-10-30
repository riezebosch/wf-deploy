using ConDep.implementation.Persistence;
using System;
using System.Activities;
using System.Activities.Tracking;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConDep.implementation.Managers
{
    public class WorkflowManager : IWorkflowManager
    {
        public IList<TrackingRecord> StartWorkflow(int id)
        {
            string fileLocation = null;
            using (var context = new WorkflowContext())
            {
                fileLocation = context.Workflows.First(c => c.Id == id).FileLocation;
            }

            var xamlData = ReadXamlFile(fileLocation);

            var tracker = new CustomTrackingParticipant();
            var wf = ActivityXamlServices.Load(new StringReader(xamlData));
            
            Dictionary<string, object> wfParams = new Dictionary<string, object>();

            AutoResetEvent syncEvent = new AutoResetEvent(false);            
            WorkflowApplication wfApp = new WorkflowApplication(wf);
            wfApp.Extensions.Add(tracker);
            wfApp.Extensions.Add<TextWriter>(() => new StreamWriter(@"C:/XAML/log.txt"));
            // Handle the desired lifecycle events.
            wfApp.Completed = (e) => syncEvent.Set();

            // Start the workflow.
            wfApp.Run();
            syncEvent.WaitOne();

            //using (WorkflowContext context = new WorkflowContext())
            //{
            //    context.Tracks.Add();
            //}
            return tracker.Records;
        }

        private string ReadXamlFile(string fileLocation)
        {
            if (File.Exists(fileLocation))
            {
                return File.ReadAllText(fileLocation);
            }

            throw new FileNotFoundException();
        }

        public void AddWorkflow(Workflow workflow)
        {
            using(var context = new WorkflowContext())
            {
                context.Workflows.Add(workflow);
                context.SaveChanges();
            }
        }

        public IEnumerable<Workflow> RecieveWorkflows()
        {
            using(var context = new WorkflowContext())
            {
                return context.Workflows.ToList();
            }
        }
    }
}
