using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Sertification :BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photos { get; set; }
    }
}
