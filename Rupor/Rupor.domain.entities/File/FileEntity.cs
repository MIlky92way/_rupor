using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.File
{
    public class FileEntity : BaseEntity
    {
        public bool Picture { get; set; }
        public string Alt { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
    }
}
