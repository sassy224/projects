using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VCCI.DAL;

namespace VCCI.Controllers
{
    public class QuotaController : Controller
    {
        private VCCIEntities db = new VCCIEntities();

        //
        // GET: /Quota/

        public ActionResult Index()
        {
            return View(db.Quotas.ToList());
        }

        //
        // GET: /Quota/Details/5

        public ActionResult Details(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            return View(quota);
        }

        //
        // GET: /Quota/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Quota/Create

        [HttpPost]
        public ActionResult Create(Quota quota)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Quotas.Add(quota);
                    if (db.GetValidationErrors().Count() > 0)
                    {
                        //ViewBag.Message
                    }
                    else
                    {
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                
            }

            return View(quota);
        }

        //
        // GET: /Quota/Edit/5

        public ActionResult Edit(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            return View(quota);
        }

        //
        // POST: /Quota/Edit/5

        [HttpPost]
        public ActionResult Edit(Quota quota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quota);
        }

        //
        // GET: /Quota/Delete/5

        public ActionResult Delete(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
            if (quota == null)
            {
                return HttpNotFound();
            }
            return View(quota);
        }

        //
        // POST: /Quota/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Quota quota = db.Quotas.Find(id);
            db.Quotas.Remove(quota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}