using Rupor.Services.Core.Base.Models;
using Rupor.Services.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rupor.Public.Areas.Cab.Models.ResSection
{
    public class ResSectionIndexViewModel : BaseModel
    {
        public IEnumerable<ResSectionDto> ResSections { get; set;
        }
        public ResSectionIndexViewModel()
        {

        }

        public void Init(IServiceCore service)
        {
            ResSections = service
                .AppResourceSectionService
                .Select(s => new ResSectionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    DateCreate = s.DateCreate
                })
                .ToList();
        }

    }

    public class ResSectionDto
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}