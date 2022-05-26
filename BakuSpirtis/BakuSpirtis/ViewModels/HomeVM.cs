using BakuSpirtis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<About> Abouts { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<Advertisment> Advertisments { get; set; }
        public List<Galery> Galeries { get; set; }
    }
}
