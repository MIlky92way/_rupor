using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Profile.Models
{
    public interface IProfileSettingsModel
    {
        int Id { get; set; }
        int ProfileId { get; set; }
        ICollection<int> SelectedSectionIds { get; set; }
        ICollection<int> SelectedAuthorIds { get; set; }
        ICollection<int> SelectedChannelIds { get; set; }
    }
}
