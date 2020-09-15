using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using UMApi.Controllers;
using UMApi.Dtos;
using UMApi.Helpers;
using UMApi.Models;
using UMApi.Services;

using Xunit;

namespace UMApi.UnitTest
{
    //Arrange
    public class RoleTest
    {
        private RoleController roleController;

   
        public RoleTest()
        {
            AppSettings appSettings = new AppSettings() { Secret = "MyTestSMyTestSecretKeyecretKeyMyTestSecretKeyMyTestSecretKeyMyTestSecretKey" };
            IOptions <AppSettings> options = Options.Create(appSettings);

            var itemServiceMock = new Mock<IRoleService>();
            itemServiceMock.Setup(service => service.GetAll()).Returns(
                new List<Role>
                {

                new Role
                {
                    Id = 1,
                    RoleName = "Guest1"

                },
                new Role
                {
                    Id = 2,
                    RoleName = "Guest2"

                },
                new Role
                {
                    Id = 3,
                    RoleName = "Guest3"

                }
                }
            );
            roleController = new RoleController (itemServiceMock.Object, AutomapperSingleton.Mapper, options);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = roleController.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = roleController.GetAll().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<CreateRoleDto>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
    }
}
