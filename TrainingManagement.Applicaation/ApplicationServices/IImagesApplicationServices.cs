using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface IImagesApplicationServices: ITrainingManagementApplicationService<Images>
    {
        Task<Images> FindAsync(Guid id);
    }
}
