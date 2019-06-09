using Rupor.Domain.Entities.Section;
using Rupor.Public.Models.Dto;
using Rupor.Services.Core.Section;
using System.Collections.Generic;
using System.Linq;

namespace Rupor.Public.Models.Component
{
    public class CategoryViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(ISectionService sectionService, bool onAside = true)
        {
            if (sectionService.Where(s => !s.IsDefault).Count() > 0)
            {
                Categories = sectionService
                    .Where(s => !s.IsDefault && s.OnAside == onAside && s.IsActive && !s.IsDelete)
                    .Select(s => MapFrom(s)).ToList();
            }
            else
            {
                Categories = sectionService.GetDefaults()
                    .Select(s => MapFrom(s));
            }
        }

        private CategoryDto MapFrom(SectionEntity section)
        {
            return new CategoryDto
            {
                Id = section.Id,
                Name = section.Name
            };
        }
    }
      
}