using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConDep.implementation.Persistence;

namespace ConDep.implementation.Managers
{
    public class TrackManager : ITrackManager
    {
        public IEnumerable<Track> RetrieveTracks(int id)
        {
            using (var context = new WorkflowContext())
            {
                return context.Tracks.Where(c => c.WorkflowRunId == id).ToList();
            }
        }
    }
}
