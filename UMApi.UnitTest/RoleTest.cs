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

        //Testing GetAll
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
        //Testing GetById 
        [Fact]
        public void GetById_ExistingId_ReturnsOkResult()
        {
            var okResult = roleController.GetById(1).Result;
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]

        public void GetById_ExistingId_ReturnsItemWithId()
        {
            var okResult = roleController.GetById(1).Result as OkObjectResult;
            Assert.IsType<CreateRoleDto>(okResult.Value);
            Assert.Equal(1, (okResult.Value as CreateRoleDto).Id);
        }

        [Fact]
        public void GetById_UnknownId_ReturnsNotFound()
        {
            var notFoundResult = roleController.GetById(null).Result;
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        //Testing Create

        [Fact]
        public void Create_FedValidItem_CreatesNewItem()
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

        public void Create_GivenInvalidItem_ReturnsBadRequest()
        {
            var newRole = new CreateRoleDto(){ 
                Id = 5
            };
            roleController.ModelState.AddModelError("RoleName", "Required");
            var badResponse = roleController.CreateRole(newRole);
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CreateRoleDto()
            {
                RoleName = "Administrator"
                
            };
            // Act
            var createdResponse = roleController.CreateRole(testItem) as CreatedAtActionResult;

            var item = createdResponse.Value as CreateRoleDto;
            // Assert
            Assert.IsType<CreateRoleDto>(item);
            Assert.Equal("Administrator", item.RoleName);
        }

        [Fact]
        public void Update_NonExistingId_ReturnsNotFound()
        {
            var itemtoUpdate = new CreateRoleDto()
            {
                Id = 10,
                RoleName = "RolewithId10"
            };
            var notFoundResponse = roleController.Update(itemtoUpdate.Id, itemtoUpdate);
            Assert.IsType<NotFoundResult>(notFoundResponse);
        }

        [Fact]
        public void Update_ExistingId_ReturnsNoContent()
        {
            var itemtoUpdate = new CreateRoleDto()
            {
                Id = 1,
                RoleName = "RoleisUpdated"
            };
            var Response = roleController.Update(itemtoUpdate.Id, itemtoUpdate);
            Assert.IsType<NoContentResult>(Response);
        }
        [Fact]
        public void Update_IncompleteObject_ReturnsNoContent()
        {
            var itemtoUpdate = new CreateRoleDto()
            {
                Id = 1
                
            };
            var notFoundResponse = roleController.Update(itemtoUpdate.Id, itemtoUpdate);
            Assert.IsType<NoContentResult>(notFoundResponse);
        }
        [Fact]
        public void Delete_ExistingId_ReturnsOk()
        {
            var delResponse = roleController.Delete(1);
            Assert.IsType<OkObjectResult>(delResponse);
        }
        [Fact]
        public void Delete_ExistingId_ReturnsOkDeleted()
        {
            var delResponse = roleController.Delete(1) as OkObjectResult;
            Assert.Equal("Deleted", delResponse.Value);
        }
        [Fact]
        public void Delete_NonexistingId_ReturnsNotFound()
        {
            var delResponse = roleController.Delete(100);
            Assert.IsType<NotFoundResult>(delResponse);
        }
    }
}
