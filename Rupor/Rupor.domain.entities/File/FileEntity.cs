using Rupor.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.File
{
    /// <summary>
    /// Тип файла.
    /// </summary>
    public enum FileType
    {
        Document = 0,
        Image = 1,
        Archive = 2
    }
    /// <summary>
    /// Область контекста, в котором будет отоюражаться файл
    /// </summary>
    public enum FileArea
    {
        Profile,
        Article,
        Section,
        FeedChannel
    }



    [Table("File")]
    public class FileEntity : BaseEntity
    {
        /// <summary>
        /// Файлы отдающиеся как дефолтные (условия использования, нормативные документы, дефолтные картинки и т.д.) 
        /// </summary>
        public bool IsDefault { get; set; }
        public bool Picture { get; set; }
        [MaxLength(25)]
        public string Alt { get; set; }
        public string FileExtension { get; set; }
        [MaxLength(25)]
        public string FileName { get; set; }
        /// <summary>
        /// Имя файла с его расширением
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(515)]
        public string Description { get; set; }

        [MaxLength(120)]
        public string ContentType { get; set; }
        public FileType FileType { get; set; }
        public FileArea FileArea { get; set; }
    }
}
