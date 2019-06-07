using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Common;
using System.Linq;
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
        public int AllowSections { get; set; }
        public int AllowOnTopSection { get; set; }
        public int TotalAll { get; set; }
        public int TotalOnTop { get; set; }
        public int TotalOther { get; set; }

        /// <summary>
        /// Отображает переполнение разделов.
        /// </summary>
        public bool Overflow { get; set; }
        /// <summary>
        /// Переполнение разделов в шапке
        /// </summary>
        public bool OverflowOnTop { get; set; }

        public SectionEditModel()
        {

        }

        public SectionEditModel(IServiceCore service, int id = 0)
        {
            var settings = service.SectionSettingsService.Settings;

            Overflow = service.SectionService
                .Where(s=>s.IsActive && !s.IsDefault && !s.IsDelete)
                .Count() >= settings.MaxAllowedSections;
            OverflowOnTop = service
                .SectionService
                .Where(x => x.IsActive && !x.IsDefault && !x.IsDelete && x.OnAside)
                .Count() >= settings.MaxAllowedSectionsOnTop;

            TotalAll = service.SectionService
                .Where(s => s.IsActive && !s.IsDelete && !s.IsDefault)
                .Count();

            TotalOnTop = service.SectionService
                .Where(s => s.IsActive && !s.IsDelete && !s.IsDefault && s.OnAside)
                .Count();

            TotalOther = service.SectionService
                .Where(s => s.IsActive && !s.IsDelete && !s.IsDefault && !s.OnAside)
                .Count();

            AllowSections = settings.MaxAllowedSections;
            AllowOnTopSection = settings.MaxAllowedSectionsOnTop;
            if (id > 0)
            {
                var section = service.SectionService[id];
                if (section != null)
                {
                    Id = section.Id;
                    Name = section.Name;
                    Description = section.Description;
                    OnTop = section.OnAside;
                    IsActive = section.IsActive;
                    IsDelete = section.IsDelete;
                    ImageId = section.ImageId;
                }
            }
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
                OnAside = model.OnTop
            };
            return entity;
        }
    }
}