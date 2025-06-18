using Microsoft.AspNetCore.Http;

namespace Mission.Repositories.Helpers
{
    public class UploadFile
    {
        public static async Task<string> SaveImageAsync(IFormFile file, string folderName, string imageUploadPath)
        {
            try
            {
                if (file == null || file.Length == 0) return string.Empty;

                string uploadsFolder = Path.Combine(imageUploadPath, folderName);
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.UtcNow:yyyyMMddHHmmss}{fileExtension}";
                string fullPath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine(folderName, fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
