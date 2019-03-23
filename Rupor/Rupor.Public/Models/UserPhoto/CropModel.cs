using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models.UserPhoto
{
    public class CropModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FileId { get; set; }
    }
}