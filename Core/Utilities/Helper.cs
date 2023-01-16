using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities
{
    public class Helper
    {
        public static void FileDelete(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public static void FileUpload(IFormFile formFile, string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
        }
    }
}
