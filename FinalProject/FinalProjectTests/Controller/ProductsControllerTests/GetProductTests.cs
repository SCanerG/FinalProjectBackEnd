using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using FinalProjectTests.Constants;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace FinalProjectTests.Controller.ProductsControllerTests
{
    public class GetProductTests
    {
        [Fact]
        public void GetAll_ProductController_ReturnsBadRequest()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            var product = ProductSamples.Default;
            mockService.Setup(s => s.GetAll())
                   .Returns(new SuccessDataResult<List<Product>>(ProductSamples.GetAllSamples(),Messages.ProductsListed));



            var result = controller.GetAll();


            var okRequest = Assert.IsType<OkObjectResult>(result);
            var dataResult = Assert.IsType<SuccessDataResult<List<Product>>>(okRequest.Value);
            Assert.Equal(200, okRequest.StatusCode);
            Assert.Equal("Ürünler başarıyla Listelendi", (dataResult.Message));

        }
        [Fact]
        public void GetById_ProductController_ReturnsBadRequest()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            var product = ProductSamples.Default;
            mockService.Setup(s => s.GetById(product.ProductId))
                   .Returns(new SuccessDataResult<Product>(ProductSamples.Default, Messages.ProductsListed));



            var result = controller.GetById(product.ProductId);

            var okRequest = Assert.IsType<OkObjectResult>(result);
            var dataResult = Assert.IsType<SuccessDataResult<Product>>(okRequest.Value);
            Assert.Equal(200, okRequest.StatusCode);
            Assert.Equal("Ürünler başarıyla Listelendi", (dataResult.Message));

        }
        [Fact]
        public void GetByCategoryId_ProductController_ReturnsBadRequest()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            var product = ProductSamples.Product1OfCategory1;
            mockService.Setup(s => s.GetAllByCategoryId(product.CategoryId))
                   .Returns(new SuccessDataResult<List<Product>>(ProductSamples.GetByCategory(product.CategoryId), Messages.ProductsListed));

            var result = controller.GetByCategory(product.CategoryId);
            var okRequest = Assert.IsType<OkObjectResult>(result);
            var dataResult = Assert.IsType<SuccessDataResult<List<Product>>>(okRequest.Value);
            Assert.Equal(200, okRequest.StatusCode);
            Assert.Equal("Ürünler başarıyla Listelendi", (dataResult.Message));


        }
    }
}
