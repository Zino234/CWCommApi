using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeCommApi.Controllers
{
    [ApiController]
    [Route("file/[action]")]
    public class FileController : ControllerBase
    {
        public static IWebHostEnvironment _enviroment;

        public FileController(IWebHostEnvironment env){
            _enviroment=env;
        }




        public class FileUploadDto {
            [Required]
            public IFormFile file {get;set;}
        }

[HttpGet]
public IActionResult GetAllFiles()
{
    try
    {
        var uploadFolderPath = Path.Combine(_enviroment.WebRootPath, "Uploads");

        if (Directory.Exists(uploadFolderPath))
        {
            var files = Directory.GetFiles(uploadFolderPath)
                                  .Select(Path.GetFileName)
                                  .ToList();

            return Ok(files);
        }
        else
        {
            return NotFound("Uploads folder not found");
        }
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}




[HttpGet("{fileName}")]
public IActionResult Download(string fileName)
{
    try
    {
        var filePath = Path.Combine(_enviroment.WebRootPath, "Uploads", fileName);

        if (System.IO.File.Exists(filePath))
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        else
        {
            return NotFound(); // or handle the case when the file is not found
        }
    }
    catch (Exception ex)
    {
        // Log or handle the exception appropriately
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}

        [HttpPost]
        public async Task<string> Upload([FromForm] FileUploadDto obj){
          try
          {
              if(obj.file.Length>0){
                if(!Directory.Exists(_enviroment.WebRootPath+"\\Uploads\\")){
                    Directory.CreateDirectory(_enviroment.WebRootPath+"\\Uploads\\");
                }
                using(FileStream stream =System.IO.File.Create(_enviroment.WebRootPath+"\\Uploads\\"+obj.file.FileName)){
                       await obj.file.CopyToAsync(stream);
    return obj.file.FileName;
                }
            }
            return "Invalid File || Something went wrong";
          }
          catch (System.Exception ex)
          {
            return ex.Message.ToString();
          }
        }
    }
}