using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class TaineerSubjectApplicationServices : TrainingManagementApplicationService<TrainerSubjectMapping>,
        ITaineerSubjectApplicationServices
    {
        private readonly ITaineerSubjectBusinessServices _taineerSubjectBusinessServices;
        public TaineerSubjectApplicationServices(ITaineerSubjectBusinessServices taineerSubjectBusinessServices)
            : base(taineerSubjectBusinessServices)
        {
            _taineerSubjectBusinessServices = taineerSubjectBusinessServices;
        }

        public async Task<TrainerSubjectMapping> FindAsync(Guid id)
        {
            return await _taineerSubjectBusinessServices.FindAsync(id);
        }
    }
}
