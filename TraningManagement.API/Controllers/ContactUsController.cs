using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrainingManagement.API.Controllers
{
    [Route("api/ContactUs")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new
            {
                Id=Guid.NewGuid(),
                FirstName = "Rajesh",
                MiddleName = "Kumar",
                LastName = "Kushwaha",
                UserName = "Rajesh",
                Email = "rajesh.kushwaha@softvision.com",
                EmailConfirmed = true,
                BirthDay = new DateTime(1988,05,03),
                PhoneNumber = "+91-9818481284",
                PhoneNumberConfirmed=true,
            };

            return Ok(result);
        }
        #endregion
    }
}