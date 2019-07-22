using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class TopHeaderInformationApplicationServices:TrainingManagementApplicationService<TopHeaderInformation>,
        ITopHeaderInformationApplicationServices
    {
        private readonly ITopHeaderInformationBusinessServices _topHeaderInformationBusinessServices;
        public TopHeaderInformationApplicationServices(ITopHeaderInformationBusinessServices topHeaderInformationBusinessServices)
            : base(topHeaderInformationBusinessServices)
        {
            _topHeaderInformationBusinessServices = topHeaderInformationBusinessServices;
        }
    }
}
