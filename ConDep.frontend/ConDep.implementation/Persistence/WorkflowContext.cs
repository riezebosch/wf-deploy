using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConDep.implementation.Persistence
{
    public class WorkflowContext : DbContext
    {
        public WorkflowContext() 
            : base("WorkflowContext")
        {
            Database.SetInitializer(new WorkflowDataInitializer());
        }

        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<WorkflowRun> WorkflowRuns { get; set; }

        public DbSet<Track> Tracks { get; set; }
    }

    public class WorkflowDataInitializer : DropCreateDatabaseIfModelChanges<WorkflowContext>
    {

    }
}
