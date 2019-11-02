using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using ShopOnline.Core.Models;

namespace ShopOnline.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        //Save Product in to local memory
        public void Commit()
        {
            cache["products"] = products;

        }

        //Insert Product in a list
        public void Insert(Product product)
        {
            products.Add(product);
        }

        //Update Product Information

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(c => c.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not found");
            }
            
        }

        //Find Product
        public Product Find(string Id)
        {
            Product productToFind = products.Find(c => c.Id == Id);
            if (productToFind != null)
            {
                return productToFind;
            }
            else {
                throw new Exception("Product Not Found");
            }
        
        }

        //Return List of Products
        public IQueryable<Product> Collection() 
        {
            return products.AsQueryable();
        }

        //Delete Product
        public void Delete(string Id)
        {
            Product ProductToDelete = products.Find(c => c.Id == Id);
            if (ProductToDelete != null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
            

    }
}
