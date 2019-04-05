using Rupor.Domain.Entities.File;
using Rupor.Services.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.File
{
    public interface IFileService : IRuporService<FileEntity>

    {
        IEnumerable<FileEntity> Get(Expression<Func<FileEntity, bool>> expr);
        void Remove(int id);
        void Remove(int[] ids);
    }
}
