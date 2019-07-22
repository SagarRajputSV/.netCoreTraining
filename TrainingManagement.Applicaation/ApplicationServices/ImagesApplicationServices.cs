using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingManagement.Application.BusinessServices;
using TrainingManagement.Models;

namespace TrainingManagement.Application.ApplicationServices
{
    public class ImagesApplicationServices : TrainingManagementApplicationService<Images>,
        IImagesApplicationServices
    {
        private readonly IImagesBusinessService _imagesBusinessService;
        public ImagesApplicationServices(IImagesBusinessService imagesBusinessService)
        : base(imagesBusinessService)
        {
            _imagesBusinessService = imagesBusinessService;
        }

        public async Task<Images> FindAsync(Guid id)
        {
            return await _imagesBusinessService.FindAsync(id);
        }
    }
}
