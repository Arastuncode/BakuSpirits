using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Models
{
    public class Company: BaseEntity  
    {
        public string Desc { get; set; }
        public string Image { get; set; }

    }
}
