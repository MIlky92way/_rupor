using Rupor.Domain.Entities.Section;
using System;
using System.Web;

namespace Rupor.Public.Areas.Cab.Models.Section
{
    public class SectionEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool OnTop { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public HttpPostedFileBase SectionImage { get; set; }
        public int? ImageId { get; set; }

        public SectionEditModel()
        {

        }

        public SectionEditModel(SectionEntity section)
        {
            Id = section.Id;
            Name = section.Name;
            Description = section.Description;
            OnTop = section.OnTop;
            IsActive = section.IsActive;
            IsDelete = section.IsDelete;
            ImageId = section.ImageId.GetValueOrDefault();
        }

        internal SectionEntity MapFrom(SectionEditModel model)
        {
            SectionEntity entity = new SectionEntity
            {
                Description = model.Description,
                Id = model.Id,
                IsActive = model.IsActive,
                ImageId = model.ImageId,
                IsDelete = model.IsDelete,
                Name = model.Name,
                OnTop = model.OnTop
            };
            return entity;
        }
    }
}