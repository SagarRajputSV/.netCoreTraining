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
    [Authorize]
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectApplicationService _iSubjectApplicationServices;
        public SubjectController(ISubjectApplicationService iSubjectApplicationServices)
        {
            _iSubjectApplicationServices = iSubjectApplicationServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var subjectObj = await _iSubjectApplicationServices.GetAllAsync();
            var result = subjectObj.Select(x => new
            {
                x.Id,
                x.Name,
                x.IsActive,
                x.IsFree,
                x.Prerequisites,
                x.Description,
                x.Price
            });
            return Ok(result);
        }

        [HttpGet("ToggleActiveStatus/{id}")]
        public async Task<IActionResult> ToggelSubjectStatus(Guid id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existObj = await _iSubjectApplicationServices.FindAsync(id);
            if (existObj!=null)
            {
                existObj.IsActive = existObj.IsActive == true ? false : true;
                existObj.UpdatedDate = DateTime.Now;
                existObj.UpdatedBy = claimModel.UserId;
                var result = await _iSubjectApplicationServices.UpdateAsync(existObj);
                return Ok(result);
            }
            return Ok(false);   
        }

        [HttpGet("ToggleTypeStatus/{id}")]
        public async Task<IActionResult> ToggelSubjectTypeStatus(Guid id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existObj = await _iSubjectApplicationServices.FindAsync(id);
            if (existObj!=null)
            {
                existObj.IsFree = existObj.IsFree == true ? false : true;
                existObj.UpdatedDate = DateTime.Now;
                existObj.UpdatedBy = claimModel.UserId;
                var result = await _iSubjectApplicationServices.UpdateAsync(existObj);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpPut]
        public async Task<IActionResult> Put(SubjectModel subject)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var existObj = await _iSubjectApplicationServices.FindAsync(subject.Id);
            if (existObj!=null)
            {
                existObj.IsFree = subject.IsFree;
                existObj.IsActive = subject.IsActive;
                existObj.Name = subject.Name;
                existObj.Description = subject.Description;
                existObj.Prerequisites = subject.Prerequisites;
                existObj.UpdatedDate = DateTime.Now;
                existObj.UpdatedBy = claimModel.UserId;
                var result = await _iSubjectApplicationServices.UpdateAsync(existObj);
                return Ok(result);
            }
            return Ok(false);
        }

        [HttpDelete]
        public async Task<IActionResult>Delete(Guid id)
        {
            var result = await _iSubjectApplicationServices.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SubjectModel subject)
        {
            var subjectObj = new Subject
            {
                IsActive = true,
                Name = subject.Name,
                Description = subject.Description,
                IsFree = subject.IsFree,
                Prerequisites = subject.Prerequisites,
                CreatedBy = "90515121-68b5-4dee-9b59-1977f9d51043",
                CreatedDate = DateTime.Now
            };

            var result = await _iSubjectApplicationServices.AddAsync(subjectObj);
            return Ok(result);
        }
    }
}