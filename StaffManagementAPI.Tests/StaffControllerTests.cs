using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using StaffManagementAPI.Controllers;
using StaffManagementAPI.DTOs;
using StaffManagementAPI.Models;
using StaffManagementAPI.Services;

namespace StaffManagementAPI.Tests;

public class StaffControllerTests
{
    [Fact]
    public async Task PostStaffAsync_ValidData_ReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<IStaffService>();
        var logger = Mock.Of<ILogger<StaffController>>();
        var controller = new StaffController(mockService.Object, logger);
        var staffDTO = new StaffDTO
        {
            StaffID = "ID000001",
            FullName = "Ra Chanreach",
            Gender = GenderType.Male,
            Birthday = DateTime.Now.AddYears(24),
        };

        // Act
        var result = await controller.PostStaffAsync(staffDTO) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
    [Fact]
    public async Task GetStaffByIdAsync_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<IStaffService>();
        var logger = Mock.Of<ILogger<StaffController>>();
        var controller = new StaffController(mockService.Object, logger);
        var id = 1; // Assuming this ID exists in the database

        // Act
        var result = await controller.GetStaffByIdAsync(id) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
    [Fact]
    public async Task DeleteStaffAsync_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<IStaffService>();
        mockService.Setup(s => s.DeleteStaffAsync(It.IsAny<int>()));
        var logger = Mock.Of<ILogger<StaffController>>();
        var controller = new StaffController(mockService.Object, logger);
        var id = 1; // Assuming this ID exists in the database

        // Act
        var result = await controller.DeleteStaffAsync(id) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task PutStaffAsync_ExistingId_ValidData_ReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<IStaffService>();
        mockService.Setup(s => s.PutStaffAsync(It.IsAny<int>(), It.IsAny<StaffDTO>()));
        var logger = Mock.Of<ILogger<StaffController>>();
        var controller = new StaffController(mockService.Object, logger);
        var id = 1;
        var staffDTO = new StaffDTO
        {
            FullName = null,
            Gender = GenderType.Female,
            Birthday = DateTime.Now.AddYears(27)
        };

        // Act
        var result = await controller.PutStaffAsync(id, staffDTO) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task GetStaffAsync_ValidCriteria_ReturnsOkResult()
    {
        // Arrange
        var mockService = new Mock<IStaffService>();
        var logger = Mock.Of<ILogger<StaffController>>();
        var controller = new StaffController(mockService.Object, logger);
        var searchCriteria = new SearchStaffDTO
        {
            StaffID = null,
            Gender = 0,
            FromDate = null,
            ToDate = null
        };

        // Act
        var result = await controller.GetStaffAsync(searchCriteria) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}