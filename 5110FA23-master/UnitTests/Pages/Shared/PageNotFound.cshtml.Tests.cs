﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SuperHeroes.WebSite.Pages;
using System.Diagnostics;

namespace SuperHeroes.WebSite.Tests.Pages
{
    [TestFixture]
    public class PageNotFoundModelTests
    {
        private Mock<ILogger<ErrorModel>> _loggerMock;
        private PageNotFoundModel _pageModel;

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a mock logger for the page model
            _loggerMock = new Mock<ILogger<ErrorModel>>();

            // Act: Instantiate PageNotFoundModel with the mock logger
            _pageModel = new PageNotFoundModel(_loggerMock.Object);
        }

        [Test]
        public void OnGet_SetsRequestIdFromActivity_WhenActivityExists()
        {
            // Arrange
            Activity activity = new Activity("TestActivity");
            activity.Start();

            // Act
            _pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(activity.Id, _pageModel.RequestId);
        }
        [Test]
        public void OnGet_SetsRequestIdFromHttpContext_WhenActivityIsNull()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "TestTraceId";

            // Act
            _pageModel.PageContext = new PageContext { HttpContext = httpContext };
            _pageModel.OnGet();

            // Assert
            Assert.AreEqual(httpContext.TraceIdentifier, _pageModel.RequestId);
        }
        [Test]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsNullOrEmpty()
        {
            // Arrange

            // Act
            var result = _pageModel.ShowRequestId;

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void ShowRequestId_ReturnsTrue_WhenRequestIdIsValid()
        {
            // Arrange
            _pageModel.RequestId = "TestRequestId";

            // Act
            var result = _pageModel.ShowRequestId;

            // Assert
            Assert.IsTrue(result);
        }

    }
}
