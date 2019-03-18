using Rupor.Domain.Context;
using Rupor.Domain.Entities.File;
using Rupor.Logik.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.File
{
    public class FileService : IFileService
    {
        public FileEntity this[int id] => Get(id);

        public FileEntity Edit(FileEntity model)
        {
            FileEntity entity = null;

            using (var context = new RuporDbContext())
            {
                if (model.Id > 0)
                {
                    entity = context.Files.FirstOrDefault(f => f.Id == model.Id);
                }
                else
                {
                    entity = new FileEntity();
                    context.Files.Add(entity);
                }

                entity.FileExtension = model.FileExtension;
                entity.FileName = model.FileName;
                entity.Name = model.Name;
                entity.Picture = model.Picture;
                entity.Alt = model.Alt;

                context.SaveChanges();
            }
            return entity;
        }

        public IEnumerable<FileEntity> Get(Expression<Func<FileEntity, bool>> expr)
        {
            ICollection<FileEntity> files = new List<FileEntity>();
            using (var context = new RuporDbContext())
            {
                files = context.Files.Where(expr).ToList();
            }

            return files;
        }

        public IEnumerable<FileEntity> Get()
        {
            throw new NotImplementedException("Заглушка");
        }

        public void Remove(int id)
        {
            using (var context = new RuporDbContext())
            {
                var file = context.Files.FirstOrDefault(f => f.Id == id);
                context.Files.Remove(file);
                context.SaveChanges();
            }
        }

        public void Remove(int[] ids)
        {
            using (var context = new RuporDbContext())
            {
                var files = context.Files.Where(f => ids.Any(id => id == f.Id));
                context.Files.RemoveRange(files);
                context.SaveChanges();
            }
        }

        public void Remove(FileEntity entry)
        {
            using (var context = new RuporDbContext())
            {
                context.Files.Attach(entry);
                context.Files.Remove(entry);
                context.SaveChanges();
            }
        }

        private FileEntity Get(int id)
        {
            FileEntity file = null;
            if (id > 0)
            {
                using (var context = new RuporDbContext())
                {
                    file = context.Files.FirstOrDefault(x => x.Id == id);
                }
            }

            return file;
        }
    }
}
