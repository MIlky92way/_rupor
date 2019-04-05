using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Base.Models
{
    public class Model
    {

        public Model()
        {
            Pages = 10;
            Page = 1;
        }

        public int Pages { get; set; }
        public int Items { get; set; }//total elements from  data
        public int Page { get; set; } //pages
        public int Total
        {
            get { return (int)Math.Ceiling((decimal)Items / Pages); }
        }

        public int[] CountOnPageArray
        {
            get
            {
                return new int[] { 10, 30, 50, 70 };
            }
        }
    }
}
