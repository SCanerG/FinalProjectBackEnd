using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
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
 
    public class PostProductTests
    {
       
        [Fact]
        public void Add_ProductController_ReturnsBadRequest()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            var product =  ProductSamples.Default;
            mockService.Setup(s => s.Add(It.IsAny<Product>()))
                   .Returns(new ErrorResult("Ekleme başarısız"));


            var result = controller.Add(product);

            var badRequest=Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400,badRequest.StatusCode);
            Assert.Equal("Ekleme başarısız", badRequest.Value);

        }
        [Fact]
        public void Add_ProductController_ReturnsOkRequest()
        {
            var mockService = new Mock<IProductService>();
            var controller = new ProductsController(mockService.Object);
            var product = ProductSamples.Default;
            mockService.Setup(s => s.Add(It.IsAny<Product>()))
                   .Returns(new SuccessResult(Messages.ProductAdded));


            var result = controller.Add(product);

            var okRequest = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okRequest.StatusCode);
            Assert.Equal(Messages.ProductAdded, ((SuccessResult)okRequest.Value).Message);

        }

    }
}
