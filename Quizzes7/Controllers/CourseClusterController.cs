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
    public class CourseClusterController : Controller
    {
        private QuizzesContext databaseContext = new QuizzesContext();

        // GET: CourseCluster
        public ActionResult Index(int? id, int? clusterID)
        {
            var viewModel = new CourseClusterIndexData();
            viewModel.courses = databaseContext.course
                .Include(i => i.clusters);

            if (id != null)
            {
                ViewBag.CourseID = id.Value;
                viewModel.clusters = viewModel.courses.Where(i => i.id == id.Value).Single().clusters;
            }

            if (clusterID != null)
            {
                ViewBag.ClusterID = clusterID.Value;
                viewModel.units = viewModel.clusters.Where(x => x.id == clusterID).Single().units;
            }



            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = databaseContext.course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = databaseContext.course
                .Include(i => i.clusters)
                .Where(i => i.id == id)
                .Single();
            
            populateAssignedClusterData(course);

            if (course == null)
            {
                return HttpNotFound();
            }
            
            return View(course);
        } 

        public ActionResult Create()
        {
            var course = new Course();
            course.clusters = new List<Cluster>();
            populateAssignedClusterData(course);
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Name")]Course course, string[] selectedClusters)
        {
            if (selectedClusters != null)
            {
                course.clusters = new List<Cluster>();
                foreach (var cluster in selectedClusters)
                {
                    var clusterToAdd = databaseContext.cluster.Find(int.Parse(cluster));
                    course.clusters.Add(clusterToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                databaseContext.course.Add(course);
                databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }

            populateAssignedClusterData(course);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string[] selectedClusters)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseToUpdate = databaseContext.course
                .Include(i => i.clusters)
                .Where(i => i.id == id)
                .Single();

            if (TryUpdateModel(courseToUpdate, "", new string[] { "name" }))
            {
                try
                {
                    updateCourseClusters(selectedClusters, courseToUpdate);

                    databaseContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            populateAssignedClusterData(courseToUpdate);
            return View(courseToUpdate);
        }

        private void populateAssignedClusterData(Course course)
        {
            var allClusters = databaseContext.cluster;
            var courseClusters = new HashSet<int>(course.clusters.Select(c => c.id));
            var viewModel = new List<AssignedClustersData>();

            foreach (var cluster in allClusters)
            {
                viewModel.Add(new AssignedClustersData
                    {
                        ClusterID = cluster.id,
                        Name = cluster.name,
                        Assigned = courseClusters.Contains(cluster.id)
                    });
            }

            ViewBag.Clusters = viewModel;
        }

        private void updateCourseClusters(string[] selectedClusters, Course courseToUpdate)
        {
            if (selectedClusters == null)
            {
                courseToUpdate.clusters = new List<Cluster>();
                return;
            }

            var selectedClustersHS = new HashSet<string>(selectedClusters);
            var courseClusters = new HashSet<int>(courseToUpdate.clusters.Select(c => c.id));

            foreach (var cluster in databaseContext.cluster)
            {
                if (selectedClustersHS.Contains(cluster.id.ToString()))
                {
                    if (!courseClusters.Contains(cluster.id))
                    {
                        courseToUpdate.clusters.Add(cluster);
                    }
                }
                else
                {
                    if (courseClusters.Contains(cluster.id))
                    {
                        courseToUpdate.clusters.Remove(cluster);
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
            Course course = databaseContext.course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = databaseContext.course
              .Where(i => i.id == id)
              .Single();

            databaseContext.course.Remove(course);
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