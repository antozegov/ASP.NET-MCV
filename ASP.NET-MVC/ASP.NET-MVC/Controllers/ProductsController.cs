using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC.Models;

namespace ASP.NET_MVC.Controllers
{
    public class ProductsController : Controller
    {
        private SQL_DatabaseEntities db = new SQL_DatabaseEntities();

        public ViewResult Index(string search)
        {
            var products = db.Products.Include(p => p.Category);
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,CategoryId,IsActive,Created,Modified")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand
                    (
                    "SET IDENTITY_INSERT dbo.Product OFF " +
                    "INSERT INTO dbo.Product (Name, Price, CategoryId, IsActive, Created, Modified) " +
                    "VALUES ('" + product.Name + "'," + product.Price + "," + product.CategoryId + "," + Convert.ToInt32(product.IsActive) + ",GETDATE(),GETDATE()) " +
                    "SET IDENTITY_INSERT dbo.Product ON"
                    );
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,CategoryId,IsActive,Created,Modified")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand
                    (
                    "SET IDENTITY_INSERT dbo.Product OFF UPDATE dbo.Product SET " +
                    "Name = '" + product.Name + "', Price = " + product.Price + ", CategoryId = " + product.CategoryId + ", isActive = " + Convert.ToInt32(product.IsActive) + ", Modified = GETDATE() " +
                    "WHERE Id = " + product.Id +
                    " SET IDENTITY_INSERT dbo.Product ON"
                    );
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
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
