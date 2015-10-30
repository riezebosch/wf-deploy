using ConDep.implementation.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConDep.frontend.Controllers
{
    public class WorkflowController : Controller
    {
        #region Properties

        public IWorkflowManager WorkflowManager { get; set; }

        #endregion

        #region Constructors

        public WorkflowController() : this(null) { }

        public WorkflowController(IWorkflowManager manager)
        {
            WorkflowManager = manager ?? new WorkflowManager();
        }

        #endregion

        public ActionResult Start()
        {
            try
            {
                WorkflowManager.StartWorkflow("sample");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", ex);
            }

            return View(ModelState);
        }
    }
}