namespace pwa_converter.tests.Controllers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pwa_converter.Services;

public class PwaControllerTests
{
    private IPwaControllerService _mockPwaControllerService;
    private IWebHostEnvironment _mockWebHostEnvironment;
    private PwaController _controller;

    [SetUp]
    public void Setup()
    {
        _mockPwaControllerService = new Mock<IPwaControllerService>().Object;
        _mockWebHostEnvironment = new Mock<IWebHostEnvironment>().Object;
        _controller = new PwaController(_mockWebHostEnvironment, _mockPwaControllerService);
    }

    [Test]
    public void GivenACallToIcon_WhenNoParameterPassed_ThenReturnsViewResult()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = _controller.Icon();

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToIcon_WhenNoParameterPassed_ThenReturnsViewResultWithIconViewName()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = (ViewResult)_controller.Icon();

        Assert.That(result.ViewName, Is.EqualTo("Icon"));
    }

    [Test]
    public void GivenACallToIcon_WhenNoParameterPassed_ThenReturnsViewResultWithDomainNameString()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.HttpContext.Request.Host = new HostString("pwa-converter.com");
        _controller.ControllerContext.HttpContext.Request.Scheme = "https";

        var result = (ViewResult)_controller.Icon();

        Assert.That(result.Model, Is.EqualTo("https://pwa-converter.com"));
    }

    [Test]
    public void GivenACallToFaviconGeneratorApi_WhenValidParameterPassed_ThenReturnsRedirectToRouteResult()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = _controller.FaviconGeneratorApi("json string");

        Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToRouteResult)));
    }

    [Test]
    public void GivenACallToFaviconGeneratorApi_WhenValidParameterPassed_ThenReturnsRedirectToRouteResultWithManifestGetRouteName()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = (RedirectToRouteResult)_controller.FaviconGeneratorApi("json string");

        Assert.That(result.RouteName, Is.EqualTo("ManifestGet"));
    }

    [Test]
    public void GivenACallToFaviconGeneratorApi_WhenValidParameterPassed_ThenACookieWithNameSetToPWAConverterCookieIsSet()
    {
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var mockFaviconGeneratorApiResponse = "json string";
        var result = (RedirectToRouteResult)_controller.FaviconGeneratorApi(mockFaviconGeneratorApiResponse);

        Mock.Get(_mockPwaControllerService).Verify(pwaControllerService => pwaControllerService.AddCookie(It.IsAny<HttpContext>(),
                                                                                  It.IsAny<string>(),
                                                                                  mockFaviconGeneratorApiResponse), Times.Once());
    }

    [Test]
    public void GivenACallToFaviconGeneratorApi_WhenEmptyParameterPassed_ThenReturnsViewResult()
    {
        var result = _controller.FaviconGeneratorApi("");

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToFaviconGeneratorApi_WhenEmptyParameterPassed_ThenReturnsViewResultWithErrorViewName()
    {
        var result = (ViewResult)_controller.FaviconGeneratorApi("");

        Assert.That(result.ViewName, Is.EqualTo("Error"));
    }

    [Test]
    public void GivenACallToManifest_WhenEmptyParameterPassed_ThenReturnsViewResult()
    {
        var result = _controller.Manifest();

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToManifest_WhenEmptyParameterPassed_ThenReturnsViewResultWithEmptyViewName()
    {
        var result = (ViewResult)_controller.Manifest();

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsInvalid_ThenReturnsViewResult()
    {
        var model = new ManifestViewModel();
        _controller.ModelState.AddModelError("error", "Name is required.");

        var result = _controller.Manifest(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsInvalid_ThenReturnsViewResultWithEmptyViewName()
    {
        var model = new ManifestViewModel();
        _controller.ModelState.AddModelError("error", "Name is required.");

        var result = (ViewResult)_controller.Manifest(model);

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsInvalid_ThenReturnsViewResultWithTheInvalidModel()
    {
        var model = new ManifestViewModel();
        _controller.ModelState.AddModelError("error", "Name is required.");

        var result = (ViewResult)_controller.Manifest(model);

        Assert.That(result.Model, Is.EqualTo(model));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsValid_ThenReturnsRedirectToRouteResult()
    {
        var model = new ManifestViewModel()
        {
            Name = "PWA Converter",
            ShortName = "PWA Converter",
            Description = "PWA Converter is an application for converting websites into a PWA. Additionally, images can be optimised and the current state of the website can be viewed",
            Id = "pwa-converter",
            StartUrl = "/",
            Scope = "/",
            Language = "en",
            Display = "standalone",
            ThemeColor = "#FFFFFF",
            BackgroundColor = "#000000"
        };
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = _controller.Manifest(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToRouteResult)));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsValid_ThenReturnsRedirectToRouteResultWithServiceWorkerGetRouteName()
    {
        
        var model = new ManifestViewModel()
        {
            Name = "PWA Converter",
            ShortName = "PWA Converter",
            Description = "PWA Converter is an application for converting websites into a PWA. Additionally, images can be optimised and the current state of the website can be viewed",
            Id = "pwa-converter",
            StartUrl = "/",
            Scope = "/",
            Language = "en",
            Display = "standalone",
            ThemeColor = "#FFFFFF",
            BackgroundColor = "#000000"
        };

        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = (RedirectToRouteResult)_controller.Manifest(model);

        Assert.That(result.RouteName, Is.EqualTo("ServiceWorkerGet"));
    }

    [Test]
    public void GivenACallToManifestPost_WhenModelIsValid_ThenAllValuesOfTheModelIsAddedToCookies()
    {
        var model = new ManifestViewModel()
        {
            Name = "PWA Converter",
            ShortName = "PWA Converter",
            Description = "PWA Converter is an application for converting websites into a PWA. Additionally, images can be optimised and the current state of the website can be viewed",
            Id = "pwa-converter",
            StartUrl = "/",
            Scope = "/",
            Language = "en",
            Display = "standalone",
            ThemeColor = "#FFFFFF",
            BackgroundColor = "#000000"
        };

        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = (RedirectToRouteResult)_controller.Manifest(model);

        Mock.Get(_mockPwaControllerService).Verify(pwaControllerService => pwaControllerService.AddCookie(It.IsAny<HttpContext>(),
                                                                  It.IsRegex("pwa-converter-manifest-[a-zA-Z]*$"),
                                                                  It.IsAny<string>()), Times.Exactly(10));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenNoParameterPassed_ThenReturnsViewResult()
    {
        var result = _controller.ServiceWorker();

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenNoParameterPassed_ThenReturnsViewResultWithEmptyViewName()
    {
        var result = (ViewResult)_controller.ServiceWorker();

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenInvalidModelIsPassed_ThenReturnsViewResult()
    {
        
        var model = new ServiceWorkerViewModel();
        _controller.ModelState.AddModelError("error", "Caching strategy is required.");

        var result = _controller.ServiceWorker(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenInvalidModelIsPassed_ThenReturnsViewResultWithEmptyViewName()
    {
        var model = new ServiceWorkerViewModel();
        _controller.ModelState.AddModelError("error", "Caching strategy is required.");

        var result = (ViewResult)_controller.ServiceWorker(model);

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenInvalidModelIsPassed_ThenReturnsViewResultWithTheInvalidModel()
    {
        var model = new ServiceWorkerViewModel();
        _controller.ModelState.AddModelError("error", "Caching strategy is required.");

        var result = (ViewResult)_controller.ServiceWorker(model);

        Assert.That(result.Model, Is.EqualTo(model));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenValidModelIsPassed_ThenReturnsRedirectToRouteResult()
    {
        var model = new ServiceWorkerViewModel()
        {
            CacheContent = "/",
            CachingStrategy = "stale-while-revalidate"
        };
        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = _controller.ServiceWorker(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToRouteResult)));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenValidModelIsPassed_ThenReturnsRedirectToRouteResultWithGeneratedResourcesGetRouteName()
    {
        var model = new ServiceWorkerViewModel()
        {
            CacheContent = "/",
            CachingStrategy = "stale-while-revalidate"
        };

        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = (RedirectToRouteResult)_controller.ServiceWorker(model);

        Assert.That(result.RouteName, Is.EqualTo("ResourcesGet"));
    }

    [Test]
    public void GivenACallToServiceWorker_WhenValidModelIsPassed_ThenAllValuesOfTheModelAreAddedToCookies()
    {
        var model = new ServiceWorkerViewModel()
        {
            CacheContent = "/",
            CachingStrategy = "stale-while-revalidate"
        };

        _controller.ControllerContext = new ControllerContext();
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = _controller.ServiceWorker(model);

        Mock.Get(_mockPwaControllerService).Verify(pwaControllerService => pwaControllerService.AddCookie(It.IsAny<HttpContext>(),
                                                                  It.IsRegex("pwa-converter-sw-[a-zA-Z-]*$"),
                                                                  It.IsAny<string>()), Times.Exactly(2));
    }

    //[Test]
    //public void GivenACallToResources_WhenNoParametersPassed_ThenReturnsViewResult()
    //{
    //    var result = _controller.Resources();

    //    Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    //}

    //[Test]
    //public void GivenACallToResources_WhenNoParametersPassed_ThenReturnsViewResultWithViewName()
    //{
    //    var result = (ViewResult)_controller.Resources();

    //    Assert.That(result.ViewName, Is.EqualTo("Resources"));
    //}

    //[Test]
    //public void GivenACallToResources_WhenNoParametersPassed_ThenReturnsViewResultWithStringAsAModel()
    //{
    //    var result = (ViewResult)_controller.Resources();

    //    Assert.That(result.Model, Is.EqualTo(""));
    //}

    //[Test]
    //public void GivenACallToDownload_WhenNoParametersPassed_ThenReturnsFileResult()
    //{
    //    var result = _controller.Download();

    //    Assert.That(result.GetType(), Is.EqualTo(typeof(FileResult)));
    //}

    //[Test]
    //public void GivenACallToDownload_WhenNoParametersPassed_ThenReturnsFileResultWithXBytes()
    //{

    //}

    //[Test]
    //public void GivenACallToDownload_WhenNoParametersPassed_ThenReturnsFileResultWithContentTypeSetToJson()
    //{
    //    var result = (FileResult)_controller.Download();

    //    Assert.That(result.ContentType, Is.EqualTo("application/json"));
    //}

    //[Test]
    //public void GivenACallToDownload_WhenNoParametersPassed_ThenReturnsFileResultWithFileDownloadName()
    //{
    //    var mockHttpContext = new Mock<HttpContext>();
    //    var mockHttpRequest = new Mock<HttpRequest>();
    //    var mockRequestCookieCollection = new Mock<IRequestCookieCollection>();
    //    mockHttpContext.Setup(httpContext => httpContext.Request).Returns(mockHttpRequest.Object);
    //    mockHttpRequest.Setup(httpRequest => httpRequest.Cookies).Returns(mockRequestCookieCollection.Object);

    //    var a = new IRequestCookieCollection();
    //    mockRequestCookieCollection.Object.Add();
    //    var result = (FileResult)_controller.Download();

    //    Assert.That(result.FileDownloadName, Is.EqualTo(Path.Combine()));
    //}
}