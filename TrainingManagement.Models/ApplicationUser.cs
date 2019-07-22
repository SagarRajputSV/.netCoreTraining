using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
            : base()
        {
        }

        public ApplicationUser(string userName, string firstName,string middleName, string lastName, DateTime birthDay)
            : base(userName)
        {
            base.Email = userName;

            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.BirthDay = birthDay;
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string FullName => $"{this.FirstName} {this.LastName}";

        //public virtual ICollection<TraineeFeedback>  TraineeFeedbacks { get; set; }
    }
}
