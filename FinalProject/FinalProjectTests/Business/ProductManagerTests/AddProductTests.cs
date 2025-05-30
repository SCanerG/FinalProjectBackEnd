using Business.Abstract;
using Business.BusinessRules;
using Business.Concrete;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FinalProjectTests.Constants;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests.Business.ProductManagerTests
{
 public class AddProductTests
{
    
        public static IEnumerable<object[]> AddProductFailingRules =>
    new List<object[]>
    {
        new object[] { "CheckIfProductNameExists", Messages.ProductNameAlreadyExists },
        new object[] { "CheckIfProductCountOfCategoryCorrect", Messages.ProductCountOfCategoryError },
        new object[] { "CheckIfCategoryLimitExceeded", Messages.CategoryLimitExceded }
    };

        [Theory]
        [MemberData(nameof(AddProductFailingRules))]
        //[InlineData("CheckIfCategoryLimitExceded", "Kategori limiti aşıldı.")]
        public void Add_ProductManager_ShouldReturnError(string failingRule, string expectedMessage)
        {
            // Arrange
            var mockProductDal = new Mock<IProductDal>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockRules = new Mock<IProductBusinessRules>(MockBehavior.Strict);
            var product = ProductSamples.WithZeroStock;

            mockRules.Setup(x => x.CheckIfProductNameExists(It.IsAny<string>())).Returns(new SuccessResult());
            mockRules.Setup(x => x.CheckIfProductCountOfCategoryCorrect(It.IsAny<int>())).Returns(new SuccessResult());
            mockRules.Setup(x => x.CheckIfCategoryLimitExceeded()).Returns(new SuccessResult());
          
            switch (failingRule)
            {
               
                case nameof(IProductBusinessRules.CheckIfProductNameExists):
                    mockRules.Setup(x => x.CheckIfProductNameExists(product.ProductName))
                             .Returns(new ErrorResult(expectedMessage));
                    break;

                case nameof(IProductBusinessRules.CheckIfProductCountOfCategoryCorrect):
                    mockRules.Setup(x => x.CheckIfProductCountOfCategoryCorrect(product.CategoryId))
                             .Returns(new ErrorResult(expectedMessage));
                    break;

                case nameof(IProductBusinessRules.CheckIfCategoryLimitExceeded):
                    mockRules.Setup(x => x.CheckIfCategoryLimitExceeded())
                             .Returns(new ErrorResult(expectedMessage));
                    break;
            }

            var productManager = new ProductManager(
                mockProductDal.Object,
                mockCategoryService.Object,
                mockRules.Object
            );

            // Act
            var result = productManager.Add(product);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(expectedMessage, result.Message);
            mockProductDal.Verify(x => x.Add(It.IsAny<Product>()), Times.Never); // Veri eklenmemeli
        }

        [Fact]
        public void Add_ProductManager_ShouldReturnSuccess()
        {
            // Arrange
            var mockProductDal = new Mock<IProductDal>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockRules = new Mock<IProductBusinessRules>(MockBehavior.Strict);
            var product = ProductSamples.Default;

            mockRules.Setup(x => x.CheckIfProductNameExists(product.ProductName)).Returns(new SuccessResult());
            mockRules.Setup(x => x.CheckIfProductCountOfCategoryCorrect(product.CategoryId)).Returns(new SuccessResult());
            mockRules.Setup(x => x.CheckIfCategoryLimitExceeded()).Returns(new SuccessResult());

            var productManager = new ProductManager(
                mockProductDal.Object,
                mockCategoryService.Object,
                mockRules.Object
            );

            // Act
            var result = productManager.Add(product);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.ProductAdded, result.Message);
            mockProductDal.Verify(x => x.Add(It.IsAny<Product>()), Times.Once); // Veri eklenmiş olmalı
        }
    }
}
