using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool OnTop { get; set; }
        public bool IsDefault { get; set; }
        public string Alias { get; set; }
        public int? ImageId { get; internal set; }

        //TODO: CategoryDto -> Articles 
        //TODO: CategoryDto -> Authors
    }
}