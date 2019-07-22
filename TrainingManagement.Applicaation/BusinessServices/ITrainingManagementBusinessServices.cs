using System;
using System.Collections.Generic;
using System.Text;

namespace TrainingManagement.Application.BusinessServices
{ 
    public interface ITrainingManagementBusinessServices<T> : IBusinessService<T> where T : class
    {
    }
}
