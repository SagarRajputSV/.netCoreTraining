using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class MenuModel
    {
        public string DisplayName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string MenuType { get; set; }
        public bool IsContainsSubMenu { get; set; }
        public List<MenuModel> SubMenu { get; set; }
        public bool IsForAdminOnly { get; set; } = false;
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string Visibility { get; set; } = "visible";
    }
}
