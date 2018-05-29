using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Product_CRUDOperation.Models;

namespace Product_CRUDOperation.Controllers
{
    public class StoresController : Controller
    {
        private MVCEntities1 db = new MVCEntities1();

        // GET: Stores
        public ActionResult Index()
        {
            var storeList = db.Stores.Select(x => new StoreViewModel
            {
                Address = x.Address,
                Name = x.Name,
                ID = x.ID
            }).ToList();
            return View(storeList);

        }

        // GET: Stores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var storeList = db.Stores.Where(x => x.ID == id).Select(y => new StoreViewModel
            {
                Address = y.Address,
                Name = y.Name,
                ID = y.ID
            }).FirstOrDefault();
            if (storeList == null)
            {
                return HttpNotFound();
            }
            return View(storeList);
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address")] StoreViewModel storeViewModel)
        {
            if (ModelState.IsValid)
            {
                var store = new Store();
                store.Name = storeViewModel.Name;
                store.Address = storeViewModel.Address;
                db.Stores.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeViewModel);
        }

        // GET: Stores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            var model = new StoreViewModel
            {
                ID = store.ID,
                Name = store.Name,
                Address = store.Address
            };
            return View(model);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address")] StoreViewModel storeViewModel)
        {
            if (ModelState.IsValid)
            {
                var store = db.Stores.Find(storeViewModel.ID);
                if (store == null)
                {
                    return HttpNotFound();
                }
                store.Name = storeViewModel.Name;
                store.Address = storeViewModel.Address;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storeViewModel);
        }

        // GET: Stores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            var model = new StoreViewModel
            {
                ID = store.ID,
                Name = store.Name,
                Address = store.Address
            };

            ViewBag.CanDelete = !store.ProductSolds.Any();
            return View(model);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Stores.Find(id);
            db.Stores.Remove(store);
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
    }
}
