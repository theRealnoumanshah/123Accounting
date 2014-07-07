using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123Accounting.Models;
using _123Accounting.DAL;

namespace _123Accounting.Controllers
{
    public class AccountingController : Controller
    {
        private AccountingContext db = new AccountingContext();

        //
        // GET: /Accounting/

        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Product);
            return View(orders.ToList());
        }

        //
        // GET: /Accounting/Details/5

        public ActionResult Details(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // GET: /Accounting/Create

        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        //
        // POST: /Accounting/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            return View(order);
        }

        //
        // GET: /Accounting/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            return View(order);
        }

        //
        // POST: /Accounting/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", order.ProductID);
            return View(order);
        }

        //
        // GET: /Accounting/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Accounting/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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