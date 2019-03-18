using Rupor.Domain.Entities.File;
using Rupor.Logik.File;
using Rupor.Public.Infrastructure.FileTools.Additional;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Rupor.Public.Infrastructure.FileTools
{
    public enum ImageArea
    {
        Profile,
        Article,
        Section
    }

    public class ImageTools : FileTools
    {
        private const int _ThumbZise = 256;
        private readonly string _PicPathProfile = $"{_RootDir}/protected/profile/";
        private readonly string _PicPathArticle = $"{_RootDir}/protected/article/";
        private readonly string _PicPathSection = $"{_RootDir}/protected/section/";

        public ImageTools(HttpContextBase context, HttpServerUtilityBase server, IFileService fileSrv)
            : base(context, server, fileSrv)
        {
            _PicPathArticle = Server.MapPath(_PicPathArticle);
            _PicPathProfile = Server.MapPath(_PicPathProfile);
            _PicPathSection = Server.MapPath(_PicPathSection);
        }

        /// <summary>
        /// Сохраняет 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="imageArea"></param>
        /// <param name="compressed"></param>
        /// <param name="crop"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
        public int SaveImage(HttpPostedFile file, ImageArea imageArea, bool compressed = true, bool crop = false, ImageSaveData saveData = null)
        {
            var fileEntity = new FileEntity();
            bool successSave = false;
            string path = string.Empty;

            switch (imageArea)
            {
                case ImageArea.Article:
                    path = $"{_PicPathArticle}/{file.FileName}";
                    break;
                case ImageArea.Section:
                    path = $"{_PicPathSection}/{file.FileName}";
                    break;
                case ImageArea.Profile:

                default:
                    path = $"{_PicPathProfile}/{file.FileName}";
                    break;
            }
                            
            if (compressed && crop)
            {
                successSave = Save(file, path, compressed, crop, saveData);
            }                
            else if (compressed)
                successSave = Save(file, path, compressed);
            else
                successSave = Save(file, path);

            if (successSave)
            {
                SaveFileEntity(file.FileName, crop);

                if (crop)
                {
                    SaveFileEntity(file.FileName, crop);
                }
            }
            return fileEntity.Id;
        }


        public bool Save(HttpPostedFile file, string path, bool compressed = true, bool crop = false, ImageSaveData saveData = null)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            EncoderParameters encoderParams = null;

            using (Stream stream = file.InputStream)
            {
                using (Bitmap srcImg = new Bitmap(stream))
                {
                    if (compressed)
                    {
                        var myEncoder = Encoder.Quality;
                        var myEncoderParameter = new EncoderParameter(myEncoder, 50L);

                        if (Path.GetExtension(path).ToLower() == "png")
                            jpgEncoder = GetEncoder(ImageFormat.Png);
                        else if(Path.GetExtension(path).ToLower() == "bmp")
                            jpgEncoder = GetEncoder(ImageFormat.Bmp);
                        else
                            jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                        encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = myEncoderParameter;

                    }
                    if (crop)
                    {
                        Rectangle rect = new Rectangle(saveData.X, saveData.Y, saveData.Width, saveData.Height);
                        using (Bitmap cropImg = new Bitmap(saveData.Width, saveData.Height))
                        {
                            using (Graphics g = Graphics.FromImage(cropImg))
                            {
                                g.DrawImage(srcImg, cropImg.Width, cropImg.Height, rect, GraphicsUnit.Pixel);
                            }

                            if (compressed)
                                cropImg.Save(path, jpgEncoder, encoderParams);

                            cropImg.Save(path);

                        }
                    }
                    else
                    {
                        if (compressed)
                            srcImg.Save(path, jpgEncoder, encoderParams);

                        srcImg.Save(path);
                    }
                }
            }

            return true;
        }

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

        private FileEntity SaveFileEntity(string fileName,bool crop)
        {
            FileEntity file = new FileEntity();
            try
            {                
                if (crop)
                    file.Alt = file.Name = 
                        $"{Guid.NewGuid().ToString().Split('-')[2]}-th";
                else
                    file.Alt = file.Name = Guid.NewGuid().ToString().Split('-')[2];

                file.FileExtension = Path.GetExtension(fileName);
                file.Picture = true;
                FileService.Edit(file);                
            }
            catch (Exception ex)
            {
                //TODO log
                throw ex;
            }

            return file;
        }
    }
}