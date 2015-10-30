using ConDep.implementation.Managers;
using ConDep.implementation.Model;
﻿using ConDep.frontend.Models;
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

        public ActionResult Start()
        {
            try
            {
                var records = WorkflowManager.StartWorkflow("sample.xaml");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("error", ex);
            }

            return View(ModelState);
        }

        public ActionResult Overview()
        {
            OverviewModel model = new OverviewModel();
            model.Files = Directory.GetFiles(@"C:\XAML\");
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
            }

            return RedirectToAction("Overview", "Workflow");
        }
    }
}