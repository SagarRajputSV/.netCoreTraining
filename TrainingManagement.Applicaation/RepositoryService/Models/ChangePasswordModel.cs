﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        //public string ConfirmPassword { get; set; }
    }
}
