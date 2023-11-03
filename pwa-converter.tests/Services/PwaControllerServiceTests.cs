namespace pwa_converter.tests.Services;

using pwa_converter.Services;

public class PwaControllerServiceTests
{
    private IPwaControllerService _service;
    private HttpContext _mockHttpContext;

    [SetUp]
    public void Setup()
    {
        _service = new PwaControllerService();
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

    [Test]
    public void GivenACallToValidateStartUrl_WhenParameterIsValid_ThenOriginalStartUrlIsReturned()
    {
        var result = _service.ValidateStartUrl("/start");

        Assert.That(result, Is.EqualTo("/start"));
    }

    [Test]
    public void GivenACallToValidateStartUrl_WhenParameterIsInvalid_ThenReturnsStartUrlWithForwardSlashPrepended()
    {
        var result = _service.ValidateStartUrl("start");

        Assert.That(result, Is.EqualTo("/start"));
    }

    [Test]
    public void GivenACallToValidateScope_WhenParameterIsValid_ThenOriginalScopeIsReturned()
    {
        var result = _service.ValidateStartUrl("/scope/");

        Assert.That(result, Is.EqualTo("/scope/"));
    }

    [Test]
    public void GivenACallToValidateScope_WhenParameterIsInvalid_ThenReturnsScopeWithForwardSlashPrepended()
    {
        var result = _service.ValidateScope("scope/");

        Assert.That(result, Is.EqualTo("/scope/"));
    }

    [Test]
    public void GivenACallToValidateScope_WhenParameterIsInvalidAndMoreThanOneCharacter_ThenReturnsScopeWithForwardSlashAppended()
    {
        var result = _service.ValidateScope("/scope");

        Assert.That(result, Is.EqualTo("/scope/"));
    }

    [Test]
    public void GivenACallToValidateScope_WhenParameterIsInvalidAndMoreThanOneCharacter_ThenReturnsScopeWithForwardSlashPrependedAndAppended()
    {
        var result = _service.ValidateScope("scope");

        Assert.That(result, Is.EqualTo("/scope/"));
    }
}
