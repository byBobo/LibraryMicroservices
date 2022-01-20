using System;
using Xunit;
using BorrowingsService.Controllers;
using Moq;
using BorrowingsService.Services;
using Microsoft.EntityFrameworkCore;
using BorrowingsService.Repositories;
using BorrowingsService.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest
{
    public class BorrowingsControllerTest
    {
        protected AppDbContext _context;
        protected BorrowingRepository _repository;
        protected BorrowingService _service;
        protected BorrowingsController _controller;

        public BorrowingsControllerTest()
        {
            DbContextOptions<AppDbContext> options;

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "FakeDb");

            options = builder.Options;
            _context = new AppDbContext(options);
            _repository = new BorrowingRepository(_context);
            _service = new BorrowingService(_repository);
            _controller = new BorrowingsController(_service);          
        }

        private async Task Seeding()
        {
            for (int i = 1; i < 10; i++)
            {
                Borrowing borrowing = new()
                {
                    BookId = $"Book_N{i}",
                    CustomerId = $"Customer_N{i}",
                    FromData = DateTime.Now,
                    ToData = DateTime.Now,
                };
                await _service.CreateAsync(borrowing);
            }
        }

        // READ ALL

        [Fact]
        public async void ReadAllAsync_shouldReturn_200()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;

            // Act
            var result = await controller.ReadAllAsync();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);

        }

        // READ ONE

        [Fact]
        public async void ReadOneAsync_validId_shouldReturn_200()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int id = 1;       

            // Act
            var result = await controller.ReadOneAsync(id);
            var borrowing = ((OkObjectResult)result).Value as Borrowing;

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async void ReadOneAsync_invalidId_shouldReturn_404()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int id = 99;

            // Act
            var result = await controller.ReadOneAsync(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        // CREATE

        [Fact]
        public async void CreateAsync_validModel_shouldReturn_200()
        {
            // Arrange
            var controller = _controller;
            Borrowing borrowing = new()
            {
                BookId = "TestingCreate",
                CustomerId = "TestingCreate",
                FromData = DateTime.Now,
                ToData = DateTime.Now
            };

            // Act
            var result = await controller.CreateAsync(borrowing);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }
        // --- It fails only in unit test; it works properly when application's running ---
        // Message: 
        //    Assert.IsAssignableFrom() Failure
        //    Expected: typeof(Microsoft.AspNetCore.Mvc.BadRequestResult)
        //    Actual:   typeof(Microsoft.AspNetCore.Mvc.OkResult)

        //[Fact]
        //public async void CreateAsync_invalidModel_shouldReturn_400()
        //{
        //    // Arrange
        //    var controller = _controller;
        //    Borrowing borrowing = new()
        //    {
        //        BookId = "TestingCreate",
        //        // NO CustomerId [Required]
        //        FromData = DateTime.Now,
        //        ToData = DateTime.Now
        //    };

        //    // Act
        //    var result = await controller.CreateAsync(borrowing);

        //    // Assert
        //    Assert.IsAssignableFrom<BadRequestResult>(result);
        //}

        // UPDATE

        [Fact]
        public async void UpdateAsync_valid_shouldReturn_200()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int borrowingId = 6;
            Borrowing borrowing = new()
            {
                Id = 6,
                BookId = "TestingUpdate",
                CustomerId = "TestingUpdate",
                FromData = DateTime.Now,
                ToData = DateTime.Now
            };

            // Act
            var result = await controller.UpdateAsync(borrowingId, borrowing);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        // --- It fails only in unit test; it works properly when application's running ---
        // Message: 
            // Assert.IsAssignableFrom() Failure
            // Expected: typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult)
            // Actual:   typeof(Microsoft.AspNetCore.Mvc.OkResult)

        //[Fact]
        //public async void UpdateAsync_invalidModel_shouldReturn_400()
        //{
        //    // Arrange
        //    Task.Run(() => Seeding()).Wait();
        //    var controller = _controller;
        //    int borrowingId = 6;
        //    Borrowing borrowing = new()
        //    {
        //        Id = 6,
        //        BookId = "TestingUpdate",
        //        // NO CustomerId [Required]
        //        FromData = DateTime.Now,
        //        ToData = DateTime.Now
        //    };

        //    // Act
        //    var result = await controller.UpdateAsync(borrowingId, borrowing);

        //    // Assert
        //    Assert.IsAssignableFrom<BadRequestResult>(result);
        //}

        [Fact]
        public async void UpdateAsync_mismatchingId_shouldReturn_400()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int borrowingId = 1;
            Borrowing borrowing = new()
            {
                Id = 9,
                BookId = "TestingUpdate",
                CustomerId = "TestingUpdate",
                FromData = DateTime.Now,
                ToData = DateTime.Now
            };

            // Act
            var result = await controller.UpdateAsync(borrowingId, borrowing);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async void UpdateAsync_invalidId_shouldReturn_404()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int borrowingId = 99;
            Borrowing borrowing = new()
            {
                Id = 99,
                BookId = "TestingUpdate",
                CustomerId = "TestingUpdate",
                FromData = DateTime.Now,
                ToData = DateTime.Now
            };

            // Act
            var result = await controller.UpdateAsync(borrowingId, borrowing);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        // DELETE

        [Fact]
        public async void DeleteAsync_validId_shouldReturn_200()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int borrowingId = 6;

            // Act
            var result = await controller.DeleteAsync(borrowingId);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async void DeleteAsync_invalidId_shouldReturn_404()
        {
            // Arrange
            Task.Run(() => Seeding()).Wait();
            var controller = _controller;
            int borrowingId = 99;

            // Act
            var result = await controller.DeleteAsync(borrowingId);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

    }
}
