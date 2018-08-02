using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP.NET_MVC.Models;

namespace ASP.NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MockCategories()
        {
            using (var db = new SQL_DatabaseEntities())
            {
                var NewCategoryFood = new Category
                {
                    Id = 1,
                    Name = "Food",
                };
                db.Categories.Add(NewCategoryFood);
                var NewCategoryBeverages = new Category
                {
                    Id = 2,
                    Name = "Beverages",
                };
                db.Categories.Add(NewCategoryBeverages);
                var NewCategoryOther = new Category
                {
                    Id = 3,
                    Name = "Other",
                };
                db.Categories.Add(NewCategoryOther);
                db.SaveChanges();
            }
            return View();
        }
    }
}