﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Controllers
{
    public class PartnersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}