using Abp.Application.Services;
using Abp.Web.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace Hinnova.Web.Controllers
{
    //[WrapResult]
    [WrapResult(false)]
    [RemoteService(false)]
    public class FileManagerImagesController : HinnovaControllerBase
    {

       // static readonly string SampleImagesRelativePath = Path.Combine("SampleData", "SampleImages");
        //static readonly string SampleImagesRelativePath = Path.Combine("SampleData", "SampleImages");
        private readonly string connectionString;
        public IWebHostEnvironment _env { get; }
   
      
        public FileManagerImagesController(IWebHostEnvironment hostingEnvironment ) 
        {
            _env = hostingEnvironment;
           
        }
        [Route("api/file-manager-file-system-images", Name = "FileManagementImagesApi")]
        [HttpGet]
    //    public object FileSystem(FileSystemCommand command, string arguments)
    //{
    //    var config = new FileSystemConfiguration
    //    {   
    //        Request = Request,
    //        FileSystemProvider = new DefaultFileProvider(
    //            Path.Combine(_env.WebRootPath, $"Common{Path.DirectorySeparatorChar}Images{Path.DirectorySeparatorChar}SampleProfilePics"),
    //            (fileSystemItem, clientItem) => {
    //                if (!clientItem.IsDirectory)
    //                    clientItem.CustomFields["url"] = GetFileItemUrl(fileSystemItem);
    //            }
    //        ),
    //        //uncomment the code below to enable file/directory management
    //        AllowCopy = true,
    //        AllowCreate = true,
    //        AllowMove = true,
    //        AllowRemove = true,
    //        AllowRename = true,
    //        AllowUpload = true,
    //        AllowDownload = true
    //    };
    //    var processor = new FileSystemCommandProcessor(config);
    //    var result = processor.Execute(command, arguments);
    //    return result.GetClientCommandResult();
    //}

    string GetFileItemUrl(FileSystemInfo fileSystemItem)
    {
        var relativeUrl = fileSystemItem.FullName
            .Replace(_env.WebRootPath, "")
            .Replace(Path.DirectorySeparatorChar, '/');
        return $"{Request.Scheme}://{Request.Host}{Request.PathBase}{relativeUrl}";
    }
        //[HttpPost]
        //public void FileSelection(IFormFile photo)
        //{
        //    string name_image = "";

        //    if (photo != null)
        //    {
        //        SaveFile(photo);
        //        name_image = photo.FileName;

        //    }

        //}
        //void SaveFile(IFormFile file)
        //{
        //    try
        //    {
        //        var path = Path.Combine(_env.WebRootPath, "uploads");

        //        if (!Directory.Exists(path))
        //            Directory.CreateDirectory(path);

        //        using (var fileStream = System.IO.File.Create(Path.Combine(path, file.FileName)))
        //        {
        //            file.CopyTo(fileStream);
        //        }
        //    }
        //    catch
        //    {
        //        Response.StatusCode = 400;
        //    }
        //}


        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
