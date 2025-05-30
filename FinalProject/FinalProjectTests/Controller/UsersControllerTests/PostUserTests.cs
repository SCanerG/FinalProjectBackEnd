using Business.Abstract;
using Business.BusinessRules;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using FinalProjectTests.Constants;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace FinalProjectTests.Controller.UsersControllerTests
{
    public class PostUserTests
    {
        [Fact]
        public void Login_AuthController_ReturnsBadRequest()
        {
            var mockService = new Mock<IAuthService>();
            var mockBusinessRules = new Mock<IUserBusinessRules>();
            var controller = new AuthController(mockService.Object,mockBusinessRules.Object);
            var userForLoginDto= UserSamples.LoginDefault;
            mockService.Setup(s => s.Login(It.IsAny<UserForLoginDto>()))
                   .Returns(new ErrorDataResult<User>(UserSamples.DefaultUser, Messages.UserNotFound));


            var result = controller.Login(userForLoginDto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal("Kullanıcı bulunamadı", badRequest.Value);

        }


        [Fact]
        public void Login_AuthController_ReturnsOkRequest()
        {
            var mockService = new Mock<IAuthService>();
            var mockBusinessRules = new Mock<IUserBusinessRules>();
            var controller = new AuthController(mockService.Object, mockBusinessRules.Object);
            var userForLoginDto = UserSamples.LoginDefault;
            mockService.Setup(s => s.Login(It.IsAny<UserForLoginDto>()))
     .Returns(new SuccessDataResult<User>(
         UserSamples.DefaultUser,
         Messages.SuccessfulLogin
     ));

            mockService.Setup(s => s.CreateAccessToken(It.IsAny<User>()))
                .Returns(new SuccessDataResult<AccessToken>(
                    new AccessToken
                    {
                        Token = "fake-jwt-token",
                        Expiration = DateTime.Now.AddHours(1)
                    },
                    "Access token başarıyla oluşturuldu"
                ));


            var result = controller.Login(userForLoginDto);

            var okRequest = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okRequest.StatusCode);
            var dataResult = Assert.IsType<SuccessDataResult<AccessToken>>(okRequest.Value);
            Assert.Equal(Messages.AccessTokenCreated, dataResult.Message);

        }
    }
}
