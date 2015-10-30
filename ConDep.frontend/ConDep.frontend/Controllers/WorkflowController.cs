using ConDep.implementation.Managers;
﻿using ConDep.frontend.Models;
using ConDep.implementation.Persistence;
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

        public ActionResult Detail(int id)
        {
            var sw = new StartWorkflowModel();
            sw.StartingParams = WorkflowManager.GetArgumentList(id);
            sw.Id = id;            

            return View(sw);
        }

        [HttpPost]
        public ActionResult Start(int id)
        {
            var paramToStart = Request.Params;
            var paramList = WorkflowManager.GetArgumentList(id);

            Dictionary<string, object> wfParams = new Dictionary<string, object>();
            foreach (var item in paramList)
            {
                wfParams.Add(item, paramToStart[item]);
            }

            try
            {
                var records = WorkflowManager.StartWorkflow(id, wfParams);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex);
            }

            return View(ModelState);
        }

        //public ActionResult Start(int id)
        //{
        //    try
        //    {
        //        var records = WorkflowManager.StartWorkflow(id);
        //    }
        //    catch(Exception ex)
        //    {
        //        ModelState.AddModelError("error", ex);
        //    }

            //    return View(ModelState);
            //}

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

        public ActionResult History(int id)
        {
            var workflowRuns = WorkflowManager.Recieveruns(id);
            HistoryModel model = new HistoryModel()
            {
                WorkflowRuns = new List<WorkflowRunViewModel>()
            };
            foreach (var workflowRun in workflowRuns)
            {
                model.WorkflowRuns.Add(new WorkflowRunViewModel()
                {
                    WorkflowId = workflowRun.WorkflowId,
                    WorkflowRunId = workflowRun.WorkflowRunId,
                    RunTime = workflowRun.StartTime
                });
            }
            return View(model);
        }
    }
}