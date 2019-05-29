using Rupor.Domain.Entities.Resources;
using Rupor.Services.Core.App.Model;
using System;

namespace Rupor.Public.Areas.Cab.Models.ResSection
{
    public class ResSectionEditViewModel : IAppResourceSectionModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Description { get; set; }
        public ResSectionEditViewModel()
        {

        }
        public ResSectionEditViewModel(AppResourceSectionEntity resSection)
        {
            Name = resSection.Name;
            Id = resSection.Id;
            DateCreate = resSection.DateCreate;
            Description = resSection.Description;
        }
    }
}