using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        #region Private Variables
        private readonly IImagesApplicationServices _imagesApplicationServices;
        public static IHostingEnvironment _environment;
        #endregion

        #region Contructors
        public ImagesController(IImagesApplicationServices imagesApplicationServices,
            IHostingEnvironment environment)
        {
            _imagesApplicationServices = imagesApplicationServices;
            _environment = environment;
        }
        #endregion

        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultObj = await _imagesApplicationServices.GetAsync(x => x.IsActive);
            return Ok(resultObj);
        }
        #endregion

        #region Put APIs
        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put(IFormFile images)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var fileDetails = HttpContext.Request.Form.Files[0];
                var contentType = fileDetails.ContentType;
                var fileSize = fileDetails.Length;
                var fileName = fileDetails.FileName;
                var stream = fileDetails.OpenReadStream();

                var requestObj = new Images
                {
                    FileName = fileName,
                    ImageData = ReadToEnd(stream),
                    IsActive = true,
                    ContentType = contentType,
                    CreatedBy = "5BCFE8AF-6E21-48AA-4F2D-08D6CA38493C",
                    FileSize = fileSize,
                    CreatedDate = DateTime.Now
                };
                var result = await _imagesApplicationServices.AddAsync(requestObj);
                return Ok(requestObj.Id);
            }
            return Ok();
        }
        #endregion

        #region Patch APIs

        #endregion

        #region Delete APIs

        #endregion

        #region Post APIs
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(IFormFile images)
        {
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var fileDetails = HttpContext.Request.Form.Files[0];
                var contentType = fileDetails.ContentType;
                var fileSize = fileDetails.Length;
                var fileName = fileDetails.FileName;
                var stream = fileDetails.OpenReadStream();
                var requestObj = new Images
                {
                    FileName = fileName,
                    ImageData = ReadToEnd(stream),
                    IsActive = true,
                    ContentType = contentType,
                    CreatedBy = "5BCFE8AF-6E21-48AA-4F2D-08D6CA38493C",
                    FileSize = fileSize,
                    CreatedDate = DateTime.Now
                };
                var result = await _imagesApplicationServices.AddAsync(requestObj);
                return Ok(requestObj.Id);
            }
            return Ok();
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        //public async Task<IActionResult> Post(Images images)
        //{
        //    var result = await _imagesApplicationServices.AddAsync(images);
        //    return Ok(result);
        //}
        #endregion
    }
}