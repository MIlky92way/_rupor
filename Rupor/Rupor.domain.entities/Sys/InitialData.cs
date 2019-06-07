using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rupor.Domain.Entities.Sys
{
    [Table("_InitialData")]
    public class InitialData
    {
        public int Id { get; set; }
        public DateTime DateInitial { get; set; }
        public string InitialName { get; set; }

    }
}
