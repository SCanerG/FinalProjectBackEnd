using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests.Constants
{
    public class ProductSamples
    {
       public static Product Default => new Product
    {
        ProductName = "Kalem",
        CategoryId = 2,
        UnitPrice = 5,
        UnitsInStock = 20
    };

    public static Product WithZeroStock => new Product
    {
        ProductName = "Boş Kalem",
        CategoryId = 3,
        UnitPrice = 10,
        UnitsInStock = 0
    };

    public static Product ExpensiveProduct => new Product
    {
        ProductName = "Lüks Kalem",
        CategoryId = 2,
        UnitPrice = 999,
        UnitsInStock = 5
    };
        public static List<Product> GetAllSamples() => new List<Product>
             {
        Default,
        WithZeroStock,
        ExpensiveProduct
    };
        public static Product Product1OfCategory1 => new Product
        {
            ProductName = "Ayna",
            CategoryId = 1,
            UnitPrice = 5,
            UnitsInStock = 20
        }; public static Product Product2OfCategory1 => new Product
        {
            ProductName = "Ayva",
            CategoryId = 1,
            UnitPrice = 5,
            UnitsInStock = 20
        };
        public static List<Product> GetByCategory(int categoryId) => new List<Product>
        {
Product1OfCategory1,
Product2OfCategory1
        };
            }
}
