using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Models
{
    public class Images:BaseEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public string DirectoryPath { get; set; }
        public string ContainerName { get; set; }
        public bool IsActive { get; set; }
    }
}
