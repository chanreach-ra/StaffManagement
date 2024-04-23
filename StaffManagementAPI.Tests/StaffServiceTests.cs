using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using StaffManagementAPI.Data;
using StaffManagementAPI.DTOs;
using StaffManagementAPI.Exceptions;
using StaffManagementAPI.Models;
using StaffManagementAPI.Services;
using Xunit;

namespace StaffManagementAPI.Tests
{
    public class StaffServiceTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        public StaffServiceTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task PostStaffAsync_ValidData_ReturnsCorrectResult()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var service = new StaffService(dbContext, mapper);
            var staffDTO = new StaffDTO
            {
                StaffID = "S0000001",
                FullName = "John Doe",
                Gender = GenderType.Male,
                Birthday = new DateTime(1990, 1, 1)
            };

            // Act
            var result = await service.PostStaffAsync(staffDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetStaffDTO>(result);
        }

        [Fact]
        public async Task PostStaffAsync_DuplicateStaffID_ThrowsException()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            dbContext.Staff.Add(new Staff
            {
                StaffID = "S0000001",
                FullName = "Sam Arth",
                Gender = GenderType.Male,
                Birthday = new DateTime(1992, 1, 1)
            });
            await dbContext.SaveChangesAsync();
            var service = new StaffService(dbContext, mapper);
            var staffDTO = new StaffDTO
            {
                StaffID = "S0000001", // Existing StaffID
                FullName = "Sam Arth",
                Gender = GenderType.Male,
                Birthday = new DateTime(1992, 1, 1)
            };

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(async () => await service.PostStaffAsync(staffDTO));
        }

        [Fact]
        public async Task PutStaffAsync_ExistingId_ValidData_ReturnsCorrectResult()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var existingStaff = await dbContext.Staff.FirstOrDefaultAsync(e => e.IsDeleted == false && e.StaffID == "S0000001");
            var service = new StaffService(dbContext, mapper);
            var staffDTO = new StaffDTO
            {
                StaffID = "S0000001",
                FullName = "Sam Arth Sokha",
                Gender = GenderType.Male,
                Birthday = new DateTime(1992, 1, 1)
            };

            // Act
            var result = await service.PutStaffAsync(existingStaff.Id, staffDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetStaffDTO>(result);
        }

        [Fact]
        public async Task PutStaffAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var existingStaff = await dbContext.Staff.FirstOrDefaultAsync(e => e.IsDeleted == false && e.StaffID == "S0000001");
            var service = new StaffService(dbContext, mapper);
            var staffDTO = new StaffDTO
            {

                FullName = "Test Error", // Updated name
            };

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(async () => await service.PutStaffAsync(999, staffDTO));
        }

        [Fact]
        public async Task DeleteStaffAsync_ExistingId_ReturnsCorrectResult()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var existingStaff = new Staff
            {
                StaffID = "S0000001",
                FullName = "John Doe",
                Gender = GenderType.Male,
                Birthday = new DateTime(1994, 1, 1)
            };
            dbContext.Staff.Add(existingStaff);
            await dbContext.SaveChangesAsync();

            var service = new StaffService(dbContext, mapper);
            // Act
            var result = await service.DeleteStaffAsync(existingStaff.Id);

            // Assert
            Assert.Null(result);
            Assert.True(existingStaff.IsDeleted);
        }

        [Fact]
        public async Task DeleteStaffAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var service = new StaffService(dbContext, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(async () => await service.DeleteStaffAsync(999));
        }

        [Fact]
        public async Task GetStaffByIdAsync_ExistingId_ReturnsCorrectResult()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var existingStaff = new Staff
            {
                StaffID = "S1234567",
                FullName = "John Doe",
                Gender = GenderType.Male,
                Birthday = new DateTime(1990, 1, 1)
                // Add other properties as needed
            };
            dbContext.Staff.Add(existingStaff);
            await dbContext.SaveChangesAsync();
            var service = new StaffService(dbContext, mapper);

            // Act
            var result = await service.GetStaffByIdAsync(existingStaff.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetStaffDTO>(result);
            Assert.Equal(existingStaff.Id, result.Id);
        }

        [Fact]
        public async Task GetStaffByIdAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            var service = new StaffService(dbContext, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<CustomException>(async () => await service.GetStaffByIdAsync(999));
        }

        [Fact]
        public async Task GetStaffAsync_ValidCriteria_ReturnsCorrectResult()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(mapperConfig);
            using var dbContext = new AppDbContext(_dbContextOptions);
            dbContext.Staff.Add(new Staff
            {
                StaffID = "S1234567",
                FullName = "John Doe",
                Gender = GenderType.Male,
                Birthday = new DateTime(1990, 1, 1)
                // Add other properties as needed
            });
            await dbContext.SaveChangesAsync();
            var service = new StaffService(dbContext, mapper);
            var searchCriteria = new SearchStaffDTO
            {
                StaffID = "S1234567",
                Gender = 0
            };

            // Act
            var result = await service.GetStaffAsync(searchCriteria);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<GetStaffDTO>>(result);
        }
    }
}
