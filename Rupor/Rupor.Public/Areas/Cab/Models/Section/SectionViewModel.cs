using Rupor.Public.Models;
using Rupor.Services.Core.Base.Models;
using System;
using System.Collections.Generic;

namespace Rupor.Public.Areas.Cab.Models.Section
{
    public class SectionViewModel : BaseModel
    {
        public IEnumerable<SectionDto> Sections { get; set; }

        public SectionViewModel()
        {

        }
               
    }

    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool OnTop { get; set; }
    }
}