namespace BuyMovies.Helpers
{
    public class DocumentSettings
    {
        public static string UploadImages(IFormFile file,string FolderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            var fileStream=new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName; 

        }
    }
}
