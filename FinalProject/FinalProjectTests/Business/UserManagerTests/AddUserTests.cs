using Business.Abstract;
using Business.BusinessRules;
using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FinalProjectTests.Constants;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests.Business.UserManagerTests
{
    public class AddUserTests
    {

        //Add_FailsBusinessRule_ShouldReturnError
        public static IEnumerable<object[]> AddUserFailingRules =>
    new List<object[]>
    {
        new object[] { "UserExists", Messages.UserAlreadyExists },
        new object[] { "CheckIfPasswordLenghtLessThan", Messages.PasswordLengthError }
    };
        [Theory]
        [MemberData(nameof(AddUserFailingRules))]
        public void Register_AuthManager_ShouldReturnError(string failingRule, string expectedMessage)
        {
        var mockUserService = new Mock<IUserService>();
            var mockTokenHelper = new Mock<ITokenHelper>();
            var mockRules = new Mock<IUserBusinessRules>(MockBehavior.Strict);
            var user = UserSamples.PasswordLenght4;
           mockRules.Setup(x => x.UserExists(It.IsAny<string>())).Returns(new SuccessResult());

            mockRules.Setup(x => x.CheckIfPasswordLenghtLessThan(It.IsAny<string>())).Returns(new SuccessResult());

            switch (failingRule)
            {

                case nameof(IUserBusinessRules.CheckIfPasswordLenghtLessThan):
                    mockRules.Setup(x => x.CheckIfPasswordLenghtLessThan(user.Password))
                             .Returns(new ErrorResult(expectedMessage));
                    break;

                case nameof(IUserBusinessRules.UserExists):
                    mockRules.Setup(x => x.UserExists(user.Email))
                             .Returns(new ErrorResult(expectedMessage));
                    break;

                
            }
            var AuthManager = new AuthManager(
              mockUserService.Object,
              mockTokenHelper.Object,
              mockRules.Object
          );
            // Act
            var result = AuthManager.Register(user,user.Password);
            Assert.False(result.Success);
            Assert.Equal(expectedMessage, result.Message);
            mockUserService.Verify(x => x.Add(It.IsAny<User>()), Times.Never); // Çünkü eklenmemeli


        }
        [Fact]
        public void Register_AuthManager_ShouldReturnSuccess()
        {
            var mockUserService = new Mock<IUserService>();
            var mockTokenHelper = new Mock<ITokenHelper>();
            var mockRules = new Mock<IUserBusinessRules>(MockBehavior.Strict);
            var user = UserSamples.RegisterDefault;
            mockRules.Setup(x => x.CheckIfPasswordLenghtLessThan(It.IsAny<string>()))
         .Returns(new SuccessResult());
            mockRules.Setup(x => x.UserExists(It.IsAny<string>())).Returns(new SuccessResult());



            var AuthManager = new AuthManager(
              mockUserService.Object,
              mockTokenHelper.Object,
              mockRules.Object
          );
            // Act
            var result = AuthManager.Register(user, user.Password);
            Assert.True(result.Success);
            Assert.Equal(Messages.UserRegistered, result.Message);
            mockUserService.Verify(x => x.Add(It.IsAny<User>()), Times.Once);


        }
    }
}
