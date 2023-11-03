namespace pwa_converter.Controllers;
using Microsoft.AspNetCore.Mvc;
using pwa_converter.Enums;
using pwa_converter.Models.ViewModels;
using pwa_converter.Services;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly IHomeControllerService _homeControllerService;
    private readonly IWebHostEnvironment _hostEnvironment;


    public HomeController(IWebHostEnvironment hostEnvironment, IHomeControllerService homeControllerService)
    {
        _hostEnvironment = hostEnvironment;
        _homeControllerService = homeControllerService;
    }

    [HttpGet("/", Name = "default")]
    [HttpGet("[action]", Name="indexGet")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("[action]", Name = "IndexPost")]
    public async Task<IActionResult> Index(IndexViewModel model)
    {
        if(ModelState.IsValid)
        {
            if (_homeControllerService.ValidateUrl(model.Url))
            {
                var resultFileName = "";
                try
                {
                    resultFileName = await _homeControllerService.RunLighthouseAudit(model.Url, Path.Combine("wwwroot", "lighthouse"));
                }
                catch (FileNotFoundException exception)
                {
                    return View("Error");
                }

                var isValidLighthouseAuditResultFile = _homeControllerService.ValidateLighthouseAuditResultFile(Path.Combine(_hostEnvironment.WebRootPath, "lighthouse", resultFileName));

                if (isValidLighthouseAuditResultFile)
                {
                    return RedirectToRoute("ScoreGet", new { fileName = resultFileName });
                }
                else 
                {
                    return View("Error");
                }   
            }
            else
            {
                ModelState.AddModelError("Url", "URL is not valid.");
            }
        }
        return View(model);
    }

    [HttpGet("[action]/{fileName}", Name = "ScoreGet")]
    public IActionResult Score(string fileName)
    {
        var lighthouseAuditResult = _homeControllerService.DeserialiseLighthouseAuditResult(Path.Combine(_hostEnvironment.WebRootPath, "lighthouse", fileName));

        var auditResultsViewData = _homeControllerService.GetAuditResultsViewData(lighthouseAuditResult);


        var numOfPassesPerformance = _homeControllerService.CountNumOfAuditsPassed(auditResultsViewData[Category.Performance]);
        var numOfPassesPwa = _homeControllerService.CountNumOfAuditsPassed(auditResultsViewData[Category.Pwa]);

        _homeControllerService.DeleteAFile(Path.Combine(_hostEnvironment.WebRootPath, "lighthouse", fileName));

        var model = new ScoreViewModel(auditResultsViewData[Category.Performance].Count, auditResultsViewData[Category.Pwa].Count, numOfPassesPerformance , numOfPassesPwa, auditResultsViewData);
        return View(model);
    }
}