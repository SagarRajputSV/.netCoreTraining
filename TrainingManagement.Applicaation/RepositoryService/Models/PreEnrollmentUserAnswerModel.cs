using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.Models
{
    public class ListOfAnswerModel
    {
        public List<AnswerModel> AnswerModels { get; set; }
    }
    public class AnswerModel
    {
        public Guid Id { get; set; }
        public Guid CourseEnrollmentId { get; set; }
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public string Comments { get; set; }
        public string TimeTaken { get; set; }
    }
}
