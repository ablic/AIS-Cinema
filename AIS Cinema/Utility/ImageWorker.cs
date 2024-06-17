namespace AIS_Cinema
{
    public class ImageWorker
    {
        public const string FolderName = "posters";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageWorker(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string name =
                Path.GetFileNameWithoutExtension(image.FileName) +
                DateTime.Now.ToString("yymmssfff") +
                Path.GetExtension(image.FileName);

            using (FileStream fileStream = new FileStream(Path.Combine(wwwRootPath, FolderName, name), FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return name;
        }

        public void DeleteImage(string? path)
        {
            if (!string.IsNullOrEmpty(path))
                File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, FolderName, path));
        }
    }
}
