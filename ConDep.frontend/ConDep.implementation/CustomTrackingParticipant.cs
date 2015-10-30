using ConDep.implementation.Model;
using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation
{
    public class CustomTrackingParticipant : TrackingParticipant
    {
        public bool IsInvoked { get; internal set; }

        public IList<TrackingRecord> Records { get; } = new List<TrackingRecord>();

        public IList<TraceRecord> GetTraceRecords() {
            List<TraceRecord> traces = new List<TraceRecord>();

            foreach (var record in Records)
            {
                traces.Add(new TraceRecord
                {
                    Annotations = record.Annotations,
                    EventTime = record.EventTime,
                    InstanceId = record.InstanceId,
                    RecordNumber = record.RecordNumber
                });
            }
            return traces;
        } 

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            Records.Add(record);
            IsInvoked = true;
        }
    }
}
