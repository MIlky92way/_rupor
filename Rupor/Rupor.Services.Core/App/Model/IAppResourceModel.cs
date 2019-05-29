using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.App.Model
{
    public interface IAppResourceModel
    {
        string Value { get; set; }
        string Key { get; set; }
        int Id { get; set; }
        int SectionId { get; set; }
    }
}
