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
    [Route("api/trainersubject")]
    [ApiController]
    [Authorize]
    public class TraineerSubjectController : ControllerBase
    {
        #region Private Variables
        private readonly ITaineerSubjectApplicationServices _traineerSubjectApplicationServices;
        #endregion

        #region Constructor
        public TraineerSubjectController(ITaineerSubjectApplicationServices traineerSubjectApplicationServices)
        {
            _traineerSubjectApplicationServices = traineerSubjectApplicationServices;
        }
        #endregion

        #region Get APIs
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var includeProperties = new string[] { "Trainer", "Subject" };
            var traineerSubjectObj = await _traineerSubjectApplicationServices.GetAsync(x=>x.CreatedDate!=null, 0, includeProperties);
            var result = traineerSubjectObj.Select(x => new
            {
                x.Id,
                x.Instructions,
                x.Trainer.Name,
                Subject = x.Subject.Name,
                x.SubjectId,
                x.TrainerId,
                StartDate = x.StartDate.ToShortDateString(),
                EndDate = x.EndDate.ToShortDateString(),
                x.IsActive
            });
            return Ok(result);
        }

        [HttpGet("{trainerId}")]
        public async Task<IActionResult> Get(Guid trainerId)
        {
            var traineerSubjectmappedObj = await _traineerSubjectApplicationServices
                .GetAsync(x => x.TrainerId == trainerId && x.IsActive, 0, "Subject");
            var result = traineerSubjectmappedObj.Select(x => new
            {
                x.Subject.Name,
                x.Subject.Id,
                x.Subject.IsActive,
                x.TrainerId
            });

            return Ok(result);
        }

        [HttpGet("GetMapedTrainingById/{Id}")]
        public async Task<IActionResult> GetMapedTrainingById(Guid Id)
        {
            var traineerSubjectmappedObj = await _traineerSubjectApplicationServices
                .GetAsync(x => x.Id == Id, 0, "Subject");
            var result = traineerSubjectmappedObj.Select(x => new
            {
                x.Subject.Name,
                x.Subject.IsActive,
                x.TrainerId,
                x.SubjectId
            }).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet("GetEnrollmentInformation/{Id}")]
        public async Task<IActionResult> GetEnrollmentInformation(Guid Id)
        {
            var includeProperties = new string[] { "Trainer", "Subject", "PreEnrollmentQuestions" };

            var traineerSubjectmappedObj = await _traineerSubjectApplicationServices
                .GetAsync(x => x.Id == Id, 0, includeProperties);
            var result = traineerSubjectmappedObj.Select(x => new
            {
                SubjectName = x.Subject.Name,
                SubjectId = x.Subject.Id,
                TrainerId = x.Trainer.Id,
                TrainerName = x.Trainer.Name,
                StartDate = x.StartDate.ToShortDateString(),
                EndDate = x.EndDate.ToShortDateString(),
            }).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet("GetEnrollmentQuestions/{Id}")]
        public async Task<IActionResult> GetEnrollmentQuestions(Guid Id)
        {
            var includeProperties = new string[] { "Trainer", "Subject", "PreEnrollmentQuestions" };

            var traineerSubjectmappedObj = await _traineerSubjectApplicationServices
                .GetAsync(x => x.Id == Id, 0, includeProperties);
            var result = traineerSubjectmappedObj.Select(x => new
            {
                SubjectName = x.Subject.Name,
                SubjectId = x.Subject.Id,
                TrainerId = x.Trainer.Id,
                TrainerName = x.Trainer.Name,
                StartDate = x.StartDate.ToShortDateString(),
                EndDate = x.EndDate.ToShortDateString(),
                PreEnrollmentQuestions = x.PreEnrollmentQuestions.Where(s => s.IsActive).Select(p => new
                {
                    p.Question,
                    p.Id,
                    p.QuestionWeight
                })
            }).FirstOrDefault();

            return Ok(result);
        }

        #endregion

        #region Put APIs
        [HttpGet("ToggleTraining/{Id}")]
        public async Task<IActionResult>ToggleTraining(Guid Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var getTraining = await _traineerSubjectApplicationServices.FindAsync(Id);
            if (getTraining!=null)
            {
                getTraining.IsActive = getTraining.IsActive == true ? false : true;
                getTraining.UpdatedDate = DateTime.Now;
                getTraining.UpdatedBy = claimModel.UserId;
                var result = await _traineerSubjectApplicationServices.UpdateAsync(getTraining);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TraineerSubjectModel traineerSubjectModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existingRecord = await _traineerSubjectApplicationServices.FindAsync(traineerSubjectModel.Id);
            if (existingRecord == null)
                return Ok(false);

            existingRecord.UpdatedBy = claimModel.UserId;
            existingRecord.UpdatedDate = DateTime.Now;
            existingRecord.TrainerId = traineerSubjectModel.TrainerId;
            existingRecord.SubjectId = traineerSubjectModel.SubjectId;
            existingRecord.StartDate = traineerSubjectModel.StartDate;
            existingRecord.EndDate = traineerSubjectModel.EndDate;
            existingRecord.Instructions = traineerSubjectModel.Instructions;

            var result = await _traineerSubjectApplicationServices.UpdateAsync(existingRecord);
            return Ok(result);
        }
        #endregion

        #region Post APIs
        [HttpPost]
        public async Task<IActionResult> Post(TraineerSubjectModel traineerSubjectModel)
        {
            var dateDiffrence = DateTime.Compare(traineerSubjectModel.EndDate, traineerSubjectModel.StartDate);
            if (dateDiffrence < 0)
            {
                return Ok("End Date can't less than Start Date");
            }

            //Check Traineer already mapped with same subject on same date
            var oldMappingObj = await _traineerSubjectApplicationServices.GetAsync(x => x.TrainerId == traineerSubjectModel.TrainerId && x.SubjectId == traineerSubjectModel.SubjectId
            && x.StartDate == traineerSubjectModel.StartDate && x.IsActive);
            if (oldMappingObj.Count > 0)
            {
                return Ok("Requested Traineer already mapped with requested subject with requested start date.");
            }
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var traineerSubjectRequestedObj = new TrainerSubjectMapping
            {
                CreatedDate = DateTime.Now,
                IsActive = true,
                SubjectId = traineerSubjectModel.SubjectId,
                TrainerId = traineerSubjectModel.TrainerId,
                CreatedBy = claimModel.UserId,
                StartDate = traineerSubjectModel.StartDate,
                EndDate = traineerSubjectModel.EndDate,
                Instructions = traineerSubjectModel.Instructions
            };
            var result = await _traineerSubjectApplicationServices.AddAsync(traineerSubjectRequestedObj);

            if (result)
            {
                //Send email to Traineer with details
            }
            return Ok(result);
        }
        #endregion
    }
}