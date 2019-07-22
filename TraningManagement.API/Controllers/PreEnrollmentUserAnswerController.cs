using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/PreEnrollmentUserAnswer")]
    [ApiController]
    [Authorize]
    public class PreEnrollmentUserAnswerController : Controller
    {
        #region Private Variables
        private readonly IPreEnrollmentUserAnswerApplicationServices _preEnrollmentUserAnswerApplicationServices;
        #endregion

        #region Constructor
        public PreEnrollmentUserAnswerController(IPreEnrollmentUserAnswerApplicationServices preEnrollmentUserAnswerApplicationServices)
        {
            _preEnrollmentUserAnswerApplicationServices = preEnrollmentUserAnswerApplicationServices;
        }
        #endregion

        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var resultObj = await _preEnrollmentUserAnswerApplicationServices.
                GetAsync(x => x.CourseEnrollment.UserId == Guid.Parse(claimModel.UserId));
            return Ok(resultObj);
        }

        [HttpGet("IsAlreadySubmittedAnser/{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var resultObj = await _preEnrollmentUserAnswerApplicationServices.
                GetAsync(x => x.CourseEnrollmentId == Id && x.CreatedBy == claimModel.UserId);

            if (resultObj.Count > 0)
            {
                return Json(new { Success = false, Message = Constants.AlreadySubmitted });
            }
            return Json(new { Success = true, Message = "" }); ;
        }
        #endregion

        #region Put APIs

        #endregion

        #region Patch APIs

        #endregion

        #region Post APIs
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Post(ListOfAnswerModel listAnswerModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var result = await _preEnrollmentUserAnswerApplicationServices.AddAll(listAnswerModel,claimModel);

            if (result > 0)
            {
                return Json(new { Success = true, Message = Constants.ThankYouForYourAnswer });
            }
            return Json(new { Success = false, Message = Constants.RequestProcessingFailed });
        }
        #endregion

        #region Delete APIs

        #endregion
    }
}