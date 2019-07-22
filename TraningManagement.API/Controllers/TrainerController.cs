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
    [Route("api/trainer")]
    [ApiController]
    [Authorize]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerApplicationServices _iTrainerApplicationServices;
        public TrainerController(ITrainerApplicationServices iTrainerApplicationServices)
        {
            _iTrainerApplicationServices = iTrainerApplicationServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var resultObj = await _iTrainerApplicationServices.GetAllAsync();
            var result = resultObj.Select(x => new
            {
                x.Id,
                x.Name,
                x.Experience,
                x.IsActive,
                x.AboutTrainer,
                x.Skills,
                x.Email,
                x.FacebookId,
                x.InstagramId,
                x.TwitterId
            });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TrainerModel trainer)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var existingTrainer = await _iTrainerApplicationServices.FindAsync(trainer.Id);
            if (existingTrainer != null)
            {
                existingTrainer.Name = trainer.Name;
                existingTrainer.Experience = trainer.Experience;
                existingTrainer.UpdatedDate = DateTime.UtcNow;
                existingTrainer.UpdatedBy = claimModel.UserId;
                existingTrainer.IsActive = trainer.IsActive;
                existingTrainer.Skills = trainer.Skills;
                existingTrainer.AboutTrainer = trainer.AboutTrainer;
                existingTrainer.Email = trainer.Email;
                existingTrainer.TwitterId = trainer.TwitterId;
                existingTrainer.FacebookId = trainer.FacebookId;
                existingTrainer.InstagramId = trainer.InstagramId;

                var result = await _iTrainerApplicationServices.UpdateAsync(existingTrainer);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpGet("ToggleTypeStatus/{id}")]
        public async Task<IActionResult> ToggelSubjectTypeStatus(Guid id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existObj = await _iTrainerApplicationServices.FindAsync(id);
            if (existObj!=null)
            {
                existObj.IsActive = existObj.IsActive == true ? false : true;
                existObj.UpdatedDate = DateTime.Now;
                existObj.UpdatedBy = claimModel.UserId;
                var result = await _iTrainerApplicationServices.UpdateAsync(existObj);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpGet("IsTrainerAlreadyRegistered/{email}")]
        public async Task<IActionResult> IsTrainerAlreadyRegistered(string email)
        {
            var existObj = await _iTrainerApplicationServices.GetAsync(t => t.Email == email);
            var result = existObj.Count > 0 ? true : false;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TrainerModel trainer)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var trainerObj = new Trainer
            {
                IsActive = trainer.IsActive,
                Name = trainer.Name,
                TrainerImage = trainer.TrainerImage,
                Experience = trainer.Experience,
                Skills = trainer.Skills,
                AboutTrainer = trainer.AboutTrainer,
                CreatedDate = DateTime.Now,
                Email = trainer.Email,
                CreatedBy = claimModel.UserId,
                InstagramId = trainer.InstagramId,
                FacebookId = trainer.FacebookId,
                TwitterId = trainer.TwitterId
            };
            var result = await _iTrainerApplicationServices.AddAsync(trainerObj);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _iTrainerApplicationServices.DeleteAsync(id);
            return Ok(result);
        }
    }
}