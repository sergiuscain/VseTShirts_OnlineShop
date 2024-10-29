
namespace VseTShirts.Helpers
{
    public class ImageProvider
    {
        private readonly IWebHostEnvironment _environment;
        public ImageProvider(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public List<string> SaveFiles(List<IFormFile> files, ImageFolders folder)
        {
            List<string> imagePaths = new List<string>();
            foreach (var file in files)
            {
                var imagePath = SaveFile(file, folder);
                imagePaths.Add(imagePath);
            }
            return imagePaths;
        }

        public string SaveFile(IFormFile file, ImageFolders folder)
        {
            if (file != null)
            {
                var folderPath = Path.Combine(_environment.WebRootPath + "/Images/" + folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                var imagePath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return $"/Images/{folder}/{fileName}";
            }
            return null;
        }
    }
}
