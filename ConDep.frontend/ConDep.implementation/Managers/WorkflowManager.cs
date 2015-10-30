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
            Dictionary<string, object> wfParams = new Dictionary<string, object>();
            return this.StartWorkflow(id, wfParams);
        }
            
        public IList<TrackingRecord> StartWorkflow(int id, Dictionary<string, object> wfParams)
        {
            string fileLocation = null;
            int workflowRunId = 0;
            using (var context = new WorkflowContext())
            {
                fileLocation = context.Workflows.First(c => c.Id == id).FileLocation; // Hier kan een NullReferenceException optreden
                var workflowRun = new WorkflowRun()
                {
                    WorkflowId = id,
                    StartTime = DateTime.Now
                };
                context.WorkflowRuns.Add(workflowRun);
                context.SaveChanges();
                workflowRunId = workflowRun.WorkflowRunId;
            }

            var xamlData = ReadXamlFile(fileLocation);

            var tracker = new CustomTrackingParticipant();
            var wf = ActivityXamlServices.Load(new StringReader(xamlData), new ActivityXamlServicesSettings {  CompileExpressions = true });

            AutoResetEvent syncEvent = new AutoResetEvent(false);
            WorkflowApplication wfApp = new WorkflowApplication(wf, wfParams);
            wfApp.Extensions.Add(tracker);
            wfApp.Extensions.Add<TextWriter>(() => new StreamWriter(@"C:/XAML/log.txt"));
            // Handle the desired lifecycle events.
            wfApp.Completed = (e) => syncEvent.Set();
            wfApp.OnUnhandledException += (WorkflowApplicationUnhandledExceptionEventArgs e) => 
            {
                throw e.UnhandledException;
            };

            // Start the workflow.
            wfApp.Run();
            syncEvent.WaitOne();

            var tracks = MapTracks(tracker.Records, (t) =>
            {
                t.WorkflowRunId = workflowRunId;
                return t;
            });

            using (WorkflowContext context = new WorkflowContext())
            {
                context.Tracks.AddRange(tracks);
                context.SaveChanges();
            }
            return tracker.Records;
        }

        public IList<string> GetArgumentList(int id)
        {
            List<string> argumentList = new List<string>();
            string fileLocation = null;
            using (var context = new WorkflowContext())
            {
                fileLocation = context.Workflows.First(c => c.Id == id).FileLocation; // Hier kan een NullReferenceException optreden

                string xamlData = ReadXamlFile(fileLocation);
                var activity = ActivityXamlServices.Load(new StringReader(xamlData)) as DynamicActivity;
                
                var properties = activity.Properties.ToList();

                foreach (var item in properties)
                {
                    argumentList.Add(item.Name);
                }
            }

            return argumentList;
        }
        private IEnumerable<Track> MapTracks(IList<TrackingRecord> trackingRecords, Func<Track, Track> afterMap)
        {
            var tracks = new List<Track>();

            foreach(var trackingRecord in trackingRecords.Where(c => c.GetType() == typeof(ActivityStateRecord)))
            {
                var record = trackingRecord as ActivityStateRecord;
                var track = new Track()
                {
                    State = record.State,
                    ActivityName = record.Activity.Name,
                    EventTime = record.EventTime
                };
                track = afterMap(track);
                tracks.Add(track);
            }
            return tracks;
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

        public IEnumerable<WorkflowRun> Recieveruns(int id)
        {
            using (var context = new WorkflowContext())
            {
                return context.WorkflowRuns.Where(c => c.WorkflowId == id).ToList();
            }
        }

    }
}
