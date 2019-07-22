using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/MyProfile")]
    [ApiController]
    public class MyProfileController : ControllerBase
    {
        #region Private Variables
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Constructor
        public MyProfileController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #endregion

        #region APIs

        #region Get APIs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            var userDetails = await _userManager.FindByIdAsync(claimModel.UserId);
            var result = new
            {
                userDetails.Id,
                userDetails.FirstName,
                userDetails.MiddleName,
                userDetails.LastName,
                userDetails.UserName,
                userDetails.Email,
                userDetails.EmailConfirmed,
                userDetails.BirthDay,
                userDetails.PhoneNumber,
                userDetails.PhoneNumberConfirmed,
            };

            return Ok(result);
        }
        #endregion

        #region Put APIs

        #endregion

        #region Patch APIs

        #endregion

        #region Post APIs

        #endregion

        #region Delete APIs

        #endregion

        #endregion
    }
}