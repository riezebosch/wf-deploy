using ConDep.frontend.Models;
using ConDep.implementation.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConDep.frontend.Controllers
{
    public class WorkflowRunController : Controller
    {
        #region Properties

        public ITrackManager TrackManager { get; set; }

        #endregion

        #region Constructors

        public WorkflowRunController() : this(null) { }

        public WorkflowRunController(ITrackManager manager)
        {
            TrackManager = manager ?? new TrackManager();
        }

        #endregion

        public ActionResult Details(int id)
        {
            var tracks = TrackManager.RetrieveTracks(id);
            TracksViewModel model = new TracksViewModel()
            {
                Tracks = new List<TrackViewModel>()
            };
            foreach (var track in tracks)
            {
                model.Tracks.Add(new TrackViewModel()
                {
                    ActivityName = track.ActivityName,
                    EventTime = track.EventTime,
                    State = track.State
                });
            }
            return View(model);
        }
    }
}