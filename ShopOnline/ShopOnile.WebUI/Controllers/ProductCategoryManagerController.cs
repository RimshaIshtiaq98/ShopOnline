using ShopOnline.Core.Models;
using ShopOnline.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnile.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository Categoryrepository;


        //Initalize our products repository 
        public ProductCategoryManagerController()
        {
            Categoryrepository = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //Get all product list from repository and save in a list named as products
            List<ProductCategory> productCategories = Categoryrepository.Collection().ToList();
            return View(productCategories);
        }

        //Takes Product info input from form
        public ActionResult Create()
        {
            ProductCategory productCategories = new ProductCategory();
            return View(productCategories);
        }

        //Check validation and add product in local memory
        [HttpPost]
        public ActionResult Create(ProductCategory productCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategories);
            }
            else
            {
                Categoryrepository.Insert(productCategories);
                Categoryrepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategories = Categoryrepository.Find(Id);
            if (productCategories == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategories);
            }
        }
        //default template returnupdated product and id of the original product
        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productCategoriesToUpdate = Categoryrepository.Find(Id);
            if (productCategoriesToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productCategoriesToUpdate.Category = product.Category;
               

                Categoryrepository.Commit();
                return RedirectToAction("Index");
            }
        }

        //Delete Product
        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoriesToDelete = Categoryrepository.Find(Id);

            if (productCategoriesToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoriesToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {

            ProductCategory productCategoriesToDelete = Categoryrepository.Find(Id);

            if (productCategoriesToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Categoryrepository.Delete(Id);
                Categoryrepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}