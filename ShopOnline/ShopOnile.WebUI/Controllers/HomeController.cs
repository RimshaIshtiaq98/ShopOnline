using ShopOnline.Core.Contracts;
using ShopOnline.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> repository;
        IRepository<ProductCategory> product_categories;

        //Initalize our products repository 
        public HomeController(IRepository<Product> ProductContext, IRepository<ProductCategory> ProductCategoryContext)
        {
            //repository = new InMemoryRepository<Product>();
            //product_categories = new InMemoryRepository<ProductCategory>();
            repository = ProductContext;
            product_categories = ProductCategoryContext;

        }
        public ActionResult Index()
        {
            List<Product> products = repository.Collection().ToList();
            return View(products); 
        }

        public ActionResult Details(string Id)
        {
            Product product = repository.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else {
                return View(product);
            }
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
    }
}