using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class CourseEnrollmentApplicationServices : TrainingManagementApplicationService<CourseEnrollment>,
        ITrainingSubjectApplicationService
    {
        private readonly ICourseEnrollmentBusinessServices _courseEnrollmentBusinessServices;
        public CourseEnrollmentApplicationServices(ICourseEnrollmentBusinessServices courseEnrollmentBusinessServices)
            : base(courseEnrollmentBusinessServices)
        {
            _courseEnrollmentBusinessServices = courseEnrollmentBusinessServices;
        }

        public async Task<CourseEnrollment> FindAsync(Guid id)
        {
            return await _courseEnrollmentBusinessServices.FindAsync(id);
        }
    }
}
