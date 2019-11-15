using ShopOnline.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();
            }
        }

        //Save Product in to local memory
        public void Commit()
        {
            cache["productcategories"] = productcategories;

        }

        //Insert Product in a list
        public void Insert(ProductCategory productcat)
        {
            productcategories.Add(productcat);
        }

        //Update Product Information

        public void Update(ProductCategory productcat)
        {
            ProductCategory productCatToUpdate = productcategories.Find(c => c.Id == productcat.Id);

            if (productCatToUpdate != null)
            {
                productCatToUpdate = productcat;
            }
            else
            {
                throw new Exception("Product Category Not found");
            }

        }

        //Find Product
        public ProductCategory Find(string Id)
        {
            ProductCategory productCatToFind = productcategories.Find(c => c.Id == Id);
            if (productCatToFind != null)
            {
                return productCatToFind;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }

        }

        //Return List of Products
        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }

        //Delete Product
        public void Delete(string Id)
        {
            ProductCategory ProductCatToDelete = productcategories.Find(c => c.Id == Id);
            if (ProductCatToDelete != null)
            {
                productcategories.Remove(ProductCatToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

    }
}
