using Rupor.Domain.Entities.File;
using Rupor.Domain.Entities.User;
using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools.Additional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Infrastructure.FileTools
{
    public class FileTools
    {
        protected const string _RootDir = "~/uploads";

        public readonly HttpContextBase HttpContext;
        public HttpServerUtilityBase Server { get; }
        public IFileService FileService { get; }
        protected string[] NotSupportedExtensions { get; }

        public FileTools(HttpContextBase context, HttpServerUtilityBase server, IFileService fileSrv)
        {
            HttpContext = context;
            Server = server;
            FileService = fileSrv;

            NotSupportedExtensions = new string[] {
                "exe",
                "dll",
                "js",
                "css",
                "scss",
                "sass",
                "sql"
            };

        }

        /// <summary>
        /// Получение файла.
        /// </summary>
        /// <param name="path">Путь куда будет сохранен файл</param>
        /// <param name="id">Идентификатор файла (опционально)</param>
        /// <returns>FileStream</returns>
        public ToolsFileInfo GetFile(string path, int id = 0)
        {
            var result = new ToolsFileInfo();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                result.FileStream = stream;
                //TODO File from Db

                return result;
            }
        }

        public FileStream GetFile(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return stream;
            }
        }

        /// <summary>
        /// Сохранение набора файлов. 
        /// </summary>
        /// <param name="files">Набор сохраняемых файлов</param>
        /// <param name="path">Путь куда будет сохранен файл</param>
        /// <returns>Набор идентификаторов файлов сохрпаненных в источник</returns>
        public virtual ICollection<int> SaveFilesRange(HttpFileCollection files, string path, FileArea area, FileType fileType)
        {
            List<int> fileIdentifiers = new List<int>();
            foreach (string key in files)
            {
                var file = files[key];
                var isNotAllowedExt = NotSupportedExtensions
                    .Any(e => e == Path.GetExtension(file.FileName));

                if (file == null || isNotAllowedExt)
                {
                    continue;
                }

                var id = SaveFile(file, path, area, fileType);

                fileIdentifiers.Add(id);
            }

            return fileIdentifiers;
        }

        /// <summary>
        /// Создает файл, сохраняя его в каталог с посл-м сохранением информации в источник данных.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        /// <returns>Набор идентификатор файла сохраненного в источник</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="HttpException"></exception>
        /// <exception cref="Exception"></exception>
        public virtual int SaveFile(HttpPostedFile file, string path, FileArea area, FileType fileType)
        {
            var fileEntity = new FileEntity();

            if (CheckNotSupportedFile(file.FileName))
            {
                throw new NotSupportedException("File not supported");
            }

            try
            {
                file.SaveAs(path);

                fileEntity.Name = file.FileName;
                fileEntity.Alt = fileEntity.FileName =
                    Path.GetFileName(file.FileName);
                fileEntity.FileExtension = Path.GetExtension(file.FileName);
                fileEntity.FileType = fileType;
                fileEntity.FileArea = area;
                FileService.Edit(fileEntity);
            }
            catch (HttpException httpEx)
            {
                //TODO LOG
                throw httpEx;
            }
            catch (Exception ex)
            {                
                //TODO LOG
                throw ex;
            }

            return fileEntity.Id;
        }

        protected bool CheckNotSupportedFile(string fileName)
        {
            return NotSupportedExtensions.Any(x => Path.GetExtension(fileName) == x);
        }
        protected bool InitialDirs(string path)
        {
            bool check = true;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return check;
        }

    }
}