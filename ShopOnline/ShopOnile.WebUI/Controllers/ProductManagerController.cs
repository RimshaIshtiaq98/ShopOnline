using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Core.Contracts;
using ShopOnline.Core.Models;
using ShopOnline.Core.ViewModel;
using ShopOnline.DataAccess.InMemory;

namespace ShopOnline.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> repository;
        IRepository<ProductCategory> product_categories;

        //Initalize our products repository 
        public ProductManagerController(IRepository<Product> ProductContext, IRepository<ProductCategory> ProductCategoryContext)
        {
            //repository = new InMemoryRepository<Product>();
            //product_categories = new InMemoryRepository<ProductCategory>();
            repository = ProductContext;
            product_categories = ProductCategoryContext;

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
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else 
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + product.Image));
                }
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
                PMViewModel.Product = product;
                PMViewModel.ProductCategories = product_categories.Collection();
                return View(PMViewModel);
            }
        }
        //default template returnupdated product and id of the original product
        [HttpPost]
        public ActionResult Edit(Product product, string Id,HttpPostedFileBase file)
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

                if (file != null)
                {
                    producToUpdate.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + producToUpdate.Image));
                }
                producToUpdate.Category = product.Category;
                producToUpdate.Description = product.Description;
               // producToUpdate.Image = product.Image;
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