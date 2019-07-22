using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.EmailHelper;
using TrainingManagement.Application.Models;
using TrainingManagement.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettings;
        private readonly SmtpConfigurationModel _smtpConfigurationModel;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<AccountController> _logger;
        private readonly CryptoEngineModel _cryptoEngineModel;
        public AccountController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings,
            IOptions<SmtpConfigurationModel> smtpConfigurationModel,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AccountController> logger,
            IOptions<CryptoEngineModel> cryptoEngineModel)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _applicationSettings = appSettings.Value;
            _smtpConfigurationModel = smtpConfigurationModel.Value;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _cryptoEngineModel = cryptoEngineModel.Value;
        }

        [AllowAnonymous]
        [HttpGet("IsUserNameAlreadyRegistered/{username}")]
        public async Task<IActionResult> IsUserNameAlreadyRegistered(string username)
        {
            var resultObj = await _userManager.FindByNameAsync(username);
            var result = resultObj == null ? true : false;
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("IsEmailAlreadyRegistered/{email}")]
        public async Task<IActionResult> IsEmailAlreadyRegistered(string email)
        {
            var resultObj = await _userManager.FindByEmailAsync(email);
            var result = resultObj == null ? true : false;
            return Ok(result);
        }

        [AllowAnonymous]
        /// <summary>
        /// Register user with minimum information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> AddUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = CryptoEngine.Decrypt(model.UserName, _cryptoEngineModel.Key);
                var password = CryptoEngine.Decrypt(model.Password, _cryptoEngineModel.Key);
                var email = CryptoEngine.Decrypt(model.Email, _cryptoEngineModel.Key);

                _logger.LogInformation("UserName:" + userName);
                _logger.LogInformation("Password:" + password);

                model.ReturnUrl = model.ReturnUrl.Replace("$value", model.Email);

                model.UserName = userName;
                model.Password = password;
                model.Email = email;

                ApplicationUser user = new ApplicationUser
                {
                    //FirstName = model.FirstName,
                    //MiddleName = model.MiddleName,
                    //LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var returnUrl = UtilitiesHelpers.GetUrlWithProtocols(model.ReturnUrl, HttpContext.Request.IsHttps);
                            var generatedLink = returnUrl.Replace("$verificationLink", UtilitiesHelpers.Base64ForUrlEncode(code));

                            //Send Email for account confirmation
                            string subject = "Email Confirmation";
                            string body = System.IO.File.ReadAllText(_hostingEnvironment.WebRootPath + @"\EmailTemplate.html")
                                .Replace("$title", "Email Confirmation")
                                .Replace("$user", string.IsNullOrWhiteSpace(user.FirstName) ? user.Email : user.FirstName + " " + user.LastName)
                                .Replace("$content", EmailContent.AddUser)
                                .Replace("$anchorBody", "Confirm Email")
                                .Replace("$urlClass", "emailButton")
                                .Replace("$link", generatedLink);

                            EmailHelper emailHelper = new EmailHelper(_smtpConfigurationModel, _hostingEnvironment, _logger);
                            bool mailStatus = await emailHelper.SendEmailAsync(model.Email, model.Email, subject, body, "rajesh.kushwaha@softvision.com");
                            return Json(new { Success = result.Succeeded, Message = String.Join(", ", result.Errors.Select(x => x.Description)) });
                        }
                    }
                }
                else
                {
                    return Json(new { Success = result.Succeeded, Message = String.Join(", ", result.Errors.Select(x => x.Description)) });
                }
            }
            return Json(new { Success = false, Message = "Some error occured, please contact to application admin." });

        }

        [HttpPut]
        public async Task<IActionResult> EditUser(string id, EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    //user.FullName = model.Name;
                    user.Email = model.Email;
                    string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                    string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return Ok(true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Ok(false);
        }

        [HttpDelete]
        //[AllowAnonymous]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return Ok(true);
                    }
                }
            }
            return Ok(false);
        }
        //[HttpPost("Register")]
        //public async Task<IActionResult> Post(UserModel model)
        //{
        //    var applicationUser = new ApplicationUser
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FullName = model.FullName,
        //    };

        //    try
        //    {
        //        var result = await _userManager.CreateAsync(applicationUser, model.Password);
        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var userName = CryptoEngine.Decrypt(model.UserName, _cryptoEngineModel.Key);
            var password = CryptoEngine.Decrypt(model.Password, _cryptoEngineModel.Key);

            _logger.LogInformation("UserName:" + userName);
            _logger.LogInformation("Password:" + password);

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userName);
            }
            var roleDetails = await _userManager.GetRolesAsync(user);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    string token = GenerateToken(user, roleDetails);
                    //return Ok(new { token });
                    return Json(new { Success = true, Message = token });
                }
                return Json(new { Success = false, Message = "Username or Password doesn't match." });
            }
            return null;
        }

        [AllowAnonymous]
        [HttpPost("SendVerificationMail")]
        public async Task<IActionResult> SendVerificationMail(SendVerificationMailModel sendVerificationMailModel)
        {
            var emailId = CryptoEngine.Decrypt(sendVerificationMailModel.VerificationId, _cryptoEngineModel.Key);
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var returnUrl = UtilitiesHelpers.GetUrlWithProtocols(sendVerificationMailModel.ReturnUrl, HttpContext.Request.IsHttps);
                    var generatedLink = returnUrl.Replace("$verificationLink", UtilitiesHelpers.Base64ForUrlEncode(code))
                        .Replace("$value", sendVerificationMailModel.VerificationId);

                    //Send Email for account confirmation
                    string subject = "Account Verification";
                    string body = System.IO.File.ReadAllText(_hostingEnvironment.WebRootPath + @"\EmailTemplate.html")
                        .Replace("$title", "Email Confirmation")
                        .Replace("$user", string.IsNullOrWhiteSpace(user.Email) ? "" : user.FirstName + " " + user.LastName)
                        .Replace("$content", EmailContent.UserVerification)
                        .Replace("$anchorBody", "Confirm Email")
                        .Replace("$urlClass", "emailButton")
                        .Replace("$link", generatedLink);


                    EmailHelper emailHelper = new EmailHelper(_smtpConfigurationModel, _hostingEnvironment, _logger);
                    bool mailStatus = await emailHelper.SendEmailAsync(user.Email, user.Email, subject, body, "rajesh.kushwaha@softvision.com");

                    string message = mailStatus == true ? "Verification email sent to registered email" : "Unable to sent email, please try again.";
                    return Json(new { Success = true, Message = message });
                }
                return Json(new { Success = true, Message = "Account already verified" });
            }

            return Json(new { Success = false, Message = "Account doesn't not exist" });
        }

        [AllowAnonymous]
        [HttpPost("VerifyAccount")]
        public async Task<IActionResult> VerifyAccount(SendVerificationMailModel sendVerificationMailModel)
        {
            var emailId = CryptoEngine.Decrypt(sendVerificationMailModel.VerificationId, _cryptoEngineModel.Key);
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user != null)
            {
                if (!user.EmailConfirmed)
                {
                    //var result = await _userManager.ConfirmEmailAsync(user, sendVerificationMailModel.Key);
                    var decodedValue = UtilitiesHelpers.Base64ForUrlDecode(sendVerificationMailModel.Key);

                    var result = await _userManager.ConfirmEmailAsync(user, decodedValue);
                    if (result.Succeeded)
                    {
                        return Json(new { Success = true, Message = "Thank you for confirming your email." });
                    }
                }
                return Json(new { Success = true, Message = "Account already verified" });
            }

            return Json(new { Success = false, Message = "Account doesn't not exist" });
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);

            var Id = CryptoEngine.Decrypt(changePasswordModel.UserName, _cryptoEngineModel.Key);
            var UserName = CryptoEngine.Decrypt(changePasswordModel.UserName, _cryptoEngineModel.Key);
            var OldPassword = CryptoEngine.Decrypt(changePasswordModel.OldPassword, _cryptoEngineModel.Key);
            var NewPassword = CryptoEngine.Decrypt(changePasswordModel.NewPassword, _cryptoEngineModel.Key);

            var user = await _userManager.FindByEmailAsync(Id);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPassword = _userManager.ResetPasswordAsync(user,code, "Nokia@12345");
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, OldPassword, NewPassword);
            if (changePasswordResult.Succeeded)
            {
                return Json(new { Success = true, Message = "Password Changes Successfully" });
            }
            var errors = string.Join(", ", changePasswordResult.Errors.Select(x => x.Description).ToList());

            return Json(new { Success = false, Message = "", Error = errors, Code = 400 });
        }

        [NonAction]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string Id)
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            if (claimModel.RoleName== "Administrator")
            {
                var user = await _userManager.FindByEmailAsync(Id);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetPassword = await _userManager.ResetPasswordAsync(user, code, "Nokia@12345");
                if (resetPassword.Succeeded)
                {
                    return Json(new { Success = true, Message = "Password Changes Successfully" });
                }
                var errors = string.Join(", ", resetPassword.Errors.Select(x => x.Description).ToList());

                return Json(new { Success = false, Message = "", Error = errors, Code = 400 });
            }
            return Json(new { Success = false, Message = "Unauthorized", Error = "", Code = 401 });
        }

        #region Private Methods
        private string GenerateToken(ApplicationUser user, System.Collections.Generic.IList<string> roleDetails)
        {
            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                                {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("UserRole",roleDetails.FirstOrDefault().ToString()),
                        new Claim("FirstName",string.IsNullOrWhiteSpace(user.FirstName)?"":user.FirstName),
                        new Claim("MiddleName",string.IsNullOrWhiteSpace(user.MiddleName)?"":user.MiddleName),
                        new Claim("LastName",string.IsNullOrWhiteSpace(user.LastName)?"":user.LastName),
                        new Claim("Email",user.Email),
                        new Claim("EmailConfirmed",user.EmailConfirmed.ToString()),
                        new Claim("AccessFailedCount",user.AccessFailedCount.ToString()),
                        new Claim("PhoneNumber",string.IsNullOrWhiteSpace(user.PhoneNumber)?"":user.PhoneNumber.ToString()),
                        new Claim("BirthDay",user.BirthDay.ToString()),
                        new Claim("UserName",user.UserName)
                                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JWT_Secret)),
                                SecurityAlgorithms.HmacSha256Signature)
            };

            //var authProperties = new AuthenticationProperties { };

            //var claimsIdentity = new ClaimsIdentity(
            //        tokenDiscriptor.Subject.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity),
            //        authProperties);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDiscriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
        #endregion
    }
}