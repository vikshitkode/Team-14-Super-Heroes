﻿using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Routing;

namespace UnitTests.Pages.Shared
{
    public class AboutUs
    {
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static AboutUsModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
                //RequestServices = serviceProviderMock.Object,
            };
            httpContextDefault.HttpContext.TraceIdentifier = "trace";

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
                HttpContext = httpContextDefault
            };

            ///creating variable mockWebHostEnvironment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            ///creating variable MockLoggerDirect
            var MockLoggerDirect = Mock.Of<ILogger<AboutUsModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new AboutUsModel(MockLoggerDirect)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        [Test]
        public void Constructor_SetsLogger()
        {
            // Arrange
            var logger = new Logger<AboutUsModel>(new LoggerFactory());

            // Act
            var pageModel = new AboutUsModel(logger);

            // Assert
            Assert.AreEqual(logger, pageModel._logger);
        }

        [Test]
        public void OnGet_DoesNotThrowException()
        {
            // Arrange
            var logger = new Logger<AboutUsModel>(new LoggerFactory());

            // Act
            var pageModel = new AboutUsModel(logger);

            // Assert
            Assert.DoesNotThrow(pageModel.OnGet);
        }
    }
}