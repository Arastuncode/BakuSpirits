using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Partners: BaseEntity
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
