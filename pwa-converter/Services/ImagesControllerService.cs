using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace pwa_converter.Services;

public class ImagesControllerService : IImagesControllerService
{
    public DirectoryInfo CreateDirectory(string directoryPath)
    {
        return Directory.CreateDirectory(directoryPath);
    }

    public async Task CreateUploadedImage(string path, IFormFile uploadedUserImage)
    {
        using (var stream = File.Create(path))
        {
            await uploadedUserImage.CopyToAsync(stream);
        }
    }

    public void ConvertImageIntoWebp(string directoryPath, string imageName, string imageNameWithoutExtension)
    {
        var ps = new ProcessStartInfo();
        ps.WorkingDirectory = directoryPath;
        ps.FileName = "cmd.exe";
        ps.WindowStyle = ProcessWindowStyle.Hidden;
        ps.Arguments = $"/c cwebp -q 80 {imageName} -o {imageNameWithoutExtension}.webp";
        var processStarted = Process.Start(ps);
        while (!File.Exists(Path.Combine(directoryPath, imageNameWithoutExtension + ".webp")))
        {
        }
        while (IsFileLocked(new FileInfo(Path.Combine(directoryPath, imageNameWithoutExtension + ".webp"))))
        {
        }
        processStarted.Kill();
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;

        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        return false;
    }

    public void DeleteImage(string imagePath)
    {
        File.Delete(imagePath);
    }

    public void ZipAFile(string pathToFileToZip, string destinationPath)
    {
        ZipFile.CreateFromDirectory(pathToFileToZip, destinationPath);
    }

    public void CopyFile(string pathToOrigioanlFile, string destinationPath)
    {
        System.IO.File.Copy(pathToOrigioanlFile, destinationPath);
    }

    public void DeleteDirectory(string path, bool recursive)
    {
        Directory.Delete(path, recursive);
    }

    public void AddCookie(HttpContext httpContext, string cookieName, string cookieValue)
    {
        var options = new CookieOptions();
        options.IsEssential = true;
        options.Path = "/images";

        httpContext.Response.Cookies.Append(cookieName, cookieValue, options);
    }

    public void DeleteFile(string path)
    {
        File.Delete(path);
    }

    public byte[] ReadAllBytesOfFile(string path)
    {
        return File.ReadAllBytes(path);
    }

}
