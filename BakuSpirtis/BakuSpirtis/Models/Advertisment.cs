using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Advertisment:BaseEntity
    {
        public string Video { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Videos { get; set; }
    }
}
