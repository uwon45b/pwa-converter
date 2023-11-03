namespace pwa_converter.Services
{
    public interface IImagesControllerService
    {
        public DirectoryInfo CreateDirectory(string directoryPath);

        public Task CreateUploadedImage(string path, IFormFile uploadedUserImage);

        public void ConvertImageIntoWebp(string directoryPath, string imageName, string imageNameWithoutExtension);

        public void DeleteImage(string imagePath);

        public void ZipAFile(string pathToFileToZip, string destinationPath);

        public void CopyFile(string pathToOrigioanlFile, string destinationPath);

        public void DeleteDirectory(string path, bool recursive);

        public void DeleteFile(string path);

        public void AddCookie(HttpContext httpContext, string cookieName, string cookieValue);

        public byte[] ReadAllBytesOfFile(string path);
    }
}
