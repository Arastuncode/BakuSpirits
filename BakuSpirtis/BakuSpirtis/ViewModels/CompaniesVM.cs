using BakuSpirtis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels
{
    public class CompaniesVM
    {
        public List<Company> Companies { get; set; }
        public List<Corusel> Corusels { get; set; }
        public List<CompanyVideo> CompanyVideos { get; set; }
        public List<Sertification> Sertifications { get; set; }

    }
}
