using System;
using System.Threading.Tasks;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface ISubjectApplicationService: ITrainingManagementApplicationService<Subject>
    {
        Task<Subject> FindAsync(Guid id);
    }
}
