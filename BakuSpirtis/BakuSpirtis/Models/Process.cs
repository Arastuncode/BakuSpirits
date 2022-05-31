using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Process :BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Text { get; set; }

    }
}
