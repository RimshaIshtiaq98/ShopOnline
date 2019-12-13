using ShopOnline.Core.Contracts;
using ShopOnline.Core.Models;
using ShopOnline.Core.ViewModel;
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
            repository = ProductContext;
            product_categories = ProductCategoryContext;

        }
        public ActionResult Index(string Category=null)
        {
            List<Product> products ;
            List<ProductCategory> P_category = product_categories.Collection().ToList();
            if (Category == null)
            {
                products = repository.Collection().ToList();
            }
            else {
                products = repository.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = P_category;
            return View(model); 
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