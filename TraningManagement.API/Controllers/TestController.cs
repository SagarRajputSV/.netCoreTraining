using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainingManagement.API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(FileInputModel file)
        {
            if (file == null || file.FileData.Length == 0)
                return Content("file not selected");

            return Ok();
        }
    }

    public class FileInputModel
    {
        public Stream FileData { get; set; }
        public string FirstName { get; set; }
        public long Lenght { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}