using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class TrainerApplicationServices: TrainingManagementApplicationService<Trainer>,
        ITrainerApplicationServices
    {
        private readonly ITrainerBusinessServices _trainerBusinessServices;
        public TrainerApplicationServices(ITrainerBusinessServices trainerBusinessServices)
            : base(trainerBusinessServices)
        {
            _trainerBusinessServices = trainerBusinessServices;
        }

        public async Task<Trainer> FindAsync(Guid id)
        {
            return await _trainerBusinessServices.FindAsync(id);
        }
    }
}
