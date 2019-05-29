using Rupor.Domain.Entities.Section;
using Rupor.Services.Core.Base;
using Rupor.Services.Core.Base.Models;
using System;
using System.Collections.Generic;

namespace Rupor.Services.Core.Section
{
    public interface ISectionService : IEnumerable<SectionEntity>, IRuporService<SectionEntity>
    {
        IEnumerable<SectionEntity> Get(BaseModel filterModel = null, Func<SectionEntity, bool> expr = null);
        IEnumerable<SectionEntity> Get(Func<SectionEntity, bool> expr = null);
        IEnumerable<SectionEntity> GetDefaults();
        void Remove(int id);
        void ChangeActiveState(int id, bool? isActive);
        void ChangeDeleteState(int id, bool? isDelete);
    }
}
