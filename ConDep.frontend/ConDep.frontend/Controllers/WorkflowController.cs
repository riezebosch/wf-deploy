using ConDep.implementation.Managers;
using ConDep.implementation.Model;
﻿using ConDep.frontend.Models;
using ConDep.implementation.Persistence;
using ConDep.implementation.Managers;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Start(int id)
        {
            try
            {
                var records = WorkflowManager.StartWorkflow(id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", ex);
            }

            return View(ModelState);
        }

        public ActionResult Overview()
        {
            var workflows = WorkflowManager.RecieveWorkflows();
            OverviewModel model = new OverviewModel()
            {
                Workflows = new List<WorkflowViewModel>()
            };
            foreach(var workflow in workflows)
            {
                model.Workflows.Add(new WorkflowViewModel()
                {
                    Id = workflow.Id,
                    Filename = workflow.Filename
                });
            }
            return View(model);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(@"C:/XAML", fileName);
                file.SaveAs(path);

                Workflow workflow = new Workflow()
                {
                    FileLocation = path,
                    Filename = fileName
                };
                WorkflowManager.AddWorkflow(workflow);
            }

            return RedirectToAction("Overview", "Workflow");
        }
    }
}