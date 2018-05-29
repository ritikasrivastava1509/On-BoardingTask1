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
    public class CustomersController : Controller
    {
        private MVCEntities1 db = new MVCEntities1();

        // GET: Customers/Details
        public ActionResult Index()

        {
            var CustomerList = db.Customers.Select(x => new CustomerViewModel
            {
                Address = x.Address,
                Name = x.Name,
                ID = x.ID
            }).ToList();
            return View(CustomerList);

        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CustomerList = db.Customers.Where(x => x.ID == id).Select(y => new CustomerViewModel
            {
                Address = y.Address,
                Name = y.Name,
                ID = y.ID
            }).FirstOrDefault();
            if (CustomerList == null)
            {
                return HttpNotFound();
            }
            return View(CustomerList);
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
        public ActionResult Create([Bind(Include = "ID,Name,Address")] Customer customerModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer();
                customer.Name = customerModel.Name;
                customer.Address = customerModel.Address;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(customerModel);
        }


        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel model = new CustomerViewModel
            {
                ID = customer.ID,
                Name = customer.Name,
                Address = customer.Address
            };
            return View(model);
        }



        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address")] CustomerViewModel customerModel)
        {


            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(customerModel.ID);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                customer.Name = customerModel.Name;
                customer.Address = customerModel.Address;
                db.SaveChanges();
                return RedirectToAction("Index");


            }


            return View(customerModel);
        }



        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel model = new CustomerViewModel
            {
                ID = customer.ID,
                Name = customer.Name,
                Address = customer.Address
            };

            ViewBag.CanDelete = !customer.ProductSolds.Any();
            return View(model);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
