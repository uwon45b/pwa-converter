namespace pwa_converter.Controllers;
using Microsoft.AspNetCore.Mvc;
using pwa_converter.Models.ViewModels;
using pwa_converter.Services;
using System;

[Route("[controller]")]
public class ImagesController : Controller
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IImagesControllerService _imagesControllerService;

    public ImagesController(IWebHostEnvironment hostEnvironment, IImagesControllerService imagesControllerService)
    {
        _hostEnvironment = hostEnvironment;
        _imagesControllerService = imagesControllerService;
    }

    [HttpGet("[action]", Name = "OptimiseGet")]
    public IActionResult Optimise()
    {
        return View();
    }

    [HttpPost("[action]", Name = "OptimisePost")]
    public async Task<IActionResult> Optimise(OptimiseViewModel model)
    {
        if (ModelState.IsValid) 
        {
            var userId = Guid.NewGuid();

            var wwwrootPath = _hostEnvironment.WebRootPath;
            var directoryPath = Path.Combine(wwwrootPath, $"images-{userId}");
            _imagesControllerService.CreateDirectory(directoryPath);

            var newImageFolder = Path.Combine(wwwrootPath, $"new-images-{userId}");
            _imagesControllerService.CreateDirectory(newImageFolder);

            foreach (var imageFile in model.Images)
            {
                if (imageFile.Length > 0)
                {
                    var imageNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    var extension = Path.GetExtension(imageFile.FileName);
                    var imageNameWithExtension = $"{imageNameWithoutExtension}{extension}";
                    var imagePath = Path.Combine(directoryPath, imageNameWithExtension);
                    
                    _imagesControllerService.CreateUploadedImage(imagePath, imageFile);

                    _imagesControllerService.ConvertImageIntoWebp(directoryPath, imageNameWithExtension, imageNameWithoutExtension);

                    _imagesControllerService.CopyFile(Path.Combine(directoryPath, $"{imageNameWithoutExtension}.webp"), Path.Combine(newImageFolder, $"{imageNameWithoutExtension}.webp"));
                }
            }

            _imagesControllerService.ZipAFile(newImageFolder, Path.Combine(wwwrootPath, $"optimised-images-{userId}.zip"));
            var zipFilePath = Path.Combine(wwwrootPath, $"optimised-images-{userId}.zip");

            _imagesControllerService.DeleteDirectory(newImageFolder, true);

            _imagesControllerService.AddCookie(HttpContext, "pwa-converter-image-optimisation-user-id", userId.ToString());

            return File(_imagesControllerService.ReadAllBytesOfFile(zipFilePath), "image/webp", Path.GetFileName(zipFilePath));
        }

        return View(model);
    }

    [HttpGet("[action]/{userId}", Name = "CleanUpImagesGet")]
    public void CleanUp(string userId)
    {
        var wwwrootPath = _hostEnvironment.WebRootPath;
        var directoryPath = Path.Combine(wwwrootPath, $"images-{userId}");
        var newImageFolder = Path.Combine(wwwrootPath, $"optimised-images-{userId}.zip");

        _imagesControllerService.DeleteDirectory(directoryPath, true);
        _imagesControllerService.DeleteFile(newImageFolder);
    }
}