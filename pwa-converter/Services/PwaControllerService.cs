namespace pwa_converter.Services;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public class PwaControllerService : IPwaControllerService
{
    private IDictionary<string, string> _serviceWorkerCodeDictionary;

    public PwaControllerService()
    {
        _serviceWorkerCodeDictionary = new Dictionary<string, string>
        {
            ["serviceWorkerRegistration"] = "registerSW()\r\n\r\nfunction registerSW(){\r\n    if ('serviceWorker' in navigator) {\r\n        window.addEventListener('load', function() {\r\n        navigator.serviceWorker.register('/service-worker.js')\r\n        });\r\n    }    \r\n}",
            ["installEvent"] = "self.addEventListener(\"install\", event=> {\r\n    event.waitUntil(caches.open(cache_name).then(cache=>{\r\n        return cache.addAll(urlsToCache)\r\n    }))\r\n})",
            ["activateEvent"] = "self.addEventListener(\"activate\", event=> {\r\n    event.waitUntil(caches.keys().then(cacheNames=>{\r\n        return Promise.all(cacheNames.filter(cacheName=>cacheName !== cache_name).map(cacheName=>caches.delete(cacheName)))\r\n    }))\r\n})",
            ["stale-while-revalidate"] = "self.addEventListener('fetch', event => {\r\n  event.respondWith(\r\n    caches.match(event.request).then(cachedResponse => {\r\n        const networkFetch = fetch(event.request).then(response => {\r\n          const responseClone = response.clone()\r\n          caches.open(cache_name).then(cache => {\r\n            cache.put(event.request, responseClone)\r\n          })\r\n          return response\r\n        }).catch(function (reason) {\r\n          console.error('ServiceWorker fetch failed: ', reason)\r\n        })\r\n        return cachedResponse || networkFetch\r\n      }\r\n    )\r\n  )\r\n})",
            ["cache-first"] = "self.addEventListener(\"fetch\", event => {\r\n    event.respondWith(\r\n      caches.match(event.request)\r\n      .then(cachedResponse => {\r\n          return cachedResponse || fetch(event.request)\r\n      }\r\n    )\r\n   )\r\n })",
            ["network-first"] = "self.addEventListener(\"fetch\", event => {\r\n    event.respondWith(\r\n      fetch(event.request)\r\n      .catch(error => {\r\n        return caches.match(event.request)\r\n      })\r\n    )\r\n })",
            ["cache-only"] = "self.addEventListener(\"fetch\", event => {\r\n  event.respondWith(caches.match(event.request))\r\n})",
            ["network-only"] = ""
        };
    }

    public void AddCookie(HttpContext httpContext, string cookieName, string cookieValue)
    {
        var options = new CookieOptions();
        options.IsEssential = true;
        options.Path = "/Pwa";

        httpContext.Response.Cookies.Append(cookieName, cookieValue, options);
    }

    public string ValidateStartUrl(string startUrl)
    {
        if (!ValidateIfStartsWithForwardSlash(startUrl))
        {
            startUrl = startUrl.Insert(0, "/");
        }

        return startUrl;
    }

    public string ValidateScope(string scope)
    {
        if (!ValidateIfStartsWithForwardSlash(scope))
        {
            scope = scope.Insert(0, "/");
        }

        if (scope.Length > 1)
        {
            if (!scope.EndsWith('/'))
            {
                scope += '/';
            }
        }
        return scope;
    }

    public void DownloadFavicons(string wwwrootPath, string folderToDownloadTo ,string faviconGeneratorDownloadUrl, string id)
    {
        var zipFileName = $"favicons-{id}.zip";
        using (var client = new WebClient())
        {
            client.DownloadFile(faviconGeneratorDownloadUrl, Path.Combine(wwwrootPath, zipFileName));
        }

        ZipFile.ExtractToDirectory(Path.Combine(wwwrootPath, zipFileName),
                                                Path.Combine(wwwrootPath, folderToDownloadTo, "favicons"),
                                                Encoding.UTF8,
                                                true);
    }

    public void MoveFile(string fileToMove, string moveFileTo)
    {
        File.Move(fileToMove, moveFileTo);
    }

    public void UnzipAFile(string zipFileLocation, string unzipLocation)
    {
        ZipFile.ExtractToDirectory(zipFileLocation,
                                                 Path.Combine(unzipLocation),
                                                 Encoding.UTF8,
                                                 true);
    }

    public void ZipAFile(string pathToFileToZip, string destinationPath)
    {
        ZipFile.CreateFromDirectory(pathToFileToZip, destinationPath);
    }

    private bool ValidateIfStartsWithForwardSlash(string str)
    {
        var firstCharacter = str[0];

        return firstCharacter == '/';
    }

    public void GenerateManifestFile(IDictionary<string, string> manifestCookies, string manifestFileLocation)
    {
        var content = File.ReadAllText(manifestFileLocation);
        var pattern = new Regex("\"icons.*]", RegexOptions.Singleline);
        var iconField = pattern.Match(content);

        var manifestContent = "{" +
                      $"\r\n    \"name\": \"{manifestCookies["pwa-converter-manifest-Name"]}\"," +
                      $"\r\n    \"short_name\": \"{manifestCookies["pwa-converter-manifest-ShortName"]}\"," +
                      $"\r\n    \"description\": \"{manifestCookies["pwa-converter-manifest-Description"]}\"," +
                      $"\r\n    \"start_url\": \"{manifestCookies["pwa-converter-manifest-StartUrl"]}\"," +
                      $"\r\n    \"theme_color\": \"{manifestCookies["pwa-converter-manifest-ThemeColor"]}\"," +
                      $"\r\n    \"background_color\": \"{manifestCookies["pwa-converter-manifest-BackgroundColor"]}\"," +
                      $"\r\n    \"display\": \"{manifestCookies["pwa-converter-manifest-Display"]}\"," +
                      $"\r\n    \"scope\": \"{manifestCookies["pwa-converter-manifest-Scope"]}\"," +
                      $"\r\n    \"id\": \"{manifestCookies["pwa-converter-manifest-Id"]}\"," +
                      $"\r\n    \"lang\": \"{manifestCookies["pwa-converter-manifest-Language"]}\"," +
                      $"\r\n    {iconField}" +
                      "\r\n}";


        File.WriteAllText(manifestFileLocation, manifestContent);
    }

    private void CreateNewFile(string path, string content)
    {
        using (var fs = File.Create(path))
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            var byteCount = Encoding.UTF8.GetByteCount(content);
            fs.Write(bytes, 0, byteCount);
        }
    }

    public void GenerateServiceWorker(string createServiceWorkerFilesAt, string cacheName, string[] urlsToCache, string cachingStrategy)
    {
        CreateNewFile(Path.Combine(createServiceWorkerFilesAt, "service-worker-registration.js"), _serviceWorkerCodeDictionary["serviceWorkerRegistration"]);

        var urlsToCacheString = "\"favicons/android-chrome-192x192.png\", \"favicons/android-chrome-512x512.png\", \"favicons/apple-touch-icon.png\", \"favicons/browserconfig.xml\", \"favicons/favicon-16x16.png\", \"favicons/favicon-32x32.png\", \"favicons/favicon.ico\", \"favicons/mstile-150x150.png\", \"favicons/safari-pinned-tab.svg\"";
        for (var i = 0; i < urlsToCache.Length; i++)
        {
                urlsToCacheString += $", \"{urlsToCache[i]}\"";
        }

        var text = $"const cache_name = {cacheName}\nconst urlsToCache = [{urlsToCacheString}]\n\n{_serviceWorkerCodeDictionary["installEvent"]}\n\n{_serviceWorkerCodeDictionary["activateEvent"]}";
        if (cachingStrategy != "network-only")
        {
            text += $"\n\n{_serviceWorkerCodeDictionary[cachingStrategy]}";
        }

        CreateNewFile(Path.Combine(createServiceWorkerFilesAt, "service-worker.js"), text);
    }

}