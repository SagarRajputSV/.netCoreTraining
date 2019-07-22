using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.ApplicationServices;
using TrainingManagement.Application.EmailHelper;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/CourseEnrollment")]
    [ApiController]
    [Authorize]
    public class CourseEnrollmentController : Controller
    {
        #region Private Variables
        private readonly ITrainingSubjectApplicationService _courseEnrollmentApplicationServices;
        private readonly ITaineerSubjectApplicationServices _taineerSubjectApplicationServices;
        private readonly ApplicationSettings _applicationSettings;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SmtpConfigurationModel _smtpConfigurationModel;
        private readonly ILogger<CourseEnrollmentController> _logger;
        private readonly CryptoEngineModel _cryptoEngineModel;
        //private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public CourseEnrollmentController(ITrainingSubjectApplicationService courseEnrollmentApplicationServices,
            ITaineerSubjectApplicationServices taineerSubjectApplicationServices,
            IOptions<ApplicationSettings> appSettings, IHostingEnvironment hostingEnvironment,
            IOptions<SmtpConfigurationModel> smtpConfigurationModel, 
            ILogger<CourseEnrollmentController> logger,
            IOptions<CryptoEngineModel> cryptoEngineModel)
        {
            _courseEnrollmentApplicationServices = courseEnrollmentApplicationServices;
            _taineerSubjectApplicationServices = taineerSubjectApplicationServices;
            _applicationSettings = appSettings.Value;
            _hostingEnvironment = hostingEnvironment;
            _smtpConfigurationModel = smtpConfigurationModel.Value;
            _logger = logger;
            _cryptoEngineModel = cryptoEngineModel.Value;
        }
        #endregion

        #region APIS

        #region GET APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultObj = await _courseEnrollmentApplicationServices.GetAllAsync();
            return Ok(resultObj);
        }

        /// <summary>
        /// Check user is already registerd for training by trainingId and UserId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var isAlreadyRegistered = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId) && x.TrainingId == Id && x.IsActive);

            if (isAlreadyRegistered.Count > 0)
            {
                return Json(new { Success = true, Message = Constants.AlreadyRegisterd });
            }
            return Json(new { Success = true, Message = "" });
        }

        [HttpGet("IsPreEnrollmentLinkVisited/{Id}")]
        public async Task<IActionResult> IsPreEnrollmentLinkVisited(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var enrollmentDetails = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId) && x.TrainingId == Id && x.IsActive && x.IsPreEnrollmentLinkVisited);

            if (enrollmentDetails.Count > 0)
            {
                return Json(new { Success = true, Message = Constants.LinkAlreadyUsed });
            }
            return Json(new { Success = true, Message = "" });
        }

        [HttpGet("GetRegistrationDetails/{Id}")]
        public async Task<IActionResult> GetRegistrationDetails(Guid Id)
        {
            var includeProperties = new string[] { "TrainerSubjectMappings", "TrainerSubjectMappings.PreEnrollmentQuestions",
            "TrainerSubjectMappings.Subject","TrainerSubjectMappings.Trainer"};
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var resultObj = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId) && x.Id == Id && x.IsActive, 0, includeProperties);

            var result = resultObj.Select(x => new
            {
                //x.UserId,
                StartDate = x.TrainerSubjectMappings.StartDate.ToShortDateString(),
                EndDate = x.TrainerSubjectMappings.EndDate.ToShortDateString(),
                SubjectName = x.TrainerSubjectMappings.Subject.Name,
                SubjectId = x.TrainerSubjectMappings.Subject.Id,
                TrainerId = x.TrainerSubjectMappings.TrainerId,
                TrainerName = x.TrainerSubjectMappings.Trainer.Name,
                TrainingId = x.TrainerSubjectMappings.Id,
                PreEnrollmentQuestions =  x.TrainerSubjectMappings.PreEnrollmentQuestions.Select(q => new
                {
                    q.Id,
                    q.QuestionWeight,
                    q.Question,
                    q.PrerequisiteLinks,
                    Answer = q.Answer = string.Empty,
                    q.MaxAnswerTime
                })
            }).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet("GetEnrolledCoursesByUserId")]
        public async Task<IActionResult> GetEnrolledCoursesByUserId()
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var includeProperties = new string[] { "TrainerSubjectMappings",
            "TrainerSubjectMappings.Subject","TrainerSubjectMappings.Trainer"};

            var resultObj = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId), 0, includeProperties);

            var result = resultObj.Select(x => new
            {
                StartDate = x.TrainerSubjectMappings.StartDate.ToShortDateString(),
                EndDate = x.TrainerSubjectMappings.EndDate.ToShortDateString(),
                SubjectName = x.TrainerSubjectMappings.Subject.Name,
                SubjectId = x.TrainerSubjectMappings.Subject.Id,
                x.TrainerSubjectMappings.TrainerId,
                TrainerName = x.TrainerSubjectMappings.Trainer.Name,
                TrainingId = x.TrainerSubjectMappings.Id
            });

            return Ok(result);
        }
        #endregion

        #region PUT APIs
        [HttpPut]
        public async Task<IActionResult> Put(CourseEnrollmentModel courseEnrollment)
        {
            var existingObj = await _courseEnrollmentApplicationServices.FindAsync(courseEnrollment.Id);

            if (existingObj != null)
            {
            }

            return Ok();
        }

        [HttpPut("UpdateIsPreEnrollmentLinkVisited")]
        public async Task<IActionResult> UpdateIsPreEnrollmentLinkVisited(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var enrollmentDetails = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId) && x.TrainingId == Id && x.IsActive && !x.IsPreEnrollmentLinkVisited);

            if (enrollmentDetails.Count > 0)
            {
                enrollmentDetails.FirstOrDefault().IsPreEnrollmentLinkVisited = true;
                enrollmentDetails.FirstOrDefault().UpdatedDate = DateTime.Now;
                enrollmentDetails.FirstOrDefault().UpdatedBy = claimModel.UserId;

                var result = await _courseEnrollmentApplicationServices.UpdateAsync(enrollmentDetails.FirstOrDefault());
                return Json(new { Success = true, Message = Constants.LinkAlreadyUsed });
            }
            return Json(new { Success = true, Message = "" });
        }
        #endregion

        #region PATCH APIs
        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            return Ok();
        }
        #endregion

        #region POST APIs
        [HttpPost]
        public async Task<IActionResult> Post(CourseEnrollmentModel courseEnrollment)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var isAlreadyRegistered = await _courseEnrollmentApplicationServices.
                GetAsync(x => x.UserId == Guid.Parse(claimModel.UserId) &&
                x.TrainingId == courseEnrollment.TrainingId && x.IsActive, 0, "TrainerSubjectMappings");
            if (isAlreadyRegistered.Count > 0)
                return Json(new { Success = true, Message = Constants.AlreadyRegisterd });

            var requestObj = new CourseEnrollment
            {
                TrainingId = courseEnrollment.TrainingId,
                IsActive = true,
                IsPreEnrollmentLinkVisited = false,
                UserId = Guid.Parse(claimModel.UserId),
                CreatedBy = claimModel.UserId,
                CreatedDate = DateTime.Now,
            };

            var result = await _courseEnrollmentApplicationServices.AddAsync(requestObj);

            if (result)
                await SendExamEmail(claimModel, courseEnrollment.PreEnrollmentUrl,requestObj);

            var message = result == true ? Constants.SuccessRegisterdMessage
                : Constants.FaileRegisterdMessage;
            return Json(new { Success = result, Message = message });
        }
        #endregion

        #region DELETE APIs
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _courseEnrollmentApplicationServices.DeleteAsync(Id);
            return Ok(result);
        }
        #endregion

        #endregion

        #region Private Methods
        /// <summary>
        /// Sent email to 
        /// </summary>
        /// <param name="claimModel"></param>
        /// <param name="url"></param>
        /// <param name="courseEnrollment"></param>
        /// <returns></returns>
        private async Task<bool> SendExamEmail(ClaimModel claimModel, string url, CourseEnrollment courseEnrollment)
        {
            var trainingDetails = await _taineerSubjectApplicationServices.FindAsync(courseEnrollment.TrainingId);
            //Change days from hard code to appSetting.json
            var trainingLastDayRegisteration = trainingDetails.StartDate.AddDays(-5);

            var returnUrl = UtilitiesHelpers.GetUrlWithProtocols(url, HttpContext.Request.IsHttps);
            //returnUrl = $"{returnUrl}{url}";
            var encryptedId = CryptoEngine.Encrypt(courseEnrollment.Id.ToString(), _cryptoEngineModel.Key);
            var generatedLink = returnUrl.Replace("$preenrollment", UtilitiesHelpers.Base64ForUrlEncode(encryptedId));

            //Send Email for account confirmation
            string subject = "Pre-enrollment Exam";
            string body = System.IO.File.ReadAllText(_hostingEnvironment.WebRootPath + @"\EmailTemplate.html")
                .Replace("$title", "Pre-enrollment Exam")
                .Replace("$user", string.IsNullOrWhiteSpace(claimModel.UserName) ? claimModel.Email : claimModel.UserName)
                .Replace("$content", string.Format(EmailContent.PreEnrollmentExam, trainingLastDayRegisteration, trainingLastDayRegisteration))
                .Replace("$anchorBody", "Pre-enrollment Email")
                .Replace("$urlClass", "emailButton")
                .Replace("$link", generatedLink);

            EmailHelper emailHelper = new EmailHelper(_smtpConfigurationModel, _hostingEnvironment, _logger);
            bool mailStatus = await emailHelper.SendEmailAsync(claimModel.Email, claimModel.Email, subject, body, "rajesh.kushwaha@softvision.com");
            return mailStatus;
        }
        #endregion
    }
}