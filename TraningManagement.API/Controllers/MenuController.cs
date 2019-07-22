using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingManagement.API.UtilitiesHelper;
using TrainingManagement.Application.Models;

namespace TrainingManagement.API.Controllers
{
    [Route("api/menu")]
    [ApiController]
    //[Authorize]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var claimModel = UtilitiesHelpers.SetClaims(User);
            List<MenuModel> menuObj = null;

            menuObj = AnonymousUserMenu();

            if (claimModel != null)
            {
                var authorizeMenuObj = AuthorizeUserMenu();
                menuObj.AddRange(authorizeMenuObj);
            }

            return Ok(menuObj);
        }

        private List<MenuModel> AnonymousUserMenu()
        {
            var menuObj = new List<MenuModel>
            {
                new MenuModel
                {
                    ActionName="Index",
                    ControllerName = "Home",
                    DisplayName = "Home",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName = "Index",
                    ControllerName = "Courses",
                    DisplayName = "Courses",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName="Index",
                    ControllerName = "TraineeFeedback",
                    DisplayName = "Feedback",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName="Index",
                    ControllerName = "ScheduledTraining",
                    DisplayName = "Scheduled Training",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                //new MenuModel
                //{
                //    ActionName="#",
                //    ControllerName = "#",
                //    DisplayName = "#",
                //    MenuType = "Menu",
                //    IsContainsSubMenu = false,
                //    IsActive = true,
                //    Visibility = "hidden"
                //},
                new MenuModel
                {
                    ActionName="Index",
                    ControllerName = "ContactUs",
                    DisplayName = "Contact us",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName="Index",
                    ControllerName = "Instructors",
                    DisplayName = "Instructors",
                    MenuType = "Menu",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName="Register",
                    ControllerName = "Account",
                    DisplayName = "Register",
                    MenuType = "Button",
                    IsContainsSubMenu = false,
                    IsActive = true
                },
                new MenuModel
                {
                    ActionName="Login",
                    ControllerName = "Account",
                    DisplayName = "Login",
                    MenuType = "Button",
                    IsContainsSubMenu = false,
                    IsActive = true
                }
            };
            return menuObj;
        }

        private List<MenuModel> AuthorizeUserMenu()
        {
            var menuObj = new List<MenuModel>
            {
                new MenuModel
                {
                    ActionName="#",
                    ControllerName = "#",
                    DisplayName = "Admin Mangement",
                    MenuType = "Menu",
                    IsContainsSubMenu = true,
                    IsForAdminOnly = true,
                    SubMenu = new List<MenuModel>
                    {
                        new MenuModel
                        {
                            ActionName = "Index",
                            ControllerName = "Subject",
                            DisplayName = "Subject",
                            MenuType = "Menu",
                            IsContainsSubMenu = false,
                            IsActive = true,
                            Title = "Subject Management"
                        },
                        new MenuModel
                        {
                            ActionName = "Index",
                            ControllerName = "Trainer",
                            DisplayName = "Trainer",
                            MenuType = "Menu",
                            IsContainsSubMenu = false,
                            IsActive = true,
                            Title = "Trainer Management"
                        },
                        new MenuModel
                        {
                            ActionName = "Index",
                            ControllerName = "TraineerSubject",
                            DisplayName = "TSM",
                            MenuType = "Menu",
                            IsContainsSubMenu = false,
                            IsActive = true,
                            Title = "Trainer Subject Management"
                        },
                        new MenuModel
                        {
                            ActionName = "Index",
                            ControllerName = "PreEnrollmentQuestionMapping",
                            DisplayName = "PEQM",
                            MenuType = "Menu",
                            IsContainsSubMenu = false,
                            IsActive = true,
                            Title = "Pre-Enrollment Question Management"
                        }
                    },
                    IsActive = true
                }
            };
            return menuObj;
        }
    }
}