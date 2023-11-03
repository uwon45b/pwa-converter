namespace pwa_converter.Services;

public interface IPwaControllerService
{
    public void AddCookie(HttpContext httpContext, string cookieName, string cookieValue);

    public string ValidateStartUrl(string startUrl);

    public string ValidateScope(string scope);

    public void DownloadFavicons(string wwwrootPath, string folderToDownloadTo, string faviconGeneratorDownloadUrl, string id);

    public void UnzipAFile(string zipFileLocation, string unzipLocation);

    public void ZipAFile(string pathToFileToZip, string destinationPath);

    public void MoveFile(string fileToMove, string moveFileTo);

    public void GenerateManifestFile(IDictionary<string, string> manifestCookies, string manifestFileLocation);

    public void GenerateServiceWorker(string createServiceWorkerFilesAt, string cacheName, string[] urlsToCache, string cachingStrategy);
}
