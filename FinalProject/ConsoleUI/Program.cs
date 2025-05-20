//using Business.Concrete;
//using DataAccess.Concrete.EntityFramework;

////CategoryTest();
////ProductTest();

////static void ProductTest()
////{
////    ProductManager productManager = new ProductManager(new EfProductDal());
////    var result = productManager.GetProductDetailDtos();
////    if (result.Success) { 
////    foreach (var product in result.Data)
////    {
////        Console.WriteLine(product.ProductName + " / " + product.CategoryName);
////    }
////    }
////}

////static void CategoryTest()
////{
////    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
////    foreach (var category in categoryManager.GetAll())
////    {
////        Console.WriteLine(category.CategoryName);

////    }
////}