using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMApi.Controllers;
using UMApi.Dtos;
using UMApi.Helpers;
using UMApi.Models;
using UMApi.Services;
using Xunit;

namespace UMApi.UnitTest
{
    public class UserTest
    {
        private UserController userController;
        private IUserService _userService;

        public UserTest()
        {
            AppSettings appSettings = new AppSettings() { Secret = "MyTestSMyTestSecretKeyecretKeyMyTestSecretKeyMyTestSecretKeyMyTestSecretKey" };
            IOptions<AppSettings> options = Options.Create(appSettings);
            var mockUserService = new Mock<IUserService>();
            IList<User> userList = new List<User>()
            {
                new User
                {
                    Id = 1,
                    RoleId =1,
                    FirstName = "Seleena",
                    LastName = "Tandukar",
                    Username ="Seleenat",
                    Password = "Password"
                },
                   new User
                {
                    Id = 2,
                    RoleId =1,
                    FirstName = "Seleena",
                    LastName = "Tandukar",
                    Username = "Seleenatandukar",
                    Password = "Password"
                }
            };
            mockUserService.Setup(s => s.GetAll()).Returns(userList.ToList());
            mockUserService.Setup(s => s.GetById(It.IsAny<int>()))
                           .Returns((int i) => userList.Where(us => us.Id == i).FirstOrDefault());
            mockUserService.Setup(s => s.Create(It.IsAny<User>(), It.IsAny<string>())).Verifiable();
            mockUserService.Setup(s => s.Update(It.IsAny<User>())).Verifiable();
            mockUserService.Setup(s => s.Delete(It.IsAny<int>())).Verifiable();

            userController = new UserController(mockUserService.Object, AutomapperSingleton.Mapper, options);
        }


        [Fact]
        public void GetAll_WhenCalled_ReturnsOk()
        {
            var userResponse = userController.GetAll();
            Assert.IsType<OkObjectResult>(userResponse.Result);
        }
        [Fact]
        public void GetAll_Called_ReturnsList()
        {
            var userResponse = userController.GetAll().Result as OkObjectResult;
            Assert.IsType<List<ReadUserDto>>(userResponse.Value);          

        }
        [Fact]
        public void GetAll_Called_ReturnsListoftwo()
        {
            var userResponse = userController.GetAll().Result as OkObjectResult;
            var items = Assert.IsType<List<ReadUserDto>>(userResponse.Value);
            Assert.Equal(2, items.Count);
            
        }

        [Fact]
        public void Create_AddNewUser_ReturnsCreated()
        {
            var newUser = new CreateUserDto()
            {

                RoleId = 1,
                FirstName = "Seleena2",
                LastName = "Tandukar2",
                Username = "Seleenatandukar2",
                Password = "Password"
            };
            var createResponse = userController.CreateUser(newUser);
            Assert.IsType<CreatedAtActionResult>(createResponse);
        }
        [Fact]
        public void Create_AddNewUser_ReturnsCreatedItem()
        {
            var newUser = new CreateUserDto()
            {

                RoleId = 1,
                FirstName = "Seleena2",
                LastName = "Tandukar2",
                Username = "Seleenatandukar2",
                Password = "Password"
            };
            var createResponse = userController.CreateUser(newUser) as CreatedAtActionResult;
            Assert.IsType<ReadUserDto>(createResponse.Value);
        }
    }
}
