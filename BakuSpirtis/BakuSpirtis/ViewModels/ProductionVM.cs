using BakuSpirtis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.ViewModels
{
    public class ProductionVM
    {
        public List<Process> Processes { get; set; }
        public List<Card> Cards { get; set; }
    }
}
