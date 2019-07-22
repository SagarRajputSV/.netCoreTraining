using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.ApplicationServices
{
    public interface ITrainingManagementApplicationService<T> : IApplicationService<T> where T : class
    {

    }
}
