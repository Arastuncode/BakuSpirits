using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
