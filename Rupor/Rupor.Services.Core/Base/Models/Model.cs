using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Services.Core.Base.Models
{
    public class BaseModel
    {

        public BaseModel()
        {
            CountOnPage = 15;
            Page = 1;
        }

        public int CountOnPage { get; set; }
        public int Items { get; set; }//total elements from  data
        public int Page { get; set; } //curr page

        public int Total
        {
            get { return (int)Math.Ceiling((decimal)Items / CountOnPage); }
        }

        public int[] CountOnPageArray
        {
            get
            {
                return new int[] { 15, 30, 50, 70 };
            }
        }
    }
}
