using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class SubjectApplicationService : TrainingManagementApplicationService<Subject>, 
        ISubjectApplicationService
    {
        private readonly ISubjectBusinessService _subjectBusinessServices;
        public SubjectApplicationService(ISubjectBusinessService subjectBusinessServices)
            : base(subjectBusinessServices)
        {
            _subjectBusinessServices = subjectBusinessServices;
        }

        public async Task<Subject> FindAsync(Guid id)
        {
            return await _subjectBusinessServices.FindAsync(id);
        }
    }
}
