using Microsoft.AspNetCore.Hosting;
using pwa_converter.Enums;
using pwa_converter.Services;

namespace pwa_converter.tests.Controllers;

public class HomeControllerTests
{
    private IHomeControllerService _mockHomeControllerService;
    private IWebHostEnvironment _mockWebHostEnvironment;
    private HomeController _controller;

    [SetUp]
    public void Setup()
    {
        _mockHomeControllerService = new Mock<IHomeControllerService>().Object;
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns("wwwroot");
        _mockWebHostEnvironment = mockWebHostEnvironment.Object;
        _controller = new HomeController(_mockWebHostEnvironment, _mockHomeControllerService);
    }

    [Test]
    public void GivenACallToIndex_WhenNoParamsPassed_ThenViewResultReturned()
    {
        var result = _controller.Index();

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToIndex_WhenNoParamsPassed_ThenViewResultWithEmptyViewNameReturned()
    {
        var result = (ViewResult)_controller.Index();

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public async Task GivenACallToIndex_WhenInvalidModelPassed_ThenReturnsViewResult()
    {
        var model =  new IndexViewModel { Url = "invalid" };
        var result = await _controller.Index(model);
        var a = result.GetType();
        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public async Task GivenACallToIndex_WhenInvalidModelPassed_ThenReturnsViewResultWithEmptyViewName()
    {
        var model = new IndexViewModel { Url = "invalid" };
        var result = (ViewResult)await _controller.Index(model);

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public async Task GivenACallToIndex_WhenInvalidModelPassed_ThenReturnsViewResultWithInvalidModel()
    {
        var model = new IndexViewModel { Url = "invalid" };
        var result = (ViewResult)await _controller.Index(model);

        Assert.That(result.Model, Is.EqualTo(model));
    }

    [Test]
    public void GivenACallToIndex_WhenInvalidModelPassed_ThenAnErrorIsAdded()
    {
        var model = new IndexViewModel { Url = "invalid" };
        var result = _controller.Index(model);
        var errors= _controller.ModelState.Values.SelectMany(value => value.Errors);
        var error = errors.FirstOrDefault(error => error.ErrorMessage == "URL is not valid.");
        Assert.That(error.ErrorMessage, Is.EqualTo("URL is not valid."));
    }

    [Test]
    public async Task GivenACallToIndex_WhenValidModeIsPassedAndValidResultFileIsCreated_ThenReturnsRedirectToRouteResult()
    {
        var model = new IndexViewModel { Url = "https://valid.com" };
        
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateUrl(It.IsAny<string>())).Returns(true);
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.RunLighthouseAudit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateLighthouseAuditResultFile(It.IsAny<string>())).Returns(true);
        var result = await _controller.Index(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(RedirectToRouteResult)));
    }

    [Test]
    public async Task GivenACallToIndex_WhenValidModeIsPassedAndValidResultFileIsCreated_ThenReturnsRedirectToRouteResultWithScoreName()
    {
        var model = new IndexViewModel { Url = "https://valid.com" };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateUrl(It.IsAny<string>())).Returns(true);
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.RunLighthouseAudit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateLighthouseAuditResultFile(It.IsAny<string>())).Returns(true);
        var result = (RedirectToRouteResult) await _controller.Index(model);

        Assert.That(result.RouteName, Is.EqualTo("ScoreGet"));
    }

    [Test]
    public async Task GivenACallToIndex_WhenValidModeIsPassedAndValidResultFileIsCreated_ThenReturnsRedirectToRouteResultWithRouteValue()
    {
        var model = new IndexViewModel { Url = "https://valid.com" };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateUrl(It.IsAny<string>())).Returns(true);
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.RunLighthouseAudit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateLighthouseAuditResultFile(It.IsAny<string>())).Returns(true);
        var result = (RedirectToRouteResult) await _controller.Index(model);

        Assert.That(result.RouteValues.First().Key, Is.EqualTo("fileName"));
    }

    [Test]
    public async Task GivenACallToIndex_WhenValidModeIsPassedButInvalidResultFileIsCreated_ThenReturnsViewResult()
    {
        var model = new IndexViewModel { Url = "https://valid.com" };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateUrl(It.IsAny<string>())).Returns(true);
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.RunLighthouseAudit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateLighthouseAuditResultFile(It.IsAny<string>())).Returns(false);
        var result = await _controller.Index(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public async Task GivenACallToIndex_WhenValidModeIsPassedButInvalidResultFileIsCreated_ThenReturnsViewResultWithViewName()
    {
        var model = new IndexViewModel { Url = "https://valid.com" };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateUrl(It.IsAny<string>())).Returns(true);
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.RunLighthouseAudit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("");
        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.ValidateLighthouseAuditResultFile(It.IsAny<string>())).Returns(false);
        var result = (ViewResult) await _controller.Index(model);

        Assert.That(result.ViewName, Is.EqualTo("Error"));
    }

    [Test]
    public void GivenACallToScore_WhenFileNameIsPassed_ThenReturnsViewResult()
    {
        var mockAuditResultsViewData = new Dictionary<Category, IList<pwa_converter.Models.AuditResult>>
        {
            [Category.Performance] = new List<pwa_converter.Models.AuditResult>(),
            [Category.Pwa] = new List<pwa_converter.Models.AuditResult>()
        };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.GetAuditResultsViewData(It.IsAny<pwa_converter.Models.LighthouseAuditResultJson>()))
                                            .Returns(mockAuditResultsViewData);

        var result = _controller.Score("");

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToScore_WhenFileNameIsPassed_ThenReturnsViewResultWithEmptyViewName()
    {
        var mockAuditResultsViewData = new Dictionary<Category, IList<pwa_converter.Models.AuditResult>>
        {
            [Category.Performance] = new List<pwa_converter.Models.AuditResult>(),
            [Category.Pwa] = new List<pwa_converter.Models.AuditResult>()
        };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.GetAuditResultsViewData(It.IsAny<pwa_converter.Models.LighthouseAuditResultJson>()))
                                            .Returns(mockAuditResultsViewData);

        var result = (ViewResult)_controller.Score("");

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public void GivenACallToScore_WhenFileNameIsPassed_ThenReturnsViewResultWithScoreViewModel()
    {
        var mockAuditResultsViewData = new Dictionary<Category, IList<pwa_converter.Models.AuditResult>>
        {
            [Category.Performance] = new List<pwa_converter.Models.AuditResult>(),
            [Category.Pwa] = new List<pwa_converter.Models.AuditResult>()
        };

        Mock.Get(_mockHomeControllerService).Setup(homeControllerService => homeControllerService.GetAuditResultsViewData(It.IsAny<pwa_converter.Models.LighthouseAuditResultJson>()))
                                            .Returns(mockAuditResultsViewData);

        var result = (ViewResult)_controller.Score("");

        Assert.That(result.Model.GetType(), Is.EqualTo(typeof(ScoreViewModel)));
    }
}
