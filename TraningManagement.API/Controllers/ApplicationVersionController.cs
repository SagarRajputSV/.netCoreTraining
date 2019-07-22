using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/ApplicationVersion")]
    [ApiController]
    public class ApplicationVersionController : ControllerBase
    {

        #region Private Variables
        private readonly IApplicationVersionApplicationService _applicationVersionApplicationServices;

        #endregion

        #region Constructor
        public ApplicationVersionController(IApplicationVersionApplicationService applicationVersionApplicationServices)
        {
            _applicationVersionApplicationServices = applicationVersionApplicationServices;
        }

        #endregion

        #region Get APIs

        [HttpGet("GetUiLatestVersion")]
        public async Task<IActionResult> GetUiLatestVersion()
        {
            var resultObj = await _applicationVersionApplicationServices.GetAsync(x => x.IsActive && x.IsUIVeriosnActive);
            var result = resultObj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet("GetApiLatestVersion")]
        public async Task<IActionResult> GetApiLatestVersion()
        {
            var resultObj = await _applicationVersionApplicationServices.GetAsync(x => x.IsActive && x.IsAPIVeriosnActive);
            var result = resultObj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var resultObj = await _applicationVersionApplicationServices.GetAsync(x => x.IsActive && x.IsAPIVeriosnActive && x.IsUIVeriosnActive);
            var result = resultObj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var resultObj = await _applicationVersionApplicationServices.GetAsync(x => x.IsActive);
            var result = resultObj.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return Ok(result);
        }
        #endregion

        #region Put APIs
        [HttpPut]
        public async Task<IActionResult> Put(ApplicationVersionModel applicationVersionModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            if (claimModel.RoleName.Equals("Administrator", StringComparison.CurrentCultureIgnoreCase))
            {
                var existingObj = await _applicationVersionApplicationServices.FindAsync(applicationVersionModel.Id);
                existingObj.APIMajorChanges = applicationVersionModel.APIMajorChanges;
                existingObj.APIVersion = applicationVersionModel.APIVersion;
                existingObj.IsAPIVeriosnActive = applicationVersionModel.IsAPIVeriosnActive;
                existingObj.UIVersion = applicationVersionModel.UIVersion;
                existingObj.UIMajorChanges = applicationVersionModel.UIMajorChanges;
                existingObj.IsUIVeriosnActive = applicationVersionModel.IsUIVeriosnActive;
                var result = _applicationVersionApplicationServices.UpdateAsync(existingObj);
                return Ok(result);
            }

            return Ok(false);
        }
        #endregion

        #region Patch APIs
        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            return Ok();
        }
        #endregion

        #region Post APIs
        [HttpPost]
        public async Task<IActionResult> Post(ApplicationVersionModel applicationVersionModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            if (claimModel.RoleName.Equals("",StringComparison.CurrentCultureIgnoreCase))
            {
                var requestobj = new ApplicationVersion
                {
                    APIVersion = applicationVersionModel.APIVersion,
                    APIMajorChanges = applicationVersionModel.APIMajorChanges,
                    IsAPIVeriosnActive = applicationVersionModel.IsAPIVeriosnActive,
                    UIVersion = applicationVersionModel.UIVersion,
                    UIMajorChanges = applicationVersionModel.UIMajorChanges,
                    IsUIVeriosnActive = applicationVersionModel.IsUIVeriosnActive,
                    IsActive = true,
                    CreatedBy = claimModel.UserId,
                    CreatedDate = DateTime.Now
                };

                var result = await _applicationVersionApplicationServices.AddAsync(requestobj);

                return Ok(result);
            }
            return Ok(false);
        }
        #endregion

        #region Delete APIs
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _applicationVersionApplicationServices.DeleteAsync(Id);
            return Ok(result);
        }
        #endregion
    }
}