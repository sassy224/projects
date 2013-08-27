using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VCCI.DAL;
using log4net;
using VCCI.Models;

namespace VCCI.Controllers
{
    [Authorize]
    public class QuotaController : Controller
    {
        private VCCIEntities db = new VCCIEntities();
        //log to C:\log\TRI-RoundUP-Web.txt
		//log4net.Config.XmlConfigurator.Configure();
        private ILog log;

        public QuotaController()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger(typeof(QuotaController));
        }

        //
        // GET: /Quota/

        public ActionResult Index()
        {
			List<Quota> quotas = db.Quotas.OrderByDescending(m => m.CreatedAt).ToList();
            
			//try
			//{   
			//    foreach (Quota quota in db.Quotas.ToList().OrderByDescending(m => m.CreatedAt))
			//    {
			//        //if (quota.Description.Length > 100)
			//        //    quota.Description = quota.Description.Substring(0, 100) + "...";
			//        quotas.Add(quota);
			//    }
			//}
			//catch (Exception ex)
			//{
			//    log.Error(ex.Message, ex);
			//}
            return View(quotas);
           
        }

        //
        // GET: /Quota/Details/5

        public ActionResult Details(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
            try
            {
                if (quota == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return View(quota);
        }

        //
        // GET: /Quota/Create

        public ActionResult Create()
        {		
			return View(new QuotaModel());
        }

        //
        // POST: /Quota/Create

        [HttpPost]
        public ActionResult Create(QuotaModel quotaModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
					Quota quota = new Quota();

					quota.Title = quotaModel.Title;
					quota.Description = quotaModel.Description;
					quota.CreatedBy = quotaModel.CreatedBy;
					quota.DepartmentId = int.Parse(quotaModel.SelectedDepartment);
					quota.LastModifiedAt = DateTime.Now;
                    quota.CreatedAt = DateTime.Now;
                    quota.LastModifiedBy = User.Identity.Name;
                    
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
                    log.Error(ex.Message, ex);
                }
                
            }

            return View(quotaModel);
        }

        //
        // GET: /Quota/Edit/5

        public ActionResult Edit(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
			QuotaModel quotaModel = new QuotaModel();
            try
            {
                if (quota == null)
                {
                    return HttpNotFound();
                }
				quotaModel.ID = quota.ID;
				quotaModel.Title = quota.Title;
				quotaModel.Description = quota.Description;
				quotaModel.CreatedBy = quota.CreatedBy;
				quotaModel.SelectedDepartment = quota.DepartmentId.ToString();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
			return View(quotaModel);
        }

        //
        // POST: /Quota/Edit/5

        [HttpPost]
        public ActionResult Edit(QuotaModel quotaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
					Quota quota = db.Quotas.Find(quotaModel.ID);

					quota.Title = quotaModel.Title;
					quota.Description = quotaModel.Description;
					quota.CreatedBy = quotaModel.CreatedBy;
					quota.DepartmentId = int.Parse(quotaModel.SelectedDepartment);
					quota.LastModifiedAt = DateTime.Now;
					quota.CreatedAt = DateTime.Now;
					quota.LastModifiedBy = User.Identity.Name;

					db.Entry(quota).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return View(quotaModel);
        }

        //
        // GET: /Quota/Delete/5

        public ActionResult Delete(decimal id = 0)
        {
            Quota quota = db.Quotas.Find(id);
            try
            {
                if (quota == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
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