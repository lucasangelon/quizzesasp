using Quizzes7.DAL;
using Quizzes7.Models;
using Quizzes7.Helpers;
using Quizzes7.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace Quizzes7.Controllers
{
    public class ClusterUnitController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();

        // GET: ClusterUnit
        public ActionResult Index(int? id, int? unitID)
        {
            var viewModel = new ClusterUnitIndexData();
            viewModel.clusters = databaseContext.cluster
                .Include(i => i.units);

            if (id != null)
            {
                ViewBag.ClusterID = id.Value;
                viewModel.units = viewModel.clusters.Where(i => i.id == id.Value).Single().units;
            }

            if (unitID != null)
            {
                ViewBag.UnitID = unitID.Value;
                viewModel.lecturers = viewModel.units.Where(x => x.id == unitID).Single().users.Where(x => x.id == 2);
            }

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cluster cluster = databaseContext.cluster
                .Include(i => i.units)
                .Where(i => i.id == id)
                .Single();

            populateAssignedUnitsData(cluster);

            if (cluster == null)
            {
                return HttpNotFound();
            }

            return View(cluster);
        }

        public ActionResult Create()
        {
            var cluster = new Cluster();
            cluster.units = new List<Unit>();
            populateAssignedUnitsData(cluster);
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name")]Cluster cluster, string[] selectedUnits)
        {
            if (selectedUnits != null)
            {
                cluster.units = new List<Unit>();
                foreach (var unit in selectedUnits)
                {
                    var unitToAdd = databaseContext.unit.Find(int.Parse(unit));
                    cluster.units.Add(unitToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                databaseContext.cluster.Add(cluster);
                databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }

            populateAssignedUnitsData(cluster);
            return View(cluster);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string[] selectedUnits)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clusterToUpdate = databaseContext.cluster
                .Include(i => i.units)
                .Where(i => i.id == id)
                .Single();

            if (TryUpdateModel(clusterToUpdate, "", new string[] { "name" }))
            {
                try
                {
                    updateClusterUnits(selectedUnits, clusterToUpdate);

                    databaseContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            populateAssignedUnitsData(clusterToUpdate);
            return View(clusterToUpdate);
        }

        private void populateAssignedUnitsData(Cluster cluster)
        {
            var allUnits = databaseContext.unit;
            var clusterUnits = new HashSet<int>(cluster.units.Select(c => c.id));
            var viewModel = new List<AssignedUnitsData>();

            foreach(var unit in allUnits)
            {
                viewModel.Add(new AssignedUnitsData
                    {
                        UnitID = unit.id,
                        Name = unit.name,
                        Assigned = clusterUnits.Contains(unit.id)
                    });
            }

            ViewBag.Units = viewModel;
        }

        private void updateClusterUnits(string[] selectedUnits, Cluster clusterToUpdate)
        {
            if (selectedUnits == null)
            {
                clusterToUpdate.units = new List<Unit>();
                return;
            }

            var selectedUnitsHS = new HashSet<string>(selectedUnits);
            var clusterUnits = new HashSet<int>(clusterToUpdate.units.Select(c => c.id));

            foreach (var unit in databaseContext.unit)
            {
                if (selectedUnitsHS.Contains(unit.id.ToString()))
                {
                    if (!clusterUnits.Contains(unit.id))
                    {
                        clusterToUpdate.units.Add(unit);
                    }
                }
                else
                {
                    if (clusterUnits.Contains(unit.id))
                    {
                        clusterToUpdate.units.Remove(unit);
                    }
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cluster cluster = databaseContext.cluster.Find(id);
            if (cluster == null)
            {
                return HttpNotFound();
            }
            return View(cluster);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cluster cluster = databaseContext.cluster
              .Where(i => i.id == id)
              .Single();

            databaseContext.cluster.Remove(cluster);
            databaseContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                databaseContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}