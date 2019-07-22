using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class ApplicationVersionModel
    {
        public Guid Id { get; set; }
        public string APIVersion { get; set; }
        public string APIMajorChanges { get; set; }
        public bool IsAPIVeriosnActive { get; set; }
        public string UIVersion { get; set; }
        public string UIMajorChanges { get; set; }
        public bool IsUIVeriosnActive { get; set; }
        public bool IsActive { get; set; }
    }
}
