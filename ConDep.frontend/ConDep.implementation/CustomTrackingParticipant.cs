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

        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            Records.Add(record);
            IsInvoked = true;
        }
    }
}
