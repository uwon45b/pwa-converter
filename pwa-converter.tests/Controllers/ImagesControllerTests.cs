namespace pwa_converter.tests.Controllers;
using Microsoft.AspNetCore.Hosting;
using pwa_converter.Services;

public class ImagesControllerTests
{
    private IImagesControllerService _mockImagesControllerService;
    private IWebHostEnvironment _mockWebHostEnvironment;
    private ImagesController _controller;

    [SetUp]
    public void Setup()
    {
        _mockImagesControllerService = new Mock<IImagesControllerService>().Object;
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(webHostEnvironment => webHostEnvironment.WebRootPath).Returns("wwwroot");
        _mockWebHostEnvironment = mockWebHostEnvironment.Object;
        _controller = new ImagesController(_mockWebHostEnvironment, _mockImagesControllerService);
    }

    [Test]
    public void GivenACallToOptimise_WhenNoParamsPassed_ThenViewResultIsReturned()
    {
        var result = _controller.Optimise();

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public void GivenACallToOptimise_WhenNoParamsPassed_ThenViewResultWithEmptyViewNameIsReturned()
    {
        var result = (ViewResult)_controller.Optimise();

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public async Task GiveACallToOptimise_WhenModelIsInvalid_ThenReturnsViewResult()
    {
        var model = new OptimiseViewModel { Images = new FormFileCollection() };
        _controller.ModelState.AddModelError("error", "Image is required.");
        var result = await _controller.Optimise(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(ViewResult)));
    }

    [Test]
    public async Task GiveACallToOptimise_WhenModelIsInvalid_ThenReturnsViewResultWithEmptyViewName()
    {
        var model = new OptimiseViewModel { Images = new FormFileCollection() };
        _controller.ModelState.AddModelError("error", "Image is required.");
        var result = (ViewResult) await _controller.Optimise(model);

        Assert.That(result.ViewName, Is.EqualTo(null));
    }

    [Test]
    public async Task GiveACallToOptimise_WhenModelIsInvalid_ThenReturnsViewResultWithInvalidModel()
    {
        var model = new OptimiseViewModel { Images = new FormFileCollection() };
        _controller.ModelState.AddModelError("error", "Image is required.");
        var result = (ViewResult)await _controller.Optimise(model);

        Assert.That(result.Model, Is.EqualTo(model));
    }

    [Test]
    public async Task GiveACallToOptimise_WhenModelIsValid_ThenReturnsFileContentResult()
    {
        var model = new OptimiseViewModel { Images = new FormFileCollection() };
        var result = await _controller.Optimise(model);

        Assert.That(result.GetType(), Is.EqualTo(typeof(FileContentResult)));
    }

    [Test]
    public async Task GiveACallToOptimise_WhenModelIsValid_ThenReturnsFileContentResultWithWebpContentType()
    {
        var model = new OptimiseViewModel { Images = new FormFileCollection() };
        var result = (FileContentResult) await _controller.Optimise(model);

        Assert.That(result.ContentType, Is.EqualTo("image/webp"));
    }

    [Test]
    public void GivenACallToCleanUp_WhenUserIdIsPassed_ThenDeleteDirectoryIsCalledOnce()
    {
        _controller.CleanUp("userId");

        Mock.Get(_mockImagesControllerService).Verify(imagesControllerService => imagesControllerService.DeleteDirectory(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
    }

    [Test]
    public void GivenACallToCleanUp_WhenUserIdIsPassed_ThenDeleteFileIsCalledOnce()
    {
        _controller.CleanUp("userId");

        Mock.Get(_mockImagesControllerService).Verify(imagesControllerService => imagesControllerService.DeleteFile(It.IsAny<string>()), Times.Once);
    }
}
