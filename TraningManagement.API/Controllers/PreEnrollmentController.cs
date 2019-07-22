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
    [Route("api/PreEnrollment")]
    [ApiController]
    [Authorize]
    public class PreEnrollmentController : ControllerBase
    {
        #region Private Variables
        private readonly IPreEnrollmentQuestionApplicationServices _preEnrollmentApplicationService;
        #endregion

        #region Constructor
        public PreEnrollmentController(IPreEnrollmentQuestionApplicationServices preEnrollmentApplicationService)
        {
            _preEnrollmentApplicationService = preEnrollmentApplicationService;
        }
        #endregion

        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var includeProperties = new string[] { "TrainerSubjectMapping", "TrainerSubjectMapping.Subject" };
            List<PreEnrollmentQuestions> resultObj;

            if (string.Equals(claimModel.RoleName, "Administrator", StringComparison.CurrentCultureIgnoreCase))
            {
                resultObj = await _preEnrollmentApplicationService.GetAsync(x => x.Question != null, 0, includeProperties);
            }
            else
            {
                resultObj = await _preEnrollmentApplicationService.GetAsync(x => x.Question != null &&
                x.CreatedBy == claimModel.UserId, 0, includeProperties);
            }

            var result = resultObj.Select(x => new
            {
                x.Id,
                x.Question,
                x.Answer,
                x.MinimumPassingMarks,
                x.PrerequisiteLinks,
                x.IsActive,
                x.QuestionWeight,
                x.TrainerSubjectMapping.Subject.Name,
                x.TrainingId,
                x.MaxAnswerTime
            });

            return Ok(result);
        }
        #endregion

        #region Put APIs
        [HttpPut]
        public async Task<IActionResult> Put(PreEnrollmentQuestions preEnrollmentQuestions)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existingObj = await _preEnrollmentApplicationService.FindAsync(preEnrollmentQuestions.Id);
            if (existingObj!=null)
            {
                existingObj.Answer = preEnrollmentQuestions.Answer;
                existingObj.Question = preEnrollmentQuestions.Question;
                existingObj.MinimumPassingMarks = preEnrollmentQuestions.MinimumPassingMarks;
                existingObj.PrerequisiteLinks = preEnrollmentQuestions.PrerequisiteLinks;
                existingObj.QuestionWeight = preEnrollmentQuestions.QuestionWeight;
                existingObj.TrainingId = preEnrollmentQuestions.TrainingId;
                existingObj.UpdatedBy = claimModel.UserId;
                existingObj.UpdatedDate = DateTime.Now;
                existingObj.MaxAnswerTime = preEnrollmentQuestions.MaxAnswerTime;
                existingObj.IsActive = preEnrollmentQuestions.IsActive;

                var result =await _preEnrollmentApplicationService.UpdateAsync(existingObj);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpGet("ToggleStatus/{Id}")]
        public async Task<IActionResult>Put(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existingObj = await _preEnrollmentApplicationService.FindAsync(Id);
            if (existingObj != null)
            {
                existingObj.UpdatedBy = claimModel.UserId;
                existingObj.UpdatedDate = DateTime.Now;
                existingObj.IsActive = existingObj.IsActive == true ? false : true;

                var result = await _preEnrollmentApplicationService.UpdateAsync(existingObj);
                return Ok(result);
            }
            return Ok(false);
        }
        #endregion

        #region Patch APIs
        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            var result = await _preEnrollmentApplicationService.GetAllAsync();
            return Ok(result);
        }
        #endregion

        #region Post APIs
        [HttpPost]
        public async Task<IActionResult> Post(PreEnrollmentQuestionModel preEnrollmentQuestion)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var requestObj = new PreEnrollmentQuestions
            {
                Question = preEnrollmentQuestion.Question,
                Answer = preEnrollmentQuestion.Answer,
                MinimumPassingMarks = preEnrollmentQuestion.MinimumPassingMarks,
                QuestionWeight = preEnrollmentQuestion.QuestionWeight,
                PrerequisiteLinks = preEnrollmentQuestion.PrerequisiteLinks,
                TrainingId = preEnrollmentQuestion.TrainingId,
                MaxAnswerTime = preEnrollmentQuestion.MaxAnswerTime,
                CreatedBy = claimModel.UserId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            var result = await _preEnrollmentApplicationService.AddAsync(requestObj);
            return Ok(result);
        }
        #endregion

        #region Delete APIs
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var result = await _preEnrollmentApplicationService.GetAllAsync();
            return Ok(result);
        }
        #endregion
    }
}