using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Models;

namespace TrainingManagement.Application.BusinessServices
{
    public interface ITaineerSubjectBusinessServices : ITrainingManagementBusinessServices<TrainerSubjectMapping>
    {
        Task<TrainerSubjectMapping> FindAsync(Guid id);
    }
}
