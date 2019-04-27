using Rupor.Public.Models.Dto;
using Rupor.Services.Core.Common;
using Rupor.Services.Core.Section;
using System.Linq;

namespace Rupor.Public.Models.Category
{
    public class CategoryIndexViewModel
    {
        public CategoryDto Category { get; set; }
        private IServiceCore service;
        public CategoryIndexViewModel()
        {

        }
        public CategoryIndexViewModel(IServiceCore service, int id)
        {
            this.service = service;

            var cat = service.SectionService
                    .OrderByDescending(c => c.DateCreate)
                    .FirstOrDefault(c => id == 0 || c.Id == id);

            Category = new CategoryDto
            {
                Id = cat.Id,
                Name = cat.Name,
                ImageId = cat.ImageId
            };
        }
    }



}