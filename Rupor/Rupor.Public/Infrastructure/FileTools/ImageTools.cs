using Rupor.Domain.Entities.File;
using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools.Additional;
using Rupor.Tools.Consts;
using Rupor.Tools.Extend;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rupor.Public.Infrastructure.FileTools
{

    /// <summary>
    /// Обработчик изображений
    /// </summary>
    public class ImageTools : FileTools
    {
        private const int _ThumbZise = 256;
        public readonly string _PicPathProfile = $"{_RootDir}/protected/profile/";
        private readonly string _PicPathArticle = $"{_RootDir}/protected/article/";
        private readonly string _PicPathSection = $"{_RootDir}/protected/section/";
        private readonly string _PicPathFeedChannel = $"{_RootDir}/protected/feed";
        private string fileName;
        private string fileExtension;

        public ImageTools()
            : base(new FileService())
        {
            //_PicPathArticle;
            //_PicPathProfile = Server.MapPath(_PicPathProfile);
            //_PicPathSection = Server.MapPath(_PicPathSection);
        }

        #region Save images
        /// <summary>
        /// Сохраняет изображение учитывая сохранение в иcточник.
        /// </summary>
        /// <param name="file">Файл</param>
        /// <param name="imageArea">Область контекста</param>
        /// <param name="compressed">Сжатие</param>
        /// <param name="crop">Обрезка</param>
        /// <param name="saveData">Данные для работы с изо.</param>
        /// <returns></returns>
        public int SaveImage(HttpPostedFileBase file, FileArea imageArea, bool compressed = true, bool crop = false, bool isDefault = false, ImageSaveData saveData = null)
        {
            var fileEntity = new FileEntity();
            bool successSave = false;
            var path = string.Empty;

            fileExtension = Path.GetExtension(file.FileName).ToLower().Substring(1);
            fileName = FileNameExtend.GetRandomFileName(11);
            fileName = $"{fileName}.{fileExtension}";

            path = InitialPath(imageArea, isDefault);

            using (var stream = file.InputStream)
            {
                if (compressed && crop)
                {
                    successSave = Save(file, path, compressed, crop, saveData);
                }
                else if (compressed)
                    successSave = Save(file, path, compressed);
                else
                    successSave = Save(file, path);
            }


            if (successSave)
            {
                if (crop)
                    fileEntity = SaveFileEntity(imageArea, fileName, crop, isDefault, file.ContentType, fileEntity);
                else
                    fileEntity = SaveFileEntity(imageArea, fileName, crop, isDefault, file.ContentType, fileEntity);
            }

            return fileEntity.Id;
        }

        public int SaveImageFromStream(Stream stream, int fileId, FileArea imageArea, bool compressed = true, bool crop = false, bool isDefault = false, ImageSaveData saveData = null)
        {
            if (stream == null)
                throw new ArgumentException("stream can't be null!");

            if (fileId == 0)
                throw new ArgumentException("fileId can't be zero!");
            FileEntity minFile = null;
            var path = string.Empty;
            var originalFile = FileService[fileId];
            var successSave = false;
            fileExtension = originalFile.FileExtension;
            fileName = FileNameExtend.GetRandomFileName(11);

            path = path = InitialPath(imageArea, isDefault);

            using (stream)
            {
                if (compressed && crop)
                {
                    successSave = SaveImageFromStream(stream, path, compressed, crop, saveData);
                }
                else if (compressed)
                    successSave = SaveImageFromStream(stream, path, compressed);
                else
                    successSave = SaveImageFromStream(stream, path);
            }


            if (successSave)
            {
                if (crop)
                    minFile = SaveFileEntity(imageArea, fileName, crop, isDefault, originalFile.ContentType);
                else
                    minFile = SaveFileEntity(imageArea, fileName, crop, isDefault, originalFile.ContentType);
            }
            return minFile.Id;
        }

        private bool SaveImageFromStream(Stream stream, string path, bool compressed = true, bool crop = false, ImageSaveData saveData = null)
        {
            ImageCodecInfo jpgEncoder = null;
            EncoderParameters encoderParams = null;

            try
            {
                using (stream)
                {
                    using (Bitmap srcImg = new Bitmap(stream))
                    {
                        if (compressed)
                        {
                            var myEncoder = Encoder.Quality;
                            var myEncoderParameter = new EncoderParameter(myEncoder, 50L);

                            if (fileExtension == ImageFormat.Png.ToString().ToLower())
                                jpgEncoder = GetEncoder(ImageFormat.Png);
                            else if (fileExtension == ImageFormat.Bmp.ToString().ToLower())
                                jpgEncoder = GetEncoder(ImageFormat.Bmp);
                            else
                                jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                            encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = myEncoderParameter;

                        }
                        if (crop)
                        {
                            if (saveData == null)
                            {
                                throw new ArgumentNullException("saveData");
                            }
                            Rectangle rect = new Rectangle(saveData.X, saveData.Y, saveData.Width, saveData.Height);
                            Bitmap cropImg = srcImg.Clone(rect, srcImg.PixelFormat);
                            if (compressed)
                                cropImg.Save(path, jpgEncoder, encoderParams);
                            else
                                cropImg.Save(path);

                            cropImg.Dispose();
                        }
                        else
                        {
                            if (compressed)
                                srcImg.Save(path, jpgEncoder, encoderParams);
                            else
                                srcImg.Save(path);
                        }
                    }
                }


                return true;
            }
            catch
            {
                return false;
            }

        }


        private bool Save(HttpPostedFileBase file, string path, bool compressed = true, bool crop = false, ImageSaveData saveData = null)
        {
            using (Stream stream = file.InputStream)
            {
                return SaveImageFromStream(stream, path, compressed, crop, saveData);
            }
        }


        private FileEntity SaveFileEntity(FileArea area, string fileName, bool crop, bool isDefault, string contentType, FileEntity entity = null)
        {
            entity = entity ?? new FileEntity();
            try
            {
                if (isDefault)
                {

                    var defaultImage =
                        FileService.Get(f => f.IsDefault && f.FileType == FileType.Image && f.FileArea == area);

                    if (defaultImage != null)
                    {
                        FileService.Remove(defaultImage.Select(x => x.Id).ToArray());
                    }
                }

                if (crop)
                    entity.Alt = entity.Name = entity.FileName =
                        $"thumb-{fileName}";
                else
                    entity.Alt = fileName;

                entity.FileExtension = fileExtension;
                entity.IsPicture = true;
                entity.FileType = FileType.Image;
                entity.FileArea = area;
                entity.ContentType = contentType;
                entity.Name = entity.FileName = fileName;
                entity.Alt = fileName;
                entity.IsDefault = isDefault;
                entity = FileService.Edit(entity);
            }
            catch (Exception ex)
            {
                //TODO log
                throw ex;
            }

            return entity;
        }

        #endregion

        #region get images

        public FileStreamResult GetImage(FileArea area, int fileId)
        {
            var path = string.Empty;
            if (fileId == 0)
            {
                throw new ArgumentException("fileId is required!");
            }
            var file = FileService[fileId];
            if (file == null)
            {
                throw new NullReferenceException("file");
            }
            switch (area)
            {
                case FileArea.Article:
                    path = Server.MapPath($"{_PicPathArticle}/{file.Name}");
                    break;
                case FileArea.Section:
                    path = Server.MapPath($"{_PicPathSection}/{file.Name}");
                    break;
                case FileArea.FeedChannel:
                    path = Server.MapPath($"{_PicPathFeedChannel}/{file.Name}");
                    break;
                case FileArea.Profile:
                default:
                    path = Server.MapPath($"{_PicPathProfile}/{file.Name}");
                    break;
            }
            return new FileStreamResult(GetFile(path), file.ContentType);
        }

        public FileStreamResult GetDefaultImage(FileArea area, int id = 0)
        {
            var path = string.Empty;
            var defaultPath = string.Empty;
            FileStreamResult result = null;
            var file = FileService.Get(f => f.FileArea == area && f.FileType == FileType.Image && f.IsDefault && id == 0 || f.Id == id)
                .OrderByDescending(f => f.DateCreate)
                .FirstOrDefault();

            if (file == null)
            {
                return null;
            }

            path = defaultPath = Server.MapPath($"{AppFilePath.DefaultPathImage}/{file.Name}");

            try
            {
                if (id > 0)
                {
                    path = GetPathByArea(area, file);

                    result = new FileStreamResult(GetFile(path), file.ContentType);
                }
            }
            catch (FileNotFoundException ex)
            {
                result = new FileStreamResult(GetFile(defaultPath), file.ContentType);
            }
            catch (Exception ex)
            {
                result = new FileStreamResult(GetFile(defaultPath), file.ContentType);
            }

            return result;
        }

        #endregion

        #region tool

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


        private void RemovePhysImage(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            file.Delete();
        }
        #endregion

        private string InitialPath(FileArea imageArea, bool isDefault)
        {
            var path = string.Empty;
            switch (imageArea)
            {
                case FileArea.Article:

                    if (isDefault)
                        path = AppFilePath.DefaultPathImage;
                    else
                        path = _PicPathArticle;

                    if (InitialDirs(Server.MapPath(path)))
                        path = Server.MapPath($"{path}/{fileName}");

                    break;
                case FileArea.Section:

                    if (isDefault)
                        path = AppFilePath.DefaultPathImage;
                    else
                        path = _PicPathSection;

                    if (InitialDirs(Server.MapPath(path)))
                        path = Server.MapPath($"{path}/{fileName}");

                    break;

                case FileArea.FeedChannel:
                    path = Server.MapPath(_PicPathFeedChannel);
                    if (InitialDirs(path))
                        path = Server.MapPath($"{path}/{fileName}");
                    break;
                case FileArea.Profile:
                default:

                    if (isDefault)
                        path = AppFilePath.DefaultPathImage;
                    else
                        path = _PicPathProfile;

                    if (InitialDirs(Server.MapPath(path)))
                        path = Server.MapPath($"{path}/{fileName}");

                    break;
            }

            return path;
        }

        private string GetPathByArea(FileArea area, FileEntity file)
        {
            var path = string.Empty;
            switch (area)
            {
                case FileArea.Profile:
                    path = Server.MapPath($"{_PicPathProfile}/{file.Name}");
                    break;
                case FileArea.Article:
                    path = Server.MapPath($"{_PicPathArticle}/{file.Name}");
                    break;
                case FileArea.Section:
                    path = Server.MapPath($"{_PicPathSection}/{file.Name}");
                    break;
                case FileArea.FeedChannel:
                    path = Server.MapPath($"{_PicPathFeedChannel}/{file.Name}");
                    break;
                default:

                    break;
            }

            return path;
        }
    }
}