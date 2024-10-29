using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group12_iCAREAPP.Models;

namespace Group12_iCAREAPP.Controllers
{
    public class PatientRecordsController : Controller
    {
        private Group12_iCAREDBEntities db = new Group12_iCAREDBEntities();

        // GET: PatientRecords
        public ActionResult Index()
        {
            var patientRecord = db.PatientRecord.Include(p => p.GeoCodes).Include(p => p.iCAREWorker);
            return View(patientRecord.ToList());
        }

        // GET: PatientRecords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecord.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // GET: PatientRecords/Create
        public ActionResult Create()
        {
            ViewBag.geoUnitID = new SelectList(db.GeoCodes, "ID", "description");
            ViewBag.maintainWorkerID = new SelectList(db.iCAREWorker, "ID", "profession");
            return View();
        }

        // POST: PatientRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,address,dateOfBirth,height,weight,bloodGroup,bedID,treatmentArea,geoUnitID,maintainWorkerID")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.PatientRecord.Add(patientRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.geoUnitID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.geoUnitID);
            ViewBag.maintainWorkerID = new SelectList(db.iCAREWorker, "ID", "profession", patientRecord.maintainWorkerID);
            return View(patientRecord);
        }

        // GET: PatientRecords/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecord.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.geoUnitID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.geoUnitID);
            ViewBag.maintainWorkerID = new SelectList(db.iCAREWorker, "ID", "profession", patientRecord.maintainWorkerID);
            return View(patientRecord);
        }

        // POST: PatientRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,dateOfBirth,height,weight,bloodGroup,bedID,treatmentArea,geoUnitID,maintainWorkerID")] PatientRecord patientRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.geoUnitID = new SelectList(db.GeoCodes, "ID", "description", patientRecord.geoUnitID);
            ViewBag.maintainWorkerID = new SelectList(db.iCAREWorker, "ID", "profession", patientRecord.maintainWorkerID);
            return View(patientRecord);
        }

        // GET: PatientRecords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientRecord patientRecord = db.PatientRecord.Find(id);
            if (patientRecord == null)
            {
                return HttpNotFound();
            }
            return View(patientRecord);
        }

        // POST: PatientRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PatientRecord patientRecord = db.PatientRecord.Find(id);
            db.PatientRecord.Remove(patientRecord);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AssignWorkerToPatient(string patientID)
        {
            // Get the current user's ID, assumed from authentication
            string workerID = Session["userID"].ToString();

            // Retrieve the patient record from the database
            PatientRecord patient = db.PatientRecord.SingleOrDefault(p => p.ID == patientID);

            if (patient != null)
            {
                // Assign the worker's ID to the maintainWorkerID field
                patient.maintainWorkerID = workerID;
                db.SaveChanges();
                ViewBag.Message = "Worker ID added";
            }
            else
            {
                ViewBag.Message = "Worker failed to be added";
            }
            return RedirectToAction("Index", "PatientRecords");
        }

        //ListAssignedPatients
        public ActionResult ListAssignedPatients()
        {
            // Get the current user's ID
            string currentUserID = Session["userID"].ToString();

            // Retrieve patients assigned to the current user
            var assignedPatients = db.PatientRecord
                                     .Where(p => p.maintainWorkerID == currentUserID)
                                     .ToList();

            return View(assignedPatients);
        }
    }
}
