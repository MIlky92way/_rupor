using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Base;
using Rupor.Services.Core.Base.Models;
using System;
using System.Collections.Generic;

namespace Rupor.Services.Core.Section
{
    public interface ISectionService : IRuporService<SectionEntity>
    {
        IEnumerable<SectionEntity> Get(BaseModel filterModel, Func<SectionEntity, bool> expr = null);
    }
}
