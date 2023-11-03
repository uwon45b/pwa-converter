namespace pwa_converter.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pwa_converter.Models;
using pwa_converter.Models.ViewModels;
using pwa_converter.Services;
using System.Text.RegularExpressions;

[Route("[controller]")]
public class PwaController : Controller
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IPwaControllerService _pwaControllerService;

    public PwaController(IWebHostEnvironment hostEnvironment, IPwaControllerService pwaControllerService)
    {
        _hostEnvironment = hostEnvironment;
        _pwaControllerService = pwaControllerService;
    }

    [HttpGet("[action]", Name = "IconGet")]
    public IActionResult Icon()
    {
        var host = HttpContext.Request.Host;
        var scheme = HttpContext.Request.Scheme;
        var domainName = $"{scheme}://{host}";
        return View("Icon", domainName);
    }

    [HttpGet("[action]", Name = "FaviconGeneratorApiGet")]
    public IActionResult FaviconGeneratorApi(string json_result) 
    {
        if (!string.IsNullOrEmpty(json_result))
        {
            _pwaControllerService.AddCookie(HttpContext, "pwa-converter-icon", json_result);

            return RedirectToRoute("ManifestGet");
        }

        return View("Error");
    }

    [HttpGet("[action]", Name = "ManifestGet")]
    public IActionResult Manifest()
    {
        return View();
    }

    [HttpPost("[action]", Name = "ManifestPost")]
    [ValidateAntiForgeryToken]
    public IActionResult Manifest(ManifestViewModel model)
    {
        if (ModelState.IsValid)
        {
            var properties = typeof(ManifestViewModel).GetProperties();
            foreach (var property in properties)
            {
                var fieldName = property.Name;
                var fieldValue = (string)property.GetValue(model);
                if(fieldName == "StartUrl")
                {
                    fieldValue = _pwaControllerService.ValidateStartUrl(fieldValue);
                }
                else if (fieldName == "Scope")
                {
                    fieldValue = _pwaControllerService.ValidateScope(fieldValue);
                }
                _pwaControllerService.AddCookie(HttpContext, $"pwa-converter-manifest-{fieldName}", fieldValue);
            }

            return RedirectToRoute("ServiceWorkerGet");
        }
        return View(model);
    }

    [HttpGet("[action]", Name = "ServiceWorkerGet")]
    public IActionResult ServiceWorker()
    {
        return View();
    }

    [HttpPost("[action]", Name = "ServiceWorkerPost")]
    [ValidateAntiForgeryToken]
    public IActionResult ServiceWorker(ServiceWorkerViewModel model)
    {
        if (ModelState.IsValid)
        {
            _pwaControllerService.AddCookie(HttpContext, "pwa-converter-sw-cache-content", model.CacheContent);
            _pwaControllerService.AddCookie(HttpContext, "pwa-converter-sw-caching-strategy", model.CachingStrategy);

            return RedirectToRoute("ResourcesGet");
        }
        return View(model);
    }

    [HttpGet("[action]", Name = "ResourcesGet")]
    public IActionResult Resources()
    {
        var faviconJsonString = HttpContext.Request.Cookies["pwa-converter-icon"];
        var faviconGeneratorApiResult = JsonConvert.DeserializeObject<FaviconGeneratorApiJson>(faviconJsonString);
        var htmlCode = faviconGeneratorApiResult.favicon_generation_result.favicon.html_code;
        htmlCode = htmlCode.Replace("/favicons/site.webmanifest", "/site.webmanifest");
        htmlCode += "\n<script src=\"/service-worker-registration.js\" defer></script>";
        return View("Resources", htmlCode);
    }

    [HttpGet("[action]", Name = "DownloadGet")]
    public IActionResult Download()
    {
        var userId = Guid.NewGuid().ToString();
        _pwaControllerService.AddCookie(HttpContext, "pwa-converter-user-id", userId);
        var folderName = $"pwa-generated-resources-{userId}";

        var faviconJsonString = HttpContext.Request.Cookies["pwa-converter-icon"];
        var faviconGeneratorApiResult = JsonConvert.DeserializeObject<FaviconGeneratorApiJson>(faviconJsonString);
        var faviconDownloadLink = faviconGeneratorApiResult.favicon_generation_result.favicon.package_url;
        var faviconHtmlCode = faviconGeneratorApiResult.favicon_generation_result.favicon.html_code;
        var wwwrootPath = _hostEnvironment.WebRootPath;

        _pwaControllerService.DownloadFavicons(wwwrootPath, folderName, faviconDownloadLink, userId);
        _pwaControllerService.UnzipAFile(Path.Combine(wwwrootPath, $"favicons-{userId}.zip"), Path.Combine(wwwrootPath, folderName, $"favicons"));
        _pwaControllerService.MoveFile(Path.Combine(wwwrootPath, folderName, $"favicons", "site.webmanifest"),
                                       Path.Combine(wwwrootPath, folderName, "site.webmanifest"));

        var manifestFileCookies = HttpContext.Request.Cookies.Where(cookie => Regex.IsMatch(cookie.Key, @"^pwa-converter-manifest-[a-z-]*")).ToDictionary(cookie => cookie.Key, cookie => cookie.Value);
        _pwaControllerService.GenerateManifestFile(manifestFileCookies, Path.Combine(wwwrootPath, folderName, "site.webmanifest"));

        _pwaControllerService.GenerateServiceWorker(Path.Combine(wwwrootPath, folderName),
                                                    $"\"{HttpContext.Request.Cookies["pwa-converter-manifest-ShortName"]}-v1\"",
                                                    HttpContext.Request.Cookies["pwa-converter-sw-cache-content"].Split(","),
                                                    HttpContext.Request.Cookies["pwa-converter-sw-caching-strategy"]);

        var zipFilePath = Path.Combine(wwwrootPath, $"pwa-resources-{userId}.zip");
        _pwaControllerService.ZipAFile(Path.Combine(wwwrootPath, folderName), zipFilePath);
        return File(System.IO.File.ReadAllBytes(zipFilePath), "application/json", Path.GetFileName(zipFilePath));

    }

    [HttpGet("[action]/{userId}", Name = "CleanUpGet")]
    public void CleanUp(string userId)
    {
        var pwaGeneratedResourcesFolder = $"pwa-generated-resources-{userId}";
        var zippedFaviconsFile = $"favicons-{userId}.zip";
        var zippedPwaGeneratedResourcesFile = $"pwa-resources-{userId}.zip"; 

        var wwwrootPath = _hostEnvironment.WebRootPath;

        Directory.Delete(Path.Combine(wwwrootPath, pwaGeneratedResourcesFolder), true);
        System.IO.File.Delete(Path.Combine(wwwrootPath, zippedFaviconsFile));
        System.IO.File.Delete(Path.Combine(wwwrootPath, zippedPwaGeneratedResourcesFile));
    }
}