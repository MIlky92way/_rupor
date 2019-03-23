using Rupor.Domain.Entities.Base;
using Rupor.Domain.Entities.File;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.User
{
    [Table("UserProfile")]
    public class ProfileEntity : BaseEntity
    {
        public ProfileEntity()
        {
            //Files = new HashSet<FileEntity>();
        }

        public DateTime LastAuth { get; set; }
        [MaxLength(120)]
        public string GivenName { get; set; }
        [MaxLength(120)]
        public string FamilyName { get; set; }
        [MaxLength(120)]
        public string Patronymic { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(128)]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public UserEntity Owner { get; set; }
        public string FileNameOriginal { get; set; }
        public string FileNameMin { get; set; }
        public int? AccessLevelToImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Email { get; set; }

        public int? OriginalPictureId { get; set; }
        public int? MinPictureId { get; set; }

        [ForeignKey("OriginalPictureId")]
        public FileEntity OriginalPicture { get; set; }
        [ForeignKey("MinPictureId")]
        public FileEntity MinPicture { get; set; }

    }
}
