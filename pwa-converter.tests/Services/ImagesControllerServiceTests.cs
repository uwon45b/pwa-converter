namespace pwa_converter.tests.Services;
using pwa_converter.Services;

public class ImagesControllerServiceTests
{
    private IImagesControllerService _service;

    [SetUp]
    public void Setup()
    {
        _service = new ImagesControllerService();
    }

    [Test]
    public void GivenACallToAddCookie_WhenParametersAreValid_ThenAppendIsCalledOnce()
    {
        var mockHttpContext = new Mock<HttpContext>();
        var mockHttpResponse = new Mock<HttpResponse>();
        var mockResponseCookies = new Mock<IResponseCookies>();
        mockHttpContext.Setup(httpContext => httpContext.Response).Returns(mockHttpResponse.Object);
        mockHttpResponse.Setup(httpResponse => httpResponse.Cookies).Returns(mockResponseCookies.Object);

        _service.AddCookie(mockHttpContext.Object, "cookieName", "cookieValue");

        mockHttpContext.Verify(httpContext => httpContext.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CookieOptions>()), Times.Once());
    }
}