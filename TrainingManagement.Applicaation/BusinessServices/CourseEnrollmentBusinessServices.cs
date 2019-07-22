using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.RepositoryService;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public class CourseEnrollmentBusinessServices : TrainingManagementBusinessService<CourseEnrollment>,
        ICourseEnrollmentBusinessServices
    {
        public CourseEnrollmentBusinessServices(Func<ITrainingManagementUnitOfWork> iCourseEnrollmentBusinessServices) :
            base(iCourseEnrollmentBusinessServices)
        {

        }

        public async Task<CourseEnrollment> FindAsync(Guid id)
        {
            using (var repositories = RepositoriesFactory.Invoke())
            {
                return await repositories.GetRepositoryServiceSqlServerEf<CourseEnrollment>().FindAsync(id);
            }
        }
    }
}
