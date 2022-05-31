using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels.Admin
{
    public class CardVM
    {
        public int Id { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        public string Desc { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
