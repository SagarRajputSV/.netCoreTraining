using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class ApplicationVersionApplicationService : TrainingManagementApplicationService<ApplicationVersion>,
        IApplicationVersionApplicationService
    {
        private readonly IApplicationVersionBusinessServices _applicationVersionBusinessServices;
        public ApplicationVersionApplicationService(IApplicationVersionBusinessServices applicationVersionBusinessServices)
            : base(applicationVersionBusinessServices)
        {
            _applicationVersionBusinessServices = applicationVersionBusinessServices;
        }

        public async Task<ApplicationVersion> FindAsync(Guid id)
        {
            return await _applicationVersionBusinessServices.FindAsync(id);
        }

    }
}
