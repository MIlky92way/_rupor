using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Profile.Model
{
    public class PictureModel
    {
        public int ProfileId { get; set; }
        public string UrlToOriginalImage { get; set; }
        public string UrlToMinImage { get; set; }
    }
}
