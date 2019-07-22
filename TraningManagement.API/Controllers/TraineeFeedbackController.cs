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
    [Route("api/traineefeedback")]
    [ApiController]
    [Authorize]
    public class TraineeFeedbackController : ControllerBase
    {
        #region Private Variables
        private ClaimModel _claimModel;
        private readonly ITraineeFeedbackApplicationServices _traineeFeedbackApplicationServices;
        #endregion

        #region Constructor
        public TraineeFeedbackController(ITraineeFeedbackApplicationServices traineeFeedbackApplicationServices)
        {
            _traineeFeedbackApplicationServices = traineeFeedbackApplicationServices;
        }
        #endregion

        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var traineeFeedbackObj = await _traineeFeedbackApplicationServices.GetAllAsync();
            return Ok(traineeFeedbackObj);
        }
        #endregion

        #region Post APIs
        [HttpPost]
        public async Task<IActionResult> Post(TraineeFeedbackModel traineeFeedback)
        {
            if (traineeFeedback.TrainingDate>DateTime.Now)
            {
                return Ok("Training feedback date can't be future date");
            }

            _claimModel = UtilitiesHelpers.SetClaims(User);
            //Check user already given feedback with TraineerId, SubjectId and TrainingDate
            if (await _traineeFeedbackApplicationServices.ChaeckAlredyFeedbackSubmitted(Guid.Parse(traineeFeedback.TraineerId),
                    Guid.Parse(traineeFeedback.TraineeId), Guid.Parse(traineeFeedback.SubjectId),
                    traineeFeedback.TrainingDate))
            {
                var result = await _traineeFeedbackApplicationServices.AddTraineeFeedback(traineeFeedback, _claimModel);
                return Ok(result == true ? "Thank you for your feedback." : "We are not able to process your request, please try again.");
            }

            return Ok("You already submitted your feedback. Thank you.");
            
        }
        #endregion
    }
}