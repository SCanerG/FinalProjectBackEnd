using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{

    
    public class InMemoryProductDal : IProductDal
    {
    List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
                new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
                new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
                new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 },
                new Product { ProductId = 1, CategoryId = 2, ProductName = "Bardak", UnitPrice = 5, UnitsInStock = 3 }
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete=_products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> Getall()
        {
            return new List<Product>();
        }

        public List<Product> Getall(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p=> p.CategoryId==categoryId).ToList();  

        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;  
            productToUpdate.CategoryId = product.CategoryId;  
            productToUpdate.UnitPrice= product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;


        }
    }
}
