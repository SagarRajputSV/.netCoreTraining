using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/topheaderinformation")]
    [ApiController]
    //[Authorize]
    public class TopHeaderInformationController : ControllerBase
    {
        private readonly ITopHeaderInformationApplicationServices _topHeaderInformationApplicationServices;
        public TopHeaderInformationController(ITopHeaderInformationApplicationServices topHeaderInformationApplicationServices)
        {
            _topHeaderInformationApplicationServices = topHeaderInformationApplicationServices;
        }

        /// <summary>
        /// Get latest contact details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultObj = await _topHeaderInformationApplicationServices.GetAsync(x => x.IsActive);
            var result = resultObj.OrderByDescending(o => o.CreatedDate).Select(x => new
            {
                x.Id,
                x.IsActive,
                x.OfficialContactEmail,
                x.OfficialContactNumber,
                x.OfficialFacebookId,
                x.OfficialInstagramId,
                x.OfficialTwitterId,
                x.OfficialLinkedInId
            }).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TopHeaderInformationModel topHeaderInformation)
        {
            if (topHeaderInformation!=null && ModelState.IsValid)
            {
                var topHeaderInformationObj = new TopHeaderInformation
                {
                    CreatedBy = "90515121-68b5-4dee-9b59-1977f9d51043",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    OfficialContactEmail = topHeaderInformation.OfficialContactEmail,
                    OfficialContactNumber = topHeaderInformation.OfficialContactNumber,
                    OfficialFacebookId = topHeaderInformation.OfficialFacebookId,
                    OfficialInstagramId = topHeaderInformation.OfficialInstagramId,
                    OfficialLinkedInId = topHeaderInformation.OfficialLinkedInId,
                    OfficialTwitterId = topHeaderInformation.OfficialTwitterId,
                };

                var result = await _topHeaderInformationApplicationServices.AddAsync(topHeaderInformationObj);
                return Ok();
            }
            return Ok(BadRequest());
        }
    }
}