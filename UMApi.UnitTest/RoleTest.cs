using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UMApi.Controllers;
using UMApi.Dtos;
using UMApi.Helpers;
using UMApi.Models;
using UMApi.Models.Menus;
using UMApi.Services;

using Xunit;

namespace UMApi.UnitTest
{
    //Arrange
    public class RoleTest
    {
        private RoleController roleController;
        private IRoleService _roleService;

        public RoleTest()
        {
            AppSettings appSettings = new AppSettings() { Secret = "MyTestSMyTestSecretKeyecretKeyMyTestSecretKeyMyTestSecretKeyMyTestSecretKey" };
            IOptions<AppSettings> options = Options.Create(appSettings);
            _roleService = new MockRoleService();     
            roleController = new RoleController(_roleService, AutomapperSingleton.Mapper, options);
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

        [Fact]
        public void Add_WhenFedValidItem_CreatesNewItem()
        {
            //Act
            CreateRoleDto newRole = new CreateRoleDto
            {
               
                RoleName = "Guest4"

            };
            var createdResponse = roleController.CreateRole(newRole);
            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }
        [Fact]

        public void Add_WhenGivenInvalidItem_ReturnsBadRequest()
        {
            var newRole = new CreateRoleDto { 
                Id = 5
            };
            roleController.ModelState.AddModelError("RoleName", "Required");
            var badResponse = roleController.CreateRole(newRole);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
