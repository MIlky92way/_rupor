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

       

        protected readonly HttpContextBase HttpContext;
        protected HttpServerUtilityBase Server { get; }
        protected IFileService FileService { get; }
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

        /// <summary>
        /// Сохранение набора файлов. 
        /// </summary>
        /// <param name="files">Набор сохраняемых файлов</param>
        /// <param name="path">Путь куда будет сохранен файл</param>
        public void SaveFilesRange(HttpFileCollection files, string path, bool isPic = false)
        {
            foreach (string key in files)
            {
                var file = files[key];
                var isNotAllowedExt = NotSupportedExtensions
                    .Any(e => e == Path.GetExtension(file.FileName));

                if (file == null || isNotAllowedExt)
                {
                    continue;
                }

                SaveFile(file, path, isPic);
            }
        }

        /// <summary>
        /// Создает файл, сохраняя его в каталог с посл-м сохранением информации в источник данных.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        public int SaveFile(HttpPostedFile file, string path, bool isPic = false)
        {

            var fileEntity = new FileEntity();
            try
            {
              

                fileEntity.Alt = fileEntity.FileName =
                    Path.GetFileName(file.FileName);

                fileEntity.FileExtension = Path.GetExtension(file.FileName);

                FileService.Edit(fileEntity);
            }                            
            catch (Exception ex)
            {
                //TODO LOG
                throw ex;
            }

            file.SaveAs(path);

            return fileEntity.Id;
        }


        private bool InitialDirs(string path)
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