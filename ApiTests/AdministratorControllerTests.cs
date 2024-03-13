using AutoFixture;
using BookDeliveryAPI.Controllers;
using BookDeliverySystemAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ApiTests
{
    [TestClass]
    public class AdministratorControllerTests
    {
        private Mock<IAdministratorRepository> oAdministrator;
        private Fixture _fixture;
        private AdministratorController _controller;

        public AdministratorControllerTests()
        {
            _fixture = new Fixture();
            oAdministrator = new Mock<IAdministratorRepository>();
            _controller = new AdministratorController(oAdministrator.Object);
        }

        [TestMethod]
        public void GetClients_ReturnsOkResult_WithClients()
        {
            // Arrange
            var clients = _fixture.Create<List<BookDeliveryCore.Client>>();  //using AutoFixture set up our mock repository to return this list 
            oAdministrator.Setup(repo => repo.GetClients()).Returns(clients);

            // Act
            var result = _controller.GetClients();  //call the GetClients method of the controller

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult)); //returned result is of type OkObjectResult, ensuring that the request was successful. Then, we check if the returned clients match the expected clients
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(clients, okResult.Value);
        }

        [TestMethod]
        public void GetClients_ReturnsNoContent_WhenNoClients()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetClients()).Returns((List<BookDeliveryCore.Client>)null);

            // Act
            var result = _controller.GetClients();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GetClients_ReturnsBadRequest_OnException()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetClients()).Throws(new Exception("Test Exception"));

            // Act
            var result = _controller.GetClients();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Test Exception", badRequestResult.Value);
        }

        [TestMethod]
        public void GetCouriers_ReturnsOkResult_WithCouriers()
        {
            // Arrange
            var couriers = _fixture.Create<List<BookDeliveryCore.Courier>>();
            oAdministrator.Setup(repo => repo.GetCouriers()).Returns(couriers);

            // Act
            var result = _controller.GetCouriers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(couriers, okResult.Value);
        }

        [TestMethod]
        public void GetCouriers_ReturnsNoContent_WhenNoCouriers()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetCouriers()).Returns((List<BookDeliveryCore.Courier>)null);

            // Act
            var result = _controller.GetCouriers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GetCouriers_ReturnsBadRequest_OnException()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetCouriers()).Throws(new Exception("Test Exception"));

            // Act
            var result = _controller.GetCouriers();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Test Exception", badRequestResult.Value);
        }

        [TestMethod]
        public void GetAdministrators_ReturnsOkResult_WithAdministrators()
        {
            // Arrange
            var administrators = _fixture.Create<List<BookDeliveryCore.Administrator>>();
            oAdministrator.Setup(repo => repo.GetAdministrators()).Returns(administrators);

            // Act
            var result = _controller.GetAdministrators();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(administrators, okResult.Value);
        }

        [TestMethod]
        public void GetAdministrators_ReturnsNoContent_WhenNoAdministrators()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetAdministrators()).Returns((List<BookDeliveryCore.Administrator>)null);

            // Act
            var result = _controller.GetAdministrators();

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GetAdministrators_ReturnsBadRequest_OnException()
        {
            // Arrange
            oAdministrator.Setup(repo => repo.GetAdministrators()).Throws(new Exception("Test Exception"));

            // Act
            var result = _controller.GetAdministrators();

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Test Exception", badRequestResult.Value);
        }
    }
}
