using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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

            WorkflowInvoker.Invoke(wf, wfParams);
        }

        private string ReadXamlFile(string name)
        {
            var pickupDir = ConfigurationManager.AppSettings["XAMLPickupDirectory"];
            var fileLocation = Path.Combine(pickupDir, name, ".xaml");
            if (File.Exists(fileLocation))
            {
                return File.ReadAllText(fileLocation);
            }

            throw new FileNotFoundException();
        }
    }
}
