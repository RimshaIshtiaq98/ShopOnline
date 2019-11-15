using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Core.Models;
using ShopOnline.Core.ViewModel;
using ShopOnline.DataAccess.InMemory;

namespace ShopOnline.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> repository;
        //  ProductRepository repository;
        InMemoryRepository<ProductCategory> product_categories;

        //Initalize our products repository 
        public ProductManagerController()
        {
            repository = new InMemoryRepository<Product>();
            product_categories = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //Get all product list from repository and save in a list named as products
            List<Product> products = repository.Collection().ToList();
            return View(products);
        }

        //Takes Product info input from form
        public ActionResult Create()
        {
            ProductManagerViewModel PMViewModel = new ProductManagerViewModel();
            PMViewModel.Product = new Product();
            PMViewModel.ProductCategories = product_categories.Collection();
            return View(PMViewModel);
        }

        //Check validation and add product in local memory
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else 
            {
                repository.Insert(product);
                repository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = repository.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else 
            {
                ProductManagerViewModel PMViewModel = new ProductManagerViewModel();
                PMViewModel.Product = new Product();
                PMViewModel.ProductCategories = product_categories.Collection();
                return View(PMViewModel);
            }
        }
        //default template returnupdated product and id of the original product
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product producToUpdate = repository.Find(Id);
            if (producToUpdate == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                producToUpdate.Category = product.Category;
                producToUpdate.Description = product.Description;
                producToUpdate.Image = product.Image;
                producToUpdate.Name = product.Name;
                producToUpdate.Price = product.Price;

                repository.Commit();
                return RedirectToAction("Index");
            }
        }

        //Delete Product
        public ActionResult Delete(string Id)
        {
            Product ProductToDelete = repository.Find(Id);

            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else 
            {
                return View(ProductToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete( string Id)
        {

            Product ProductToDelete = repository.Find(Id);

            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                repository.Delete(Id);
                repository.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}