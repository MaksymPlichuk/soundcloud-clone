using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.BLL.Services
{
    public class FileSongService
    {
        public async Task<ServiceResponse> CreateSongAsync(IFormFile file, string fullPath)
        {
            try
            {
                var type = file.ContentType.Split("/");
                if (type.Length > 2 || type[0] != "audio")
                {
                    return ServiceResponse.Failure("File is not auidio!");
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string savePath = Path.Combine(fullPath, fileName);

                using var openFileStream = File.OpenWrite(savePath);
                await file.CopyToAsync(openFileStream);


                string savePathWithSub = fullPath.ToLower() + "/" + fileName;

                return ServiceResponse.Success("Image saved!", savePathWithSub);

            }
            catch (Exception ex)
            {
                return ServiceResponse.Failure(ex.Message);
            }
        }

        public ServiceResponse DeleteSong(string basePath, string fileName)
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
