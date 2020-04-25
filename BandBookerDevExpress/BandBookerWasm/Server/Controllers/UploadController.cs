using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BandBookerWasm.Server.Controllers
{
    public class ChunkMetadata
    {
        public int Index { get; set; }
        public int TotalCount { get; set; }
        public int FileSize { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileGuid { get; set; }
    }


    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {

        [HttpPost]
        [Route("UploadFile")]
        public ActionResult UploadFile(IFormFile file)
        {
            try
            {
                var path = Environment.CurrentDirectory + "\\images";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpPost]
        [Route("UploadImage")]
        public ActionResult UploadImage(IFormFile imageFile)
        {
            try
            {
                string[] imageExtensions = { ".jpg", ".jpeg", ".gif", ".png" };

                var fileName = imageFile.FileName.ToLower();
                var isValidExtenstion = imageExtensions.Any(ext => {
                    return fileName.LastIndexOf(ext) > -1;
                });

                if (isValidExtenstion)
                {
                    var path = "C:\\uploads";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    using (var fileStream = System.IO.File.Create(Path.Combine(path, imageFile.FileName)))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpPost]
        [Route("UploadChunkFile")]
        public ActionResult UploadChunkFile(IFormFile file, string chunkMetadata)
        {
            var tempPath = "C:\\uploads";
            // Removes temporary files
            RemoveTempFilesAfterDelay(tempPath, new TimeSpan(0, 5, 0));

            try
            {
                if (!string.IsNullOrEmpty(chunkMetadata))
                {
                    var metaDataObject = JsonConvert.DeserializeObject<ChunkMetadata>(chunkMetadata);

                    // Uncomment to save the file
                    var tempFilePath = Path.Combine(tempPath, metaDataObject.FileGuid + ".tmp");
                    if (!Directory.Exists(tempPath))
                        Directory.CreateDirectory(tempPath);

                    AppendContentToFile(tempFilePath, file);

                    if (metaDataObject.Index == (metaDataObject.TotalCount - 1))
                        ProcessUploadedFile(tempFilePath, metaDataObject.FileName);
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
        void RemoveTempFilesAfterDelay(string path, TimeSpan delay)
        {
            var dir = new DirectoryInfo(path);
            if (dir.Exists)
                foreach (var file in dir.GetFiles("*.tmp").Where(f => f.LastWriteTimeUtc.Add(delay) < DateTime.UtcNow))
                    file.Delete();
        }
        void ProcessUploadedFile(string tempFilePath, string fileName)
        {
            // Check if the uploaded file is a valid image
            var path = "C:\\uploads";
            System.IO.File.Copy(tempFilePath, Path.Combine(path, fileName));
        }
        void AppendContentToFile(string path, IFormFile content)
        {
            using (var stream = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                content.CopyTo(stream);
            }
        }
    }
}
