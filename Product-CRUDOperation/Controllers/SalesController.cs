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
    public class SalesController : Controller
    {
        private MVCEntities1 db = new MVCEntities1();

        // GET: Sales
        public ActionResult Index()
        {
            List<SalesVIewModel> productSolds = db.ProductSolds.Include(p => p.Customer).Include(p => p.Store).Include(p => p.Product).Select(x => new SalesVIewModel
            {
                ID = x.ID,
                ProductID = x.ProductID,
                CustomerID = x.CustomerID,
                DateSold = x.DateSold,
                StoreID = x.StoreID,
                ProductName = x.Product.Name,
                CustomerName = x.Customer.Name,
                StoreName = x.Store.Name,
            }).ToList();
            return View(productSolds);
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] SalesVIewModel salesSold)
        {
            if (ModelState.IsValid)
            {
                var Sale = new ProductSold();
                Sale.ProductID = salesSold.ProductID;
                Sale.StoreID = salesSold.StoreID;
                Sale.CustomerID = salesSold.CustomerID;
                Sale.DateSold = salesSold.DateSold;
                db.ProductSolds.Add(Sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", salesSold.CustomerID);
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Name", salesSold.StoreID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", salesSold.ProductID);
            return View(salesSold);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            SalesVIewModel salesViewModel = new SalesVIewModel
            {
                DateSold = productSold.DateSold,
                CustomerID = productSold.CustomerID,
                ProductID = productSold.ProductID,
                StoreID = productSold.StoreID
            };
            return View(salesViewModel);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] SalesVIewModel salesSold)
        {
            if (ModelState.IsValid)
            {
                var saleData = db.ProductSolds.Find(salesSold.ID);
                if(saleData == null)
                {
                    return HttpNotFound();
                }
                saleData.CustomerID = salesSold.CustomerID;
                saleData.ProductID = salesSold.ProductID;
                saleData.StoreID = salesSold.StoreID;
                saleData.DateSold = salesSold.DateSold;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "Name", salesSold.CustomerID);
            ViewBag.StoreID = new SelectList(db.Stores, "ID", "Name", salesSold.StoreID);
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", salesSold.ProductID);
            return View(salesSold);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSolds.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            SalesVIewModel productViewModel = new SalesVIewModel
            {
                ProductName = productSold.Product.Name,
                StoreName = productSold.Store.Name,
                CustomerName = productSold.Customer.Name,
                DateSold = productSold.DateSold
            };
            return View(productViewModel);
    }

    // POST: Sales/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        ProductSold productSold = db.ProductSolds.Find(id);
        db.ProductSolds.Remove(productSold);
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
