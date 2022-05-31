using BakuSpirtis.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels
{
    public class ProductVM
    {
        public List<ProductImage> ProductImages { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public string Name { get; set; }
        

    }
}
