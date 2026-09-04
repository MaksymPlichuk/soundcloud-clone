using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Services
{
    public class ImageService
    {
        public async Task<ServiceResponse> CreateImageAsync(IFormFile file, string basePath, string subPath)
        {
            try
            {
                var type = file.ContentType.Split("/");
                if (type.Length > 2 || type[0] != "image")
                {
                    return ServiceResponse.Failure("File is not Image!");
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string savePath = Path.Combine(basePath, subPath, fileName);

                using var openFileStream = File.OpenWrite(savePath);
                await file.CopyToAsync(openFileStream);


                string savePathWithSub = subPath.ToLower() + "/" + fileName;

                return ServiceResponse.Success("Image saved!", savePathWithSub);

            }
            catch (Exception ex)
            {
                return ServiceResponse.Failure(ex.Message);
            }
        }

        public ServiceResponse DeleteImage(string basePath, string fileName) 
        { 

            string capitalizedForPath = char.ToUpper(fileName[0]) + fileName.Substring(1);
            string fullFilePath = Path.Combine(basePath, capitalizedForPath);

            if (!File.Exists(fullFilePath)) { return ServiceResponse.Failure("File doesn't exist"); }
            try
            {
                File.Delete(fullFilePath);
                return ServiceResponse.Success($"File '{fileName}' was Deleted!");
            }
            catch (Exception ex)
            {
                return ServiceResponse.Failure(ex.Message);
            }

        }
    }
}
