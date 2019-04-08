using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rupor.Public.Models
{
    
    public class BaseModel
    {
        public BaseModel()
        {
            SuccessCnages = true;
        }
        public bool Change { get; set; }
        public bool SuccessCnages { get; set; }
        public string Message { get; set; }

    }
}