using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels.Admin
{
    public class CompanyVideoVM
    {
        public int Id { get; set; }
        [Required]
        public List<IFormFile> Video { get; set; }
        public string Videoes { get; set; }
    }
}
