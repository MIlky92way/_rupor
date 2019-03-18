using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Logik.Base.Model
{
    public class BaseModel
    {

        public BaseModel()
        {
            CountOnPage = 10;
            Page = 1;
        }

        public int CountOnPage { get; set; }
        public int TotalItems { get; set; }//total elements from  data
        public int Page { get; set; } //pages
        public int Total
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / CountOnPage); }
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
