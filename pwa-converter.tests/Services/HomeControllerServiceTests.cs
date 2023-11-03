namespace pwa_converter.tests.Services;

using pwa_converter.Enums;
using pwa_converter.Models;
using pwa_converter.Services;

public class HomeControllerServiceTests
{
    private IHomeControllerService _service;

    [SetUp]
    public void Setup()
    {
        var dictionary = new Dictionary<Category, IDictionary<Type, IDictionary<System.Type, Audit>>>
        {
            [Category.Performance] = new Dictionary<Type, IDictionary<System.Type, Audit>>(),
            [Category.Pwa] = new Dictionary<Type, IDictionary<System.Type, Audit>>()
        };

        dictionary[Category.Performance].Add(Type.Opportunity, new Dictionary<System.Type, Audit>());
        dictionary[Category.Performance].Add(Type.Diagnostics, new Dictionary<System.Type, Audit>());
        dictionary[Category.Pwa].Add(Type.PwaOptimized, new Dictionary<System.Type, Audit>());
        dictionary[Category.Pwa].Add(Type.Installable, new Dictionary<System.Type, Audit>());

        var mockLighthouseAuditResultContainer = new Mock<ILighthouseAuditResultContainer>();
        mockLighthouseAuditResultContainer.Setup(lighthouseAuditResultContainer => lighthouseAuditResultContainer.LighthouseAuditResults)
                                          .Returns(dictionary);

        _service = new HomeControllerService(mockLighthouseAuditResultContainer.Object);
    }

    [Test]
    public void GivenACallToValidateUrl_WhenUrlIsInvalid_ThenReturnsFalse()
    {
        var result = _service.ValidateUrl("invalid");

        Assert.IsFalse(result);
    }

    [Test]
    public void GivenACallToValidateUrl_WhenUrlIsValid_ThenReturnsTrue()
    {
        var result = _service.ValidateUrl("https://valid.com");

        Assert.IsTrue(result);
    }

    [Test]
    public void GivenACallToCountNumOfAuditsPassed_WhenAListWithTwoPassedAuditResultsIsPassed_ThenReturnsTwo()
    {
        var result = _service.CountNumOfAuditsPassed(new List<AuditResult> { new AuditResult(null, 1), new AuditResult(null, 1), new AuditResult(null, 0), new AuditResult(null, 0.5) });

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void GivenACallToGetAuditResultsViewData_WhenNoParameterPassed_ThenReturnsDictionaryWithAuditResults()
    {
        var lighthouseAuditResultJson = new LighthouseAuditResultJson { audits = new Audits() };
        var result = _service.GetAuditResultsViewData(lighthouseAuditResultJson);

        Assert.That(result.GetType(), Is.EqualTo(typeof(Dictionary<Category, IList<AuditResult>>)));
    }
}