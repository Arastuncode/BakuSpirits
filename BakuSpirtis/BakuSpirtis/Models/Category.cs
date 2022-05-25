using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Category: BaseEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "Uzunluq cox ola bilmez")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
