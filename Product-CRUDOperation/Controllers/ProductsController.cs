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
    public class ProductsController : Controller
    {
        private MVCEntities1 db = new MVCEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var productList = db.Products.Select(x => new ProductViewModel
            {
                Price = x.Price,
                Name = x.Name,
                ID = x.ID
            }).ToList();
            return View(productList);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productList = db.Products.Where(x => x.ID == id).Select(y => new ProductViewModel
            {
                Price = y.Price,
                Name = y.Name,
                ID = y.ID
            }).FirstOrDefault();
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // GET: Products/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price")] ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.SingleOrDefault(x => x.ID == productModel.ID);
                if (product == null)
                {
                    product = new Product();
                    product.Name = productModel.Name;
                    product.Price = Convert.ToDecimal(productModel.Price);
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(productModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productModel = db.Products.Where(x => x.ID == id).Select(y => new ProductViewModel
            {
                ID = y.ID,
                Name = y.Name,
                Price = y.Price
            }).FirstOrDefault();
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);

        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price")] ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {

                var prod = db.Products.Find(productModel.ID);
                prod.Name = productModel.Name;
                prod.Price = productModel.Price;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View(productModel);
            
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductViewModel productViewModel = new ProductViewModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,

            };
            ViewBag.CanDelete = !product.ProductSolds.Any();
            //db.SaveChanges();
            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
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
