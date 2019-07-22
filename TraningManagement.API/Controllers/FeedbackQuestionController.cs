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
    [Route("api/feedbackquestion")]
    [ApiController]
    [Authorize]
    public class FeedbackQuestionController : ControllerBase
    {
        private readonly IFeedbackQuestionApplicationServices _feedbackQuestionApplicationServices;
        public FeedbackQuestionController(IFeedbackQuestionApplicationServices feedbackQuestionApplicationServices)
        {
            _feedbackQuestionApplicationServices = feedbackQuestionApplicationServices;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var result = await _feedbackQuestionApplicationServices.GetAsync(x=>x.IsActive);
            return Ok(result);
        }

        [HttpPut]
        //public async Task<IActionResult> Put()
        //{
        //    return Ok();
        //}

        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(FeedbackQuestionModel feedbackQuestionModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var feedbackQuestionObj = new FeedbackQuestion
            {
                CreatedBy = claimModel.UserId,
                IsActive = true,
                IsOptional = feedbackQuestionModel.IsOptional,
                MaxMark = feedbackQuestionModel.MaxMark,
                MinMark = feedbackQuestionModel.MinMark,
                Question = feedbackQuestionModel.Question,
                CreatedDate = DateTime.Now
            };
            var result = await _feedbackQuestionApplicationServices.AddAsync(feedbackQuestionObj);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _feedbackQuestionApplicationServices.DeleteAsync(Id);
            return Ok(result);
        }
    }
}