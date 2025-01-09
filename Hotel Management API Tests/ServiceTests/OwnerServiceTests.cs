using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Hotel_Management_API.Data.DBContexts;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Resources;
using Hotel_Management_API.Entities;
using Hotel_Management_API.Exceptions;
using Hotel_Management_API.Repositories;
using Hotel_Management_API.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hotel_Management_API_Tests.ServiceTests
{
    [TestFixture]
    public class OwnerServiceTests
    {
        private PostOwnerRequest postOwnerRequest;
        private PutOwnerRequest putOwnerRequest;
        private OwnerService ownerService;
        private HotelDBContext hotelDBContext;
        private IOwnerRepository ownerRepository;

        [SetUp]
        public void Setup()
        {
            postOwnerRequest = TestUtilities.OwnerTestUtilities.SetupPostOwnerRequest();
            putOwnerRequest = TestUtilities.OwnerTestUtilities.SetupPutOwnerRequest();
            var options = new DbContextOptionsBuilder<HotelDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            hotelDBContext = new HotelDBContext(options);
            ownerRepository = new OwnerRepository(hotelDBContext);
            ownerService = new OwnerService(ownerRepository);
        }

        [Test]
        public async Task Given_A_Valid_PostOwnerRequest_And_An_Owner_Service_When_Post_Owner_Is_Invoked_On_The_Service_Then_Should_Invoke_Add_On_The_Db_Context_And_Save()
        {
            //Arrange
            var existingOwner = new Owner
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@yahoo.com"
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.SaveChangesAsync();

            //Act
            var result = await ownerService.ProcessPostOwnerRequest(postOwnerRequest);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(typeof(OwnerResource).IsAssignableTo(result.GetType()));
        }

        [Test]
        public async Task Given_A_Valid_PostOwnerRequest_And_An_Existing_Owner_When_PostOwner_Is_Invoked_On_The_Service_Then_Should_Throw_Duplicate_Exception()
        {
            //Arrange
            var existingOwner = new Owner
            {
                Id = 1,
                FirstName = postOwnerRequest.FirstName,
                LastName = postOwnerRequest.LastName,
                Email = postOwnerRequest.Email
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.SaveChangesAsync();

            //Act
            //Assert
            Assert.ThrowsAsync<DuplicateRequestException>(async () => await ownerService.ProcessPostOwnerRequest(postOwnerRequest));
        }

        [Test]
        public async Task Given_A_Valid_PutOwnerRequest_With_A_Non_Existing_Owner_When_ProcessPutOwner_Is_Invoked_On_The_Service_Then_Should_Throw_BadRequest_Exception()
        {
            //Arrange
            //Act
            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => await ownerService.ProcessPutOwnerRequest(5L, putOwnerRequest));
        }

        [Test]
        public async Task Given_A_Valid_PutOwnerRequest_With_An_Existing_Owner_When_ProcessPutOwner_Is_Invoked_On_The_Service_Then_Should_Update_The_Owner_And_Save_Changes()
        {
            //Arrange
            var existingOwner = new Owner
            {
                Id = 1L,
                FirstName = putOwnerRequest.FirstName,
                LastName = putOwnerRequest.LastName,
                Email = putOwnerRequest.Email
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.SaveChangesAsync();

            //Act
            var result = await ownerService.ProcessPutOwnerRequest(existingOwner.Id, putOwnerRequest);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Given_An_Existing_Owner_When_GetOwner_With_The_Id_Is_Invoked_On_The_Service_Then_Should_Return_Owner_With_That_Id()
        {
            //Arrange
            var existingOwner = new Owner
            {
                Id = 1L,
                FirstName = putOwnerRequest.FirstName,
                LastName = putOwnerRequest.LastName,
                Email = putOwnerRequest.Email
            };
            await hotelDBContext.AddAsync(existingOwner);
            await hotelDBContext.SaveChangesAsync();

            //Act
            var result = await ownerService.GetOwnerAsync(existingOwner.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.GetType().IsAssignableTo(typeof(OwnerResource)));
        }

        [Test]
        public async Task Given_An_NonExisting_Owner_When_GetOwner_With_The_Id_Is_Invoked_On_The_Service_Then_Should_Throw_Not_Found_Exception()
        {
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async() => await ownerService.GetOwnerAsync(1L));
        }
    }
}