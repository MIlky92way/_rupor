using Rupor.Domain.Entities.Section;
using Rupor.Public.Models.Dto;
using Rupor.Services.Core.Common;
using System.Collections.Generic;
using System.Linq;

namespace Rupor.Public.Models.Category
{
    public class AllViewModel
    {
        private IServiceCore service;
        public IEnumerable<CategoryDto> Categories { get; set; }

        public AllViewModel()
        {

        }

        public AllViewModel(IServiceCore service)
        {
            
            var countSections = service.SectionService.Where(c => c.IsActive && !c.IsDelete && !c.IsDefault).Count();

            if (countSections > 0)
                Categories = service.SectionService.Where(c => c.IsActive && !c.IsDelete && !c.IsDefault)
                    .Select(c => new CategoryDto { Id = c.Id, Name = c.Name, ImageId = c.ImageId })
                    .ToList();
            else
                Categories = service.SectionService.Where(c => c.IsActive && !c.IsDelete && c.IsDefault)
                    .Select(c => new CategoryDto { Id = c.Id, Name = c.Name, ImageId = c.ImageId })
                    .ToList();
        }
    }
}