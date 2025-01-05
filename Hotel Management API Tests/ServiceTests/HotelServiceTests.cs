using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Models;
using Hotel_Management_API.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hotel_Management_API_Tests.ServiceTests
{
    [TestFixture]
    public class HotelServiceTests
    {
        private PostHotelRequest postHotelRequest;
        private PutHotelRequest putHotelRequest;
        private HotelService HotelService;
        private HotelDBContext hotelDBContext;
        private OwnerService ownerService;

        [SetUp]
        public void Setup()
        {
            postHotelRequest = TestUtilities.HotelTestUtilities.SetupPostHotelRequest();
            putHotelRequest = TestUtilities.HotelTestUtilities.SetupPutHotelRequest();
            var options = new DbContextOptionsBuilder<HotelDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            hotelDBContext = new HotelDBContext(options);
            ownerService = new OwnerService(hotelDBContext);
            HotelService = new HotelService(hotelDBContext, ownerService);
        }

        [Test]
        public async Task Given_A_Valid_PostHotelRequest_And_An_Hotel_Service_When_Post_Hotel_Is_Invoked_On_The_Service_Then_Should_Invoke_Add_On_The_Db_Context_And_Save()
        {
            //Arrange
            var existingOwner = new Owner
            {
                Id = postHotelRequest.OwnerId,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@yahoo.com"
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.SaveChangesAsync();
            //Act
            var result = await HotelService.ProcessPostHotelRequest(postHotelRequest);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(typeof(HotelResource).IsAssignableTo(result.GetType()));
        }

        [Test]
        public async Task Given_A_Valid_PostHotelRequest_And_An_Existing_Hotel_When_PostHotel_Is_Invoked_On_The_Service_Then_Should_Throw_Duplicate_Exception()
        {
            //Arrange
            var existingHotel = new Hotel
            {
                Name = postHotelRequest.Name,
                OwnerId = postHotelRequest.OwnerId,
                Address = postHotelRequest.Address,
                State = postHotelRequest.State,
                StarRating = 4,
                City = "Ibadan",
                PostalCode = "28493"
            };
            var existingOwner = new Owner
            {
                Id = postHotelRequest.OwnerId,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@yahoo.com"
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.AddAsync(existingHotel);
            await hotelDBContext.SaveChangesAsync();

            //Act
            //Assert
            Assert.ThrowsAsync<DuplicateRequestException>(async () => await HotelService.ProcessPostHotelRequest(postHotelRequest));
        }

        [Test]
        public async Task Given_A_Valid_PutHotelRequest_With_A_Non_Existing_Hotel_When_ProcessPutHotel_Is_Invoked_On_The_Service_Then_Should_Throw_BadRequest_Exception()
        {
            //Arrange
             var existingOwner = new Owner
            {
                Id = postHotelRequest.OwnerId,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@yahoo.com"
            };
            await hotelDBContext.AddAsync(existingOwner);
            //Act
            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => await HotelService.ProcessPutHotelRequest(putHotelRequest, 5L));
        }

        [Test]
        public async Task Given_A_Valid_PutHotelRequest_With_An_Existing_Hotel_When_ProcessPutHotel_Is_Invoked_On_The_Service_Then_Should_Update_The_Hotel_And_Save_Changes()
        {
            //Arrange
            var existingHotel = new Hotel
            {
                Id = 5L,
                Name = postHotelRequest.Name,
                OwnerId = postHotelRequest.OwnerId,
            };
            await hotelDBContext.AddAsync(existingHotel);
            await hotelDBContext.SaveChangesAsync();

            //Act
            var result = await HotelService.ProcessPutHotelRequest(putHotelRequest, existingHotel.Id);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Given_An_Existing_Hotel_When_GetHotel_With_The_Id_Is_Invoked_On_The_Service_Then_Should_Return_Hotel_With_That_Id()
        {
            //Arrange
            var existingHotel = new Hotel
            {
                Id = 1L,
                Name = postHotelRequest.Name,
                OwnerId = postHotelRequest.OwnerId,
            };
            await hotelDBContext.AddAsync(existingHotel);
            await hotelDBContext.SaveChangesAsync();

            //Act
            var result = await HotelService.GetHotelAsync(existingHotel.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.GetType().IsAssignableTo(typeof(HotelResource)));
        }

        [Test]
        public async Task Given_An_NonExisting_Hotel_When_GetHotel_With_The_Id_Is_Invoked_On_The_Service_Then_Should_Throw_Not_Found_Exception()
        {
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await HotelService.GetHotelAsync(1L));
        }
    }
}
